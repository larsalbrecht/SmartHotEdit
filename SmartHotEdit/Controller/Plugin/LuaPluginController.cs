using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using SmartHotEditPluginHost.Model;
using System.IO;
using MoonSharp.Interpreter.REPL;
using SmartHotEdit.Model.Lua;
using SmartHotEditPluginHost;
using NLog;
using System.Text;

namespace SmartHotEdit.Controller.Plugin
{
    class LuaPluginController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private List<APlugin> plugins = new List<APlugin>();

        private PluginController pluginController;

        public LuaPluginController(PluginController pluginController)
        {
            logger.Trace("Construct LuaPluginController");
            this.pluginController = pluginController;
            this.loadPlugins();
            logger.Debug("Lua Plugins found: " + this.plugins.Count());
        }

        private Script getConfiguredScript()
        {
            Script script = new Script();

            // TODO Check if directories exists and disable plugin if not
            // set script loader to load modules
            script.Options.ScriptLoader = new ReplInterpreterScriptLoader();
            var originalModulePaths = ((ScriptLoaderBase)script.Options.ScriptLoader).ModulePaths;
            var customModulePaths = new string[] { "Lua/Modules/?", "Lua/Modules/?.lua", "Lua/Plugins/?", "Lua/Plugins/?.lua" };
            logger.Trace("Custom module paths added: " + string.Join("; ", customModulePaths));
            var mergedModulePaths = new string[customModulePaths.Length + originalModulePaths.Length];
            originalModulePaths.CopyTo(mergedModulePaths, 0);
            customModulePaths.CopyTo(mergedModulePaths, originalModulePaths.Length);
            logger.Debug("Merged module paths: " + string.Join("; ", mergedModulePaths));
            ((ScriptLoaderBase)script.Options.ScriptLoader).ModulePaths = mergedModulePaths;


            string[] neededModules = new string[] { "class", "baseplugin", "pluginhelper", "inspect" };

            logger.Debug("Require needed modules: " + string.Join("; ", neededModules));
            foreach (string moduleName in neededModules)
            {
                script.RequireModule(moduleName);
            }
            return script;
        }

        private void loadPlugins()
        {
            Script script = this.getConfiguredScript();

            logger.Trace("Register UserData types");
            // register types
            UserData.RegisterType<SmartHotEditPluginHost.Model.Function>();
            UserData.RegisterType<Argument>();
            UserData.RegisterType<List<Argument>>();

            String pluginPath = "Lua\\Plugins\\";
            String pluginSearchPattern = "*_plugin.lua";
            logger.Trace("Find plugins in path: " + pluginPath + "; with search pattern: " + pluginSearchPattern);
            // find plugins
            string[] filePaths = Directory.GetFiles(pluginPath, pluginSearchPattern);
            foreach (string path in filePaths)
            {
                logger.Trace("Script found at: " + path);
                APlugin plugin = this.getPluginFromScript(script, path);
                if(plugin != null)
                {
                    this.plugins.Add(plugin);
                    logger.Debug("Plugin found: " + plugin.getName());
                }
            }
            
        }

        private APlugin getPluginFromScript(Script script, String scriptPath)
        {
            logger.Trace("Try to get plugin from script");
            script.DoFile(scriptPath);
            DynValue res = script.Globals.Get("plugin");

            return buildLuaPlugin(res);
        }

        private APlugin buildLuaPlugin(DynValue dynValue)
        {
            SmartHotEdit.Model.Lua.Plugin luaPlugin = null;

            if (dynValue.Type == DataType.Table)
            {
                luaPlugin = new SmartHotEdit.Model.Lua.Plugin();
                foreach (DynValue dynMembers in dynValue.Table.Keys)
                {
                    // parse functions if available
                    if (dynValue.Table.Get(dynMembers).Type == DataType.Table)
                    {
                        foreach (DynValue dynFunction in dynValue.Table.Get(dynMembers).Table.Keys)
                        {
                            var luaFunction = buildLuaFunction(dynValue.Table.Get(dynMembers).Table.Get(dynFunction));
                            luaPlugin.addLuaFunction(luaFunction);
                        }
                    }
                    // parse members if available
                    else if (dynValue.Table.Get(dynMembers).Type == DataType.String)
                    {
                        if (dynMembers.String == "name")
                        {
                            luaPlugin.name = dynValue.Table.Get(dynMembers).String;
                        }
                        else if (dynMembers.String == "description")
                        {
                            luaPlugin.description = dynValue.Table.Get(dynMembers).String;
                        }
                    }
                }
            }

            return luaPlugin;
        }

        private LuaFunction buildLuaFunction(DynValue dynValue)
        {
            LuaFunction func = null;
            if (dynValue.Type == DataType.Table)
            {
                String name = null;
                String description = null;
                Closure closure = null;
                List<Argument> arguments = null;
                foreach (DynValue dyn in dynValue.Table.Keys)
                {
                    if (dyn.String == "name")
                    {
                        name = dynValue.Table.Get(dyn).String;
                    }
                    else if (dyn.String == "description")
                    {
                        description = dynValue.Table.Get(dyn).String;
                    }
                    else if (dyn.String == "calledFunction")
                    {
                        closure = dynValue.Table.Get(dyn).Function;
                    } else if(dyn.String == "arguments")
                    {
                        arguments = this.buildArguments(dynValue.Table.Get(dyn));
                    }
                }
                if (name != null && description != null && closure != null)
                {
                    func = new LuaFunction(name, description, closure, arguments);
                }
            }

            return func;
        }

        private List<Argument> buildArguments(DynValue dynValue)
        {
            List<Argument> arguments = null;
            if (dynValue.Type == DataType.Table)
            {
                Console.WriteLine(dynValue.Table);
                foreach (DynValue dynArgument in dynValue.Table.Keys)
                {
                    String key = null;
                    String description = null;
                    foreach (DynValue dyn in dynValue.Table.Get(dynArgument).Table.Keys)
                    {
                        if (dyn.String == "key")
                        {
                            key = dynValue.Table.Get(dynArgument).Table.Get(dyn).String;
                            
                        }
                        else if (dyn.String == "description")
                        {
                            description = dynValue.Table.Get(dynArgument).Table.Get(dyn).String;
                        }
                    }
                    if(key != null && description != null)
                    {
                        if(arguments == null)
                        {
                            arguments = new List<Argument>();
                        }
                        arguments.Add(new Argument(key, description));
                    }
                }
            }

            return arguments;
        }

        public APlugin[] getPlugins()
        {
            return this.plugins.ToArray();
        }

    }
}
