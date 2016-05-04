using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using SmartHotEditPluginHost;

namespace SmartHotEdit.Controller.Plugin
{
    class DefaultPluginController
    {

        [ImportMany(typeof(APlugin))]
        private APlugin[] plugins = null;

        public DefaultPluginController()
        {
            this.loadPlugins();
            System.Diagnostics.Debug.WriteLine("Default Plugins found: " + this.plugins.Length);
        }

        private void loadPlugins()
        {
            var pluginCatalog = new DirectoryCatalog(".");
            var container = new CompositionContainer(pluginCatalog);
            container.ComposeParts(this);
        }

        public APlugin[] getPlugins()
        {
            return this.plugins;
        }

    }
}
