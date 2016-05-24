using System;
using System.Collections.Generic;

namespace SmartHotEditPluginHost.Model
{
    /// <summary>
    /// Description of Function.
    /// </summary>
    public class Function
    {
        public String Name;
        public String Description;
        public List<Argument> arguments;
        public delegate String Transform(String s, List<Argument> arguments = null);
        public Transform transform;

        public Function(String name, String description, Transform transform, List<Argument> arguments = null)
        {
            this.Name = name;
            this.Description = description;
            this.transform = transform;
            this.arguments = arguments;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public String processFunction(String value, List<Argument> arguments)
        {
            try
            {
                return this.transform(value, arguments);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}
