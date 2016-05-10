using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using SmartHotEditPluginHost.Model;

namespace SmartHotEdit.Model.Python
{
    public class PythonFunction
    {

        private Function func;
        private dynamic pythonFuncToCall;

        public PythonFunction(String name, String description, IronPython.Runtime.PythonFunction pythonFuncToCall, List<Argument> arguments)
        {
            this.pythonFuncToCall = pythonFuncToCall;
            this.func = new Function(name, description, new Function.Transform(closureCall), arguments);
        }

        private String closureCall(String input, List<Argument> arguments = null)
        {
            IronPython.Runtime.PythonDictionary pythonArgs = null;
            if(arguments != null)
            {
                pythonArgs = new IronPython.Runtime.PythonDictionary();
                foreach (Argument arg in arguments)
                {
                    pythonArgs.Add(arg.key, arg.value);
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
