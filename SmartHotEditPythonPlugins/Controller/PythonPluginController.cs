using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using SmartHotEditPluginHost;
using NLog;
using Microsoft.Scripting.Hosting;
using System.IO;
using SmartHotEditPluginHost.Model;
using ScintillaNET;
using System.Drawing;
using SmartHotEditPythonPlugins.Model;
using System.ComponentModel.Composition;

namespace SmartHotEditPythonPlugins.Controller
{

    [Export(typeof(SmartHotEditPluginHost.AScriptPluginController))]
    class PythonPluginController : AScriptPluginController
    {
        private List<APlugin> plugins = new List<APlugin>();
        private const String PLUGIN_PATH = @"Python\Plugins\";

        private ScriptEngine engine;
        private ScriptScope scope;

        [ImportingConstructor]
        public PythonPluginController([Import("IPluginController")]IPluginController pluginController) : base(pluginController)
        {
            logger.Trace("Construct JavascriptPluginController");
            this.Type = "Python";
            this.TypeFileExt = "py";
            this.TypePluginPath = PythonPluginController.PLUGIN_PATH;
            this.TypeScintillaLexer = Lexer.Python;
        }        

        public override void loadPlugins()
        {
            this.plugins.Clear();
            this.engine = Python.CreateEngine();
            this.scope = engine.CreateScope();

            ICollection<string> paths = engine.GetSearchPaths();
            paths.Add(Path.GetFullPath(@"Python\Modules"));
            engine.SetSearchPaths(paths);

            // find plugins
            string[] filePaths = this.findScriptPlugins(PythonPluginController.PLUGIN_PATH, "*_plugin.py");
            foreach (string path in filePaths)
            {
                logger.Trace("Script found at: " + path);
                APlugin plugin = this.getPluginFromPython(path);
                if (plugin != null)
                {
                    this.plugins.Add(plugin);
                    logger.Debug("Plugin found: " + plugin.getName());
                } else
                {
                    logger.Warn("Plugin not found in script: " + path);
                }
            }
        }

        private APlugin getPluginFromPython(String pythonPath)
        {
            logger.Trace("Try to get plugin from python script");
            try
            {
                this.scope = this.engine.ExecuteFile(pythonPath);
                var plugin = scope.GetVariable("plugin");

                return this.buildPythonPlugin(plugin);
            } catch(Microsoft.Scripting.SyntaxErrorException e)
            {
                logger.Error("Error occured on execute file: " + pythonPath + " > (Line Number: " + e.Line + ") [" + e.Message + "] {Errorline: '" + e.GetCodeLine() + "'}");
            }
            return null;
        }

        private APlugin buildPythonPlugin(dynamic plugin)
        {
            Plugin pythonPlugin = null;

            if(plugin != null)
            {
                pythonPlugin = new Plugin(plugin.name, plugin.description);
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

        protected override APlugin[] getPlugins()
        {
            return this.plugins.ToArray();
        }

        public override bool isEnabled()
        {
            return true;// TODO fix (make dynamic) Properties.Settings.Default.EnablePythonPlugins;
        }

        public override string getTemplate()
        {
            return SmartHotEditPythonPlugins.Properties.Resources.template_py;
        }

        public override void setScintillaConfiguration(Scintilla scintilla)
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
            for (int i = Marker.FolderEnd; i <= Marker.FolderOpen; i++)
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
            scintilla.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

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
            var python2 = "and as assert break class continue def del elif else except exec finally for from global if import in is lambda not or pass print raise return try while with yield";
            var cython = "cdef cimport cpdef";

            scintilla.SetKeywords(0, python2 + " " + cython);
        }

        public override APlugin getPluginForScript(string text)
        {
            throw new NotImplementedException();
        }
    }
}
