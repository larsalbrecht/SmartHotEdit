using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartHotEdit.View.Controls
{
    public partial class ArgumentPanel : UserControl
    {

        public event KeyEventHandler InputKeyDown;
        public String LabelText
        {
            get { return this.argumentNameLabel.Text; }
            set { this.argumentNameLabel.Text = value; }
        }

        public String InputText
        {
            get { return this.functionArgumentInput.Text; }
            set { this.functionArgumentInput.Text = value; }
        }

        public new void Focus()
        {
            this.functionArgumentInput.Focus();
        }

        public ArgumentPanel()
        {
            InitializeComponent();
        }

        private void functionArgumentInput_KeyDown(object sender, KeyEventArgs e)
        {
            this.InputKeyDown?.Invoke(sender, e);
        }
    }
}
