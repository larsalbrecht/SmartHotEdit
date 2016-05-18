using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ScintillaNET;

namespace SmartHotEditPluginHost
{
    public abstract class AScriptPluginController : APluginController
    {

        public string TypeFileExt { get; set; }
        public Lexer TypeScintillaLexer { get; set; }

        public AScriptPluginController(IPluginController pluginController) : base(pluginController)
        {
        }

        public new bool isFullyImplemented()
        {
            return base.isFullyImplemented() && this.TypeFileExt != null;
        }

        protected string[] findScriptPlugins(String pathStr, String searchPattern)
        {
            logger.Trace("Find plugins in path: " + pathStr + "; with search pattern: " + searchPattern);
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

        public abstract String getTemplate();

        public abstract void setScintillaConfiguration(Scintilla scintilla);
    }
}
