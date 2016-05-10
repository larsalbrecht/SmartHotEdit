class Argument(object):
    def __init__(self, key, description):
        self.key = key
        self.description = description
        self.value = None
        pass


class Function(object):

    def __init__(self, name, description, called_function, arguments=None):
        self.name = name
        self.description = description
        self.called_function = called_function
        self.arguments = arguments
        pass

    def process(self, value, arguments=dict):
        return self.called_function(value, arguments)

    def get_called_function(self):
        return self.called_function
