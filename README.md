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
Make sure to have a `conf.t` configuration file in the client's binary folder.
The format is as follows:
```
127.0.0.1;7878;show
```
- `127.0.0.1` is where you type the server's IP.
- `7878` is the default port the server operates on.
- `show` shows the console window, `hide` to hide it. 
