using System;
using System.Collections.Generic;
using NLog;
using SmartHotEditPluginHost.Helper;
using SmartHotEditPluginHost.Properties;

namespace SmartHotEditPluginHost
{
    public abstract class APluginController
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        // ReSharper disable once NotAccessedField.Local
        private IPluginController _pluginController;
        public readonly IList<APlugin> DisabledPlugins = new List<APlugin>();
        public readonly IList<APlugin> EnabledPlugins = new List<APlugin>();

        public IList<APlugin> LoadedPlugins = new List<APlugin>();

        protected APluginController(IPluginController pluginController)
        {
            this._pluginController = pluginController;
        }

        public string Type { get; protected set; }

        protected abstract APlugin[] GetPlugins();

        public bool Enabled
        {
            get
            {
                return (bool)Settings.Default[this.GetPropertynameForEnablePluginController()];
            }
            set
            {
                Logger.Trace($"Setting {this.GetPropertynameForEnablePluginController()} changed to {value}");
                Settings.Default[this.GetPropertynameForEnablePluginController()] = value;
                Settings.Default.Save();
            }
        }

        public void Init()
        {
            PropertyHelper.CreateProperty(this.GetPropertynameForEnablePluginController(), true, typeof(bool));
        }

        public bool IsFullyImplemented => this.Type != null;

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
                plugin.PluginController = this;
                plugin.Type = this.Type;
                plugin.Init();

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

        private string GetPropertynameForEnablePluginController()
        {
            return "Plugin" + this.Type + "Enabled";
        }
    }
}