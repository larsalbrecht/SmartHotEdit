using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SmartHotEdit.Handler
{
    internal static class NativeMethods
    {
        #region Interop

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int UnregisterHotKey(IntPtr hWnd, int id);

        internal const uint WM_HOTKEY = 0x312;

        internal const uint MOD_ALT = 0x1;
        internal const uint MOD_CONTROL = 0x2;
        internal const uint MOD_SHIFT = 0x4;
        internal const uint MOD_WIN = 0x8;

        internal const uint ERROR_HOTKEY_ALREADY_REGISTERED = 1409;

        #endregion
    }

    public class HotkeyHandler : IMessageFilter
    {
        private const int maximumID = 0xBFFF;

        private static int currentID;
        private bool alt;
        private bool control;

        [XmlIgnore] private int id;

        private Keys keyCode;

        [XmlIgnore] private bool registered;

        private bool shift;

        [XmlIgnore] private Control windowControl;

        private bool windows;

        public HotkeyHandler() : this(Keys.None, false, false, false, false)
        {
            // No work done here!
        }

        public HotkeyHandler(Keys keyCode, bool shift, bool control, bool alt, bool windows)
        {
            // Assign properties
            this.KeyCode = keyCode;
            this.Shift = shift;
            this.Control = control;
            this.Alt = alt;
            this.Windows = windows;

            // Register us as a message filter
            Application.AddMessageFilter(this);
        }

        public bool Empty
        {
            get { return this.keyCode == Keys.None; }
        }

        public bool Registered
        {
            get { return this.registered; }
        }

        public Keys KeyCode
        {
            get { return this.keyCode; }
            set
            {
                // Save and reregister
                this.keyCode = value;
                this.Reregister();
            }
        }

        public bool Shift
        {
            get { return this.shift; }
            set
            {
                // Save and reregister
                this.shift = value;
                this.Reregister();
            }
        }

        public bool Control
        {
            get { return this.control; }
            set
            {
                // Save and reregister
                this.control = value;
                this.Reregister();
            }
        }

        public bool Alt
        {
            get { return this.alt; }
            set
            {
                // Save and reregister
                this.alt = value;
                this.Reregister();
            }
        }

        public bool Windows
        {
            get { return this.windows; }
            set
            {
                // Save and reregister
                this.windows = value;
                this.Reregister();
            }
        }

        public bool PreFilterMessage(ref Message message)
        {
            // Only process WM_HOTKEY messages
            if (message.Msg != NativeMethods.WM_HOTKEY)
            {
                return false;
            }

            // Check that the ID is our key and we are registerd
            if (this.registered && (message.WParam.ToInt32() == this.id))
            {
                // Fire the event and pass on the event if our handlers didn't handle it
                return this.OnPressed();
            }
            return false;
        }

        public event HandledEventHandler Pressed;

        ~HotkeyHandler()
        {
            // Unregister the hotkey if necessary
            if (this.Registered)
            {
                this.Unregister();
            }
        }

        public HotkeyHandler Clone()
        {
            // Clone the whole object
            return new HotkeyHandler(this.keyCode, this.shift, this.control, this.alt, this.windows);
        }

        public bool GetCanRegister(Control windowControl)
        {
            // Handle any exceptions: they mean "no, you can't register" :)
            try
            {
                // Attempt to register
                if (!this.Register(windowControl))
                {
                    return false;
                }

                // Unregister and say we managed it
                this.Unregister();
                return true;
            }
            catch (Win32Exception)
            {
                return false;
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }

        public bool Register(Control windowControl)
        {
            // Check that we have not registered
            if (this.registered)
            {
                throw new NotSupportedException("You cannot register a hotkey that is already registered");
            }

            // We can't register an empty hotkey
            if (this.Empty)
            {
                throw new NotSupportedException("You cannot register an empty hotkey");
            }

            // Get an ID for the hotkey and increase current ID
            this.id = currentID;
            currentID = currentID + 1%maximumID;

            // Translate modifier keys into unmanaged version
            var modifiers = (this.Alt ? NativeMethods.MOD_ALT : 0) | (this.Control ? NativeMethods.MOD_CONTROL : 0) |
                            (this.Shift ? NativeMethods.MOD_SHIFT : 0) | (this.Windows ? NativeMethods.MOD_WIN : 0);

            // Register the hotkey
            if (NativeMethods.RegisterHotKey(windowControl.Handle, this.id, modifiers, keyCode) == 0)
            {
                // Is the error that the hotkey is registered?
                if (Marshal.GetLastWin32Error() == NativeMethods.ERROR_HOTKEY_ALREADY_REGISTERED)
                {
                    return false;
                }
                throw new Win32Exception();
            }

            // Save the control reference and register state
            this.registered = true;
            this.windowControl = windowControl;

            // We successfully registered
            return true;
        }

        public void Unregister()
        {
            // Check that we have registered
            if (!this.registered)
            {
                throw new NotSupportedException("You cannot unregister a hotkey that is not registered");
            }

            // It's possible that the control itself has died: in that case, no need to unregister!
            if (!this.windowControl.IsDisposed)
            {
                // Clean up after ourselves
                if (NativeMethods.UnregisterHotKey(this.windowControl.Handle, this.id) == 0)
                {
                    throw new Win32Exception();
                }
            }

            // Clear the control reference and register state
            this.registered = false;
            this.windowControl = null;
        }

        private void Reregister()
        {
            // Only do something if the key is already registered
            if (!this.registered)
            {
                return;
            }

            // Save control reference
            var windowControl = this.windowControl;

            // Unregister and then reregister again
            this.Unregister();
            this.Register(windowControl);
        }

        private bool OnPressed()
        {
            // Fire the event if we can
            var handledEventArgs = new HandledEventArgs(false);
            if (this.Pressed != null)
            {
                this.Pressed(this, handledEventArgs);
            }

            // Return whether we handled the event or not
            return handledEventArgs.Handled;
        }

        public override string ToString()
        {
            // We can be empty
            if (this.Empty)
            {
                return "(none)";
            }

            // Build key name
            var keyName = Enum.GetName(typeof(Keys), this.keyCode);
            ;
            switch (this.keyCode)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    // Strip the first character
                    keyName = keyName.Substring(1);
                    break;
                default:
                    // Leave everything alone
                    break;
            }

            // Build modifiers
            var modifiers = "";
            if (this.shift)
            {
                modifiers += "Shift+";
            }
            if (this.control)
            {
                modifiers += "Control+";
            }
            if (this.alt)
            {
                modifiers += "Alt+";
            }
            if (this.windows)
            {
                modifiers += "Windows+";
            }

            // Return result
            return modifiers + keyName;
        }
    }
}