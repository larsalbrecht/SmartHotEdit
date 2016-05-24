﻿using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System.IO;
using MoonSharp.Interpreter.REPL;
using SmartHotEditPluginHost;
using SmartHotEditPluginHost.Model;
using SmartHotEditLuaPlugins.Model;
using NLog;
using System.Text;
using ScintillaNET;
using System.Drawing;
using System.ComponentModel.Composition;

namespace SmartHotEditLuaPlugins.Controller
{
    [Export(typeof(SmartHotEditPluginHost.AScriptPluginController))]
    class LuaPluginController : AScriptPluginController
    {
        private List<APlugin> plugins = new List<APlugin>();
        private const String PLUGIN_PATH = @"Lua\Plugins\";

        [ImportingConstructor]
        public LuaPluginController([Import("IPluginController")]IPluginController pluginController) : base(pluginController)
        {
            Logger.Trace("Construct LuaPluginController");
            this.Type = "Lua";
            this.TypeFileExt = "lua";
            this.TypePluginPath = LuaPluginController.PLUGIN_PATH;
            this.TypeScintillaLexer = Lexer.Lua;
        }

        private Script getConfiguredScript()
        {
            Script script = new Script();

            // TODO Check if directories exists and disable plugin if not
            // set script loader to load modules
            script.Options.ScriptLoader = new ReplInterpreterScriptLoader();
            var originalModulePaths = ((ScriptLoaderBase)script.Options.ScriptLoader).ModulePaths;
            var customModulePaths = new string[] { "Lua/Modules/?", "Lua/Modules/?.lua", LuaPluginController.PLUGIN_PATH + "?", LuaPluginController.PLUGIN_PATH + "?.lua" };
            Logger.Trace("Custom module paths added: " + string.Join("; ", customModulePaths));
            var mergedModulePaths = new string[customModulePaths.Length + originalModulePaths.Length];
            originalModulePaths.CopyTo(mergedModulePaths, 0);
            customModulePaths.CopyTo(mergedModulePaths, originalModulePaths.Length);
            Logger.Debug("Merged module paths: " + string.Join("; ", mergedModulePaths));
            ((ScriptLoaderBase)script.Options.ScriptLoader).ModulePaths = mergedModulePaths;


            string[] neededModules = new string[] { "class", "baseplugin", "pluginhelper", "inspect" };

            Logger.Debug("Require needed modules: " + string.Join("; ", neededModules));
            foreach (string moduleName in neededModules)
            {
                script.RequireModule(moduleName);
            }
            return script;
        }

        public override void LoadPlugins()
        {
            this.plugins.Clear();
            Script script = this.getConfiguredScript();

            Logger.Trace("Register UserData types");
            // register types
            UserData.RegisterType<SmartHotEditPluginHost.Model.Function>();
            UserData.RegisterType<Argument>();
            UserData.RegisterType<List<Argument>>();

            // find plugins
            string[] filePaths = this.FindScriptPlugins(LuaPluginController.PLUGIN_PATH, "*_plugin.lua");
            foreach (string path in filePaths)
            {
                Logger.Trace("Script found at: " + path);
                APlugin plugin = this.getPluginFromScript(script, path);
                if (plugin != null)
                {
                    this.plugins.Add(plugin);
                    Logger.Debug("Plugin found: " + plugin.Name);
                } else
                {
                    Logger.Warn("Plugin not found in script: " + path);
                }
            }
        }

        private APlugin getPluginFromScript(Script script, String scriptPath)
        {
            Logger.Trace("Try to get plugin from script");
            try
            {
                script.DoFile(scriptPath);
                DynValue res = script.Globals.Get("plugin");

                return this.buildLuaPlugin(res);
            }
            catch (Exception)
            {
                // TODO fix logger.Error("Error occured on execute file: " + scriptPath + " > (Line Number: " + e.Line + ") [" + e.Message + "] {Errorline: '" + e.GetCodeLine() + "'}");
            }
            return null;
        }

        private APlugin buildLuaPlugin(DynValue dynValue)
        {
            Plugin luaPlugin = null;

            if (dynValue.Type == DataType.Table)
            {
                luaPlugin = new Plugin();
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
                    }
                    else if (dyn.String == "arguments")
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
                    if (key != null && description != null)
                    {
                        if (arguments == null)
                        {
                            arguments = new List<Argument>();
                        }
                        arguments.Add(new Argument(key, description));
                    }
                }
            }

            return arguments;
        }

        protected override APlugin[] GetPlugins()
        {
            return this.plugins.ToArray();
        }

        public override bool IsEnabled()
        {
            return true; // TODO fix this (make dynamic) Properties.Settings.Default.EnableLuaPlugins;
        }

        public override string GetTemplate()
        {
            return SmartHotEditLuaPlugins.Properties.Resources.template_lua;
        }

        public override void SetScintillaConfiguration(Scintilla scintilla)
        {
            // Obtained from SciLexer.h
            const int SCE_LUA_DEFAULT = 0;
            const int SCE_LUA_COMMENT = 1;

            scintilla.Styles[SCE_LUA_DEFAULT].ForeColor = Color.Black;
            scintilla.Styles[SCE_LUA_COMMENT].ForeColor = Color.Green;

            scintilla.SetKeywords(0, "and break do else elseif end false for function if in local nil not or repeat return then true until while");
            scintilla.SetKeywords(1, "+-* / % ^ #  == ~= <= >= < > =  ( ) { } [ ]  ; : , . .. ...");
        }

        public override APlugin GetPluginForScript(string text)
        {
            throw new NotImplementedException();
        }
    }
}
