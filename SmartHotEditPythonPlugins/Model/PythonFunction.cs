using System.Collections.Generic;
using IronPython.Runtime;
using SmartHotEditPluginHost.Model;

namespace SmartHotEditPythonPlugins.Model
{
    public class PythonFunction
    {
        private readonly Function func;
        private readonly dynamic pythonFuncToCall;

        public PythonFunction(string name, string description, IronPython.Runtime.PythonFunction pythonFuncToCall,
            List<Argument> arguments)
        {
            this.pythonFuncToCall = pythonFuncToCall;
            this.func = new Function(name, description, closureCall, arguments);
        }

        private string closureCall(string input, List<Argument> arguments = null)
        {
            PythonDictionary pythonArgs = null;
            if (arguments != null)
            {
                pythonArgs = new PythonDictionary();
                foreach (var arg in arguments)
                {
                    pythonArgs.Add(arg.Key, arg.Value);
                }
            }


            return this.pythonFuncToCall(input, pythonArgs);
        }

        public Function getFunction()
        {
            return this.func;
        }
    }
}