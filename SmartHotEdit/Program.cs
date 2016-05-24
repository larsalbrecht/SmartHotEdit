using System;
using System.Windows.Forms;
using SmartHotEdit.Controller;
using NLog.Config;

namespace SmartHotEdit
{

    static class Program
    {

        public static string AppName = "SmartHotEdit";

        /// <summary>
        /// Main entrypoint to the application
        /// </summary>
        [STAThread]
        static void Main()
        {
            // NLog
            ConfigurationItemFactory.Default.LayoutRenderers
                            .RegisterDefinition("buildConfiguration", typeof(NLogger.LayoutRenderer.NLogBuildTypeLayoutRenderer));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ReSharper disable once ObjectCreationAsStatement
            new MainController();
        }
    }
}
