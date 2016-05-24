using System;
using System.Collections.Generic;
using SmartHotEditPluginHost.Model;
using System.Linq;

namespace SmartHotEditPluginHost
{
    public abstract class APlugin : IPlugin
    {

        protected List<Function> FunctionList = new List<Function>();

        public bool Enabled = true;
        public string Type = null;

        public abstract string Name { get; }

        public abstract string Description { get; }

        protected void AddFunction(Function function)
        {
            this.FunctionList.Add(function);
        }

        public string GetResultFromFunction(Function function, String value, List<Argument> arguments = null)
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

        public string GetPropertynameForEnablePlugin()
        {
            return "Plugin" + this.Type + this.Name+ "Enabled";
        }
    }
}
