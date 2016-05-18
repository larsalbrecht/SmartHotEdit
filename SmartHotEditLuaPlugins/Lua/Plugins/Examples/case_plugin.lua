local pluginhelper = require "pluginhelper"
require "class"
require "baseplugin"


LuaCasePlugin = class(APlugin, function(base)
         APlugin.init(base, 'LuaCase', 'Some functions to modify the case with lua')  -- must init base!
         lowerCaseFunc = Function('Lower Case', 'Converts a text to lower case', base.toLowerCase)
         upperCaseFunc = Function('Upper Case', 'Converts a text to upper case', base.toUpperCase)
         
         base:addFunction(lowerCaseFunc)
         base:addFunction(upperCaseFunc)
      end)
      
function LuaCasePlugin:toLowerCase(input, arguments)
  return string.lower(input)
end

function LuaCasePlugin:toUpperCase(input, arguments)
  return string.upper(input)
end


plugin = LuaCasePlugin()
return plugin