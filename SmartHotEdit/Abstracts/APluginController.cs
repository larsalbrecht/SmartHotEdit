using NLog;
using SmartHotEdit.Controller;
using SmartHotEdit.Helper;
using SmartHotEditPluginHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHotEdit.Abstracts
{
    abstract public class APluginController
    {

        protected static Logger logger = LogManager.GetCurrentClassLogger();
        private PluginController pluginController;
        public string Type { get; set; }

        public IList<APlugin> LoadedPlugins = new List<APlugin>();
        public IList<APlugin> EnabledPlugins = new List<APlugin>();
        public IList<APlugin> DisabledPlugins = new List<APlugin>();

        protected abstract APlugin[] getPlugins();

        public abstract bool isEnabled();

        public APluginController(PluginController pluginController)
        {
            this.pluginController = pluginController;
        }

        public bool isFullyImplemented()
        {
            return this.Type != null;
        }

        public void preLoadPlugins()
        {
            logger.Debug("Load " + this.Type + " Plugins");
        }

        public abstract void loadPlugins();

        public void postLoadPlugins()
        {
            this.LoadedPlugins = this.getPlugins();
            this.EnabledPlugins.Clear();
            this.DisabledPlugins.Clear();
            foreach (APlugin plugin in this.LoadedPlugins)
            {
                plugin.Type = this.Type;
                Console.WriteLine("Property exists: " + plugin.getPropertynameForEnablePlugin() + " | " + PropertyHelper.PropertiesHasKey(plugin.getPropertynameForEnablePlugin()));
                if (!PropertyHelper.PropertiesHasKey(plugin.getPropertynameForEnablePlugin()))
                {
                    PropertyHelper.CreateProperty(plugin.getPropertynameForEnablePlugin(), true, typeof(bool));
                }
                plugin.Enabled = (bool)Properties.Settings.Default[plugin.getPropertynameForEnablePlugin()];
                if (plugin.Enabled)
                {   
                    this.EnabledPlugins.Add(plugin);
                } else
                {
                    this.DisabledPlugins.Add(plugin);
                }
            }
            logger.Debug(this.Type + " Plugins found: " + this.LoadedPlugins.Count);
            logger.Debug(this.Type + " Plugins enabled: " + this.EnabledPlugins.Count);
            logger.Debug(this.Type + " Plugins disabled: " + this.DisabledPlugins.Count);
        }
    }

}
