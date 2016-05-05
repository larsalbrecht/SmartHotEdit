using System;
using System.Windows.Forms;
using SmartHotEditPluginHost.Model;
using SmartHotEdit.Controller.Plugin;
using System.Collections.Generic;
using System.Linq;
using SmartHotEditPluginHost;

namespace SmartHotEdit.View
{
	/// <summary>
	/// Description of EditForm.
	/// </summary>
	public partial class EditForm : Form
	{
		enum POPUP_STATE {Small, Big};

        private PluginController pluginController;
		
		private int _currentPopupState;
        private int _listIndex = -1;
        private List<Argument> _argumentsToFill = null;
        private APlugin _currentPlugin = null;
        private Function _currentFunction = null;

        public EditForm(PluginController pluginController)
		{
			InitializeComponent();

            this.pluginController = pluginController;
            this._currentPopupState = (int)POPUP_STATE.Small;
		}
		void EditFormLoad(object sender, EventArgs e)
		{
			// init textbox with clipboard text
			if (Clipboard.ContainsText()) {
				this.clipboardTextBox.Text = Clipboard.GetText();
			}
			
			// add columns
			pluginList.Columns.Add("Plugin", -2, HorizontalAlignment.Left);
			pluginList.Columns.Add("Description", -2, HorizontalAlignment.Left);

			foreach(APlugin plugin in this.pluginController.getPlugins())
			{
				var tmpPluginEntry = new ListViewItem();
				tmpPluginEntry.Tag = plugin;
				tmpPluginEntry.Text = plugin.getName();
				pluginList.Items.Add(tmpPluginEntry);
			}

			pluginList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		
		void PluginListKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == (Keys.Enter)) {
				if(clipboardTextBox.Text != null && clipboardTextBox.Text.Length > 0)
                {
                    MessageBox.Show(clipboardTextBox.Text);
					Clipboard.SetText(clipboardTextBox.Text);
				}
                this.Close();
                e.SuppressKeyPress = true;
            } else if (e.KeyData == (Keys.Control | Keys.Space)) {
				if (pluginList.SelectedItems.Count > 0) {
					var selectedItem = pluginList.SelectedItems[0];
					functionListUpDown.Items.Clear();
					var plugin = (APlugin)selectedItem.Tag;

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
				var myFunction = functionListUpDown.SelectedItem as Function;
				var plugin = (APlugin)pluginList.SelectedItems[0].Tag;
				if(myFunction != null && plugin != null){
					System.Diagnostics.Debug.WriteLine("Call function " + myFunction.Name + " of plugin " + plugin.getName());
					if(myFunction.arguments != null && myFunction.arguments.Count > 0){
						System.Diagnostics.Debug.WriteLine("Called function has " + myFunction.arguments.Count + " arguments");
						if(!myFunction.arguments.Any())
							return;
						this._argumentsToFill = myFunction.arguments;
						this._currentFunction = myFunction;
						this._currentPlugin = plugin;
						this._listIndex = 0;

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
				if(this._currentPopupState == (int)POPUP_STATE.Small){
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
				var textBox = this.functionArgumentInput;
				this._argumentsToFill[_listIndex++].value = textBox.Text;
				textBox.Text = "";
				if (_listIndex >= _argumentsToFill.Count())
				{
					textBox.Visible = false;
					_listIndex = -1;
					this.clipboardTextBox.Text = this._currentPlugin.getResultFromFunction(_currentFunction, this.clipboardTextBox.Text, this._argumentsToFill);
					_argumentsToFill = null;
					_currentPlugin = null;
				}
                e.SuppressKeyPress = true;
            } else if(e.KeyCode == Keys.Escape){
				functionArgumentInput.Visible = false;
                e.SuppressKeyPress = true;
            }
		}
	}
}
