using SmartHotEditPluginHost;

namespace SmartHotEditLuaPlugins.Model
{
    internal class Plugin : APlugin
    {
        // ReSharper disable once InconsistentNaming
        public string description;
        // ReSharper disable once InconsistentNaming
        public string name;

        public override string Description => this.description;

        public override string Name => this.name;

        public void AddLuaFunction(LuaFunction function)
        {
            if (function?.GetFunction() != null)
            {
                AddFunction(function.GetFunction());
            }
        }
    }
}