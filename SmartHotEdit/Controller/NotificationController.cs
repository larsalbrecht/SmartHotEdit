using System;
using System.Windows.Forms;
using NLog;
using SmartHotEdit.View;

namespace SmartHotEdit.Controller
{
    public sealed class NotificationController : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly MainController _mainController;

        private readonly NotificationIcon _notificationIcon;

        public NotificationController(MainController mainController)
        {
            Logger.Trace("Init NotificationController now");
            this._mainController = mainController;
            Logger.Trace("Create TrayIcon");
            this._notificationIcon = new NotificationIcon(this._mainController);
            this._notificationIcon.GetNotifyIcon().Visible = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void CreateBalloonTip(ToolTipIcon toolTipIcon, string title, string text, int duration)
        {
            Logger.Trace("Create BalloonTip");
            var notifyIcon = this._notificationIcon.GetNotifyIcon();
            notifyIcon.BalloonTipIcon = toolTipIcon;
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = text;
            notifyIcon.ShowBalloonTip(duration);
        }

        public void OnClose()
        {
            Logger.Trace("Dispose NotificationIcon");
            this._notificationIcon.GetNotifyIcon().Dispose();
        }

        ~NotificationController()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (disposing != true) return;
            this._notificationIcon?.Dispose();
        }
    }
}