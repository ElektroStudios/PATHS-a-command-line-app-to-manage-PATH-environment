' ***********************************************************************
' Author   : Elektro
' Modified : 08-November-2026
' ***********************************************************************
' <copyright file="PathUtilRegistryTests.vb" company="Elektro Studios">
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
Imports System.Linq
Imports System.Text

Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.Win32

Imports PATHS.Tools

#End Region

#Region " PathUtil Registry Tests "

''' <summary>
''' Integration tests for the registry operations of <see cref="PathUtil"/>.
''' <para></para>
''' Every test runs against a temporary sandbox key under <c>HKEY_CURRENT_USER\Software\PATHS.Tests</c>
''' (see <see cref="RegistrySandbox"/>), so the real PATH and PATHEXT values are never read nor modified,
''' and no administrator privileges are required.
''' </summary>
<TestClass>
<TestCategory(TestCategories.Registry)>
Public NotInheritable Class PathUtilRegistryTests

#Region " Fields "

    ''' <summary>
    ''' The registry sandbox of the current test.
    ''' </summary>
    Private sandbox As RegistrySandbox

#End Region

#Region " Initialization / Cleanup "

    ''' <summary>
    ''' Creates the registry sandbox for the current test.
    ''' </summary>
    <TestInitialize>
    Public Sub TestInitialize()

        Me.sandbox = New RegistrySandbox()

    End Sub

    ''' <summary>
    ''' Deletes the registry sandbox of the current test.
    ''' </summary>
    <TestCleanup>
    Public Sub TestCleanup()

        Me.sandbox.Dispose()

    End Sub

#End Region

#Region " Sandbox "

    ''' <summary>
    ''' The sandbox redirects <see cref="PathUtil"/> to the sandbox keys, and disposing it restores the real keys.
    ''' </summary>
    <TestMethod>
    Public Sub Sandbox_RedirectsAndRestoresEnvironmentKeys()

        Assert.AreEqual(Me.sandbox.CurrentUserKeyPath, PathUtil.EnvironmentPathCurrentUser)
        Assert.AreEqual(Me.sandbox.AllUsersKeyPath, PathUtil.EnvironmentPathAllUsers)

        Me.sandbox.Dispose()

        Assert.AreEqual("HKEY_CURRENT_USER\Environment", PathUtil.EnvironmentPathCurrentUser)
        Assert.AreEqual("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment", PathUtil.EnvironmentPathAllUsers)
        Using key As RegistryKey = Registry.CurrentUser.OpenSubKey(Me.sandbox.SubKeyPath)
            Assert.IsNull(key, "The sandbox key must be deleted.")
        End Using

    End Sub

    ''' <summary>
    ''' Overriding the keys with null or empty paths throws (fail-fast).
    ''' </summary>
    <TestMethod>
    Public Sub OverrideEnvironmentKeys_NullOrEmpty_Throws()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.OverrideEnvironmentKeys(Nothing, "HKCU\A"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.OverrideEnvironmentKeys("HKCU\A", " "))

    End Sub

#End Region

#Region " Exists / Get "

    ''' <summary>
    ''' When the values do not exist, the exists methods return False and the getters return empty data.
    ''' </summary>
    <TestMethod>
    <DataRow(PathUtil.UserMode.Current)>
    <DataRow(PathUtil.UserMode.AllUsers)>
    Public Sub MissingValues_ExistsIsFalseAndDataIsEmpty(userMode As PathUtil.UserMode)

        Assert.IsFalse(PathUtil.PathExists(userMode))
        Assert.IsFalse(PathUtil.PathExtExists(userMode))
        Assert.AreEqual(String.Empty, PathUtil.GetPathDataString(userMode))
        Assert.AreEqual(String.Empty, PathUtil.GetPathExtDataString(userMode))
        Assert.AreEqual(0, PathUtil.GetPathDataList(userMode).Count())
        Assert.AreEqual(0, PathUtil.GetPathExtDataList(userMode).Count())

    End Sub

    ''' <summary>
    ''' An existing empty value exists.
    ''' </summary>
    <TestMethod>
    Public Sub EmptyValue_Exists()

        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATH", String.Empty, RegistryValueKind.ExpandString)

        Assert.IsTrue(PathUtil.PathExists(PathUtil.UserMode.Current))
        Assert.AreEqual(String.Empty, PathUtil.GetPathDataString(PathUtil.UserMode.Current))

    End Sub

    ''' <summary>
    ''' The getters read the raw data, without expanding the environment variables
    ''' (regression: the original code expanded them, and then wrote them back expanded as REG_SZ).
    ''' </summary>
    <TestMethod>
    Public Sub GetPathDataString_DoesNotExpandEnvironmentVariables()

        Const data As String = "%SystemRoot%\system32;%SystemRoot%"
        Me.sandbox.SetRawValue(PathUtil.UserMode.AllUsers, "PATH", data, RegistryValueKind.ExpandString)

        Assert.AreEqual(data, PathUtil.GetPathDataString(PathUtil.UserMode.AllUsers))
        CollectionAssert.AreEqual({"%SystemRoot%\system32", "%SystemRoot%"}, PathUtil.GetPathDataList(PathUtil.UserMode.AllUsers).ToArray())

    End Sub

    ''' <summary>
    ''' The user modes are isolated: each one reads its own key.
    ''' </summary>
    <TestMethod>
    Public Sub UserModes_ReadTheirOwnKey()

        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATH", "C:\User", RegistryValueKind.ExpandString)
        Me.sandbox.SetRawValue(PathUtil.UserMode.AllUsers, "PATH", "C:\Machine", RegistryValueKind.ExpandString)
        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATHEXT", ".USR", RegistryValueKind.String)
        Me.sandbox.SetRawValue(PathUtil.UserMode.AllUsers, "PATHEXT", ".MCH", RegistryValueKind.String)

        Assert.AreEqual("C:\User", PathUtil.GetPathDataString(PathUtil.UserMode.Current))
        Assert.AreEqual("C:\Machine", PathUtil.GetPathDataString(PathUtil.UserMode.AllUsers))
        Assert.AreEqual(".USR", PathUtil.GetPathExtDataString(PathUtil.UserMode.Current))
        Assert.AreEqual(".MCH", PathUtil.GetPathExtDataString(PathUtil.UserMode.AllUsers))

    End Sub

    ''' <summary>
    ''' A PATH value that is not a string (corrupt) throws instead of being silently ignored.
    ''' </summary>
    <TestMethod>
    Public Sub GetPathDataString_NonStringValue_Throws()

        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATH", 123, RegistryValueKind.DWord)

        Assert.ThrowsExactly(Of InvalidOperationException)(Sub() PathUtil.GetPathDataString(PathUtil.UserMode.Current))

    End Sub

    ''' <summary>
    ''' An unexpected user mode throws <see cref="ArgumentException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub UnexpectedUserMode_Throws()

        Dim invalid As PathUtil.UserMode = CType(99, PathUtil.UserMode)

        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.GetPathDataString(invalid))
        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.GetPathExtDataString(invalid))
        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.PathExists(invalid))
        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.PathExtExists(invalid))
        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.CreatePath(invalid, "C:\A"))
        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.CreatePathExt(invalid, ".PY"))

    End Sub

#End Region

#Region " Create "

    ''' <summary>
    ''' Creating a new PATH value writes it as REG_EXPAND_SZ.
    ''' </summary>
    <TestMethod>
    Public Sub CreatePath_NewValue_IsExpandString()

        PathUtil.CreatePath(PathUtil.UserMode.Current, "C:\A;C:\B")

        Assert.IsTrue(PathUtil.PathExists(PathUtil.UserMode.Current))
        Assert.AreEqual("C:\A;C:\B", Me.sandbox.GetRawValue(PathUtil.UserMode.Current, "PATH"))
        Assert.AreEqual(RegistryValueKind.ExpandString, Me.sandbox.GetRawValueKind(PathUtil.UserMode.Current, "PATH"))

    End Sub

    ''' <summary>
    ''' Creating a value without data creates an empty value.
    ''' </summary>
    <TestMethod>
    Public Sub CreatePath_DefaultData_CreatesEmptyValue()

        PathUtil.CreatePath(PathUtil.UserMode.AllUsers)
        PathUtil.CreatePathExt(PathUtil.UserMode.AllUsers)

        Assert.IsTrue(PathUtil.PathExists(PathUtil.UserMode.AllUsers))
        Assert.IsTrue(PathUtil.PathExtExists(PathUtil.UserMode.AllUsers))
        Assert.AreEqual(String.Empty, Me.sandbox.GetRawValue(PathUtil.UserMode.AllUsers, "PATH"))
        Assert.AreEqual(String.Empty, Me.sandbox.GetRawValue(PathUtil.UserMode.AllUsers, "PATHEXT"))

    End Sub

    ''' <summary>
    ''' Rewriting an existing REG_SZ value preserves its kind.
    ''' </summary>
    <TestMethod>
    Public Sub CreatePathExt_ExistingStringValue_PreservesKind()

        Me.sandbox.SetRawValue(PathUtil.UserMode.AllUsers, "PATHEXT", ".COM", RegistryValueKind.String)

        PathUtil.CreatePathExt(PathUtil.UserMode.AllUsers, ".COM;.EXE")

        Assert.AreEqual(".COM;.EXE", Me.sandbox.GetRawValue(PathUtil.UserMode.AllUsers, "PATHEXT"))
        Assert.AreEqual(RegistryValueKind.String, Me.sandbox.GetRawValueKind(PathUtil.UserMode.AllUsers, "PATHEXT"))

    End Sub

    ''' <summary>
    ''' Rewriting an existing REG_EXPAND_SZ value preserves its kind and its environment variables.
    ''' </summary>
    <TestMethod>
    Public Sub CreatePath_ExistingExpandStringValue_PreservesKindAndVariables()

        Me.sandbox.SetRawValue(PathUtil.UserMode.AllUsers, "PATH", "%SystemRoot%", RegistryValueKind.ExpandString)

        PathUtil.CreatePath(PathUtil.UserMode.AllUsers, "%SystemRoot%;%SystemRoot%\system32")

        Assert.AreEqual("%SystemRoot%;%SystemRoot%\system32", Me.sandbox.GetRawValue(PathUtil.UserMode.AllUsers, "PATH"))
        Assert.AreEqual(RegistryValueKind.ExpandString, Me.sandbox.GetRawValueKind(PathUtil.UserMode.AllUsers, "PATH"))

    End Sub

    ''' <summary>
    ''' Writing environment variables into a REG_SZ value promotes it to REG_EXPAND_SZ.
    ''' </summary>
    <TestMethod>
    Public Sub CreatePath_VariablesIntoStringValue_PromotesToExpandString()

        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATH", "C:\A", RegistryValueKind.String)

        PathUtil.CreatePath(PathUtil.UserMode.Current, "C:\A;%USERPROFILE%\bin")

        Assert.AreEqual(RegistryValueKind.ExpandString, Me.sandbox.GetRawValueKind(PathUtil.UserMode.Current, "PATH"))

    End Sub

    ''' <summary>
    ''' Writing into a registry key that does not exist throws instead of failing silently.
    ''' </summary>
    <TestMethod>
    Public Sub CreatePath_MissingKey_Throws()

        PathUtil.OverrideEnvironmentKeys($"HKEY_CURRENT_USER\{Me.sandbox.SubKeyPath}\DoesNotExist", Me.sandbox.AllUsersKeyPath)

        Assert.ThrowsExactly(Of InvalidOperationException)(Sub() PathUtil.CreatePath(PathUtil.UserMode.Current, "C:\A"))
        Assert.ThrowsExactly(Of InvalidOperationException)(Sub() PathUtil.PathExists(PathUtil.UserMode.Current))

    End Sub

#End Region

#Region " Directories "

    ''' <summary>
    ''' Adding a directory appends it and keeps the existing entries (including environment variables) untouched.
    ''' </summary>
    <TestMethod>
    Public Sub AddDirectory_AppendsAndKeepsExistingRawEntries()

        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATH", "%USERPROFILE%\bin;C:\A", RegistryValueKind.ExpandString)

        PathUtil.AddDirectory(PathUtil.UserMode.Current, "C:\New Dir")

        Assert.AreEqual("%USERPROFILE%\bin;C:\A;C:\New Dir", Me.sandbox.GetRawValue(PathUtil.UserMode.Current, "PATH"))
        Assert.AreEqual(RegistryValueKind.ExpandString, Me.sandbox.GetRawValueKind(PathUtil.UserMode.Current, "PATH"))
        Assert.IsTrue(PathUtil.ContainsDirectory(PathUtil.UserMode.Current, "c:\new dir\"))

    End Sub

    ''' <summary>
    ''' Adding a directory into a missing PATH value creates it.
    ''' </summary>
    <TestMethod>
    Public Sub AddDirectory_MissingValue_CreatesIt()

        PathUtil.AddDirectory(PathUtil.UserMode.AllUsers, "C:\A")

        Assert.AreEqual("C:\A", Me.sandbox.GetRawValue(PathUtil.UserMode.AllUsers, "PATH"))

    End Sub

    ''' <summary>
    ''' Adding a directory that already exists does not duplicate it.
    ''' </summary>
    <TestMethod>
    Public Sub AddDirectory_Existing_IsNotDuplicated()

        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATH", "C:\A;C:\B", RegistryValueKind.ExpandString)

        PathUtil.AddDirectory(PathUtil.UserMode.Current, "c:\a\")

        Assert.AreEqual("C:\A;C:\B", Me.sandbox.GetRawValue(PathUtil.UserMode.Current, "PATH"))

    End Sub

    ''' <summary>
    ''' Adding an invalid directory throws, and the PATH is not modified.
    ''' </summary>
    <TestMethod>
    Public Sub AddDirectory_Invalid_ThrowsAndDoesNotModify()

        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATH", "C:\A", RegistryValueKind.ExpandString)

        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.AddDirectory(PathUtil.UserMode.Current, "C:\B;C:\C"))
        Assert.AreEqual("C:\A", Me.sandbox.GetRawValue(PathUtil.UserMode.Current, "PATH"))

    End Sub

    ''' <summary>
    ''' Deleting a directory by name removes all its occurrences, and only from the specified user mode.
    ''' </summary>
    <TestMethod>
    Public Sub DeleteDirectory_ByName_RemovesOnlyFromSpecifiedUserMode()

        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATH", "C:\A;C:\B;c:\a\", RegistryValueKind.ExpandString)
        Me.sandbox.SetRawValue(PathUtil.UserMode.AllUsers, "PATH", "C:\A", RegistryValueKind.ExpandString)

        PathUtil.DeleteDirectory(PathUtil.UserMode.Current, "C:\A")

        Assert.AreEqual("C:\B", Me.sandbox.GetRawValue(PathUtil.UserMode.Current, "PATH"))
        Assert.AreEqual("C:\A", Me.sandbox.GetRawValue(PathUtil.UserMode.AllUsers, "PATH"))

    End Sub

    ''' <summary>
    ''' Deleting a directory by zero-based index removes exactly that entry.
    ''' </summary>
    <TestMethod>
    Public Sub DeleteDirectory_ByIndex_RemovesThatEntry()

        Me.sandbox.SetRawValue(PathUtil.UserMode.AllUsers, "PATH", "C:\A;C:\B;C:\C", RegistryValueKind.ExpandString)

        PathUtil.DeleteDirectory(PathUtil.UserMode.AllUsers, 1)

        Assert.AreEqual("C:\A;C:\C", Me.sandbox.GetRawValue(PathUtil.UserMode.AllUsers, "PATH"))

    End Sub

    ''' <summary>
    ''' Deleting a directory by an out of range index throws, and the PATH is not modified.
    ''' </summary>
    <TestMethod>
    <DataRow(-1)>
    <DataRow(3)>
    Public Sub DeleteDirectory_ByOutOfRangeIndex_ThrowsAndDoesNotModify(index As Integer)

        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATH", "C:\A;C:\B;C:\C", RegistryValueKind.ExpandString)

        Assert.ThrowsExactly(Of ArgumentOutOfRangeException)(Sub() PathUtil.DeleteDirectory(PathUtil.UserMode.Current, index))
        Assert.AreEqual("C:\A;C:\B;C:\C", Me.sandbox.GetRawValue(PathUtil.UserMode.Current, "PATH"))

    End Sub

#End Region

#Region " Extensions "

    ''' <summary>
    ''' Adding an extension without the leading dot stores it normalized
    ''' (regression: the original code stored "PY" instead of ".PY").
    ''' </summary>
    <TestMethod>
    Public Sub AddExtension_WithoutDot_IsNormalized()

        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATHEXT", ".COM;.EXE", RegistryValueKind.String)

        PathUtil.AddExtension(PathUtil.UserMode.Current, "PY")

        Assert.AreEqual(".COM;.EXE;.PY", Me.sandbox.GetRawValue(PathUtil.UserMode.Current, "PATHEXT"))
        Assert.IsTrue(PathUtil.ContainsExtension(PathUtil.UserMode.Current, "py"))

    End Sub

    ''' <summary>
    ''' Adding an extension that already exists does not duplicate it
    ''' (regression: the original code appended duplicates).
    ''' </summary>
    <TestMethod>
    Public Sub AddExtension_Existing_IsNotDuplicated()

        Me.sandbox.SetRawValue(PathUtil.UserMode.AllUsers, "PATHEXT", ".COM;.EXE", RegistryValueKind.String)

        PathUtil.AddExtension(PathUtil.UserMode.AllUsers, "exe")

        Assert.AreEqual(".COM;.EXE", Me.sandbox.GetRawValue(PathUtil.UserMode.AllUsers, "PATHEXT"))

    End Sub

    ''' <summary>
    ''' Deleting an extension by name modifies the PATHEXT value and NEVER the PATH value
    ''' (regression: the original code wrote the remaining extensions into the PATH value, destroying it).
    ''' </summary>
    <TestMethod>
    Public Sub DeleteExtension_ByName_ModifiesPathExtAndNotPath()

        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATH", "C:\A;C:\B", RegistryValueKind.ExpandString)
        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATHEXT", ".COM;.PY;.EXE", RegistryValueKind.String)

        PathUtil.DeleteExtension(PathUtil.UserMode.Current, ".PY")

        Assert.AreEqual(".COM;.EXE", Me.sandbox.GetRawValue(PathUtil.UserMode.Current, "PATHEXT"))
        Assert.AreEqual("C:\A;C:\B", Me.sandbox.GetRawValue(PathUtil.UserMode.Current, "PATH"))

    End Sub

    ''' <summary>
    ''' Deleting an extension without the leading dot works
    ''' (regression: the original code discarded the result of String.Insert).
    ''' </summary>
    <TestMethod>
    Public Sub DeleteExtension_WithoutDot_Works()

        Me.sandbox.SetRawValue(PathUtil.UserMode.AllUsers, "PATHEXT", ".COM;.PY;.EXE", RegistryValueKind.String)

        PathUtil.DeleteExtension(PathUtil.UserMode.AllUsers, "py")

        Assert.AreEqual(".COM;.EXE", Me.sandbox.GetRawValue(PathUtil.UserMode.AllUsers, "PATHEXT"))

    End Sub

    ''' <summary>
    ''' Deleting an extension by zero-based index removes exactly that entry, without touching the PATH.
    ''' </summary>
    <TestMethod>
    Public Sub DeleteExtension_ByIndex_RemovesThatEntry()

        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATH", "C:\A", RegistryValueKind.ExpandString)
        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATHEXT", ".COM;.PY;.EXE", RegistryValueKind.String)

        PathUtil.DeleteExtension(PathUtil.UserMode.Current, 0)

        Assert.AreEqual(".PY;.EXE", Me.sandbox.GetRawValue(PathUtil.UserMode.Current, "PATHEXT"))
        Assert.AreEqual("C:\A", Me.sandbox.GetRawValue(PathUtil.UserMode.Current, "PATH"))
        Assert.ThrowsExactly(Of ArgumentOutOfRangeException)(Sub() PathUtil.DeleteExtension(PathUtil.UserMode.Current, 5))

    End Sub

#End Region

#Region " Export "

    ''' <summary>
    ''' Exporting writes a UTF-16 registry script that contains the raw data of the four values.
    ''' </summary>
    <TestMethod>
    Public Sub Export_WritesUnicodeRegistryScriptWithRawData()

        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATH", "%USERPROFILE%\bin;C:\A", RegistryValueKind.ExpandString)
        Me.sandbox.SetRawValue(PathUtil.UserMode.Current, "PATHEXT", ".PY", RegistryValueKind.String)
        Me.sandbox.SetRawValue(PathUtil.UserMode.AllUsers, "PATH", "%SystemRoot%\system32", RegistryValueKind.ExpandString)
        Me.sandbox.SetRawValue(PathUtil.UserMode.AllUsers, "PATHEXT", ".COM;.EXE", RegistryValueKind.String)

        Dim filePath As String = Path.Combine(Path.GetTempPath(), $"PATHS.Tests_{Guid.NewGuid():N}.reg")
        Try
            PathUtil.Export(filePath)

            Dim bytes As Byte() = File.ReadAllBytes(filePath)
            Assert.IsTrue(bytes.Length > 2 AndAlso bytes(0) = &HFF AndAlso bytes(1) = &HFE, "The file must be UTF-16 LE with BOM.")

            Dim text As String = File.ReadAllText(filePath, Encoding.Unicode)
            Const userSection As String = "[HKEY_CURRENT_USER\Environment]"
            Const machineSection As String = "[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment]"
            Dim name As String = Nothing

            StringAssert.StartsWith(text, "Windows Registry Editor Version 5.00")
            Assert.AreEqual("%USERPROFILE%\bin;C:\A", PathUtilExportTests.DecodeHexExpandString(PathUtilExportTests.GetValueLines(text, userSection, "PATH"), name))
            Assert.AreEqual(".PY", PathUtilExportTests.DecodeHexExpandString(PathUtilExportTests.GetValueLines(text, userSection, "PATHEXT"), name))
            Assert.AreEqual("%SystemRoot%\system32", PathUtilExportTests.DecodeHexExpandString(PathUtilExportTests.GetValueLines(text, machineSection, "PATH"), name))
            Assert.AreEqual(".COM;.EXE", PathUtilExportTests.DecodeHexExpandString(PathUtilExportTests.GetValueLines(text, machineSection, "PATHEXT"), name))

        Finally
            File.Delete(filePath)

        End Try

    End Sub

    ''' <summary>
    ''' Exporting omits the values that do not exist.
    ''' </summary>
    <TestMethod>
    Public Sub Export_MissingValues_AreOmitted()

        Me.sandbox.SetRawValue(PathUtil.UserMode.AllUsers, "PATH", "C:\A", RegistryValueKind.ExpandString)

        Dim filePath As String = Path.Combine(Path.GetTempPath(), $"PATHS.Tests_{Guid.NewGuid():N}.reg")
        Try
            PathUtil.Export(filePath)
            Dim text As String = File.ReadAllText(filePath, Encoding.Unicode)

            Assert.IsNull(PathUtilExportTests.GetValueLines(text, "[HKEY_CURRENT_USER\Environment]", "PATH"))
            Assert.IsNotNull(PathUtilExportTests.GetValueLines(text, "[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment]", "PATH"))

        Finally
            File.Delete(filePath)

        End Try

    End Sub

    ''' <summary>
    ''' Exporting to a null or empty file path throws (fail-fast).
    ''' </summary>
    <TestMethod>
    Public Sub Export_NullOrEmptyPath_Throws()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.Export(Nothing))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.Export("  "))

    End Sub

#End Region

End Class

#End Region
