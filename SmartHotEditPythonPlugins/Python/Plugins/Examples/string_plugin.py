from baseplugin import *


class PythonStringPlugin(Plugin.APlugin):
    
    def __init__(self):
        super(PythonStringPlugin, self).__init__("PythonString", "Some function to modify a string")
        replace_string_func = Models.Function("Replace", "Replaces a string in a string",
			PythonStringPlugin.replace_string,
			[
				Models.Argument("oldString", "old string"),
				Models.Argument("newString", "new string")
			])
        self.add_function(replace_string_func)

    @staticmethod
    def replace_string(value, arguments):
        if isinstance(value, basestring):
            if type(arguments) is dict and len(arguments) == 2:
                return value.replace(arguments["oldString"], arguments["newString"])
            else:
                print "Wrong arguments (must be a dictionary with 2 elements)"
        else:
            print "Wrong value (must string) given"

plugin = PythonStringPlugin()