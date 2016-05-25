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

        private bool _disposed;

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
            // ReSharper disable once UnusedVariable
            using (var mtx = new Mutex(true, "SmartHotEdit", out isFirstInstance))
            {
                if (isFirstInstance)
                {
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

        public NotificationController NotificationController { get; }
        public HotKeyController HotKeyController { get; }
        public PluginController PluginController { get; }

        public void Dispose()
        {
            this.HotKeyController?.Dispose();
            this.NotificationController?.Dispose();
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
            }

            _disposed = true;
        }

        ~MainController() // the finalizer
        {
            Dispose(false);
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