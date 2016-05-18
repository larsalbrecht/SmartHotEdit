from baseplugin import *


class MyPlugin(Plugin.APlugin):
    
    def __init__(self):
        super(MyPlugin, self).__init__("<Pluginname>", "<Description>")
        func_name = Models.Function("<Function-Name>", "<Function-Description>", MyPlugin.my_function)
        self.add_function(func_name)

    @staticmethod
    def my_function(value, arguments):
        return value

plugin = MyPlugin()
