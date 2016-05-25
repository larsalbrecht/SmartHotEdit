using SmartHotEditPluginHost;
using SmartHotEditPluginHost.Model;

namespace SmartHotEditJavascriptPlugins.Model
{
    internal class Plugin : APlugin
    {

        public override string Description { get; }

        public override string Name { get; }

        public Plugin(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public new void AddFunction(Function function)
        {
            base.AddFunction(function);
        }
    }
}