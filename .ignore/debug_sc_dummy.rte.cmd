@echo off
echo Compressing...
7z a ./dummy.rte "./Kontrol 2 Client/bin/x64/Debug/net6.0-windows/win-x64/*"
echo Moving to 'Data/SelfContained.k2'...
move "./dummy.rte" "./Kontrol 2 Server/bin/x64/Debug/net6.0-windows/win-x64/Data/SelfContained.k2/dummy.rte"
echo Done
cmd /k