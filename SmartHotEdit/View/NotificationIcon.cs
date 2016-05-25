using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SmartHotEdit.Controller;
using SmartHotEdit.Properties;
using SmartHotEdit.View.Editor;

namespace SmartHotEdit.View
{
    public sealed class NotificationIcon : IDisposable
    {
        private readonly MainController _mainController;
        private readonly ContextMenu _notificationMenu;
        private readonly NotifyIcon _notifyIcon;
        private ScriptPluginEditor _scriptPluginEditor;
        private SettingsView _settingsView;

        public void Dispose()
        {
            this._settingsView?.Dispose();
            this._scriptPluginEditor?.Dispose();
            this._notifyIcon?.Dispose();
            this._notificationMenu?.Dispose();
        }

        public NotifyIcon GetNotifyIcon()
        {
            return this._notifyIcon;
        }

        #region Initialize icon and menu

        public NotificationIcon(MainController mainController)
        {
            this._mainController = mainController;

            _notifyIcon = new NotifyIcon();
            _notificationMenu = new ContextMenu(InitializeMenu());

            _notifyIcon.DoubleClick += IconDoubleClick;
            var resources = new ComponentResourceManager(typeof(NotificationIcon));
            _notifyIcon.Icon = (Icon) resources.GetObject("$this.Icon");
            _notifyIcon.ContextMenu = _notificationMenu;
        }

        private MenuItem[] InitializeMenu()
        {
            MenuItem[] menu =
            {
                new MenuItem("Settings", MenuSettingsClick),
                new MenuItem("Script Plugin Editor", MenuOpenScriptPluginEditorClick),
                new MenuItem("-"),
                new MenuItem("About", MenuAboutClick),
                new MenuItem("Exit", MenuExitClick)
            };
            return menu;
        }

        #endregion

        #region Event Handlers

        private void MenuOpenScriptPluginEditorClick(object sender, EventArgs e)
        {
            this._scriptPluginEditor = new ScriptPluginEditor(this._mainController);
            this._scriptPluginEditor.Show();
        }

        private void MenuSettingsClick(object sender, EventArgs e)
        {
            if (_settingsView == null)
            {
                _settingsView = new SettingsView(this._mainController);
            }
            if (_settingsView.Visible == false)
            {
                _settingsView.ShowDialog();
            }
        }

        private void MenuAboutClick(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.NotificationIcon_MenuAboutClick__c__2016_Lars_Albrecht);
        }

        private void MenuExitClick(object sender, EventArgs e)
        {
            _mainController.OnUserWillClose();
        }

        private void IconDoubleClick(object sender, EventArgs e)
        {
            this.MenuSettingsClick(sender, e);
        }

        #endregion
    }
}