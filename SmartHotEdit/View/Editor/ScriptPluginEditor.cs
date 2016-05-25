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
        private static Logger _logger;
        private AScriptPluginController _currentPluginController;

        private string _filepathToSave;
        private bool _isSaved = true;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly MainController _mainController;


        private int _maxLineNumberCharLength;

        private readonly string _originalHeadertext;
        private readonly IList<AScriptPluginController> _scriptPluginController;

        public ScriptPluginEditor(MainController mainController)
        {
            InitializeComponent();

            this._originalHeadertext = this.Text;
            this._mainController = mainController;
            this._scriptPluginController =
                this._mainController.PluginController.GetPluginControllerList(typeof(AScriptPluginController))
                    .Cast<AScriptPluginController>()
                    .ToList();
            this.openScriptDialog.Filter = this.GetFilterForDialog();
            this.saveScriptDialog.Filter = this.GetFilterForDialog();
            var bindingSource = new BindingSource {DataSource = this._scriptPluginController};
            this.scriptTypeList.DataSource = bindingSource;
            this.scriptTypeList.DisplayMember = "Type";
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private bool IsSaved
        {
            get { return _isSaved; }
            set
            {
                _isSaved = value;
                var baseText = this._originalHeadertext + " -" +
                               (_filepathToSave != null ? " [" + _filepathToSave + "]" : "");
                if (_isSaved == false)
                {
                    this.Text = baseText + @" *";
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
                this.Text = this._originalHeadertext + @" - [" + _filepathToSave + @"]";
            }
        }

        private string GetFilterForDialog()
        {
            var resultList = new List<string> {"All Files (*.*)|*.*"};
            foreach (var pluginController in this._scriptPluginController)
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
                foreach (var pluginController in this._scriptPluginController)
                {
                    Console.WriteLine(Path.GetExtension(scriptFile));
                    if ("." + pluginController.TypeFileExt != Path.GetExtension(scriptFile)) continue;
                    this._currentPluginController = pluginController;
                    break;
                }
                try
                {
                    this.scintilla.Text = File.ReadAllText(scriptFile);
                    this.IsSaved = true;
                }
                catch (IOException ex)
                {
                    _logger.Error(ex.Message);
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
            this.SaveFile();
        }

        private bool SaveFile()
        {
            if (this.FilepathToSave == null)
            {
                var result = this.saveScriptDialog.ShowDialog();
                if (result != DialogResult.OK || saveScriptDialog.FileName == "") return false;
                this.FilepathToSave = saveScriptDialog.FileName;
                _logger.Debug("Set new save target: " + this.FilepathToSave);
                try
                {
                    if (WriteFile(this.FilepathToSave, this.scintilla.Text))
                    {
                        this.IsSaved = true;
                        return true;
                    }
                }
                catch (IOException ex)
                {
                    _logger.Error(ex.Message);
                }
            }
            else
            {
                if (!WriteFile(this.FilepathToSave, this.scintilla.Text)) return false;
                this.IsSaved = true;
                return true;
            }
            return false;
        }

        private static bool WriteFile(string filePath, string text)
        {
            File.WriteAllText(filePath, text);
            _logger.Debug("Filecontents write to: " + filePath);
            return true;
        }

        private void scintilla_TextChanged(object sender, EventArgs e)
        {
            this.IsSaved = false;

            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = this.scintilla.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this._maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            this.scintilla.Margins[0].Width =
                this.scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            this._maxLineNumberCharLength = maxLineNumberCharLength;
        }

        private void scriptTypeList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(this.scriptTypeList.SelectedValue is AScriptPluginController)) return;
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

            _logger?.Debug("Lexer changed: " + this.scintilla.Lexer);
        }

        private void ScriptPluginEditor_Load(object sender, EventArgs e)
        {
            if (_logger != null) return;
            var config = LogManager.Configuration;
            var target = new RichTextBoxTarget
            {
                Name = "control",
                TargetRichTextBox = this.outputRichTextBox,
                Layout = @"${longdate} - ${uppercase:${level}} - ${message}"
            };

            config.AddTarget("control", target);

            var rule = new LoggingRule("SmartHotEdit.View.Editor.*", LogLevel.Info, target);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;

            _logger = LogManager.GetCurrentClassLogger();
            _logger.Info("Logger initialized");
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            if (this.IsSaved)
            {
                _logger.Trace("File saved already, close");
                this.Close();
            }
            else
            {
                var dialogResult = MessageBox.Show("The current script is unsaved. Do you want to save?",
                    "Unsaved changes", MessageBoxButtons.YesNoCancel);
                if (dialogResult != DialogResult.Cancel)
                {
                    if ((dialogResult == DialogResult.OK && this.SaveFile()) || dialogResult == DialogResult.No)
                    {
                        _logger.Trace("Close after save");
                        this.Close();
                    }
                    else
                    {
                        _logger.Trace("Could not save, do not close");
                    }
                }
                else
                {
                    _logger.Trace("Close canceled");
                }
            }
        }

        private void templateLoadMenuItem_Click(object sender, EventArgs e)
        {
            if (this._currentPluginController == null) return;
            var template = this._currentPluginController.GetTemplate();
            if (template != null)
            {
                this.scintilla.Text = template;
                this.outputRichTextBox.Clear();
                _logger.Info("Template loaded for " + this._currentPluginController.Type);
            }
            else
            {
                MessageBox.Show(
                    "There is no template for this scriptplugin: " + this._currentPluginController.Type,
                    "No template found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void fileNewMenuItem_Click(object sender, EventArgs e)
        {
            if (this.IsSaved)
            {
                _logger.Trace("File saved already, create new");
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
                    if ((dialogResult == DialogResult.OK && this.SaveFile()) || dialogResult == DialogResult.No)
                    {
                        this.scintilla.ClearAll();
                        this._filepathToSave = null;
                        this._isSaved = true;
                        this.Close();
                    }
                    else
                    {
                        _logger.Trace("Could not save, do not close");
                    }
                }
                else
                {
                    _logger.Trace("Close canceled");
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
                if (plugin == null) return;

                _logger.Info("Plugin found: " + plugin.Name);

                var functions = plugin.GetFunctionsAsArray();
                this.nameLabel.Text = plugin.Name;

                if (functions == null) return;
                functionsListView.Columns.Add("Name", -2, HorizontalAlignment.Left);
                functionsListView.Columns.Add("Description", -2, HorizontalAlignment.Left);

                this.functionLabel.Text = functions.Length.ToString();

                foreach (var function in functions)
                {
                    var tmpFunctionEntry = new ListViewItem
                    {
                        Tag = function,
                        Text = function.Name
                    };
                    tmpFunctionEntry.SubItems.Add(function.Description);
                    functionsListView.Items.Add(tmpFunctionEntry);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("No valid plugin found. Exception during parsing by {0}",
                    this._currentPluginController.Type);
                _logger.Error(ex.Message);
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