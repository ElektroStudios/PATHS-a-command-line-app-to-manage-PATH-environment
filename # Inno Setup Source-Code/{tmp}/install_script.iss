

#define Version "1.4"
#define AppName "PATHS"
#define ExeName "PATHS"
#define StartMenuGroup "• ElektroStudios"


#define AuthorWebsite "https://github.com/ElektroStudios/PATHS-a-command-line-app-to-manage-PATH-environment"

[Languages]
Name: en; MessagesFile: compiler:default.isl
Name: es; MessagesFile: compiler:Languages\Spanish.isl

[LangOptions]
DialogFontName=Segoe UI
DialogFontSize=10
WelcomeFontName=Verdana
WelcomeFontSize=14

[Messages]
SetupAppTitle={#AppName}

en.SelectLanguageTitle=Install: {#AppName}
es.SelectLanguageTitle=Instalar: {#AppName}

en.SetupWindowTitle=Install: {#AppName}
es.SetupWindowTitle=Instalar: {#AppName}

[CustomMessages]
; English message first, so unespecified languages fallbacks to English.

en.SetupMadeBy=Installer made by ElektroStudios
es.SetupMadeBy=Instalador creado por ElektroStudios

en.SetupOpenAuthorWebsite=Click here if you want to visit the official website of the program author for more information or updates.
es.SetupOpenAuthorWebsite=Haga clic aquí si desea visitar el sitio web oficial del autor del programa para obtener más información o actualizaciones.

en.SetupSelectAllTasks=Select all tasks
es.SetupSelectAllTasks=Seleccionar todo

en.SetupUnselectAllTasks=Clear selection
es.SetupUnselectAllTasks=Borrar selección

en.SetupQuestionDeleteFolder=Do you also want to delete
es.SetupQuestionDeleteFolder=¿Quieres borrar también

en.SetupPasswordHint=
es.SetupPasswordHint=

[Setup]
AppName={#AppName}
AppID={#AppName}
AppVerName={#AppName} {#Version}
SetupMutex={#AppName} {#Version}
AppVersion={#Version}
VersionInfoVersion={#Version}
AppCopyright=
VersionInfoCopyright=© ElektroStudios
AppPublisher=
DefaultDirName={autopf}\ElektroStudios\{#AppName}
DefaultGroupName=ElektroStudios Installers\{#StartMenuGroup}
UninstallDisplayIcon={app}\{#ExeName}.exe
OutputBaseFilename={#AppName} {#Version}
VersionInfoCompany=ElektroStudios
VersionInfoProductName={#AppName}
VersionInfoProductVersion={#Version}
VersionInfoDescription={#AppName} Setup
VersionInfoTextVersion={#Version}
AppPublisherURL=
AppSupportURL=
AlwaysUsePersonalGroup=False
DisableFinishedPage=False
DisableReadyMemo=False
CreateUninstallRegKey=True
UpdateUninstallLogAppName=True
UninstallDisplayName={#AppName}
UseSetupLdr=true
AppMutex=
ChangesAssociations=False
AllowCancelDuringInstall=True
ChangesEnvironment=False
DiskSliceSize=max
Encryption=false
EncryptionKeyDerivation=pbkdf2/200000
ExtraDiskSpaceRequired=1_048_576
ReserveBytes=1048576
Compression=lzma
InternalCompressLevel=max
SolidCompression=false
AlwaysShowComponentsList=False
DisableWelcomePage=False
UserInfoPage=False
DisableDirPage=False
AllowRootDirectory=False
AllowNetworkDrive=False
AllowUNCPath=False
AppendDefaultDirName=True
AppendDefaultGroupName=False
UsePreviousAppDir=True
UsePreviousGroup=True
WizardImageStretch=True
TerminalServicesAware=True
MinVersion=0,6.1sp1
Password=
DisableProgramGroupPage=False
AllowNoIcons=True
DirExistsWarning=True
DisableReadyPage=False
AlwaysShowDirOnReadyPage=True
AlwaysShowGroupOnReadyPage=True
WizardStyle=modern dynamic includetitlebar
SetupLogging=true
UninstallLogMode=Append
ASLRCompatible=True
DEPCompatible=True
LZMAUseSeparateProcess=True
MissingMessagesWarning=True
MissingRunOnceIdsWarning=True
NotRecognizedMessagesWarning=True
UsedUserAreasWarning=True
CompressionThreads=Auto
CloseApplications=True
CloseApplicationsFilter=*.*
CloseApplicationsFilterExcludes=dummy_example.exe
DefaultDialogFontName=Tahoma
DisableStartupPrompt=True
FlatComponentsList=False
LanguageDetectionMethod=UILanguage
PrivilegesRequired=PowerUser
RestartIfNeededByRun=True
ShowLanguageDialog=Auto
UsePreviousLanguage=True
ShowTasksTreeLines=True
SetupIconFile=Icon.ico
WizardBackColor=#F3F3F3
WizardBackColorDynamicDark=#2B2B2B
WizardBackImageFile=
WizardBackImageFileDynamicDark=
WizardBackImageOpacity=255
WizardImageAlphaFormat=None
WizardImageBackColor=White
WizardImageBackColorDynamicDark=#1F1F1F
WizardKeepAspectRatio=True
WizardSizePercent=120
WizardSmallImageBackColor=White
WizardSmallImageBackColorDynamicDark=#1F1F1F
WizardImageFile=embedded\WizardImage.bmp
WizardImageFileDynamicDark=embedded\WizardImage.bmp
WizardSmallImageFile=embedded\WizardSmallImage.bmp
WizardSmallImageFileDynamicDark=embedded\WizardSmallImage.bmp
InfoBeforeFile=embedded\InfoBefore.rtf
Uninstallable=True
ArchitecturesAllowed=x86os x64os
ArchitecturesInstallIn64BitMode=x64os

[Dirs]
Name: {autopf}\ElektroStudios; Attribs: readonly; Flags: UninsNeverUninstall

[Files]

; Temp files
Source: {tmp}\*; DestDir: {tmp}; Flags: RecurseSubDirs CreateAllSubdirs IgnoreVersion DeleteAfterInstall OnlyIfDoesntExist NoEncryption

; Program
Source: {autopf}\*; DestDir: {autopf}; Flags: RecurseSubDirs CreateAllSubdirs IgnoreVersion UninsNeverUninstall OverwriteReadOnly; Attribs: readonly hidden
Source: {app}\*; DestDir: {app}; Flags: RecurseSubDirs CreateAllSubdirs IgnoreVersion OverwriteReadOnly UninsRemoveReadOnly

[Registry]
; ContextMenu Integration
Root: HKCR; SubKey: Directory\Shell\{#AppName}; ValueType: string; ValueName: MUIVerb; ValueData: {#AppName}; Flags: UninsDeleteKey; Tasks: dirmenu
Root: HKCR; SubKey: Directory\Shell\{#AppName}; ValueType: string; ValueName: SubCommands; ValueData: "{#ExeName}.AddDir;{#ExeName}.RemoveDir;{#ExeName}.List;{#ExeName}.Clean"; Flags: UninsDeleteKey; Tasks: dirmenu
Root: HKCR; SubKey: Directory\Shell\{#AppName}; ValueType: string; ValueName: Icon; ValueData: {app}\{#ExeName}.Title.ico; Flags: UninsDeleteKey; Tasks: dirmenu
Root: HKCR; SubKey: Directory\Shell\{#AppName}; ValueType: string; ValueName: Position; ValueData: bottom; Flags: UninsDeleteKey; Tasks: dirmenu

Root: HKCR; SubKey: Directory\Background\Shell\{#AppName}; ValueType: string; ValueName: MUIVerb; ValueData: {#AppName}; Flags: UninsDeleteKey; Tasks: dirbackmenu
Root: HKCR; SubKey: Directory\Background\Shell\{#AppName}; ValueType: string; ValueName: SubCommands; ValueData: "{#ExeName}.AddDirBackground;{#ExeName}.RemoveDirBackground;{#ExeName}.List;{#ExeName}.Clean"; Flags: UninsDeleteKey; Tasks: dirbackmenu
Root: HKCR; SubKey: Directory\Background\Shell\{#AppName}; ValueType: string; ValueName: Icon; ValueData: {app}\{#ExeName}.Title.ico; Flags: UninsDeleteKey; Tasks: dirbackmenu
Root: HKCR; SubKey: Directory\Background\Shell\{#AppName}; ValueType: string; ValueName: Position; ValueData: bottom; Flags: UninsDeleteKey; Tasks: dirbackmenu

; Shell Commands
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.AddDir; ValueType: string; ValueName: ; ValueData: Add directory to PATH; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.AddDir; ValueType: string; ValueName: icon; ValueData: {app}\{#ExeName}.Add.ico; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.AddDir\command; ValueType: string; ValueName: ; ValueData: "CMD /C "" ""{app}\PATHS.exe"" /add ""%1"" & Timeout /T 4"" "; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu

Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.AddDirBackground; ValueType: string; ValueName: ; ValueData: Add directory to PATH; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.AddDirBackground; ValueType: string; ValueName: icon; ValueData: {app}\{#ExeName}.Add.ico; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.AddDirBackground\command; ValueType: string; ValueName: ; ValueData: "CMD /C "" ""{app}\PATHS.exe"" /add ""%%CD%%"" & Timeout /T 4"" "; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu

Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.RemoveDir; ValueType: string; ValueName: ; ValueData: Remove directory from PATH; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.RemoveDir; ValueType: string; ValueName: icon; ValueData: {app}\{#ExeName}.Remove.ico; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.RemoveDir\command; ValueType: string; ValueName: ; ValueData: "CMD /C "" ""{app}\PATHS.exe"" /del ""%1"" & Timeout /T 4"" "; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu

Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.RemoveDirBackground; ValueType: string; ValueName: ; ValueData: Remove directory from PATH; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.RemoveDirBackground; ValueType: string; ValueName: icon; ValueData: {app}\{#ExeName}.Remove.ico; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.RemoveDirBackground\command; ValueType: string; ValueName: ; ValueData: "CMD /C "" ""{app}\PATHS.exe"" /del ""%%CD%%"" & Timeout /T 4"" "; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu

Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.List; ValueType: string; ValueName: ; ValueData: List PATH; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.List; ValueType: string; ValueName: icon; ValueData: {app}\{#ExeName}.List.ico; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.List\command; ValueType: string; ValueName: ; ValueData: "CMD /C "" ""{app}\PATHS.exe"" /list & Timeout /T 999 >NUL"" "; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu

Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.Clean; ValueType: string; ValueName: ; ValueData: Clean PATH; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.Clean; ValueType: string; ValueName: icon; ValueData: {app}\{#ExeName}.Clean.ico; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu
Root: HKLM; SubKey: SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{#ExeName}.Clean\command; ValueType: string; ValueName: ; ValueData: "CMD /C "" ""{app}\PATHS.exe"" /clean & Timeout /T 999 >NUL"" "; Flags: UninsDeleteKey; Tasks: dirmenu or dirbackmenu

[Tasks]
Name: envPath; Description: Add {#AppName} directory to PATH environment variable; GroupDescription: Shell Integration:; Flags: Unchecked

Name: dirmenu; Description: For directories; GroupDescription: ContextMenu Integration:
Name: dirbackmenu; Description: For directories (background); GroupDescription: ContextMenu Integration:

[Run]
Filename: {app}\{#ExeName}.cmd; Description: {cm:LaunchProgram,{#AppName} (CommandLine)}; Flags: PostInstall Unchecked SkipIfSilent NoWait

[UninstallRun]

[Icons]
Name: {autodesktop}\{#AppName}; Filename: {app}\{#ExeName}.cmd; WorkingDir: {app}; IconFilename: {app}\{#ExeName}.exe; Check: IsCreateDesktopShortcutChecked
Name: {group}\{#AppName}; Filename: {app}\{#ExeName}.cmd; WorkingDir: {app}; IconFilename: {app}\{#ExeName}.exe


[Code]

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Variables ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

Var
  // Custom Pages
  CustomSelectDirPage: TWizardPage;

  // Custom Controls
  DesktopShortcutCheckBox: TNewCheckBox;
  CheckAllTasksButton   : TNewButton;
  UncheckAllTasksButton : TNewButton;

  // Custom Vars
  UninstallSuccess  : Boolean; // Determines whether the uninstallation succeeded.
  TaskButtonsCreated: Boolean;
  ForceSelectAtLestOneTask: Boolean;
  HeaderBackgroundBitmap: TBitmapImage;
  FooterBackgroundBitmap: TBitmapImage;

  // Transparent Label controls replacement for all the default TNewStaticText controls.
  TransparentBeveledLabel: TLabel;
  TransparentComponentsDiskSpaceLabel: TLabel;
  TransparentDiskSpaceLabel: TLabel;
  // TransparentFilenameLabel: TLabel;
  TransparentFinishedHeadingLabel: TLabel;
  TransparentFinishedLabel: TLabel;
  TransparentInfoAfterClickLabel: TLabel;
  TransparentInfoBeforeClickLabel: TLabel;
  TransparentLicenseLabel1: TLabel;
  TransparentPageDescriptionLabel: TLabel;
  TransparentPageNameLabel: TLabel;
  TransparentPasswordEditLabel: TLabel;
  TransparentPasswordLabel: TLabel;
  TransparentPreparingLabel: TLabel;
  TransparentReadyLabel: TLabel;
  TransparentSelectComponentsLabel: TLabel;
  TransparentSelectDirBrowseLabel: TLabel;
  TransparentSelectDirLabel: TLabel;
  TransparentSelectStartMenuFolderBrowseLabel: TLabel;
  TransparentSelectStartMenuFolderLabel: TLabel;
  TransparentSelectTasksLabel: TLabel;
  // TransparentStatusLabel: TLabel;
  TransparentUserInfoNameLabel: TLabel;
  TransparentUserInfoOrgLabel: TLabel;
  TransparentUserInfoSerialLabel: TLabel;
  TransparentWelcomeLabel1: TLabel;
  TransparentWelcomeLabel2: TLabel;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Platform Invoke ------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

// ------------------------- //
// ------------------------- //
// InnoHelper Functions ---- //
// ------------------------- //
// ------------------------- //

// Function GetInnoHelperAPIVersion() As <MarshalAs(UnmanagedType.BStr)> String
function GetInnoHelperAPIVersion(): Cardinal;
  external 'GetInnoHelperAPIVersion@files:InnoHelper.dll stdcall setuponly delayload';

// Function GetTasksListRadioButtonsCount(<MarshalAs(UnmanagedType.SysInt)> tasksListHandle As IntPtr,
//                                        <MarshalAs(UnmanagedType.Bool)> ignoreDisabled As Boolean
//                                        ) As <MarshalAs(UnmanagedType.I4)> Integer
function GetTasksListRadioButtonsCount(tasksListHandle: HWND; ignoreDisabled: Boolean): Integer;
  external 'GetTasksListRadioButtonsCount@files:InnoHelper.dll stdcall setuponly delayload';

// Function SetCheckedStateInTasksListItems(<MarshalAs(UnmanagedType.SysInt)> tasksListHandle As IntPtr,
//                                          <MarshalAs(UnmanagedType.Bool)> checked As Boolean
//                                         ) As <MarshalAs(UnmanagedType.I4)> Integer
function SetCheckedStateInTasksListItems(tasksListHandle: HWND; checked: Boolean): Integer;
  external 'SetCheckedStateInTasksListItems@files:InnoHelper.dll stdcall setuponly delayload';



// ------------------------- //
// ------------------------- //
// Windows API Functions --- //
// ------------------------- //
// ------------------------- //

// Creates a symbolic link.
// https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createsymboliclinkw
function CreateSymbolicLink(symlinkFileName, targetFilename: string; Flags: Integer): Boolean;
  external 'CreateSymbolicLinkW@kernel32.dll stdcall setuponly delayload';

// Determines the length of the specified string (not including the terminating null character).
// https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-lstrlenw
Function lstrlenW(str: Cardinal): Cardinal;
  external 'lstrlenW@kernel32.dll stdcall delayload';

// Copies a string to a buffer.
// https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-lstrcpyw
Function lstrcpyW(strDest: String; strSrc: Cardinal): Cardinal;
  external 'lstrcpyW@kernel32.dll stdcall delayload';

// Determines whether the specified window handle identifies an existing window.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindow
function IsWindow(hWnd: HWND): Boolean;
  external 'IsWindow@user32.dll stdcall delayload';

// Determines the visibility state of the specified window.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindowvisible
function IsWindowVisible(hWnd: HWND): Boolean;
  external 'IsWindowVisible@user32.dll stdcall delayload';

// Determines which, if any, of the child windows belonging to a parent window contains the specified point.
// The search is restricted to immediate child windows. Grandchildren, and deeper descendant windows are not searched.
// To skip certain child windows, use the ChildWindowFromPointEx function.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-windowfrompoint
function WindowFromPoint(pt: TPoint): HWND;
  external 'WindowFromPoint@user32.dll stdcall delayload';

// Retrieves a handle to the window that contains the specified physical point.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-windowfromphysicalpoint
function WindowFromPhysicalPoint(pt: TPoint): HWND;
  external 'WindowFromPhysicalPoint@user32.dll stdcall delayload';

// Determines which, if any, of the child windows belonging to a parent window contains the specified point.
// The search is restricted to immediate child windows. Grandchildren, and deeper descendant windows are not searched.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-childwindowfrompoint
function ChildWindowFromPoint(hWndParent: HWND; pt: TPoint): HWND;
  external 'ChildWindowFromPoint@user32.dll stdcall delayload';

// Determines which, if any, of the child windows belonging to the specified parent window contains the specified point.
// The function can ignore invisible, disabled, and transparent child windows.
// The search is restricted to immediate child windows. Grandchildren and deeper descendants are not searched.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-childwindowfrompointex
function ChildWindowFromPointEx(hWnd: HWND; pt: TPoint; Flags: UINT): HWND;
  external 'ChildWindowFromPointEx@user32.dll stdcall delayload';

// Retrieves a handle to the child window at the specified point.
// The search is restricted to immediate child windows; grandchildren and deeper descendant windows are not searched.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-realchildwindowfrompoint
function RealChildWindowFromPoint(hWndParent: HWND; pt: TPoint): HWND;
  external 'RealChildWindowFromPoint@user32.dll stdcall delayload';

// Retrieves a handle to a window that has the specified relationship (Z-Order or owner) to the specified window.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindow
function GetWindow(hWnd: HWND; cmd: UINT): HWND;
  external 'GetWindow@user32.dll stdcall delayload';

// Examines the Z order of the child windows associated with the specified parent window
// and retrieves a handle to the child window at the top of the Z order.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-gettopwindow
function GetTopWindow(hWnd: HWND): HWND;
  external 'GetTopWindow@user32.dll stdcall delayload';

// Retrieves the dimensions of the bounding rectangle of the specified window.
// The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect
function GetWindowRect(hWnd: HWND; var rect: TRect): Boolean;
  external 'GetWindowRect@user32.dll stdcall delayload';

// Retrieves the coordinates of a window's client area.
// The client coordinates specify the upper-left and lower-right corners of the client area.
// Because client coordinates are relative to the upper-left corner of a window's client area, the coordinates of the upper-left corner are (0,0).
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getclientrect
function GetClientRect(hWnd: HWND; var rect: TRect): Boolean;
  external 'GetClientRect@user32.dll stdcall delayload';

// Converts the client-area coordinates of a specified point to screen coordinates.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-clienttoscreen
function ClientToScreen(hWnd: HWND; var pt: TPoint): Boolean;
  external 'ClientToScreen@user32.dll stdcall delayload';

// Converts the screen coordinates of a specified point on the screen to client-area coordinates.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-screentoclient
function ScreenToClient(hWnd: HWND; var pt: TPoint): Boolean;
  external 'ScreenToClient@user32.dll stdcall delayload';

// Converts (maps) a set of points from a coordinate space relative to one window, to a coordinate space relative to another window.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mapwindowpoints
function MapWindowPoints(hWndFrom: HWND; hWndTo: HWND; var points: TPoint; pointsCount: UINT): Integer;
  external 'MapWindowPoints@user32.dll stdcall delayload';

// Converts the logical coordinates of a point in a window to physical coordinates.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-logicaltophysicalpoint
function LogicalToPhysicalPoint(hWnd: HWND; var pt: TPoint): Boolean;
  external 'LogicalToPhysicalPoint@user32.dll stdcall delayload';

// Converts the physical coordinates of a point in a window to logical coordinates.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-physicaltologicalpoint
function PhysicalToLogicalPoint(hWnd: HWND; var pt: TPoint): Boolean;
  external 'PhysicalToLogicalPoint@user32.dll stdcall delayload';

// Retrieves a handle to the foreground window (the window with which the user is currently working).
// The system assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow
function GetForegroundWindow: HWND;
  external 'GetForegroundWindow@user32.dll stdcall delayload';

// Brings the thread that created the specified window into the foreground and activates the window.
// Keyboard input is directed to the window, and various visual cues are changed for the user.
// The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setforegroundwindow
function SetForegroundWindow(hWnd: HWND): Boolean;
  external 'SetForegroundWindow@user32.dll stdcall delayload';

// Retrieves the length, in characters, of the specified window's title bar text (if the window has a title bar).
// If the specified window is a control, the function retrieves the length of the text within the control.
// However, GetWindowTextLength cannot retrieve the length of the text of an edit control in another application.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextlengthw
function GetWindowTextLength(hWnd: HWND): Integer;
  external 'GetWindowTextLengthW@user32.dll stdcall delayload';

// Copies the text of the specified window's title bar (if it has one) into a buffer.
// If the specified window is a control, the text of the control is copied.
// However, GetWindowText cannot retrieve the text of a control in another application.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextw
function GetWindowText(hWnd: HWND; out buffer: String; maxCount: Integer): Integer;
  external 'GetWindowTextW@user32.dll stdcall delayload';

// [This function is not intended for general use. It may be altered or unavailable in subsequent versions of Windows.]
// Copies the text of the specified window's title bar (if it has one) into a buffer.
// This function is similar to the GetWindowText function. However, it obtains the window text directly from
// the window structure associated with the specified window's handle and then always provides the text as a Unicode string.
// This is unlike GetWindowText which obtains the text by sending the window a WM_GETTEXT message.
// If the specified window is a control, the text of the control is obtained.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-internalgetwindowtext
function InternalGetWindowText(hWnd: HWND; out buffer: string; maxCount: Integer): Integer;
  external 'InternalGetWindowText@user32.dll stdcall delayload';

// Changes the text of the specified window's title bar (if it has one).
// If the specified window is a control, the text of the control is changed.
// However, SetWindowText cannot change the text of a control in another application.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowtextw
function SetWindowText(hWnd: HWND; text: String): Boolean;
  external 'SetWindowTextW@user32.dll stdcall delayload';

// Sets the specified window's show state.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
function ShowWindow(hWnd: HWND; cmdShow: Integer): Boolean;
  external 'ShowWindow@user32.dll stdcall delayload';

// Sets the show state of a window without waiting for the operation to complete.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindowasync
function ShowWindowAsync(hWnd: HWND; cmdShow: Integer): Boolean;
  external 'ShowWindowAsync@user32.dll stdcall delayload';

// Changes the position and dimensions of the specified window.
// For a top-level window, the position and dimensions are relative to the upper-left corner of the screen.
// For a child window, they are relative to the upper-left corner of the parent window's client area.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-movewindow
function MoveWindow(hWnd: HWND; x: Integer; y: Integer; width: Integer; height: Integer; repaint: Boolean): Boolean;
  external 'MoveWindow@user32.dll stdcall delayload';

// Changes the size, position, and Z order of a child, pop-up, or top-level window.
// These windows are ordered according to their appearance on the screen.
// The topmost window receives the highest rank and is the first window in the Z order.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos
function SetWindowPos(hWnd: HWND; hWndInsertAfter: HWND; x: Integer; y: Integer; width: Integer; height: Integer; Flags: UINT): Boolean;
  external 'SetWindowPos@user32.dll stdcall delayload';

// The UpdateWindow function updates the client area of the specified window by sending a
// WM_PAINT message to the window if the window's update region is not empty.
// The function sends a WM_PAINT message directly to the window procedure of the specified window, bypassing the application queue.
// If the update region is empty, no message is sent.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-updatewindow
function UpdateWindow(hWnd: HWND): Boolean;
  external 'UpdateWindow@user32.dll stdcall delayload';

// Brings the specified window to the top of the Z order.
// If the window is a top-level window, it is activated.
// If the window is a child window, the top-level parent window associated with the child window is activated.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-bringwindowtotop
function BringWindowToTop(hWnd: HWND): Boolean;
  external 'BringWindowToTop@user32.dll stdcall delayload';

// Enables or disables mouse and keyboard input to the specified window or control.
// When input is disabled, the window does not receive input such as mouse clicks and key presses.
// When input is enabled, the window receives all input.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enablewindow
function EnableWindow(hWnd: HWND; enable: Boolean): Boolean;
  external 'EnableWindow@user32.dll stdcall delayload';

// Sets the keyboard focus to the specified window.
// The window must be attached to the calling thread's message queue.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setfocus
function SetFocus(hWnd: HWND): HWND;
  external 'SetFocus@user32.dll stdcall delayload';

// Retrieves a handle to the top-level window whose class name and window name match the specified strings.
// This function does not search child windows.
// This function does not perform a case-sensitive search.
// To search child windows, beginning with a specified child window, use the FindWindowEx function.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-findwindoww
function FindWindow(className, windowText: string): Integer;
  external 'FindWindowW@user32.dll stdcall delayload';

// Retrieves a handle to a window whose class name and window name match the specified strings.
// The function searches child windows, beginning with the one following the specified child window.
// This function does not perform a case-sensitive search.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-findwindowexw
function FindWindowEx(hWndParent: HWND; hWndChildAfter: HWND; className: String; windowText: String): Integer;
  external 'FindWindowExW@user32.dll stdcall delayload';

// Determines whether a window is a child window or descendant window of a specified parent window.
// A child window is the direct descendant of a specified parent window if that parent window is in the chain of parent windows;
// the chain of parent windows leads from the original overlapped or pop-up window to the child window.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-ischild
function IsChild(hWndParent: HWND; hWndChild: HWND): Boolean;
  external 'IsChild@user32.dll stdcall delayload';

// Retrieves a handle to the specified window's parent or owner.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getparent
function GetParent(hWnd: HWND): HWND;
  external 'GetParent@user32.dll stdcall delayload';

// Changes the parent window of the specified child window.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setparent
function SetParent(hWndChild: HWND; hWndParent: HWND): HWND;
  external 'SetParent@user32.dll stdcall delayload';

// Retrieves the name of the class to which the specified window belongs.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getclassnamew
function GetClassName(hWnd: HWND; out buffer: String; maxCount: Integer): Integer;
  external 'GetClassNameW@user32.dll stdcall delayload';

// Retrieves a string that specifies the window type.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-realgetwindowclassw
function RealGetWindowClass(hWnd: HWND; out buffer: string; maxCount: UINT): Integer;
  external 'RealGetWindowClassW@user32.dll stdcall delayload';

// Minimizes (but does not destroy) the specified window.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-closewindow
function CloseWindow(hWnd: HWND): Boolean;
  external 'CloseWindow@user32.dll stdcall delayload';

// Destroys the specified window. The function sends WM_DESTROY and WM_NCDESTROY messages
// to the window to deactivate it and remove the keyboard focus from it.
// The function also destroys the window's menu, destroys timers, removes clipboard ownership,
// and breaks the clipboard viewer chain (if the window is at the top of the viewer chain).
// If the specified window is a parent or owner window, DestroyWindow automatically destroys the
// associated child or owned windows when it destroys the parent or owner window.
// The function first destroys child or owned windows, and then it destroys the parent or owner window.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-destroywindow
function DestroyWindow(hWnd: HWND): Boolean;
  external 'DestroyWindow@user32.dll stdcall delayload';

// Copies the coordinates of one rectangle to another.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-copyrect
function CopyRect(var destination: TRect; const source: TRect): Boolean;
  external 'CopyRect@user32.dll stdcall delayload';

// Sends the specified message (with wParam: Integer, wParam: Integer) to a window or windows.
// The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessagew
function SendMessageIntInt(hWnd: HWND; msg: UINT; wParam: Integer; lParam: Integer): Integer;
  external 'SendMessageW@user32.dll stdcall delayload';

// Sends the specified message (with wParam: Integer, wParam: String) to a window or windows.
// The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessagew
function SendMessageIntStr(hWnd: HWND; msg: UINT; wParam: Integer; lParam: String): Integer;
  external 'SendMessageW@user32.dll stdcall delayload';

// Sends the specified message (with wParam: Integer, wParam: TRect) to a window or windows.
// The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message.
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessagew
function SendMessageIntRect(hWnd: HWND; msg: UINT; wParam: Integer; var lParam: TRect): Integer;
  external 'SendMessageW@user32.dll stdcall delayload';

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Utility Functions ----------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

// Summary:
// -----------------------------------------------------------------------------------
// PathEnvAddDir(dirPath: String; forAllUsers: Boolean)
// PathEnvDelDir(dirPath: String; forAllUsers: Boolean)
// IsDirEmpty(dirName: String): Boolean
// IsRootDir(Path: String): Boolean
// StrEquals(const Str1, Str2: string; IgnoreCase: Boolean): Boolean;
// StrEndsWith(S, SubText: string; IgnoreCase: Boolean): Boolean
// StrReplace(S: String; FromStr: String; ToStr: String): String
// StrContains(S: String; Find: String; IgnoreCase: Boolean): Boolean
// StrContainsAny(S: String; FindArray: TArrayOfString; IgnoreCase: Boolean): Boolean
// StrSplit(S: String; Separator: String): TArrayOfString
// BoolToStr(Value: Boolean): String
// YesNoOrTrueFalseToBool(Value: String): Boolean
// IsProcessRunning(const ProcessName: string): Boolean
// IsAnyProcessRunning(ProcessNames: TArrayOfString): Boolean;
// KillProcess(ProcessName: String): Boolean
// KillProcesses(ProcessNames: TArrayOfString): Boolean
// AskToKillRunningProcess(ProcessName: String; Message: String);
// AskToKillRunningProcesses(ProcessNames: TArrayOfString; Message: String);
// IfProcessIsRunning_RetryOrAbort(ProcessName: String; Message: String);
// IfAnyProcessIsRunning_RetryOrAbort(ProcessNames: TArrayOfString; Message: String);
// AbortIfMissingEnvVar(VarName: String; Message: String);
// AbortIfMissingEnvVars(VarNames: TArrayOfString; Message: String);
// AbortIfMissingFile(FilePath: String; Message: String);
// AbortIfMissingFile(FilePaths: TArrayOfString; Message: String);
// AbortIfMissingDir(DirPath: String; Message: String);
// AbortIfMissingDirs(DirPaths: TArrayOfString; Message: String);
// DisableTaskControlsByDescription(taskDescriptions: TArrayOfString);
// DisableTaskControlsByIndex(taskIndices: TArrayOfInteger);
// IsAnyTaskSelected(): Boolean
// CheckOrUncheckkAllTasks(checked: Boolean);
// InheritBoundsRect(ASource, ATarget: TControl);
// ConvertBStrToInnoString(BStr: Cardinal): String;
// -----------------------------------------------------------------------------------



// - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Adds a directory path to PATH environment variable  //
// - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure PathEnvAddDir(dirPath: String; forAllUsers: Boolean);
var
    PathValue: string;
    RootKey: Integer;
begin
    if forAllUsers then RootKey := HKEY_LOCAL_MACHINE else RootKey := HKEY_CURRENT_USER;

    // Retrieve current PATH value (use empty string if entry not exists).
    if not RegQueryStringValue(RootKey, 'SYSTEM\CurrentControlSet\Control\Session Manager\Environment', 'Path', PathValue) then
        PathValue := '';

    if PathValue = '' then begin
        PathValue := dirPath + ';';
    end else begin
        // Skip if string already found in path.
        if Pos(';' + Uppercase(dirPath) + ';',  ';' + Uppercase(PathValue) + ';') > 0 then exit;
        if Pos(';' + Uppercase(dirPath) + '\;', ';' + Uppercase(PathValue) + ';') > 0 then exit;

        // Append directory path to the end of the PATH environment variable.
        Log(Format('Right(PathValue, 1): [%s]', [PathValue[length(PathValue)]]));
        if PathValue[length(PathValue)] = ';' then begin
          // Don't double up ';' in env(PATH).
          PathValue := PathValue + dirPath + ';';
        end else begin
          PathValue := PathValue + ';' + dirPath + ';';
        end;
    end;

    // Overwrite (or create if missing) rhe PATH environment variable.
    if RegWriteStringValue(RootKey, 'SYSTEM\CurrentControlSet\Control\Session Manager\Environment', 'Path', PathValue) then begin
      Log(Format('The directory [%s] was added to PATH environment variable: [%s]', [dirPath, PathValue]));
    end else begin
      Log(Format('Error while adding the directory [%s] to PATH environment variable: [%s]', [dirPath, PathValue]));
    end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Removes a directory path from PATH environment variable //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure PathEnvDelDir(dirPath: String; forAllUsers: Boolean);
var
    PathValue: string;
    RootKey: Integer;
    P: Integer;
    Offset: Integer;
    DelimLen: Integer;
begin
    if forAllUsers then RootKey := HKEY_LOCAL_MACHINE else RootKey := HKEY_CURRENT_USER;

    // Skip if registry entry not exists.
    if not RegQueryStringValue(RootKey, 'SYSTEM\CurrentControlSet\Control\Session Manager\Environment', 'Path', PathValue) then exit;

    // Skip if string not found in path.
    DelimLen := 1; // Length(';')
    P := Pos(';' + Uppercase(dirPath) + ';', ';' + Uppercase(PathValue) + ';');
    if P = 0 then begin
        // perhaps dirPath lives in PathValue, but terminated by '\;'.
        DelimLen := 2; // Length('\;')
        P := Pos(';' + Uppercase(dirPath) + '\;', ';' + Uppercase(PathValue) + ';');
        if P = 0 then exit;
    end;

    // Decide where to start string subset in Delete() operation.
    if P = 1 then begin
        Offset := 0;
    end else begin
        Offset := 1;
    end;

    // Update PATH environment variable.
    Delete(PathValue, P - Offset, Length(dirPath) + DelimLen);

    // Overwrite (or create if missing) rhe PATH environment variable.
    if RegWriteStringValue(RootKey, 'SYSTEM\CurrentControlSet\Control\Session Manager\Environment', 'Path', PathValue) then begin
      Log(Format('The directory [%s] was removed from PATH environment variable: [%s]', [dirPath, PathValue]));
    end else begin
      Log(Format('Error while removing the directory [%s] from PATH environment variable: [%s]', [dirPath, PathValue]));
    end;
end;

// - - - - - - - - - - - - - - - - - - - - //
// Determines whether a directory is empty //
// - - - - - - - - - - - - - - - - - - - - //
function IsDirEmpty(dirName: String): Boolean;
var
  FindRec: TFindRec;
  FileCount: Integer;
begin
  Result := Not DirExists(dirName);
  if FindFirst(dirName+'\*', FindRec) then begin
    try
      repeat
        if (FindRec.Name <> '.') and (FindRec.Name <> '..') then begin
          FileCount := 1;
          break;
        end;
      until not FindNext(FindRec);
    except
      ShowExceptionMessage();
    finally
      FindClose(FindRec);
      if FileCount = 0 then Result := True;
    end;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Determine whether the source directory path is a root directory (eg. 'C:\') //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
Function IsRootDir(Path: String): Boolean;
var
  PathLength: LongInt;
begin
  StringChangeEx(Path, ':', '', True);
  Path := RemoveBackslash(Path);
  PathLength := Length(Path);
  Result := (PathLength <= 1)
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Returns a value that determine whether two string values are equal. //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function StrEquals(const Str1, Str2: string; IgnoreCase: Boolean): Boolean;
begin
  if IgnoreCase then begin
    Result := SameText(Str1, Str2) // https://jrsoftware.org/ishelp/index.php?topic=isxfunc_sametext
  end else begin
    Result := SameStr(Str1, Str2)  // https://jrsoftware.org/ispphelp/index.php?topic=samestr
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Determine whether the source string ends with the specified characters      //
//                                                                             //
//https://stackoverflow.com/a/61522649                                         //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function StrEndsWith(S, SubText: string; IgnoreCase: Boolean): Boolean;
var
  EndStr: string;
begin
  EndStr := Copy(S, Length(S) - Length(SubText) + 1, Length(SubText));
  if IgnoreCase then begin
    Result := SameText(SubText, EndStr);
  end else begin
    Result := SameStr(SubText, EndStr);
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Replaces all occurrences of the specified keyword in the source string  //
// and returns the modified string.                                        //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function StrReplace(S: String; FromStr: String; ToStr: String): String;
begin
  StringChangeEx(S, FromStr, ToStr, True);
  Result := S;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Determine whether the source string contains the specified string value //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function StrContains(S: String; Find: String; IgnoreCase: Boolean): Boolean;
begin
  if IgnoreCase then begin
    Result := (Pos(LowerCase(Find), LowerCase(S)) <> 0);
  end else begin
    Result := (Pos(Find, S) <> 0);
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Determine whether the source string contains at least one of the specified string values  //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function StrContainsAny(S: String; FindArray: TArrayOfString; IgnoreCase: Boolean): Boolean;
var
  I: Integer;
begin
  Result := False;
  if IgnoreCase then begin
    for I := 0 to GetArrayLength(FindArray) - 1 do begin
      if (Pos(LowerCase(FindArray[I]), LowerCase(S)) <> 0) then begin
        Result := True;
        Break;
      end;
    end;
  end else begin
    for I := 0 to GetArrayLength(FindArray) - 1 do begin
      if (Pos(FindArray[I], S) <> 0) then begin
        Result := True;
        Break;
      end;
    end;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Splits a string by the specified delimiter character  //
//                                                       //
// https://stackoverflow.com/a/37916394/1248295          //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function StrSplit(S: String; Separator: String): TArrayOfString;
var
  i, p: Integer;
  Dest: TArrayOfString;
begin
  i := 0;
  repeat
    SetArrayLength(Dest, i+1);
    p := Pos(Separator,S);
    if p > 0 then begin
      Dest[i] := Copy(S, 1, p-1);
      S := Copy(S, p + Length(Separator), Length(S));
      i := i + 1;
    end else begin
      Dest[i] := S;
      S := '';
    end;
  until Length(S)=0;
  Result := Dest
end;

// - - - - - - - - - - - - - - - - - - - - - -  //
// Converts a Boolean to String                 //
//                                              //
// https://stackoverflow.com/a/35044243/1248295 //
// - - - - - - - - - - - - - - - - - - - - - -  //
function BoolToStr(Value: Boolean): String;
begin
  if Value then
    Result := 'True'
  else
    Result := 'False';
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - //
// Converts a Yes/No or True/False string to Boolean //
// - - - - - - - - - - - - - - - - - - - - - - - - - //
function YesNoOrTrueFalseToBool(Value: String): Boolean;
begin
  if (Value = '') then begin
    RaiseException('YesNoOrTrueFalseToBool function: The parameter Value is empty. (Are you missing to declare a setup setting?)');
  end;

  if (LowerCase(Value) = 'true') or (LowerCase(Value) = 'yes') or (LowerCase(Value) = 'auto') then begin
    Result := True;

  end else if (LowerCase(Value) = 'false') or (LowerCase(Value) = 'no') then begin
    Result := False;

  end else begin
    RaiseException('YesNoOrTrueFalseToBool function: Invalid string format for parameter Value ("' + Value + '")');
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Determine whether a process with the specified name is running  //
//                                                                 //
// https://stackoverflow.com/a/24181158/1248295                    //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function IsProcessRunning(const ProcessName: string): Boolean;
var
  SWbemLocator, WMIService, WbemObjectSet: Variant;
  ProcessCount: Integer;
begin
  SWbemLocator  := CreateOleObject('WBEMScripting.SWBEMLocator');
  WMIService    := SWbemLocator.ConnectServer('localhost', 'root\CIMV2');
  WbemObjectSet := WMIService.ExecQuery(Format('SELECT Name FROM Win32_Process Where Name="%s"', [ProcessName]));
  if VarIsNull(WbemObjectSet) then Exit;
  ProcessCount  := WbemObjectSet.Count;
  Result := (ProcessCount > 0)
  Log(Format('Function Call: ''IsProcessRunning("%s")''; Result: ''%s''', [ProcessName, BoolToStr(Result)]));
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Determine whether at least one process with the specified process names is running  //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function IsAnyProcessRunning(ProcessNames: TArrayOfString): Boolean;
var
  Index: Integer;
begin
  Result := False;
  for Index := 0 to Length(ProcessNames) - 1 do begin
    if IsProcessRunning(ProcessNames[Index]) then begin
      Result := True;
      Break;
    end;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Terminates all the processes with the specified process name    //
//                                                                 //
// Returns False if no process were found with the specified name  //
// Returns True otherwise                                          //
//                                                                 //
// https://stackoverflow.com/a/24014649/1248295                    //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function KillProcess(ProcessName: String): Boolean;
var
  SWbemLocator, WMIService, WbemObjectSet, WbemObject: Variant;
  ProcessCount: Integer;
  Index: Integer;
begin;
  Result := False;
  SWbemLocator  := CreateOleObject('WbemScripting.SWbemLocator');
  WMIService    := SWbemLocator.ConnectServer('localhost', 'root\CIMV2');
  WbemObjectSet := WMIService.ExecQuery(Format('SELECT Name FROM Win32_Process Where Name="%s"', [ProcessName]));
  if VarIsNull(WbemObjectSet) then Exit;
  ProcessCount := WbemObjectSet.Count;
  if (ProcessCount > 0) then begin
    Result := True;
    Log(Format('Function Call: ''KillProcess("%s")''; Process Count: %d', [ProcessName, ProcessCount]));
    for Index := 0 to (WbemObjectSet.Count - 1) do begin
      Log(Format('Function Call: ''KillProcess("%s")''; Killing process with index %d ...', [ProcessName, Index]));
      WbemObject := WbemObjectSet.ItemIndex(Index);
      if not VarIsNull(WbemObject) then begin
        WbemObject.Terminate();
        WbemObject := Unassigned;
      end;
    end;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Terminates all the processes with the specified process names     //
//                                                                   //
// Returns False if no processes were found with the specified names //
// Returns True otherwise                                            //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function KillProcesses(ProcessNames: TArrayOfString): Boolean;
var
  Index: Integer;
begin;
  for Index := 0 to Length(ProcessNames) - 1 do begin
    if KillProcess(ProcessNames[Index]) then Result := True;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Checks for a running process with the specified name.             //
//                                                                   //
// If the process is found, a Ok/Cancel MsgBox is shown              //
// asking the user to let the setup terminate the process            //
// or to abort the installation.                                     //
//                                                                   //
// The message to show can be customized via the Message parameter.  //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure AskToKillRunningProcess(ProcessName: String; Message: String);
begin
  If Message = '' then Message := 'The running process ' + ProcessName + ' must be terminated in order to continue the installation.' + #13#10 + #13#10 +
                                  'If you continue, setup will force the process termination. If you cancel, the installation will abort.';

  if IsProcessRunning(ProcessName) then begin
    case MsgBox(Message, mbError, MB_OKCANCEL or MB_DEFBUTTON1) of
      IDOK: begin
        KillProcess(ProcessName);
      end;
      IDCANCEL: begin
        MsgBox('Setup was aborted. This program will terminate now.', mbError, MB_OK);
        Abort;
      end;
    end;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Checks for a set of running processes with the specified names.   //
//                                                                   //
// If any of the processes are found, a Ok/Cancel MsgBox is shown    //
// asking the user to let the setup terminate the process            //
// or to abort the installation.                                     //
//                                                                   //
// The message to show can be customized via the Message parameter.  //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure AskToKillRunningProcesses(ProcessNames: TArrayOfString; Message: String);
var
  Index: Integer;
  isMessageEmpty: Boolean;
begin
  isMessageEmpty := (Message = '')
  while IsAnyProcessRunning(ProcessNames) do begin
    for Index := 0 to Length(ProcessNames) - 1 do begin
      while IsProcessRunning(ProcessNames[Index]) do begin
        If isMessageEmpty then Message := 'The running process ' + ProcessNames[Index] + ' must be terminated in order to continue the installation.' + #13#10 + #13#10 +
                                          'If you continue, setup will force the process termination. If you cancel, the installation will abort.';

        case MsgBox(Message, mbError, MB_OKCANCEL or MB_DEFBUTTON1) of
          IDOK: begin
            KillProcess(ProcessNames[Index]);
          end;
          IDCANCEL: begin
            MsgBox('Setup was aborted. This program will terminate now.', mbError, MB_OK);
            Abort;
          end;
        end;
      end;
    end;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Checks for a running process with the specified name,             //
// until the process is closed / not running anymore.                //
//                                                                   //
// If the process is found, a Retry/Cancel MsgBox is shown           //
// requesting the user to retry the process check                    //
// or to abort the setup.                                            //
//                                                                   //
// If the process is not found, the loop ends.                       //
//                                                                   //
// The message to show can be customized via the Message parameter.  //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure IfProcessIsRunning_RetryOrAbort(ProcessName: String; Message: String);
begin
  If Message = '' then Message := 'The running process ' + ProcessName + ' must be terminated in order to continue the installation.' + #13#10 + #13#10 +
                                  'Please manually close ' + ProcessName + ' and retry again, or cancel to abort the installation.';

  while IsProcessRunning(ProcessName) do begin
    if MsgBox(Message, mbError, MB_RETRYCANCEL or MB_DEFBUTTON1) = IDCANCEL then begin
      MsgBox('Setup was aborted. This program will terminate now.', mbError, MB_OK);
      Abort;
    end;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Checks for a set of running processes with the specified names,   //
// until the processes are closed / not running anymore.             //
//                                                                   //
// If any process is found, a Retry/Cancel MsgBox is shown           //
// requesting the user to retry the process check                    //
// or to abort the setup.                                            //
//                                                                   //
// If the processes are not found, the loop ends.                    //
//                                                                   //
// The message to show can be customized via the Message parameter.  //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure IfAnyProcessIsRunning_RetryOrAbort(ProcessNames: TArrayOfString; Message: String);
var
  Index: Integer;
  isMessageEmpty: Boolean;
begin
  isMessageEmpty := (Message = '')
  while IsAnyProcessRunning(ProcessNames) do begin
    for Index := 0 to Length(ProcessNames) - 1 do begin
      while IsProcessRunning(ProcessNames[Index]) do begin
        If isMessageEmpty then Message := 'The running process ' + ProcessNames[Index] + ' must be terminated in order to continue the installation.' + #13#10 + #13#10 +
                                          'Please manually close ' + ProcessNames[Index] + ' and retry again, or cancel to abort the installation.';

        if MsgBox(Message, mbError, MB_RETRYCANCEL or MB_DEFBUTTON1) = IDCANCEL then begin
          MsgBox('Setup was aborted. This program will terminate now.', mbError, MB_OK);
          Abort;
        end;
      end;
    end;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// If does not exists the specified environment variable,  //
// displays an error msgbox with the specified message     //
// and aborts the setup.                                   //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure AbortIfMissingEnvVar(VarName: String; Message: String);
begin
  If Message = '' then Message := 'Missing "' + VarName + '" environment variable. The installation can''t continue.';

  If GetEnv(VarName) = '' then begin
    MsgBox(Message, mbCriticalError, MB_OK);
    Abort;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// If does not exists any of the specified environment variables,  //
// displays an error msgbox with the specified message             //
// and aborts the setup.                                           //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure AbortIfMissingEnvVars(VarNames: TArrayOfString; Message: String);
var
  Index: Integer;
  isMessageEmpty: Boolean;
begin
  isMessageEmpty := (Message = '')
  for Index := 0 to Length(VarNames) - 1 do begin
    If GetEnv(VarNames[Index]) = '' then begin
      If isMessageEmpty then  Message :=  'Missing "' + VarNames[Index] + '" environment variable. The installation can''t continue.';
      MsgBox(Message, mbCriticalError, MB_OK);
      Abort;
    end;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - //
// If the specified file path does not exists,         //
// displays an error msgbox with the specified message //
// and aborts the setup.                               //
// - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure AbortIfMissingFile(FilePath: String; Message: String);
begin
  If Message = '' then Message := 'Missing file: "' + FilePath + '". The installation can''t continue.';

  If Not FileExists(FilePath) then begin
    MsgBox(Message, mbCriticalError, MB_OK);
    Abort;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - //
// If any of the specified file paths does not exists, //
// displays an error msgbox with the specified message //
// and aborts the setup.                               //
// - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure AbortIfMissingFiles(FilePaths: TArrayOfString; Message: String);
var
  Index: Integer;
  isMessageEmpty: Boolean;
begin
  isMessageEmpty := (Message = '')
  for Index := 0 to Length(FilePaths) - 1 do begin
    If Not FileExists(FilePaths[Index]) then begin
      If isMessageEmpty then  Message := 'Missing file: "' + FilePaths[Index] + '". The installation can''t continue.';
      MsgBox(Message, mbCriticalError, MB_OK);
      Abort;
    end;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - //
// If the specified directory path does not exists,    //
// displays an error msgbox with the specified message //
// and aborts the setup.                               //
// - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure AbortIfMissingDir(DirPath: String; Message: String);
begin
  If Message = '' then Message := 'Missing directory: "' + DirPath + '". The installation can''t continue.';

  If Not DirExists(DirPath) then begin
    MsgBox(Message, mbCriticalError, MB_OK);
    Abort;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// If any of the specified directory path does not exists, //
// displays an error msgbox with the specified message     //
// and aborts the setup.                                   //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure AbortIfMissingDirs(DirPaths: TArrayOfString; Message: String);
var
  Index: Integer;
  isMessageEmpty: Boolean;
begin
  isMessageEmpty := (Message = '')
  for Index := 0 to Length(DirPaths) - 1 do begin
    If Not DirExists(DirPaths[Index]) then begin
      If isMessageEmpty then  Message := 'Missing directory: "' + DirPaths[Index] + '". The installation can''t continue.';
      MsgBox(Message, mbCriticalError, MB_OK);
      Abort;
    end;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Disables the task controls that belongs to the      //
// specified task descriptions.                        //
//                                                     //
// NOTE: Don't call this method outside CurPageChanged //
// - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure DisableTaskControlsByDescription(taskDescriptions: TArrayOfString);
var
  descriptionsIndex: Integer;
  taskIndex: Integer;
  taskFound: Boolean;
begin
  for descriptionsIndex := 0 to GetArrayLength(taskDescriptions) - 1 do begin
    taskIndex := WizardForm.TasksList.Items.IndexOf(taskDescriptions[descriptionsIndex]);
    taskFound := (taskIndex <> -1);
    if taskFound then begin
      WizardForm.TasksList.ItemEnabled[taskIndex] := False;
    end else begin
      MsgBox('''DisableTasks'' method: a task with description ''' + taskDescriptions[descriptionsIndex] + ''' was not found.', mbCriticalError, MB_OK);
    end;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Disables the task controls that belongs to the      //
// specified task indices.                             //
//                                                     //
// NOTE: Don't call this method outside CurPageChanged //
// - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure DisableTaskControlsByIndex(taskIndices: TArrayOfInteger);
var
  I: Integer;
begin
  for I := 0 to GetArrayLength(taskIndices) - 1 do begin
    WizardForm.TasksList.ItemEnabled[taskIndices[I]] := False;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Determine whether at least one task is selected in the WizardForm.TasksList page. //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function IsAnyTaskSelected(): Boolean;
begin
  Result := not Bool(StrEquals(WizardSelectedTasks(False), '', {IgnoreCase} True));
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Checks or unchecks all task items.                                  //
//                                                                     //
// Due difficulties with Inno Setup API, this method checks/unchecks   //
// ALL checkboxes (except if they are disabled) and ALL radiobuttons.  //
//                                                                     //
// If you want to avoid checking radiobuttons, use                     //
// external .NET function 'SetCheckedStateInTasksListItems' instead.   //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure CheckOrUncheckkAllTasks(checked: Boolean);
var
  TasksListHandle: HWND;
  I: Integer;
  RadioButtonsCount: Integer;
  CbChangedCount: Integer;

begin
  TasksListHandle := WizardForm.TasksList.Handle;
  RadioButtonsCount := GetTasksListRadioButtonsCount(TasksListHandle, true);
  if RadioButtonsCount <> 0 then begin
    CbChangedCount := SetCheckedStateInTasksListItems(TasksListHandle, checked);
  end else begin
    for I := 0 to WizardForm.TasksList.Items.Count - 1 do begin
      if WizardForm.TasksList.ItemEnabled[I] then begin
        if checked then begin
          if not WizardForm.TasksList.Checked[I] then begin
            WizardForm.TasksList.Checked[I] := True;
          end;
        end else begin
          if WizardForm.TasksList.Checked[I] then begin
            WizardForm.TasksList.Checked[I] := False;
          end;
        end;
      end;
    end;
  end;

  // Disable "Next" button if ForceSelectAtLestOneTask option is enabled and there is no task selected.
  if ForceSelectAtLestOneTask = True then begin
    WizardForm.NextButton.Enabled := IsAnyTaskSelected;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Copies the bounds (position and size) of a source control (ASource) to a target control (ATarget).            //
//                                                                                                               //
// This is useful for aligning one control to another by matching their Left, Top, Width, and Height properties. //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure InheritBoundsRect(ASource, ATarget: TControl);
begin
  ATarget.Left := ASource.Left;
  ATarget.Top := ASource.Top;
  ATarget.Width := ASource.Width;
  ATarget.Height := ASource.Height;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// This function converts a COM BStr string into a usable string in Inno Setup.          //
//                                                                                       //
// A .NET function must return a COM BStr string, as shown in the following examples:    //
//                                                                                       //
// VB.NET:                                                                               //
// <DllExport(CallingConvention:=CallingConvention.StdCall)>                             //
// Public Shared Function MyFunction() As <MarshalAs(UnmanagedType.BStr)> String         //
//     Return "Hello World"                                                              //
// End Function                                                                          //
//                                                                                       //
// C#:                                                                                   //
// [DllExport(CallingConvention = CallingConvention.StdCall)]                            //
// [return: MarshalAs(UnmanagedType.BStr)]                                               //
// public static string MyFunction() {                                                   //
//     return "Hello World";                                                             //
// }                                                                                     //
//                                                                                       //
// The function can then be imported in Inno Setup as follows:                           //
//                                                                                       //
// function MyFunction(): Cardinal;                                                      //
//   external 'MyFunction@files:MyNetAPI.dll stdcall';                                   //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
Function ConvertBStrToInnoString(BStr: Cardinal): String;
var
  strLength: Cardinal;
  innoString: String;

begin
  strLength := lstrlenW(BStr);
  SetLength(innoString, strLength);
  if lstrcpyW(innoString, BStr) <> 0 then begin
    Result := innoString;
  end else begin
    MsgBox('Failed to convert BStr to Inno Setup string.', mbError, MB_OK);
  end;

end;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Custom Event-Handlers ------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Occurs when the 'AuthorWebsiteLabel' and 'AuthorWebsiteBitmap' controls are clicked //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure AuthorWebsiteControlClick(Sender: TObject);
var
  ErrorCode: Integer;
begin
  // This will open the specified url in the default web-browser.
  // https://stackoverflow.com/a/38934870/1248295
  ShellExec('open', '{#AuthorWebsite}', '', '', SW_SHOWNORMAL, ewNoWait, ErrorCode);
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Occurs when the 'Next' button in the 'CustomSelectDirPage' page is clicked  //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function CustomSelectDirPageNextButtonClick(Sender: TWizardPage): Boolean;
begin
  Result := True;

  // Ignore validations if installing in a temporary directory or in Windows directory.
  if StrContainsAny(WizardDirValue, ['{tmp}', '.tmp'], {IgnoreCase} True) then Exit;
  if StrContainsAny(WizardDirValue, ['{win}', ExpandConstant('{win}')], {IgnoreCase} True) then Exit;

  // Prevents the user from installing the program in a root directory if 'AllowRootDirectory' directive is set to 'False'.
  If not YesNoOrTrueFalseToBool('{#SetupSetting("AllowRootDirectory")}') and IsRootDir(WizardDirValue) then begin
    MsgBox('Setup does not allow {#StringChange(AppName, "'", "’")} to be installed on a root directory ("' + WizardDirValue + '").', mbCriticalError, MB_OK);
    Result := False;
    Exit;
  end;

  // Optionally, forces the user to install the program on a empty directory.
  // Result := IsDirEmpty(ExpandConstant(WizardDirValue));
  // If not Result then begin
  //   MsgBox('{#StringChange(AppName, "'", "’")} can only be installed to an empty directory.', mbCriticalError, MB_OK);
  //   Exit;
  // end;

  // Prevents the user from installing the program in a empty directory if 'DirExistsWarning' directive is set to 'True'.
  if (YesNoOrTrueFalseToBool('{#SetupSetting("DirExistsWarning")}')) and (DirExists(WizardDirValue))  then begin
    if MsgBox(FmtMessage(SetupMessage(msgDirExists), [WizardDirValue]), mbConfirmation, MB_YESNO) = IDNO then begin
      Result := false;
    end;
  end;

end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Occurs when the 'CheckAllTasksButton' button in the 'wpSelectTasks' page is clicked //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure CheckAllTasksButtonClick(Sender: TObject);
begin
  CheckAllTasksButton.Enabled := False;
  WizardForm.BackButton.Enabled := False;
  WizardForm.NextButton.Enabled := False;

  CheckOrUncheckkAllTasks(True);

  CheckAllTasksButton.Enabled := True;
  WizardForm.BackButton.Enabled := True;
  WizardForm.NextButton.Enabled := True;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Occurs when the 'CheckAllTasksButton' button in the 'wpSelectTasks' page is clicked //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure UncheckAllTasksButtonClick(Sender: TObject);
begin
  UncheckAllTasksButton.Enabled := False;
  WizardForm.BackButton.Enabled := False;
  WizardForm.NextButton.Enabled := False;

  CheckOrUncheckkAllTasks(False);

  UncheckAllTasksButton.Enabled := True;
  WizardForm.BackButton.Enabled := True;
  WizardForm.NextButton.Enabled := True;
end;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Custom UI Methods ----------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //



// - - - - - - - - - - - - - - - - - - - - - - //
// Creates the author website related controls //
// - - - - - - - - - - - - - - - - - - - - - - //
procedure CreateAuthorControls(AuthorWebsiteUrl: String);
var
  UseDarkTheme              : Boolean;
  InstallerAuthorLabel      : TLabel;
  AuthorWebsitePanel        : TPanel;
  AuthorWebsitePanelHeight  : Integer;
  AuthorWebsiteLabel        : TNewStaticText;
  AuthorWebsiteBitmap       : TBitmapImage;

begin
  if (AuthorWebsiteUrl <> '') then begin
    UseDarkTheme := IsDarkInstallMode();

    // Set AuthorWebsitePanel control properties (1).
    AuthorWebsitePanelHeight := ScaleY(64);

    AuthorWebsitePanel            := TPanel.Create(WizardForm);
    AuthorWebsitePanel.Parent     := WizardForm.WelcomePage;
    AuthorWebsitePanel.Height     := AuthorWebsitePanelHeight;
    AuthorWebsitePanel.Left       := WizardForm.WelcomeLabel2.Left;
    AuthorWebsitePanel.Top        := WizardForm.OuterNotebook.Top + WizardForm.OuterNotebook.ClientHeight - AuthorWebsitePanelHeight - ScaleY(8);
    AuthorWebsitePanel.Width      := WizardForm.WelcomePage.Width - WizardForm.WelcomeLabel2.Left;
    AuthorWebsitePanel.Color      := WizardForm.WelcomePage.Color;
    AuthorWebsitePanel.BevelOuter := bvLowered;
    AuthorWebsitePanel.Anchors    := [akRight, akBottom];

    // Set AuthorWebsiteBitmap control properties.
    AuthorWebsiteBitmap         := TBitmapImage.Create(WizardForm);
    AuthorWebsiteBitmap.Parent  := AuthorWebsitePanel;
    AuthorWebsiteBitmap.Left    := ScaleX(4);
    AuthorWebsiteBitmap.Top     := ScaleY(4);
    if UseDarkTheme then begin
      ExtractTemporaryFiles('{tmp}\WizardBitmaps\AuthorWebsiteDark.bmp');
      AuthorWebsiteBitmap.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\AuthorWebsiteDark.bmp'));
    end else begin
      ExtractTemporaryFiles('{tmp}\WizardBitmaps\AuthorWebsiteLight.bmp');
      AuthorWebsiteBitmap.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\AuthorWebsiteLight.bmp'));
    end;
    AuthorWebsiteBitmap.AutoSize := True;
    AuthorWebsiteBitmap.Cursor   := crHand;
    AuthorWebsiteBitmap.Visible  := True;
    AuthorWebsiteBitmap.OnClick  := @AuthorWebsiteControlClick;
    AuthorWebsiteBitmap.Anchors  := [akLeft, akBottom];

    // Set AuthorWebsiteLabel control properties.
    AuthorWebsiteLabel          := TNewStaticText.Create(WizardForm);
    AuthorWebsiteLabel.Parent   := AuthorWebsitePanel;
    AuthorWebsiteLabel.Left     := AuthorWebsiteBitmap.Left + AuthorWebsiteBitmap.Width + ScaleX(8);
    AuthorWebsiteLabel.Top      := AuthorWebsiteBitmap.Top;
    AuthorWebsiteLabel.Cursor   := crHand;
    AuthorWebsiteLabel.OnClick  := @AuthorWebsiteControlClick;
    AuthorWebsiteLabel.WordWrap := True;
    AuthorWebsiteLabel.AutoSize := False;
    AuthorWebsiteLabel.Width    := AuthorWebsitePanel.ClientWidth - AuthorWebsiteLabel.Left - ScaleX(8);
    AuthorWebsiteLabel.Height   := AuthorWebsitePanel.ClientHeight - AuthorWebsiteBitmap.Top;
    AuthorWebsiteLabel.Caption  := CustomMessage('SetupOpenAuthorWebsite');
    AuthorWebsiteLabel.Visible  := True
    AuthorWebsiteLabel.Anchors  := [akLeft, akRight, akBottom];
  end;

  // Set InstallerAuthorLabel control properties.
  InstallerAuthorLabel          := TLabel.Create(WizardForm);
  InstallerAuthorLabel.Parent   := WizardForm;
  InstallerAuthorLabel.Left     := ScaleX(2);
  InstallerAuthorLabel.Top      := WizardForm.NextButton.Top + WizardForm.NextButton.Height div 2 + ScaleY(10) - ScaleY(2);
  InstallerAuthorLabel.Font     := WizardForm.Font;
  InstallerAuthorLabel.Anchors  := [akLeft, akBottom];
  InstallerAuthorLabel.WordWrap := False;
  InstallerAuthorLabel.Visible  := True;
  InstallerAuthorLabel.Caption  := CustomMessage('SetupMadeBy');
  InstallerAuthorLabel.BringToFront();
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Creates a control that serves as a password hint for the password textbox.  //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure CreatePasswordHintControl();
var
  PasswordHintLabel  : TNewStaticText;

begin
  // Exit if no password has been specified.
  if ('{SetupSetting("Password"))' = '') then exit;

  // Set PasswordHintLabel control properties...
  PasswordHintLabel         := TNewStaticText.Create(WizardForm);
  PasswordHintLabel.Parent  := WizardForm.PasswordPage;
  PasswordHintLabel.Left    := WizardForm.PasswordEdit.Left;
  PasswordHintLabel.Top     := WizardForm.PasswordEdit.Top + WizardForm.PasswordEdit.Height + ScaleY(10);
  PasswordHintLabel.Anchors := [akLeft, akTop];
  PasswordHintLabel.Caption := CustomMessage('SetupPasswordHint');
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Creates a custom wizard page to select program install folder, and start menu group folder. //
//                                                                                             //
// This page is a full replacement for both 'wpSelectDir' and 'wpSelectProgramGroup' pages.    //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure CreateCustomSelectDirPage(CrateShortCutCheckBox: Boolean; ForceDisableDirControls: Boolean; ForceDisableGroupControls: Boolean);
var
  WizardStyle: String; // Determines the wizard style, 'modern' or 'classic'.

  // Pages
  ThisPage: TWizardPage;
  SelectDirPage: TWizardPage;
  SelectProgramGroupPage: TWizardPage;

begin
  // Abort custom page creation if both 'DisableDirPage' and 'DisableProgramGroupPage' settings are True.
  If YesNoOrTrueFalseToBool('{#SetupSetting("DisableDirPage")}') and YesNoOrTrueFalseToBool('{#SetupSetting("DisableProgramGroupPage")}') then begin
    Exit;
  end;

  WizardStyle := LowerCase('{#SetupSetting("WizardStyle")}');

  // Pages
  SelectDirPage          := PageFromID(wpSelectDir);
  SelectProgramGroupPage := PageFromID(wpSelectProgramGroup);

  If YesNoOrTrueFalseToBool('{#SetupSetting("DisableDirPage")}') then begin
    ThisPage := CreateCustomPage(wpInfoBefore, SelectProgramGroupPage.Caption, SelectProgramGroupPage.Description);
  end else if YesNoOrTrueFalseToBool('{#SetupSetting("DisableProgramGroupPage")}') then begin
    ThisPage := CreateCustomPage(wpInfoBefore, SelectDirPage.Caption, SelectDirPage.Description);
  end else begin
    ThisPage := CreateCustomPage(wpInfoBefore, SelectDirPage.Caption + ' | ' + SelectProgramGroupPage.Caption,
                                               SelectDirPage.Description + #13#10 + SelectProgramGroupPage.Description);
  end;

  CustomSelectDirPage    := ThisPage;

  // wpSelectDir Control Parenting
  WizardForm.SelectDirBitmapImage.Parent := ThisPage.Surface;
  WizardForm.SelectDirLabel.Parent       := ThisPage.Surface;
  WizardForm.SelectDirBrowseLabel.Parent := ThisPage.Surface;
  WizardForm.DirEdit.Parent              := ThisPage.Surface;
  WizardForm.DirBrowseButton.Parent      := ThisPage.Surface;
  WizardForm.DiskSpaceLabel.Parent       := ThisPage.Surface;

  // wpSelectProgramGroup Control Parenting
  WizardForm.SelectGroupBitmapImage.Parent           := ThisPage.Surface;
  WizardForm.SelectStartMenuFolderLabel.Parent       := ThisPage.Surface;
  WizardForm.SelectStartMenuFolderBrowseLabel.Parent := ThisPage.Surface;
  WizardForm.GroupEdit.Parent                        := ThisPage.Surface;
  WizardForm.GroupBrowseButton.Parent                := ThisPage.Surface;
  WizardForm.NoIconsCheck.Parent                     := ThisPage.Surface;

  // Anchors adjustements to preserve control positions when 'WizardResizable' setting is True.
  WizardForm.NoIconsCheck.Anchors := [akLeft, akTop];
  WizardForm.DiskSpaceLabel.Anchors := [akLeft, akTop];

  // Hide directory selection controls when 'DisableDirPage' setting is True.
  If YesNoOrTrueFalseToBool('{#SetupSetting("DisableDirPage")}') then begin
    WizardForm.DirEdit.Visible              := False;
    WizardForm.DirBrowseButton.Visible      := False;
    WizardForm.SelectDirBitmapImage.Visible := False;
    WizardForm.SelectDirLabel.Visible       := False;
    WizardForm.SelectDirBrowseLabel.Visible := False;
    WizardForm.DiskSpaceLabel.Visible       := False;
  end;

  // Hide group selection controls when 'DisableProgramGroupPage' setting is True.
  If YesNoOrTrueFalseToBool('{#SetupSetting("DisableProgramGroupPage")}') then begin
    WizardForm.GroupEdit.Visible                        := False;
    WizardForm.GroupBrowseButton.Visible                := False;
    WizardForm.NoIconsCheck.Visible                     := False;
    WizardForm.SelectGroupBitmapImage.Visible           := False;
    WizardForm.SelectStartMenuFolderLabel.Visible       := False;
    WizardForm.SelectStartMenuFolderBrowseLabel.Visible := False;
  end;

  // Create a 'Create a desktop shortcut' checkbox.
  DesktopShortcutCheckBox := TNewCheckBox.Create(ThisPage);
  if CrateShortCutCheckBox then begin
    If not YesNoOrTrueFalseToBool('{#SetupSetting("DisableDirPage")}') then begin
      DesktopShortcutCheckBox.Parent  := ThisPage.Surface;
      DesktopShortcutCheckBox.Caption := CustomMessage('CreateDesktopIcon');
      DesktopShortcutCheckBox.Checked := True;
      DesktopShortcutCheckBox.Width   := WizardForm.NoIconsCheck.Width;
      DesktopShortcutCheckBox.Anchors := [akLeft, akTop];
    end;
  end;

  // Control Positioning
  If YesNoOrTrueFalseToBool('{#SetupSetting("DisableDirPage")}') then begin
    WizardForm.SelectGroupBitmapImage.Top           := WizardForm.SelectDirBitmapImage.Top;
    WizardForm.SelectStartMenuFolderLabel.Top       := WizardForm.SelectGroupBitmapImage.Top;
    WizardForm.SelectStartMenuFolderBrowseLabel.Top := WizardForm.SelectStartMenuFolderLabel.Top + ScaleX(35);
    WizardForm.GroupEdit.Top                        := WizardForm.SelectStartMenuFolderBrowseLabel.Top + ScaleX(35);
    WizardForm.GroupBrowseButton.Top                := WizardForm.GroupEdit.Top;
    WizardForm.NoIconsCheck.Top                     := WizardForm.GroupEdit.Top + ScaleX(30);
  end else begin
    WizardForm.SelectDirBrowseLabel.Visible             := False;
    WizardForm.SelectStartMenuFolderBrowseLabel.Visible := False;
    WizardForm.SelectDirLabel.Top                   := WizardForm.SelectDirBitmapImage.Top;
    WizardForm.DirEdit.Top                          := WizardForm.SelectDirLabel.Top + ScaleX(35);
    WizardForm.DirBrowseButton.Top                  := WizardForm.DirEdit.Top;
    DesktopShortcutCheckBox.Top                     := WizardForm.DirEdit.Top + ScaleX(30);
    WizardForm.DiskSpaceLabel.Top                   := DesktopShortcutCheckBox.Top + ScaleX(25);
    WizardForm.NoIconsCheck.Top                     := WizardForm.InnerPage.ClientHeight - ScaleX(115);
    WizardForm.GroupBrowseButton.Top                := WizardForm.NoIconsCheck.Top - ScaleX(28);
    WizardForm.GroupEdit.Top                        := WizardForm.GroupBrowseButton.Top;
    WizardForm.SelectStartMenuFolderLabel.Top       := WizardForm.GroupEdit.Top - ScaleX(40);
    WizardForm.SelectGroupBitmapImage.Top           := WizardForm.SelectStartMenuFolderLabel.Top;
  end;

  // Load custom bitmap images
  if IsDarkInstallMode() then begin
    ExtractTemporaryFiles('{tmp}\WizardBitmaps\FolderDark.bmp');
    WizardForm.SelectDirBitmapImage.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\FolderDark.bmp'));
    ExtractTemporaryFiles('{tmp}\WizardBitmaps\StartMenuDark.bmp');
    WizardForm.SelectGroupBitmapImage.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\StartMenuDark.bmp'));
  end else begin
    ExtractTemporaryFiles('{tmp}\WizardBitmaps\FolderLight.bmp');
    WizardForm.SelectDirBitmapImage.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\FolderLight.bmp'));
    ExtractTemporaryFiles('{tmp}\WizardBitmaps\StartMenuLight.bmp');
    WizardForm.SelectGroupBitmapImage.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\StartMenuLight.bmp'));
  end;

  // Disable 'DirEdit' and 'DirBrowseButton' controls if 'WizardGroupValue' contains a temp directory or Windows directory.
  WizardForm.DirEdit.Enabled := not StrContainsAny(WizardDirValue, ['{tmp}', '.tmp', '{win}', '\windows\'], {IgnoreCase} True)
  WizardForm.DirBrowseButton.Enabled := WizardForm.DirEdit.Enabled

  // Disable 'DirEdit' and 'DirBrowseButton' controls if specified in the function parameter.
  if ForceDisableDirControls then begin
    WizardForm.DirEdit.Enabled         := False;
    WizardForm.DirBrowseButton.Enabled := False;
  end;

  // Disable 'GroupEdit', 'GroupBrowseButton' and 'NoIconsCheck' controls if 'WizardGroupValue' is the default / not set.
  WizardForm.GroupEdit.Enabled         := not StrContains(WizardGroupValue, '(default)', {IgnoreCase} True)
  WizardForm.GroupBrowseButton.Enabled := WizardForm.GroupEdit.Enabled
  WizardForm.NoIconsCheck.Enabled      := WizardForm.GroupEdit.Enabled

  // Disable 'GroupEdit', 'GroupBrowseButton' and 'NoIconsCheck' controls if specified in the function parameter.
  if ForceDisableGroupControls then begin
    WizardForm.GroupEdit.Enabled         := False;
    WizardForm.GroupBrowseButton.Enabled := False;
    WizardForm.NoIconsCheck.Enabled      := False;
  end;

  // Set tab Order
  WizardForm.DirEdit.TabOrder           := 900;
  WizardForm.DirBrowseButton.TabOrder   := 901;
  DesktopShortcutCheckBox.TabOrder      := 902;
  WizardForm.GroupEdit.TabOrder         := 903;
  WizardForm.GroupBrowseButton.TabOrder := 904;
  WizardForm.NoIconsCheck.TabOrder      := 905;
  WizardForm.DiskSpaceLabel.TabOrder    := 906;

  // Set page event-handlers
  ThisPage.OnNextButtonClick := @CustomSelectDirPageNextButtonClick;

end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Determines whether the user has choosed to create a desktop shortcut of the program executable. //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function IsCreateDesktopShortcutChecked(): Boolean;
begin
  Result := not (CustomSelectDirPage = nil) and DesktopShortcutCheckBox.Checked;
end;

// - - - - - - - - - - - - - - - - - - - - - //
// Personalize the header of the wizard form //
// - - - - - - - - - - - - - - - - - - - - - //
procedure PersonalizeHeader();
var
  UseDarkTheme: Boolean;
begin
  UseDarkTheme := IsDarkInstallMode();

  // Load header background image.
  HeaderBackgroundBitmap          := TBitmapImage.Create(WizardForm);
  HeaderBackgroundBitmap.Parent   := WizardForm.MainPanel;
  HeaderBackgroundBitmap.AutoSize := False;
  HeaderBackgroundBitmap.Stretch  := True;
  HeaderBackgroundBitmap.Top      := 0;
  HeaderBackgroundBitmap.Left     := 0;
  HeaderBackgroundBitmap.Width    := WizardForm.ClientWidth;
  HeaderBackgroundBitmap.Height   := WizardForm.Height - WizardForm.OuterNotebook.Height;
  HeaderBackgroundBitmap.Anchors  := [akLeft, akRight, akTop];

  if UseDarkTheme then begin
    ExtractTemporaryFiles('{tmp}\WizardBitmaps\BackgroundHeaderDark.bmp');
    HeaderBackgroundBitmap.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\BackgroundHeaderDark.bmp'));
  end else begin
    ExtractTemporaryFiles('{tmp}\WizardBitmaps\BackgroundHeaderLight.bmp');
    HeaderBackgroundBitmap.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\BackgroundHeaderLight.bmp'));
  end;

  WizardForm.WizardSmallBitmapImage.BringToFront();
end;

// - - - - - - - - - - - - - - - - - - - - - //
// Personalize the footer of the wizard form //
// - - - - - - - - - - - - - - - - - - - - - //
procedure PersonalizeFooter();
var
  UseDarkTheme: Boolean;
begin
  UseDarkTheme := IsDarkInstallMode();

  // Fix Back/Next button positions when using VCL Styles.
  WizardForm.BackButton.Top := WizardForm.CancelButton.Top;
  WizardForm.NextButton.Top := WizardForm.CancelButton.Top;

  // Load footer background image.
  FooterBackgroundBitmap          := TBitmapImage.Create(WizardForm);
  FooterBackgroundBitmap.Parent   := WizardForm;
  FooterBackgroundBitmap.AutoSize := False;
  FooterBackgroundBitmap.Stretch  := True;
  FooterBackgroundBitmap.Top      := WizardForm.OuterNotebook.Top + WizardForm.OuterNotebook.Height;
  FooterBackgroundBitmap.Left     := 0;
  FooterBackgroundBitmap.Width    := WizardForm.ClientWidth;
  FooterBackgroundBitmap.Height   := WizardForm.ClientHeight - FooterBackgroundBitmap.Top;
  FooterBackgroundBitmap.Anchors  := [akLeft, akRight, akBottom];
  // Send the bitmap to back, to be able see the "Made by ElektroStudios" label control.
  FooterBackgroundBitmap.SendToBack();

  if UseDarkTheme then begin
    ExtractTemporaryFiles('{tmp}\WizardBitmaps\BackgroundFooterDark.bmp');
    FooterBackgroundBitmap.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\BackgroundFooterDark.bmp'));
  end else begin
    ExtractTemporaryFiles('{tmp}\WizardBitmaps\BackgroundFooterLight.bmp');
    FooterBackgroundBitmap.Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardBitmaps\BackgroundFooterLight.bmp'));
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Updates the properties of the source TLabel control in the wizard header, //
// by inheriting the properties of the specified TNewStaticText control.     //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure UpdateTransparentLabelInHeader(var labelControl: TLabel; staticTextControl: TNewStaticText);
begin
  if labelControl = nil then begin
    labelControl        := TLabel.Create(WizardForm);
    labelControl.Parent := staticTextControl.Parent;
    labelControl.Font   := staticTextControl.Font;
  end;
  labelControl.AutoSize := staticTextControl.AutoSize;
  labelControl.WordWrap := staticTextControl.WordWrap;
  labelControl.Anchors  := staticTextControl.Anchors;
  InheritBoundsRect(staticTextControl, labelControl);
  labelControl.Caption  := staticTextControl.Caption;
  labelControl.Visible  := True;

  staticTextControl.Visible := False;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Updates the properties of the source TLabel control in a wizard page, //
// by inheriting the properties of the specified TNewStaticText control. //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure UpdateTransparentLabelInPage(var labelControl: TLabel; staticTextControl: TNewStaticText);
begin
  if labelControl = nil then begin
    labelControl        := TLabel.Create(WizardForm);
    labelControl.Parent := staticTextControl.Parent;
    labelControl.Font   := staticTextControl.Font;
    labelControl.Visible:= staticTextControl.Visible;
  end;
  labelControl.AutoSize := staticTextControl.AutoSize;
  labelControl.WordWrap := staticTextControl.WordWrap;
  labelControl.Caption  := staticTextControl.Caption;
  labelControl.Anchors  := staticTextControl.Anchors;

  staticTextControl.Visible := False;
  InheritBoundsRect(staticTextControl, labelControl);
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Updates the properties of the transparent labels in the wizard header //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure UpdateTransparentLabelsInHeader();
begin
  UpdateTransparentLabelInHeader(TransparentBeveledLabel, WizardForm.BeveledLabel);
  UpdateTransparentLabelInHeader(TransparentPageDescriptionLabel, WizardForm.PageDescriptionLabel);
  UpdateTransparentLabelInHeader(TransparentPageNameLabel, WizardForm.PageNameLabel);
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Updates the properties of the transparent labels in the specified wizard page //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure UpdateTransparentLabelsInPage(CurPageID: Integer);
begin

  case CurPageID of

    wpWelcome:
      begin
        UpdateTransparentLabelInPage(TransparentWelcomeLabel1, WizardForm.WelcomeLabel1);
        UpdateTransparentLabelInPage(TransparentWelcomeLabel2, WizardForm.WelcomeLabel2);
      end;

    wpLicense:
      begin
        UpdateTransparentLabelInPage(TransparentLicenseLabel1, WizardForm.LicenseLabel1);
      end;

    wpPassword:
      begin
        UpdateTransparentLabelInPage(TransparentPasswordEditLabel, WizardForm.PasswordEditLabel);
        UpdateTransparentLabelInPage(TransparentPasswordLabel, WizardForm.PasswordLabel);
      end;

    wpInfoBefore:
      begin
        UpdateTransparentLabelInPage(TransparentInfoBeforeClickLabel, WizardForm.InfoBeforeClickLabel);
      end;

    wpUserInfo:
      begin
        UpdateTransparentLabelInPage(TransparentUserInfoNameLabel, WizardForm.UserInfoNameLabel);
        UpdateTransparentLabelInPage(TransparentUserInfoOrgLabel, WizardForm.UserInfoOrgLabel);
        UpdateTransparentLabelInPage(TransparentUserInfoSerialLabel, WizardForm.UserInfoSerialLabel);
      end;

    wpSelectDir:
      begin
        UpdateTransparentLabelInPage(TransparentSelectDirLabel, WizardForm.SelectDirLabel);
        UpdateTransparentLabelInPage(TransparentSelectDirBrowseLabel, WizardForm.SelectDirBrowseLabel);
        UpdateTransparentLabelInPage(TransparentDiskSpaceLabel, WizardForm.DiskSpaceLabel);
      end;

    wpSelectComponents:
      begin
        UpdateTransparentLabelInPage(TransparentSelectComponentsLabel, WizardForm.SelectComponentsLabel);
        UpdateTransparentLabelInPage(TransparentComponentsDiskSpaceLabel, WizardForm.ComponentsDiskSpaceLabel);
      end;

    wpSelectProgramGroup:
      begin
        UpdateTransparentLabelInPage(TransparentSelectStartMenuFolderLabel, WizardForm.SelectStartMenuFolderLabel);
        UpdateTransparentLabelInPage(TransparentSelectStartMenuFolderBrowseLabel, WizardForm.SelectStartMenuFolderBrowseLabel);
      end;

    wpSelectTasks:
      begin
        UpdateTransparentLabelInPage(TransparentSelectTasksLabel, WizardForm.SelectTasksLabel);
      end;

    wpReady:
      begin
        UpdateTransparentLabelInPage(TransparentReadyLabel, WizardForm.ReadyLabel);
      end;

    wpPreparing:
      begin
        UpdateTransparentLabelInPage(TransparentPreparingLabel, WizardForm.PreparingLabel);
      end;

    wpInstalling:
      begin
        // UpdateTransparentLabelInPage(TransparentStatusLabel, WizardForm.StatusLabel);
        // UpdateTransparentLabelInPage(TransparentFilenameLabel, WizardForm.FilenameLabel);
      end;

    wpInfoAfter:
      begin
        UpdateTransparentLabelInPage(TransparentInfoAfterClickLabel, WizardForm.InfoAfterClickLabel);
      end;

    wpFinished:
      begin
        UpdateTransparentLabelInPage(TransparentFinishedHeadingLabel, WizardForm.FinishedHeadingLabel);
        UpdateTransparentLabelInPage(TransparentFinishedLabel, WizardForm.FinishedLabel);
      end;

  else // Handle custom pages here...
    if (CustomSelectDirPage <> nil) and (PageFromID(CurPageID) = CustomSelectDirPage) then begin
        UpdateTransparentLabelInPage(TransparentSelectDirLabel, WizardForm.SelectDirLabel);
        UpdateTransparentLabelInPage(TransparentSelectDirBrowseLabel, WizardForm.SelectDirBrowseLabel);
        UpdateTransparentLabelInPage(TransparentDiskSpaceLabel, WizardForm.DiskSpaceLabel);
        UpdateTransparentLabelInPage(TransparentSelectStartMenuFolderLabel, WizardForm.SelectStartMenuFolderLabel);
        UpdateTransparentLabelInPage(TransparentSelectStartMenuFolderBrowseLabel, WizardForm.SelectStartMenuFolderBrowseLabel);
        Exit;
    end;

  end;
end;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Installer Events ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

// - - - - - - - - - - - - - - - - - - - //
// Occurs when the installer initializes //
// - - - - - - - - - - - - - - - - - - - //
function InitializeSetup(): Boolean;
begin

  Result := True;
end;

// - - - - - - - - - - - - - - - - - - - - //
// Occurs when the installer deinitializes //
// - - - - - - - - - - - - - - - - - - - - //
procedure DeinitializeSetup();
begin

  If FileExists(ExpandConstant('{tmp}\InnoHelper.dll')) then begin
    UnloadDll(ExpandConstant('{tmp}\InnoHelper.dll'));
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - //
// Occurs when the current step has changed  //
// - - - - - - - - - - - - - - - - - - - - - //
procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then begin
    if WizardIsTaskSelected('envPath') then PathEnvAddDir(ExpandConstant('{app}'), True);
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - //
// Occurs when the current wizard page has changed //
// - - - - - - - - - - - - - - - - - - - - - - - - //
procedure CurPageChanged(CurPageID: Integer);
begin

  UpdateTransparentLabelsInHeader();
  // UpdateTransparentLabelsInPage(CurPageID);

  if CurPageID = wpSelectTasks then begin
    // We have entered the tasks selection page,
    // Here you can disable task controls ny calling DisableTaskControlsByDescription() or DisableTaskControlsByIndex() methods.

    // Disable "Next" button if ForceSelectAtLestOneTask option is enabled and there is no task selected.
    if ForceSelectAtLestOneTask = True then begin
      WizardForm.NextButton.Enabled := IsAnyTaskSelected;
    end;

    // Create 'Select all tasks' and 'Deselect all tasks' buttons.
    if (not TaskButtonsCreated) then begin
      CheckAllTasksButton         := TNewButton.Create(WizardForm);
      CheckAllTasksButton.Parent  := WizardForm;
      CheckAllTasksButton.Width   := ScaleX(100);
      CheckAllTasksButton.Height  := WizardForm.NextButton.Height;
      CheckAllTasksButton.Left    := ScaleX(1);
      CheckAllTasksButton.Top     := WizardForm.NextButton.Top - WizardForm.NextButton.Height div 2;
      CheckAllTasksButton.Anchors := [akLeft, akBottom];
      CheckAllTasksButton.OnClick := @CheckAllTasksButtonClick;

      UncheckAllTasksButton         := TNewButton.Create(WizardForm);
      UncheckAllTasksButton.Parent  := WizardForm;
      UncheckAllTasksButton.Width   := ScaleX(100);
      UncheckAllTasksButton.Height  := WizardForm.NextButton.Height;
      UncheckAllTasksButton.Left    := CheckAllTasksButton.Left + CheckAllTasksButton.Width;
      UncheckAllTasksButton.Top     := CheckAllTasksButton.Top;
      UncheckAllTasksButton.Anchors := [akLeft, akBottom];
      UncheckAllTasksButton.OnClick := @UncheckAllTasksButtonClick;

      CheckAllTasksButton.Caption := CustomMessage('SetupSelectAllTasks');
      UncheckAllTasksButton.Caption := CustomMessage('SetupUnselectAllTasks');

      TaskButtonsCreated := True;
    end else begin
      CheckAllTasksButton.Visible := True;
      UncheckAllTasksButton.Visible := True;
    end;
  end;

  // Hide CheckAllTasksButton and UncheckAllTasksButton buttons if current page is not 'wpSelectTasks'.
  if (CurPageID <> wpSelectTasks) and (TaskButtonsCreated) then begin
    CheckAllTasksButton.Visible := False;
    UncheckAllTasksButton.Visible := False;
  end;

end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Occurs when a task item is checked or unchecked in the WizardForm.TasksList page  //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure TaskOnClickCheck(Sender: TObject);
begin
  if ForceSelectAtLestOneTask = True then begin
    WizardForm.NextButton.Enabled := IsAnyTaskSelected;
  end;
end;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Installer Event Functions ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
//  It is called automatically to determine whether a wizard page should be skipped / not shown  //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function ShouldSkipPage(PageID: Integer): Boolean;
begin
  // Force to skip the 'wpSelectDir' and 'wpSelectProgramGroup' pages if 'CustomSelectDirPage' is not nul.
  If ((PageID = wpSelectDir) or (PageID = wpSelectProgramGroup)) and (CustomSelectDirPage <> nil) then begin
    Result := True;
  end;
end;

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// It is called automatically when the Ready to Install wizard page becomes the active page.           //
//                                                                                                     //
// It should return the text to be displayed in the settings memo on the Ready to Install wizard page. //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
function UpdateReadyMemo(
  Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo,
  MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
var
  additionalText: string;
begin
  if Length(MemoUserInfoInfo) > 0 then begin
    Result := Result + MemoUserInfoInfo + NewLine + NewLine;
  end;

  // Force to skip the the MemoDirInfo if 'WizardDirValue' contains a temp directory.
  if (Length(MemoDirInfo) > 0) and not StrContainsAny(WizardDirValue, ['{tmp}', '.tmp'], {IgnoreCase} True) then begin
    Result := Result + MemoDirInfo + NewLine + NewLine;
  end;

  if Length(MemoTypeInfo) > 0 then begin
    Result := Result + MemoTypeInfo + NewLine + NewLine;
  end;

  if Length(MemoComponentsInfo) > 0 then begin
    Result := Result + MemoComponentsInfo + NewLine + NewLine;
  end;

  // This is necessary when 'CreateCustomSelectDirPage' method is called.
  if not (WizardNoIcons) and (CustomSelectDirPage <> nil) and not StrContains(WizardGroupValue, '(default)', {IgnoreCase} True) then begin
    additionalText := WizardGroupValue;
    StringChange(additionalText, '&', '');
    if (Length(MemoGroupInfo) = 0) then begin
      Result := Result + SetupMessage(msgReadyMemoGroup) + NewLine + Space + additionalText + NewLine + NewLine;
    end else begin
      Result := Result + MemoGroupInfo + NewLine + NewLine;
    end;
  end else if (Length(MemoGroupInfo) > 0) and not StrContains(WizardGroupValue, '(default)', {IgnoreCase} True) then begin
      Result := Result + MemoGroupInfo + NewLine + NewLine;
  end;

  // This is necessary when 'CreateCustomSelectDirPage' method is called.
  if not (DesktopShortcutCheckBox = nil) and (DesktopShortcutCheckBox.Checked) then begin
    additionalText := DesktopShortcutCheckBox.Caption;
    StringChange(additionalText, '&', '');
    if (Length(MemoTasksInfo) = 0) then begin
      Result := Result + SetupMessage(msgReadyMemoTasks) + NewLine + Space + additionalText + NewLine + NewLine;
    end else begin
      Result := Result + MemoTasksInfo + NewLine + Space + additionalText + NewLine + NewLine;
    end;
  end else if (Length(MemoTasksInfo) > 0) then begin
      Result := Result + MemoTasksInfo + NewLine + NewLine;
  end;

end;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// Uninstaller Events ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

// - - - - - - - - - - - - - - - - - - - - //
// Occurs when the uninstaller initializes //
// - - - - - - - - - - - - - - - - - - - - //
function InitializeUninstall: Boolean;
begin

  Result := True;
end;

// - - - - - - - - - - - - - - - - - - - - - //
// Occurs when the uninstaller deinitializes //
// - - - - - - - - - - - - - - - - - - - - - //
procedure DeinitializeUninstall();
begin

  If FileExists(ExpandConstant('{tmp}\InnoHelper.dll')) then begin
    UnloadDll(ExpandConstant('{tmp}\InnoHelper.dll'));
  end;

end;

// - - - - - - - - - - - - - - - - - - - - - - - - - //
// Occurs when the uninstaller current page changes  //
// - - - - - - - - - - - - - - - - - - - - - - - - - //
procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
  DeleteCustomFoldersEnabled: Boolean;
  FolderPath: String;
  FolderDescriptionEN: String;
  FolderDescriptionES: String;
  FoldersToDelete: array of string;
  Tokens: array of string;
  Index: Integer;
  DeleteFolderString: String;
  SelectedTasksHKLM: String;
  SelectedTasksHKCU: String;
begin
  case CurUninstallStep of
    usPostUninstall: begin
    end;

    usDone: begin
      UninstallSuccess:= True;
    end;

    usUninstall: begin
      // Remove the application directory from PATH environment variable.
      RegQueryStringValue(HKLM, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\' + '{#SetupSetting("AppId")}' + '_is1', 'Inno Setup: Selected Tasks', SelectedTasksHKLM)
      RegQueryStringValue(HKCU, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\' + '{#SetupSetting("AppId")}' + '_is1', 'Inno Setup: Selected Tasks', SelectedTasksHKCU)
      if StrContains(SelectedTasksHKLM + SelectedTasksHKCU, 'envpath', {IgnoreCase} True) then PathEnvDelDir(ExpandConstant('{app}'), True);

      if not DeleteCustomFoldersEnabled then Exit; // Exits from this block if DeleteCustomFoldersEnabled = False

      // Set custom folders to delete (Path|Description)
      SetArrayLength(FoldersToDelete, 10);
      FoldersToDelete[0] := ExpandConstant('{userappdata}\MyProgramFolder') + '|current user settings|la configuración de usuario actual';
      FoldersToDelete[1] := ExpandConstant('{app}\tmp')                     + '|program temp files|los archivos temporales del programa';

      // Delete custom folders
      for Index := 0 to Length(foldersToDelete) - 1 do begin
        if foldersToDelete[Index] = '' then Continue; // Ignore empty array items.
        Tokens := StrSplit(FoldersToDelete[Index], '|');
        FolderPath := Tokens[0];
        FolderDescriptionEN := Tokens[1];
        FolderDescriptionES := Tokens[2];

        if DirExists(FolderPath) then begin
          if ActiveLanguage = 'es' then begin
              DeleteFolderString := CustomMessage('SetupQuestionDeleteFolder') + ' ' + FolderDescriptionES + '?';
          end else begin
              DeleteFolderString := CustomMessage('SetupQuestionDeleteFolder') + ' ' + FolderDescriptionEN + '?';
          end;

          if MsgBox(DeleteFolderString, mbConfirmation, MB_YESNO or MB_DEFBUTTON2) = IDYES then begin
            DelTree(FolderPath, True, True, True);
          end;
        end; // DirExists
      end; // for
    end; // usUninstall
  end; // case CurUninstallStep
end;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// InitializeWizard Event ------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
// Occurs when the wizard initializes                                                //
// Use this event function to make changes to the wizard or wizard pages at startup. //
// At the time this event is triggered, the wizard form does not yet exist.          //
// https://jrsoftware.org/ishelp/index.php?topic=scriptevents                        //
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - //
<event('InitializeWizard')>
procedure InitializeWizard1();
begin
  // No need to call CreateAuthorControls(), CreatePasswordHintControl() and CreateCustomSelectDirPage() if wizard has no GUI or if it is uninstalling.
  if WizardSilent or IsUninstaller then Exit;

  // Task selection configuration.
  ForceSelectAtLestOneTask := False;
  If ForceSelectAtLestOneTask = True then begin
    WizardForm.TasksList.OnClickCheck := @TaskOnClickCheck;
  end;

  // Page customizations.
  CreateAuthorControls(ExpandConstant('{#AuthorWebsite}'));
  CreatePasswordHintControl();
  CreateCustomSelectDirPage({CrateShortCutCheckBox} True, {DisableDirControls} True, {DisableGroupControls} False);
  PersonalizeHeader()
  PersonalizeFooter()
end;
