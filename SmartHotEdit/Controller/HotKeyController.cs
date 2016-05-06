using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHotEdit.Helper;
using System.Windows.Forms;
using SmartHotEdit.View;
using NLog;

namespace SmartHotEdit.Controller
{
    class HotKeyController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        Hotkey hk;
        EditForm ef;

        MainController mainController;

        public HotKeyController(MainController mainController)
        {
            this.mainController = mainController;
            init();
        }

        private void init()
        {
            logger.Trace("Register HotKey");
            this.hk = new Hotkey();

            this.hk.KeyCode = Keys.Y;
            this.hk.Control = true;
            this.hk.Windows = true;
            this.hk.Pressed += delegate {
                onHotKeyPressed();
            };

            HotKeyControl hkc = new HotKeyControl();
            if (!this.hk.GetCanRegister(hkc))
            {
                logger.Warn("HotKey could not be registered");
                this.onHotKeyCouldNotRegistered();
            }
            else
            {
                logger.Debug("HotKey registered");
                this.hk.Register(hkc);
                this.onHotKeyIsRegistered();
            }
        }

        private void onHotKeyPressed()
        {
            logger.Trace("HotKeyPressed");
            if (ef == null)
            {
                logger.Trace("Create EditForm, it is null");
                ef = new EditForm(this.mainController.getPluginController());
            }
            if (!ef.Visible)
            {
                ef.Show();
            }
        }

        private void onHotKeyCouldNotRegistered()
        {
            this.mainController.getNotificationController().createBalloonTip(ToolTipIcon.Warning, "Hot Key", "Hot key could not be set!", 5000);
        }

        private void onHotKeyIsRegistered()
        {
            this.mainController.getNotificationController().createBalloonTip(ToolTipIcon.Info, "Hot Key", "Hot key registered", 1000);
        }

        public void onClose()
        {
            this.hk.Unregister();
        }
    }
}
