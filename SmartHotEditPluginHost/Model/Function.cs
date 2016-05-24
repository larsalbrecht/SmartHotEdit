using System;
using System.Collections.Generic;

namespace SmartHotEditPluginHost.Model
{
    /// <summary>
    /// Description of Function.
    /// </summary>
    public class Function
    {
        public string Name;
        public string Description;
        public List<Argument> Arguments;
        public delegate String Transform(String s, List<Argument> arguments = null);
        public Transform TransformFunction;

        public Function(String name, String description, Transform transformFunction, List<Argument> arguments = null)
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
