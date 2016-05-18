using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using SmartHotEditPluginHost.Model;

namespace SmartHotEditLuaPlugins.Model
{
    public class LuaFunction
    {

        private Function func;
        Closure closureToCall;

        public LuaFunction(String name, String description, Closure closure, List<Argument> arguments)
        {
            this.closureToCall = closure;
            this.func = new Function(name, description, new Function.Transform(closureCall), arguments);
        }

        private String closureCall(String input, List<Argument> arguments = null)
        {
            return this.closureToCall.Call(null, input, arguments).String;
        }

        public Function getFunction()
        {
            return this.func;
        }
    }
}
