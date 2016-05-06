using NLog;
using SmartHotEdit.Controller;
using SmartHotEdit.Model;
using System;
using System.Windows.Forms;

namespace SmartHotEdit.View
{
    public partial class SettingsView : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private HotKeyController hotKeyController;

        public SettingsView(HotKeyController hotKeyController)
        {
            InitializeComponent();
            this.hotKeyController = hotKeyController;
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

            if (this.hotKeyController.registerCustomHotKey(this.hotKeyTextBox.Hotkey, isShift, isControl, isAlt, isWin)) {
                Console.WriteLine("Key changed");
                HotKey hotKey = new HotKey(this.hotKeyTextBox.Hotkey, isShift, isControl, isAlt, isWin);
                Properties.Settings.Default.HotKey = hotKey;
            }
        }

        private void SettingsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
