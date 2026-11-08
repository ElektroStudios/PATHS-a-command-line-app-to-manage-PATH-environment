' ***********************************************************************
' Author   : Elektro
' Modified : 08-November-2026
' ***********************************************************************
' <copyright file="ConsoleUtilTests.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.IO

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports PATHS.Tools

#End Region

#Region " ConsoleUtil Tests "

''' <summary>
''' Tests for the color-delimited console writer of <see cref="ConsoleUtil"/>.
''' <para></para>
''' The console output is redirected to a <see cref="StringWriter"/> to capture the written text.
''' </summary>
<TestClass>
Public NotInheritable Class ConsoleUtilTests

#Region " Fields "

    ''' <summary>
    ''' The original console output writer.
    ''' </summary>
    Private originalOut As TextWriter

    ''' <summary>
    ''' The writer that captures the console output of the current test.
    ''' </summary>
    Private output As StringWriter

#End Region

#Region " Initialization / Cleanup "

    ''' <summary>
    ''' Redirects the console output.
    ''' </summary>
    <TestInitialize>
    Public Sub TestInitialize()

        Me.originalOut = Console.Out
        Me.output = New StringWriter()
        Console.SetOut(Me.output)

    End Sub

    ''' <summary>
    ''' Restores the console output.
    ''' </summary>
    <TestCleanup>
    Public Sub TestCleanup()

        Console.SetOut(Me.originalOut)
        Me.output.Dispose()

    End Sub

#End Region

#Region " Delimited Text "

    ''' <summary>
    ''' The color tags are removed from the written text.
    ''' </summary>
    <TestMethod>
    <DataRow("*F10*Hello *F14*World!*-F*", "Hello World!")>
    <DataRow("*B15**F12* Hello World! *-F**-B*", " Hello World! ")>
    <DataRow("*F7*single digit*-F*", "single digit")>
    <DataRow("*f12*lower case*-f*", "lower case")>
    <DataRow("No tags at all", "No tags at all")>
    <DataRow(" *F12*[X] ERROR*F08*: *F07*Message.*-F*", " [X] ERROR: Message.")>
    Public Sub WriteColoredText_RemovesColorTags(text As String, expected As String)

        ConsoleUtil.WriteColoredText(text, {"*"c})

        Assert.AreEqual(expected, Me.output.ToString())

    End Sub

    ''' <summary>
    ''' Custom delimiters (e.g. braces) are supported.
    ''' </summary>
    <TestMethod>
    Public Sub WriteColoredText_CustomDelimiters()

        ConsoleUtil.WriteColoredText("{B15}{F12} Hello World! {-F}{-B}", {"{"c, "}"c})

        Assert.AreEqual(" Hello World! ", Me.output.ToString())

    End Sub

    ''' <summary>
    ''' The line variant appends a new line.
    ''' </summary>
    <TestMethod>
    Public Sub WriteColoredTextLine_AppendsNewLine()

        ConsoleUtil.WriteColoredTextLine("*F10*Hello*-F*", {"*"c})

        Assert.AreEqual($"Hello{Environment.NewLine}", Me.output.ToString())

    End Sub

    ''' <summary>
    ''' The console colors are restored after writing delimited text.
    ''' </summary>
    <TestMethod>
    Public Sub WriteColoredText_RestoresConsoleColors()

        Dim foreColor As ConsoleColor = Console.ForegroundColor
        Dim backColor As ConsoleColor = Console.BackgroundColor

        ConsoleUtil.WriteColoredText("*F12**B14*Colored text without restore tags", {"*"c})

        Assert.AreEqual(foreColor, Console.ForegroundColor)
        Assert.AreEqual(backColor, Console.BackgroundColor)

    End Sub

#End Region

#Region " Explicit Colors "

    ''' <summary>
    ''' The explicit color variant writes the text as-is.
    ''' </summary>
    <TestMethod>
    Public Sub WriteColoredText_ExplicitColors_WritesTextAsIs()

        ConsoleUtil.WriteColoredText("*F12*not parsed*", ConsoleColor.Red, ConsoleColor.Black)

        Assert.AreEqual("*F12*not parsed*", Me.output.ToString())

    End Sub

    ''' <summary>
    ''' The explicit color line variant appends a new line and restores the console colors.
    ''' </summary>
    <TestMethod>
    Public Sub WriteColoredTextLine_ExplicitColors_AppendsNewLineAndRestoresColors()

        Dim foreColor As ConsoleColor = Console.ForegroundColor
        Dim backColor As ConsoleColor = Console.BackgroundColor

        ConsoleUtil.WriteColoredTextLine("Hello", ConsoleColor.Magenta, ConsoleColor.Gray)

        Assert.AreEqual($"Hello{Environment.NewLine}", Me.output.ToString())
        Assert.AreEqual(foreColor, Console.ForegroundColor)
        Assert.AreEqual(backColor, Console.BackgroundColor)

    End Sub

#End Region

End Class

#End Region
