@echo off
setlocal
pushd "%~dp0Axes in Motion Server"
title Axes in Motion SQL Server API
set AXES_IN_MOTION_SERVER_URLS=http://localhost:5080
dotnet build "Axes in Motion Server.csproj" -p:OutputPath=bin\SqlServerRuntimeFinal\ -p:UseAppHost=false
if errorlevel 1 goto buildFailed
echo Server URL: http://localhost:5080
dotnet "bin\SqlServerRuntimeFinal\Axes in Motion Server.dll"
popd
endlocal
exit /b

:buildFailed
echo The server build failed. Close any running server window and try again.
pause
popd
endlocal
exit /b 1
