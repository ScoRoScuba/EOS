@echo off

IF EXIST EOS2.Web\locale cd EOS2.Web\locale
IF EXIST ..\locale cd ..\locale
IF NOT EXIST pull-in-translations.bat goto wrongdir

SET root=..\..\..\EOS2-translations\locale

IF NOT EXIST %root%\messages.pot goto error
IF NOT EXIST %root%\messages.mo goto error

copy %root%\messages.pot .
copy %root%\messages.mo .

IF NOT EXIST %root%\af goto error
IF NOT EXIST %root%\en-GB goto error
IF NOT EXIST %root%\en-US goto error

IF NOT EXIST af mkdir af
IF NOT EXIST en-GB mkdir en-GB
IF NOT EXIST en-US mkdir en-US

copy %root%\af\messages.mo af
copy %root%\af\messages.po af

copy %root%\en-GB\messages.mo en-GB
copy %root%\en-GB\messages.po en-GB

copy %root%\en-US\messages.mo en-US
copy %root%\en-US\messages.po en-US

exit /B 0

:error
echo "Failed"
exit /B 1

:wrongdir
echo "Not in locale directory."
exit /B 2


