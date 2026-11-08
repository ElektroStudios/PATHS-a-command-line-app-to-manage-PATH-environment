' ***********************************************************************
' Author   : Elektro
' Modified : 08-November-2026
' ***********************************************************************
' <copyright file="EndToEndTests.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Diagnostics
Imports System.IO
Imports System.Text

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports PATHS.Tools

#End Region

#Region " End-To-End Tests "

''' <summary>
''' End-to-end tests that launch the PATHS.exe process and check its exit code and console output.
''' <para></para>
''' IMPORTANT: these tests only run commands that DO NOT modify the registry
''' (help, argument validation errors, /List and /Export), so they are safe to run on any machine.
''' </summary>
<TestClass>
<TestCategory(TestCategories.EndToEnd)>
Public NotInheritable Class EndToEndTests

#Region " Types "

    ''' <summary>
    ''' Represents the result of a PATHS.exe execution.
    ''' </summary>
    Private NotInheritable Class ProcessResult

        ''' <summary>
        ''' Gets the process exit code.
        ''' </summary>
        ''' <value>The process exit code.</value>
        Friend ReadOnly Property ExitCode As Integer

        ''' <summary>
        ''' Gets the standard output of the process.
        ''' </summary>
        ''' <value>The standard output of the process.</value>
        Friend ReadOnly Property Output As String

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ProcessResult"/> class.
        ''' </summary>
        ''' <param name="exitCode">The process exit code.</param>
        ''' <param name="output">The standard output of the process.</param>
        Friend Sub New(exitCode As Integer, output As String)

            Me.ExitCode = exitCode
            Me.Output = output

        End Sub

    End Class

#End Region

#Region " Helpers "

    ''' <summary>
    ''' Runs PATHS.exe with the specified arguments and waits for its exit.
    ''' </summary>
    ''' <param name="arguments">The command-line arguments.</param>
    ''' <returns>The execution result.</returns>
    Private Shared Function RunPaths(arguments As String) As ProcessResult

        Dim exePath As String = Path.Combine(Path.GetDirectoryName(GetType(PathUtil).Assembly.Location), "PATHS.exe")
        Assert.IsTrue(File.Exists(exePath), $"PATHS.exe not found: {exePath}")

        Dim startInfo As New ProcessStartInfo(exePath, arguments) With {
            .UseShellExecute = False,
            .CreateNoWindow = True,
            .RedirectStandardOutput = True,
            .RedirectStandardError = True,
            .StandardOutputEncoding = Encoding.UTF8,
            .StandardErrorEncoding = Encoding.UTF8
        }

        Using process As Process = Process.Start(startInfo)
            Dim errorTask As Threading.Tasks.Task(Of String) = process.StandardError.ReadToEndAsync()
            Dim output As String = process.StandardOutput.ReadToEnd()

            If Not process.WaitForExit(60000) Then
                process.Kill()
                Assert.Fail($"PATHS.exe {arguments} did not exit in time.")
            End If

            Dim errorOutput As String = errorTask.Result
            Assert.IsTrue(String.IsNullOrEmpty(errorOutput), $"Unexpected standard error output: {errorOutput}")

            Return New ProcessResult(process.ExitCode, output)
        End Using

    End Function

#End Region

#Region " Help "

    ''' <summary>
    ''' Without arguments, the help is shown and the exit code is 0.
    ''' </summary>
    <TestMethod>
    Public Sub NoArguments_ShowsHelpAndExitsWithZero()

        Dim result As ProcessResult = RunPaths(String.Empty)

        Assert.AreEqual(0, result.ExitCode)
        StringAssert.Contains(result.Output, "Syntax")
        StringAssert.Contains(result.Output, "Usage examples")

    End Sub

    ''' <summary>
    ''' The help switches show the help and the exit code is 0.
    ''' </summary>
    <TestMethod>
    <DataRow("/?")>
    <DataRow("/help")>
    <DataRow("/HELP")>
    Public Sub HelpSwitch_ShowsHelpAndExitsWithZero(arguments As String)

        Dim result As ProcessResult = RunPaths(arguments)

        Assert.AreEqual(0, result.ExitCode)
        StringAssert.Contains(result.Output, "Switches")
        Assert.IsFalse(result.Output.Contains("*F"), "Color tags must not be printed.")

    End Sub

    ''' <summary>
    ''' An unknown switch shows the help and the exit code is 1.
    ''' </summary>
    <TestMethod>
    <DataRow("/unknown")>
    <DataRow("C:\Directory")>
    Public Sub UnknownSwitch_ShowsHelpAndExitsWithOne(arguments As String)

        Dim result As ProcessResult = RunPaths(arguments)

        Assert.AreEqual(1, result.ExitCode)
        StringAssert.Contains(result.Output, "Switches")

    End Sub

    ''' <summary>
    ''' The logo shows the assembly version.
    ''' </summary>
    <TestMethod>
    Public Sub Logo_ShowsAssemblyVersion()

        Dim version As Version = GetType(PathUtil).Assembly.GetName().Version
        Dim result As ProcessResult = RunPaths("/?")

        StringAssert.Contains(result.Output, $"v{version.Major}.{version.Minor}")

    End Sub

#End Region

#Region " Argument Validation Errors "

    ''' <summary>
    ''' Switches that require a value show the expected error and the exit code is 1, without modifying anything.
    ''' <para></para>
    ''' Regression: /AddExt -User and /AddExt -Machine without value showed the "Please provide a directory" error.
    ''' </summary>
    <TestMethod>
    <DataRow("/add", "Please provide a directory to add.")>
    <DataRow("/a", "Please provide a directory to add.")>
    <DataRow("/add -user", "Please provide a directory to add.")>
    <DataRow("/add -machine", "Please provide a directory to add.")>
    <DataRow("/del", "Please provide a directory to delete.")>
    <DataRow("/d", "Please provide a directory to delete.")>
    <DataRow("/del -user", "Please provide a directory to delete.")>
    <DataRow("/del -machine", "Please provide a directory to delete.")>
    <DataRow("/del 5", "Deletion by Entry Index is only allowed with -user and -machine parameters.")>
    <DataRow("/export", "Please provide a File to export the data.")>
    <DataRow("/e", "Please provide a File to export the data.")>
    <DataRow("/addext", "Please provide a File-Extension to add.")>
    <DataRow("/addext -user", "Please provide a File-Extension to add.")>
    <DataRow("/addext -machine", "Please provide a File-Extension to add.")>
    <DataRow("/delext", "Please provide a File-Extension to delete.")>
    <DataRow("/delext -user", "Please provide a File-Extension to delete.")>
    <DataRow("/delext -machine", "Please provide a File-Extension to delete.")>
    Public Sub MissingValue_ShowsErrorAndExitsWithOne(arguments As String, expectedError As String)

        Dim result As ProcessResult = RunPaths(arguments)

        Assert.AreEqual(1, result.ExitCode)
        StringAssert.Contains(result.Output, expectedError)

    End Sub

    ''' <summary>
    ''' The error example matches the switch that was used.
    ''' </summary>
    <TestMethod>
    <DataRow("/addext -user", "/AddExt -user")>
    <DataRow("/addext -machine", "/AddExt -machine")>
    <DataRow("/delext -user", "/DelExt -user")>
    <DataRow("/delext -machine", "/DelExt -machine")>
    Public Sub MissingValue_ShowsExampleOfTheSameSwitch(arguments As String, expectedExample As String)

        Dim result As ProcessResult = RunPaths(arguments)

        StringAssert.Contains(result.Output, $"EXAMPLE: PATHS.exe {expectedExample}")

    End Sub

#End Region

#Region " Read-Only Commands "

    ''' <summary>
    ''' The /List switch lists the four sections and the exit code is 0.
    ''' </summary>
    <TestMethod>
    <DataRow("/list")>
    <DataRow("/L")>
    Public Sub List_ShowsAllSectionsAndExitsWithZero(arguments As String)

        Dim result As ProcessResult = RunPaths(arguments)

        Assert.AreEqual(0, result.ExitCode)
        StringAssert.Contains(result.Output, "Current User's PATH:")
        StringAssert.Contains(result.Output, "Local Machine PATH:")
        StringAssert.Contains(result.Output, "Current User's PATHEXT:")
        StringAssert.Contains(result.Output, "Local Machine PATHEXT:")

    End Sub

    ''' <summary>
    ''' The /List switch shows one-based indices, the same ones accepted by /Del -User and /Del -Machine.
    ''' </summary>
    <TestMethod>
    Public Sub List_ShowsOneBasedIndices()

        Dim result As ProcessResult = RunPaths("/list")

        StringAssert.Contains(result.Output, " 01 = ")
        Assert.IsFalse(result.Output.Contains(" 00 = "), "Indices must be one-based.")

    End Sub

    ''' <summary>
    ''' The /Export switch writes a registry script file and the exit code is 0.
    ''' </summary>
    <TestMethod>
    Public Sub Export_WritesRegistryScriptAndExitsWithZero()

        Dim filePath As String = Path.Combine(Path.GetTempPath(), $"PATHS.Tests_{Guid.NewGuid():N}.reg")
        Try
            Dim result As ProcessResult = RunPaths($"/export ""{filePath}""")

            Assert.AreEqual(0, result.ExitCode)
            StringAssert.Contains(result.Output, "Export done.")
            Assert.IsTrue(File.Exists(filePath))

            Dim text As String = File.ReadAllText(filePath, Encoding.Unicode)
            StringAssert.StartsWith(text, "Windows Registry Editor Version 5.00")
            StringAssert.Contains(text, "[HKEY_CURRENT_USER\Environment]")
            StringAssert.Contains(text, "[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment]")
            StringAssert.Contains(text, """PATH""=hex(2):")

        Finally
            File.Delete(filePath)

        End Try

    End Sub

    ''' <summary>
    ''' The /Export switch with extra arguments is rejected and the exit code is 1.
    ''' </summary>
    <TestMethod>
    Public Sub Export_ExtraArguments_ShowsErrorAndExitsWithOne()

        Dim result As ProcessResult = RunPaths("/export ""C:\a.reg"" extra")

        Assert.AreEqual(1, result.ExitCode)
        StringAssert.Contains(result.Output, "Please provide a File to export the data.")

    End Sub

#End Region

End Class

#End Region
