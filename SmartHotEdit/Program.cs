using System;
using System.Windows.Forms;
using SmartHotEdit.Controller;
using NLog.Config;

namespace SmartHotEdit
{

    static class Program
    {

        public static String AppName = "SmartHotEdit";

        /// <summary>
        /// Main entrypoint to the application
        /// </summary>
        [STAThread]
        static void Main()
        {
            // NLog
            ConfigurationItemFactory.Default.LayoutRenderers
                            .RegisterDefinition("buildConfiguration", typeof(SmartHotEdit.NLogger.LayoutRenderer.NLogBuildTypeLayoutRenderer));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            new MainController();
        }
    }
}
