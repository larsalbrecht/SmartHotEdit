using System;
using SmartHotEdit.View;
using System.Windows.Forms;
using NLog;

namespace SmartHotEdit.Controller
{
    public class NotificationController : IDisposable
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly NotificationIcon _notificationIcon;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly MainController _mainController;

        public NotificationController(MainController mainController)
        {
            Logger.Trace("Init NotificationController now");
            this._mainController = mainController;
            Logger.Trace("Create TrayIcon");
            this._notificationIcon = new NotificationIcon(this._mainController);
            this._notificationIcon.getNotifyIcon().Visible = true;
        }

        public void CreateBalloonTip(ToolTipIcon toolTipIcon, string title, string text, int duration)
        {
            Logger.Trace("Create BalloonTip");
            var notifyIcon = this._notificationIcon.getNotifyIcon();
            notifyIcon.BalloonTipIcon = toolTipIcon;
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = text;
            notifyIcon.ShowBalloonTip(duration);
        }

        public void OnClose()
        {
            Logger.Trace("Dispose NotificationIcon");
            this._notificationIcon.getNotifyIcon().Dispose();
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
            if (disposing != true) return;
            this._notificationIcon?.Dispose();
        }
    }
}
