using System.Collections.Generic;

namespace SmartHotEditPluginHost.Model
{
    /// <summary>
    ///     Description of Function.
    /// </summary>
    public class Function
    {
        public delegate string Transform(string s, List<Argument> arguments = null);

        public List<Argument> Arguments;
        public string Description;
        public string Name;
        public Transform TransformFunction;

        public Function(string name, string description, Transform transformFunction, List<Argument> arguments = null)
        {
            this.Name = name;
            this.Description = description;
            this.TransformFunction = transformFunction;
            this.Arguments = arguments;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public string ProcessFunction(string value, List<Argument> arguments)
        {
            return this.TransformFunction(value, arguments);
        }
    }
}