namespace SmartHotEdit.View
{
    partial class PluginScriptEditor
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
            this.scriptTypeLabel = new System.Windows.Forms.Label();
            this.scriptTypeBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorSplitContainer)).BeginInit();
            this.editorSplitContainer.Panel1.SuspendLayout();
            this.editorSplitContainer.Panel2.SuspendLayout();
            this.editorSplitContainer.SuspendLayout();
            this.editorSettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.editorSplitContainer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.editorSettingsGroupBox, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.53846F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.46154F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(558, 340);
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
            this.editorSplitContainer.Size = new System.Drawing.Size(441, 334);
            this.editorSplitContainer.SplitterDistance = 241;
            this.editorSplitContainer.TabIndex = 0;
            // 
            // editorRichTextBox
            // 
            this.editorRichTextBox.AcceptsTab = true;
            this.editorRichTextBox.AutoWordSelection = true;
            this.editorRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.editorRichTextBox.Name = "editorRichTextBox";
            this.editorRichTextBox.Size = new System.Drawing.Size(441, 241);
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
            this.outputRichTextBox.Size = new System.Drawing.Size(441, 89);
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
            this.editorSettingsGroupBox.Size = new System.Drawing.Size(105, 203);
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
            // PluginScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 364);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PluginScriptEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PluginScriptEditor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.editorSplitContainer.Panel1.ResumeLayout(false);
            this.editorSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editorSplitContainer)).EndInit();
            this.editorSplitContainer.ResumeLayout(false);
            this.editorSettingsGroupBox.ResumeLayout(false);
            this.editorSettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer editorSplitContainer;
        private System.Windows.Forms.RichTextBox editorRichTextBox;
        private System.Windows.Forms.RichTextBox outputRichTextBox;
        private System.Windows.Forms.GroupBox editorSettingsGroupBox;
        private System.Windows.Forms.ListBox scriptTypeBox;
        private System.Windows.Forms.Label scriptTypeLabel;
    }
}