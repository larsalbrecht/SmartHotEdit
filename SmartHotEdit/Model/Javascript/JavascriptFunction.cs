using SmartHotEditPluginHost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SmartHotEditPluginHost.Model.Function;

namespace SmartHotEdit.Model.Javascript
{
    class JavascriptFunction
    {

        private Function func;
        private dynamic javascriptFuncToCall;

        public JavascriptFunction(String name, String description, Transform transform, List<Argument> arguments)
        {
            //this.javascriptFuncToCall = javascriptFuncToCall;
            this.func = new Function(name, description, new Function.Transform(closureCall), arguments);
        }

        private String closureCall(String input, List<Argument> arguments = null)
        {
            return null;
        }

        public Function getFunction()
        {
            return this.func;
        }

    }
}
