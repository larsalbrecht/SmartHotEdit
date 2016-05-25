using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartHotEdit.View.Controls
{
    public sealed class HotKeyControl : RichTextBox
    {
        /*
         * Property to contain the Hotkey value.
         */
        private Keys _hotkey = Keys.None;

        /*
         * Property to contain the Modifiers value.
         */
        private Keys _modifiers = Keys.None;

        private bool _winModifier;

        /*
         * When the class is constructed: Modify some properties by default and add event call backs.
         */

        public HotKeyControl()
        {
            this.Cursor = Cursors.Arrow;
            this.ReadOnly = true;
            this.BackColor = Color.White;

            this.KeyPress += Control_KeyPress;
            this.KeyDown += Control_KeyDown;
            this.KeyUp += Control_KeyUp;
            this.SelectionChanged += Control_SelectionChanged;
        }

        public Keys Hotkey
        {
            get { return _hotkey; }
            set
            {
                _hotkey = value;
                Redraw();
            }
        }

        public Keys Modifiers
        {
            get { return _modifiers; }
            set
            {
                _modifiers = value;
                Redraw();
            }
        }

        public bool WinModifier
        {
            get { return _winModifier; }
            set
            {
                _winModifier = value;
                Redraw();
            }
        }

        private void ResetHotKeys()
        {
            Hotkey = Keys.None;
            Modifiers = Keys.None;
            WinModifier = false;
            this.Text = @"None";
        }

        /*
         * Bypassess the controls default handling. 
         */

        private static void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        /*
         * Obtain the Modifiers and Key then 'draw' the string.
         */

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                ResetHotKeys();
                return;
            }

            // Workaround to handle WinKeys as modifiers
            Modifiers = e.Modifiers;
            if (e.KeyCode == Keys.LWin || e.KeyCode == Keys.RWin)
            {
                WinModifier = true;
            }
            Hotkey = e.KeyCode;
            Redraw();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }


        /*
         * This isn't really required.
         */

        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            if (Hotkey == Keys.None && ModifierKeys == Keys.None)
            {
                ResetHotKeys();
            }
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        /*
         * This prevents the user from selecting text (my preference).
         */

        private void Control_SelectionChanged(object sender, EventArgs e)
        {
            if (this.SelectionStart != this.TextLength)
            {
                this.SelectionStart = this.TextLength;
            }
        }

        /*
         * This method is called to print the hotkey combination.
         */

        private void Redraw()
        {
            if (Modifiers != Keys.None)
            {
                this.Text = Modifiers.ToString().Replace(", ", " + ");
            }

            if (Hotkey == Keys.None)
            {
                return;
            }

            // Will handled otherwise
            if (Hotkey == Keys.LWin || Hotkey == Keys.RWin)
            {
                return;
            }

            if (Hotkey == Keys.Menu || Hotkey == Keys.ShiftKey || Hotkey == Keys.ControlKey)
            {
                Hotkey = Keys.None;
                return;
            }

            if (Modifiers != Keys.None)
            {
                this.Text = Modifiers.ToString().Replace(", ", " + ") + @" + " + (WinModifier ? "Win + " : "") + Hotkey;
                return;
            }

            this.Text = Hotkey.ToString();
        }
    }
}