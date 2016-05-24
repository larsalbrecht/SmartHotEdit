using System.Collections.Generic;
using MoonSharp.Interpreter;
using SmartHotEditPluginHost.Model;

namespace SmartHotEditLuaPlugins.Model
{
    public class LuaFunction
    {
        private readonly Closure closureToCall;

        private readonly Function func;

        public LuaFunction(string name, string description, Closure closure, List<Argument> arguments)
        {
            this.closureToCall = closure;
            this.func = new Function(name, description, closureCall, arguments);
        }

        private string closureCall(string input, List<Argument> arguments = null)
        {
            return this.closureToCall.Call(null, input, arguments).String;
        }

        public Function getFunction()
        {
            return this.func;
        }
    }
}