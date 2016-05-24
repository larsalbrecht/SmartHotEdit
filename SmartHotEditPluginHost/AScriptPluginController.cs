using System;
using System.IO;
using ScintillaNET;

namespace SmartHotEditPluginHost
{
    public abstract class AScriptPluginController : APluginController
    {

        public string TypePluginPath { get; set; }
        public string TypeFileExt { get; set; }
        public Lexer TypeScintillaLexer { get; set; }

        protected AScriptPluginController(IPluginController pluginController) : base(pluginController)
        {
        }

        // ReSharper disable once UnusedMember.Local
        private new bool IsFullyImplemented()
        {
            return base.IsFullyImplemented() && this.TypeFileExt != null && this.TypePluginPath != null;
        }

        protected string[] FindScriptPlugins(String pathStr, String searchPattern)
        {
            Logger.Trace("Find plugins in path: " + pathStr + "; with search pattern: " + searchPattern);
            string[] result = null;
            if (pathStr != null && searchPattern != null)
            {
                var path = Path.GetFullPath(pathStr);
                if (Directory.Exists(path))
                {
                    result = Directory.GetFiles(path, searchPattern);
                }
            }

            return result;
        }

        public abstract string GetTemplate();

        public abstract void SetScintillaConfiguration(Scintilla scintilla);

        public abstract APlugin GetPluginForScript(string text);
    }
}
