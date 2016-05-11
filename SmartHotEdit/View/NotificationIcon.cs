using System;
using System.Drawing;
using System.Windows.Forms;
using SmartHotEdit.Controller;

namespace SmartHotEdit.View
{
    public sealed class NotificationIcon
    {
        private MainController mainController;
        private NotifyIcon notifyIcon;
        private ContextMenu notificationMenu;
        private SettingsView settingsView;

        #region Initialize icon and menu
        public NotificationIcon(MainController mainController)
        {
            this.mainController = mainController;

            notifyIcon = new NotifyIcon();
            notificationMenu = new ContextMenu(InitializeMenu());

            notifyIcon.DoubleClick += IconDoubleClick;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationIcon));
            notifyIcon.Icon = (Icon)resources.GetObject("$this.Icon");
            notifyIcon.ContextMenu = notificationMenu;
        }

        private MenuItem[] InitializeMenu()
        {
            MenuItem[] menu = new MenuItem[] {
                new MenuItem("Settings", menuSettingsClick),
                new MenuItem("About", menuAboutClick),
                new MenuItem("Exit", menuExitClick)
            };
            return menu;
        }
        #endregion

        #region Event Handlers
        private void menuSettingsClick(object sender, EventArgs e)
        {
            if(settingsView == null)
            {
                settingsView = new SettingsView(this.mainController);
            }
            if(settingsView.Visible == false)
            {
                settingsView.ShowDialog();
            }
        }

        private void menuAboutClick(object sender, EventArgs e)
        {
            MessageBox.Show("(c) 2016 Lars Albrecht");
        }

        private void menuExitClick(object sender, EventArgs e)
        {
            mainController.onUserWillClose();
        }

        private void IconDoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show("The icon was double clicked");
        }
        #endregion

        public NotifyIcon getNotifyIcon()
        {
            return this.notifyIcon;
        }

    }
}
