@echo off
set /p ProgramName= Your program name:
csc -platform:x86 /unsafe %ProgramName%.cs