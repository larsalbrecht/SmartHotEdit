using System;
using System.Windows.Forms;
using SmartHotEditPluginHost.Model;
using SmartHotEdit.Controller;
using System.Collections.Generic;
using System.Linq;
using SmartHotEditPluginHost;
using NLog;

namespace SmartHotEdit.View
{
    /// <summary>
    /// Description of EditForm.
    /// </summary>
    public partial class EditForm : Form
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        enum POPUP_STATE { Small, Big };

        private MainController mainController;

        private int _currentPopupState;
        private int _listIndex = -1;
        private List<Argument> _argumentsToFill = null;
        private APlugin _currentPlugin = null;
        private Function _currentFunction = null;

        public EditForm(MainController mainController)
        {
            logger.Trace("Init EditForm now");

            InitializeComponent();

            this.mainController = mainController;
            this._currentPopupState = (int)POPUP_STATE.Small;
        }

        private void EditFormLoad(object sender, EventArgs e)
        {
            // init textbox with clipboard text
            logger.Trace("Clipboard contains text?: " + Clipboard.ContainsText());
            if (Clipboard.ContainsText())
            {
                this.clipboardTextBox.Text = Clipboard.GetText();
            }

            // add columns
            pluginList.Columns.Add("Plugin", -2, HorizontalAlignment.Left);
            pluginList.Columns.Add("Description", -2, HorizontalAlignment.Left);

            logger.Debug("Add plugins to view");

            foreach (APlugin plugin in this.mainController.getPluginController().EnabledPlugins)
            {
                var tmpPluginEntry = new ListViewItem();
                tmpPluginEntry.Tag = plugin;
                tmpPluginEntry.Text = plugin.getName();
                tmpPluginEntry.SubItems.Add(plugin.getDescription());
                pluginList.Items.Add(tmpPluginEntry);
                logger.Info(String.Format("Plugin added to view: [{0}] {1}", plugin.Type, plugin.getName()));
            }

            pluginList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        void PluginListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Enter))
            {
                logger.Trace("PluginListKeyDown > Enter");
                if (clipboardTextBox.Text != null && clipboardTextBox.Text.Length > 0)
                {
                    logger.Debug("Set text to clipboard: " + clipboardTextBox.Text);
                    Clipboard.SetText(clipboardTextBox.Text);
                }
                this.Close();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Space))
            {
                logger.Trace("PluginListKeyDown > Control + Space");
                if (this.pluginList.SelectedItems.Count > 0)
                {
                    var selectedItem = pluginList.SelectedItems[0];
                    this.functionListUpDown.Items.Clear();

                    var plugin = (APlugin)selectedItem.Tag;
                    logger.Debug("Selected plugin: " + plugin.getName());

                    this.functionListUpDown.Items.AddRange(plugin.getFunctionsAsArray());

                    _currentPopupState = (int)POPUP_STATE.Small;

                    this.functionListUpDown.Left = (selectedItem.ListView.Columns[0].Width);
                    this.functionListUpDown.Top = selectedItem.Bounds.Top;
                    this.functionListUpDown.Visible = true;
                    this.functionListUpDown.Focus();
                }
                e.SuppressKeyPress = true;
            }
        }

        void FunctionListUpDownKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Enter))
            {
                // TODO refactor!
                logger.Trace("FunctionListUpDownKeyDown > Enter");
                var myFunction = this.functionListUpDown.SelectedItem as Function;
                var plugin = (APlugin)this.pluginList.SelectedItems[0].Tag;
                if (myFunction != null && plugin != null)
                {
                    logger.Debug(String.Format("Selected function '{0}' of plugin '{1}'.", myFunction.Name, plugin.getName()));
                    logger.Debug(String.Format("Function has: {0} arguments"), (myFunction.arguments == null || myFunction.arguments.Count == 0 ? 0 : myFunction.arguments.Count));
                    if (myFunction.arguments != null && myFunction.arguments.Count > 0)
                    {
                        if (!myFunction.arguments.Any())
                            return;
                        logger.Trace("Open argument inputs");

                        this._currentPopupState = (int)POPUP_STATE.Small;
                        this._argumentsToFill = myFunction.arguments;
                        this._currentFunction = myFunction;
                        this._currentPlugin = plugin;
                        this._listIndex = 0;

                        // get selected item to calculate position of input
                        var selectedItem = this.pluginList.SelectedItems[0];

                        argumentPanel.LabelText = myFunction.arguments[this._listIndex].description;
                        argumentPanel.InputText = "";
                        argumentPanel.Left = selectedItem.ListView.Columns[0].Width;
                        argumentPanel.Top = selectedItem.Bounds.Top;
                        argumentPanel.Visible = true;
                        argumentPanel.Focus();
                    }
                    else
                    {
                        this.clipboardTextBox.Text = this.executeFunctionOfPlugin(this.clipboardTextBox.Text, plugin, myFunction, true, this._argumentsToFill);
                    }
                    this.functionListUpDown.Visible = false;
                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Space))
            {
                logger.Trace("FunctionListUpDownKeyDown > Control + Space");
                logger.Trace("Change size of functionListUpDown input");
                if (this._currentPopupState == (int)POPUP_STATE.Small)
                {
                    this._currentPopupState = (int)POPUP_STATE.Big;
                    this.functionListUpDown.Height = this.functionListUpDown.PreferredHeight;
                }
                else
                {
                    this._currentPopupState = (int)POPUP_STATE.Small;
                    this.functionListUpDown.Height = this.functionListUpDown.ItemHeight;
                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                logger.Trace("FunctionListUpDownKeyDown > Escape");
                this.functionListUpDown.Visible = false;
                e.SuppressKeyPress = true;
            }
        }

        private void argumentPanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                logger.Trace("FunctionArgumentInputKeyDown > Enter");

                this._argumentsToFill[_listIndex++].value = this.argumentPanel.InputText;
                logger.Trace("Argument '" + (_listIndex - 1) + "' filled with: " + this.argumentPanel.InputText);
                if (_listIndex >= _argumentsToFill.Count())
                {
                    logger.Trace("No further arguments");
                    this.argumentPanel.Visible = false;
                    _listIndex = -1;
                    this.clipboardTextBox.Text = this.executeFunctionOfPlugin(this.clipboardTextBox.Text, this._currentPlugin, this._currentFunction, true, this._argumentsToFill);
                    _argumentsToFill = null;
                    _currentPlugin = null;
                } else
                {
                    // TODO refactor!
                    var selectedItem = this.pluginList.SelectedItems[0];
                    var myFunction = this.functionListUpDown.SelectedItem as Function;
                    var plugin = (APlugin)selectedItem.Tag;

                    logger.Trace("More arguments found");
                    // get selected item to calculate position of input
                    argumentPanel.LabelText = myFunction.arguments[this._listIndex].description;
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
                logger.Trace("FunctionArgumentInputKeyDown > Escape");
                this.argumentPanel.Visible = false;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Execute the function of the given plugin on the text and returned it. Can be executed for each line or for the whole text.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="plugin"></param>
        /// <param name="function"></param>
        /// <param name="forEachLine"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private String executeFunctionOfPlugin(String text, APlugin plugin, Function function, Boolean forEachLine = true, List<Argument> arguments = null)
        {
            logger.Trace(String.Format("[{0}] Plugin '{1}' will execute function '{2}'", plugin.Type, plugin.getName(), function.Name));
            String result = null;
            try
            {
                result = "";
                if (text != null)
                {
                    // TODO create option for this
                    if (forEachLine == true)
                    {
                        String[] lines = text.Split('\n');
                        foreach (String line in lines)
                        {
                            result += plugin.getResultFromFunction(function, line, arguments) + Environment.NewLine;
                        }
                    }
                    else
                    {
                        result = plugin.getResultFromFunction(function, text, arguments);
                    }
                    this.toolStripStatusLabel.Text = String.Format("[{0}] Plugin '{1}' has executed function '{2}'", plugin.Type, plugin.getName(), function.Name);
                }
            }
            catch (Exception e)
            {
                logger.Error("An Error occured " + "(" + e.Source + "): " + e.Message + "\n" + e.StackTrace);
                this.toolStripStatusLabel.Text = String.Format("[{0}] Plugin '{1}' has an error in function '{2}': {3}", plugin.Type, plugin.getName(), function.Name, e.Message);
            }
            return result;
        }
    }
}
