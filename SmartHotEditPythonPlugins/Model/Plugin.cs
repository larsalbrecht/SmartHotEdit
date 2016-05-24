﻿using SmartHotEditPluginHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHotEditPythonPlugins.Model
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

        public override string Description
        {
            get
            {
                return this.description;
            }
        }

        public override string Name
        {
            get
            {
                return this.name;
            }
        }

        public void addPythonFunction(PythonFunction function)
        {
            if (function != null && function.getFunction() != null)
            {
                base.AddFunction(function.getFunction());
            }
        }
    }
}
