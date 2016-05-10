using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartHotEditPluginHost.Model;

namespace SmartHotEditPluginHost
{
    public interface IPlugin
    {

        String getName();
        String getDescription();
        String getResultFromFunction(Function function, String value, List<Argument> arguments = null);
        Function[] getFunctionsAsArray();


    }
}
