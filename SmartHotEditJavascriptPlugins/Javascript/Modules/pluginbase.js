"use strict";

export class Argument {
    constructor(key, description) {
        this.key = key;
        this.description = description;
        this.value = null;
    }
}

export class Function {
    constructor(name, description, calledFuction, argumentList) {
        this.name = name;
        this.description = description;
        this.calledFuction = calledFuction;
        this.argumentList = argumentList;
    }
    process(value, argumentList) {
        return this.calledFuction(value, argumentList);
    }
}

export class APlugin {
    constructor(name, description) {
        this.name = name;
        this.description = description;
        this.functions = [];
    }
    addFunction(customFunction) {
        if (customFunction instanceof Function) {
            this.functions.push(customFunction);
        } else {
            console.log("Tried to add a Function that is not a Function");
        }
    }
    getFunction(index) {
        return this.functions[index];
    }
    getFunctions() {
        return this.functions;
    }
}