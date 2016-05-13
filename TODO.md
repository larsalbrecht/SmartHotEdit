# SmartHotEdit ToDos

## Features
New features
### JavaScript Plugin / Lua Plugin
* Arguments must be given as map (JavaScript: JSON-Object, Lua: Table), so the script can use the argument key to get the value (see Python Plugin)

### General
* Add method to set if the function will be called on every line (currently) or for the whole text (simple question before function will be executed?)

### General Pluginbehavior
* Better error detection with disabling the *PluginController OR the function only
* Let the user enable or disable functions of loaded plugins
* Show errormessages if script is invalid (like indent errors in python)
* Add a testform for plugins: New menupoint "Test Plugin"; Add a textarea, so the user can choose a plugintype and paste the code in it. Then the user can click "run test" and the script will be loaded.
 * The user also can call the application with a parameter to only test a given file. Something like: *.exe -file=<path-to-file> -type=<Lua|JavaScript|Python>

## Bugs
All known issues
### Settings
* HotKey-Field is "None", must be filled with "current" HotKey.

## Refactoring
All things that must be refactored
### *PluginController
*PluginController can be more abstract