' ***********************************************************************
' Author   : Elektro
' Modified : 08-November-2026
' ***********************************************************************
' <copyright file="PathUtilExportTests.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports PATHS.Tools

#End Region

#Region " PathUtil Export Tests "

''' <summary>
''' Pure logic tests for the registry script (*.reg) generation of <see cref="PathUtil"/>.
''' <para></para>
''' These tests do not access the Windows registry.
''' </summary>
<TestClass>
Public NotInheritable Class PathUtilExportTests

#Region " Helpers "

    ''' <summary>
    ''' Decodes a <c>"Name"=hex(2):...</c> value line (possibly wrapped in multiple lines) of a registry script file.
    ''' </summary>
    ''' <param name="valueLines">The value line(s).</param>
    ''' <param name="valueName">Returns the decoded value name.</param>
    ''' <returns>The decoded value data (without the null terminator).</returns>
    Friend Shared Function DecodeHexExpandString(valueLines As String, ByRef valueName As String) As String

        Dim joined As String = Regex.Replace(valueLines, "\\\r?\n\s*", String.Empty)
        Dim match As Match = Regex.Match(joined, "^""(?<name>(?:[^""\\]|\\.)*)""=hex\(2\):(?<hex>[0-9a-f,]*)$")
        Assert.IsTrue(match.Success, $"Unexpected value line format: {joined}")

        valueName = match.Groups("name").Value.Replace("\""", """").Replace("\\", "\")

        Dim bytes As Byte() = match.Groups("hex").Value.
            Split(","c).
            Select(Function(token As String) Byte.Parse(token, NumberStyles.HexNumber, CultureInfo.InvariantCulture)).
            ToArray()

        Assert.IsTrue(bytes.Length >= 2 AndAlso bytes(bytes.Length - 1) = 0 AndAlso bytes(bytes.Length - 2) = 0,
                      "The data must be null-terminated.")

        Return Encoding.Unicode.GetString(bytes, 0, bytes.Length - 2)

    End Function

    ''' <summary>
    ''' Gets the value line(s) for the specified value name inside a section of a registry script text.
    ''' </summary>
    ''' <param name="text">The registry script text.</param>
    ''' <param name="sectionHeader">The section header, e.g. <c>[HKEY_CURRENT_USER\Environment]</c>.</param>
    ''' <param name="valueName">The value name.</param>
    ''' <returns>The value line(s), or <see langword="Nothing"/> if the value is not found in the section.</returns>
    Friend Shared Function GetValueLines(text As String, sectionHeader As String, valueName As String) As String

        Dim sectionStart As Integer = text.IndexOf(sectionHeader, StringComparison.Ordinal)
        Assert.AreNotEqual(-1, sectionStart, $"Section not found: {sectionHeader}")

        Dim sectionEnd As Integer = text.IndexOf($"{Environment.NewLine}[", sectionStart + sectionHeader.Length, StringComparison.Ordinal)
        Dim section As String = If(sectionEnd = -1, text.Substring(sectionStart), text.Substring(sectionStart, sectionEnd - sectionStart))

        Dim match As Match = Regex.Match(section, $"^""{Regex.Escape(valueName)}""=hex\(2\):(?:[^\r\n]*\\\r?\n)*[^\r\n]*", RegexOptions.Multiline)
        Return If(match.Success, match.Value, Nothing)

    End Function

#End Region

#Region " FormatRegExpandStringValue "

    ''' <summary>
    ''' The formatted value round-trips the data, including environment variables and special characters
    ''' (regression: the original export did not escape backslashes and quotes, producing a corrupt registry script).
    ''' </summary>
    <TestMethod>
    <DataRow("")>
    <DataRow("C:\Windows")>
    <DataRow("%SystemRoot%\system32;%SystemRoot%;C:\Program Files\Tool")>
    <DataRow("C:\Dir With ""Quotes""\bin")>
    <DataRow("C:\Ñandú\Café;C:\日本語")>
    Public Sub FormatRegExpandStringValue_RoundTripsData(data As String)

        Dim name As String = Nothing
        Dim decoded As String = DecodeHexExpandString(PathUtil.FormatRegExpandStringValue("PATH", data), name)

        Assert.AreEqual("PATH", name)
        Assert.AreEqual(data, decoded)

    End Sub

    ''' <summary>
    ''' A long value is wrapped in multiple lines of at most 80 characters, each one (but the last) ending with ",\".
    ''' </summary>
    <TestMethod>
    Public Sub FormatRegExpandStringValue_LongData_IsWrapped()

        Dim data As String = String.Join(";", Enumerable.Range(1, 30).Select(Function(i As Integer) $"C:\Some Long Directory Name {i}"))
        Dim formatted As String = PathUtil.FormatRegExpandStringValue("PATH", data)
        Dim lines As String() = formatted.Split({Environment.NewLine}, StringSplitOptions.None)

        Assert.IsTrue(lines.Length > 1, "A long value must be wrapped.")
        For i As Integer = 0 To lines.Length - 1
            Assert.IsTrue(lines(i).Length <= 80, $"Line {i} exceeds 80 characters: {lines(i).Length}")
            If i < lines.Length - 1 Then
                StringAssert.EndsWith(lines(i), ",\")
            Else
                Assert.IsFalse(lines(i).EndsWith(",", StringComparison.Ordinal))
                Assert.IsFalse(lines(i).EndsWith("\", StringComparison.Ordinal))
            End If
            If i > 0 Then
                StringAssert.StartsWith(lines(i), "  ")
            End If
        Next i

        Dim name As String = Nothing
        Assert.AreEqual(data, DecodeHexExpandString(formatted, name))

    End Sub

    ''' <summary>
    ''' A short value fits in a single line.
    ''' </summary>
    <TestMethod>
    Public Sub FormatRegExpandStringValue_ShortData_IsSingleLine()

        Assert.AreEqual("""PATH""=hex(2):43,00,3a,00,5c,00,00,00", PathUtil.FormatRegExpandStringValue("PATH", "C:\"))

    End Sub

    ''' <summary>
    ''' The value name is escaped.
    ''' </summary>
    <TestMethod>
    Public Sub FormatRegExpandStringValue_EscapesValueName()

        Dim formatted As String = PathUtil.FormatRegExpandStringValue("A""B\C", "x")

        StringAssert.StartsWith(formatted, """A\""B\\C""=hex(2):")
        Dim name As String = Nothing
        DecodeHexExpandString(formatted, name)
        Assert.AreEqual("A""B\C", name)

    End Sub

    ''' <summary>
    ''' Invalid arguments throw (fail-fast).
    ''' </summary>
    <TestMethod>
    Public Sub FormatRegExpandStringValue_InvalidArguments_Throw()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.FormatRegExpandStringValue(Nothing, "x"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.FormatRegExpandStringValue(String.Empty, "x"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.FormatRegExpandStringValue("PATH", Nothing))

    End Sub

#End Region

#Region " BuildExportText "

    ''' <summary>
    ''' The export text starts with the RegEdit v5 header and contains both Environment keys.
    ''' </summary>
    <TestMethod>
    Public Sub BuildExportText_HasHeaderAndBothSections()

        Dim text As String = PathUtil.BuildExportText("A", "B", "C", "D")

        StringAssert.StartsWith(text, $"Windows Registry Editor Version 5.00{Environment.NewLine}{Environment.NewLine}")
        StringAssert.Contains(text, "[HKEY_CURRENT_USER\Environment]")
        StringAssert.Contains(text, "[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment]")
        Assert.IsTrue(text.IndexOf("[HKEY_CURRENT_USER", StringComparison.Ordinal) <
                      text.IndexOf("[HKEY_LOCAL_MACHINE", StringComparison.Ordinal))

    End Sub

    ''' <summary>
    ''' Each value is written in its own section, and round-trips its data.
    ''' </summary>
    <TestMethod>
    Public Sub BuildExportText_ValuesAreInTheirSections()

        Const userPath As String = "%USERPROFILE%\bin;C:\User Tools"
        Const userPathExt As String = ".PY"
        Const machinePath As String = "%SystemRoot%\system32;%SystemRoot%"
        Const machinePathExt As String = ".COM;.EXE;.BAT"

        Dim text As String = PathUtil.BuildExportText(userPath, userPathExt, machinePath, machinePathExt)
        Const userSection As String = "[HKEY_CURRENT_USER\Environment]"
        Const machineSection As String = "[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment]"
        Dim name As String = Nothing

        Assert.AreEqual(userPath, DecodeHexExpandString(GetValueLines(text, userSection, "PATH"), name))
        Assert.AreEqual(userPathExt, DecodeHexExpandString(GetValueLines(text, userSection, "PATHEXT"), name))
        Assert.AreEqual(machinePath, DecodeHexExpandString(GetValueLines(text, machineSection, "PATH"), name))
        Assert.AreEqual(machinePathExt, DecodeHexExpandString(GetValueLines(text, machineSection, "PATHEXT"), name))

    End Sub

    ''' <summary>
    ''' A value that does not exist (<see langword="Nothing"/>) is omitted, instead of being exported as an empty value
    ''' that would erase the value when importing the script.
    ''' </summary>
    <TestMethod>
    Public Sub BuildExportText_MissingValues_AreOmitted()

        Dim text As String = PathUtil.BuildExportText(Nothing, Nothing, "C:\A", Nothing)
        Const userSection As String = "[HKEY_CURRENT_USER\Environment]"
        Const machineSection As String = "[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment]"

        Assert.IsNull(GetValueLines(text, userSection, "PATH"))
        Assert.IsNull(GetValueLines(text, userSection, "PATHEXT"))
        Assert.IsNotNull(GetValueLines(text, machineSection, "PATH"))
        Assert.IsNull(GetValueLines(text, machineSection, "PATHEXT"))

    End Sub

    ''' <summary>
    ''' The export text never contains plain string values ("Name"="..."), which require escaping and lose the REG_EXPAND_SZ kind.
    ''' </summary>
    <TestMethod>
    Public Sub BuildExportText_DoesNotContainPlainStringValues()

        Dim text As String = PathUtil.BuildExportText("C:\A", ".PY", "C:\B", ".EXE")

        Assert.IsFalse(Regex.IsMatch(text, "^""[^""]+""=""", RegexOptions.Multiline))

    End Sub

#End Region

End Class

#End Region
