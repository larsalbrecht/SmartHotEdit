using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using SmartHotEditPluginHost;
using SmartHotEditPluginHost.Model;

namespace SmartHotEditBasePlugins
{
    /// <summary>
    ///     Description of StringPlugin.
    /// </summary>
    [Export(typeof(APlugin))]
    public class StringPlugin : APlugin
    {
        public StringPlugin()
        {
            this.AddFunction(new Function("Replace", "Replaces a string in a string", Replace,
                new List<Argument> {new Argument("oldString", "old string"), new Argument("newString", "new string")}));
        }

        public override string Name => "String";

        public override string Description => "Some functions to modify a string";

        private static string Replace(string input, List<Argument> arguments = null)
        {
            if (arguments == null || arguments.Count < 2)
            {
                return null;
            }

            return input.Replace(arguments.ElementAt(0).Value, arguments.ElementAt(1).Value);
        }
    }
}