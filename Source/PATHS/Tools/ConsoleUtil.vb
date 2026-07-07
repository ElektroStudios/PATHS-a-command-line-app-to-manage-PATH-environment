' ***********************************************************************
' Author   : Elektro
' Assembly : HelpSection
' Modified : 09-April-2015
' ***********************************************************************
' <copyright file="ConsoleUtil.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

Namespace Tools

    ''' <summary>
    ''' Contains related Console tools.
    ''' </summary>
    Public NotInheritable  Class ConsoleUtil

        ' Name.......: Write Colored Text
        ' Author.....: Elektro
        ' Description: Methods to write colorized strings.
        ' Last Update: 05/01/2014
        ' References.: LINQ
        ' Indications: Use *F##* as the ForeColor beginning delimiter, use *-F* to restore the console forecolor.
        '              Use *B##* as the BackColor beginning delimiter, use *-B* to restore the console BackColor.
        '
        ' Example Usages:
        '
        ' WriteColoredText(    " Hello World! ", ConsoleColor.Blue,    ConsoleColor.Blue)
        ' WriteColoredTextLine(" Hello World! ", ConsoleColor.Magenta, ConsoleColor.Gray)
        ' WriteColoredTextLine(" Hello World! ", Nothing,              Nothing)
        '
        ' WriteColoredText("*F10*Hello *F14*World!*-F*", {"*"c})
        ' WriteColoredTextLine("{B15}{F12} Hello World! {-F}{-B}", {"{"c, "}"c})
        ' WriteColoredTextLine(String.Format("*B15**F12* {0} *F0*{1} *-F**-B*", "Hello", "World!"), {"*"c})

        ''' <summary>
        ''' Writes colored text on the Console.
        ''' </summary>
        ''' <param name="text">Indicates the color-delimited text to parse and then write.</param>
        ''' <param name="delimiters">Indicates a set of (1 or 2) delimiters to parse a color-delimited string.</param>
        Public Shared Sub WriteColoredText(ByVal text As String,
                                           ByVal delimiters As Char())

            ' Store the current console colors to restore them later.
            Dim currentForegroundColor As ConsoleColor = Console.ForegroundColor
            Dim currentBackgroundColor As ConsoleColor = Console.BackgroundColor

            ' Split the string to retrieve and parse the color-delimited strings.
            Dim stringParts As String() =
                text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)

            ' Parse the string parts
            For Each part As String In stringParts

                If part.ToUpper Like "F#" _
                OrElse part.ToUpper Like "F##" Then ' Change the ForeColor.

                    Console.ForegroundColor = DirectCast(CInt(part.Substring(1)), ConsoleColor)

                ElseIf part.ToUpper Like "B#" _
                OrElse part.ToUpper Like "B##" Then ' Change the BackgroundColor.

                    Console.BackgroundColor = DirectCast(CInt(part.Substring(1)), ConsoleColor)

                ElseIf part.ToUpper Like "-F" Then  ' Restore the original Forecolor.

                    Console.ForegroundColor = currentForegroundColor

                ElseIf part.ToUpper Like "-B" Then ' Restore the original BackgroundColor.

                    Console.BackgroundColor = currentBackgroundColor

                Else ' String part is not a delimiter so we can print it.

                    Console.Write(part)

                End If

            Next part

            ' Finish by restoring the original console colors.
            Console.BackgroundColor = currentBackgroundColor
            Console.ForegroundColor = currentForegroundColor

        End Sub

        ''' <summary>
        ''' Writes colored text on the Console.
        ''' </summary>
        ''' <param name="text">Indicates the text to write.</param>
        ''' <param name="foreColor">Indicates the text color.</param>
        ''' <param name="backColor">Indicates the background color.</param>
        Public Shared Sub WriteColoredText(ByVal text As String,
                                           ByVal foreColor As ConsoleColor,
                                           ByVal backColor As ConsoleColor)

            ' Store the current console colors to restore them later.
            Dim currentForegroundColor As ConsoleColor = Console.ForegroundColor
            Dim currentBackgroundColor As ConsoleColor = Console.BackgroundColor

            ' Set the new temporal console colors.
            Console.ForegroundColor = If(foreColor = Nothing, currentForegroundColor, foreColor)
            Console.BackgroundColor = If(backColor = Nothing, currentBackgroundColor, backColor)

            ' Print the text.
            Console.Write(text)

            ' Finish by restoring the original console colors.
            Console.ForegroundColor = currentForegroundColor
            Console.BackgroundColor = currentBackgroundColor

        End Sub

        ''' <summary>
        ''' Writes colored text on the Console and adds an empty line at the end.
        ''' </summary>
        ''' <param name="text">Indicates the color-delimited text to parse and then write.</param>
        ''' <param name="delimiters">Indicates a set of (1 or 2) delimiters to parse a color-delimited string.</param>
        Public Shared Sub WriteColoredTextLine(ByVal text As String,
                                               ByVal delimiters As Char())

            WriteColoredText(text & Environment.NewLine, delimiters)

        End Sub

        ''' <summary>
        ''' Writes colored text on the Console and adds an empty line at the end.
        ''' </summary>
        ''' <param name="text">Indicates the color-delimited text to parse and then write.</param>
        ''' <param name="foreColor">Indicates the text color.</param>
        ''' <param name="backColor">Indicates the background color.</param>
        Public Shared Sub WriteColoredTextLine(ByVal text As String,
                                               ByVal foreColor As ConsoleColor,
                                               ByVal backColor As ConsoleColor)

            WriteColoredText(text & Environment.NewLine, foreColor, backColor)

        End Sub

    End Class

End Namespace