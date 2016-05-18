using System;
using SmartHotEdit.View;
using System.Windows.Forms;
using NLog;

namespace SmartHotEdit.Controller
{
    public class NotificationController : IDisposable
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        NotificationIcon notificationIcon;
        MainController mainController;

        public NotificationController(MainController mainController)
        {
            logger.Trace("Init NotificationController now");
            this.mainController = mainController;
            init();
        }

        private void init()
        {
            logger.Trace("Create TrayIcon");
            this.notificationIcon = new NotificationIcon(this.mainController);
            this.notificationIcon.getNotifyIcon().Visible = true;
        }

        public void createBalloonTip(ToolTipIcon toolTipIcon, String title, String text, int duration)
        {
            logger.Trace("Create BalloonTip");
            var notifyIcon = this.notificationIcon.getNotifyIcon();
            notifyIcon.BalloonTipIcon = toolTipIcon;
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = text;
            notifyIcon.ShowBalloonTip(duration);
        }

        public void onClose()
        {
            logger.Trace("Dispose NotificationIcon");
            this.notificationIcon.getNotifyIcon().Dispose();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~NotificationController()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                if (this.notificationIcon != null)
                {
                    this.notificationIcon.Dispose();
                }
            }
        }
    }
}
