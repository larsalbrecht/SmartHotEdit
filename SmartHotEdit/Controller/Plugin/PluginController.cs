using SmartHotEditPluginHost;
using System;


namespace SmartHotEdit.Controller.Plugin
{
    public class PluginController
    {
        private DefaultPluginController defaultPluginController;
        private LuaPluginController luaPluginController;

        private APlugin[] plugins = null;

        public PluginController()
        {
            this.defaultPluginController = new DefaultPluginController();
            this.luaPluginController = new LuaPluginController();
            this.plugins = new APlugin[0];

            this.loadPlugins();
            System.Diagnostics.Debug.WriteLine("Plugins found: " + this.plugins.Length);
        }

        public void loadPlugins()
        {
            this.plugins = this.arrayMerge(this.plugins, this.defaultPluginController.getPlugins());
            this.plugins = this.arrayMerge(this.plugins, this.luaPluginController.getPlugins());
        }

        private APlugin[] arrayMerge(APlugin[] baseArray, APlugin[] arrayToMerge)
        {
            int originalLength = baseArray.Length;
            APlugin[] luaPlugins = luaPluginController.getPlugins();
            Array.Resize(ref baseArray, originalLength + arrayToMerge.Length);
            Array.Copy(arrayToMerge, 0, baseArray, originalLength, arrayToMerge.Length);

            return baseArray;
        }
        
        public APlugin[] getPlugins()
        {
            return this.plugins;
        }

    }
}
