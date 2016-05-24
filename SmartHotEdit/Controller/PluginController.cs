using NLog;
using SmartHotEditPluginHost;
using System;
using SmartHotEdit.Controller.Plugin;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;

namespace SmartHotEdit.Controller
{
    // TODO let the user disable different plugin loader
    public class PluginController : IPluginController
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public List<APlugin> LoadedPlugins = new List<APlugin>();
        public List<APlugin> EnabledPlugins = new List<APlugin>();
        public List<APlugin> DisabledPlugins = new List<APlugin>();

        private IList<APluginController> pluginControllerList;

        [ImportMany(typeof(AScriptPluginController))]
        private AScriptPluginController[] scriptPluginControllerList = null;

        [Export("IPluginController")]
        private IPluginController pluginController { get; set; }

        private IList<FileSystemWatcher> watcherList;

        public PluginController()
        {
            logger.Trace("Construct PluginController");

            logger.Trace("Load ScriptPluginController using CompositionContainer");
            var pluginCatalog = new DirectoryCatalog(".");
            var container = new CompositionContainer(pluginCatalog);
            this.pluginController = this;
            container.ComposeParts(this);
            container.Dispose();
            logger.Trace("Load ScriptPluginController finished");

            this.pluginControllerList = new List<APluginController>();
            this.pluginControllerList.Add(new DefaultPluginController(this));

            foreach (AScriptPluginController scriptPluginController in this.scriptPluginControllerList)
            {
                this.pluginControllerList.Add(scriptPluginController);
            }

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
                foreach (APluginController concretePluginController in this.pluginControllerList)
                {
                    if ((concretePluginController is AScriptPluginController && ((AScriptPluginController)concretePluginController).isFullyImplemented()) || (!(concretePluginController is AScriptPluginController) && concretePluginController.isFullyImplemented()))
                    {
                        logger.Trace(concretePluginController.Type + " is fully implemented");
                        if (concretePluginController.isEnabled())
                        {
                            logger.Trace(concretePluginController.Type + " is enabled");
                            concretePluginController.preLoadPlugins();
                            concretePluginController.loadPlugins();
                            concretePluginController.postLoadPlugins();

                            if (concretePluginController is AScriptPluginController)
                            {
                                if (this.watcherList == null)
                                {
                                    this.watcherList = new List<FileSystemWatcher>();
                                }
                                // TODO temporary disabled | this.watchScriptPluginController((AScriptPluginController)concretePluginController);
                            }

                            this.LoadedPlugins.AddRange(concretePluginController.LoadedPlugins);
                            this.EnabledPlugins.AddRange(concretePluginController.EnabledPlugins);
                            this.DisabledPlugins.AddRange(concretePluginController.DisabledPlugins);
                        }
                        else
                        {
                            logger.Debug(concretePluginController.Type + " is disabled");
                        }
                    }
                    else
                    {
                        throw new NotImplementedException("The Controller is not fully implemented: " + concretePluginController.Type);
                    }
                }
            }
            logger.Debug("Plugins found: " + this.LoadedPlugins.Count);
            logger.Debug("Plugins enabled: " + this.EnabledPlugins.Count);
            logger.Debug("Plugins disabled: " + this.DisabledPlugins.Count);
        }

        private void watchScriptPluginController(AScriptPluginController scriptPluginController)
        {
            var path = Path.GetFullPath(scriptPluginController.TypePluginPath);
            var fileFilter = "*." + scriptPluginController.TypeFileExt;

            if (Directory.Exists(path))
            {
                // TODO implement http://stackoverflow.com/questions/1406808/wait-for-file-to-be-freed-by-process/1406853#1406853
                var watcher = new FileSystemWatcher();
                watcher.Path = path;
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Filter = fileFilter;
                watcher.EnableRaisingEvents = true;
                watcher.IncludeSubdirectories = false;
                watcher.Changed += new FileSystemEventHandler(OnPluginDirectoryChanged);

                this.watcherList.Add(watcher);
            }
            else
            {
                logger.Info("ScriptPluginController can not be watched (pluginpath did not exists): " + path + "; " + scriptPluginController.Type);
            }

        }
        
        private void OnPluginDirectoryChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("Type: " + e.ChangeType);
            Console.WriteLine("Name: " + e.Name);
            Console.WriteLine("File changed: " + e.FullPath);
        }

        private APlugin[] arrayMerge(APlugin[] baseArray, APlugin[] arrayToMerge)
        {
            if (baseArray == null)
            {
                baseArray = new APlugin[0];
            }
            if (arrayToMerge == null)
            {
                arrayToMerge = new APlugin[0];
            }
            int originalLength = baseArray.Length;
            Array.Resize(ref baseArray, originalLength + arrayToMerge.Length);
            Array.Copy(arrayToMerge, 0, baseArray, originalLength, arrayToMerge.Length);

            return baseArray;
        }

        public IList<APluginController> getPluginControllerList(Type pluginControllerType = null)
        {
            if (pluginControllerType == null)
            {
                return this.pluginControllerList;
            }

            var pluginControllerList = new List<APluginController>();
            foreach (APluginController pluginController in this.pluginControllerList)
            {
                if (pluginController.GetType().IsSubclassOf(pluginControllerType) ||
                    pluginController.GetType() == pluginControllerType)
                {
                    pluginControllerList.Add(pluginController);
                }
            }

            return pluginControllerList;
        }

    }
}
