using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NLog;
using SmartHotEdit.Controller;
using SmartHotEditPluginHost;
using SmartHotEditPluginHost.Model;

namespace SmartHotEdit.View
{
    /// <summary>
    ///     Description of EditForm.
    /// </summary>
    public partial class EditForm : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly MainController _mainController;
        private List<Argument> _argumentsToFill;
        private Function _currentFunction;
        private APlugin _currentPlugin;

        private int _currentPopupState;
        private int _listIndex = -1;

        public EditForm(MainController mainController)
        {
            Logger.Trace("Init EditForm now");

            InitializeComponent();

            this._mainController = mainController;
            this._currentPopupState = (int) PopupState.Small;
        }

        private void EditFormLoad(object sender, EventArgs e)
        {
            // init textbox with clipboard text
            Logger.Trace("Clipboard contains text?: " + Clipboard.ContainsText());
            if (Clipboard.ContainsText())
            {
                this.clipboardTextBox.Text = Clipboard.GetText();
            }

            // add columns
            pluginList.Columns.Add("Plugin", -2, HorizontalAlignment.Left);
            pluginList.Columns.Add("Description", -2, HorizontalAlignment.Left);

            Logger.Debug("Add plugins to view");

            foreach (var plugin in this._mainController.PluginController.EnabledPlugins)
            {
                var tmpPluginEntry = new ListViewItem
                {
                    Tag = plugin,
                    Text = plugin.Name
                };
                tmpPluginEntry.SubItems.Add(plugin.Description);
                pluginList.Items.Add(tmpPluginEntry);
                Logger.Info($"Plugin added to view: [{plugin.Type}] {plugin.Name}");
            }

            pluginList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void PluginListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Logger.Trace("PluginListKeyDown > Enter");
                if (!string.IsNullOrEmpty(clipboardTextBox.Text))
                {
                    Logger.Debug("Set text to clipboard: " + clipboardTextBox.Text);
                    Clipboard.SetText(clipboardTextBox.Text);
                }
                this.Close();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Space))
            {
                Logger.Trace("PluginListKeyDown > Control + Space");
                if (this.pluginList.SelectedItems.Count > 0)
                {
                    var selectedItem = pluginList.SelectedItems[0];
                    this.functionListUpDown.Items.Clear();

                    var plugin = (APlugin) selectedItem.Tag;
                    Logger.Debug("Selected plugin: " + plugin.Name);

                    // ReSharper disable once CoVariantArrayConversion
                    this.functionListUpDown.Items.AddRange(plugin.GetFunctionsAsArray());

                    _currentPopupState = (int) PopupState.Small;

                    this.functionListUpDown.Left = selectedItem.ListView.Columns[0].Width;
                    this.functionListUpDown.Top = selectedItem.Bounds.Top;
                    this.functionListUpDown.Visible = true;
                    this.functionListUpDown.Focus();
                }
                e.SuppressKeyPress = true;
            }
        }

        private void FunctionListUpDownKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                // TODO refactor!
                Logger.Trace("FunctionListUpDownKeyDown > Enter");
                var myFunction = this.functionListUpDown.SelectedItem as Function;
                var plugin = (APlugin) this.pluginList.SelectedItems[0].Tag;
                if (myFunction != null && plugin != null)
                {
                    Logger.Debug($"Selected function '{myFunction.Name}' of plugin '{plugin.Name}'.");
                    // ReSharper disable once PassStringInterpolation
                    Logger.Debug("Function has: {0} arguments",
                        myFunction.Arguments == null || myFunction.Arguments.Count == 0 ? 0 : myFunction.Arguments.Count);
                    if (myFunction.Arguments != null && myFunction.Arguments.Count > 0)
                    {
                        if (!myFunction.Arguments.Any())
                            return;
                        Logger.Trace("Open argument inputs");

                        this._currentPopupState = (int) PopupState.Small;
                        this._argumentsToFill = myFunction.Arguments;
                        this._currentFunction = myFunction;
                        this._currentPlugin = plugin;
                        this._listIndex = 0;

                        // get selected item to calculate position of input
                        var selectedItem = this.pluginList.SelectedItems[0];

                        argumentPanel.LabelText = myFunction.Arguments[this._listIndex].Description;
                        argumentPanel.InputText = "";
                        argumentPanel.Left = selectedItem.ListView.Columns[0].Width;
                        argumentPanel.Top = selectedItem.Bounds.Top;
                        argumentPanel.Visible = true;
                        argumentPanel.Focus();
                    }
                    else
                    {
                        this.clipboardTextBox.Text = this.ExecuteFunctionOfPlugin(this.clipboardTextBox.Text, plugin,
                            myFunction, true, this._argumentsToFill);
                    }
                    this.functionListUpDown.Visible = false;
                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Space))
            {
                Logger.Trace("FunctionListUpDownKeyDown > Control + Space");
                Logger.Trace("Change size of functionListUpDown input");
                if (this._currentPopupState == (int) PopupState.Small)
                {
                    this._currentPopupState = (int) PopupState.Big;
                    this.functionListUpDown.Height = this.functionListUpDown.PreferredHeight;
                }
                else
                {
                    this._currentPopupState = (int) PopupState.Small;
                    this.functionListUpDown.Height = this.functionListUpDown.ItemHeight;
                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Logger.Trace("FunctionListUpDownKeyDown > Escape");
                this.functionListUpDown.Visible = false;
                e.SuppressKeyPress = true;
            }
        }

        private void argumentPanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Logger.Trace("FunctionArgumentInputKeyDown > Enter");

                this._argumentsToFill[_listIndex++].Value = this.argumentPanel.InputText;
                Logger.Trace("Argument '" + (_listIndex - 1) + "' filled with: " + this.argumentPanel.InputText);
                if (_listIndex >= _argumentsToFill.Count)
                {
                    Logger.Trace("No further arguments");
                    this.argumentPanel.Visible = false;
                    _listIndex = -1;
                    this.clipboardTextBox.Text = this.ExecuteFunctionOfPlugin(this.clipboardTextBox.Text,
                        this._currentPlugin, this._currentFunction, true, this._argumentsToFill);
                    _argumentsToFill = null;
                    _currentPlugin = null;
                }
                else
                {
                    // TODO refactor!
                    var selectedItem = this.pluginList.SelectedItems[0];
                    var myFunction = this.functionListUpDown.SelectedItem as Function;

                    Logger.Trace("More arguments found");
                    // get selected item to calculate position of input
                    if (myFunction != null) argumentPanel.LabelText = myFunction.Arguments[this._listIndex].Description;
                    argumentPanel.InputText = "";
                    argumentPanel.Left = selectedItem.ListView.Columns[0].Width;
                    argumentPanel.Top = selectedItem.Bounds.Top;
                    argumentPanel.Visible = true;
                    argumentPanel.Focus();
                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Logger.Trace("FunctionArgumentInputKeyDown > Escape");
                this.argumentPanel.Visible = false;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        ///     Execute the function of the given plugin on the text and returned it. Can be executed for each line or for the
        ///     whole text.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="plugin"></param>
        /// <param name="function"></param>
        /// <param name="forEachLine"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private string ExecuteFunctionOfPlugin(string text, APlugin plugin, Function function, bool forEachLine = true,
            List<Argument> arguments = null)
        {
            Logger.Trace($"[{plugin.Type}] Plugin '{plugin.Name}' will execute function '{function.Name}'");
            string result = null;
            try
            {
                result = "";
                if (text != null)
                {
                    // TODO create option for this
                    if (forEachLine)
                    {
                        var lines = text.Split('\n');
                        result = lines.Aggregate(result,
                            (current, line) =>
                                current + plugin.GetResultFromFunction(function, line, arguments) + Environment.NewLine);
                    }
                    else
                    {
                        result = plugin.GetResultFromFunction(function, text, arguments);
                    }
                    this.toolStripStatusLabel.Text =
                        $"[{plugin.Type}] Plugin '{plugin.Name}' has executed function '{function.Name}'";
                }
            }
            catch (Exception e)
            {
                Logger.Error("An Error occured " + "(" + e.Source + "): " + e.Message + "\n" + e.StackTrace);
                this.toolStripStatusLabel.Text =
                    $"[{plugin.Type}] Plugin '{plugin.Name}' has an error in function '{function.Name}': {e.Message}";
            }
            return result;
        }

        private enum PopupState
        {
            Small = 0,
            Big = 1
        }
    }
}