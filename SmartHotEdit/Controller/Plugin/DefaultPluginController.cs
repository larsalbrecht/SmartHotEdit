using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using SmartHotEditPluginHost;
using NLog;

namespace SmartHotEdit.Controller.Plugin
{
    class DefaultPluginController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [ImportMany(typeof(APlugin))]
        private APlugin[] plugins = null;

        private PluginController pluginController;

        public DefaultPluginController(PluginController pluginController)
        {
            logger.Trace("Construct DefaultPluginController");
            this.pluginController = pluginController;
            this.loadPlugins();
            logger.Debug("Default Plugins found: " + this.plugins.Length);
        }

        private void loadPlugins()
        {
            logger.Trace("Load DefaultPlugins using CompositionContainer");
            var pluginCatalog = new DirectoryCatalog(".");
            var container = new CompositionContainer(pluginCatalog);
            container.ComposeParts(this);
            logger.Trace("Load DefaultPlugins finished");
        }

        public APlugin[] getPlugins()
        {
            return this.plugins;
        }

    }
}
