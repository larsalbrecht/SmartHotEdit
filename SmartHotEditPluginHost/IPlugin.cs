using System.Collections.Generic;
using SmartHotEditPluginHost.Model;

namespace SmartHotEditPluginHost
{
    public interface IPlugin
    {
        string Name { get; }

        string Description { get; }

        string GetResultFromFunction(Function function, string value, List<Argument> arguments = null);
        Function[] GetFunctionsAsArray();
    }
}