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