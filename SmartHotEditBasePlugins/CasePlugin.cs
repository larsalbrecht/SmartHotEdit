using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using SmartHotEditPluginHost;
using SmartHotEditPluginHost.Model;

namespace SmartHotEditBasePlugins
{
    /// <summary>
    ///     Description of CasePlugin.
    /// </summary>
    [Export(typeof(APlugin))]
    public class CasePlugin : APlugin
    {
        public CasePlugin()
        {
            this.AddFunction(new Function("CamelToKebab", "Converts a text from camelCase to kebab-case",
                CamelCaseToKebabCase));
            this.AddFunction(new Function("CamelToSnake", "Converts a text from camelCase to snake_case",
                CamelCaseToSnakeCase));

            this.AddFunction(new Function("KebabToCamel", "Converts a text from kebab-case to camelCase",
                KebabCaseToCamelCase));
            this.AddFunction(new Function("KebabToSnake", "Converts a text from kebab-case to snake_case",
                KebabCaseToSnakeCase));

            this.AddFunction(new Function("SnakeToCamel", "Converts a text from snake_case to camelCase",
                SnakeCaseToCamelCase));
            this.AddFunction(new Function("SnakeToKebab", "Converts a text from snake_case to kebab-case",
                SnakeCaseToKebaCase));

            this.AddFunction(new Function("LowerCase", "Converts a text to lower case", ToLowerCase));
            this.AddFunction(new Function("UpperCase", "Converts a text to upper case", ToUpperCase));

            this.AddFunction(new Function("LCFirst", "Converts the first character of a text to lower case", Lcfirst));
        }

        public override string Name => "Case";

        public override string Description => "Some functions to modify the case";

        private static string CamelCaseToKebabCase(string input, List<Argument> arguments = null)
        {
            return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x.ToString() : x.ToString()));
        }

        private static string CamelCaseToSnakeCase(string input, List<Argument> arguments = null)
        {
            return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));
        }

        private static string KebabCaseToCamelCase(string input, List<Argument> arguments = null)
        {
            var camel = new StringBuilder();
            var atoms = input.Split('-');
            foreach (var atom in atoms)
            {
                if (atom.Any())
                {
                    camel.Append(char.ToUpper(atom[0]));
                }
                if (atom.Length > 1)
                {
                    camel.Append(atom.Substring(1).ToLower());
                }
            }
            return camel.ToString();
        }

        private static string KebabCaseToSnakeCase(string input, List<Argument> arguments = null)
        {
            return "";
        }

        private static string SnakeCaseToCamelCase(string input, List<Argument> arguments = null)
        {
            var camel = new StringBuilder();
            var atoms = input.Split('_');
            foreach (var atom in atoms)
            {
                if (atom.Any())
                {
                    camel.Append(char.ToUpper(atom[0]));
                }
                if (atom.Length > 1)
                {
                    camel.Append(atom.Substring(1).ToLower());
                }
            }
            return camel.ToString();
        }

        private static string SnakeCaseToKebaCase(string input, List<Argument> arguments = null)
        {
            return "";
        }

        private static string ToLowerCase(string input, List<Argument> arguments = null)
        {
            return input.ToLower();
        }

        private static string ToUpperCase(string input, List<Argument> arguments = null)
        {
            return input.ToUpper();
        }

        private static string Lcfirst(string input, List<Argument> arguments = null)
        {
            if (string.IsNullOrWhiteSpace(input) || char.IsLower(input, 0))
            {
                return input;
            }
            if (input.Length == 1)
            {
                return input.ToLower();
            }
            return char.ToLower(input[0]) + input.Substring(1);
        }
    }
}