' ***********************************************************************
' Author   : Elektro
' Modified : 08-November-2026
' ***********************************************************************
' <copyright file="RegistrySandbox.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports Microsoft.Win32

Imports PATHS.Tools

#End Region

#Region " Registry Sandbox "

''' <summary>
''' Creates a temporary registry key under <c>HKEY_CURRENT_USER\Software\PATHS.Tests</c>
''' with two subkeys (<c>CurrentUser</c> and <c>AllUsers</c>) that emulate the Environment keys,
''' and redirects <see cref="PathUtil"/> to them.
''' <para></para>
''' Disposing the instance deletes the temporary key and restores the real Environment keys of <see cref="PathUtil"/>,
''' so the real PATH and PATHEXT values are never modified by the tests.
''' </summary>
Friend NotInheritable Class RegistrySandbox : Implements IDisposable

#Region " Constants "

    ''' <summary>
    ''' The parent subkey path (relative to HKCU) of all the sandbox keys.
    ''' </summary>
    Private Const ParentSubKeyPath As String = "Software\PATHS.Tests"

#End Region

#Region " Properties "

    ''' <summary>
    ''' Gets the subkey path (relative to HKCU) of this sandbox.
    ''' </summary>
    ''' <value>The subkey path (relative to HKCU) of this sandbox.</value>
    Friend ReadOnly Property SubKeyPath As String

    ''' <summary>
    ''' Gets the full registry path of the key that emulates the current user's Environment key.
    ''' </summary>
    ''' <value>The full registry path of the key that emulates the current user's Environment key.</value>
    Friend ReadOnly Property CurrentUserKeyPath As String

    ''' <summary>
    ''' Gets the full registry path of the key that emulates the local machine Environment key.
    ''' </summary>
    ''' <value>The full registry path of the key that emulates the local machine Environment key.</value>
    Friend ReadOnly Property AllUsersKeyPath As String

#End Region

#Region " Fields "

    ''' <summary>
    ''' Flag to detect redundant calls when disposing.
    ''' </summary>
    Private isDisposed As Boolean

#End Region

#Region " Constructors "

    ''' <summary>
    ''' Initializes a new instance of the <see cref="RegistrySandbox"/> class.
    ''' </summary>
    Friend Sub New()

        Me.SubKeyPath = $"{ParentSubKeyPath}\{Guid.NewGuid():N}"
        Me.CurrentUserKeyPath = $"HKEY_CURRENT_USER\{Me.SubKeyPath}\CurrentUser"
        Me.AllUsersKeyPath = $"HKEY_CURRENT_USER\{Me.SubKeyPath}\AllUsers"

        Registry.CurrentUser.CreateSubKey($"{Me.SubKeyPath}\CurrentUser").Dispose()
        Registry.CurrentUser.CreateSubKey($"{Me.SubKeyPath}\AllUsers").Dispose()

        PathUtil.OverrideEnvironmentKeys(Me.CurrentUserKeyPath, Me.AllUsersKeyPath)

    End Sub

#End Region

#Region " Methods "

    ''' <summary>
    ''' Writes a value directly into one of the sandbox keys, bypassing <see cref="PathUtil"/>.
    ''' </summary>
    ''' <param name="userMode">The user mode that selects the sandbox key.</param>
    ''' <param name="valueName">The value name.</param>
    ''' <param name="data">The value data.</param>
    ''' <param name="kind">The value kind.</param>
    Friend Sub SetRawValue(userMode As PathUtil.UserMode, valueName As String, data As Object, kind As RegistryValueKind)

        Using key As RegistryKey = Me.OpenKey(userMode, writable:=True)
            key.SetValue(valueName, data, kind)
        End Using

    End Sub

    ''' <summary>
    ''' Reads the raw (not expanded) data of a value directly from one of the sandbox keys, bypassing <see cref="PathUtil"/>.
    ''' </summary>
    ''' <param name="userMode">The user mode that selects the sandbox key.</param>
    ''' <param name="valueName">The value name.</param>
    ''' <returns>The raw value data, or <see langword="Nothing"/> if the value does not exist.</returns>
    Friend Function GetRawValue(userMode As PathUtil.UserMode, valueName As String) As Object

        Using key As RegistryKey = Me.OpenKey(userMode, writable:=False)
            Return key.GetValue(valueName, Nothing, RegistryValueOptions.DoNotExpandEnvironmentNames)
        End Using

    End Function

    ''' <summary>
    ''' Gets the kind of a value directly from one of the sandbox keys.
    ''' </summary>
    ''' <param name="userMode">The user mode that selects the sandbox key.</param>
    ''' <param name="valueName">The value name.</param>
    ''' <returns>The value kind.</returns>
    Friend Function GetRawValueKind(userMode As PathUtil.UserMode, valueName As String) As RegistryValueKind

        Using key As RegistryKey = Me.OpenKey(userMode, writable:=False)
            Return key.GetValueKind(valueName)
        End Using

    End Function

    ''' <summary>
    ''' Opens one of the sandbox keys.
    ''' </summary>
    ''' <param name="userMode">The user mode that selects the sandbox key.</param>
    ''' <param name="writable">If <see langword="True"/>, opens the key with write access.</param>
    ''' <returns>The opened registry key.</returns>
    ''' <exception cref="InvalidOperationException">The sandbox key does not exist.</exception>
    Private Function OpenKey(userMode As PathUtil.UserMode, writable As Boolean) As RegistryKey

        Dim childName As String = If(userMode = PathUtil.UserMode.Current, "CurrentUser", "AllUsers")
        Dim key As RegistryKey = Registry.CurrentUser.OpenSubKey($"{Me.SubKeyPath}\{childName}", writable)
        If key Is Nothing Then
            Throw New InvalidOperationException($"The sandbox key does not exist: ""{Me.SubKeyPath}\{childName}"".")
        End If
        Return key

    End Function

#End Region

#Region " IDisposable Implementation "

    ''' <summary>
    ''' Deletes the sandbox key and restores the real Environment keys of <see cref="PathUtil"/>.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose

        If Me.isDisposed Then
            Return
        End If

        PathUtil.RestoreEnvironmentKeys()
        Registry.CurrentUser.DeleteSubKeyTree(Me.SubKeyPath, throwOnMissingSubKey:=False)

        ' Remove the parent key when no other sandbox remains.
        Using parent As RegistryKey = Registry.CurrentUser.OpenSubKey(ParentSubKeyPath, writable:=False)
            If parent IsNot Nothing AndAlso parent.SubKeyCount = 0 AndAlso parent.ValueCount = 0 Then
                parent.Close()
                Registry.CurrentUser.DeleteSubKey(ParentSubKeyPath, throwOnMissingSubKey:=False)
            End If
        End Using

        Me.isDisposed = True

    End Sub

#End Region

End Class

#End Region
