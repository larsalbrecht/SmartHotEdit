using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using SmartHotEditPluginHost;

namespace SmartHotEdit.Controller.Plugin
{
    class DefaultPluginController : APluginController
    {

        [ImportMany(typeof(APlugin))]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private APlugin[] _plugins = null;

        public DefaultPluginController(IPluginController pluginController) : base(pluginController)
        {
            Logger.Trace("Construct DefaultPluginController");
            this.Type = "Default";
        }

        public override void LoadPlugins()
        {
            Logger.Trace("Load DefaultPlugins using CompositionContainer");
            var pluginCatalog = new DirectoryCatalog(".");
            var container = new CompositionContainer(pluginCatalog);
            container.ComposeParts(this);
            Logger.Trace("Load DefaultPlugins finished");
        }

        protected override APlugin[] GetPlugins()
        {
            return this._plugins;
        }

        public override bool IsEnabled()
        {
            return Properties.Settings.Default.EnableDefaultPlugins;
        }
    }
}
