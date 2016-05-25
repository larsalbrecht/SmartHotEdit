using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using NLog;
using SmartHotEdit.Controller.Plugin;
using SmartHotEdit.Properties;
using SmartHotEditPluginHost;

namespace SmartHotEdit.Controller
{
    // TODO let the user disable different plugin loader
    public class PluginController : IPluginController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IList<APluginController> _pluginControllerList;

        [ImportMany(typeof(AScriptPluginController))]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private AScriptPluginController[] _scriptPluginControllerList = null;

        private IList<FileSystemWatcher> _watcherList;
        private readonly List<APlugin> _disabledPlugins = new List<APlugin>();
        public readonly List<APlugin> EnabledPlugins = new List<APlugin>();
        private readonly List<APlugin> _loadedPlugins = new List<APlugin>();

        public PluginController()
        {
            Logger.Trace("Construct PluginController");
            Logger.Trace("Load ScriptPluginController using CompositionContainer");
            var pluginCatalog = new DirectoryCatalog(".");
            var container = new CompositionContainer(pluginCatalog);
            this.PluginControllerList = this;
            container.ComposeParts(this);
            container.Dispose();
            Logger.Trace("Load ScriptPluginController finished");

            this._pluginControllerList = new List<APluginController> {new DefaultPluginController(this)};

            foreach (var scriptPluginController in this._scriptPluginControllerList)
            {
                this._pluginControllerList.Add(scriptPluginController);
            }

            this.LoadPlugins();
        }

        [Export("IPluginController")]
        private IPluginController PluginControllerList { get; set; }

        public void LoadPlugins()
        {
            this._loadedPlugins.Clear();
            this.EnabledPlugins.Clear();
            this._disabledPlugins.Clear();
            Logger.Trace("Get Plugins from *PluginController");
            Logger.Trace("Use plugins: " + Settings.Default.EnablePlugins);
            if (Settings.Default.EnablePlugins)
            {
                foreach (var concretePluginController in this._pluginControllerList)
                {
                    if ((concretePluginController is AScriptPluginController &&
                         ((AScriptPluginController) concretePluginController).IsFullyImplemented) ||
                        (!(concretePluginController is AScriptPluginController) &&
                         concretePluginController.IsFullyImplemented))
                    {
                        Logger.Trace(concretePluginController.Type + " is fully implemented");
                        concretePluginController.Init();
                        if (concretePluginController.Enabled)
                        {
                            Logger.Trace(concretePluginController.Type + " is enabled");
                            concretePluginController.PreLoadPlugins();
                            concretePluginController.LoadPlugins();
                            concretePluginController.PostLoadPlugins();

                            if (concretePluginController is AScriptPluginController)
                            {
                                if (this._watcherList == null)
                                {
                                    this._watcherList = new List<FileSystemWatcher>();
                                }
                                // TODO temporary disabled | this.watchScriptPluginController((AScriptPluginController)concretePluginController);
                            }

                            this._loadedPlugins.AddRange(concretePluginController.LoadedPlugins);
                            this.EnabledPlugins.AddRange(concretePluginController.EnabledPlugins);
                            this._disabledPlugins.AddRange(concretePluginController.DisabledPlugins);
                        }
                        else
                        {
                            Logger.Debug(concretePluginController.Type + " is disabled");
                        }
                    }
                    else
                    {
                        throw new NotImplementedException("The Controller is not fully implemented: " +
                                                          concretePluginController.Type);
                    }
                }
            }
            Logger.Debug("Plugins found: " + this._loadedPlugins.Count);
            Logger.Debug("Plugins enabled: " + this.EnabledPlugins.Count);
            Logger.Debug("Plugins disabled: " + this._disabledPlugins.Count);
        }

        // ReSharper disable once UnusedMember.Local
        private void WatchScriptPluginController(AScriptPluginController scriptPluginController)
        {
            var path = Path.GetFullPath(scriptPluginController.TypePluginPath);
            var fileFilter = "*." + scriptPluginController.TypeFileExt;

            if (Directory.Exists(path))
            {
                // TODO implement http://stackoverflow.com/questions/1406808/wait-for-file-to-be-freed-by-process/1406853#1406853
                var watcher = new FileSystemWatcher
                {
                    Path = path,
                    NotifyFilter = NotifyFilters.LastWrite,
                    Filter = fileFilter,
                    EnableRaisingEvents = true,
                    IncludeSubdirectories = false
                };
                watcher.Changed += OnPluginDirectoryChanged;

                this._watcherList.Add(watcher);
            }
            else
            {
                Logger.Info("ScriptPluginController can not be watched (pluginpath did not exists): " + path + "; " +
                            scriptPluginController.Type);
            }
        }

        private static void OnPluginDirectoryChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine(@"Type: " + e.ChangeType);
            Console.WriteLine(@"Name: " + e.Name);
            Console.WriteLine(@"File changed: " + e.FullPath);
        }

        // ReSharper disable once UnusedMember.Local
        private APlugin[] ArrayMerge(APlugin[] baseArray, APlugin[] arrayToMerge)
        {
            if (baseArray == null)
            {
                baseArray = new APlugin[0];
            }
            if (arrayToMerge == null)
            {
                arrayToMerge = new APlugin[0];
            }
            var originalLength = baseArray.Length;
            Array.Resize(ref baseArray, originalLength + arrayToMerge.Length);
            Array.Copy(arrayToMerge, 0, baseArray, originalLength, arrayToMerge.Length);

            return baseArray;
        }

        public IEnumerable<APluginController> GetPluginControllerList(Type pluginControllerType = null)
        {
            return pluginControllerType == null
                ? this._pluginControllerList
                : this._pluginControllerList.Where(
                    pluginController =>
                        pluginController.GetType().IsSubclassOf(pluginControllerType) ||
                        pluginController.GetType() == pluginControllerType
                    ).ToList();
        }

    }
}