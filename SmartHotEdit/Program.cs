using System;
using System.Windows.Forms;
using NLog.Config;
using SmartHotEdit.Controller;
using SmartHotEdit.NLogger.LayoutRenderer;

namespace SmartHotEdit
{
    internal static class Program
    {
        public static string AppName = "SmartHotEdit";

        /// <summary>
        ///     Main entrypoint to the application
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // NLog
            ConfigurationItemFactory.Default.LayoutRenderers
                .RegisterDefinition("buildConfiguration", typeof(NLogBuildTypeLayoutRenderer));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ReSharper disable once ObjectCreationAsStatement
            new MainController();
        }
    }
}