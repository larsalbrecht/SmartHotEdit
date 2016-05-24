using SmartHotEditPluginHost;

namespace SmartHotEditLuaPlugins.Model
{
    internal class Plugin : APlugin
    {
        public string description;
        public string name;

        public Plugin()
        {
        }

        public Plugin(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public override string Description
        {
            get { return this.description; }
        }

        public override string Name
        {
            get { return this.name; }
        }

        public void addLuaFunction(LuaFunction function)
        {
            if (function != null && function.getFunction() != null)
            {
                AddFunction(function.getFunction());
            }
        }
    }
}