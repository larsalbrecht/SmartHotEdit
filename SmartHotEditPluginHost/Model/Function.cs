using System.Collections.Generic;

namespace SmartHotEditPluginHost.Model
{
    /// <summary>
    ///     Description of Function.
    /// </summary>
    public class Function
    {
        public delegate string Transform(string s, List<Argument> arguments = null);

        private readonly Transform _transformFunction;

        public readonly List<Argument> Arguments;
        public readonly string Description;
        public readonly string Name;

        public Function(string name, string description, Transform transformFunction, List<Argument> arguments = null)
        {
            this.Name = name;
            this.Description = description;
            this._transformFunction = transformFunction;
            this.Arguments = arguments;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public string ProcessFunction(string value, List<Argument> arguments)
        {
            return this._transformFunction(value, arguments);
        }
    }
}