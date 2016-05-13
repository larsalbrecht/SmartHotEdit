using SmartHotEditPluginHost;
using SmartHotEditPluginHost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHotEdit.Model.Javascript
{
    class Plugin : APlugin
    {
        public String name;
        public String description;

        public Plugin() { }

        public Plugin(String name, String description)
        {
            this.name = name;
            this.description = description;
        }

        public override string getDescription()
        {
            return this.description;
        }

        public override string getName()
        {
            return this.name;
        }

        public new void addFunction(Function function)
        {
            base.addFunction(function);
        }
    }
}
