# PATHS Change Log 📋

## v1.5 (2026) *(current)* 🆕
#### 🌟 Improvements:
    • Added a unit test project (MSTest) that covers the PATH/PATHEXT entry manipulation, the registry operations (on a temporary sandbox key), the registry script export, the colored console output, the help text and the command-line behavior.
    • PATH and PATHEXT values are now read and written without expanding environment variables, preserving entries such as "%SystemRoot%\system32" and the original value kind (REG_EXPAND_SZ / REG_SZ).
    • /Export now writes the values as REG_EXPAND_SZ (hex(2)), preserving environment variables and special characters. Values that do not exist are omitted instead of being exported as empty values.
    • Switches that modify PATH or PATHEXT now notify Windows about the environment change (WM_SETTINGCHANGE), so new processes see the changes without logging off.
    • A friendly error message is shown when administrator privileges are required to modify the local machine PATH or PATHEXT, instead of crashing with an unhandled exception.
    • Directory comparisons now expand environment variables and ignore trailing separators and surrounding quotes (e.g. "C:\Tools\" is the same as "c:\tools").
    • /AddExt now validates the file extension and shows an error message for invalid ones.
    • /Del -User and /Del -Machine now report the valid index range when the entry index is out of range.

#### 🐛 Bug Fixes:
    • /DelExt wrote the remaining PATHEXT entries into the PATH value, destroying the PATH.
    • File extensions without leading dot were never normalized, so /AddExt "PY" stored "PY", and /DelExt "PY" did not find ".PY".
    • /AddExt appended duplicated file extensions.
    • Any modification converted the PATH from REG_EXPAND_SZ to REG_SZ, replacing environment variables with their expanded paths.
    • /Clean sorted PATH and PATHEXT alphabetically, changing the command and file extension resolution priority. The original order is now preserved.
    • /Clean created empty PATH and PATHEXT values when they did not exist.
    • /Del -User N and /Del -Machine N used zero-based indices while /List shows one-based indices, so the wrong entry was deleted. Indices now match the ones shown by /List.
    • /Del -User and /Del -Machine crashed with a negative entry index.
    • /Export generated a corrupt registry script (backslashes and quotes were not escaped).
    • /AddExt -User and /AddExt -Machine without value showed the "Please provide a directory to add" error message.
    • Directories containing ";" or wildcard characters were accepted, corrupting the PATH.
    • Fixed typos in the help and in error messages.

## v1.4 (2024) 🔄
#### 🌟 Improvements:
    • Source-code upgraded to .NET Framework 4.8
    • Syntax usage (switch names) and syntax examples has been reworked.

## v1.1 to v1.3 (2019) 🔄
*Change log information not available*

## v1.0 (2014) 🔄
Initial Release.