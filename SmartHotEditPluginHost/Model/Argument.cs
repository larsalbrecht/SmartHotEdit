namespace SmartHotEditPluginHost.Model
{
    public class Argument
    {
        public readonly string Description;

        public readonly string Key;

        public string Value;

        public Argument(string key, string description)
        {
            this.Key = key;
            this.Description = description;
        }
    }
}