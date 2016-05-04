require "class"

---------------
-- Class Argument
Argument = class(function(arg, key, description)
	arg.key = key
	arg.description = description
	arg.value = nil
end)


---------------

---------------
-- Class Function
Function = class(function(func, name, description, calledFunction, arguments)
  func.name = name
  func.description = description
  func.arguments = arguments
  func.calledFunction = calledFunction

end)

function Function:process(value, arguments)
  return self:calledFunction(value, arguments)
end

---------------
-- Class APlugin
APlugin = class(function(plugin, name, description)
  plugin.name = name
  plugin.description = description
  plugin.functions = {}
  return plugin
end)

-- Add functions to a list
function APlugin:addFunction(func)
  if func:is_a(Function) then
    table.insert(self.functions, func)
  else
    print('Tried to add a Function that is not a Function!')
  end
end

function APlugin:getFunction(index)
  return self.functions[index]
end

function APlugin:getFunctions(func)
  return self.functions
end