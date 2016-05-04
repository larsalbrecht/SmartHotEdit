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

namespace SmartHotEdit.Controller.Plugin
{
    class LuaPluginController
    {
        private List<APlugin> plugins = null;

        public LuaPluginController()
        {
            this.plugins = new List<APlugin>();
            this.loadPlugins();
            System.Diagnostics.Debug.WriteLine("Lua Plugins found: " + this.plugins.Count());
        }

        private void loadPlugins()
        {
            Script script = new Script();

            // set script loader to load modules
            script.Options.ScriptLoader = new ReplInterpreterScriptLoader();
            var originalModulePaths = ((ScriptLoaderBase)script.Options.ScriptLoader).ModulePaths;
            var customModulePaths = new string[] { "Lua/Modules/?.lua", "Lua/Plugins/?.lua" };
            var mergedModulePaths = new string[customModulePaths.Length + originalModulePaths.Length];
            originalModulePaths.CopyTo(mergedModulePaths, 0);
            customModulePaths.CopyTo(mergedModulePaths, originalModulePaths.Length);
            ((ScriptLoaderBase)script.Options.ScriptLoader).ModulePaths = mergedModulePaths;

            // default modules
            script.RequireModule("pluginhelper");
            script.RequireModule("inspect");

            // register types
            UserData.RegisterType<SmartHotEditPluginHost.Model.Function>();
            UserData.RegisterType<Argument>();
            UserData.RegisterType<List<Argument>>();

            // find plugins
            string[] filePaths = Directory.GetFiles("Lua\\Plugins\\", "*_plugin.lua");
            foreach (string path in filePaths)
            {
                APlugin plugin = this.getPluginFromScript(script, path);
                if(plugin != null)
                {
                    this.plugins.Add(plugin);
                }
            }
            
        }

        private APlugin getPluginFromScript(Script script, String scriptPath)
        {
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
