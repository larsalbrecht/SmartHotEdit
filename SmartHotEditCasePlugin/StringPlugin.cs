using System;
using System.Linq;
using System.Collections.Generic;
using SmartHotEditPluginHost;
using SmartHotEditPluginHost.Model;
using System.ComponentModel.Composition;

namespace SmartHotEdit.Plugin
{
    /// <summary>
    /// Description of StringPlugin.
    /// </summary>
    [Export(typeof(SmartHotEditPluginHost.APlugin))]
    public class StringPlugin : APlugin
	{
		
		public StringPlugin(){
    		this.addFunction(new Function("Replace", "Replaces a string in a string", new Function.Transform(this.replace), new List<Argument> { new Argument("oldString", "old string"), new Argument("newString", "new string") }));
		}

		public override String getName(){
			return "String";
		}
		
		public override String getDescription(){
			return "Some functions to modify a string";
		}
		
		String replace(String input, List<Argument> arguments = null){
            if(arguments == null || arguments.Count < 2)
            {
                return null;
            }

            return input.Replace(arguments.ElementAt(0).value, arguments.ElementAt(1).value);
		}
		
	}
}
