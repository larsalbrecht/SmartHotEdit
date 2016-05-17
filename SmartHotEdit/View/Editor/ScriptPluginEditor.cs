using NLog;
using NLog.Config;
using ScintillaNET;
using SmartHotEdit.Abstracts;
using SmartHotEdit.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartHotEdit.View.Editor
{
    public partial class ScriptPluginEditor : Form
    {
        protected static Logger logger;

        private String originalHeadertext;
        private MainController mainController;
        private IList<AScriptPluginController> scriptPluginController;
        private String _filepathToSave;
        private Boolean _isSaved = true;

        private Boolean IsSaved
        {
            get { return _isSaved; }
            set
            {
                _isSaved = value;
                var baseText = this.originalHeadertext + " -" + (_filepathToSave != null ? " [" + _filepathToSave + "]" : "" );
                if (_isSaved == false)
                {
                    this.Text = baseText + " *";
                } else
                {
                    this.Text = baseText;
                }
            }
        }

        private String FilepathToSave
        {
            get { return _filepathToSave; }
            set
            {
                _filepathToSave = value;
                this.Text = this.originalHeadertext + " - [" + _filepathToSave + "]";
            }
        }


        private int maxLineNumberCharLength;

        public ScriptPluginEditor(MainController mainController)
        {
            InitializeComponent();

            this.originalHeadertext = this.Text;
            this.mainController = mainController;
            this.scriptPluginController = this.mainController.getPluginController().getPluginControllerList(typeof(AScriptPluginController)).Cast<AScriptPluginController>().ToList();
            this.openScriptDialog.Filter = this.getFilterForDialog();
            this.saveScriptDialog.Filter = this.getFilterForDialog();
            var bindingSource = new BindingSource();
            bindingSource.DataSource = this.scriptPluginController;
            this.scriptTypeList.DataSource = bindingSource;
            this.scriptTypeList.DisplayMember = "Type";
            //this.scriptTypeList.ValueMember = "TypeScintillaLexer";
        }

        private AScriptPluginController[] getItemsForScriptTypeBox()
        {
            return this.scriptPluginController.ToArray<AScriptPluginController>();
        }

        private String getFilterForDialog()
        {
            var resultList = new List<string>();
            foreach (AScriptPluginController pluginController in this.scriptPluginController)
            {
                resultList.Add(pluginController.Type + "|" + "*." + pluginController.TypeFileExt);
            }
            return string.Join("|", resultList);
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            var pluginController = ((AScriptPluginController)this.scriptTypeList.SelectedValue);
            this.openScriptDialog.DefaultExt = pluginController.TypeFileExt;

            DialogResult result = this.openScriptDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string scriptFile = this.openScriptDialog.FileName;
                this.FilepathToSave = scriptFile;
                try
                {
                    this.scintilla.Text = File.ReadAllText(scriptFile);
                    this.IsSaved = true;
                }
                catch (IOException ex)
                {
                    logger.Error(ex.Message);
                }
            }
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFile();
        }

        private bool saveFile()
        {
            if(this.FilepathToSave == null)
            {
                DialogResult result = this.saveScriptDialog.ShowDialog();
                if (result == DialogResult.OK && saveScriptDialog.FileName != "")
                {
                    this.FilepathToSave = saveScriptDialog.FileName;
                    logger.Debug("Set new save target: " + this.FilepathToSave);
                    try
                    {
                        if (this.writeFile(this.FilepathToSave, this.scintilla.Text))
                        {
                            this.IsSaved = true;
                            return true;
                        }
                    }
                    catch (IOException ex)
                    {
                        logger.Error(ex.Message);
                    }
                }
            } else
            {
                if (this.writeFile(this.FilepathToSave, this.scintilla.Text))
                {
                    this.IsSaved = true;
                    return true;
                }
            }
            return false;
        }

        private bool writeFile(string filePath, string text)
        {
            File.WriteAllText(filePath, text);
            logger.Debug("Filecontents write to: " + filePath);
            return true;
        }

        private void scintilla_TextChanged(object sender, EventArgs e)
        {
            this.IsSaved = false;

            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            this.maxLineNumberCharLength = maxLineNumberCharLength;
        }

        private void scriptTypeList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.scriptTypeList.SelectedValue != null && this.scriptTypeList.SelectedValue is AScriptPluginController)
            {
                var pluginController = ((AScriptPluginController)this.scriptTypeList.SelectedValue);
                this.scintilla.Lexer = pluginController.TypeScintillaLexer;
                this.saveScriptDialog.DefaultExt = pluginController.TypeFileExt;

                if (logger != null)
                {
                    logger.Debug("Lexer changed: " + this.scintilla.Lexer.ToString());
                }

            }
        }

        private void ScriptPluginEditor_Load(object sender, EventArgs e)
        {
            if (logger == null)
            {
                var config = LogManager.Configuration;
                var target = new NLog.Windows.Forms.RichTextBoxTarget();
                target.Name = "control";
                target.TargetRichTextBox = this.outputRichTextBox;
                target.Layout = @"${longdate} - ${uppercase:${level}} - ${message}";

                config.AddTarget("control", target);

                var rule = new NLog.Config.LoggingRule("SmartHotEdit.View.Editor.*", LogLevel.Info, target);
                config.LoggingRules.Add(rule);

                LogManager.Configuration = config;

                logger = LogManager.GetCurrentClassLogger();
                logger.Info("Logger initialized");
            }
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            if (this.IsSaved)
            {
                logger.Trace("File saved already, close");
                this.Close();
            } else
            {
                DialogResult dialogResult = MessageBox.Show("Sure", "Some Title", MessageBoxButtons.YesNoCancel);
                if (dialogResult != DialogResult.Cancel)
                {
                    if((dialogResult == DialogResult.OK && this.saveFile()) || dialogResult == DialogResult.No)
                    {
                        logger.Trace("Close after save");
                        this.Close();
                    } else
                    {
                        logger.Trace("Could not save, do not close");
                    }
                } else
                {
                    logger.Trace("Close canceled");
                }
            }
        }
    }
}
