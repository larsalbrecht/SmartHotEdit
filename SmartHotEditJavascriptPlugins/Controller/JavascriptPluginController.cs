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
        private const string PLUGIN_PATH = @"Javascript\Plugins\";

        [ImportingConstructor]
        public JavascriptPluginController([Import("IPluginController")]IPluginController pluginController) : base(pluginController)
        {
            Logger.Trace("Construct JavascriptPluginController");
            this.Type = "Javascript";
            this.TypeFileExt = "js";
            this.TypePluginPath = JavascriptPluginController.PLUGIN_PATH;
            this.TypeScintillaLexer = Lexer.Cpp;
        }

        private APlugin getPluginFromScript(string script)
        {
            APlugin resultPlugin = null;
            Module module = null;

            var modulePath = Path.GetFullPath(@"Javascript\Modules\pluginbase.js");

            // find plugins
            if (File.Exists(modulePath) && script != null && script != "")
            {
                var moduleContent = File.ReadAllText(modulePath);
                var finalScript = moduleContent + Environment.NewLine + script;
                module = new Module(finalScript);
                module.Run();

                var jsPlugin = module.Context.GetVariable("plugin");
                if (this.isValidPlugin(jsPlugin))
                {
                    resultPlugin = this.buildJavascriptPlugin(jsPlugin);
                }
            }

            return resultPlugin;
        }

        public override void LoadPlugins()
        {
            this.plugins.Clear();
            try
            {
                string[] filePaths = this.FindScriptPlugins(JavascriptPluginController.PLUGIN_PATH, "*_plugin.js");
                foreach (string pluginPath in filePaths)
                {
                    var fileContent = File.ReadAllText(pluginPath);

                    APlugin plugin = this.getPluginFromScript(fileContent);
                    if (plugin != null)
                    {
                        this.plugins.Add(plugin);
                        Logger.Debug("Plugin found: " + plugin.Name);
                    }
                    else
                    {
                        Logger.Warn("Plugin not found in script: " + pluginPath);
                    }
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

        private bool isValidPlugin(JSValue jsPlugin)
        {
            bool result = false;

            if(jsPlugin != null && jsPlugin.Is(JSValueType.Object))
            {
                if(jsPlugin["name"] != null && jsPlugin["description"] != null)
                {
                    result = true;
                }
            }

            return result;
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
                    if (tempFunction != null)
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
                if (value["argumentList"] != null)
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

        protected override APlugin[] GetPlugins()
        {
            return this.plugins.ToArray();
        }

        public override bool IsEnabled()
        {
            return true; // Fix this (make dynamic) Properties.Settings.Default.EnablePythonPlugins;
        }

        public override string GetTemplate()
        {
            return SmartHotEditJavascriptPlugins.Properties.Resources.template_js;
        }

        public override void SetScintillaConfiguration(Scintilla scintilla)
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

        public override APlugin GetPluginForScript(string text)
        {
            return this.getPluginFromScript(text);
        }
    }
}
