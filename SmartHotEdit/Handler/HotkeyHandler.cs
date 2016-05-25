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

        internal const uint WmHotkey = 0x312;

        internal const uint ModAlt = 0x1;
        internal const uint ModControl = 0x2;
        internal const uint ModShift = 0x4;
        internal const uint ModWin = 0x8;

        internal const uint ErrorHotkeyAlreadyRegistered = 1409;

        #endregion
    }

    public class HotkeyHandler : IMessageFilter
    {
        private const int MaximumId = 0xBFFF;

        private static int _currentId;
        private bool _alt;
        private bool _control;

        [XmlIgnore] private int _id;

        private Keys _keyCode;

        [XmlIgnore] private bool _registered;

        private bool _shift;

        [XmlIgnore] private Control _windowControl;

        private bool _windows;

        public HotkeyHandler() : this(Keys.None, false, false, false, false)
        {
            // No work done here!
        }

        private HotkeyHandler(Keys keyCode, bool shift, bool control, bool alt, bool windows)
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

        private bool Empty => this._keyCode == Keys.None;

        public bool Registered => this._registered;

        public Keys KeyCode
        {
            set
            {
                // Save and reregister
                this._keyCode = value;
                this.Reregister();
            }
        }

        public bool Shift
        {
            private get { return this._shift; }
            set
            {
                // Save and reregister
                this._shift = value;
                this.Reregister();
            }
        }

        public bool Control
        {
            private get { return this._control; }
            set
            {
                // Save and reregister
                this._control = value;
                this.Reregister();
            }
        }

        public bool Alt
        {
            private get { return this._alt; }
            set
            {
                // Save and reregister
                this._alt = value;
                this.Reregister();
            }
        }

        public bool Windows
        {
            private get { return this._windows; }
            set
            {
                // Save and reregister
                this._windows = value;
                this.Reregister();
            }
        }

        public bool PreFilterMessage(ref Message message)
        {
            // Only process WM_HOTKEY messages
            if (message.Msg != NativeMethods.WmHotkey)
            {
                return false;
            }

            // Check that the ID is our key and we are registerd
            if (this._registered && (message.WParam.ToInt32() == this._id))
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

        // ReSharper disable once UnusedMember.Global
        public HotkeyHandler Clone()
        {
            // Clone the whole object
            return new HotkeyHandler(this._keyCode, this._shift, this._control, this._alt, this._windows);
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
            if (this._registered)
            {
                throw new NotSupportedException("You cannot register a hotkey that is already registered");
            }

            // We can't register an empty hotkey
            if (this.Empty)
            {
                throw new NotSupportedException("You cannot register an empty hotkey");
            }

            // Get an ID for the hotkey and increase current ID
            this._id = _currentId;
            _currentId = _currentId + 1%MaximumId;

            // Translate modifier keys into unmanaged version
            var modifiers = (this.Alt ? NativeMethods.ModAlt : 0) | (this.Control ? NativeMethods.ModControl : 0) |
                            (this.Shift ? NativeMethods.ModShift : 0) | (this.Windows ? NativeMethods.ModWin : 0);

            // Register the hotkey
            if (NativeMethods.RegisterHotKey(windowControl.Handle, this._id, modifiers, _keyCode) == 0)
            {
                // Is the error that the hotkey is registered?
                if (Marshal.GetLastWin32Error() == NativeMethods.ErrorHotkeyAlreadyRegistered)
                {
                    return false;
                }
                throw new Win32Exception();
            }

            // Save the control reference and register state
            this._registered = true;
            this._windowControl = windowControl;

            // We successfully registered
            return true;
        }

        public void Unregister()
        {
            // Check that we have registered
            if (!this._registered)
            {
                throw new NotSupportedException("You cannot unregister a hotkey that is not registered");
            }

            // It's possible that the control itself has died: in that case, no need to unregister!
            if (!this._windowControl.IsDisposed)
            {
                // Clean up after ourselves
                if (NativeMethods.UnregisterHotKey(this._windowControl.Handle, this._id) == 0)
                {
                    throw new Win32Exception();
                }
            }

            // Clear the control reference and register state
            this._registered = false;
            this._windowControl = null;
        }

        private void Reregister()
        {
            // Only do something if the key is already registered
            if (!this._registered)
            {
                return;
            }

            // Save control reference
            var windowControl = this._windowControl;

            // Unregister and then reregister again
            this.Unregister();
            this.Register(windowControl);
        }

        private bool OnPressed()
        {
            // Fire the event if we can
            var handledEventArgs = new HandledEventArgs(false);
            this.Pressed?.Invoke(this, handledEventArgs);

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
            var keyName = Enum.GetName(typeof(Keys), this._keyCode);
            if (this._keyCode == Keys.D0 || this._keyCode == Keys.D1 || this._keyCode == Keys.D2 ||
                this._keyCode == Keys.D3 || this._keyCode == Keys.D4 || this._keyCode == Keys.D5 ||
                this._keyCode == Keys.D6 || this._keyCode == Keys.D7 || this._keyCode == Keys.D8 ||
                this._keyCode == Keys.D9)
            {
                // Strip the first character
                keyName = keyName?.Substring(1);
            }
            else
            {
                // Leave everything alone
            }

            // Build modifiers
            var modifiers = "";
            if (this._shift)
            {
                modifiers += "Shift+";
            }
            if (this._control)
            {
                modifiers += "Control+";
            }
            if (this._alt)
            {
                modifiers += "Alt+";
            }
            if (this._windows)
            {
                modifiers += "Windows+";
            }

            // Return result
            return modifiers + keyName;
        }
    }
}