# Kontrol 2 by Nedd.
Remote Administration Tool written in DotNet C#.

# Features
- Process Manager
- File manager
- Remote Shell
- Download/Upload files (partially works)
- Remote Camera
- Remote Audio/Mic
- Remote Desktop
- Compile C# script on the fly
- Fun Menu (Very fun!!)
- More...

# Still in development
The program is mostly functional but needs polishing. As of now I don't feel like working on it, infact I want to re-write it entirley.

# Build instruction
Requires Visual Studio 2019 or higher with DotNet 6 Installed. 
Just open the solution file and hit build, VS will automatically do the rest for you.
You do not need a conf.t file anymore. The settings of the client are stored in the Settings class in ```Program.cs```.

You can also use the builder but you're goning to need to compile the client as a dummy (basically a stub) and then ZIP compress all the binaries and place it in K2's `Data/SelfContained.k2` folder as `dummy.rte` (yes, it is a reference to cc). If `Data` and `SelfContained.k2` folders don't exist then create them yourself.
Make sure that you compiled the client as Self Contained! Runtime dependant or single file self contained methods do not work for now.
