using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHotEdit.Helper;
using System.Windows.Forms;
using SmartHotEdit.View;

namespace SmartHotEdit.Controller
{
    class HotKeyController
    {

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
            System.Diagnostics.Debug.WriteLine("Register HotKey");
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
                System.Diagnostics.Debug.WriteLine("Whoops, looks like attempts to register will fail or throw an exception, show an error/visual user feedback");
                this.onHotKeyCouldNotRegistered();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Yeha, registered");
                this.hk.Register(hkc);
                this.onHotKeyIsRegistered();
            }
        }

        private void onHotKeyPressed()
        {
            System.Diagnostics.Debug.WriteLine("Windows+Alt+Y pressed!");
            if (ef == null || !ef.Visible)
            {
                ef = new EditForm(this.mainController.getPluginController());
                ef.Show();
            }
        }

        private void onHotKeyCouldNotRegistered()
        {
            this.mainController.getNotificationController().createBalloonTip(ToolTipIcon.Warning, "Hot Key", "Hot key could not be set!", 20000);
        }

        private void onHotKeyIsRegistered()
        {
            this.mainController.getNotificationController().createBalloonTip(ToolTipIcon.Info, "Hot Key", "Hot key registered", 10000);
        }

        public void onClose()
        {
            this.hk.Unregister();
        }
    }
}
