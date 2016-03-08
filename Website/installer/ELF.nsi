!define VERSION 1.1.3

!include LogicLib.nsh  ; Allow use of ${If}, ${Switch}, loops, etc
!include MUI2.nsh ; Modern UI interface, allows a wizard-based installer

!include FontReg.nsh
!include FontName.nsh
!include WinMessages.nsh

!define SHORT_APP_NAME "ELF Phonics Games"
!define SUPPORT_EMAIL "support@e-l-fun.com"

!include "DotNET.nsh"

Name "English Language Fundamentals Phonics Games"
Outfile ELF.exe 
SetCompressor /SOLID lzma
; SetCompressor /SOLID zlib  ; Smallest memory footprint
; SetCompressorDictSize 2 ; MAke the lzma dictionary smaller

!define MUI_ICON ..\..\ELF_Game\ELF_Main.ico
!define MUI_UNICON ..\..\ELF_Game\ELF_Main.ico

; Replace the values of the two defines below to your application's window class and window title, respectivelly. 
!define WNDCLASS "WindowsForms10.Window.8.app.0.33c0d9d"
!define WNDTITLE "English Language Fundamentals"

Function .onInit
  FindWindow $0 "${WNDCLASS}" "${WNDTITLE}"
  StrCmp $0 0 continueInstall
    MessageBox MB_ICONSTOP|MB_OK "The ELF Phonics Games are currently running.  Please close the games before re-installing."
    Abort
  continueInstall:
    SetShellVarContext all
    SetAutoClose true
FunctionEnd

Function un.onInit
  FindWindow $0 "${WNDCLASS}" "${WNDTITLE}"
  StrCmp $0 0 continueInstall
    MessageBox MB_ICONSTOP|MB_OK "The ELF Phonics Games are currently running.  Please close the games before uninstalling."
    Abort
  continueInstall:
    SetShellVarContext all
    SetAutoClose true
FunctionEnd


RequestExecutionLevel admin
InstallDir $PROGRAMFILES\ELF

!insertmacro MUI_PAGE_INSTFILES
!define MUI_FINISHPAGE_TEXT "Installation of the ELF Phonics Games is complete.  Please check your Desktop for the ELF icon.  You can start the ELF Phonics Games at any time by double-clicking on the ELF icon."
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_INSTFILES

Section "Core Files"

    SetOutPath $INSTDIR
    File "..\..\ELF_Game\bin\Release\ELF_Locked\ELF.exe"
    File "..\..\ELF_Game\bin\Release\ELF_Resources.dll"
    File "..\..\ELF_Game\bin\Release\ELF.exe.config"
    File "ELF.url"

    SetOutPath "$INSTDIR\Sample Lessons"
    File "/oname=First Grade Sample Lesson.pdf" "..\lessons\Lesson_1_7_sample.pdf"
    File "/oname=Second Grade Sample Lesson.pdf" "..\lessons\Lesson_2_8_sample.pdf"
    File "/oname=Third Grade Sample Lesson.pdf" "..\lessons\Lesson_3_7_sample.pdf"
    File "/oname=Fourth Grade Sample Lesson.pdf" "..\lessons\Lesson_4_3_sample.pdf"
    File "/oname=Fifth Grade Sample Lesson.pdf" "..\lessons\Lesson_5_1_sample.pdf"

    SetOutPath $INSTDIR

    IfErrors 0 InstallFonts
    MessageBox MB_OK "Some ELF files were not able to be installed.  It is likely that the ELF Games are currently running.  Please close all ELF Games and re-run the installer."
    Abort

InstallFonts:
    ; SetOutPath $FONTS

    StrCpy $FONT_DIR $FONTS
 
    !insertmacro InstallTTFFont "..\..\ELF_Resources\Fonts\Cooper Black BT.ttf"
    !insertmacro InstallTTFFont "..\..\ELF_Resources\Fonts\Filmcryptic.ttf"
    !insertmacro InstallTTFFont "..\..\ELF_Resources\Fonts\GOTHIC.TTF"
 
    SendMessage ${HWND_BROADCAST} ${WM_FONTCHANGE} 0 0 /TIMEOUT=5000

    SendMessage ${HWND_BROADCAST} ${WM_FONTCHANGE} 0 0

    WriteUninstaller $INSTDIR\uninst.exe

    CreateDirectory "$SMPROGRAMS\ELF"
    CreateShortCut "$SMPROGRAMS\ELF\ELF Phonics Games.lnk" "$INSTDIR\ELF.exe" "" "$INSTDIR\ELF.exe" 0

    CreateShortCut "$SMPROGRAMS\ELF\Visit ELF Online for more information.lnk" "$INSTDIR\ELF.url" "" "$INSTDIR\ELF.exe" 0

    CreateShortCut "$SMPROGRAMS\ELF\First Grade Sample Lesson.lnk" "$INSTDIR\Sample Lessons\First Grade Sample Lesson.pdf"
    CreateShortCut "$SMPROGRAMS\ELF\Second Grade Sample Lesson.lnk" "$INSTDIR\Sample Lessons\Second Grade Sample Lesson.pdf"
    CreateShortCut "$SMPROGRAMS\ELF\Third Grade Sample Lesson.lnk" "$INSTDIR\Sample Lessons\Third Grade Sample Lesson.pdf"
    CreateShortCut "$SMPROGRAMS\ELF\Fourth Grade Sample Lesson.lnk" "$INSTDIR\Sample Lessons\Fourth Grade Sample Lesson.pdf"
    CreateShortCut "$SMPROGRAMS\ELF\Fifth Grade Sample Lesson.lnk" "$INSTDIR\Sample Lessons\Fifth Grade Sample Lesson.pdf"

    CreateShortCut "$DESKTOP\ELF Phonics Games.lnk" "$INSTDIR\ELF.exe" "" "$INSTDIR\ELF.exe" 0
    CreateShortcut "$DESKTOP\ELF Sample Lessons.lnk" "$INSTDIR\Sample Lessons"

    ; Add ourselves to Add/Remove Programs Control Panel
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ELF" "DisplayName" "ELF Phonics Games"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ELF" "DisplayIcon" "$\"$INSTDIR\ELF.exe$\""
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ELF" "HelpLink" "http://www.e-l-fun.com/"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ELF" "NoModify" "1"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ELF" "NoRepair" "1"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ELF" "UninstallString" "$\"$INSTDIR\uninst.exe$\""
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ELF" "QuietUninstallString" "$\"$INSTDIR\uninst.exe$\" /S"
    
    !insertmacro CheckDotNET
SectionEnd

Section "Uninstall"
    Delete /REBOOTOK "$DESKTOP\ELF Phonics Games.lnk"
    Delete /REBOOTOK "$DESKTOP\ELF Sample Lessons.lnk"

    Delete /REBOOTOK "$SMPROGRAMS\ELF\ELF Phonics Games.lnk"
    Delete /REBOOTOK "$SMPROGRAMS\ELF\Visit ELF Online for more information.lnk"

    Delete /REBOOTOK "$SMPROGRAMS\ELF\First Grade Sample Lesson.lnk" 
    Delete /REBOOTOK "$SMPROGRAMS\ELF\Second Grade Sample Lesson.lnk"
    Delete /REBOOTOK "$SMPROGRAMS\ELF\Third Grade Sample Lesson.lnk" 
    Delete /REBOOTOK "$SMPROGRAMS\ELF\Fourth Grade Sample Lesson.lnk"
    Delete /REBOOTOK "$SMPROGRAMS\ELF\Fifth Grade Sample Lesson.lnk" 

    Delete /REBOOTOK "$INSTDIR\Sample Lessons\First Grade Sample Lesson.pdf"
    Delete /REBOOTOK "$INSTDIR\Sample Lessons\Second Grade Sample Lesson.pdf"
    Delete /REBOOTOK "$INSTDIR\Sample Lessons\Third Grade Sample Lesson.pdf"
    Delete /REBOOTOK "$INSTDIR\Sample Lessons\Fourth Grade Sample Lesson.pdf"
    Delete /REBOOTOK "$INSTDIR\Sample Lessons\Fifth Grade Sample Lesson.pdf"

    RMDir /REBOOTOK "$INSTDIR\Sample Lessons"

    RMDir /REBOOTOK $SMPROGRAMS\ELF

    Delete /REBOOTOK $INSTDIR\ELF.url
    Delete /REBOOTOK $INSTDIR\uninst.exe
    Delete /REBOOTOK $INSTDIR\ELF.exe
    Delete /REBOOTOK $INSTDIR\ELF_Resources.dll
    Delete /REBOOTOK $INSTDIR\ELF.exe.config
    RMDir $INSTDIR

    DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ELF"

    IfRebootFlag 0 noreboot
        MessageBox MB_YESNO \
            "A reboot is required to remove files that are in use. Do you wish to reboot now?" \
            IDNO noreboot
        Reboot
noreboot:
    
SectionEnd
