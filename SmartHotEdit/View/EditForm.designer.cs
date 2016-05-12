using SmartHotEdit.View.Controls;

namespace SmartHotEdit.View
{
	partial class EditForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.ListView pluginList;
		private System.Windows.Forms.RichTextBox clipboardTextBox;
		private System.Windows.Forms.ListBox functionListUpDown;
        private ArgumentPanel argumentPanel;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.argumentPanel = new SmartHotEdit.View.Controls.ArgumentPanel();
            this.pluginList = new System.Windows.Forms.ListView();
            this.clipboardTextBox = new System.Windows.Forms.RichTextBox();
            this.functionListUpDown = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.argumentPanel);
            this.splitContainer.Panel1.Controls.Add(this.pluginList);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.clipboardTextBox);
            this.splitContainer.Size = new System.Drawing.Size(634, 261);
            this.splitContainer.SplitterDistance = 300;
            this.splitContainer.TabIndex = 0;
            // 
            // argumentPanel
            // 
            this.argumentPanel.Location = new System.Drawing.Point(0, 20);
            this.argumentPanel.Name = "argumentPanel";
            this.argumentPanel.Size = new System.Drawing.Size(110, 50);
            this.argumentPanel.TabIndex = 0;
            this.argumentPanel.Visible = false;
            // 
            // pluginList
            // 
            this.pluginList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginList.Location = new System.Drawing.Point(0, 0);
            this.pluginList.MultiSelect = false;
            this.pluginList.Name = "pluginList";
            this.pluginList.Size = new System.Drawing.Size(300, 261);
            this.pluginList.TabIndex = 0;
            this.pluginList.UseCompatibleStateImageBehavior = false;
            this.pluginList.View = System.Windows.Forms.View.Details;
            this.pluginList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PluginListKeyDown);
            // 
            // clipboardTextBox
            // 
            this.clipboardTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clipboardTextBox.Location = new System.Drawing.Point(0, 0);
            this.clipboardTextBox.Name = "clipboardTextBox";
            this.clipboardTextBox.Size = new System.Drawing.Size(330, 261);
            this.clipboardTextBox.TabIndex = 10;
            this.clipboardTextBox.TabStop = false;
            this.clipboardTextBox.Text = "";
            // 
            // functionListUpDown
            // 
            this.functionListUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.functionListUpDown.Location = new System.Drawing.Point(0, 0);
            this.functionListUpDown.Name = "functionListUpDown";
            this.functionListUpDown.Size = new System.Drawing.Size(120, 17);
            this.functionListUpDown.TabIndex = 1;
            this.functionListUpDown.Visible = false;
            this.functionListUpDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FunctionListUpDownKeyDown);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 261);
            this.Controls.Add(this.functionListUpDown);
            this.Controls.Add(this.splitContainer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditForm";
            this.Load += new System.EventHandler(this.EditFormLoad);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}
    }
}
