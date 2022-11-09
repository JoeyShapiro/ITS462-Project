@echo off
echo WARNING: You are about to RESET the database of your container to the default
echo Please enter LEGENDARY" into the prompt to confirm this destruction
set /p "input=$ "
if %input%==LEGENDARY echo RESETING Database