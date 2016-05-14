using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SmartHotEdit.Controller;
using ScintillaNET;

namespace SmartHotEdit.Abstracts
{
    abstract class AScriptPluginController : APluginController
    {

        public string TypeFileExt;
        public Lexer TypeScintillaLexer;

        public AScriptPluginController(PluginController pluginController) : base(pluginController)
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
    }
}
