using NLog;
using SmartHotEdit.Abstracts;
using SmartHotEdit.Controller;
using SmartHotEdit.Helper;
using SmartHotEdit.Model;
using SmartHotEditPluginHost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace SmartHotEdit.View
{
    public partial class SettingsView : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private MainController mainController;

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
            if (Properties.Settings.Default.EnableLogging == true)
            {
                System.Diagnostics.Debug.WriteLine("Enable logger now");
                LogManager.EnableLogging();
                foreach (var rule in LogManager.Configuration.LoggingRules)
                {
                    // Iterate over all levels up to and including the target, (re)enabling them.
                    for (int i = LogLevel.Trace.Ordinal; i <= 5; i++)
                    {
                        rule.EnableLoggingForLevel(LogLevel.FromOrdinal(i));
                    }
                }
                logger.Debug("Enabled logger");
            } else
            {
                logger.Debug("Disable logger now");
                System.Diagnostics.Debug.WriteLine("Disable logger now");
                LogManager.DisableLogging();
            }
            LogManager.ReconfigExistingLoggers();
            System.Diagnostics.Debug.WriteLine("Change logger state. Level?: " + LogManager.GlobalThreshold + "; Enabled?: " + LogManager.IsLoggingEnabled());
        }

        private void changeHotKeyButton_Click(object sender, EventArgs e)
        {

            bool isShiftControl = this.hotKeyTextBox.Modifiers == (Keys.Shift | Keys.Control);
            bool isAltControl = this.hotKeyTextBox.Modifiers == (Keys.Alt | Keys.Control);
            bool isShiftAltControl = this.hotKeyTextBox.Modifiers == (Keys.Shift | Keys.Alt | Keys.Control);
            bool isShiftAlt = this.hotKeyTextBox.Modifiers == (Keys.Shift | Keys.Alt);

            bool isShift = isShiftControl || isShiftAlt || isShiftAltControl || this.hotKeyTextBox.Modifiers == Keys.Shift;
            bool isAlt = isAltControl || isShiftAlt || isShiftAltControl || this.hotKeyTextBox.Modifiers == Keys.Alt;
            bool isControl = isShiftControl || isAltControl || isShiftAltControl || this.hotKeyTextBox.Modifiers == Keys.Control;
            bool isWin = this.hotKeyTextBox.WinModifier;

            if (this.mainController.getHotKeyController().registerCustomHotKey(this.hotKeyTextBox.Hotkey, isShift, isControl, isAlt, isWin)) {
                HotKey hotKey = new HotKey(this.hotKeyTextBox.Hotkey, isShift, isControl, isAlt, isWin);
                Properties.Settings.Default.HotKey = hotKey;
            }
        }

        private void SettingsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void SettingsView_Shown(object sender, EventArgs e)
        {
            this.enableDisablePluginListView.Items.Clear();
            this.enableDisablePluginListView.Groups.Clear();
            foreach (APluginController concretePluginController in this.mainController.getPluginController().getPluginControllerList())
            {
                var tempListViewGroup = new ListViewGroup(concretePluginController.Type, System.Windows.Forms.HorizontalAlignment.Left);
                tempListViewGroup.Header = concretePluginController.Type;
                tempListViewGroup.Name = concretePluginController.Type + "Group";
                tempListViewGroup.Tag = concretePluginController;

                this.enableDisablePluginListView.Groups.Add(tempListViewGroup);

                if (concretePluginController.LoadedPlugins.Count > 0)
                {
                    foreach(APlugin plugin in concretePluginController.LoadedPlugins)
                    {
                        var tempListViewItem = new ListViewItem(plugin.getName());
                        tempListViewItem.Group = tempListViewGroup;
                        tempListViewItem.StateImageIndex = 0;
                        tempListViewItem.Tag = plugin;
                        tempListViewItem.Checked = plugin.Enabled;
                        this.enableDisablePluginListView.Items.Add(tempListViewItem);
                    }
                }
            }
        }

        private void enableDisablePluginListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (this.enableDisablePluginListView.Focused)
            {
            ListViewItem item = e.Item as ListViewItem;
            APlugin plugin = (APlugin)item.Tag;
            plugin.Enabled = item.Checked;

            logger.Debug("Plugin " + plugin.getName() + " changed state. Is it enabled? " + plugin.Enabled);
            
            Properties.Settings.Default[plugin.getPropertynameForEnablePlugin()] = plugin.Enabled;
            Properties.Settings.Default.Save();
            }
        }
    }
}
