using NLog;
using SmartHotEditPluginHost;
using System;
using SmartHotEdit.Controller.Plugin;
using System.Collections.Generic;
using SmartHotEdit.Abstracts;
using System.Linq;

namespace SmartHotEdit.Controller
{
    // TODO let the user disable different plugin loader
    public class PluginController
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public List<APlugin> LoadedPlugins = new List<APlugin>();
        public List<APlugin> EnabledPlugins = new List<APlugin>();
        public List<APlugin> DisabledPlugins = new List<APlugin>();

        private IList<APluginController> pluginControllerList;

        public PluginController()
        {
            logger.Trace("Construct PluginController");

            this.pluginControllerList = new List<APluginController>();
            /*this.pluginControllerList.Add(new DefaultPluginController(this));
            this.pluginControllerList.Add(new LuaPluginController(this));*/
            this.pluginControllerList.Add(new PythonPluginController(this));
            this.pluginControllerList.Add(new JavascriptPluginController(this));

            this.loadPlugins();
        }

        public void loadPlugins()
        {
            this.LoadedPlugins.Clear();
            this.EnabledPlugins.Clear();
            this.DisabledPlugins.Clear();
            logger.Trace("Get Plugins from *PluginController");
            logger.Trace("Use plugins: " + Properties.Settings.Default.EnablePlugins);
            if (Properties.Settings.Default.EnablePlugins)
            {
                foreach(APluginController concretePluginControlelr in this.pluginControllerList)
                {
                    if (concretePluginControlelr.isFullyImplemented())
                    {
                        logger.Trace(concretePluginControlelr.Type + " is fully implemented");
                        if (concretePluginControlelr.isEnabled())
                        {
                            logger.Trace(concretePluginControlelr.Type + " is enabled");
                            concretePluginControlelr.preLoadPlugins();
                            concretePluginControlelr.loadPlugins();
                            concretePluginControlelr.postLoadPlugins();

                            this.LoadedPlugins.AddRange(concretePluginControlelr.LoadedPlugins);
                            this.EnabledPlugins.AddRange(concretePluginControlelr.EnabledPlugins);
                            this.DisabledPlugins.AddRange(concretePluginControlelr.DisabledPlugins);
                        }
                        else
                        {
                            logger.Debug(concretePluginControlelr.Type + " is disabled");
                        }
                    } else
                    {
                        throw new NotImplementedException("The Controller for the " + concretePluginControlelr.Type + " Plugins is not fully implemented!");
                    }
                }
            }
            logger.Debug("Plugins found: " + this.LoadedPlugins.Count);
            logger.Debug("Plugins enabled: " + this.EnabledPlugins.Count);
            logger.Debug("Plugins disabled: " + this.DisabledPlugins.Count);
        }

        private APlugin[] arrayMerge(APlugin[] baseArray, APlugin[] arrayToMerge)
        {
            if(baseArray == null)
            {
                baseArray = new APlugin[0];
            }
            if(arrayToMerge == null)
            {
                arrayToMerge = new APlugin[0];
            }
            int originalLength = baseArray.Length;
            Array.Resize(ref baseArray, originalLength + arrayToMerge.Length);
            Array.Copy(arrayToMerge, 0, baseArray, originalLength, arrayToMerge.Length);

            return baseArray;
        }

        public IList<APluginController> getPluginControllerList()
        {
            return this.pluginControllerList;
        }

    }
}
