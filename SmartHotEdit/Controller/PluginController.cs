using SmartHotEditPluginHost;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace SmartHotEdit.Controller
{
    public class PluginController
    {

        [ImportMany(typeof(APlugin))]
        private APlugin[] plugins = null;

        public PluginController()
        {
            this.loadPlugins();
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
