using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHotEdit.Handler;
using SmartHotEdit.Model;
using System.Windows.Forms;
using SmartHotEdit.View;
using NLog;

namespace SmartHotEdit.Controller
{
    public sealed class HotKeyController : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        HotkeyHandler _hk;
        private EditForm _ef;

        HotKeyDummyControl _hkc = new HotKeyDummyControl();

        readonly MainController _mainController;

        public HotKeyController(MainController mainController)
        {
            this._mainController = mainController;
            Init();
        }

        private void Init()
        {
            Logger.Trace("Register HotKey");
            HotKey defaultHotKey = new HotKey(Keys.Y, false, true, false, true);
            var hotKeyDefault = true;

            HotKey hotkeyToRegister;
            // Register default
            if (Properties.Settings.Default.HotKey != null)
            {
                hotkeyToRegister = Properties.Settings.Default.HotKey;
                hotKeyDefault = false;
            }
            else
            {
                hotkeyToRegister = defaultHotKey;
            }
            if (!this.RegisterCustomHotKey(hotkeyToRegister))
            {
                if (!hotKeyDefault)
                {
                    Logger.Warn("Try to register default hotkey");
                    if (this.RegisterCustomHotKey(hotkeyToRegister))
                    {
                        this._mainController.NotificationController.CreateBalloonTip(ToolTipIcon.Warning, "Hot Key", "You tried to register a custom hotkey, but it fails. We registered the default again.", 1000);
                    }
                    else
                    {
                        Logger.Fatal("Custom hotkey could not registered and default could not registered again");
                        this._mainController.NotificationController.CreateBalloonTip(ToolTipIcon.Error, "Hot Key", "You tried to register a custom hotkey, but it fails. We tried to register the default again, but this also failed!", 1000);
                    }

                }
                // TODO check if this is the default, and if not, then register default.
                //logger.Warn("");
            }
        }

        private void OnHotKeyPressed()
        {
            Logger.Trace("HotKeyPressed");
            if (_ef == null || _ef.IsDisposed)
            {
                Logger.Trace("Create EditForm, it is null (" + _ef == null + " ) or disposed (" + (_ef != null && _ef.IsDisposed) + ")");
                this._mainController.PluginController.LoadPlugins();
                _ef = new EditForm(this._mainController);
            }
            if (!_ef.Visible)
            {
                _ef.Show();
            }
            _ef.BringToFront();
        }

        private void OnHotKeyCouldNotRegistered(Boolean alreadyRegistered = false)
        {
            Logger.Warn("HotKey could not be registered" + (alreadyRegistered ? " Already registered" : ""));
            this._mainController.NotificationController.CreateBalloonTip(ToolTipIcon.Warning, "Hot Key", "Hot key could not be set!" + (alreadyRegistered ? " Already registered!" : ""), 1000);
        }

        private void onHotKeyIsRegistered()
        {
            Logger.Debug("HotKey registered");
        }

        public void OnClose()
        {
            this._hk.Unregister();
        }

        private bool RegisterCustomHotKey(HotKey hotkeyToRegister)
        {
            return RegisterCustomHotKey(hotkeyToRegister.Hotkey, hotkeyToRegister.IsShift, hotkeyToRegister.IsControl, hotkeyToRegister.IsAlt, hotkeyToRegister.IsWin);
        }

        public bool RegisterCustomHotKey(Keys keyCode, bool shiftPressed, bool controlPressed, bool altPressed, bool winPressed)
        {
            Logger.Debug("Try to register new hotkey");
            bool isRegistered = false;
            if (this._hk != null && this._hk.Registered)
            {
                Logger.Debug("Old hotkey found, unregister first");
                this._hk.Unregister();
            }

            this._hk = new HotkeyHandler();

            this._hk.Pressed += delegate {
                OnHotKeyPressed();
            };

            this._hk.KeyCode = keyCode;
            this._hk.Shift = shiftPressed;
            this._hk.Control = controlPressed;
            this._hk.Alt = altPressed;
            this._hk.Windows = winPressed;

            if (!this._hk.GetCanRegister(_hkc))
            {
                this.OnHotKeyCouldNotRegistered();
            }
            else if (this._hk.Register(_hkc))
                {
                this.onHotKeyIsRegistered();
                isRegistered = true;
            } else
            {
                this.OnHotKeyCouldNotRegistered(true);
            }

            return isRegistered;
        }

        public void Dispose()
        {
            this._hk?.Unregister();
            this._hkc?.Dispose();
            this._ef?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
