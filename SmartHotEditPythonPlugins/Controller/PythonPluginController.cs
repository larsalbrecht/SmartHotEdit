using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using ScintillaNET;
using SmartHotEditPluginHost;
using SmartHotEditPluginHost.Model;
using SmartHotEditPythonPlugins.Model;
using SmartHotEditPythonPlugins.Properties;

namespace SmartHotEditPythonPlugins.Controller
{
    [Export(typeof(AScriptPluginController))]
    internal class PythonPluginController : AScriptPluginController
    {
        private const string PluginPath = @"Python\Plugins\";

        private ScriptEngine _engine;
        private readonly List<APlugin> _plugins = new List<APlugin>();
        private ScriptScope _scope;

        [ImportingConstructor]
        public PythonPluginController([Import("IPluginController")] IPluginController pluginController)
            : base(pluginController)
        {
            Logger.Trace("Construct JavascriptPluginController");
            this.Type = "Python";
            this.TypeFileExt = "py";
            this.TypePluginPath = PluginPath;
            this.TypeScintillaLexer = Lexer.Python;
        }

        public override void LoadPlugins()
        {
            this._plugins.Clear();
            this._engine = Python.CreateEngine();
            this._scope = _engine.CreateScope();

            var paths = _engine.GetSearchPaths();
            paths.Add(Path.GetFullPath(@"Python\Modules"));
            _engine.SetSearchPaths(paths);

            // find plugins
            var filePaths = FindScriptPlugins(PluginPath, "*_plugin.py");
            foreach (var path in filePaths)
            {
                Logger.Trace("Script found at: " + path);
                var plugin = this.GetPluginFromPython(path);
                if (plugin != null)
                {
                    this._plugins.Add(plugin);
                    Logger.Debug("Plugin found: " + plugin.Name);
                }
                else
                {
                    Logger.Warn("Plugin not found in script: " + path);
                }
            }
        }

        private APlugin GetPluginFromPython(string pythonPath)
        {
            Logger.Trace("Try to get plugin from python script");
            try
            {
                this._scope = this._engine.ExecuteFile(pythonPath);
                var plugin = _scope.GetVariable("plugin");

                return this.buildPythonPlugin(plugin);
            }
            catch (SyntaxErrorException e)
            {
                Logger.Error("Error occured on execute file: " + pythonPath + " > (Line Number: " + e.Line + ") [" +
                             e.Message + "] {Errorline: '" + e.GetCodeLine() + "'}");
            }
            return null;
        }

        private APlugin buildPythonPlugin(dynamic plugin)
        {
            if (plugin == null) return null;

            var pythonPlugin = new Plugin(plugin.name, plugin.description);
            if (plugin.get_functions().Count <= 0) return pythonPlugin;

            foreach (var function in plugin.get_functions())
            {
                List<Argument> arguments = null;
                if (function.arguments != null && function.arguments.Count > 0)
                {
                    arguments = new List<Argument>();
                    foreach (var argument in function.arguments)
                    {
                        arguments.Add(new Argument(argument.key, argument.description));
                    }
                }
                pythonPlugin.AddPythonFunction(new PythonFunction(function.name, function.description,
                    function.called_function, arguments));
            }
            return pythonPlugin;
        }

        protected override APlugin[] GetPlugins()
        {
            return this._plugins.ToArray();
        }

        public override string GetTemplate()
        {
            return Resources.template_py;
        }

        public override void SetScintillaConfiguration(Scintilla scintilla)
        {
            // Some properties we like
            scintilla.SetProperty("tab.timmy.whinge.level", "1");
            scintilla.SetProperty("fold", "1");

            // Use margin 2 for fold markers
            scintilla.Margins[2].Type = MarginType.Symbol;
            scintilla.Margins[2].Mask = Marker.MaskFolders;
            scintilla.Margins[2].Sensitive = true;
            scintilla.Margins[2].Width = 20;

            // Reset folder markers
            for (var i = Marker.FolderEnd; i <= Marker.FolderOpen; i++)
            {
                scintilla.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                scintilla.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            // Style the folder markers
            scintilla.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            scintilla.Markers[Marker.Folder].SetBackColor(SystemColors.ControlText);
            scintilla.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            scintilla.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            scintilla.Markers[Marker.FolderEnd].SetBackColor(SystemColors.ControlText);
            scintilla.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            scintilla.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            scintilla.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            scintilla.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            scintilla.AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;

            // Set the styles
            scintilla.Styles[Style.Python.Default].ForeColor = Color.FromArgb(0x80, 0x80, 0x80);
            scintilla.Styles[Style.Python.CommentLine].ForeColor = Color.FromArgb(0x00, 0x7F, 0x00);
            scintilla.Styles[Style.Python.CommentLine].Italic = true;
            scintilla.Styles[Style.Python.Number].ForeColor = Color.FromArgb(0x00, 0x7F, 0x7F);
            scintilla.Styles[Style.Python.String].ForeColor = Color.FromArgb(0x7F, 0x00, 0x7F);
            scintilla.Styles[Style.Python.Character].ForeColor = Color.FromArgb(0x7F, 0x00, 0x7F);
            scintilla.Styles[Style.Python.Word].ForeColor = Color.FromArgb(0x00, 0x00, 0x7F);
            scintilla.Styles[Style.Python.Word].Bold = true;
            scintilla.Styles[Style.Python.Triple].ForeColor = Color.FromArgb(0x7F, 0x00, 0x00);
            scintilla.Styles[Style.Python.TripleDouble].ForeColor = Color.FromArgb(0x7F, 0x00, 0x00);
            scintilla.Styles[Style.Python.ClassName].ForeColor = Color.FromArgb(0x00, 0x00, 0xFF);
            scintilla.Styles[Style.Python.ClassName].Bold = true;
            scintilla.Styles[Style.Python.DefName].ForeColor = Color.FromArgb(0x00, 0x7F, 0x7F);
            scintilla.Styles[Style.Python.DefName].Bold = true;
            scintilla.Styles[Style.Python.Operator].Bold = true;
            // scintilla.Styles[Style.Python.Identifier] ... your keywords styled here
            scintilla.Styles[Style.Python.CommentBlock].ForeColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
            scintilla.Styles[Style.Python.CommentBlock].Italic = true;
            scintilla.Styles[Style.Python.StringEol].ForeColor = Color.FromArgb(0x00, 0x00, 0x00);
            scintilla.Styles[Style.Python.StringEol].BackColor = Color.FromArgb(0xE0, 0xC0, 0xE0);
            scintilla.Styles[Style.Python.StringEol].FillLine = true;
            scintilla.Styles[Style.Python.Word2].ForeColor = Color.FromArgb(0x40, 0x70, 0x90);
            scintilla.Styles[Style.Python.Decorator].ForeColor = Color.FromArgb(0x80, 0x50, 0x00);

            // Important for Python
            scintilla.ViewWhitespace = WhitespaceMode.VisibleAlways;
            var python2 =
                "and as assert break class continue def del elif else except exec finally for from global if import in is lambda not or pass print raise return try while with yield";
            var cython = "cdef cimport cpdef";

            scintilla.SetKeywords(0, python2 + " " + cython);
        }

        public override APlugin GetPluginForScript(string text)
        {
            throw new NotImplementedException();
        }
    }
}