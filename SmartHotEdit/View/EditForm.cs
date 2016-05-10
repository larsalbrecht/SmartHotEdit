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

        enum POPUP_STATE {Small, Big};

        private PluginController pluginController;
		
		private int _currentPopupState;
        private int _listIndex = -1;
        private List<Argument> _argumentsToFill = null;
        private APlugin _currentPlugin = null;
        private Function _currentFunction = null;

        public EditForm(PluginController pluginController)
		{
            logger.Trace("Init EditForm now");

            InitializeComponent();

            this.pluginController = pluginController;
            this._currentPopupState = (int)POPUP_STATE.Small;
		}
		void EditFormLoad(object sender, EventArgs e)
		{
            // init textbox with clipboard text
            logger.Trace("Clipboard contains text?: " + Clipboard.ContainsText());
            if (Clipboard.ContainsText()) {
				this.clipboardTextBox.Text = Clipboard.GetText();
			}
			
			// add columns
			pluginList.Columns.Add("Plugin", -2, HorizontalAlignment.Left);
			pluginList.Columns.Add("Description", -2, HorizontalAlignment.Left);

            logger.Debug("Add plugins to view");

            foreach (APlugin plugin in this.pluginController.getPlugins())
			{
				var tmpPluginEntry = new ListViewItem();
				tmpPluginEntry.Tag = plugin;
				tmpPluginEntry.Text = plugin.getName();
				pluginList.Items.Add(tmpPluginEntry);
                logger.Info("Plugin added to view: " + plugin.getName());
            }

			pluginList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		
		void PluginListKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == (Keys.Enter)) {
                logger.Trace("PluginListKeyDown > Enter");
                if (clipboardTextBox.Text != null && clipboardTextBox.Text.Length > 0)
                {
                    logger.Debug("Set text to clipboard: " + clipboardTextBox.Text);
					Clipboard.SetText(clipboardTextBox.Text);
				}
                this.Close();
                e.SuppressKeyPress = true;
            } else if (e.KeyData == (Keys.Control | Keys.Space)) {
                logger.Trace("PluginListKeyDown > Control + Space");
                if (pluginList.SelectedItems.Count > 0) {
					var selectedItem = pluginList.SelectedItems[0];
					functionListUpDown.Items.Clear();
					var plugin = (APlugin)selectedItem.Tag;
                    logger.Debug("Selected plugin: " + plugin.getName());

                    functionListUpDown.Items.AddRange(plugin.getFunctionsAsCollection(functionListUpDown));

                    _currentPopupState = (int)POPUP_STATE.Small;
					
					functionListUpDown.Left = (selectedItem.ListView.Columns[0].Width);
					functionListUpDown.Top = selectedItem.Bounds.Top;
					functionListUpDown.Visible = true;
					functionListUpDown.Focus();
                }
				e.SuppressKeyPress = true;
			}
		}

		void FunctionListUpDownKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == (Keys.Enter)) {
                logger.Trace("FunctionListUpDownKeyDown > Enter");
                var myFunction = functionListUpDown.SelectedItem as Function;
				var plugin = (APlugin)pluginList.SelectedItems[0].Tag;
				if(myFunction != null && plugin != null){
                    logger.Debug("Selected function (plugin: " + plugin.getName() + ") to execute: " + myFunction.Name);
                    logger.Debug("Function has: " + (myFunction.arguments == null || myFunction.arguments.Count == 0 ? 0 : myFunction.arguments.Count) + " arguments");
                    if (myFunction.arguments != null && myFunction.arguments.Count > 0){
						if(!myFunction.arguments.Any())
							return;
                        logger.Trace("Open argument inputs");

                        this._argumentsToFill = myFunction.arguments;
						this._currentFunction = myFunction;
						this._currentPlugin = plugin;
						this._listIndex = 0;

                        // get selected item to calculate position of input
                        var selectedItem = pluginList.SelectedItems[0];
                        functionArgumentInput.Text = "";
                        functionArgumentInput.Left = (selectedItem.ListView.Columns[0].Width);
                        functionArgumentInput.Top = selectedItem.Bounds.Top;
                        functionArgumentInput.Visible = true;
						functionArgumentInput.Focus();
					} else {
						this.clipboardTextBox.Text = plugin.getResultFromFunction(myFunction, this.clipboardTextBox.Text);
					}
                    this.functionListUpDown.Visible = false;
				}
                e.SuppressKeyPress = true;
            } else if (e.KeyData == (Keys.Control | Keys.Space)) {
                logger.Trace("FunctionListUpDownKeyDown > Control + Space");
                logger.Trace("Change size of functionListUpDown input");
                if (this._currentPopupState == (int)POPUP_STATE.Small){
                    this._currentPopupState = (int)POPUP_STATE.Big;
					this.functionListUpDown.Height = this.functionListUpDown.PreferredHeight;
				} else {
					this._currentPopupState = (int)POPUP_STATE.Small;
					this.functionListUpDown.Height = this.functionListUpDown.ItemHeight;
				}
                e.SuppressKeyPress = true;
            }
		}
		
		void FunctionArgumentInputKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter){
                logger.Trace("FunctionArgumentInputKeyDown > Enter");
                var textBox = this.functionArgumentInput;
				this._argumentsToFill[_listIndex++].value = textBox.Text;
                logger.Trace("Argument '" + (_listIndex-1)  + "' filled with: " + textBox.Text);
                textBox.Text = "";
				if (_listIndex >= _argumentsToFill.Count())
				{
                    logger.Trace("No further arguments");
                    textBox.Visible = false;
					_listIndex = -1;
					this.clipboardTextBox.Text = this._currentPlugin.getResultFromFunction(_currentFunction, this.clipboardTextBox.Text, this._argumentsToFill);
					_argumentsToFill = null;
					_currentPlugin = null;
				}
                e.SuppressKeyPress = true;
            } else if(e.KeyCode == Keys.Escape){
                logger.Trace("FunctionArgumentInputKeyDown > Escape");
                functionArgumentInput.Visible = false;
                e.SuppressKeyPress = true;
            }
		}
	}
}
