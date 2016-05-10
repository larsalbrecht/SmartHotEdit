from baseplugin import *


class PythonCasePlugin(Plugin.APlugin):
    
    def __init__(self):
        super(PythonCasePlugin, self).__init__("PythonCase", "Some functions to modify the case with python")
        lowercase_func = Models.Function("Lower Case", "Converts a text to lower case", PythonCasePlugin.to_lowercase)
        uppercase_func = Models.Function("Upper Case", "Converts a text to upper case", PythonCasePlugin.to_uppercase)
        self.add_function(lowercase_func)
        self.add_function(uppercase_func)


    @staticmethod
    def to_lowercase(value, arguments):
        return value.lower()

    @staticmethod
    def to_uppercase(value, arguments):
        return value.upper()

plugin = PythonCasePlugin()