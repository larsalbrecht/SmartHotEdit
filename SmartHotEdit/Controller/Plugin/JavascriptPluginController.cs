using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHotEditPluginHost;
using System.IO;
using SmartHotEditPluginHost.Model;
using SmartHotEdit.Abstracts;
using NiL.JS;
using NiL.JS.BaseLibrary;
using NiL.JS.Core;
using NiL.JS.Extensions;
using SmartHotEdit.Model.Javascript;
using ScintillaNET;

namespace SmartHotEdit.Controller.Plugin
{

    class JavascriptPluginController : AScriptPluginController
    {
        private List<APlugin> plugins = new List<APlugin>();


        public JavascriptPluginController(PluginController pluginController) : base(pluginController)
        {
            logger.Trace("Construct JavascriptPluginController");
            this.Type = "Javascript";
            this.TypeFileExt = "js";
            this.TypeScintillaLexer = Lexer.Cpp;
        }

        public override void loadPlugins()
        {
            this.plugins.Clear();

            Module module = null;
            try
            {
                var modulePath = Path.GetFullPath(@"Javascript\Modules\pluginbase.js");

                // find plugins
                if (File.Exists(modulePath))
                {
                    var moduleContent = File.ReadAllText(modulePath);

                    string[] filePaths = this.findScriptPlugins(@"Javascript\Plugins\", "*_plugin.js");
                    foreach(string pluginPath in filePaths)
                    {
                        var fileContent = File.ReadAllText(pluginPath);
                        var script = moduleContent + Environment.NewLine + fileContent;

                        module = new Module(script);
                        module.Run();

                        var jsPlugin = module.Context.GetVariable("plugin");
                        if(jsPlugin != null)
                        {
                            APlugin plugin = buildJavascriptPlugin(jsPlugin);
                            if (plugin != null)
                            {
                                this.plugins.Add(plugin);
                                logger.Debug("Plugin found: " + plugin.getName());
                            }
                            else
                            {
                                logger.Warn("Plugin not found in script: " + pluginPath);
                            }
                        }
                    }                   
                } else
                {
                    Console.WriteLine("Does not exists");
                }
                
            }
            catch (JSException e)
            {
                var syntaxError = e.Error.Value as SyntaxError;
                if (syntaxError != null)
                {
                    Console.WriteLine(syntaxError.ToString());
                }
                else
                {
                    Console.WriteLine("Unknown error: " + e);
                }
            }
        }

        private APlugin buildJavascriptPlugin(JSValue jsPlugin)
        {
            var name = (string)jsPlugin["name"].Value;
            var description = (string)jsPlugin["description"].Value;
            Model.Javascript.Plugin plugin = plugin = new Model.Javascript.Plugin(name, description);

            var getFunctionsFunction = jsPlugin["getFunctions"].As<NiL.JS.BaseLibrary.Function>();
            if (getFunctionsFunction != null)
            {
                var functions = getFunctionsFunction.Call(jsPlugin, null);
                foreach (KeyValuePair<string, NiL.JS.Core.JSValue> keyValue in functions)
                {
                    var tempFunction = this.buildJavascriptFunction(jsPlugin, keyValue.Value);
                    if(tempFunction != null)
                    {
                        plugin.addFunction(tempFunction);
                    }
                }
            }

            return plugin;
        }

        private SmartHotEditPluginHost.Model.Function buildJavascriptFunction(JSValue jsPlugin, JSValue value)
        {
            SmartHotEditPluginHost.Model.Function function = null;
            var name = (string)value["name"].Value;
            var description = (string)value["description"].Value;
            var calledFunction = value["calledFuction"].As<NiL.JS.BaseLibrary.Function>();

            if (name != null && description != null && calledFunction != null)
            {
                List<Argument> arguments = null;
                var calledFunctionDelegate = (Func<string, List<Argument>, string>)calledFunction.MakeDelegate(typeof(Func<string, List<Argument>, string>));
                if(value["argumentList"] != null)
                {
                    arguments = new List<Argument>();
                    foreach (KeyValuePair<string, NiL.JS.Core.JSValue> keyValue in value["argumentList"])
                    {
                        arguments.Add(new Argument((string)keyValue.Value["key"].Value, (string)keyValue.Value["description"].Value));
                    }
                }
                function = new SmartHotEditPluginHost.Model.Function(name, description, new SmartHotEditPluginHost.Model.Function.Transform(calledFunctionDelegate), arguments);
            }
            return function;
        }

        protected override APlugin[] getPlugins()
        {
            return this.plugins.ToArray();
        }

        public override bool isEnabled()
        {
            return Properties.Settings.Default.EnablePythonPlugins;
        }
    }
}
