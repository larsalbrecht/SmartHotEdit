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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pluginSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.loggerSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.hotKeySettingGroupBox = new System.Windows.Forms.GroupBox();
            this.changeHotKeyButton = new System.Windows.Forms.Button();
            this.enableDisablePluginsGroup = new System.Windows.Forms.GroupBox();
            this.enableDisablePluginListView = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.startWithSystemCheckBox = new System.Windows.Forms.CheckBox();
            this.enableJavascriptPluginsCheckBox = new System.Windows.Forms.CheckBox();
            this.enablePythonPluginsCheckBox = new System.Windows.Forms.CheckBox();
            this.enableLuaPluginsCheckBox = new System.Windows.Forms.CheckBox();
            this.enableDefaultPluginsCheckBox = new System.Windows.Forms.CheckBox();
            this.enablePluginsCheckBox = new System.Windows.Forms.CheckBox();
            this.enableLoggingCheckBox = new System.Windows.Forms.CheckBox();
            this.hotKeyTextBox = new SmartHotEdit.View.Controls.HotKeyControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.pluginSettingsGroupBox.SuspendLayout();
            this.loggerSettingsGroupBox.SuspendLayout();
            this.hotKeySettingGroupBox.SuspendLayout();
            this.enableDisablePluginsGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.pluginSettingsGroupBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.loggerSettingsGroupBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.hotKeySettingGroupBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.enableDisablePluginsGroup, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(231, 467);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // pluginSettingsGroupBox
            // 
            this.pluginSettingsGroupBox.AutoSize = true;
            this.pluginSettingsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pluginSettingsGroupBox.Controls.Add(this.enableJavascriptPluginsCheckBox);
            this.pluginSettingsGroupBox.Controls.Add(this.enablePythonPluginsCheckBox);
            this.pluginSettingsGroupBox.Controls.Add(this.enableLuaPluginsCheckBox);
            this.pluginSettingsGroupBox.Controls.Add(this.enableDefaultPluginsCheckBox);
            this.pluginSettingsGroupBox.Controls.Add(this.enablePluginsCheckBox);
            this.pluginSettingsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginSettingsGroupBox.Location = new System.Drawing.Point(3, 96);
            this.pluginSettingsGroupBox.Name = "pluginSettingsGroupBox";
            this.pluginSettingsGroupBox.Size = new System.Drawing.Size(225, 147);
            this.pluginSettingsGroupBox.TabIndex = 12;
            this.pluginSettingsGroupBox.TabStop = false;
            this.pluginSettingsGroupBox.Text = "Plugin Settings";
            // 
            // loggerSettingsGroupBox
            // 
            this.loggerSettingsGroupBox.AutoSize = true;
            this.loggerSettingsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.loggerSettingsGroupBox.Controls.Add(this.enableLoggingCheckBox);
            this.loggerSettingsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loggerSettingsGroupBox.Location = new System.Drawing.Point(3, 355);
            this.loggerSettingsGroupBox.Name = "loggerSettingsGroupBox";
            this.loggerSettingsGroupBox.Size = new System.Drawing.Size(225, 56);
            this.loggerSettingsGroupBox.TabIndex = 11;
            this.loggerSettingsGroupBox.TabStop = false;
            this.loggerSettingsGroupBox.Text = "Logging";
            // 
            // hotKeySettingGroupBox
            // 
            this.hotKeySettingGroupBox.AutoSize = true;
            this.hotKeySettingGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hotKeySettingGroupBox.Controls.Add(this.changeHotKeyButton);
            this.hotKeySettingGroupBox.Controls.Add(this.hotKeyTextBox);
            this.hotKeySettingGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hotKeySettingGroupBox.Location = new System.Drawing.Point(3, 3);
            this.hotKeySettingGroupBox.Name = "hotKeySettingGroupBox";
            this.hotKeySettingGroupBox.Size = new System.Drawing.Size(225, 87);
            this.hotKeySettingGroupBox.TabIndex = 4;
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
            // 
            // enableDisablePluginsGroup
            // 
            this.enableDisablePluginsGroup.AutoSize = true;
            this.enableDisablePluginsGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.enableDisablePluginsGroup.Controls.Add(this.enableDisablePluginListView);
            this.enableDisablePluginsGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.enableDisablePluginsGroup.Location = new System.Drawing.Point(3, 249);
            this.enableDisablePluginsGroup.MinimumSize = new System.Drawing.Size(0, 100);
            this.enableDisablePluginsGroup.Name = "enableDisablePluginsGroup";
            this.enableDisablePluginsGroup.Size = new System.Drawing.Size(225, 100);
            this.enableDisablePluginsGroup.TabIndex = 10;
            this.enableDisablePluginsGroup.TabStop = false;
            this.enableDisablePluginsGroup.Text = "Enable / Disable single Plugins";
            // 
            // enableDisablePluginListView
            // 
            this.enableDisablePluginListView.CheckBoxes = true;
            this.enableDisablePluginListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.enableDisablePluginListView.FullRowSelect = true;
            this.enableDisablePluginListView.GridLines = true;
            this.enableDisablePluginListView.Location = new System.Drawing.Point(3, 16);
            this.enableDisablePluginListView.Name = "enableDisablePluginListView";
            this.enableDisablePluginListView.Size = new System.Drawing.Size(219, 81);
            this.enableDisablePluginListView.TabIndex = 5;
            this.enableDisablePluginListView.UseCompatibleStateImageBehavior = false;
            this.enableDisablePluginListView.View = System.Windows.Forms.View.SmallIcon;
            this.enableDisablePluginListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.enableDisablePluginListView_ItemChecked);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.startWithSystemCheckBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 417);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 55);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Others";
            // 
            // startWithSystemCheckBox
            // 
            this.startWithSystemCheckBox.AutoSize = true;
            this.startWithSystemCheckBox.Location = new System.Drawing.Point(6, 19);
            this.startWithSystemCheckBox.Name = "startWithSystemCheckBox";
            this.startWithSystemCheckBox.Size = new System.Drawing.Size(133, 17);
            this.startWithSystemCheckBox.TabIndex = 0;
            this.startWithSystemCheckBox.Text = "Start on system startup";
            this.startWithSystemCheckBox.UseVisualStyleBackColor = true;
            this.startWithSystemCheckBox.CheckStateChanged += new System.EventHandler(this.startWithSystemCheckBox_CheckStateChanged);
            // 
            // enableJavascriptPluginsCheckBox
            // 
            this.enableJavascriptPluginsCheckBox.AutoSize = true;
            this.enableJavascriptPluginsCheckBox.Checked = global::SmartHotEdit.Properties.Settings.Default.EnableJavascriptPlugins;
            this.enableJavascriptPluginsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableJavascriptPluginsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SmartHotEdit.Properties.Settings.Default, "EnableJavascriptPlugins", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enableJavascriptPluginsCheckBox.Location = new System.Drawing.Point(9, 111);
            this.enableJavascriptPluginsCheckBox.Name = "enableJavascriptPluginsCheckBox";
            this.enableJavascriptPluginsCheckBox.Size = new System.Drawing.Size(147, 17);
            this.enableJavascriptPluginsCheckBox.TabIndex = 14;
            this.enableJavascriptPluginsCheckBox.Text = "Enable Javascript Plugins";
            this.enableJavascriptPluginsCheckBox.UseVisualStyleBackColor = true;
            // 
            // enablePythonPluginsCheckBox
            // 
            this.enablePythonPluginsCheckBox.AutoSize = true;
            this.enablePythonPluginsCheckBox.Checked = global::SmartHotEdit.Properties.Settings.Default.EnablePythonPlugins;
            this.enablePythonPluginsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enablePythonPluginsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SmartHotEdit.Properties.Settings.Default, "EnablePythonPlugins", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enablePythonPluginsCheckBox.Location = new System.Drawing.Point(9, 88);
            this.enablePythonPluginsCheckBox.Name = "enablePythonPluginsCheckBox";
            this.enablePythonPluginsCheckBox.Size = new System.Drawing.Size(132, 17);
            this.enablePythonPluginsCheckBox.TabIndex = 13;
            this.enablePythonPluginsCheckBox.Text = "Enable Python Plugins";
            this.enablePythonPluginsCheckBox.UseVisualStyleBackColor = true;
            // 
            // enableLuaPluginsCheckBox
            // 
            this.enableLuaPluginsCheckBox.AutoSize = true;
            this.enableLuaPluginsCheckBox.Checked = global::SmartHotEdit.Properties.Settings.Default.EnableLuaPlugins;
            this.enableLuaPluginsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableLuaPluginsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SmartHotEdit.Properties.Settings.Default, "EnableLuaPlugins", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enableLuaPluginsCheckBox.Location = new System.Drawing.Point(9, 65);
            this.enableLuaPluginsCheckBox.Name = "enableLuaPluginsCheckBox";
            this.enableLuaPluginsCheckBox.Size = new System.Drawing.Size(117, 17);
            this.enableLuaPluginsCheckBox.TabIndex = 12;
            this.enableLuaPluginsCheckBox.Text = "Enable Lua Plugins";
            this.enableLuaPluginsCheckBox.UseVisualStyleBackColor = true;
            // 
            // enableDefaultPluginsCheckBox
            // 
            this.enableDefaultPluginsCheckBox.AutoSize = true;
            this.enableDefaultPluginsCheckBox.Checked = global::SmartHotEdit.Properties.Settings.Default.EnableDefaultPlugins;
            this.enableDefaultPluginsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableDefaultPluginsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SmartHotEdit.Properties.Settings.Default, "EnableDefaultPlugins", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enableDefaultPluginsCheckBox.Location = new System.Drawing.Point(9, 42);
            this.enableDefaultPluginsCheckBox.Name = "enableDefaultPluginsCheckBox";
            this.enableDefaultPluginsCheckBox.Size = new System.Drawing.Size(133, 17);
            this.enableDefaultPluginsCheckBox.TabIndex = 11;
            this.enableDefaultPluginsCheckBox.Text = "Enable Default Plugins";
            this.enableDefaultPluginsCheckBox.UseVisualStyleBackColor = true;
            // 
            // enablePluginsCheckBox
            // 
            this.enablePluginsCheckBox.AutoSize = true;
            this.enablePluginsCheckBox.Checked = global::SmartHotEdit.Properties.Settings.Default.EnablePlugins;
            this.enablePluginsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enablePluginsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SmartHotEdit.Properties.Settings.Default, "EnablePlugins", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enablePluginsCheckBox.Location = new System.Drawing.Point(9, 19);
            this.enablePluginsCheckBox.Name = "enablePluginsCheckBox";
            this.enablePluginsCheckBox.Size = new System.Drawing.Size(96, 17);
            this.enablePluginsCheckBox.TabIndex = 10;
            this.enablePluginsCheckBox.Text = "Enable Plugins";
            this.enablePluginsCheckBox.UseVisualStyleBackColor = true;
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
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(231, 467);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsView_FormClosing);
            this.Shown += new System.EventHandler(this.SettingsView_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pluginSettingsGroupBox.ResumeLayout(false);
            this.pluginSettingsGroupBox.PerformLayout();
            this.loggerSettingsGroupBox.ResumeLayout(false);
            this.loggerSettingsGroupBox.PerformLayout();
            this.hotKeySettingGroupBox.ResumeLayout(false);
            this.enableDisablePluginsGroup.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox pluginSettingsGroupBox;
        private System.Windows.Forms.GroupBox loggerSettingsGroupBox;
        private System.Windows.Forms.CheckBox enableLoggingCheckBox;
        private System.Windows.Forms.GroupBox hotKeySettingGroupBox;
        private System.Windows.Forms.Button changeHotKeyButton;
        private HotKeyControl hotKeyTextBox;
        private System.Windows.Forms.GroupBox enableDisablePluginsGroup;
        private System.Windows.Forms.ListView enableDisablePluginListView;
        private System.Windows.Forms.CheckBox enablePythonPluginsCheckBox;
        private System.Windows.Forms.CheckBox enableLuaPluginsCheckBox;
        private System.Windows.Forms.CheckBox enableDefaultPluginsCheckBox;
        private System.Windows.Forms.CheckBox enablePluginsCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox startWithSystemCheckBox;
        private System.Windows.Forms.CheckBox enableJavascriptPluginsCheckBox;
    }
}