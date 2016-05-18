"use strict";
class MyPlugin extends Plugin {
    constructor() {
        super("<Pluginname>", "<Description>");

        var funcName = new Function('<Function-Name>', '<Function-Description>', this.myFunction);
        this.addFunction(funcName);
    }

    myFunction(value, argumentList) {
        return value;
    }
};
var plugin = new MyPlugin();