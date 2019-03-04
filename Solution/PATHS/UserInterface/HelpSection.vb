' ***********************************************************************
' Author   : Elektro
' Assembly : HelpSection
' Modified : 09-April-2015
' Usage    : Use *F##* as the ForeColor beginning delimiter, use *-F* to restore the console forecolor.
'            Use *B##* as the BackColor beginning delimiter, use *-B* to restore the console BackColor.
' ***********************************************************************
' <copyright file="HelpSection.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Usage Examples "

#Region " Example Without Colorization "

'Module Module1

'    Sub Main()

'        Console.Title = HelpSection.Help.<Title>.Value

'        Dim sb As New System.Text.StringBuilder
'        sb.AppendLine(HelpSection.Help.<Logo>.Value)
'        sb.AppendLine(HelpSection.Help.<Separator>.Value)
'        sb.AppendLine(String.Format("    Executable name.......: {0}", HelpSection.Help.<Process>.Value))
'        sb.AppendLine(String.Format("    Application name......: {0}", HelpSection.Help.<Name>.Value))
'        sb.AppendLine(String.Format("    Application version...: {0}", HelpSection.Help.<Version>.Value))
'        sb.AppendLine(String.Format("    Application author....: {0}", HelpSection.Help.<Author>.Value))
'        sb.AppendLine(String.Format("    Application copyright.: {0}", HelpSection.Help.<Copyright>.Value))
'        sb.AppendLine(String.Format("    Author website........: {0}", HelpSection.Help.<Website>.Value))
'        sb.AppendLine(String.Format("    Author Skype..........: {0}", HelpSection.Help.<Skype>.Value))
'        sb.AppendLine(String.Format("    Author Email..........: {0}", HelpSection.Help.<Email>.Value))
'        sb.AppendLine(HelpSection.Help.<Separator>.Value)
'        sb.AppendLine(HelpSection.Help.<Syntax>.Value)
'        sb.AppendLine(HelpSection.Help.<SyntaxExtra>.Value)
'        sb.AppendLine(HelpSection.Help.<UsageExamples>.Value)

'         HelpSection.WriteLine(sb.ToString)

'        Threading.Thread.Sleep(60000)

'    End Sub

'End Module

#End Region

#Region " Example With Colorization "

'Module Module1

'    Sub Main()

'        Console.Title = HelpSection.ColorizedHelp.<Title>.Value

'        Dim sb As New System.Text.StringBuilder
'        sb.AppendLine(HelpSection.ColorizedHelp.<Logo>.Value)
'        sb.AppendLine(HelpSection.ColorizedHelp.<Separator>.Value)
'        sb.AppendLine(String.Format("    Executable name.......: {0}", HelpSection.ColorizedHelp.<Process>.Value))
'        sb.AppendLine(String.Format("    Application name......: {0}", HelpSection.ColorizedHelp.<Name>.Value))
'        sb.AppendLine(String.Format("    Application version...: {0}", HelpSection.ColorizedHelp.<Version>.Value))
'        sb.AppendLine(String.Format("    Application author....: {0}", HelpSection.ColorizedHelp.<Author>.Value))
'        sb.AppendLine(String.Format("    Application copyright.: {0}", HelpSection.ColorizedHelp.<Copyright>.Value))
'        sb.AppendLine(String.Format("    Author website........: {0}", HelpSection.ColorizedHelp.<Website>.Value))
'        sb.AppendLine(String.Format("    Author Skype..........: {0}", HelpSection.ColorizedHelp.<Skype>.Value))
'        sb.AppendLine(String.Format("    Author Email..........: {0}", HelpSection.ColorizedHelp.<Email>.Value))
'        sb.AppendLine(HelpSection.ColorizedHelp.<Separator>.Value)
'        sb.AppendLine(HelpSection.ColorizedHelp.<Syntax>.Value)
'        sb.AppendLine(HelpSection.ColorizedHelp.<SyntaxExtra>.Value)
'        sb.AppendLine(HelpSection.ColorizedHelp.<UsageExamples>.Value)

'        WriteColoredTextLine(sb.ToString, {"*"c})

'        Threading.Thread.Sleep(60000)

'    End Sub

'End Module

#End Region

#End Region

#Region " ConsoleColor Enumeration Helper "

' Black = 0
' DarkBlue = 1
' DarkGreen = 2
' DarkCyan = 3
' DarkRed = 4
' DarkMagenta = 5
' DarkYellow = 6
' Gray = 7
' DarkGray = 8
' Blue = 9
' Green = 10
' Cyan = 11
' Red = 12
' Magenta = 13
' Yellow = 14
' White = 15

#End Region

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " HelpSection "

Namespace UserInterface

    ''' <summary>
    ''' Class that manages the Help documentation of a Console application with colorization capabilities.
    ''' </summary>
    Public NotInheritable Class HelpSection

#Region " Members "

        ''' <summary>
        ''' Gets the name of the current process.
        ''' </summary>
        Private Shared ReadOnly processName As String =
            Process.GetCurrentProcess.MainModule.ModuleName

        ''' <summary>
        ''' Use this var into an XML if need to escape a 'GreaterThan' character.
        ''' </summary>
        Private ReadOnly greaterThanChar As Char = ">"c

        ''' <summary>
        ''' Use this var into an XML if need to escape a 'LowerThan' character.
        ''' </summary>
        Private ReadOnly lowerThanChar As Char = "<"c

#End Region

#Region " Help Text "

        ''' <summary>
        ''' Contains Help information such as Author, Syntax and Example usages.
        ''' These strings are color-delimited to print a colorized output console,
        ''' using the 'WriteColoredText' methods written by Elektro.
        ''' </summary>
        Friend Shared ReadOnly ColorizedHelp As XElement =
    <Help>

        <!-- Current process name -->
        <!-- That means even when the user manually changes the executable name -->
        <Process>*F07*<%= processName %>*-F*</Process>

        <!-- Application title -->
        <Title>PATHS .:: By Elektro ::.</Title>

        <!-- Application name -->
        <Name>*F07*PATHS*-F*</Name>

        <!-- Application author -->
        <Author>*F07*Elektro*-F*</Author>

        <!-- Application version -->
        <Version>*F07*1.3*-F*</Version>

        <!-- Copyright information -->
        <Copyright>*F07*© Elektro Studios 2015*-F*</Copyright>

        <!-- Website information -->
        <Website>*F07*http://foro.elhacker.net/profiles/elektrohcker-u436313.html*-F*</Website>

        <!-- Skype contact information -->
        <Skype>*F07*ElektroStudios*-F*</Skype>

        <!-- Email contact information -->
        <Email>*F07*ElektroStudios@ElHacker.Net*-F*</Email>

        <!-- Application Logotype -->
        <Logo>*F11*
    :::::::::     ::: ::::::::::: :::    :::  ::::::::  
    :+:    :+:  :+: :+:   :+:     :+:    :+: :+:    :+: 
    +:+    +:+ +:+   +:+  +:+     +:+    +:+ +:+        
    +#++:++#+ +#++:++#++: +#+     +#++:++#++ +#++:++#++ 
    +#+       +#+     +#+ +#+     +#+    +#+        +#+ 
    #+#       #+#     #+# #+#     #+#    #+# #+#    #+# 
    ###       ###     ### ###     ###    ###  ########    *F07*v1.3
    *-F*</Logo>

        <!-- Separator shape -->
        <Separator>
    *F11*------------------------------------------------------*F14*>>>>*-F*
    </Separator>

        <!-- Application Syntax -->
        <Syntax>
    *F11*[+]*-F* *F14*Syntax*-F*

        *F07*<%= processName %> *F10*/OPTIONS*-F* *F10*[*F07*DIRECTORY or EXTENSION or INDEX*F10*]*-F*
    </Syntax>

        <!-- Application Syntax (Additional Specifications) -->
        <SyntaxExtra>
    *F11*[+]*-F* *F14*Switches*-F*

        *F10*/L *F08*(or)*F10* /List    *F08*| *F07*Displays a list of the path entries.*-F*
        *F10*/E *F08*(or)*F10* /Export  *F08*| *F07*Expports the paths to a Registry file.*-F*
        *F10*/C *F08*(or)*F10* /Clean   *F08*| *F07*Clean duplicates and invalid entries.*-F*
        *F10*/R *F08*(or)*F10* /Restore *F08*| *F07*Restore the paths to Windows defaults.*-F*
        *F10*   *F08*         *F10*     *F08*|
        *F10*/Add -Current    *F08**F08**F08*| *F07*Add an entry to the current user PATH.*-F*
        *F10*/Add -Local      *F08**F08**F08*| *F07*Add an entry to the local machine PATH.*-F*
        *F10*/A *F08*(or)*F10* /Add     *F08*| *F07*Add an entry to both PATH's.*-F*
        *F10*   *F08*         *F10*     *F08*|
        *F10*/Del -Current    *F08**F08**F08*| *F07*Delete an entry from current user PATH.*-F*
        *F10*/Del -Local      *F08**F08**F08*| *F07*Delete an entry from local machine PATH.*-F*
        *F10*/D *F08*(or)*F10* /Del     *F08*| *F07*Delete an entry from both PATH's.*-F*
        *F10*   *F08*         *F10*     *F08*|
        *F10*/AddExt -Current *F08**F08**F08*| *F07*Add an extension to current user PATHEXT.*-F*
        *F10*/AddExt -Local   *F08**F08**F08*| *F07*Add an extension to local machine PATHEXT.*-F*
        *F10*/AddExt*F08**F08*          *F08*| *F07*Add an extension to both PATHEXT's.*-F*
        *F10*   *F08*         *F10*     *F08*|
        *F10*/DelExt -Current *F08**F08**F08*| *F07*Delete an extension from current user PATHEXT.*-F*
        *F10*/DelExt -Local   *F08**F08**F08*| *F07*Delete an extension from local machine PATHEXT.*-F*
        *F10*/DelExt          *F08**F08**F08*| *F07*Delete an extension from both PATHEXT's.*-F*
        *F10*   *F08*         *F10*     *F08*|
        *F10*/? *F08*(or)*F10* /Help    *F08*| *F07*Display this help.*-F*


    *F11*[+]*-F* *F14*Switch value types*-F*

        *F10* You can see all the entry index numbers typing:*-F* *F10*<%= processName %> /List*-F*

        *F10*/Del -Current*-F*    (*F07*Directory*-F*)
        *F10*/Del -Current*-F*    (*F07*Entry Index*-F*)

        *F10*/Del -Local*-F*      (*F07*Directory*-F*)
        *F10*/Del -Local*-F*      (*F07*Entry Index*-F*)

        *F10*/AddExt -Current*-F* (*F07*File extension*-F*)
        *F10*/AddExt -Local*-F*   (*F07*File extension*-F*)

        *F10*/DelExt -Current*-F* (*F07*File extension*-F*)
        *F10*/DelExt -Local*-F*   (*F07*File extension*-F*)
    </SyntaxExtra>

        <!-- Application Usage Examples -->
        <UsageExamples>
    *F11*[+]*-F* *F14*Usage examples*-F*

        *F10*<%= processName %> /List*-F*
        *F08*(Lists the entries of PATH and PATHEXT)*-F*

        *F10*<%= processName %> /Clean*-F*
        *F08*(Cleans duplicates and not found directories in PATH and PATHEXT)*-F*

        *F10*<%= processName %> /Restore*-F*
        *F08*(Restores the PATH and PATHEXT to Windows defaults)*-F*

        *F10*<%= processName %> /Export "C:\Registry File.reg"*-F*
        *F08*(Exports the PATH and PATHEXT values to the target file)*-F*

        *F10*<%= processName %> /Add -Current "C:\Directory"*-F*
        *F08*(Adds a new entry "C:\Directory" to Current User PATH)*-F*

        *F10*<%= processName %> /Add -Local "C:\Directory"*-F*
        *F08*(Adds a new entry "C:\Directory" to All Users PATH)*-F*

        *F10*<%= processName %> /Add "C:\Directory"*-F*
        *F08*(Adds a new entry "C:\Directory" to both PATH's)*-F*

        *F10*<%= processName %> /Del -Current "C:\Directory"*-F*
        *F08*(Deletes entries matching as "C:\Directory" from Current User PATH)*-F*

        *F10*<%= processName %> /Del -Local "C:\Directory"*-F*
        *F08*(Deletes entries matching as "C:\Directory" from All Users PATH)*-F*

        *F10*<%= processName %> /Del "C:\Directory"*-F*
        *F08*(Deletes entries matching as "C:\Directory" from both PATH's)*-F*

        *F10*<%= processName %> /Del -Current 5*-F*
        *F08*(Deletes entry index 5 from Current User PATH)*-F*

        *F10*<%= processName %> /Del -Local 5*-F*
        *F08*(Deletes the entry index 5 from All Users PATH)*-F*

        *F10*<%= processName %> /AddExt -Current ".hack"*-F*
        *F08*(Adds a new ".hack" extension to Current User PATHEXT)*-F*

        *F10*<%= processName %> /AddExt -Local ".hack"*-F*
        *F08*(Adds a new ".hack" extension to All Users PATHEXT)*-F*

        *F10*<%= processName %> /AddExt ".hack"*-F*
        *F08*(Adds a new ".hack" extension to both PATHEXT's)*-F*

        *F10*<%= processName %> /DelExt -Current ".hack"*-F*
        *F08*(Deletes extensions matching as ".hack" from Current User PATHEXT)*-F*

        *F10*<%= processName %> /DelExt -Local ".hack"*-F*
        *F08*(Deletes extensions matching as ".hack" from All Users PATHEXT)*-F*

        *F10*<%= processName %> /DelExt ".hack"*-F*
        *F08*(Deletes extensions matching as ".hack" from both PATHEXT's)*-F*
    </UsageExamples>

    </Help>

#End Region

    End Class

End Namespace

#End Region