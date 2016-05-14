namespace SmartHotEdit.View
{
    partial class ScriptPluginEditor
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.editorSplitContainer = new System.Windows.Forms.SplitContainer();
            this.editorRichTextBox = new System.Windows.Forms.RichTextBox();
            this.outputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.editorSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.scriptTypeBox = new System.Windows.Forms.ListBox();
            this.scriptTypeLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorSplitContainer)).BeginInit();
            this.editorSplitContainer.Panel1.SuspendLayout();
            this.editorSplitContainer.Panel2.SuspendLayout();
            this.editorSplitContainer.SuspendLayout();
            this.editorSettingsGroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.editorSplitContainer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.editorSettingsGroupBox, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.53846F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.46154F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(558, 325);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // editorSplitContainer
            // 
            this.editorSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorSplitContainer.Location = new System.Drawing.Point(114, 3);
            this.editorSplitContainer.Name = "editorSplitContainer";
            this.editorSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // editorSplitContainer.Panel1
            // 
            this.editorSplitContainer.Panel1.Controls.Add(this.editorRichTextBox);
            // 
            // editorSplitContainer.Panel2
            // 
            this.editorSplitContainer.Panel2.Controls.Add(this.outputRichTextBox);
            this.tableLayoutPanel1.SetRowSpan(this.editorSplitContainer, 2);
            this.editorSplitContainer.Size = new System.Drawing.Size(441, 319);
            this.editorSplitContainer.SplitterDistance = 230;
            this.editorSplitContainer.TabIndex = 0;
            // 
            // editorRichTextBox
            // 
            this.editorRichTextBox.AcceptsTab = true;
            this.editorRichTextBox.AutoWordSelection = true;
            this.editorRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.editorRichTextBox.Name = "editorRichTextBox";
            this.editorRichTextBox.Size = new System.Drawing.Size(441, 230);
            this.editorRichTextBox.TabIndex = 1;
            this.editorRichTextBox.Text = "";
            // 
            // outputRichTextBox
            // 
            this.outputRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.outputRichTextBox.Name = "outputRichTextBox";
            this.outputRichTextBox.ReadOnly = true;
            this.outputRichTextBox.Size = new System.Drawing.Size(441, 85);
            this.outputRichTextBox.TabIndex = 0;
            this.outputRichTextBox.Text = "";
            // 
            // editorSettingsGroupBox
            // 
            this.editorSettingsGroupBox.Controls.Add(this.scriptTypeBox);
            this.editorSettingsGroupBox.Controls.Add(this.scriptTypeLabel);
            this.editorSettingsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorSettingsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.editorSettingsGroupBox.Name = "editorSettingsGroupBox";
            this.editorSettingsGroupBox.Size = new System.Drawing.Size(105, 193);
            this.editorSettingsGroupBox.TabIndex = 1;
            this.editorSettingsGroupBox.TabStop = false;
            this.editorSettingsGroupBox.Text = "Settings";
            // 
            // scriptTypeBox
            // 
            this.scriptTypeBox.FormattingEnabled = true;
            this.scriptTypeBox.Items.AddRange(new object[] {
            "Lua",
            "Python",
            "Javascript"});
            this.scriptTypeBox.Location = new System.Drawing.Point(6, 32);
            this.scriptTypeBox.Name = "scriptTypeBox";
            this.scriptTypeBox.Size = new System.Drawing.Size(93, 17);
            this.scriptTypeBox.TabIndex = 1;
            // 
            // scriptTypeLabel
            // 
            this.scriptTypeLabel.AutoSize = true;
            this.scriptTypeLabel.Location = new System.Drawing.Point(6, 16);
            this.scriptTypeLabel.Name = "scriptTypeLabel";
            this.scriptTypeLabel.Size = new System.Drawing.Size(61, 13);
            this.scriptTypeLabel.TabIndex = 0;
            this.scriptTypeLabel.Text = "Script Type";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(582, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem3,
            this.saveToolStripMenuItem1,
            this.toolStripMenuItem4,
            this.closeToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "New";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Open";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(40, 20);
            this.toolStripMenuItem2.Text = "Run";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(149, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // ScriptPluginEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 364);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ScriptPluginEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PluginScriptEditor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.editorSplitContainer.Panel1.ResumeLayout(false);
            this.editorSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editorSplitContainer)).EndInit();
            this.editorSplitContainer.ResumeLayout(false);
            this.editorSettingsGroupBox.ResumeLayout(false);
            this.editorSettingsGroupBox.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer editorSplitContainer;
        private System.Windows.Forms.RichTextBox editorRichTextBox;
        private System.Windows.Forms.RichTextBox outputRichTextBox;
        private System.Windows.Forms.GroupBox editorSettingsGroupBox;
        private System.Windows.Forms.ListBox scriptTypeBox;
        private System.Windows.Forms.Label scriptTypeLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}