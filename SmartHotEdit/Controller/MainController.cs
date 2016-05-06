using System.Threading;
using System.Windows.Forms;
using SmartHotEdit.Controller.Plugin;
using NLog;
using NLog.Config;

namespace SmartHotEdit.Controller
{
    public class MainController
    {
        private NotificationController notificationController;
        private HotKeyController hotKeyController;
        private PluginController pluginController;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public MainController()
        {
            if(Properties.Settings.Default.EnableLogging == false)
            {
                System.Diagnostics.Debug.WriteLine("Disable logger");
                LogManager.DisableLogging();
            } else
            {
                LogManager.EnableLogging();
            }
            logger.Info("Program started");
            bool isFirstInstance;
            // Please use a unique name for the mutex to prevent conflicts with other programs
            using (Mutex mtx = new Mutex(true, "SmartHotEdit", out isFirstInstance))
            {
                if (isFirstInstance)
                {
                    logger.Trace("Is first instance");
                    this.initFirstInstance();
                }
                else
                {
                    logger.Trace("Is not the first instance");
                    this.onApplicationInstanceAlreadyStarted();
                }
            } // releases the Mutex
        }

        private void initFirstInstance()
        {
            logger.Trace("Init first instance");

            logger.Trace("Init PluginController now");
            this.pluginController = new PluginController();
            logger.Trace("Init NotificationController now");
            this.notificationController = new NotificationController(this);
            logger.Trace("Init HotKeyController now");
            this.hotKeyController = new HotKeyController(this);

            logger.Trace("Controller initialized, Application.Run()");
            Application.Run();
            logger.Trace("Application.Run finished");
            this.onClose();
        }

        private void onApplicationInstanceAlreadyStarted()
        {
            this.notificationController.createBalloonTip(ToolTipIcon.Info, "Smart Hot Edit", "Smart Hot Edit already running", 10000);
        }

        public void onUserWillClose()
        {
            logger.Trace("onUserWillClose");
            hotKeyController.onClose();
            Application.Exit();
        }

        private void onClose()
        {
            Properties.Settings.Default.Save();
            logger.Trace("onClose");
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

        public HotKeyController getHotKeyController()
        {
            return this.hotKeyController;
        }

    }
}
