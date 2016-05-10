import Models


class APlugin(object):
    
    def __init__(self, name, description):
        self.functions = []
        self.name = name
        self.description = description
    
    def add_function(self, function):
        if type(function) is Models.Function:
            self.functions.append(function)
        else:
            print("Tried to add a Function that is not a Function")
        return self

    def get_function(self, index):
        return self.functions[index]

    def get_functions(self):
        return self.functions

    def get_name(self):
        return self.name

    def get_description(self):
        return self.description
