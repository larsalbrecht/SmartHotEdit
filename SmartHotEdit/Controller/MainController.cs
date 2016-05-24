using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using NLog;
using SmartHotEdit.Properties;

namespace SmartHotEdit.Controller
{
    public sealed class MainController : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MainController()
        {
            if (Settings.Default.EnableLogging == false)
            {
                Debug.WriteLine("Disable logger");
                LogManager.DisableLogging();
            }
            else
            {
                LogManager.EnableLogging();
            }
            Logger.Info("Program started");
            bool isFirstInstance;
            // Please use a unique name for the mutex to prevent conflicts with other programs
            using (var mtx = new Mutex(true, "SmartHotEdit", out isFirstInstance))
            {
                if (isFirstInstance)
                {
                    Logger.Trace("Is first instance");

                    Logger.Trace("Init first instance");

                    Logger.Trace("Init PluginController now");
                    this.PluginController = new PluginController();
                    Logger.Trace("Init NotificationController now");
                    this.NotificationController = new NotificationController(this);
                    Logger.Trace("Init HotKeyController now");
                    this.HotKeyController = new HotKeyController(this);

                    Logger.Trace("Controller initialized, Application.Run()");
                    Application.Run();
                    Logger.Trace("Application.Run finished");
                    this.OnClose();
                }
                else
                {
                    Logger.Trace("Is not the first instance");
                    this.OnApplicationInstanceAlreadyStarted();
                }
            } // releases the Mutex
        }

        public NotificationController NotificationController { get; set; }
        public HotKeyController HotKeyController { get; set; }
        public PluginController PluginController { get; set; }


        public void Dispose()
        {
            this.HotKeyController?.Dispose();
            this.NotificationController?.Dispose();
            GC.SuppressFinalize(this);
        }

        private void OnApplicationInstanceAlreadyStarted()
        {
            this.NotificationController.CreateBalloonTip(ToolTipIcon.Info, "Smart Hot Edit",
                "Smart Hot Edit already running", 10000);
        }

        public void OnUserWillClose()
        {
            Logger.Trace("onUserWillClose");
            this.HotKeyController.OnClose();
            Application.Exit();
        }

        private void OnClose()
        {
            Settings.Default.Save();
            Logger.Trace("onClose");
            this.NotificationController.OnClose();
        }
    }
}