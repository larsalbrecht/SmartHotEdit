local pluginhelper = require "pluginhelper"
require "class"
require "baseplugin"


MyPlugin = class(APlugin, function(base)
         APlugin.init(base, '<Pluginname>', '<Description>')
         funcName = Function('<Function-Name>', '<Function-Description>', base.myFunction)
         
         base:addFunction(funcName)
      end)
      
function MyPlugin:myFunction(input, arguments)
  return input
end


plugin = MyPlugin()
return plugin