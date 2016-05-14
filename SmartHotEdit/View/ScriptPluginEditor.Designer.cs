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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.editorSplitContainer = new System.Windows.Forms.SplitContainer();
            this.scintilla = new ScintillaNET.Scintilla();
            this.outputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.editorSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.scriptTypeLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.openScriptDialog = new System.Windows.Forms.OpenFileDialog();
            this.aScriptPluginControllerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.scriptTypeList = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorSplitContainer)).BeginInit();
            this.editorSplitContainer.Panel1.SuspendLayout();
            this.editorSplitContainer.Panel2.SuspendLayout();
            this.editorSplitContainer.SuspendLayout();
            this.editorSettingsGroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aScriptPluginControllerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.10714F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.89286F));
            this.tableLayoutPanel1.Controls.Add(this.editorSplitContainer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.editorSettingsGroupBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.53846F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.46154F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 337);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // editorSplitContainer
            // 
            this.editorSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorSplitContainer.Location = new System.Drawing.Point(192, 3);
            this.editorSplitContainer.Name = "editorSplitContainer";
            this.editorSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // editorSplitContainer.Panel1
            // 
            this.editorSplitContainer.Panel1.Controls.Add(this.scintilla);
            // 
            // editorSplitContainer.Panel2
            // 
            this.editorSplitContainer.Panel2.Controls.Add(this.outputRichTextBox);
            this.tableLayoutPanel1.SetRowSpan(this.editorSplitContainer, 2);
            this.editorSplitContainer.Size = new System.Drawing.Size(589, 331);
            this.editorSplitContainer.SplitterDistance = 238;
            this.editorSplitContainer.TabIndex = 0;
            // 
            // scintilla
            // 
            this.scintilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintilla.EolMode = ScintillaNET.Eol.Lf;
            this.scintilla.IndentationGuides = ScintillaNET.IndentView.Real;
            this.scintilla.Location = new System.Drawing.Point(0, 0);
            this.scintilla.Name = "scintilla";
            this.scintilla.Size = new System.Drawing.Size(589, 238);
            this.scintilla.TabIndex = 2;
            this.scintilla.UseTabs = false;
            // 
            // outputRichTextBox
            // 
            this.outputRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.outputRichTextBox.Name = "outputRichTextBox";
            this.outputRichTextBox.ReadOnly = true;
            this.outputRichTextBox.Size = new System.Drawing.Size(589, 89);
            this.outputRichTextBox.TabIndex = 0;
            this.outputRichTextBox.Text = "";
            // 
            // editorSettingsGroupBox
            // 
            this.editorSettingsGroupBox.Controls.Add(this.scriptTypeList);
            this.editorSettingsGroupBox.Controls.Add(this.scriptTypeLabel);
            this.editorSettingsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorSettingsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.editorSettingsGroupBox.Name = "editorSettingsGroupBox";
            this.editorSettingsGroupBox.Size = new System.Drawing.Size(183, 201);
            this.editorSettingsGroupBox.TabIndex = 1;
            this.editorSettingsGroupBox.TabStop = false;
            this.editorSettingsGroupBox.Text = "Settings";
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
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenuItem,
            this.openMenuItem,
            this.toolStripMenuItem3,
            this.saveMenuItem,
            this.toolStripMenuItem4,
            this.closeMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // newMenuItem
            // 
            this.newMenuItem.Name = "newMenuItem";
            this.newMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newMenuItem.Text = "New";
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openMenuItem.Text = "Open";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(100, 6);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveMenuItem.Text = "Save";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(100, 6);
            // 
            // closeMenuItem
            // 
            this.closeMenuItem.Name = "closeMenuItem";
            this.closeMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeMenuItem.Text = "Close";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(40, 20);
            this.toolStripMenuItem2.Text = "Run";
            // 
            // openScriptDialog
            // 
            this.openScriptDialog.DefaultExt = "*.*";
            // 
            // aScriptPluginControllerBindingSource
            // 
            this.aScriptPluginControllerBindingSource.DataSource = typeof(SmartHotEdit.Abstracts.AScriptPluginController);
            // 
            // scriptTypeList
            // 
            this.scriptTypeList.FormattingEnabled = true;
            this.scriptTypeList.Location = new System.Drawing.Point(9, 32);
            this.scriptTypeList.Name = "scriptTypeList";
            this.scriptTypeList.Size = new System.Drawing.Size(168, 17);
            this.scriptTypeList.TabIndex = 2;
            // 
            // ScriptPluginEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
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
            ((System.ComponentModel.ISupportInitialize)(this.aScriptPluginControllerBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer editorSplitContainer;
        private System.Windows.Forms.RichTextBox outputRichTextBox;
        private System.Windows.Forms.GroupBox editorSettingsGroupBox;
        private System.Windows.Forms.Label scriptTypeLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem newMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem closeMenuItem;
        private ScintillaNET.Scintilla scintilla;
        private System.Windows.Forms.OpenFileDialog openScriptDialog;
        private System.Windows.Forms.BindingSource aScriptPluginControllerBindingSource;
        private System.Windows.Forms.ListBox scriptTypeList;
    }
}