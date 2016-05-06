using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartHotEdit.View
{
    public partial class SettingsView : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public SettingsView()
        {
            InitializeComponent();
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
    }
}
