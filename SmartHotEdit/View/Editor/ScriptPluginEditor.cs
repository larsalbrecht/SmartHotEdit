using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NLog;
using NLog.Config;
using NLog.Windows.Forms;
using ScintillaNET;
using SmartHotEdit.Controller;
using SmartHotEditPluginHost;

namespace SmartHotEdit.View.Editor
{
    public partial class ScriptPluginEditor : Form
    {
        protected static Logger logger;
        private AScriptPluginController _currentPluginController;

        private string _filepathToSave;
        private bool _isSaved = true;
        private readonly MainController mainController;


        private int maxLineNumberCharLength;

        private readonly string originalHeadertext;
        private readonly IList<AScriptPluginController> scriptPluginController;

        public ScriptPluginEditor(MainController mainController)
        {
            InitializeComponent();

            this.originalHeadertext = this.Text;
            this.mainController = mainController;
            this.scriptPluginController =
                this.mainController.PluginController.GetPluginControllerList(typeof(AScriptPluginController))
                    .Cast<AScriptPluginController>()
                    .ToList();
            this.openScriptDialog.Filter = this.getFilterForDialog();
            this.saveScriptDialog.Filter = this.getFilterForDialog();
            var bindingSource = new BindingSource();
            bindingSource.DataSource = this.scriptPluginController;
            this.scriptTypeList.DataSource = bindingSource;
            this.scriptTypeList.DisplayMember = "Type";
        }

        private bool IsSaved
        {
            get { return _isSaved; }
            set
            {
                _isSaved = value;
                var baseText = this.originalHeadertext + " -" +
                               (_filepathToSave != null ? " [" + _filepathToSave + "]" : "");
                if (_isSaved == false)
                {
                    this.Text = baseText + " *";
                }
                else
                {
                    this.Text = baseText;
                }
            }
        }

        private string FilepathToSave
        {
            get { return _filepathToSave; }
            set
            {
                _filepathToSave = value;
                this.Text = this.originalHeadertext + " - [" + _filepathToSave + "]";
            }
        }

        private AScriptPluginController[] getItemsForScriptTypeBox()
        {
            return this.scriptPluginController.ToArray();
        }

        private string getFilterForDialog()
        {
            var resultList = new List<string>();
            resultList.Add("All Files (*.*)|*.*");
            foreach (var pluginController in this.scriptPluginController)
            {
                resultList.Add(pluginController.Type + "|" + "*." + pluginController.TypeFileExt);
            }
            return string.Join("|", resultList);
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            this.openScriptDialog.DefaultExt = this._currentPluginController.TypeFileExt;

            var result = this.openScriptDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var scriptFile = this.openScriptDialog.FileName;
                this.FilepathToSave = scriptFile;
                foreach (var pluginController in this.scriptPluginController)
                {
                    Console.WriteLine(Path.GetExtension(scriptFile));
                    if ("." + pluginController.TypeFileExt == Path.GetExtension(scriptFile))
                    {
                        this._currentPluginController = pluginController;
                        break;
                    }
                }
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
            for (var i = 0; i < this.scriptTypeList.Items.Count; i++)
            {
                if (this.scriptTypeList.Items[i] == this._currentPluginController)
                {
                    this.scriptTypeList.SelectedIndex = i;
                    break;
                }
            }
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFile();
        }

        private bool saveFile()
        {
            if (this.FilepathToSave == null)
            {
                var result = this.saveScriptDialog.ShowDialog();
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
            }
            else
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
            var maxLineNumberCharLength = this.scintilla.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            this.scintilla.Margins[0].Width =
                this.scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            this.maxLineNumberCharLength = maxLineNumberCharLength;
        }

        private void scriptTypeList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.scriptTypeList.SelectedValue != null &&
                this.scriptTypeList.SelectedValue is AScriptPluginController)
            {
                this._currentPluginController = (AScriptPluginController) this.scriptTypeList.SelectedValue;
                this.scintilla.StyleResetDefault();
                this.scintilla.Styles[Style.Default].Font = "Consolas";
                this.scintilla.Styles[Style.Default].Size = 10;
                this.scintilla.StyleClearAll();

                this.scintilla.Lexer = _currentPluginController.TypeScintillaLexer;
                this._currentPluginController.SetScintillaConfiguration(this.scintilla);

                this.saveScriptDialog.DefaultExt = _currentPluginController.TypeFileExt;

                // enable/disable button for loading templating
                this.templateLoadMenuItem.Enabled = this._currentPluginController.GetTemplate() != null;

                if (logger != null)
                {
                    logger.Debug("Lexer changed: " + this.scintilla.Lexer);
                }
            }
        }

        private void ScriptPluginEditor_Load(object sender, EventArgs e)
        {
            if (logger == null)
            {
                var config = LogManager.Configuration;
                var target = new RichTextBoxTarget();
                target.Name = "control";
                target.TargetRichTextBox = this.outputRichTextBox;
                target.Layout = @"${longdate} - ${uppercase:${level}} - ${message}";

                config.AddTarget("control", target);

                var rule = new LoggingRule("SmartHotEdit.View.Editor.*", LogLevel.Info, target);
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
            }
            else
            {
                var dialogResult = MessageBox.Show("The current script is unsaved. Do you want to save?",
                    "Unsaved changes", MessageBoxButtons.YesNoCancel);
                if (dialogResult != DialogResult.Cancel)
                {
                    if ((dialogResult == DialogResult.OK && this.saveFile()) || dialogResult == DialogResult.No)
                    {
                        logger.Trace("Close after save");
                        this.Close();
                    }
                    else
                    {
                        logger.Trace("Could not save, do not close");
                    }
                }
                else
                {
                    logger.Trace("Close canceled");
                }
            }
        }

        private void templateLoadMenuItem_Click(object sender, EventArgs e)
        {
            if (this._currentPluginController != null)
            {
                var template = this._currentPluginController.GetTemplate();
                if (template != null)
                {
                    this.scintilla.Text = template;
                    this.outputRichTextBox.Clear();
                    logger.Info("Template loaded for " + this._currentPluginController.Type);
                }
                else
                {
                    MessageBox.Show(
                        "There is no template for this scriptplugin: " + this._currentPluginController.Type,
                        "No template found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void fileNewMenuItem_Click(object sender, EventArgs e)
        {
            if (this.IsSaved)
            {
                logger.Trace("File saved already, create new");
                this.scintilla.ClearAll();
                this._filepathToSave = null;
                this._isSaved = true;
            }
            else
            {
                var dialogResult = MessageBox.Show("The current script is unsaved. Do you want to save?",
                    "Unsaved changes", MessageBoxButtons.YesNoCancel);
                if (dialogResult != DialogResult.Cancel)
                {
                    if ((dialogResult == DialogResult.OK && this.saveFile()) || dialogResult == DialogResult.No)
                    {
                        this.scintilla.ClearAll();
                        this._filepathToSave = null;
                        this._isSaved = true;
                        this.Close();
                    }
                    else
                    {
                        logger.Trace("Could not save, do not close");
                    }
                }
                else
                {
                    logger.Trace("Close canceled");
                }
            }
        }

        private void runTestMenuItem_Click(object sender, EventArgs e)
        {
            var defaultValue = "n/a";
            functionsListView.Clear();
            try
            {
                var plugin = this._currentPluginController.GetPluginForScript(this.scintilla.Text);
                if (plugin != null)
                {
                    logger.Info("Plugin found: " + plugin.Name);

                    var functions = plugin.GetFunctionsAsArray();
                    this.nameLabel.Text = plugin.Name;

                    if (functions != null)
                    {
                        functionsListView.Columns.Add("Name", -2, HorizontalAlignment.Left);
                        functionsListView.Columns.Add("Description", -2, HorizontalAlignment.Left);

                        this.functionLabel.Text = functions.Length.ToString();

                        foreach (var function in functions)
                        {
                            var tmpFunctionEntry = new ListViewItem();
                            tmpFunctionEntry.Tag = function;
                            tmpFunctionEntry.Text = function.Name;
                            tmpFunctionEntry.SubItems.Add(function.Description);
                            functionsListView.Items.Add(tmpFunctionEntry);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("No valid plugin found. Exception during parsing by {0}",
                    this._currentPluginController.Type);
                logger.Error(ex.Message);
            }
            finally
            {
                this.nameLabel.Text = defaultValue;
                this.functionLabel.Text = defaultValue;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}