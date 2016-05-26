"use strict";

class MyPlugin extends APlugin {
    constructor() {
        super("<Pluginname>", "<Description>");

        const funcName = new Function("<Function-Name>", "<Function-Description>", this.myFunction);
        this.addFunction(funcName);
    }
    myFunction(value) {
        return value;
    }
};

var plugin = new MyPlugin();