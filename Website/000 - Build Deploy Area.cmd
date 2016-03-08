@echo off

del Deploy\*.as?x /F /S /Q > quiet
del Deploy\*.html /F /S /Q > quiet
del Deploy\*.config /F /S /Q > quiet
del Deploy\*.cmd /F /S /Q > quiet
del Deploy\*.js /F /S /Q > quiet
del Deploy\*.css /F /S /Q > quiet
del Deploy\*.Master /F /S /Q > quiet
del Deploy\*.exe /F /S /Q > quiet
del Deploy\*.dll /F /S /Q > quiet
del Deploy\*.pdb /F /S /Q > quiet
del Deploy\*.jpg /F /S /Q > quiet
del Deploy\*.ico /F /S /Q > quiet
del Deploy\*.png /F /S /Q > quiet
del Deploy\*.gif /F /S /Q > quiet
del Deploy\*.sql /F /S /Q > quiet
del Deploy\*.zip /F /S /Q > quiet
del Deploy\web.config.dev /F /S /Q > quiet
del Deploy\web.config.release /F /S /Q > quiet
del Deploy\*.ilproj /F /S /Q > quiet

xcopy *.as?x Deploy /Q /Y /C > quiet
xcopy web.config.* Deploy /Q /Y /C > quiet
copy web.config.release Deploy\web.config /Y > quiet
xcopy 000*Copy* Deploy /Q /Y /C > quiet
xcopy *.Master Deploy /Q /Y /C > quiet

xcopy css\* Deploy\css /I /S /Q /Y /C /EXCLUDE:build_deploy_xcopy_exclusions > quiet
xcopy js\* Deploy\js /I /S /Q /Y /C /EXCLUDE:build_deploy_xcopy_exclusions > quiet
xcopy sql\* Deploy\sql /I /S /Q /Y /C /EXCLUDE:build_deploy_xcopy_exclusions > quiet

xcopy lessons\* Deploy\lessons /I /S /Q /Y /C /EXCLUDE:build_deploy_xcopy_exclusions > quiet
xcopy full_lessons\* Deploy\full_lessons /I /S /Q /Y /C /EXCLUDE:build_deploy_xcopy_exclusions > quiet

copy installer\ELF.exe ..\CD_Website\ELF.exe /Y > quiet
copy installer\ELF.exe Deploy\installer\ELF.exe /Y > quiet
copy installer\ELF_update.xml Deploy\installer\ELF_update.xml /Y > quiet
copy installer\dotnet20\dotnetfx.exe Deploy\installer\dotnet20\dotnetfx.exe /Y > quiet
copy licensing\ELF_IntelliLock.ilproj Deploy\licensing\ELF_IntelliLock.ilproj /Y > quiet

xcopy templates\* Deploy\templates /I /S /Q  /Y /C /EXCLUDE:build_deploy_xcopy_exclusions > quiet

copy favicon.ico Deploy /Y > quiet
xcopy images\*.jpg Deploy\images /I /Q /Y /C > quiet
xcopy images\*.png Deploy\images /I /Q /Y /C > quiet
xcopy images\*.gif Deploy\images /I /Q /Y /C > quiet

xcopy bin\*.exe Deploy\bin /I /Q /Y /C > quiet
xcopy bin\*.dll Deploy\bin /I /Q /Y /C > quiet
xcopy bin\*.pdb Deploy\bin /I /Q /Y /C > quiet
del quiet

pause
