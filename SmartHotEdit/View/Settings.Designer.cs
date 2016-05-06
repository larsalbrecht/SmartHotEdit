using SmartHotEdit.View.Controls;

namespace SmartHotEdit.View
{
    partial class SettingsView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pluginSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.enableLuaPluginsCheckBox = new System.Windows.Forms.CheckBox();
            this.enableDefaultPluginsCheckBox = new System.Windows.Forms.CheckBox();
            this.enablePluginsCheckBox = new System.Windows.Forms.CheckBox();
            this.loggerSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.enableLoggingCheckBox = new System.Windows.Forms.CheckBox();
            this.hotKeySettingGroupBox = new System.Windows.Forms.GroupBox();
            this.changeHotKeyButton = new System.Windows.Forms.Button();
            this.hotKeyTextBox = new SmartHotEdit.View.Controls.HotKeyControl();
            this.pluginSettingsGroupBox.SuspendLayout();
            this.loggerSettingsGroupBox.SuspendLayout();
            this.hotKeySettingGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pluginSettingsGroupBox
            // 
            this.pluginSettingsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginSettingsGroupBox.Controls.Add(this.enableLuaPluginsCheckBox);
            this.pluginSettingsGroupBox.Controls.Add(this.enableDefaultPluginsCheckBox);
            this.pluginSettingsGroupBox.Controls.Add(this.enablePluginsCheckBox);
            this.pluginSettingsGroupBox.Location = new System.Drawing.Point(12, 97);
            this.pluginSettingsGroupBox.Name = "pluginSettingsGroupBox";
            this.pluginSettingsGroupBox.Size = new System.Drawing.Size(203, 102);
            this.pluginSettingsGroupBox.TabIndex = 0;
            this.pluginSettingsGroupBox.TabStop = false;
            this.pluginSettingsGroupBox.Text = "Plugin Settings";
            // 
            // enableLuaPluginsCheckBox
            // 
            this.enableLuaPluginsCheckBox.AutoSize = true;
            this.enableLuaPluginsCheckBox.Checked = global::SmartHotEdit.Properties.Settings.Default.EnableLuaPlugins;
            this.enableLuaPluginsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableLuaPluginsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SmartHotEdit.Properties.Settings.Default, "EnableLuaPlugins", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enableLuaPluginsCheckBox.Location = new System.Drawing.Point(7, 66);
            this.enableLuaPluginsCheckBox.Name = "enableLuaPluginsCheckBox";
            this.enableLuaPluginsCheckBox.Size = new System.Drawing.Size(117, 17);
            this.enableLuaPluginsCheckBox.TabIndex = 2;
            this.enableLuaPluginsCheckBox.Text = "Enable Lua Plugins";
            this.enableLuaPluginsCheckBox.UseVisualStyleBackColor = true;
            // 
            // enableDefaultPluginsCheckBox
            // 
            this.enableDefaultPluginsCheckBox.AutoSize = true;
            this.enableDefaultPluginsCheckBox.Checked = global::SmartHotEdit.Properties.Settings.Default.EnableDefaultPlugins;
            this.enableDefaultPluginsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableDefaultPluginsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SmartHotEdit.Properties.Settings.Default, "EnableDefaultPlugins", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enableDefaultPluginsCheckBox.Location = new System.Drawing.Point(7, 43);
            this.enableDefaultPluginsCheckBox.Name = "enableDefaultPluginsCheckBox";
            this.enableDefaultPluginsCheckBox.Size = new System.Drawing.Size(133, 17);
            this.enableDefaultPluginsCheckBox.TabIndex = 1;
            this.enableDefaultPluginsCheckBox.Text = "Enable Default Plugins";
            this.enableDefaultPluginsCheckBox.UseVisualStyleBackColor = true;
            // 
            // enablePluginsCheckBox
            // 
            this.enablePluginsCheckBox.AutoSize = true;
            this.enablePluginsCheckBox.Checked = global::SmartHotEdit.Properties.Settings.Default.EnablePlugins;
            this.enablePluginsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enablePluginsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SmartHotEdit.Properties.Settings.Default, "EnablePlugins", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enablePluginsCheckBox.Location = new System.Drawing.Point(7, 20);
            this.enablePluginsCheckBox.Name = "enablePluginsCheckBox";
            this.enablePluginsCheckBox.Size = new System.Drawing.Size(96, 17);
            this.enablePluginsCheckBox.TabIndex = 0;
            this.enablePluginsCheckBox.Text = "Enable Plugins";
            this.enablePluginsCheckBox.UseVisualStyleBackColor = true;
            this.enablePluginsCheckBox.CheckStateChanged += new System.EventHandler(this.enablePluginsCheckBox_CheckStateChanged);
            // 
            // loggerSettingsGroupBox
            // 
            this.loggerSettingsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loggerSettingsGroupBox.Controls.Add(this.enableLoggingCheckBox);
            this.loggerSettingsGroupBox.Location = new System.Drawing.Point(12, 205);
            this.loggerSettingsGroupBox.Name = "loggerSettingsGroupBox";
            this.loggerSettingsGroupBox.Size = new System.Drawing.Size(203, 49);
            this.loggerSettingsGroupBox.TabIndex = 1;
            this.loggerSettingsGroupBox.TabStop = false;
            this.loggerSettingsGroupBox.Text = "Logging";
            // 
            // enableLoggingCheckBox
            // 
            this.enableLoggingCheckBox.AutoSize = true;
            this.enableLoggingCheckBox.Checked = global::SmartHotEdit.Properties.Settings.Default.EnableLogging;
            this.enableLoggingCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableLoggingCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SmartHotEdit.Properties.Settings.Default, "EnableLogging", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enableLoggingCheckBox.Location = new System.Drawing.Point(7, 20);
            this.enableLoggingCheckBox.Name = "enableLoggingCheckBox";
            this.enableLoggingCheckBox.Size = new System.Drawing.Size(100, 17);
            this.enableLoggingCheckBox.TabIndex = 0;
            this.enableLoggingCheckBox.Text = "Enable Logging";
            this.enableLoggingCheckBox.UseVisualStyleBackColor = true;
            this.enableLoggingCheckBox.CheckStateChanged += new System.EventHandler(this.enableLoggingCheckBox_CheckStateChanged);
            // 
            // hotKeySettingGroupBox
            // 
            this.hotKeySettingGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hotKeySettingGroupBox.Controls.Add(this.changeHotKeyButton);
            this.hotKeySettingGroupBox.Controls.Add(this.hotKeyTextBox);
            this.hotKeySettingGroupBox.Location = new System.Drawing.Point(12, 12);
            this.hotKeySettingGroupBox.Name = "hotKeySettingGroupBox";
            this.hotKeySettingGroupBox.Size = new System.Drawing.Size(203, 79);
            this.hotKeySettingGroupBox.TabIndex = 2;
            this.hotKeySettingGroupBox.TabStop = false;
            this.hotKeySettingGroupBox.Text = "Hot Key";
            // 
            // changeHotKeyButton
            // 
            this.changeHotKeyButton.Location = new System.Drawing.Point(7, 45);
            this.changeHotKeyButton.Name = "changeHotKeyButton";
            this.changeHotKeyButton.Size = new System.Drawing.Size(96, 23);
            this.changeHotKeyButton.TabIndex = 1;
            this.changeHotKeyButton.Text = "Change Hot Key";
            this.changeHotKeyButton.UseVisualStyleBackColor = true;
            this.changeHotKeyButton.Click += new System.EventHandler(this.changeHotKeyButton_Click);
            // 
            // hotKeyTextBox
            // 
            this.hotKeyTextBox.BackColor = System.Drawing.Color.White;
            this.hotKeyTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.hotKeyTextBox.Hotkey = System.Windows.Forms.Keys.None;
            this.hotKeyTextBox.Location = new System.Drawing.Point(6, 19);
            this.hotKeyTextBox.Modifiers = System.Windows.Forms.Keys.None;
            this.hotKeyTextBox.Name = "hotKeyTextBox";
            this.hotKeyTextBox.ReadOnly = true;
            this.hotKeyTextBox.Size = new System.Drawing.Size(190, 20);
            this.hotKeyTextBox.TabIndex = 0;
            this.hotKeyTextBox.Text = "None";
            this.hotKeyTextBox.WinModifier = false;
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 293);
            this.Controls.Add(this.hotKeySettingGroupBox);
            this.Controls.Add(this.loggerSettingsGroupBox);
            this.Controls.Add(this.pluginSettingsGroupBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsView_FormClosing);
            this.pluginSettingsGroupBox.ResumeLayout(false);
            this.pluginSettingsGroupBox.PerformLayout();
            this.loggerSettingsGroupBox.ResumeLayout(false);
            this.loggerSettingsGroupBox.PerformLayout();
            this.hotKeySettingGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox pluginSettingsGroupBox;
        private System.Windows.Forms.CheckBox enableLuaPluginsCheckBox;
        private System.Windows.Forms.CheckBox enableDefaultPluginsCheckBox;
        private System.Windows.Forms.CheckBox enablePluginsCheckBox;
        private System.Windows.Forms.GroupBox loggerSettingsGroupBox;
        private System.Windows.Forms.CheckBox enableLoggingCheckBox;
        private System.Windows.Forms.GroupBox hotKeySettingGroupBox;
        private HotKeyControl hotKeyTextBox;
        private System.Windows.Forms.Button changeHotKeyButton;
    }
}