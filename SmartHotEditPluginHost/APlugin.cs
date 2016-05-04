using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartHotEditPluginHost.Model;

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

        public ListBox.ObjectCollection getFunctionsAsCollection(ListBox lb)
        {
            var collection = new ListBox.ObjectCollection(lb);

            foreach (Function myFunction in this.functionList)
            {
                collection.Add(myFunction);
            }

            return collection;
        }
    }
}
