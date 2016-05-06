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
            this.pluginSettingsGroupBox.SuspendLayout();
            this.loggerSettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pluginSettingsGroupBox
            // 
            this.pluginSettingsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginSettingsGroupBox.AutoSize = true;
            this.pluginSettingsGroupBox.Controls.Add(this.enableLuaPluginsCheckBox);
            this.pluginSettingsGroupBox.Controls.Add(this.enableDefaultPluginsCheckBox);
            this.pluginSettingsGroupBox.Controls.Add(this.enablePluginsCheckBox);
            this.pluginSettingsGroupBox.Location = new System.Drawing.Point(12, 12);
            this.pluginSettingsGroupBox.Name = "pluginSettingsGroupBox";
            this.pluginSettingsGroupBox.Size = new System.Drawing.Size(455, 102);
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
            this.loggerSettingsGroupBox.Location = new System.Drawing.Point(12, 120);
            this.loggerSettingsGroupBox.Name = "loggerSettingsGroupBox";
            this.loggerSettingsGroupBox.Size = new System.Drawing.Size(455, 100);
            this.loggerSettingsGroupBox.TabIndex = 1;
            this.loggerSettingsGroupBox.TabStop = false;
            this.loggerSettingsGroupBox.Text = "Logging";
            // 
            // enableLoggingCheckBox
            // 
            this.enableLoggingCheckBox.AutoSize = true;
            this.enableLoggingCheckBox.Checked = global::SmartHotEdit.Properties.Settings.Default.EnableLogging;
            this.enableLoggingCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SmartHotEdit.Properties.Settings.Default, "EnableLogging", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enableLoggingCheckBox.Location = new System.Drawing.Point(7, 20);
            this.enableLoggingCheckBox.Name = "enableLoggingCheckBox";
            this.enableLoggingCheckBox.Size = new System.Drawing.Size(100, 17);
            this.enableLoggingCheckBox.TabIndex = 0;
            this.enableLoggingCheckBox.Text = "Enable Logging";
            this.enableLoggingCheckBox.UseVisualStyleBackColor = true;
            this.enableLoggingCheckBox.CheckStateChanged += new System.EventHandler(this.enableLoggingCheckBox_CheckStateChanged);
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 292);
            this.Controls.Add(this.loggerSettingsGroupBox);
            this.Controls.Add(this.pluginSettingsGroupBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.pluginSettingsGroupBox.ResumeLayout(false);
            this.pluginSettingsGroupBox.PerformLayout();
            this.loggerSettingsGroupBox.ResumeLayout(false);
            this.loggerSettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox pluginSettingsGroupBox;
        private System.Windows.Forms.CheckBox enableLuaPluginsCheckBox;
        private System.Windows.Forms.CheckBox enableDefaultPluginsCheckBox;
        private System.Windows.Forms.CheckBox enablePluginsCheckBox;
        private System.Windows.Forms.GroupBox loggerSettingsGroupBox;
        private System.Windows.Forms.CheckBox enableLoggingCheckBox;
    }
}