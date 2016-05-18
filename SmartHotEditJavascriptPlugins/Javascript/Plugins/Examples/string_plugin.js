"use strict";
class StringPlugin extends APlugin {
	constructor(){
		super("JSString", "Some functions to modify a string");
		
		var replaceStringFunc = new Function('Replace', 'Replaces a string in a string', this.replaceString, [new Argument("oldString", "old string"), new Argument("newString", "new string")]);
		this.addFunction(replaceStringFunc);
	}
	
	replaceString(value, argumentList){
		if(value && argumentList && argumentList.length === 2){
			return value.replace(argumentList[0].value, argumentList[1].value);
		}
		return value;
	}
};
var plugin = new StringPlugin();