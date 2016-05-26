using System.Collections.Generic;
using System.Linq;
using SmartHotEditPluginHost.Helper;
using SmartHotEditPluginHost.Model;
using SmartHotEditPluginHost.Properties;

namespace SmartHotEditPluginHost
{
    public abstract class APlugin : IPlugin
    {
        private readonly List<Function> _functionList = new List<Function>();
        public string Type = null;

        public bool Enabled
        {
            get { return (bool) Settings.Default[this.GetPropertynameForEnablePlugin()]; }
            set
            {
                Settings.Default[this.GetPropertynameForEnablePlugin()] = value;
                Settings.Default.Save();
            }
        }

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
            return this._functionList.Cast<Function>().ToArray();
        }

        public void Init()
        {
            PropertyHelper.CreateProperty(this.GetPropertynameForEnablePlugin(), true, typeof(bool));
        }

        protected void AddFunction(Function function)
        {
            this._functionList.Add(function);
        }

        private string GetPropertynameForEnablePlugin()
        {
            return "Plugin" + this.Type + this.Name + "Enabled";
        }
    }
}