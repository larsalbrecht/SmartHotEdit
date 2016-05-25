using System.Collections.Generic;
using MoonSharp.Interpreter;
using SmartHotEditPluginHost.Model;

namespace SmartHotEditLuaPlugins.Model
{
    public class LuaFunction
    {
        private readonly Closure _closureToCall;

        private readonly Function _func;

        public LuaFunction(string name, string description, Closure closure, List<Argument> arguments)
        {
            this._closureToCall = closure;
            this._func = new Function(name, description, ClosureCall, arguments);
        }

        private string ClosureCall(string input, List<Argument> arguments = null)
        {
            return this._closureToCall.Call(null, input, arguments).String;
        }

        public Function GetFunction()
        {
            return this._func;
        }
    }
}