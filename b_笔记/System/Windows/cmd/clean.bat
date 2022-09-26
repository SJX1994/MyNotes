@echo off
setlocal enabledelayedexpansion

echo Please wait for cleaning...
for /f "delims=" %%i in ('dir /ah /s/b') do attrib "%%i" -s -h

del /s /q %cd%\*.suo
del /s /q %cd%\*.sdf
del /s /q %cd%\*.db
del /s /q %cd%\*.autosave
del /s /q %cd%\*.kzproj_1
del /s /q %cd%\*.kzproj_2
del /s /q %cd%\*.kzproj_3
del /s /q %cd%\*.kzproj_4
del /s /q %cd%\*.kzproj_5
del /s /q %cd%\*.kzproj_6
del /s /q %cd%\*.kzproj_7
del /s /q %cd%\*.kzproj_8
del /s /q %cd%\*.kzproj_9
del /s /q %cd%\*.kzproj_10
del /s /q %cd%\*.kzb
for /r %%f in (ipch) do rd "%%f" /s /q
for /r %%f in (compressed) do rd "%%f" /s /q
for /r %%f in (cubemaps) do rd "%%f" /s /q
for /r %%f in (mipmaps) do rd "%%f" /s /q
for /r %%f in (thumbnails) do rd "%%f" /s /q
for /r %%f in (output) do rd "%%f" /s /q
for /r %%f in (mapbox_cache) do rd "%%f" /s /q

for /r %%f in (*gradlew.bat) do (
::echo %%~pdf
set  gradlewPath=%%~pdf
::echo !gradlewPath!
cd /d !gradlewPath!
echo !cd!
call gradlew.bat clean
)

::echo %gradlewPath%
echo Clean done!
pause