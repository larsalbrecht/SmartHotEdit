using System;

using MoonSharp.Interpreter;

namespace SmartHotEditPluginHost.Model
{
    [MoonSharpUserData]
    public class Argument
    {

        public String value;
        
        public String key;
        public String description;
        
        public Argument(String key, String description){
        	this.key = key;
        	this.description = description;
        }
    }
}
