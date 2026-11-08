' ***********************************************************************
' Author   : Elektro
' Modified : 08-November-2026
' ***********************************************************************
' <copyright file="HelpSectionTests.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Linq
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Xml.Linq

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports PATHS.Tools
Imports PATHS.UserInterface

#End Region

#Region " HelpSection Tests "

''' <summary>
''' Tests for the help documentation of <see cref="HelpSection"/>.
''' </summary>
<TestClass>
Public NotInheritable Class HelpSectionTests

#Region " Helpers "

    ''' <summary>
    ''' Gets the PATHS assembly.
    ''' </summary>
    ''' <value>The PATHS assembly.</value>
    Private Shared ReadOnly Property PathsAssembly As Assembly
        Get
            Return GetType(PathUtil).Assembly
        End Get
    End Property

    ''' <summary>
    ''' Gets the value of a help element, with the color tags removed.
    ''' </summary>
    ''' <param name="elementName">The help element name.</param>
    ''' <returns>The plain text value of the help element.</returns>
    Private Shared Function GetPlainHelpValue(elementName As String) As String

        Dim element As XElement = HelpSection.ColorizedHelp.Element(elementName)
        Assert.IsNotNull(element, $"Help element not found: {elementName}")
        Return Regex.Replace(element.Value, "\*(?:F\d{1,2}|B\d{1,2}|-F|-B)\*", String.Empty)

    End Function

#End Region

#Region " Tests "

    ''' <summary>
    ''' All the help elements used by the application exist and are not empty.
    ''' </summary>
    <TestMethod>
    <DataRow("Title")>
    <DataRow("Name")>
    <DataRow("Author")>
    <DataRow("Version")>
    <DataRow("Copyright")>
    <DataRow("Website")>
    <DataRow("Logo")>
    <DataRow("Separator")>
    <DataRow("Syntax")>
    <DataRow("SyntaxExtra")>
    <DataRow("UsageExamples")>
    Public Sub HelpElement_ExistsAndIsNotEmpty(elementName As String)

        Assert.IsFalse(String.IsNullOrWhiteSpace(GetPlainHelpValue(elementName)))

    End Sub

    ''' <summary>
    ''' The version shown in the help matches the assembly version (Major.Minor), so it is not forgotten when releasing.
    ''' </summary>
    <TestMethod>
    Public Sub Version_MatchesAssemblyVersion()

        Dim version As Version = PathsAssembly.GetName().Version
        Dim expected As String = $"{version.Major}.{version.Minor}"

        Assert.AreEqual(expected, GetPlainHelpValue("Version").Trim())
        StringAssert.Contains(GetPlainHelpValue("Logo"), $"v{expected}")

    End Sub

    ''' <summary>
    ''' The assembly file version matches the assembly version.
    ''' </summary>
    <TestMethod>
    Public Sub FileVersion_MatchesAssemblyVersion()

        Dim fileVersion As AssemblyFileVersionAttribute = PathsAssembly.GetCustomAttribute(Of AssemblyFileVersionAttribute)()

        Assert.IsNotNull(fileVersion)
        Assert.AreEqual(PathsAssembly.GetName().Version.ToString(), fileVersion.Version)

    End Sub

    ''' <summary>
    ''' The last copyright year shown in the help matches the assembly copyright.
    ''' </summary>
    <TestMethod>
    Public Sub Copyright_LastYearMatchesAssemblyCopyright()

        Dim copyright As AssemblyCopyrightAttribute = PathsAssembly.GetCustomAttribute(Of AssemblyCopyrightAttribute)()
        Assert.IsNotNull(copyright)

        Dim assemblyYear As String = Regex.Match(copyright.Copyright, "(\d{4})\s*$").Groups(1).Value
        Dim helpYear As String = Regex.Match(GetPlainHelpValue("Copyright"), "(\d{4})\s*$").Groups(1).Value

        Assert.IsFalse(String.IsNullOrEmpty(assemblyYear))
        Assert.AreEqual(assemblyYear, helpYear)

    End Sub

    ''' <summary>
    ''' Every switch handled by the argument parser is documented in the switches section.
    ''' </summary>
    <TestMethod>
    <DataRow("/L")>
    <DataRow("/List")>
    <DataRow("/E")>
    <DataRow("/Export")>
    <DataRow("/C")>
    <DataRow("/Clean")>
    <DataRow("/R")>
    <DataRow("/Restore")>
    <DataRow("/A")>
    <DataRow("/Add")>
    <DataRow("/D")>
    <DataRow("/Del")>
    <DataRow("/AddExt")>
    <DataRow("/DelExt")>
    <DataRow("/?")>
    <DataRow("/Help")>
    <DataRow("-User")>
    <DataRow("-Machine")>
    Public Sub SyntaxExtra_DocumentsSwitch(switchName As String)

        Dim text As String = GetPlainHelpValue("SyntaxExtra")

        Assert.IsTrue(Regex.IsMatch(text, $"(?<![\w/-]){Regex.Escape(switchName)}(?![\w])", RegexOptions.IgnoreCase),
                      $"Switch not documented: {switchName}")

    End Sub

    ''' <summary>
    ''' The help text has no known typos.
    ''' </summary>
    <TestMethod>
    Public Sub Help_HasNoKnownTypos()

        Dim allText As String = String.Join(Environment.NewLine,
                                            HelpSection.ColorizedHelp.Elements().Select(Function(e As XElement) e.Value))

        Assert.IsFalse(allText.Contains("Adda "), "Typo: 'Adda'")
        Assert.IsFalse(allText.Contains("parametter"), "Typo: 'parametter'")

    End Sub

    ''' <summary>
    ''' The color tags of the help text are well-formed (every tag uses a valid console color number).
    ''' </summary>
    <TestMethod>
    Public Sub Help_ColorTagsAreValidConsoleColors()

        For Each element As XElement In HelpSection.ColorizedHelp.Elements()
            For Each match As Match In Regex.Matches(element.Value, "\*[FB](\d+)\*")
                Dim colorNumber As Integer = Integer.Parse(match.Groups(1).Value)
                Assert.IsTrue([Enum].IsDefined(GetType(ConsoleColor), colorNumber),
                              $"Invalid color tag '{match.Value}' in element <{element.Name}>.")
            Next match
        Next element

    End Sub

#End Region

End Class

#End Region
