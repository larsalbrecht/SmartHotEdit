local pluginhelper ={
  _VERSION = 'pluginhelper.lua 1.0.0',
  _DESCRIPTION = 'helper for the SmartHotEdit Lua Plugins',
  _LICENSE = [[
    Copyright (c) 2016 Lars Albrecht
  ]]
}


-- Transforms the userdata arguments to a lua table with keys.
-- Returns a table with argument objects and with keys.
function pluginhelper.getArgumentsAsArray(arguments)
	result = {}
    
	if arguments ~= nil and type(arguments) == 'userdata' then
		for i=0,arguments.Count-1 do
			arg = arguments[i];
			--print('Key: ' .. arg.key)
			result[arg.key] = arg
		end
	end                    
	return result
end

function pluginhelper.print_r ( t )  
    local print_r_cache={}
    local function sub_print_r(t,indent)
        if (print_r_cache[tostring(t)]) then
            print(indent.."*"..tostring(t))
        else
            print_r_cache[tostring(t)]=true
            if (type(t)=="table") then
                for pos,val in pairs(t) do
                    if (type(val)=="table") then
                        print(indent.."["..pos.."] => "..tostring(t).." {")
                        sub_print_r(val,indent..string.rep(" ",string.len(pos)+8))
                        print(indent..string.rep(" ",string.len(pos)+6).."}")
                    elseif (type(val)=="string") then
                        print(indent.."["..pos..'] => "'..val..'"')
                    else
                        print(indent.."["..pos.."] => "..tostring(val))
                    end
                end
            else
                print(indent..tostring(t))
            end
        end
    end
    if (type(t)=="table") then
        print(tostring(t).." {")
        sub_print_r(t,"  ")
        print("}")
    else
        sub_print_r(t,"  ")
    end
    print()
end

--setmetatable(pluginhelper, { __call = function(_, ...) return pluginhelper.inspect(...) end })

return pluginhelper