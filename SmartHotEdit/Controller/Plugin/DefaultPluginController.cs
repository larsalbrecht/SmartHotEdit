using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using SmartHotEditPluginHost;
using NLog;
using SmartHotEdit.Abstracts;

namespace SmartHotEdit.Controller.Plugin
{
    class DefaultPluginController : APluginController
    {

        [ImportMany(typeof(APlugin))]
        private APlugin[] plugins = null;

        public DefaultPluginController(PluginController pluginController) : base(pluginController)
        {
            logger.Trace("Construct DefaultPluginController");
            this.Type = "Default";
        }

        public override void loadPlugins()
        {
            logger.Trace("Load DefaultPlugins using CompositionContainer");
            var pluginCatalog = new DirectoryCatalog(".");
            var container = new CompositionContainer(pluginCatalog);
            container.ComposeParts(this);
            logger.Trace("Load DefaultPlugins finished");
        }

        protected override APlugin[] getPlugins()
        {
            return this.plugins;
        }

        public override bool isEnabled()
        {
            return Properties.Settings.Default.EnableDefaultPlugins;
        }
    }
}
