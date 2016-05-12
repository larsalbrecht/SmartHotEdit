namespace SmartHotEdit.View.Controls
{
    partial class ArgumentPanel
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.functionArgumentInput = new System.Windows.Forms.TextBox();
            this.argumentNameLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.functionArgumentInput, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.argumentNameLabel, 0, 0);
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(110, 50);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // functionArgumentInput
            // 
            this.functionArgumentInput.Location = new System.Drawing.Point(3, 23);
            this.functionArgumentInput.Name = "functionArgumentInput";
            this.functionArgumentInput.Size = new System.Drawing.Size(100, 20);
            this.functionArgumentInput.TabIndex = 2;
            this.functionArgumentInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.functionArgumentInput_KeyDown);
            // 
            // argumentNameLabel
            // 
            this.argumentNameLabel.AutoSize = true;
            this.argumentNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentNameLabel.Location = new System.Drawing.Point(3, 0);
            this.argumentNameLabel.Name = "argumentNameLabel";
            this.argumentNameLabel.Padding = new System.Windows.Forms.Padding(1, 3, 0, 0);
            this.argumentNameLabel.Size = new System.Drawing.Size(104, 20);
            this.argumentNameLabel.TabIndex = 0;
            this.argumentNameLabel.Text = "n/a";
            // 
            // ArgumentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "ArgumentPanel";
            this.Size = new System.Drawing.Size(108, 48);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label argumentNameLabel;
        private System.Windows.Forms.TextBox functionArgumentInput;
    }
}
