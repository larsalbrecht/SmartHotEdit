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
        public ArgumentPanel()
        {
            InitializeComponent();
        }

        public Label getLabel()
        {
            return this.argumentNameLabel;
        }

        public TextBox getInput()
        {
            return this.functionArgumentInput;
        }

    }
}
