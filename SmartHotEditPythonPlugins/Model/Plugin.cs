using SmartHotEditPluginHost;

namespace SmartHotEditPythonPlugins.Model
{
    internal class Plugin : APlugin
    {
        public Plugin(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public override string Description { get; }
        public override string Name { get; }

        public void AddPythonFunction(PythonFunction function)
        {
            if (function?.GetFunction() != null)
            {
                AddFunction(function.GetFunction());
            }
        }
    }
}