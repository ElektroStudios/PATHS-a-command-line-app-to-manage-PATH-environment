' ***********************************************************************
' Author   : Elektro
' Modified : 08-November-2026
' ***********************************************************************
' <copyright file="RegEditTests.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.Win32

Imports PATHS.Tools

#End Region

#Region " RegEdit Tests "

''' <summary>
''' Tests for the registry helpers of <see cref="RegEdit"/> that are used by <see cref="PathUtil"/>.
''' </summary>
<TestClass>
Public NotInheritable Class RegEditTests

#Region " Registry Path Formatting (Pure) "

    ''' <summary>
    ''' The root key name is resolved from both the long and the short forms, case-insensitive.
    ''' </summary>
    <TestMethod>
    <DataRow("HKEY_CURRENT_USER\Environment", "HKEY_CURRENT_USER")>
    <DataRow("HKCU\Environment", "HKEY_CURRENT_USER")>
    <DataRow("hkcu\Environment", "HKEY_CURRENT_USER")>
    <DataRow("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet", "HKEY_LOCAL_MACHINE")>
    <DataRow("HKLM\SYSTEM", "HKEY_LOCAL_MACHINE")>
    <DataRow("HKCR\.txt", "HKEY_CLASSES_ROOT")>
    <DataRow("HKEY_CLASSES_ROOT\.txt", "HKEY_CLASSES_ROOT")>
    <DataRow("HKCC\System", "HKEY_CURRENT_CONFIG")>
    <DataRow("HKEY_CURRENT_CONFIG\System", "HKEY_CURRENT_CONFIG")>
    <DataRow("HKEY_PERFORMANCE_DATA", "HKEY_PERFORMANCE_DATA")>
    <DataRow("HKCU", "HKEY_CURRENT_USER")>
    Public Sub GetRootKeyName_KnownRoots_ReturnsLongName(registryPath As String, expected As String)

        Assert.AreEqual(expected, RegEdit.GetRootKeyName(registryPath))

    End Sub

    ''' <summary>
    ''' An unknown root key returns an empty name.
    ''' </summary>
    <TestMethod>
    <DataRow("Software\Microsoft")>
    <DataRow("HKXX\Software")>
    <DataRow("")>
    Public Sub GetRootKeyName_UnknownRoot_ReturnsEmpty(registryPath As String)

        Assert.AreEqual(String.Empty, RegEdit.GetRootKeyName(registryPath))

    End Sub

    ''' <summary>
    ''' The subkey path is the path without the root key and without leading/trailing separators.
    ''' </summary>
    <TestMethod>
    <DataRow("HKEY_CURRENT_USER\Environment", "Environment")>
    <DataRow("HKCU\Software\PATHS.Tests\", "Software\PATHS.Tests")>
    <DataRow("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment", "SYSTEM\CurrentControlSet\Control\Session Manager\Environment")>
    <DataRow("\Software\Microsoft\", "Software\Microsoft")>
    <DataRow("Software\Microsoft", "Software\Microsoft")>
    Public Sub GetSubKeyPath_ReturnsPathWithoutRoot(registryPath As String, expected As String)

        Assert.AreEqual(expected, RegEdit.GetSubKeyPath(registryPath))

    End Sub

    ''' <summary>
    ''' The Environment key paths used by <see cref="PathUtil"/> are split correctly.
    ''' </summary>
    <TestMethod>
    Public Sub PathUtilEnvironmentKeys_AreSplitCorrectly()

        Assert.AreEqual("HKEY_CURRENT_USER", RegEdit.GetRootKeyName(PathUtil.EnvironmentPathCurrentUser))
        Assert.AreEqual("Environment", RegEdit.GetSubKeyPath(PathUtil.EnvironmentPathCurrentUser))
        Assert.AreEqual("HKEY_LOCAL_MACHINE", RegEdit.GetRootKeyName(PathUtil.EnvironmentPathAllUsers))
        Assert.AreEqual("SYSTEM\CurrentControlSet\Control\Session Manager\Environment", RegEdit.GetSubKeyPath(PathUtil.EnvironmentPathAllUsers))

    End Sub

#End Region

#Region " Root Keys (Registry) "

    ''' <summary>
    ''' The root key objects are resolved from both the long and the short forms.
    ''' </summary>
    <TestMethod>
    <TestCategory(TestCategories.Registry)>
    Public Sub GetRootKey_KnownRoots_ReturnsRegistryRoot()

        Assert.AreSame(Registry.CurrentUser, RegEdit.GetRootKey("HKCU\Environment"))
        Assert.AreSame(Registry.CurrentUser, RegEdit.GetRootKey("HKEY_CURRENT_USER\Environment"))
        Assert.AreSame(Registry.LocalMachine, RegEdit.GetRootKey("HKLM\SYSTEM"))
        Assert.AreSame(Registry.LocalMachine, RegEdit.GetRootKey("HKEY_LOCAL_MACHINE\SYSTEM"))
        Assert.AreSame(Registry.ClassesRoot, RegEdit.GetRootKey("HKCR\.txt"))
        Assert.AreSame(Registry.CurrentConfig, RegEdit.GetRootKey("HKCC\System"))

    End Sub

    ''' <summary>
    ''' An unknown root key returns <see langword="Nothing"/>.
    ''' </summary>
    <TestMethod>
    <TestCategory(TestCategories.Registry)>
    Public Sub GetRootKey_UnknownRoot_ReturnsNothing()

        Assert.IsNull(RegEdit.GetRootKey("Software\Microsoft"))

    End Sub

#End Region

#Region " Values (Registry Sandbox) "

    ''' <summary>
    ''' Values can be created, read, checked and deleted inside the sandbox key.
    ''' </summary>
    <TestMethod>
    <TestCategory(TestCategories.Registry)>
    Public Sub Values_CreateReadExistDelete()

        Using sandbox As New RegistrySandbox()
            Dim keyPath As String = sandbox.CurrentUserKeyPath

            Assert.IsFalse(RegEdit.ExistValue(keyPath, "TestValue"))

            RegEdit.CreateValue(Of String)(keyPath, "TestValue", "Hello World!")
            Assert.IsTrue(RegEdit.ExistValue(keyPath, "TestValue"))
            Assert.AreEqual("Hello World!", RegEdit.GetValueData(Of String)(keyPath, "TestValue"))
            Assert.AreEqual(RegistryValueKind.String, sandbox.GetRawValueKind(PathUtil.UserMode.Current, "TestValue"))

            RegEdit.DeleteValue(keyPath, "TestValue")
            Assert.IsFalse(RegEdit.ExistValue(keyPath, "TestValue"))
            Assert.IsNull(RegEdit.GetValueData(Of String)(keyPath, "TestValue"))
        End Using

    End Sub

    ''' <summary>
    ''' Subkeys can be created, checked and deleted inside the sandbox key.
    ''' </summary>
    <TestMethod>
    <TestCategory(TestCategories.Registry)>
    Public Sub SubKeys_CreateExistDelete()

        Using sandbox As New RegistrySandbox()
            Dim keyPath As String = $"{sandbox.CurrentUserKeyPath}\Child"

            Assert.IsFalse(RegEdit.ExistSubKey(keyPath))

            RegEdit.CreateSubKey(keyPath).Dispose()
            Assert.IsTrue(RegEdit.ExistSubKey(keyPath))

            RegEdit.DeleteSubKey(keyPath)
            Assert.IsFalse(RegEdit.ExistSubKey(keyPath))
        End Using

    End Sub

    ''' <summary>
    ''' Null or blank arguments throw <see cref="ArgumentNullException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub Values_NullOrBlankArguments_Throw()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegEdit.CreateValue(Of String)(" ", "Sub", "Name", "x"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegEdit.CreateValue(Of String)("HKCU", " ", "Name", "x"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegEdit.CreateValue(Of String)("HKCU", "Sub", " ", "x"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegEdit.ExistValue("HKCU", "Sub", Nothing))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegEdit.GetValueData(Of String)(String.Empty, "Sub", "Name"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegEdit.DeleteValue("HKCU", String.Empty, "Name"))

    End Sub

#End Region

End Class

#End Region
