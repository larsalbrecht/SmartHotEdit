using System;
using SmartHotEditPluginHost;
using SmartHotEditPluginHost.Model;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SmartHotEditCasePlugin
{
    /// <summary>
    /// Description of CasePlugin.
    /// </summary>
    [Export(typeof(SmartHotEditPluginHost.APlugin))]
    public class CasePlugin : APlugin
	{
		
		public CasePlugin(){
			this.addFunction(new Function("CamelToKebab", "Converts a text from camelCase to kebab-case", new Function.Transform(this.camelCaseToKebabCase)));
			this.addFunction(new Function("CamelToSnake", "Converts a text from camelCase to snake_case", new Function.Transform(this.camelCaseToSnakeCase)));
			
			this.addFunction(new Function("KebabToCamel", "Converts a text from kebab-case to camelCase", new Function.Transform(this.kebabCaseToCamelCase)));
			this.addFunction(new Function("KebabToSnake", "Converts a text from kebab-case to snake_case", new Function.Transform(this.kebabCaseToSnakeCase)));
			
			this.addFunction(new Function("SnakeToCamel", "Converts a text from snake_case to camelCase", new Function.Transform(this.snakeCaseToCamelCase)));
			this.addFunction(new Function("SnakeToKebab", "Converts a text from snake_case to kebab-case", new Function.Transform(this.snakeCaseToKebaCase)));
			
			this.addFunction(new Function("LowerCase", "Converts a text to lower case", new Function.Transform(this.toLowerCase)));
			this.addFunction(new Function("UpperCase", "Converts a text to upper case", new Function.Transform(this.toUpperCase)));
		}

		public override String getName(){
			return "Case";
		}
		
		public override String getDescription(){
			return "Some functions to modify the case";
		}

        String camelCaseToKebabCase(String input, List<Argument> arguments = null)
        {
			return string.Concat(input.Select((x,i) => i > 0 && char.IsUpper(x) ? "-" + x.ToString() : x.ToString()));
		}
		
		String camelCaseToSnakeCase(String input, List<Argument> arguments = null)
        {
			return string.Concat(input.Select((x,i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));
		}
		
		String kebabCaseToCamelCase(String input, List<Argument> arguments = null)
        {
			StringBuilder camel = new StringBuilder();
			String[] atoms = input.Split('-');
			foreach (String atom in atoms) {
				if (atom.Any()) {
					camel.Append(Char.ToUpper(atom[0]));
				}
				if (atom.Count() > 1) {
					camel.Append(atom.Substring(1).ToLower());
				}
			}
			return camel.ToString();
		}
		
		String kebabCaseToSnakeCase(String input, List<Argument> arguments = null)
        {
			return "";
		}
		
		String snakeCaseToCamelCase(String input, List<Argument> arguments = null)
        {
			StringBuilder camel = new StringBuilder();
			String[] atoms = input.Split('_');
			foreach (String atom in atoms) {
				if (atom.Any()) {
					camel.Append(Char.ToUpper(atom[0]));
				}
				if (atom.Count() > 1) {
					camel.Append(atom.Substring(1).ToLower());
				}
			}
			return camel.ToString();
		}
		
		String snakeCaseToKebaCase(String input, List<Argument> arguments = null)
        {
			return "";
		}
		
		String toLowerCase(String input, List<Argument> arguments = null)
        {
			return input.ToLower();
		}
		
		String toUpperCase(String input, List<Argument> arguments = null)
        {
			return input.ToUpper();
		}
		
		public static string lcfirst(string str) {
			if(String.IsNullOrWhiteSpace(str) || char.IsLower(str, 0)) {
				return str;
			} else {
				if(str.Length == 1) {
					return str.ToLower();
				} else {
					return char.ToLower(str[0]).ToString() + str.Substring(1);
				}
			}
		}
		
	}
}
