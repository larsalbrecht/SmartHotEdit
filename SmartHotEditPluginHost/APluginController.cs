using System;
using System.Collections.Generic;
using NLog;
using SmartHotEditPluginHost.Helper;

namespace SmartHotEditPluginHost
{
    public abstract class APluginController
    {
        protected static Logger Logger = LogManager.GetCurrentClassLogger();
        // ReSharper disable once NotAccessedField.Local
        private IPluginController _pluginController;
        public IList<APlugin> DisabledPlugins = new List<APlugin>();
        public IList<APlugin> EnabledPlugins = new List<APlugin>();

        public IList<APlugin> LoadedPlugins = new List<APlugin>();

        protected APluginController(IPluginController pluginController)
        {
            this._pluginController = pluginController;
        }

        public string Type { get; set; }

        protected abstract APlugin[] GetPlugins();

        public abstract bool IsEnabled();

        public bool IsFullyImplemented()
        {
            return this.Type != null;
        }

        public void PreLoadPlugins()
        {
            Logger.Debug("Load " + this.Type + " Plugins");
        }

        public abstract void LoadPlugins();

        public void PostLoadPlugins()
        {
            this.LoadedPlugins = this.GetPlugins();
            this.EnabledPlugins.Clear();
            this.DisabledPlugins.Clear();
            foreach (var plugin in this.LoadedPlugins)
            {
                plugin.Type = this.Type;
                Console.WriteLine("Property exists: " + plugin.GetPropertynameForEnablePlugin() + " | " +
                                  PropertyHelper.PropertiesHasKey(plugin.GetPropertynameForEnablePlugin()));
                if (!PropertyHelper.PropertiesHasKey(plugin.GetPropertynameForEnablePlugin()))
                {
                    PropertyHelper.CreateProperty(plugin.GetPropertynameForEnablePlugin(), true, typeof(bool));
                }
                plugin.Enabled = true;
                    // TODO fix this (make dynamic) (bool)Properties.Settings.Default[plugin.getPropertynameForEnablePlugin()];
                if (plugin.Enabled)
                {
                    this.EnabledPlugins.Add(plugin);
                }
                else
                {
                    this.DisabledPlugins.Add(plugin);
                }
            }
            Logger.Debug(this.Type + " Plugins found: " + this.LoadedPlugins.Count);
            Logger.Debug(this.Type + " Plugins enabled: " + this.EnabledPlugins.Count);
            Logger.Debug(this.Type + " Plugins disabled: " + this.DisabledPlugins.Count);
        }
    }
}