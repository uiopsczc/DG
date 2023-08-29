set WORKSPACE=..
set LUBAN_DLL=%WORKSPACE%\Luban\Tools\Luban\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    -t all ^
    -c cs-simple-json ^
    -d json ^
    --schemaPath %CONF_ROOT%\DesignerConfigs\Defines\__root__.xml ^
    -x inputDataDir=%CONF_ROOT%\DesignerConfigs\Datas  ^
    -x outputCodeDir=%WORKSPACE%\Assets\Luban\AutoGen\Code  ^
    -x outputDataDir=%WORKSPACE%\Assets\Luban\AutoGen\Data\json
    
pause