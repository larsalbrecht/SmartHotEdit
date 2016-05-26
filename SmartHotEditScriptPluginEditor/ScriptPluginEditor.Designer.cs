namespace SmartHotEdit.View.Editor
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
            this.baseTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.editorSplitContainer = new System.Windows.Forms.SplitContainer();
            this.scintilla = new ScintillaNET.Scintilla();
            this.outputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.editorSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.scriptTypeList = new System.Windows.Forms.ListBox();
            this.scriptTypeLabel = new System.Windows.Forms.Label();
            this.pluginInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.functionsListView = new System.Windows.Forms.ListView();
            this.functionLabel = new System.Windows.Forms.Label();
            this.functionTitleLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTitleLabel = new System.Windows.Forms.Label();
            this.openScriptDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveScriptDialog = new System.Windows.Forms.SaveFileDialog();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.fileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.fileCloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runTestMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.templateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.templateLoadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.extrasMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baseTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorSplitContainer)).BeginInit();
            this.editorSplitContainer.Panel1.SuspendLayout();
            this.editorSplitContainer.Panel2.SuspendLayout();
            this.editorSplitContainer.SuspendLayout();
            this.editorSettingsGroupBox.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.pluginInfoGroupBox.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // baseTableLayout
            // 
            this.baseTableLayout.AutoSize = true;
            this.baseTableLayout.ColumnCount = 2;
            this.baseTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.10714F));
            this.baseTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.89286F));
            this.baseTableLayout.Controls.Add(this.editorSplitContainer, 1, 0);
            this.baseTableLayout.Controls.Add(this.editorSettingsGroupBox, 0, 0);
            this.baseTableLayout.Controls.Add(this.pluginInfoGroupBox, 0, 1);
            this.baseTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseTableLayout.Location = new System.Drawing.Point(0, 24);
            this.baseTableLayout.Name = "baseTableLayout";
            this.baseTableLayout.RowCount = 2;
            this.baseTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.53846F));
            this.baseTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.46154F));
            this.baseTableLayout.Size = new System.Drawing.Size(784, 387);
            this.baseTableLayout.TabIndex = 0;
            // 
            // editorSplitContainer
            // 
            this.editorSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorSplitContainer.Location = new System.Drawing.Point(191, 3);
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
            this.baseTableLayout.SetRowSpan(this.editorSplitContainer, 2);
            this.editorSplitContainer.Size = new System.Drawing.Size(590, 381);
            this.editorSplitContainer.SplitterDistance = 273;
            this.editorSplitContainer.TabIndex = 0;
            // 
            // scintilla
            // 
            this.scintilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintilla.EdgeColumn = 120;
            this.scintilla.EdgeMode = ScintillaNET.EdgeMode.Line;
            this.scintilla.EolMode = ScintillaNET.Eol.Lf;
            this.scintilla.IndentationGuides = ScintillaNET.IndentView.Real;
            this.scintilla.Location = new System.Drawing.Point(0, 0);
            this.scintilla.Name = "scintilla";
            this.scintilla.Size = new System.Drawing.Size(590, 273);
            this.scintilla.TabIndex = 2;
            this.scintilla.UseTabs = false;
            this.scintilla.WrapIndentMode = ScintillaNET.WrapIndentMode.Indent;
            this.scintilla.WrapMode = ScintillaNET.WrapMode.Word;
            this.scintilla.WrapVisualFlags = ((ScintillaNET.WrapVisualFlags)((ScintillaNET.WrapVisualFlags.End | ScintillaNET.WrapVisualFlags.Start)));
            this.scintilla.TextChanged += new System.EventHandler(this.scintilla_TextChanged);
            // 
            // outputRichTextBox
            // 
            this.outputRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.outputRichTextBox.Name = "outputRichTextBox";
            this.outputRichTextBox.ReadOnly = true;
            this.outputRichTextBox.Size = new System.Drawing.Size(590, 104);
            this.outputRichTextBox.TabIndex = 10;
            this.outputRichTextBox.Text = "";
            // 
            // editorSettingsGroupBox
            // 
            this.editorSettingsGroupBox.Controls.Add(this.tableLayoutPanel2);
            this.editorSettingsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorSettingsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.editorSettingsGroupBox.Name = "editorSettingsGroupBox";
            this.editorSettingsGroupBox.Size = new System.Drawing.Size(182, 232);
            this.editorSettingsGroupBox.TabIndex = 1;
            this.editorSettingsGroupBox.TabStop = false;
            this.editorSettingsGroupBox.Text = "Settings";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.scriptTypeList, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.scriptTypeLabel, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(9, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.78022F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.21978F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(167, 100);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // scriptTypeList
            // 
            this.scriptTypeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptTypeList.FormattingEnabled = true;
            this.scriptTypeList.Location = new System.Drawing.Point(3, 22);
            this.scriptTypeList.Name = "scriptTypeList";
            this.scriptTypeList.Size = new System.Drawing.Size(161, 75);
            this.scriptTypeList.TabIndex = 1;
            this.scriptTypeList.SelectedValueChanged += new System.EventHandler(this.scriptTypeList_SelectedValueChanged);
            // 
            // scriptTypeLabel
            // 
            this.scriptTypeLabel.AutoSize = true;
            this.scriptTypeLabel.Location = new System.Drawing.Point(3, 0);
            this.scriptTypeLabel.Name = "scriptTypeLabel";
            this.scriptTypeLabel.Size = new System.Drawing.Size(61, 13);
            this.scriptTypeLabel.TabIndex = 5;
            this.scriptTypeLabel.Text = "Script Type";
            // 
            // pluginInfoGroupBox
            // 
            this.pluginInfoGroupBox.Controls.Add(this.functionsListView);
            this.pluginInfoGroupBox.Controls.Add(this.functionLabel);
            this.pluginInfoGroupBox.Controls.Add(this.functionTitleLabel);
            this.pluginInfoGroupBox.Controls.Add(this.nameLabel);
            this.pluginInfoGroupBox.Controls.Add(this.nameTitleLabel);
            this.pluginInfoGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginInfoGroupBox.Location = new System.Drawing.Point(3, 241);
            this.pluginInfoGroupBox.Name = "pluginInfoGroupBox";
            this.pluginInfoGroupBox.Size = new System.Drawing.Size(182, 143);
            this.pluginInfoGroupBox.TabIndex = 2;
            this.pluginInfoGroupBox.TabStop = false;
            this.pluginInfoGroupBox.Text = "Plugin Information";
            // 
            // functionsListView
            // 
            this.functionsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.functionsListView.Location = new System.Drawing.Point(6, 51);
            this.functionsListView.Name = "functionsListView";
            this.functionsListView.Size = new System.Drawing.Size(170, 86);
            this.functionsListView.TabIndex = 3;
            this.functionsListView.UseCompatibleStateImageBehavior = false;
            this.functionsListView.View = System.Windows.Forms.View.Details;
            // 
            // functionLabel
            // 
            this.functionLabel.AutoSize = true;
            this.functionLabel.Location = new System.Drawing.Point(68, 35);
            this.functionLabel.Name = "functionLabel";
            this.functionLabel.Size = new System.Drawing.Size(24, 13);
            this.functionLabel.TabIndex = 3;
            this.functionLabel.Text = "n/a";
            // 
            // functionTitleLabel
            // 
            this.functionTitleLabel.AutoSize = true;
            this.functionTitleLabel.Location = new System.Drawing.Point(6, 35);
            this.functionTitleLabel.Name = "functionTitleLabel";
            this.functionTitleLabel.Size = new System.Drawing.Size(56, 13);
            this.functionTitleLabel.TabIndex = 2;
            this.functionTitleLabel.Text = "Functions:";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(50, 18);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(24, 13);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "n/a";
            // 
            // nameTitleLabel
            // 
            this.nameTitleLabel.AutoSize = true;
            this.nameTitleLabel.Location = new System.Drawing.Point(6, 18);
            this.nameTitleLabel.Name = "nameTitleLabel";
            this.nameTitleLabel.Size = new System.Drawing.Size(38, 13);
            this.nameTitleLabel.TabIndex = 0;
            this.nameTitleLabel.Text = "Name:";
            // 
            // openScriptDialog
            // 
            this.openScriptDialog.DefaultExt = "*.*";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNewMenuItem,
            this.fileOpenMenuItem,
            this.toolStripMenuItem3,
            this.fileSaveMenuItem,
            this.toolStripMenuItem4,
            this.fileCloseMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "&File";
            // 
            // fileNewMenuItem
            // 
            this.fileNewMenuItem.Name = "fileNewMenuItem";
            this.fileNewMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.fileNewMenuItem.Size = new System.Drawing.Size(148, 22);
            this.fileNewMenuItem.Text = "New";
            this.fileNewMenuItem.Click += new System.EventHandler(this.fileNewMenuItem_Click);
            // 
            // fileOpenMenuItem
            // 
            this.fileOpenMenuItem.Name = "fileOpenMenuItem";
            this.fileOpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.fileOpenMenuItem.Size = new System.Drawing.Size(148, 22);
            this.fileOpenMenuItem.Text = "Open";
            this.fileOpenMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(145, 6);
            // 
            // fileSaveMenuItem
            // 
            this.fileSaveMenuItem.Name = "fileSaveMenuItem";
            this.fileSaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.fileSaveMenuItem.Size = new System.Drawing.Size(148, 22);
            this.fileSaveMenuItem.Text = "Save";
            this.fileSaveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(145, 6);
            // 
            // fileCloseMenuItem
            // 
            this.fileCloseMenuItem.Name = "fileCloseMenuItem";
            this.fileCloseMenuItem.Size = new System.Drawing.Size(148, 22);
            this.fileCloseMenuItem.Text = "Close";
            this.fileCloseMenuItem.Click += new System.EventHandler(this.closeMenuItem_Click);
            // 
            // testMenuItem
            // 
            this.testMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runTestMenuItem});
            this.testMenuItem.Name = "testMenuItem";
            this.testMenuItem.Size = new System.Drawing.Size(40, 20);
            this.testMenuItem.Text = "R&un";
            // 
            // runTestMenuItem
            // 
            this.runTestMenuItem.Name = "runTestMenuItem";
            this.runTestMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.runTestMenuItem.Size = new System.Drawing.Size(114, 22);
            this.runTestMenuItem.Text = "Test";
            this.runTestMenuItem.Click += new System.EventHandler(this.runTestMenuItem_Click);
            // 
            // templateMenuItem
            // 
            this.templateMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.templateLoadMenuItem});
            this.templateMenuItem.Name = "templateMenuItem";
            this.templateMenuItem.Size = new System.Drawing.Size(68, 20);
            this.templateMenuItem.Text = "&Template";
            // 
            // templateLoadMenuItem
            // 
            this.templateLoadMenuItem.Name = "templateLoadMenuItem";
            this.templateLoadMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.L)));
            this.templateLoadMenuItem.Size = new System.Drawing.Size(165, 22);
            this.templateLoadMenuItem.Text = "Load";
            this.templateLoadMenuItem.Click += new System.EventHandler(this.templateLoadMenuItem_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.testMenuItem,
            this.templateMenuItem,
            this.extrasMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(784, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // extrasMenuItem
            // 
            this.extrasMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.extrasMenuItem.Enabled = false;
            this.extrasMenuItem.Name = "extrasMenuItem";
            this.extrasMenuItem.Size = new System.Drawing.Size(49, 20);
            this.extrasMenuItem.Text = "&Extras";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // ScriptPluginEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.baseTableLayout);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "ScriptPluginEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PluginScriptEditor";
            this.Load += new System.EventHandler(this.ScriptPluginEditor_Load);
            this.ResizeEnd += new System.EventHandler(this.ScriptPluginEditor_ResizeEnd);
            this.baseTableLayout.ResumeLayout(false);
            this.editorSplitContainer.Panel1.ResumeLayout(false);
            this.editorSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editorSplitContainer)).EndInit();
            this.editorSplitContainer.ResumeLayout(false);
            this.editorSettingsGroupBox.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.pluginInfoGroupBox.ResumeLayout(false);
            this.pluginInfoGroupBox.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel baseTableLayout;
        private System.Windows.Forms.SplitContainer editorSplitContainer;
        private System.Windows.Forms.RichTextBox outputRichTextBox;
        private System.Windows.Forms.GroupBox editorSettingsGroupBox;
        private ScintillaNET.Scintilla scintilla;
        private System.Windows.Forms.OpenFileDialog openScriptDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox scriptTypeList;
        private System.Windows.Forms.Label scriptTypeLabel;
        private System.Windows.Forms.SaveFileDialog saveScriptDialog;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileNewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileOpenMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem fileSaveMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem fileCloseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runTestMenuItem;
        private System.Windows.Forms.ToolStripMenuItem templateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem templateLoadMenuItem;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.GroupBox pluginInfoGroupBox;
        private System.Windows.Forms.Label functionLabel;
        private System.Windows.Forms.Label functionTitleLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label nameTitleLabel;
        private System.Windows.Forms.ListView functionsListView;
        private System.Windows.Forms.ToolStripMenuItem extrasMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    }
}