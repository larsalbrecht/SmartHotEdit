using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using NLog;
using SmartHotEdit.Controller;
using SmartHotEdit.Model;
using SmartHotEdit.Properties;
using SmartHotEditPluginHost;

namespace SmartHotEdit.View
{
    public partial class SettingsView : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly MainController _mainController;

        public SettingsView(MainController mainController)
        {
            InitializeComponent();
            this._mainController = mainController;
        }

        private void enablePluginsCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            this.enableDefaultPluginsCheckBox.Enabled = this.enablePluginsCheckBox.Checked;
            this.scriptPluginsGroupBox.Enabled = this.enablePluginsCheckBox.Checked;
            this.enableDisablePluginsGroup.Enabled = this.enablePluginsCheckBox.Checked;
        }

        private void enableLoggingCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (Settings.Default.EnableLogging)
            {
                Logger.Trace("Enable logger now");
                LogManager.EnableLogging();
                foreach (var rule in LogManager.Configuration.LoggingRules)
                {
                    // Iterate over all levels up to and including the target, (re)enabling them.
                    for (var i = LogLevel.Trace.Ordinal; i <= 5; i++)
                    {
                        rule.EnableLoggingForLevel(LogLevel.FromOrdinal(i));
                    }
                }
                Logger.Debug("Enabled logger");
            }
            else
            {
                Logger.Debug("Disable logger now");
                LogManager.DisableLogging();
            }
            LogManager.ReconfigExistingLoggers();
            Debug.WriteLine("Change logger state. Level?: " + LogManager.GlobalThreshold + "; Enabled?: " +
                            LogManager.IsLoggingEnabled());
        }

        private void changeHotKeyButton_Click(object sender, EventArgs e)
        {
            // TODO init hotkey in hotkey control to show the user the used key
            var isShiftControl = this.hotKeyTextBox.Modifiers == (Keys.Shift | Keys.Control);
            var isAltControl = this.hotKeyTextBox.Modifiers == (Keys.Alt | Keys.Control);
            var isShiftAltControl = this.hotKeyTextBox.Modifiers == (Keys.Shift | Keys.Alt | Keys.Control);
            var isShiftAlt = this.hotKeyTextBox.Modifiers == (Keys.Shift | Keys.Alt);

            var isShift = isShiftControl || isShiftAlt || isShiftAltControl ||
                          this.hotKeyTextBox.Modifiers == Keys.Shift;
            var isAlt = isAltControl || isShiftAlt || isShiftAltControl || this.hotKeyTextBox.Modifiers == Keys.Alt;
            var isControl = isShiftControl || isAltControl || isShiftAltControl ||
                            this.hotKeyTextBox.Modifiers == Keys.Control;
            var isWin = this.hotKeyTextBox.WinModifier;

            if (this._mainController.HotKeyController.RegisterCustomHotKey(this.hotKeyTextBox.Hotkey, isShift, isControl,
                isAlt, isWin))
            {
                var hotKey = new HotKey(this.hotKeyTextBox.Hotkey, isShift, isControl, isAlt, isWin);
                Settings.Default.HotKey = hotKey;
            }
        }

        private void SettingsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void SettingsView_Shown(object sender, EventArgs e)
        {
            this.InitEnableDisablePluginListView();
            this.InitStartWithSystemCheckBox();
        }

        private void enableDisablePluginListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!this.enableDisablePluginListView.Focused) return;

            var item = e.Item;
            var plugin = (APlugin) item.Tag;
            plugin.Enabled = item.Checked;

            Logger.Debug("Plugin " + plugin.Name + " changed state. Is it enabled? " + plugin.Enabled);
        }

        private void startWithSystemCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (!this.startWithSystemCheckBox.Focused) return;
            var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rk == null)
            {
                Logger.Debug("Cannot open subkey in registry");
                return;
            }
            if (this.startWithSystemCheckBox.Checked)
            {
                rk.SetValue(Program.AppName, $"\"{Application.ExecutablePath}\"");
                Logger.Debug("Starts on system startup now");
            }
            else
            {
                rk.DeleteValue(Program.AppName, false);
                Logger.Debug("Dont starts on system startup now");
            }
        }

        private void SettingsView_Load(object sender, EventArgs e)
        {
            this.InitScriptPluginsGroupBox();
        }

        private void OnPluginCheckboxStateChange(object sender, EventArgs e)
        {
            var checkBox = (CheckBox) sender;
            var pluginController = (APluginController) checkBox.Tag;
            foreach (ListViewGroup itemGroup in this.enableDisablePluginListView.Groups)
            {
                if (itemGroup.Tag.GetType() != pluginController.GetType()) continue;
                foreach (ListViewItem item in itemGroup.Items)
                {
                    item.ForeColor = checkBox.Checked ? DefaultForeColor : Color.Silver;
                }
                return;
            }
        }

        private void scriptPluginsGroupBox_ControlAdded(object sender, ControlEventArgs e)
        {
            this.scriptPluginsGroupBox.Visible = true;
        }

        private void scriptPluginsGroupBox_ControlRemoved(object sender, ControlEventArgs e)
        {
            this.scriptPluginsGroupBox.Visible = !(this.scriptPluginsGroupBox.Controls.Count <= 0);
        }


        /// <summary>
        ///     Initilize the "enableDisablePluginListView".
        ///     First it will be cleared and then all pluginController will be
        ///     read to get the Type (create group with this) and read out all
        ///     plugins of each pluginController (create items with this).
        /// </summary>
        private void InitEnableDisablePluginListView()
        {
            this.enableDisablePluginListView.Items.Clear();
            this.enableDisablePluginListView.Groups.Clear();
            foreach (var concretePluginController in this._mainController.PluginController.GetPluginControllerList())
            {
                var tempListViewGroup = new ListViewGroup(concretePluginController.Type, HorizontalAlignment.Left)
                {
                    Header = concretePluginController.Type,
                    Name = concretePluginController.Type + "Group",
                    Tag = concretePluginController
                };

                this.enableDisablePluginListView.Groups.Add(tempListViewGroup);

                foreach (var plugin in concretePluginController.LoadedPlugins)
                {
                    var tempListViewItem = new ListViewItem(plugin.Name)
                    {
                        Group = tempListViewGroup,
                        StateImageIndex = 0,
                        Tag = plugin,
                        Checked = plugin.Enabled
                    };
                    this.enableDisablePluginListView.Items.Add(tempListViewItem);
                }
            }
        }


        /// <summary>
        ///     Initilize the "startWithSystemCheckBox".
        ///     Reads out the windows registry value (checks if it exists)
        ///     and set the checkBox to Checked, or not.
        /// </summary>
        private void InitStartWithSystemCheckBox()
        {
            var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rk != null) this.startWithSystemCheckBox.Checked = rk.GetValue(Program.AppName) != null;
            Logger.Debug("Start on system startup: " + this.startWithSystemCheckBox.Checked);
        }

        /// <summary>
        ///     Initilize the "scriptPluginsGroupBox".
        ///     Reads out all pluginController and create a checkBox.
        ///     If a check-state change, it will be saved as Property.Settings to save
        ///     the state.
        /// </summary>
        private void InitScriptPluginsGroupBox()
        {
            var scriptPlugins =
                this._mainController.PluginController.GetPluginControllerList(typeof(AScriptPluginController));
            var tabIndex = 12;
            var location = new Point(6, 19);
            foreach (var aPluginController in scriptPlugins)
            {
                var pluginController = (AScriptPluginController) aPluginController;
                var tempCheckBox = new CheckBox();
                this.scriptPluginsGroupBox.Controls.Add(tempCheckBox);

                tempCheckBox.AutoSize = true;
                tempCheckBox.Checked = pluginController.Enabled;
                tempCheckBox.CheckState = CheckState.Checked;
                tempCheckBox.Tag = pluginController;

                tempCheckBox.DataBindings.Add(new Binding("Checked", pluginController, "Enabled",
                    true, DataSourceUpdateMode.OnPropertyChanged));
                tempCheckBox.Location = location;
                location.Y = location.Y + 23;
                tempCheckBox.Size = new Size(117, 17);
                tempCheckBox.TabIndex = ++tabIndex;
                tempCheckBox.Text = $"Enable {pluginController.Type} Plugins";
                tempCheckBox.UseVisualStyleBackColor = true;

                tempCheckBox.CheckStateChanged += this.OnPluginCheckboxStateChange;
            }
        }
    }
}