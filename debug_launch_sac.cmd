@echo off
powershell "Start-Process -FilePath '.\Kontrol 2 Server\bin\x64\Debug\net6.0-windows\win-x64\k2s.exe' ; Set-Location -Path '.\Kontrol 2 Client\bin\x64\Debug\net6.0-windows\win-x64\' ; .\k2c.exe" 
cmd /k
