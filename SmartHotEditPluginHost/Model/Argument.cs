using System;

using MoonSharp.Interpreter;

namespace SmartHotEditPluginHost.Model
{
    [MoonSharpUserData]
    public class Argument
    {

        public string Value;
        
        public string Key;
        public string Description;
        
        public Argument(string key, string description){
        	this.Key = key;
        	this.Description = description;
        }
    }
}
