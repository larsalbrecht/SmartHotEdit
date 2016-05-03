using System;

namespace SmartHotEditPluginHost.Model
{
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
