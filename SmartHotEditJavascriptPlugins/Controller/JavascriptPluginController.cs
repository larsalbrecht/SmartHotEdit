using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHotEditPluginHost;
using System.IO;
using SmartHotEditPluginHost.Model;
using NiL.JS;
using NiL.JS.BaseLibrary;
using NiL.JS.Core;
using NiL.JS.Extensions;
using ScintillaNET;
using System.Drawing;
using SmartHotEditJavascriptPlugins.Model;
using System.ComponentModel.Composition;

namespace SmartHotEditJavascriptPlugins.Controller
{
    [Export(typeof(SmartHotEditPluginHost.AScriptPluginController))]
    class JavascriptPluginController : AScriptPluginController
    {
        private List<APlugin> plugins = new List<APlugin>();

        [ImportingConstructor]
        public JavascriptPluginController([Import("IPluginController")]IPluginController pluginController) : base(pluginController)
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
            Plugin plugin = plugin = new Plugin(name, description);

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
            return true; // Fix this (make dynamic) Properties.Settings.Default.EnablePythonPlugins;
        }

        public override string getTemplate()
        {
            return SmartHotEditJavascriptPlugins.Properties.Resources.template_js;
        }

        public override void setScintillaConfiguration(Scintilla scintilla)
        {
            scintilla.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            scintilla.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            scintilla.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            scintilla.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            scintilla.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            scintilla.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            scintilla.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            scintilla.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;

            scintilla.SetKeywords(0, "abstract arguments boolean break byte case catch char class const continue debugger default delete do double else enum eval export extends false final finally float for function goto if implements import in instanceof int interface let long native new null package private protected public return short static super switch synchronized this throw throws transient true try typeof var void volatile while with yield");
            scintilla.SetKeywords(1, "Array Date eval function hasOwnProperty Infinity isFinite isNaN isPrototypeOf length Math NaN name Number Object prototype String toString undefined valueOf");
        }
    }
}
