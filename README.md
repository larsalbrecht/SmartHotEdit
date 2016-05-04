# SmartHotEdit
Edit text from clipboard with smart functions. 


## Plugins
SmartHotEdit can handle plugins to add support for custom functionality.
### C*#*
You can write plugins in C#. For this, there is a small project (SmartHotEditPluginHost) with the needed classes.
#### Example Plugins
There are some examples in the project "SmartHotEditBasePlugins":
* CasePlugin
* StringPlugin

#### Short implementation description
You need to extend from APlugin (living in SmartHotEditPluginHost) and implement the needed methods (getName, getDescription). Then you only need to create functions (class that live in SmartHotEditPluginHost.Model) and add them with `` this.addFunction(Function function) ``. 
After that, you must decorate your class with `` [Export(typeof(SmartHotEditPluginHost.APlugin))] ``.
Thats it.

The generated *.dll-files must saved near the executable.

#### Example Code
```C#
using System;
using System.Linq;
using System.Collections.Generic;
using SmartHotEditPluginHost;
using SmartHotEditPluginHost.Model;
using System.ComponentModel.Composition;

namespace SmartHotEditBasePlugins {
    /// <summary>
    /// Description of StringPlugin.
    /// </summary>
    [Export(typeof(SmartHotEditPluginHost.APlugin))]
    public class StringPlugin : APlugin {

        public StringPlugin() {
            this.addFunction(
                new Function(
                    "Replace", 
                    "Replaces a string in a string", 
                    new Function.Transform(this.replace), 
                    new List<Argument> {
                        new Argument("oldString", "old string"),
                        new Argument("newString", "new string")
                    }
                )
            );
        }

        public override String getName() {
            return "String";
        }

        public override String getDescription() {
            return "Some functions to modify a string";
        }

        String replace(String input, List<Argument> arguments = null) {
            if (arguments == null || arguments.Count < 2) {
                return null;
            }

            return input.Replace(
                arguments.ElementAt(0).value, 
                arguments.ElementAt(1).value
            );
        }
    }
}
```
### LUA
You can write plugins with LUA. For this, there is a helper library you need to require to your script.
#### Example Plugins
There are some example in the directory ``Lua\Plugins\Examples`` in the project "SmartHotEditLuaPlugins":
* case_plugin.lua
* string_plugin.lua
 
#### Short implementation description
You need to create a class that extends from APlugin. For this, you must require ``class`` and ``baseplugin``. To help with some conventions, you can also import the ``pluginhelper``.
In your class you must call the ``APlugin:init`` method with ``base, <name>, <description>``.
After that, you can add your functions to the base class with ``base:addFunction(Function function)``.
The script must return a new instance of your plugin:
`` return MyPlugin() ``.

Your scripts must saved in the directory ``Lua\Plugins`` and must end with ``_plugin.lua``: ``example_plugin.lua``

#### Example Code
```Lua
local pluginhelper = require "pluginhelper"
require "class"
require "baseplugin"

LuaStringPlugin = class(APlugin, function(base)
  APlugin.init(base, 'LuaString', 'Some functions to modify a string')  -- must init base!
  replaceStringFunc = Function('Replace', 'Replaces a string in a string', base.replaceString, {Argument("oldString", "old string"), Argument("newString", "new string")})
   
  base:addFunction(replaceStringFunc)
end)
      
function LuaStringPlugin:replaceString(input, arguments)
	if arguments ~= nil and #arguments == 2 then
		result = string.gsub(input, arguments[0].value, arguments[1].value)
		return result
	else
		print("Nothing replaced, you need 2 arguments")
	end
	return input
end


plugin = LuaStringPlugin()
return plugin
```
