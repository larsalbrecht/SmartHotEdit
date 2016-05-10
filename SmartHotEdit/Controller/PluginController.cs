using NLog;
using SmartHotEditPluginHost;
using System;
using SmartHotEdit.Controller.Plugin;


namespace SmartHotEdit.Controller
{
    // TODO let the user disable different plugin loader
    public class PluginController
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        private DefaultPluginController defaultPluginController;
        private LuaPluginController luaPluginController;
        private PythonPluginController pythonPluginController;

        private APlugin[] plugins = null;

        public PluginController()
        {
            logger.Trace("Construct PluginController");
            this.plugins = new APlugin[0];

            this.loadPlugins();
            logger.Debug("Plugins found: " + this.plugins.Length);
        }

        public void loadPlugins()
        {
            logger.Trace("Get Plugins from *PluginController");
            logger.Trace("Use plugins: " + Properties.Settings.Default.EnablePlugins);
            if (Properties.Settings.Default.EnablePlugins)
            {
                logger.Trace("Use Default plugins: " + Properties.Settings.Default.EnableDefaultPlugins);
                logger.Trace("Use Lua plugins: " + Properties.Settings.Default.EnableLuaPlugins);

                if (Properties.Settings.Default.EnableDefaultPlugins)
                {
                    this.defaultPluginController = new DefaultPluginController(this);
                    this.plugins = this.arrayMerge(this.plugins, this.defaultPluginController.getPlugins());
                }
                if (Properties.Settings.Default.EnableLuaPlugins)
                {
                    this.luaPluginController = new LuaPluginController(this);
                    this.plugins = this.arrayMerge(this.plugins, this.luaPluginController.getPlugins());
                }
                if (Properties.Settings.Default.EnablePythonPlugins)
                {
                    this.pythonPluginController = new PythonPluginController(this);
                    this.plugins = this.arrayMerge(this.plugins, this.pythonPluginController.getPlugins());
                }
            }
        }

        private APlugin[] arrayMerge(APlugin[] baseArray, APlugin[] arrayToMerge)
        {
            int originalLength = baseArray.Length;
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
