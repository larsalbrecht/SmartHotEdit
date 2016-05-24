using System;
using System.Diagnostics;
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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly MainController mainController;

        // TODO init hotkey in hotkey control to show the user the used key
        public SettingsView(MainController mainController)
        {
            InitializeComponent();
            this.mainController = mainController;
        }

        private void enablePluginsCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            this.enableDefaultPluginsCheckBox.Enabled = this.enablePluginsCheckBox.Checked;
            this.enableLuaPluginsCheckBox.Enabled = this.enablePluginsCheckBox.Checked;
        }

        private void enableLoggingCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (Settings.Default.EnableLogging)
            {
                Debug.WriteLine("Enable logger now");
                LogManager.EnableLogging();
                foreach (var rule in LogManager.Configuration.LoggingRules)
                {
                    // Iterate over all levels up to and including the target, (re)enabling them.
                    for (var i = LogLevel.Trace.Ordinal; i <= 5; i++)
                    {
                        rule.EnableLoggingForLevel(LogLevel.FromOrdinal(i));
                    }
                }
                logger.Debug("Enabled logger");
            }
            else
            {
                logger.Debug("Disable logger now");
                Debug.WriteLine("Disable logger now");
                LogManager.DisableLogging();
            }
            LogManager.ReconfigExistingLoggers();
            Debug.WriteLine("Change logger state. Level?: " + LogManager.GlobalThreshold + "; Enabled?: " +
                            LogManager.IsLoggingEnabled());
        }

        private void changeHotKeyButton_Click(object sender, EventArgs e)
        {
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

            if (this.mainController.HotKeyController.RegisterCustomHotKey(this.hotKeyTextBox.Hotkey, isShift, isControl,
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
            this.enableDisablePluginListView.Items.Clear();
            this.enableDisablePluginListView.Groups.Clear();
            foreach (var concretePluginController in this.mainController.PluginController.GetPluginControllerList())
            {
                var tempListViewGroup = new ListViewGroup(concretePluginController.Type, HorizontalAlignment.Left);
                tempListViewGroup.Header = concretePluginController.Type;
                tempListViewGroup.Name = concretePluginController.Type + "Group";
                tempListViewGroup.Tag = concretePluginController;

                this.enableDisablePluginListView.Groups.Add(tempListViewGroup);

                if (concretePluginController.LoadedPlugins.Count > 0)
                {
                    foreach (var plugin in concretePluginController.LoadedPlugins)
                    {
                        var tempListViewItem = new ListViewItem(plugin.Name);
                        tempListViewItem.Group = tempListViewGroup;
                        tempListViewItem.StateImageIndex = 0;
                        tempListViewItem.Tag = plugin;
                        tempListViewItem.Checked = plugin.Enabled;
                        this.enableDisablePluginListView.Items.Add(tempListViewItem);
                    }
                }
            }

            var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            this.startWithSystemCheckBox.Checked = rk.GetValue(Program.AppName) != null;
            logger.Debug("Start on system startup: " + this.startWithSystemCheckBox.Checked);
        }

        private void enableDisablePluginListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (this.enableDisablePluginListView.Focused)
            {
                var item = e.Item;
                var plugin = (APlugin) item.Tag;
                plugin.Enabled = item.Checked;

                logger.Debug("Plugin " + plugin.Name + " changed state. Is it enabled? " + plugin.Enabled);

                Settings.Default[plugin.GetPropertynameForEnablePlugin()] = plugin.Enabled;
                Settings.Default.Save();
            }
        }

        private void startWithSystemCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.startWithSystemCheckBox.Focused)
            {
                var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (this.startWithSystemCheckBox.Checked)
                {
                    rk.SetValue(Program.AppName, Application.ExecutablePath);
                    logger.Debug("Starts on system startup now");
                }
                else
                {
                    rk.DeleteValue(Program.AppName, false);
                    logger.Debug("Dont starts on system startup now");
                }
            }
        }
    }
}