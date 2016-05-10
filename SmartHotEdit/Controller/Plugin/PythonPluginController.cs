using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using SmartHotEditPluginHost;
using NLog;
using Microsoft.Scripting.Hosting;
using System.IO;
using SmartHotEdit.Model.Python;
using SmartHotEditPluginHost.Model;

namespace SmartHotEdit.Controller.Plugin
{

    class PythonPluginController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private List<APlugin> plugins = new List<APlugin>();
        private PluginController pluginController;

        private ScriptEngine engine;
        private ScriptScope scope;

        public PythonPluginController(PluginController pluginController)
        {
            logger.Trace("Construct JavascriptPluginController");
            this.pluginController = pluginController;
            this.loadPlugins();
            logger.Debug("Javascript Plugins found: " + this.plugins.Count());
        }

        private void loadPlugins()
        {
            this.engine = Python.CreateEngine();
            this.scope = engine.CreateScope();

            ICollection<string> paths = engine.GetSearchPaths();
            paths.Add(Path.GetFullPath(@"Python\Modules"));
            engine.SetSearchPaths(paths);

            String pluginPath = Path.GetFullPath(@"Python\Plugins\");
            String pluginSearchPattern = "*_plugin.py";
            logger.Trace("Find plugins in path: " + pluginPath + "; with search pattern: " + pluginSearchPattern);
            // find plugins
            string[] filePaths = Directory.GetFiles(pluginPath, pluginSearchPattern);
            foreach (string path in filePaths)
            {
                logger.Trace("Script found at: " + path);
                APlugin plugin = this.getPluginFromPython(path);
                if (plugin != null)
                {
                    this.plugins.Add(plugin);
                    logger.Debug("Plugin found: " + plugin.getName());
                }
            }
        }

        private APlugin getPluginFromPython(String pythonPath)
        {
            logger.Trace("Try to get plugin from python script");
            this.scope = this.engine.ExecuteFile(pythonPath);
            var plugin = scope.GetVariable("plugin");

            return this.buildPythonPlugin(plugin);
        }

        private APlugin buildPythonPlugin(dynamic plugin)
        {
            Model.Python.Plugin pythonPlugin = null;

            if(plugin != null)
            {
                pythonPlugin = new Model.Python.Plugin(plugin.name, plugin.description);
                List<Argument> arguments = null;
                if (plugin.get_functions().Count > 0)
                {
                    foreach (dynamic function in plugin.get_functions())
                    {
                        arguments = null;
                        if (function.arguments != null && function.arguments.Count > 0)
                        {
                            arguments = new List<Argument>();
                            foreach(dynamic argument in function.arguments)
                            {
                                arguments.Add(new Argument(argument.key, argument.description));
                            }
                        }
                        pythonPlugin.addPythonFunction(new PythonFunction(function.name, function.description, function.called_function, arguments));
                    }

                }
            }
            return pythonPlugin;
        }

        public APlugin[] getPlugins()
        {
            return this.plugins.ToArray();
        }
    }
}
