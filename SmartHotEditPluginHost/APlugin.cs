using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartHotEditPluginHost.Model;
using System.Linq;

namespace SmartHotEditPluginHost
{
    abstract public class APlugin : IPlugin
    {

    	protected List<Function> functionList = new List<Function>();

        public abstract String getName();
        public abstract String getDescription();
        
        protected void addFunction(Function function){
        	this.functionList.Add(function);
        }

        public String getResultFromFunction(Function function, String value, List<Argument> arguments = null)
        {
            String result = "";
            if (value != null)
            {
                result = function.processFunction(value, arguments);
            }
            return result;
        }

        public Function[] getFunctionsAsArray()
        {
            return this.functionList.Cast<Function>().ToArray();
        }
    }
}
