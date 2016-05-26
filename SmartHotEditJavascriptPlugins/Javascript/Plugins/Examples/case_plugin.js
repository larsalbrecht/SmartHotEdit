"use strict";

class CasePlugin extends APlugin {
    constructor() {
        super("JSCase", "Simple plugin to change the case");

        const lowerCaseFunc = new Function("Lower Case", "Converts a text to lower case", this.to_lowercase);
        const upperCaseFunc = new Function("Upper Case", "Converts a text to upper case", this.to_uppercase);
        this.addFunction(lowerCaseFunc);
        this.addFunction(upperCaseFunc);
    }
    to_lowercase(value) {
        if (value) {
            return value.toLowerCase();
        }
        return null;
    }
    to_uppercase(value) {
        if (value) {
            return value.toUpperCase();
        }
        return null;
    }
};

var plugin = new CasePlugin();