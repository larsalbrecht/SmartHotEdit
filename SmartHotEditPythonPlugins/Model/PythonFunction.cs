using System.Collections.Generic;
using IronPython.Runtime;
using SmartHotEditPluginHost.Model;

namespace SmartHotEditPythonPlugins.Model
{
    public class PythonFunction
    {
        private readonly Function _func;
        private readonly dynamic _pythonFuncToCall;

        public PythonFunction(string name, string description, IronPython.Runtime.PythonFunction pythonFuncToCall,
            List<Argument> arguments)
        {
            this._pythonFuncToCall = pythonFuncToCall;
            this._func = new Function(name, description, ClosureCall, arguments);
        }

        private string ClosureCall(string input, List<Argument> arguments = null)
        {
            if (arguments == null) return this._pythonFuncToCall(input, (PythonDictionary) null);

            var pythonArgs = new PythonDictionary();
            foreach (var arg in arguments)
            {
                pythonArgs.Add(arg.Key, arg.Value);
            }


            return this._pythonFuncToCall(input, pythonArgs);
        }

        public Function GetFunction()
        {
            return this._func;
        }
    }
}