@echo off
SET /A "index=1"
SET /A "count=15"
:while
if %index% leq %count% (
   echo %index%
   type %index%.txt >>good.txt
   SET /A "index=index + 1"
   goto :while
)
pause
