<!-- Common Project Tags:
command-line 
console-applications 
dotnet 
netframework 
netframework48 
tool 
tools 
vbnet 
visualstudio 
windows 
windows-app 
windows-application 
windows-applications 
 -->

# PATHS

### A command-line application to manage (add, remove or clean) the PATH and PATHEXT environment variables.

------------------

## 🖼️ Screenshots

![](Images/PATHS%2001.png)

![](Images/PATHS%2002.png)

![](Images/PATHS%2003.png)

## 📝 Requirements

- Microsoft Windows OS.

## 🤖 Getting Started

Download the latest release by clicking [here](https://github.com/ElektroStudios/PATHS-a-command-line-app-to-manage-PATH-environment/releases/latest).

### Syntax

        PATHS.exe /OPTIONS [DIRECTORY PATH or FILE EXTENSION or ENTRY INDEX]

### Switches

        /L (or) /List    | Lists the PATH entries.
        /E (or) /Export  | Exports PATH and PATHEXT entries to a registry script file.
        /C (or) /Clean   | Clean duplicates and missing directory paths in PATH and PATHEXT.
        /R (or) /Restore | Restores the PATH entries to Windows defaults.
                         |
        /Add -User       | Adda a directory to current user's PATH.
        /Add -Machine    | Adds a directory to local machine PATH.
        /A (or) /Add     | Adds a directory to current user's and local machine PATHs.
                         |
        /Del -User       | Deletes a directory from current user's PATH.
        /Del -Machine    | Deletes a directory from local machine PATH.
        /D (or) /Del     | Deletes a directory from current user's and local machine PATHs.
                         |
        /AddExt -User    | Adds a file extension to current user's PATHEXT.
        /AddExt -Machine | Adds a file extension to local machine PATHEXT.
        /AddExt          | Adds a file extension to current user's and local machine PATHEXTs.
                         |
        /DelExt -User    | Deletes a file extension from current user's PATHEXT.
        /DelExt -Machine | Deletes a file extension from local machine PATHEXT.
        /DelExt          | Deletes a file extension from current user's and local machine PATHEXTs.
                         |
        /? (or) /Help    | Shows this help.

### Switch value types

        Note: You can list all the entry indices using command: PATHS.exe /List

        /Del -User       (Directory Path or Entry Index)
        /Del -Machine    (Directory Path or Entry Index)

        /AddExt -User    (File Extension)
        /AddExt -Machine (File Extension)

        /DelExt -User    (File Extension)
        /DelExt -Machine (File Extension)

### Usage examples

        PATHS.exe /List
        (Lists the entries of PATH and PATHEXT)

        PATHS.exe /Clean
        (Clean duplicates and missing directory paths in PATH and PATHEXT)

        PATHS.exe /Restore
        (Restores the PATH and PATHEXT to Windows defaults)

        PATHS.exe /Export "C:\Registry File.reg"
        (Exports the PATH and PATHEXT entries to the specified registry script file)

        PATHS.exe /Add -User "C:\Directory"
        (Adds a directory with name "C:\Directory" to current user's PATH)

        PATHS.exe /Add -Machine "C:\Directory"
        (Adds a directory with name "C:\Directory" to local machine PATH)

        PATHS.exe /Add "C:\Directory"
        (Adds a directory with name "C:\Directory" to current user's and local machine PATHs)

        PATHS.exe /Del -User "C:\Directory"
        (Deletes any directory with name "C:\Directory" from current user's PATH)

        PATHS.exe /Del -Machine "C:\Directory"
        (Deletes any directory with name "C:\Directory" from local machine PATH)

        PATHS.exe /Del "C:\Directory"
        (Deletes any directory with name "C:\Directory" from current user's and local machine PATHs)

        PATHS.exe /Del -User 5
        (Deletes the entry with index 5 from current user's PATH)

        PATHS.exe /Del -Machine 5
        (Deletes the entry with index 5 from local machine PATH)

        PATHS.exe /AddExt -User ".test"
        (Adds a file extension with name ".test" to current user's PATHEXT)

        PATHS.exe /AddExt -Machine ".test"
        (Adds a file extension with name ".test" to local machine PATHEXT)

        PATHS.exe /AddExt ".test"
        (Adds a file extension with name ".test" to current user's and local machine PATHEXTs)

        PATHS.exe /DelExt -User ".test"
        (Deletes any file extension with name ".test" from current user's PATHEXT)

        PATHS.exe /DelExt -Machine ".test"
        (Deletes any file extension with name ".test" from local machine PATHEXT)

        PATHS.exe /DelExt ".test"
        (Deletes any file extension with name ".test" from current user's and local machine PATHEXTs)

## 🔄 Change Log

Explore the complete list of changes, bug fixes, and improvements across different releases by clicking [here](/Docs/CHANGELOG.md).

## ⚠️ Disclaimer:

This Work (the repository and the content provided in) is provided "as is", without warranty of any kind, express or implied, including but not limited to the warranties of merchantability, fitness for a particular purpose and noninfringement. In no event shall the authors or copyright holders be liable for any claim, damages or other liability, whether in an action of contract, tort or otherwise, arising from, out of or in connection with the Work or the use or other dealings in the Work.

## 💪 Contributing

Your contribution is highly appreciated!. If you have any ideas, suggestions, or encounter issues, feel free to open an issue by clicking [here](https://github.com/ElektroStudios/PATHS-a-command-line-app-to-manage-PATH-environment/issues/new/choose). 

Your input helps make this Work better for everyone. Thank you for your support! 🚀

## 💰 Beyond Contribution 

This work is distributed for educational purposes and without any profit motive. However, if you find value in my efforts and wish to support and motivate my ongoing work, you may consider contributing financially through the following options:

<br></br>
<p align="center"><img src="/Images/github_circle.png" height=100></p>
<p align="center">__________________</p>
<h3 align="center">Becoming my sponsor on Github:</h3>
<p align="center">You can show me your support by clicking <a href="https://github.com/sponsors/ElektroStudios/">here</a>, <br align="center">contributing any amount you prefer, and unlocking rewards!</br></p>
<br></br>

<p align="center"><img src="/Images/paypal_circle.png" height=100></p>
<p align="center">__________________</p>
<h3 align="center">Making a Paypal Donation:</h3>
<p align="center">You can donate to me any amount you like via Paypal by clicking <a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=E4RQEV6YF5NZY">here</a>.</p>
<br></br>

<p align="center"><img src="/Images/envato_circle.png" height=100></p>
<p align="center">__________________</p>
<h3 align="center">Purchasing software of mine at Envato's Codecanyon marketplace:</h3>
<p align="center">If you are a .NET developer, you may want to explore '<b>DevCase Class Library for .NET</b>', <br align="center">a huge set of APIs that I have on sale. Check out the product by clicking <a href="https://codecanyon.net/item/elektrokit-class-library-for-net/19260282">here</a></br><br align="center"><i>It also contains all piece of reusable code that you can find across the source code of my open source works.</i></p>
<br></br>

<h2 align="center"><u>Your support means the world to me! Thank you for considering it!</u> 👍</h2>
