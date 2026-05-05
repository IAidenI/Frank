@echo off
setlocal

set "BASE_URL=https://github.com/IAidenI/Frydia/releases/download/v1.1"
set "TMP_DIR=%TEMP%\FrydiaQuickLaunch"
set "ZIP_FILE=%TMP_DIR%\Frydia.zip"

if exist "%TMP_DIR%" rmdir /s /q "%TMP_DIR%"
mkdir "%TMP_DIR%" || exit /b 1

echo [*] Telechargement de Frydia...

curl.exe -L -s -f -o "%ZIP_FILE%" "%BASE_URL%/Frydia-v1.1-win-x64.zip"
if errorlevel 1 goto error

echo [+] Archive telechargee.

echo [*] Extraction...
powershell -NoProfile -ExecutionPolicy Bypass -Command ^
  "Expand-Archive -Force '%ZIP_FILE%' '%TMP_DIR%'"
if errorlevel 1 goto error

echo [+] Fichiers extraits.

echo [*] Lancement du setup...
start /wait "" "%TMP_DIR%\setup.exe"

echo [*] Nettoyage...
cd /d "%TEMP%"
rmdir /s /q "%TMP_DIR%"

echo [+] Termine.
exit /b 0

:error
echo [!] Erreur pendant l'installation.
cd /d "%TEMP%"
rmdir /s /q "%TMP_DIR%"
pause
exit /b 1