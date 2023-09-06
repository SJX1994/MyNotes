
@echo off
IF EXIST C:\"Program Files"\Git\cmd SET PATH=%PATH%;C:\"Program Files"\Git\cmd
IF EXIST %USERPROFILE%\AppData\Local\Atlassian\SourceTree\git_local\bin SET PATH=%PATH%;%USERPROFILE%\AppData\Local\Atlassian\SourceTree\git_local\bin
git sparse-checkout disable
@REM git sparse-checkout disable
chcp 65001
echo 已按指令进入
echo [32m编辑模式[0m
echo [36m可以关闭窗口[0m
pause
