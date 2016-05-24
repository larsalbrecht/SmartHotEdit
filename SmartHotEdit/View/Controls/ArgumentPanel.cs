using System.Windows.Forms;

namespace SmartHotEdit.View.Controls
{
    public partial class ArgumentPanel : UserControl
    {
        public ArgumentPanel()
        {
            InitializeComponent();
        }

        public string LabelText
        {
            get { return this.argumentNameLabel.Text; }
            set { this.argumentNameLabel.Text = value; }
        }

        public string InputText
        {
            get { return this.functionArgumentInput.Text; }
            set { this.functionArgumentInput.Text = value; }
        }

        public event KeyEventHandler InputKeyDown;

        public new void Focus()
        {
            this.functionArgumentInput.Focus();
        }

        private void functionArgumentInput_KeyDown(object sender, KeyEventArgs e)
        {
            this.InputKeyDown?.Invoke(sender, e);
        }
    }
}