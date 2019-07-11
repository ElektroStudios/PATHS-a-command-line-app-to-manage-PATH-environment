# PATHS

**PATHS** is a command-line interface application to manage (add, remove or clean) the Windows PATH and PATHEXT environment variables. variable.

# **Donations**

##### Through Paypal:
If you like my work and want to support it, then please consider to deposit a donation through **Paypal** by clicking on the next button:

[![Donation Account](Images/Paypal.png)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=E4RQEV6YF5NZY)

[![Donation Amount](https://img.shields.io/badge/Current%20donations-0%24-red.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=E4RQEV6YF5NZY)

You are free to specify whatever amount of money you wish. That money will be sent to my **Paypal** account.

##### Through Envato:
If you are a .NET programmer, then maybe you would like to consider the purchase of 
'**DevCase for .NET Framework**', a powerful set of APIs for .NET developers, created by me. 

You can click the next button to go to the product specifications and the purchase page:

[![DevCase for .NET Framework](Images/DevCase%20Banner.png)](https://codecanyon.net/item/elektrokit-class-library-for-net/19260282)

Note that any source-code within the namespace 'DevCase' included in this **GitHub** repository, was freely extracted and distributed from the commercial library '**DevCase for .NET Framework**'.

<u>**Thanks in advance for your consideration!**</u> :thumbsup:


# Screenshots

![](Preview/PATHS%2001.png)

![](Preview/PATHS%2002.png)

![](Preview/PATHS%2003.png)

# Usage

### Syntax

    PATHS.exe /OPTIONS [DIRECTORY or EXTENSION or INDEX]

### Switches

    /L (or) /List    | Displays a list of the path entries.
    /E (or) /Export  | Expports the paths to a Registry file.
    /C (or) /Clean   | Clean duplicates and invalid entries.
    /R (or) /Restore | Restore the paths to Windows defaults.
                     |
    /Add -Current    | Add an entry to the current user PATH.
    /Add -Local      | Add an entry to the local machine PATH.
    /A (or) /Add     | Add an entry to both PATH's.
                     |
    /Del -Current    | Delete an entry from current user PATH.
    /Del -Local      | Delete an entry from local machine PATH.
    /D (or) /Del     | Delete an entry from both PATH's.
                     |
    /AddExt -Current | Add an extension to current user PATHEXT.
    /AddExt -Local   | Add an extension to local machine PATHEXT.
    /AddExt          | Add an extension to both PATHEXT's.
                     |
    /DelExt -Current | Delete an extension from current user PATHEXT.
    /DelExt -Local   | Delete an extension from local machine PATHEXT.
    /DelExt          | Delete an extension from both PATHEXT's.
                     |
    /? (or) /Help    | Display this help.
        
### Switches syntax

    /Del -Current    (Directory)
    /Del -Current    (Entry Index)

    /Del -Local      (Directory)
    /Del -Local      (Entry Index)

    /AddExt -Current (File extension)
    /AddExt -Local   (File extension)

    /DelExt -Current (File extension)
    /DelExt -Local   (File extension)
    
### Real world examples

    PATHS.exe /List
    (Lists the entries of PATH and PATHEXT)

    PATHS.exe /Clean
    (Cleans duplicates and not found directories in PATH and PATHEXT)

    PATHS.exe /Restore
    (Restores the PATH and PATHEXT to Windows defaults)

    PATHS.exe /Export "C:\Registry File.reg"
    (Exports the PATH and PATHEXT values to the target file)

    PATHS.exe /Add -Current "C:\Directory"
    (Adds a new entry "C:\Directory" to Current User PATH)

    PATHS.exe /Add -Local "C:\Directory"
    (Adds a new entry "C:\Directory" to All Users PATH)

    PATHS.exe /Add "C:\Directory"
    (Adds a new entry "C:\Directory" to both PATH's)

    PATHS.exe /Del -Current "C:\Directory"
    (Deletes entries matching as "C:\Directory" from Current User PATH)

    PATHS.exe /Del -Local "C:\Directory"
    (Deletes entries matching as "C:\Directory" from All Users PATH)

    PATHS.exe /Del "C:\Directory"
    (Deletes entries matching as "C:\Directory" from both PATH's)

    PATHS.exe /Del -Current 5
    (Deletes entry index 5 from Current User PATH)

    PATHS.exe /Del -Local 5
    (Deletes the entry index 5 from All Users PATH)

    PATHS.exe /AddExt -Current ".hack"
    (Adds a new ".hack" extension to Current User PATHEXT)

    PATHS.exe /AddExt -Local ".hack"
    (Adds a new ".hack" extension to All Users PATHEXT)

    PATHS.exe /AddExt ".hack"
    (Adds a new ".hack" extension to both PATHEXT's)

    PATHS.exe /DelExt -Current ".hack"
    (Deletes extensions matching as ".hack" from Current User PATHEXT)

    PATHS.exe /DelExt -Local ".hack"
    (Deletes extensions matching as ".hack" from All Users PATHEXT)

    PATHS.exe /DelExt ".hack"
    (Deletes extensions matching as ".hack" from both PATHEXT's)
    