@echo off
echo ========
echo RUNNING
echo ========
CHCP 65001
set /p a=path:
set string=%a%
if not defined string set  (%string% = chdir )


echo %string%
For %%A in ("%string%") do (
    Set Folder=%%~dpA
    Set Name=%%~nxA
    Set Drive=%%~dA
)
echo.Drive is:%Drive%
echo.Folder is: %Folder%
echo.Name is: %Name%
%Drive%
cd %Folder%
CHCP 1252
dir /s *.mel >config.txt
CHCP 65001
pause

