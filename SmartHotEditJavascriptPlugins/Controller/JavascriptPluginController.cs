using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using NiL.JS;
using NiL.JS.BaseLibrary;
using NiL.JS.Core;
using NiL.JS.Extensions;
using ScintillaNET;
using SmartHotEditJavascriptPlugins.Model;
using SmartHotEditJavascriptPlugins.Properties;
using SmartHotEditPluginHost;
using SmartHotEditPluginHost.Model;
using Function = SmartHotEditPluginHost.Model.Function;

namespace SmartHotEditJavascriptPlugins.Controller
{
    [Export(typeof(AScriptPluginController))]
    internal class JavascriptPluginController : AScriptPluginController
    {
        private const string PluginPath = @"Javascript\Plugins\";
        private readonly List<APlugin> _plugins = new List<APlugin>();

        [ImportingConstructor]
        public JavascriptPluginController([Import("IPluginController")] IPluginController pluginController)
            : base(pluginController)
        {
            Logger.Trace("Construct JavascriptPluginController");
            this.Type = "Javascript";
            this.TypeFileExt = "js";
            this.TypePluginPath = PluginPath;
            this.TypeScintillaLexer = Lexer.Cpp;
        }

        private APlugin GetPluginFromScript(string script)
        {
            APlugin resultPlugin = null;

            var modulePath = Path.GetFullPath(@"Javascript\Modules\pluginbase.js");

            // find plugins
            if (!File.Exists(modulePath) || string.IsNullOrEmpty(script)) return null;

            var moduleContent = File.ReadAllText(modulePath);
            var finalScript = moduleContent + Environment.NewLine + script;
            var module = new Module(finalScript);
            module.Run();

            var jsPlugin = module.Context.GetVariable("plugin");
            if (this.isValidPlugin(jsPlugin))
            {
                resultPlugin = this.BuildJavascriptPlugin(jsPlugin);
            }

            return resultPlugin;
        }

        public override void LoadPlugins()
        {
            this._plugins.Clear();
            try
            {
                var filePaths = FindScriptPlugins(PluginPath, "*_plugin.js");
                foreach (var pluginPath in filePaths)
                {
                    var fileContent = File.ReadAllText(pluginPath);

                    var plugin = this.GetPluginFromScript(fileContent);
                    if (plugin != null)
                    {
                        this._plugins.Add(plugin);
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
                    Logger.Error(syntaxError.ToString());
                }
                else
                {
                    Logger.Error("Unknown error: " + e);
                }
            }
        }

        private bool isValidPlugin(JSValue jsPlugin)
        {
            var result = false;

            if (jsPlugin != null && jsPlugin.Is(JSValueType.Object))
            {
                if (jsPlugin["name"] != null && jsPlugin["description"] != null)
                {
                    result = true;
                }
            }

            return result;
        }

        private APlugin BuildJavascriptPlugin(JSValue jsPlugin)
        {
            var name = (string) jsPlugin["name"].Value;
            var description = (string) jsPlugin["description"].Value;
            Plugin plugin = new Plugin(name, description);

            var getFunctionsFunction = jsPlugin["getFunctions"].As<NiL.JS.BaseLibrary.Function>();

            if (getFunctionsFunction == null) return plugin;

            var functions = getFunctionsFunction.Call(jsPlugin, null);
            foreach (var keyValue in functions)
            {
                var tempFunction = BuildJavascriptFunction(jsPlugin, keyValue.Value);
                if (tempFunction != null)
                {
                    plugin.AddFunction(tempFunction);
                }
            }

            return plugin;
        }

        private static Function BuildJavascriptFunction(JSValue jsPlugin, JSValue value)
        {
            if (jsPlugin == null) throw new ArgumentNullException(nameof(jsPlugin));

            var name = (string) value["name"].Value;
            var description = (string) value["description"].Value;
            var calledFunction = value["calledFuction"].As<NiL.JS.BaseLibrary.Function>();

            if (name == null || description == null || calledFunction == null) return null;

            List<Argument> arguments = null;
            var calledFunctionDelegate =
                (Func<string, List<Argument>, string>)
                    calledFunction.MakeDelegate(typeof(Func<string, List<Argument>, string>));
            if (value["argumentList"] != null)
            {
                arguments = new List<Argument>();
                foreach (var keyValue in value["argumentList"])
                {
                    arguments.Add(new Argument((string) keyValue.Value["key"].Value,
                        (string) keyValue.Value["description"].Value));
                }
            }
            var function = new Function(name, description, new Function.Transform(calledFunctionDelegate), arguments);
            return function;
        }

        protected override APlugin[] GetPlugins()
        {
            return this._plugins.ToArray();
        }

        public override string GetTemplate()
        {
            return Resources.template_js;
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

            scintilla.SetKeywords(0,
                "abstract arguments boolean break byte case catch char class const continue debugger default delete do double else enum eval export extends false final finally float for function goto if implements import in instanceof int interface let long native new null package private protected public return short static super switch synchronized this throw throws transient true try typeof var void volatile while with yield");
            scintilla.SetKeywords(1,
                "Array Date eval function hasOwnProperty Infinity isFinite isNaN isPrototypeOf length Math NaN name Number Object prototype String toString undefined valueOf");
        }

        public override APlugin GetPluginForScript(string text)
        {
            return this.GetPluginFromScript(text);
        }
    }
}