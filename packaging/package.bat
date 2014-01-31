@echo off
set msbuild=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe
set nuget=..\src\.nuget\NuGet.exe
set nuspec=NServiceBus.SLR.nuspec
set deployLocation=nuget.ds.adp.com:80/api/v2/package
set nupackLocation=%~dp0\nuget
set configuration=Release
set deploy=false
set build=false


:Loop
IF "%~1"=="" ( GOTO Continue )

IF "%~1"=="-d" (
	set deploy=true
)
IF "%~1"=="-b" (
	set build=true
)
SHIFT
GOTO Loop
:Continue

:: -----------------------------------------------------------------
rmdir /s /q %nupackLocation%
mkdir packaging\nuget
echo

IF %build%==true (
	%msbuild% ..\src\NServiceBus.SLR.sln /property:Configuration=%configuration% /verbosity:quiet
)

%nuget% pack %nuspec% -Verbosity detailed -OutputDirectory packaging\nuget -Properties Configuration=%configuration% 

IF %deploy%==true (
	%nuget% push %nupackLocation%\*.nupkg -s http://%deployLocation%/ -NonInteractive
)
