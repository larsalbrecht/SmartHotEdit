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

namespace SmartHotEdit.Controller.Plugin
{

    class JavascriptPluginController : APluginController
    {
        private List<APlugin> plugins = new List<APlugin>();


        public JavascriptPluginController(PluginController pluginController) : base(pluginController)
        {
            logger.Trace("Construct JavascriptPluginController");
            this.Type = "Javascript";
        }

        public override void loadPlugins()
        {
            this.plugins.Clear();

            Module module = null;
            try
            {
                var modulePath = Path.GetFullPath(@"Javascript\Modules\pluginbase.js");
                var pluginPath = Path.GetFullPath(@"Javascript\Plugins\case_plugin.js");
                if (File.Exists(modulePath) && File.Exists(pluginPath))
                {
                    var moduleContent = File.ReadAllText(modulePath);
                    var fileContent = File.ReadAllText(pluginPath);
                    var script = moduleContent + Environment.NewLine + fileContent + Environment.NewLine + "var plugin = new CustomPlugin(); var functions = plugin.getFunctions();";

                    module = new Module(script);
                    module.Run();
                    var plugin = module.Context.GetVariable("plugin");
                    var functions = module.Context.GetVariable("functions");
                    if (plugin != null)
                    {
                        Console.WriteLine(plugin["getFunctions"]);
                    }
                    if (functions != null)
                    {
                        Console.WriteLine(functions["0"]["name"]);
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
