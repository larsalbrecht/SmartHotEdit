using System.Threading;
using System.Windows.Forms;
using SmartHotEdit.Controller.Plugin;

namespace SmartHotEdit.Controller
{
    // Add log (simple and advanced)
    public class MainController
    {
        private NotificationController notificationController;
        private HotKeyController hotKeyController;
        private PluginController pluginController;

        public MainController()
        {
            bool isFirstInstance;
            // Please use a unique name for the mutex to prevent conflicts with other programs
            using (Mutex mtx = new Mutex(true, "SmartHotEdit", out isFirstInstance))
            {
                if (isFirstInstance)
                {
                    this.initFirstInstance();
                }
                else
                {
                    this.onApplicationInstanceAlreadyStarted();
                }
            } // releases the Mutex
        }

        private void initFirstInstance()
        {
            //EditForm ef = null;
            System.Diagnostics.Debug.WriteLine("Started");

            this.pluginController = new PluginController();
            this.notificationController = new NotificationController(this);
            this.hotKeyController = new HotKeyController(this);

            Application.Run();
            this.onClose();
        }

        private void onApplicationInstanceAlreadyStarted()
        {
            this.notificationController.createBalloonTip(ToolTipIcon.Info, "Smart Hot Edit", "Smart Hot Edit already running", 10000);
        }

        public void onUserWillClose()
        {
            System.Diagnostics.Debug.WriteLine("onUserWillClose");
            hotKeyController.onClose();
            Application.Exit();
        }

        private void onClose()
        {
            System.Diagnostics.Debug.WriteLine("onClose");
            this.notificationController.onClose();
        }
        
        public NotificationController getNotificationController()
        {
            return this.notificationController;
        }

        public PluginController getPluginController()
        {
            return this.pluginController;
        }

    }
}
