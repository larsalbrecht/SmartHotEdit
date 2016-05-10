using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHotEdit.Helper;
using SmartHotEdit.Model;
using System.Windows.Forms;
using SmartHotEdit.View;
using NLog;

namespace SmartHotEdit.Controller
{
    public class HotKeyController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        HotkeyHandler hk;
        EditForm ef;

        HotKeyDummyControl hkc = new HotKeyDummyControl();

        MainController mainController;

        public HotKeyController(MainController mainController)
        {
            this.mainController = mainController;
            init();
        }

        private void init()
        {
            logger.Trace("Register HotKey");
            HotKey defaultHotKey = new HotKey(Keys.Y, false, true, false, true);
            bool hotKeyDefault = true;

            HotKey hotkeyToRegister = null;
            // Register default
            if(Properties.Settings.Default.HotKey != null)
            {
                hotkeyToRegister = Properties.Settings.Default.HotKey;
                hotKeyDefault = false;
            } else
            {
                hotkeyToRegister = defaultHotKey;
            }
            if (!this.registerCustomHotKey(hotkeyToRegister))
            {
                if (!hotKeyDefault)
                {
                    logger.Warn("Try to register default hotkey");
                    if (this.registerCustomHotKey(hotkeyToRegister))
                    {
                        this.mainController.getNotificationController().createBalloonTip(ToolTipIcon.Warning, "Hot Key", "You tried to register a custom hotkey, but it fails. We registered the default again.", 1000);
                    } else
                    {
                        logger.Fatal("Custom hotkey could not registered and default could not registered again");
                        this.mainController.getNotificationController().createBalloonTip(ToolTipIcon.Error, "Hot Key", "You tried to register a custom hotkey, but it fails. We tried to register the default again, but this also failed!", 1000);
                    }
                    
                }
                // TODO check if this is the default, and if not, then register default.
                //logger.Warn("");
            }
        }

        private void onHotKeyPressed()
        {
            logger.Trace("HotKeyPressed");
            if (ef == null || ef.IsDisposed)
            {
                logger.Trace("Create EditForm, it is null (" + ef == null + " ) or disposed (" + (ef != null && ef.IsDisposed) + ")");
                ef = new EditForm(this.mainController.getPluginController());
            }
            if (!ef.Visible)
            {
                ef.Show();
            }
            ef.BringToFront();
        }

        private void onHotKeyCouldNotRegistered(Boolean alreadyRegistered = false)
        {
            logger.Warn("HotKey could not be registered" + (alreadyRegistered ? " Already registered" : ""));
            this.mainController.getNotificationController().createBalloonTip(ToolTipIcon.Warning, "Hot Key", "Hot key could not be set!" + (alreadyRegistered ? " Already registered!" : ""), 1000);
        }

        private void onHotKeyIsRegistered()
        {
            logger.Debug("HotKey registered");
        }

        public void onClose()
        {
            this.hk.Unregister();
        }

        private Boolean registerCustomHotKey(HotKey hotkeyToRegister)
        {
            return registerCustomHotKey(hotkeyToRegister.hotkey, hotkeyToRegister.isShift, hotkeyToRegister.isControl, hotkeyToRegister.isAlt, hotkeyToRegister.isWin);
        }

        public Boolean registerCustomHotKey(Keys keyCode, Boolean shiftPressed, Boolean controlPressed, Boolean altPressed, Boolean winPressed)
        {
            logger.Debug("Try to register new hotkey");
            Boolean isRegistered = false;
            if (this.hk != null && this.hk.Registered == true)
            {
                logger.Debug("Old hotkey found, unregister first");
                this.hk.Unregister();
            }

            this.hk = new HotkeyHandler();

            this.hk.Pressed += delegate {
                onHotKeyPressed();
            };

            this.hk.KeyCode = keyCode;
            this.hk.Shift = shiftPressed;
            this.hk.Control = controlPressed;
            this.hk.Alt = altPressed;
            this.hk.Windows = winPressed;

            if (!this.hk.GetCanRegister(hkc))
            {
                this.onHotKeyCouldNotRegistered();
            }
            else if (this.hk.Register(hkc))
                {
                this.onHotKeyIsRegistered();
                isRegistered = true;
            } else
            {
                this.onHotKeyCouldNotRegistered(true);
            }

            return isRegistered;
        }
    }
}
