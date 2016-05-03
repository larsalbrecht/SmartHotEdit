using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHotEdit.View;
using System.Windows.Forms;

namespace SmartHotEdit.Controller
{
    public class NotificationController
    {

        NotificationIcon notificationIcon;
        MainController mainController;

        public NotificationController(MainController mainController)
        {
            this.mainController = mainController;
            init();
        }

        private void init()
        {
            System.Diagnostics.Debug.WriteLine("Create Icon");
            this.notificationIcon = new NotificationIcon(this.mainController);
            this.notificationIcon.getNotifyIcon().Visible = true;
        }

        public void createBalloonTip(ToolTipIcon toolTipIcon, String title, String text, int duration)
        {
            var notifyIcon = this.notificationIcon.getNotifyIcon();
            notifyIcon.BalloonTipIcon = toolTipIcon;
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = text;
            notifyIcon.ShowBalloonTip(duration);
        }

        public void onClose()
        {
            this.notificationIcon.getNotifyIcon().Dispose();
        }
        
    }
}
