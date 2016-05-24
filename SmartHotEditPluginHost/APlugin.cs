using System.Collections.Generic;
using System.Linq;
using SmartHotEditPluginHost.Model;

namespace SmartHotEditPluginHost
{
    public abstract class APlugin : IPlugin
    {
        public bool Enabled = true;

        protected List<Function> FunctionList = new List<Function>();
        public string Type = null;

        public abstract string Name { get; }

        public abstract string Description { get; }

        public string GetResultFromFunction(Function function, string value, List<Argument> arguments = null)
        {
            var result = value;
            if (value != null)
            {
                result = function.ProcessFunction(value, arguments);
            }

            return result;
        }

        public Function[] GetFunctionsAsArray()
        {
            return this.FunctionList.Cast<Function>().ToArray();
        }

        protected void AddFunction(Function function)
        {
            this.FunctionList.Add(function);
        }

        public string GetPropertynameForEnablePlugin()
        {
            return "Plugin" + this.Type + this.Name + "Enabled";
        }
    }
}