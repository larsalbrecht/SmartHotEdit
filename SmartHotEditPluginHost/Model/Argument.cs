using MoonSharp.Interpreter;

namespace SmartHotEditPluginHost.Model
{
    [MoonSharpUserData]
    public class Argument
    {
        public string Description;

        public string Key;

        public string Value;

        public Argument(string key, string description)
        {
            this.Key = key;
            this.Description = description;
        }
    }
}