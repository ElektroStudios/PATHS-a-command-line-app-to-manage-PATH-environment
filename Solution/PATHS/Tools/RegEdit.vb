' ***********************************************************************
' Author   : Elektro
' Modified : 18-March-2015
' ***********************************************************************
' <copyright file="RegEdit.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Usage Examples "

' ----------------
' Instance RegInfo
' ----------------
'
'    Dim regInfo As New RegEdit.RegInfo
'    With regInfo
'        .RootKeyName = "HKCU"
'        .SubKeyPath = "Subkey Path"
'        .ValueName = "Value Name"
'        .ValueType = Microsoft.Win32.RegistryValueKind.String
'        .ValueData = "Hello World!"
'    End With
'
'    Dim regInfoByte As New RegEdit.RegInfo(Of Byte())
'    With regInfoByte
'        .RootKeyName = "HKCU"
'        .SubKeyPath = "Subkey Path"
'        .ValueName = "Value Name"
'        .ValueType = Microsoft.Win32.RegistryValueKind.Binary
'        .ValueData = System.Text.Encoding.ASCII.GetBytes("Hello World!")
'    End With

' ----------------
' Create SubKey
' ----------------
'
'    RegEdit.CreateSubKey(fullKeyPath:="HKCU\Subkey Path\")
'    RegEdit.CreateSubKey(rootKeyName:="HKCU",
'                         subKeyPath:="Subkey Path")
'    RegEdit.CreateSubKey(regInfo:=regInfoByte)
'
'    Dim regKey1 As Microsoft.Win32.RegistryKey =
'        RegEdit.CreateSubKey(fullKeyPath:="HKCU\Subkey Path\",
'                             registryKeyPermissionCheck:=Microsoft.Win32.RegistryKeyPermissionCheck.Default,
'                             registryOptions:=Microsoft.Win32.RegistryOptions.None)
'
'    Dim regKey2 As Microsoft.Win32.RegistryKey =
'        RegEdit.CreateSubKey(rootKeyName:="HKCU",
'                             subKeyPath:="Subkey Path",
'                             registryKeyPermissionCheck:=Microsoft.Win32.RegistryKeyPermissionCheck.Default,
'                             registryOptions:=Microsoft.Win32.RegistryOptions.None)
'
'    Dim regInfo2 As RegEdit.RegInfo(Of String) = RegEdit.CreateSubKey(Of String)(fullKeyPath:="HKCU\Subkey Path\")
'    Dim regInfo3 As RegEdit.RegInfo(Of String) = RegEdit.CreateSubKey(Of String)(rootKeyName:="HKCU",
'                                                                                 subKeyPath:="Subkey Path")

' ----------------
' Create Value
' ----------------
'
'    RegEdit.CreateValue(fullKeyPath:="HKCU\Subkey Path\",
'                        valueName:="Value Name",
'                        valueData:="Value Data",
'                        valueType:=Microsoft.Win32.RegistryValueKind.String)
'
'    RegEdit.CreateValue(rootKeyName:="HKCU",
'                        subKeyPath:="Subkey Path",
'                        valueName:="Value Name",
'                        valueData:="Value Data",
'                        valueType:=Microsoft.Win32.RegistryValueKind.String)
'
'    RegEdit.CreateValue(regInfo:=regInfoByte)
'
'    RegEdit.CreateValue(Of String)(fullKeyPath:="HKCU\Subkey Path\",
'                                   valueName:="Value Name",
'                                   valueData:="Value Data",
'                                   valueType:=Microsoft.Win32.RegistryValueKind.String)
'
'    RegEdit.CreateValue(Of String)(rootKeyName:="HKCU",
'                                   subKeyPath:="Subkey Path",
'                                   valueName:="Value Name",
'                                   valueData:="Value Data",
'                                   valueType:=Microsoft.Win32.RegistryValueKind.String)
'
'    RegEdit.CreateValue(Of Byte())(regInfo:=regInfoByte)

' ----------------
' Copy KeyTree
' ----------------
'
'    RegEdit.CopyKeyTree(sourceFullKeyPath:="HKCU\Source Subkey Path\",
'                        targetFullKeyPath:="HKCU\Target Subkey Path\")
'
'    RegEdit.CopyKeyTree(sourceRootKeyName:="HKCU",
'                        sourceSubKeyPath:="Source Subkey Path\",
'                        targetRootKeyName:="HKCU",
'                        targetSubKeyPath:="Target Subkey Path\")

' ----------------
' Move KeyTree
' ----------------
'
'    RegEdit.MoveKeyTree(sourceFullKeyPath:="HKCU\Source Subkey Path\",
'                        targetFullKeyPath:="HKCU\Target Subkey Path\")
'
'    RegEdit.MoveKeyTree(sourceRootKeyName:="HKCU",
'                        sourceSubKeyPath:="Source Subkey Path\",
'                        targetRootKeyName:="HKCU",
'                        targetSubKeyPath:="Target Subkey Path\")

' ----------------
' Copy SubKeys
' ----------------
'
'    RegEdit.CopySubKeys(sourceFullKeyPath:="HKCU\Source Subkey Path\",
'                        targetFullKeyPath:="HKCU\Target Subkey Path\")
'
'    RegEdit.CopySubKeys(sourceRootKeyName:="HKCU",
'                        sourceSubKeyPath:="Source Subkey Path\",
'                        targetRootKeyName:="HKCU",
'                        targetSubKeyPath:="Target Subkey Path\")

' ----------------
' Move SubKeys
' ----------------
'
'    RegEdit.MoveSubKeys(sourceFullKeyPath:="HKCU\Source Subkey Path\",
'                        targetFullKeyPath:="HKCU\Target Subkey Path\")
'
'    RegEdit.MoveSubKeys(sourceRootKeyName:="HKCU",
'                        sourceSubKeyPath:="Source Subkey Path\",
'                        targetRootKeyName:="HKCU",
'                        targetSubKeyPath:="Target Subkey Path\")

' ----------------
' Copy Value
' ----------------
'
'    RegEdit.CopyValue(sourceFullKeyPath:="HKCU\Source Subkey Path\",
'                      sourceValueName:="Value Name",
'                      targetFullKeyPath:="HKCU\Target Subkey Path\",
'                      targetValueName:="Value Name")
'
'    RegEdit.CopyValue(sourceRootKeyName:="HKCU",
'                      sourceSubKeyPath:="Source Subkey Path\",
'                      sourceValueName:="Value Name",
'                      targetRootKeyName:="HKCU",
'                      targetSubKeyPath:="Target Subkey Path\",
'                      targetValueName:="Value Name")

' ----------------
' Move Value
' ----------------
'
'    RegEdit.MoveValue(sourceFullKeyPath:="HKCU\Source Subkey Path\",
'                      sourceValueName:="Value Name",
'                      targetFullKeyPath:="HKCU\Target Subkey Path\",
'                      targetValueName:="Value Name")
'
'    RegEdit.MoveValue(sourceRootKeyName:="HKCU",
'                      sourceSubKeyPath:="Source Subkey Path\",
'                      sourceValueName:="Value Name",
'                      targetRootKeyName:="HKCU",
'                      targetSubKeyPath:="Target Subkey Path\",
'                      targetValueName:="Value Name")

' ----------------
' DeleteValue
' ----------------
'
'    RegEdit.DeleteValue(fullKeyPath:="HKCU\Subkey Path\",
'                        valueName:="Value Name",
'                        throwOnMissingValue:=True)
'
'    RegEdit.DeleteValue(rootKeyName:="HKCU",
'                        subKeyPath:="Subkey Path",
'                        valueName:="Value Name",
'                        throwOnMissingValue:=True)
'
'    RegEdit.DeleteValue(regInfo:=regInfoByte,
'                        throwOnMissingValue:=True)

' ----------------
' Delete SubKey
' ----------------
'
'    RegEdit.DeleteSubKey(fullKeyPath:="HKCU\Subkey Path\",
'                         throwOnMissingSubKey:=False)
'
'    RegEdit.DeleteSubKey(rootKeyName:="HKCU",
'                         subKeyPath:="Subkey Path",
'                         throwOnMissingSubKey:=False)
'
'    RegEdit.DeleteSubKey(regInfo:=regInfoByte,
'                         throwOnMissingSubKey:=False)

' ----------------
' Exist SubKey?
' ----------------
'
'    Dim exist1 As Boolean = RegEdit.ExistSubKey(fullKeyPath:="HKCU\Subkey Path\")
'
'    Dim exist2 As Boolean = RegEdit.ExistSubKey(rootKeyName:="HKCU",
'                                                subKeyPath:="Subkey Path")

' ----------------
' Exist Value?
' ----------------
'
'    Dim exist3 As Boolean = RegEdit.ExistValue(fullKeyPath:="HKCU\Subkey Path\",
'                                               valueName:="Value Name")
'
'    Dim exist4 As Boolean = RegEdit.ExistValue(rootKeyName:="HKCU",
'                                               subKeyPath:="Subkey Path",
'                                               valueName:="Value Name")

' ----------------
' Value Is Empty?
' ----------------
'
'    Dim isEmpty1 As Boolean = RegEdit.ValueIsEmpty(fullKeyPath:="HKCU\Subkey Path\",
'                                                   valueName:="Value Name")
'
'    Dim isEmpty2 As Boolean = RegEdit.ValueIsEmpty(rootKeyName:="HKCU",
'                                                   subKeyPath:="Subkey Path",
'                                                   valueName:="Value Name")

' ----------------
' Export Key
' ----------------
'
'    RegEdit.ExportKey(fullKeyPath:="HKCU\Subkey Path\",
'                      outputFile:="C:\Backup.reg")
'
'    RegEdit.ExportKey(rootKeyName:="HKCU",
'                      subKeyPath:="Subkey Path",
'                      outputFile:="C:\Backup.reg")

' ----------------
' Import RegFile
' ----------------
'
'    RegEdit.ImportRegFile(regFilePath:="C:\Backup.reg")

' ----------------
' Jump To Key
' ----------------
'
'    RegEdit.JumpToKey(fullKeyPath:="HKCU\Subkey Path\")
'
'    RegEdit.JumpToKey(rootKeyName:="HKCU",
'                      subKeyPath:="Subkey Path")

' ----------------
' Find SubKey
' ----------------
'
'    Dim regInfoSubkeyCol As IEnumerable(Of RegEdit.Reginfo) =
'        RegEdit.FindSubKey(rootKeyName:="HKCU",
'                           subKeyPath:="Subkey Path",
'                           subKeyName:="Subkey Name",
'                           matchFullSubKeyName:=False,
'                           ignoreCase:=True,
'                           searchOption:=IO.SearchOption.AllDirectories)
'
'    For Each reg As RegEdit.RegInfo In regInfoSubkeyCol
'        Debug.WriteLine(reg.RootKeyName)
'        Debug.WriteLine(reg.SubKeyPath)
'        Debug.WriteLine(reg.ValueName)
'        Debug.WriteLine(reg.ValueData.ToString)
'        Debug.WriteLine("")
'    Next reg

' ----------------
' Find Value
' ----------------
'
'    Dim regInfoValueNameCol As IEnumerable(Of RegEdit.Reginfo) =
'        RegEdit.FindValue(rootKeyName:="HKCU",
'                              subKeyPath:="Subkey Path",
'                              valueName:="Value Name",
'                              matchFullValueName:=False,
'                              ignoreCase:=True,
'                              searchOption:=IO.SearchOption.AllDirectories)
'
'    For Each reg As RegEdit.RegInfo In regInfoValueNameCol
'        Debug.WriteLine(reg.RootKeyName)
'        Debug.WriteLine(reg.SubKeyPath)
'        Debug.WriteLine(reg.ValueName)
'        Debug.WriteLine(reg.ValueData.ToString)
'        Debug.WriteLine("")
'    Next reg

' ----------------
' Find Value Data
' ----------------
'
'    Dim regInfoValueDataCol As IEnumerable(Of RegEdit.Reginfo) =
'        RegEdit.FindValueData(rootKeyName:="HKCU",
'                              subKeyPath:="Subkey Path",
'                              valueData:="Value Data",
'                              matchFullData:=False,
'                              ignoreCase:=True,
'                              searchOption:=IO.SearchOption.AllDirectories)
'
'    For Each reg As RegEdit.RegInfo In regInfoValueDataCol
'        Debug.WriteLine(reg.RootKeyName)
'        Debug.WriteLine(reg.SubKeyPath)
'        Debug.WriteLine(reg.ValueName)
'        Debug.WriteLine(reg.ValueData.ToString)
'        Debug.WriteLine("")
'    Next reg

' ----------------
' Get...
' ----------------
'
'    Dim rootKeyName As String = RegEdit.GetRootKeyName(registryPath:="HKCU\Subkey Path\")
'    Dim subKeyPath As String = RegEdit.GetSubKeyPath(registryPath:="HKCU\Subkey Path\")
'    Dim rootKey As Microsoft.Win32.RegistryKey = RegEdit.GetRootKey(registryPath:="HKCU\Subkey Path\")

' ----------------
' Get Value Data
' ----------------
'
'    Dim dataObject As Object = RegEdit.GetValueData(rootKeyName:="HKCU",
'                                                    subKeyPath:="Subkey Path",
'                                                    valueName:="Value Name")
'
'    Dim dataString As String = RegEdit.GetValueData(Of String)(fullKeyPath:="HKCU\Subkey Path\",
'                                                               valueName:="Value Name",
'                                                               registryValueOptions:=Microsoft.Win32.RegistryValueOptions.DoNotExpandEnvironmentNames)
'
'    Dim dataByte As Byte() = RegEdit.GetValueData(Of Byte())(regInfo:=regInfoByte,
'                                                             registryValueOptions:=Microsoft.Win32.RegistryValueOptions.None)
'    Debug.WriteLine("dataByte=" & String.Join(",", dataByte))

' -----------------
' Set UserAccessKey
' -----------------
'
'RegEdit.SetUserAccessKey(fullKeyPath:="HKCU\Subkey Path",
'                         userAccess:={RegEdit.ReginiUserAccess.AdministratorsFullAccess})
'
'RegEdit.SetUserAccessKey(rootKeyName:="HKCU",
'                         subKeyPath:="Subkey Path",
'                         userAccess:={RegEdit.ReginiUserAccess.AdministratorsFullAccess,
'                                      RegEdit.ReginiUserAccess.CreatorFullAccess,
'                                      RegEdit.ReginiUserAccess.SystemFullAccess})

#End Region

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports Microsoft.Win32
Imports System.IO
Imports System.Security.AccessControl
Imports System.Text

#End Region

#Region " RegEdit "

Namespace Tools

    ''' <summary>
    ''' Contains registry related methods.
    ''' </summary>
    Public NotInheritable Class RegEdit

#Region " Enumerations "

        ''' <summary>
        ''' Specifies an user identifier for Regini.exe application.
        ''' </summary>
        Public Enum ReginiUserAccess As Integer

            AdministratorsFullAccess = 1

            AdministratorsReadAccess = 2

            AdministratorsReadAndWriteAccess = 3

            AdministratorsReadWriteAndDeleteAccess = 4

            AdministratorsReadWriteAndExecuteAccess = 20

            CreatorFullAccess = 5

            CreatorReadAndWriteAccess = 6

            InteractiveUserFullAccess = 21

            InteractiveUserReadAndWriteAccess = 22

            InteractiveUserReadWriteAndDeleteAccess = 23

            PowerUsersFullAccess = 11

            PowerUsersReadAndWriteAccess = 12

            PowerUsersReadWriteAndDeleteAccess = 13

            SystemFullAccess = 17

            SystemOperatorsFullAccess = 14

            SystemOperatorsReadAndWriteAccess = 15

            SystemOperatorsReadWriteAndDeleteAccess = 16

            SystemReadAccess = 19

            SystemReadAndWriteAccess = 18

            WorldFullAccess = 7

            WorldReadAccess = 8

            WorldReadAndWriteAccess = 9

            WorldReadWriteAndDeleteAccess = 10

        End Enum

#End Region

#Region " Types "

#Region " RegInfo(Of T) "

        ''' <summary>
        ''' Defines a registry key with a specified type of data.
        ''' </summary>
        Public Class RegInfo(Of T)

#Region " Properties "

            ''' <summary>
            ''' Gets or sets the registry root key.
            ''' ( eg: HKCU or HKEY_CURRENT_USER)
            ''' </summary>
            ''' <value>The registry root key.</value>
            Public Property RootKeyName As String

            ''' <summary>
            ''' Gets or sets the registry subkey path.
            ''' ( eg: subkey1\subkey2\ )
            ''' </summary>
            ''' <value>The registry subkey path.</value>
            Public Property SubKeyPath As String

            ''' <summary>
            ''' Gets or sets the registry value name.
            ''' </summary>
            ''' <value>The registry value name.</value>
            Public Property ValueName As String

            ''' <summary>
            ''' Gets or sets the type of the registry value.
            ''' </summary>
            ''' <value>The type of the registry value.</value>
            Public Property ValueType As RegistryValueKind = RegistryValueKind.Unknown

            ''' <summary>
            ''' Gets or sets the data of the registry value.
            ''' </summary>
            ''' <value>The data of the registry value.</value>
            Public Overridable Property ValueData As T

            ''' <summary>
            ''' Gets the full key path.
            ''' </summary>
            ''' <value>The full key path.</value>
            Public ReadOnly Property FullKeyPath As String
                Get
                    Return String.Format("{0}\{1}", RegEdit.GetRootKeyName(Me.RootKeyName), RegEdit.GetSubKeyPath(Me.SubKeyPath))
                End Get
            End Property

            ''' <summary>
            ''' Gets a <see cref="RegistryKey"/> instance of the current RootKey\SubKey.
            ''' </summary>
            ''' <value>A <see cref="RegistryKey"/> instance of the current RootKey\SubKey.</value>
            Public ReadOnly Property RegistryKey(Optional ByVal registryKeyPermissionCheck As RegistryKeyPermissionCheck =
                                                                RegistryKeyPermissionCheck.Default,
                                                 Optional ByVal registryOptions As RegistryRights =
                                                                RegistryRights.CreateSubKey) As RegistryKey
                Get
                    Return RegEdit.GetRootKey(Me.RootKeyName).OpenSubKey(Me.SubKeyPath, registryKeyPermissionCheck, registryOptions)
                End Get

            End Property

#End Region

        End Class

#End Region

#Region " RegInfo "

        ''' <summary>
        ''' Defines a registry key.
        ''' </summary>
        Public NotInheritable Class RegInfo : Inherits RegInfo(Of Object)

#Region " Properties "

            ''' <summary>
            ''' Gets or sets the data of the registry value.
            ''' </summary>
            ''' <value>The data of the registry value.</value>
            Public Overrides Property ValueData As Object

#End Region

        End Class

#End Region

#End Region

#Region " Public Methods "

#Region " Create SubKey "

        ''' <summary>
        ''' Creates a new registry subkey.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="registryKeyPermissionCheck">The registry key permission check.</param>
        ''' <param name="registryOptions">The registry options.</param>
        ''' <returns>The registry key.</returns>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath</exception>
        Public Shared Function CreateSubKey(Of T)(ByVal rootKeyName As String,
                                                  ByVal subKeyPath As String,
                                                  Optional ByVal registryKeyPermissionCheck As RegistryKeyPermissionCheck =
                                                                 RegistryKeyPermissionCheck.Default,
                                                  Optional ByVal registryOptions As RegistryOptions =
                                                                 RegistryOptions.None) As RegInfo(Of T)

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Dim reg As RegistryKey = Nothing
                Try

                    Dim regInfo As New RegInfo(Of T)
                    With regInfo
                        .RootKeyName = GetRootKeyName(rootKeyName)
                        .SubKeyPath = GetSubKeyPath(subKeyPath)
                    End With

                    reg = GetRootKey(rootKeyName)
                    Using reg
                        reg.CreateSubKey(GetSubKeyPath(subKeyPath), registryKeyPermissionCheck, registryOptions)
                    End Using

                    Return regInfo

                Catch ex As Exception
                    Throw

                Finally
                    If reg IsNot Nothing Then
                        reg.Close()
                    End If

                End Try

            End If

        End Function

        ''' <summary>
        ''' Creates a new registry subkey.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <param name="registryKeyPermissionCheck">The registry key permission check.</param>
        ''' <param name="registryOptions">The registry options.</param>
        ''' <returns>The registry key.</returns>
        Public Shared Function CreateSubKey(Of T)(ByVal fullKeyPath As String,
                                                  Optional ByVal registryKeyPermissionCheck As RegistryKeyPermissionCheck =
                                                                 RegistryKeyPermissionCheck.Default,
                                                  Optional ByVal registryOptions As RegistryOptions =
                                                                 RegistryOptions.None) As RegInfo(Of T)

            Return CreateSubKey(Of T)(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath), registryKeyPermissionCheck, registryOptions)

        End Function

        ''' <summary>
        ''' Creates a new registry subkey.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="regInfo">A <see cref="Regedit.RegInfo(Of T)"/> instance containing the registry info.</param>
        ''' <param name="registryKeyPermissionCheck">The registry key permission check.</param>
        ''' <param name="registryOptions">The registry options.</param>
        ''' <returns>The registry key.</returns>
        Public Shared Function CreateSubKey(Of T)(ByVal regInfo As RegInfo(Of T),
                                                  Optional ByVal registryKeyPermissionCheck As RegistryKeyPermissionCheck =
                                                                 RegistryKeyPermissionCheck.Default,
                                                  Optional ByVal registryOptions As RegistryOptions =
                                                                 RegistryOptions.None) As RegInfo(Of T)

            Return CreateSubKey(Of T)(regInfo.RootKeyName, regInfo.SubKeyPath, registryKeyPermissionCheck, registryOptions)

        End Function

        ''' <summary>
        ''' Creates a new registry subkey.
        ''' </summary>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="registryKeyPermissionCheck">The registry key permission check.</param>
        ''' <param name="registryOptions">The registry options.</param>
        ''' <returns>The registry key.</returns>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath</exception>
        Public Shared Function CreateSubKey(ByVal rootKeyName As String,
                                            ByVal subKeyPath As String,
                                            Optional ByVal registryKeyPermissionCheck As RegistryKeyPermissionCheck =
                                                           RegistryKeyPermissionCheck.Default,
                                            Optional ByVal registryOptions As RegistryOptions =
                                                           RegistryOptions.None) As RegistryKey

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Dim reg As RegistryKey = Nothing
                Try
                    reg = GetRootKey(rootKeyName)
                    Using reg
                        Return reg.CreateSubKey(GetSubKeyPath(subKeyPath), registryKeyPermissionCheck, registryOptions)
                    End Using

                Catch ex As Exception
                    Throw

                Finally
                    If reg IsNot Nothing Then
                        reg.Close()
                    End If

                End Try

            End If

        End Function

        ''' <summary>
        ''' Creates a new registry subkey.
        ''' </summary>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <param name="registryKeyPermissionCheck">The registry key permission check.</param>
        ''' <param name="registryOptions">The registry options.</param>
        ''' <returns>The registry key.</returns>
        Public Shared Function CreateSubKey(ByVal fullKeyPath As String,
                                            Optional ByVal registryKeyPermissionCheck As RegistryKeyPermissionCheck =
                                                           RegistryKeyPermissionCheck.Default,
                                            Optional ByVal registryOptions As RegistryOptions =
                                                           RegistryOptions.None) As RegistryKey

            Return CreateSubKey(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath), registryKeyPermissionCheck, registryOptions)

        End Function

        ''' <summary>
        ''' Creates a new registry subkey.
        ''' </summary>
        ''' <param name="regInfo">A <see cref="Regedit.RegInfo(Of T)"/> instance containing the registry info.</param>
        ''' <param name="registryKeyPermissionCheck">The registry key permission check.</param>
        ''' <param name="registryOptions">The registry options.</param>
        ''' <returns>The registry key.</returns>
        Public Shared Function CreateSubKey(ByVal regInfo As RegInfo,
                                            Optional ByVal registryKeyPermissionCheck As RegistryKeyPermissionCheck =
                                                           RegistryKeyPermissionCheck.Default,
                                            Optional ByVal registryOptions As RegistryOptions =
                                                           RegistryOptions.None) As RegistryKey

            Return CreateSubKey(regInfo.RootKeyName, regInfo.SubKeyPath, registryKeyPermissionCheck, registryOptions)

        End Function

#End Region

#Region " Delete SubKey "

        ''' <summary>
        ''' Deletes a registry subkey.
        ''' </summary>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="throwOnMissingSubKey">If set to <c>true</c>, throws an exception on missing subkey.</param>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath</exception>
        Public Shared Sub DeleteSubKey(ByVal rootKeyName As String,
                                       ByVal subKeyPath As String,
                                       Optional ByVal throwOnMissingSubKey As Boolean = False)

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Dim reg As RegistryKey = Nothing
                Try
                    reg = GetRootKey(rootKeyName)
                    Using reg
                        reg.DeleteSubKeyTree(GetSubKeyPath(subKeyPath), throwOnMissingSubKey)
                    End Using

                Catch ex As Exception
                    Throw

                Finally
                    If reg IsNot Nothing Then
                        reg.Close()
                    End If

                End Try

            End If

        End Sub

        ''' <summary>
        ''' Deletes a registry subkey.
        ''' </summary>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <param name="throwOnMissingSubKey">If set to <c>true</c>, throws an exception on missing subkey.</param>
        Public Shared Sub DeleteSubKey(ByVal fullKeyPath As String,
                                       Optional ByVal throwOnMissingSubKey As Boolean = False)

            DeleteSubKey(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath), throwOnMissingSubKey)

        End Sub

        ''' <summary>
        ''' Deletes a registry subkey.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="regInfo">A <see cref="Regedit.RegInfo(of T)"/> instance containing the registry info.</param>
        ''' <param name="throwOnMissingSubKey">If set to <c>true</c>, throws an exception on missing subkey.</param>
        Public Shared Sub DeleteSubKey(Of T)(ByVal regInfo As RegInfo(Of T),
                                             Optional ByVal throwOnMissingSubKey As Boolean = False)

            DeleteSubKey(regInfo.RootKeyName, regInfo.SubKeyPath, throwOnMissingSubKey)

        End Sub

#End Region

#Region " Create Value "

        ''' <summary>
        ''' Creates or replaces a registry value.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <param name="valueData">The value data.</param>
        ''' <param name="valueType">The registry value type.</param>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath or valueName</exception>
        Public Shared Sub CreateValue(Of T)(ByVal rootKeyName As String,
                                            ByVal subKeyPath As String,
                                            ByVal valueName As String,
                                            ByVal valueData As T,
                                            Optional ByVal valueType As RegistryValueKind = RegistryValueKind.String)

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            ElseIf String.IsNullOrEmpty(valueName) OrElse String.IsNullOrWhiteSpace(valueName) Then
                Throw New ArgumentNullException("valueName")

            Else
                Dim reg As RegistryKey = Nothing
                Try
                    reg = GetRootKey(rootKeyName)
                    Using reg
                        reg.OpenSubKey(GetSubKeyPath(subKeyPath), writable:=True).SetValue(valueName, valueData, valueType)
                    End Using

                Catch ex As Exception
                    Throw

                Finally
                    If reg IsNot Nothing Then
                        reg.Close()
                    End If

                End Try

            End If

        End Sub

        ''' <summary>
        ''' Creates or replaces a registry value.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <param name="valueData">The value data.</param>
        ''' <param name="valueType">The registry value type.</param>
        Public Shared Sub CreateValue(Of T)(ByVal fullKeyPath As String,
                                            ByVal valueName As String,
                                            ByVal valueData As T,
                                            Optional ByVal valueType As RegistryValueKind = RegistryValueKind.String)

            CreateValue(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath), valueName, valueData, valueType)

        End Sub

        ''' <summary>
        ''' Creates or replaces a registry value.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="regInfo">A <see cref="Regedit.RegInfo(Of T)"/> instance containing the registry info.</param>
        Public Shared Sub CreateValue(Of T)(ByVal regInfo As RegInfo(Of T))

            CreateValue(regInfo.RootKeyName, regInfo.SubKeyPath, regInfo.ValueName, regInfo.ValueData, regInfo.ValueType)

        End Sub

#End Region

#Region " Delete Value "

        ''' <summary>
        ''' Deletes a registry subkey.
        ''' </summary>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <param name="throwOnMissingValue">If set to <c>true</c>, throws an exception on missing value.</param>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath or valueName</exception>
        Public Shared Sub DeleteValue(ByVal rootKeyName As String,
                                      ByVal subKeyPath As String,
                                      ByVal valueName As String,
                                      Optional ByVal throwOnMissingValue As Boolean = False)

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            ElseIf String.IsNullOrEmpty(valueName) OrElse String.IsNullOrWhiteSpace(valueName) Then
                Throw New ArgumentNullException("valueName")

            Else
                Dim reg As RegistryKey = Nothing
                Try
                    reg = GetRootKey(rootKeyName)
                    Using reg
                        reg.OpenSubKey(GetSubKeyPath(subKeyPath), writable:=True).DeleteValue(valueName, throwOnMissingValue)
                    End Using

                Catch ex As Exception
                    Throw

                Finally
                    If reg IsNot Nothing Then
                        reg.Close()
                    End If

                End Try

            End If

        End Sub

        ''' <summary>
        ''' Deletes a registry subkey.
        ''' </summary>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <param name="throwOnMissingValue">If set to <c>true</c>, throws an exception on missing value.</param>
        Public Shared Sub DeleteValue(ByVal fullKeyPath As String,
                                      ByVal valueName As String,
                                      Optional ByVal throwOnMissingValue As Boolean = False)

            DeleteValue(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath), valueName, throwOnMissingValue)

        End Sub

        ''' <summary>
        ''' Deletes a registry subkey.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="regInfo">A <see cref="Regedit.RegInfo(of T)"/> instance containing the registry info.</param>
        ''' <param name="throwOnMissingValue">If set to <c>true</c>, throws an exception on missing value.</param>
        Public Shared Sub DeleteValue(Of T)(ByVal regInfo As RegInfo(Of T),
                                            Optional ByVal throwOnMissingValue As Boolean = False)

            DeleteValue(regInfo.RootKeyName, regInfo.SubKeyPath, regInfo.ValueName, throwOnMissingValue)

        End Sub

#End Region

#Region " Get ValueData "

        ''' <summary>
        ''' Gets the data of a registry value.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <param name="registryValueOptions">The registry value options.</param>
        ''' <returns>The value data</returns>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath</exception>
        Public Shared Function GetValueData(Of T)(ByVal rootKeyName As String,
                                                  ByVal subKeyPath As String,
                                                  ByVal valueName As String,
                                                  Optional ByVal registryValueOptions As RegistryValueOptions =
                                                                 RegistryValueOptions.None) As T

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Dim reg As RegistryKey = Nothing
                Try
                    reg = GetRootKey(rootKeyName)
                    Using reg
                        Return DirectCast(reg.OpenSubKey(GetSubKeyPath(subKeyPath), writable:=False).
                                              GetValue(valueName, defaultValue:=Nothing, options:=registryValueOptions), T)
                    End Using

                Catch ex As Exception
                    Throw

                Finally
                    If reg IsNot Nothing Then
                        reg.Close()
                    End If

                End Try

            End If

        End Function

        ''' <summary>
        ''' Gets the data of a registry value.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <param name="registryValueOptions">The registry value options.</param>
        ''' <returns>The value data</returns>
        Public Shared Function GetValueData(Of T)(ByVal fullKeyPath As String,
                                                  ByVal valueName As String,
                                                  Optional ByVal registryValueOptions As RegistryValueOptions =
                                                                 RegistryValueOptions.None) As T

            Return GetValueData(Of T)(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath), valueName, registryValueOptions)

        End Function

        ''' <summary>
        ''' Gets the data of a registry value.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="regInfo">A <see cref="Regedit.RegInfo(of T)"/> instance containing the registry info.</param>
        ''' <param name="registryValueOptions">The registry value options.</param>
        ''' <returns>The value data</returns>
        Public Shared Function GetValueData(Of T)(ByVal regInfo As RegInfo(Of T),
                                                  Optional ByVal registryValueOptions As RegistryValueOptions =
                                                                 RegistryValueOptions.None) As T

            Return GetValueData(Of T)(regInfo.RootKeyName, regInfo.SubKeyPath, regInfo.ValueName, registryValueOptions)

        End Function

        ''' <summary>
        ''' Gets the data of a registry value.
        ''' </summary>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <param name="registryValueOptions">The registry value options.</param>
        ''' <returns>The value data</returns>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath or valueName</exception>
        Public Shared Function GetValueData(ByVal rootKeyName As String,
                                                  ByVal subKeyPath As String,
                                                  ByVal valueName As String,
                                                  Optional ByVal registryValueOptions As RegistryValueOptions =
                                                                 RegistryValueOptions.None) As Object

            Return GetValueData(Of Object)(rootKeyName, subKeyPath, valueName, registryValueOptions)

        End Function

        ''' <summary>
        ''' Gets the data of a registry value.
        ''' </summary>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <param name="registryValueOptions">The registry value options.</param>
        ''' <returns>The value data</returns>
        Public Shared Function GetValueData(ByVal fullKeyPath As String,
                                            ByVal valueName As String,
                                            Optional ByVal registryValueOptions As RegistryValueOptions =
                                                           RegistryValueOptions.None) As Object

            Return GetValueData(Of Object)(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath), valueName, registryValueOptions)

        End Function

        ''' <summary>
        ''' Gets the data of a registry value.
        ''' </summary>
        ''' <param name="regInfo">A <see cref="Regedit.RegInfo(of T)"/> instance containing the registry info.</param>
        ''' <param name="registryValueOptions">The registry value options.</param>
        ''' <returns>The value data</returns>
        Public Shared Function GetValueData(ByVal regInfo As RegInfo,
                                            Optional ByVal registryValueOptions As RegistryValueOptions =
                                                           RegistryValueOptions.None) As Object

            Return GetValueData(Of Object)(regInfo.RootKeyName, regInfo.SubKeyPath, regInfo.ValueName, registryValueOptions)

        End Function

#End Region

#Region " Exists ? "

        ''' <summary>
        ''' Determines whether a registry subkey exists.
        ''' </summary>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <returns><c>true</c> if subkey exist, <c>false</c> otherwise.</returns>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath</exception>
        Public Shared Function ExistSubKey(ByVal rootKeyName As String,
                                           ByVal subKeyPath As String) As Boolean

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Dim reg As RegistryKey = Nothing
                Try
                    reg = GetRootKey(rootKeyName)
                    Using reg
                        Return reg.OpenSubKey(GetSubKeyPath(subKeyPath), writable:=False) IsNot Nothing
                    End Using

                Catch ex As Exception
                    Throw

                Finally
                    If reg IsNot Nothing Then
                        reg.Close()
                    End If

                End Try
            End If

        End Function

        ''' <summary>
        ''' Determines whether a registry subkey exists.
        ''' </summary>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <returns><c>true</c> if subkey exist, <c>false</c> otherwise.</returns>
        Public Shared Function ExistSubKey(ByVal fullKeyPath As String) As Boolean

            Return ExistSubKey(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath))

        End Function

        ''' <summary>
        ''' Determines whether a registry value exists.
        ''' </summary>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <returns><c>true</c> if value exist, <c>false</c> otherwise.</returns>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath or valueName</exception>
        Public Shared Function ExistValue(ByVal rootKeyName As String,
                                          ByVal subKeyPath As String,
                                          ByVal valueName As String) As Boolean

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            ElseIf String.IsNullOrEmpty(valueName) OrElse String.IsNullOrWhiteSpace(valueName) Then
                Throw New ArgumentNullException("valueName")

            Else
                Dim reg As RegistryKey = Nothing
                Try
                    reg = GetRootKey(rootKeyName)
                    Using reg
                        Return reg.OpenSubKey(GetSubKeyPath(subKeyPath), writable:=False).
                                   GetValue(valueName, defaultValue:=Nothing) IsNot Nothing
                    End Using

                Catch ex As Exception
                    Throw

                Finally
                    If reg IsNot Nothing Then
                        reg.Close()
                    End If

                End Try
            End If

        End Function

        ''' <summary>
        ''' Determines whether a registry subkey exists.
        ''' </summary>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <returns><c>true</c> if subkey exist, <c>false</c> otherwise.</returns>
        Public Shared Function ExistValue(ByVal fullKeyPath As String,
                                          ByVal valueName As String) As Boolean

            Return ExistValue(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath), valueName)

        End Function

        ''' <summary>
        ''' Determines whether a registry value is empty.
        ''' </summary>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <returns><c>true</c> if value is empty, <c>false</c> otherwise.</returns>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath or valueName</exception>
        Public Shared Function ValueIsEmpty(ByVal rootKeyName As String,
                                            ByVal subKeyPath As String,
                                            ByVal valueName As String) As Boolean

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            ElseIf String.IsNullOrEmpty(valueName) OrElse String.IsNullOrWhiteSpace(valueName) Then
                Throw New ArgumentNullException("valueName")

            Else
                Dim reg As RegistryKey = Nothing
                Try
                    reg = GetRootKey(rootKeyName)
                    Using reg
                        Return String.IsNullOrEmpty(reg.OpenSubKey(GetSubKeyPath(subKeyPath), writable:=False).
                                                        GetValue(valueName, defaultValue:=Nothing).ToString)
                    End Using

                Catch ex As Exception
                    Throw

                Finally
                    If reg IsNot Nothing Then
                        reg.Close()
                    End If

                End Try
            End If

        End Function

        ''' <summary>
        ''' Determines whether a registry value is empty.
        ''' </summary>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <returns><c>true</c> if value is empty, <c>false</c> otherwise.</returns>
        Public Shared Function ValueIsEmpty(ByVal fullKeyPath As String,
                                            ByVal valueName As String) As Boolean

            Return ValueIsEmpty(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath), valueName)

        End Function

#End Region

#Region " Import / Export "

        ''' <summary>
        ''' Imports a registry file into the current registry Hive.
        ''' </summary>
        ''' <param name="regFilePath">The registry filepath.</param>
        ''' <returns><c>true</c> if operation succeeds, <c>false</c> otherwise.</returns>
        ''' <exception cref="System.ArgumentNullException">regFilePath</exception>
        Public Shared Function ImportRegFile(ByVal regFilePath As String) As Boolean

            If String.IsNullOrEmpty(regFilePath) OrElse String.IsNullOrWhiteSpace(regFilePath) Then
                Throw New ArgumentNullException("regFilePath")

            Else
                Try
                    Using proc As New Process With {
                        .StartInfo = New ProcessStartInfo() With {
                              .FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "Reg.exe"),
                              .Arguments = String.Format("Import ""{0}""", regFilePath),
                              .CreateNoWindow = True,
                              .WindowStyle = ProcessWindowStyle.Hidden,
                              .UseShellExecute = False
                            }
                        }

                        With proc
                            .Start()
                            .WaitForExit()
                        End With

                        Return Not CBool(proc.ExitCode)

                    End Using

                Catch ex As Exception
                    Throw

                End Try

            End If

        End Function

        ''' <summary>
        ''' Exports a key to a registry file.
        ''' </summary>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="OutputFile">Indicates the output file.</param>
        ''' <returns><c>true</c> if operation succeeds, <c>false</c> otherwise.</returns>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath or outputFile</exception>
        Public Shared Function ExportKey(ByVal rootKeyName As String,
                                         ByVal subKeyPath As String,
                                         ByVal outputFile As String) As Boolean

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            ElseIf String.IsNullOrEmpty(outputFile) OrElse String.IsNullOrWhiteSpace(outputFile) Then
                Throw New ArgumentNullException("outputFile")

            Else
                Dim reg As RegistryKey = Nothing
                Try
                    reg = GetRootKey(rootKeyName)
                    Using reg
                        Using proc As New Process With {
                                .StartInfo = New ProcessStartInfo() With {
                                      .FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "Reg.exe"),
                                      .Arguments = String.Format("Export ""{0}\{1}"" ""{2}"" /y",
                                                                 GetRootKeyName(rootKeyName),
                                                                 GetSubKeyPath(subKeyPath),
                                                                 outputFile),
                                      .CreateNoWindow = True,
                                      .WindowStyle = ProcessWindowStyle.Hidden,
                                      .UseShellExecute = False
                                    }
                                }

                            With proc
                                .Start()
                                .WaitForExit()
                            End With

                            Return Not CBool(proc.ExitCode)

                        End Using

                    End Using

                Catch ex As Exception
                    Throw

                Finally
                    If reg IsNot Nothing Then
                        reg.Close()
                    End If

                End Try

            End If

        End Function

        ''' <summary>
        ''' Exports a key to a registry file.
        ''' </summary>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <param name="OutputFile">Indicates the output file.</param>
        ''' <returns><c>true</c> if operation succeeds, <c>false</c> otherwise.</returns>
        Public Shared Function ExportKey(ByVal fullKeyPath As String,
                                         ByVal outputFile As String) As Boolean

            Return ExportKey(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath), outputFile)

        End Function

#End Region

#Region " Navigation "

        ''' <summary>
        ''' Runs Regedit.exe process to jump at the specified key.
        ''' </summary>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath</exception>
        Public Shared Sub JumpToKey(ByVal rootKeyName As String,
                                    ByVal subKeyPath As String)

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Try
                    CreateValue(rootKeyName:="HKCU",
                                        subKeyPath:="Software\Microsoft\Windows\CurrentVersion\Applets\Regedit",
                                        valueName:="LastKey",
                                        valueData:=String.Format("{0}\{1}", GetRootKeyName(rootKeyName), GetSubKeyPath(subKeyPath)),
                                        valueType:=RegistryValueKind.String)

                    Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Regedit.exe"))

                Catch ex As Exception
                    Throw

                End Try

            End If

        End Sub

        ''' <summary>
        ''' Runs Regedit.exe process to jump at the specified key.
        ''' </summary>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        Public Shared Sub JumpToKey(ByVal fullKeyPath As String)

            JumpToKey(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath))

        End Sub

#End Region

#Region " Copying "

        ''' <summary>
        ''' Copies a registry value (with its data) to another subkey.
        ''' </summary>
        ''' <param name="sourceFullKeyPath">The source registry key full path.</param>
        ''' <param name="sourceValueName">The source registry value name.</param>
        ''' <param name="targetFullKeyPath">The target registry key full path.</param>
        ''' <param name="targetValueName">The target registry value name.</param>
        Public Shared Sub CopyValue(ByVal sourceFullKeyPath As String,
                                    ByVal sourceValueName As String,
                                    ByVal targetFullKeyPath As String,
                                    ByVal targetValueName As String)

            CreateValue(fullKeyPath:=targetFullKeyPath,
                        valueName:=targetValueName,
                        valueData:=GetValueData(fullKeyPath:=sourceFullKeyPath, valueName:=sourceValueName),
                        valueType:=RegistryValueKind.Unknown)

        End Sub

        ''' <summary>
        ''' Copies a registry value (with its data) to another subkey.
        ''' </summary>
        ''' <param name="sourceRootKeyName">The source registry rootkey name.</param>
        ''' <param name="sourceSubKeyPath">The source registry subkey path.</param>
        ''' <param name="sourceValueName">The source registry value name.</param>
        ''' <param name="targetRootKeyName">The target registry rootkey name.</param>
        ''' <param name="targetSubKeyPath">The target registry subkey path.</param>
        ''' <param name="targetValueName">The target registry value name.</param>
        Public Shared Sub CopyValue(ByVal sourceRootKeyName As String,
                                    ByVal sourceSubKeyPath As String,
                                    ByVal sourceValueName As String,
                                    ByVal targetRootKeyName As String,
                                    ByVal targetSubKeyPath As String,
                                    ByVal targetValueName As String)

            CreateValue(rootKeyName:=targetRootKeyName,
                        subKeyPath:=targetSubKeyPath,
                        valueName:=targetValueName,
                        valueData:=GetValueData(rootKeyName:=sourceRootKeyName, subKeyPath:=sourceSubKeyPath, valueName:=sourceValueName),
                        valueType:=RegistryValueKind.Unknown)

        End Sub

        ''' <summary>
        ''' Moves a registry value (with its data) to another subkey.
        ''' </summary>
        ''' <param name="sourceFullKeyPath">The source registry key full path.</param>
        ''' <param name="sourceValueName">The source registry value name.</param>
        ''' <param name="targetFullKeyPath">The target registry key full path.</param>
        ''' <param name="targetValueName">The target registry value name.</param>
        Public Shared Sub MoveValue(ByVal sourceFullKeyPath As String,
                                    ByVal sourceValueName As String,
                                    ByVal targetFullKeyPath As String,
                                    ByVal targetValueName As String)

            CreateValue(fullKeyPath:=targetFullKeyPath,
                        valueName:=targetValueName,
                        valueData:=GetValueData(fullKeyPath:=sourceFullKeyPath, valueName:=sourceValueName),
                        valueType:=RegistryValueKind.Unknown)

            DeleteValue(fullKeyPath:=sourceFullKeyPath, valueName:=sourceValueName, throwOnMissingValue:=True)

        End Sub

        ''' <summary>
        ''' Moves a registry value (with its data) to another subkey.
        ''' </summary>
        ''' <param name="sourceRootKeyName">The source registry rootkey name.</param>
        ''' <param name="sourceSubKeyPath">The source registry subkey path.</param>
        ''' <param name="sourceValueName">The source registry value name.</param>
        ''' <param name="targetRootKeyName">The target registry rootkey name.</param>
        ''' <param name="targetSubKeyPath">The target registry subkey path.</param>
        ''' <param name="targetValueName">The target registry value name.</param>
        Public Shared Sub MoveValue(ByVal sourceRootKeyName As String,
                                    ByVal sourceSubKeyPath As String,
                                    ByVal sourceValueName As String,
                                    ByVal targetRootKeyName As String,
                                    ByVal targetSubKeyPath As String,
                                    ByVal targetValueName As String)

            CreateValue(rootKeyName:=targetRootKeyName,
                        subKeyPath:=targetSubKeyPath,
                        valueName:=targetValueName,
                        valueData:=GetValueData(rootKeyName:=sourceRootKeyName, subKeyPath:=sourceSubKeyPath, valueName:=sourceValueName),
                        valueType:=RegistryValueKind.Unknown)

            DeleteValue(rootKeyName:=sourceRootKeyName, subKeyPath:=sourceSubKeyPath, valueName:=sourceValueName, throwOnMissingValue:=True)

        End Sub

        ''' <summary>
        ''' Copies a registry key tree to another registry path.
        ''' </summary>
        ''' <param name="sourceRootKeyName">The source registry rootkey name.</param>
        ''' <param name="sourceSubKeyPath">The source registry subkey path.</param>
        ''' <param name="targetRootKeyName">The target registry rootkey name.</param>
        ''' <param name="targetSubKeyPath">The target registry subkey path.</param>
        ''' <exception cref="System.ArgumentNullException">
        ''' sourceRootKeyName or sourceSubKeyPath or targetRootKeyName or targetSubKeyPath
        ''' </exception>
        Public Shared Sub CopyKeyTree(ByVal sourceRootKeyName As String,
                                      ByVal sourceSubKeyPath As String,
                                      ByVal targetRootKeyName As String,
                                      ByVal targetSubKeyPath As String)

            If String.IsNullOrEmpty(sourceRootKeyName) OrElse String.IsNullOrWhiteSpace(sourceRootKeyName) Then
                Throw New ArgumentNullException("sourceRootKeyName")

            ElseIf String.IsNullOrEmpty(sourceSubKeyPath) OrElse String.IsNullOrWhiteSpace(sourceSubKeyPath) Then
                Throw New ArgumentNullException("sourceSubKeyPath")

            ElseIf String.IsNullOrEmpty(targetRootKeyName) OrElse String.IsNullOrWhiteSpace(targetRootKeyName) Then
                Throw New ArgumentNullException("targetRootKeyName")

            ElseIf String.IsNullOrEmpty(targetSubKeyPath) OrElse String.IsNullOrWhiteSpace(targetSubKeyPath) Then
                Throw New ArgumentNullException("targetSubKeyPath")

            Else

                Dim sourceRegistryKey As RegistryKey = Nothing
                Dim targetRegistryKey As RegistryKey = Nothing

                Try
                    sourceRegistryKey = GetRootKey(sourceRootKeyName).OpenSubKey(GetSubKeyPath(sourceSubKeyPath), writable:=False)
                    Using sourceRegistryKey

                        CreateSubKey(rootKeyName:=GetRootKeyName(targetRootKeyName), subKeyPath:=GetSubKeyPath(targetSubKeyPath))

                        targetRegistryKey = GetRootKey(targetRootKeyName).OpenSubKey(GetSubKeyPath(targetSubKeyPath), writable:=True)
                        Using targetRegistryKey
                            CopySubKeys(sourceRegistryKey, targetRegistryKey)
                        End Using

                    End Using

                Catch ex As Exception
                    Throw

                Finally
                    If sourceRegistryKey IsNot Nothing Then
                        sourceRegistryKey.Close()
                    End If
                    If targetRegistryKey IsNot Nothing Then
                        targetRegistryKey.Close()
                    End If

                End Try

            End If

        End Sub

        ''' <summary>
        ''' Copies a registry key tree to another registry path.
        ''' </summary>
        ''' <param name="sourceFullKeyPath">The source registry key full path.</param>
        ''' <param name="targetFullKeyPath">The target registry key full path.</param>
        Public Shared Sub CopyKeyTree(ByVal sourceFullKeyPath As String,
                                      ByVal targetFullKeyPath As String)

            CopyKeyTree(sourceRootKeyName:=GetRootKeyName(sourceFullKeyPath),
                        sourceSubKeyPath:=GetSubKeyPath(sourceFullKeyPath),
                        targetRootKeyName:=GetRootKeyName(targetFullKeyPath),
                        targetSubKeyPath:=GetSubKeyPath(targetFullKeyPath))

        End Sub

        ''' <summary>
        ''' Copies the sub-keys of the specified registry key.
        ''' </summary>
        ''' <param name="sourceRegistryKey">Indicates the old key.</param>
        ''' <param name="targetRegistryKey">Indicates the new key.</param>
        Private Shared Sub CopySubKeys(ByVal sourceRegistryKey As RegistryKey,
                                       ByVal targetRegistryKey As RegistryKey)

            ' Copy Values.
            For Each valueName As String In sourceRegistryKey.GetValueNames()
                targetRegistryKey.SetValue(valueName, sourceRegistryKey.GetValue(valueName))
            Next valueName

            ' Copy Subkeys.
            For Each subKeyName As String In sourceRegistryKey.GetSubKeyNames

                CreateSubKey(fullKeyPath:=String.Format("{0}\{1}", targetRegistryKey.Name, subKeyName))
                CopySubKeys(sourceRegistryKey.OpenSubKey(subKeyName, writable:=False),
                            targetRegistryKey.OpenSubKey(subKeyName, writable:=True))

            Next subKeyName

        End Sub

        ''' <summary>
        ''' Copies the sub-keys of the specified registry key.
        ''' </summary>
        ''' <param name="sourceRootKeyName">The source registry rootkey name.</param>
        ''' <param name="sourceSubKeyPath">The source registry subkey path.</param>
        ''' <param name="targetRootKeyName">The target registry rootkey name.</param>
        ''' <param name="targetSubKeyPath">The target registry subkey path.</param>
        Public Shared Sub CopySubKeys(ByVal sourceRootKeyName As String,
                                      ByVal sourceSubKeyPath As String,
                                      ByVal targetRootKeyName As String,
                                      ByVal targetSubKeyPath As String)

            Dim sourceRegistryKey As RegistryKey = Nothing
            Dim targetRegistryKey As RegistryKey = Nothing

            Try
                sourceRegistryKey = GetRootKey(sourceRootKeyName).OpenSubKey(GetSubKeyPath(sourceSubKeyPath), writable:=False)
                targetRegistryKey = GetRootKey(targetRootKeyName).OpenSubKey(GetSubKeyPath(targetSubKeyPath), writable:=True)

                CopySubKeys(sourceRegistryKey, targetRegistryKey)

            Catch ex As Exception
                Throw

            Finally
                If sourceRegistryKey IsNot Nothing Then
                    sourceRegistryKey.Close()
                End If
                If targetRegistryKey IsNot Nothing Then
                    targetRegistryKey.Close()
                End If

            End Try

        End Sub

        ''' <summary>
        ''' Copies the sub-keys of the specified registry key.
        ''' </summary>
        ''' <param name="sourceFullKeyPath">The source registry key full path.</param>
        ''' <param name="targetFullKeyPath">The target registry key full path.</param>
        Public Shared Sub CopySubKeys(ByVal sourceFullKeyPath As String,
                                      ByVal targetFullKeyPath As String)

            CopySubKeys(sourceRootKeyName:=GetRootKeyName(sourceFullKeyPath),
                        sourceSubKeyPath:=GetSubKeyPath(sourceFullKeyPath),
                        targetRootKeyName:=GetRootKeyName(targetFullKeyPath),
                        targetSubKeyPath:=GetSubKeyPath(targetFullKeyPath))

        End Sub

        ''' <summary>
        ''' Moves a registry key tree to another registry path.
        ''' </summary>
        ''' <param name="sourceRootKeyName">The source registry rootkey name.</param>
        ''' <param name="sourceSubKeyPath">The source registry subkey path.</param>
        ''' <param name="targetRootKeyName">The target registry rootkey name.</param>
        ''' <param name="targetSubKeyPath">The target registry subkey path.</param>
        ''' <exception cref="System.ArgumentNullException">
        ''' sourceRootKeyName or sourceSubKeyPath or targetRootKeyName or targetSubKeyPath
        ''' </exception>
        Public Shared Sub MoveKeyTree(ByVal sourceRootKeyName As String,
                                      ByVal sourceSubKeyPath As String,
                                      ByVal targetRootKeyName As String,
                                      ByVal targetSubKeyPath As String)

            If String.IsNullOrEmpty(sourceRootKeyName) OrElse String.IsNullOrWhiteSpace(sourceRootKeyName) Then
                Throw New ArgumentNullException("sourceRootKeyName")

            ElseIf String.IsNullOrEmpty(sourceSubKeyPath) OrElse String.IsNullOrWhiteSpace(sourceSubKeyPath) Then
                Throw New ArgumentNullException("sourceSubKeyPath")

            ElseIf String.IsNullOrEmpty(targetRootKeyName) OrElse String.IsNullOrWhiteSpace(targetRootKeyName) Then
                Throw New ArgumentNullException("targetRootKeyName")

            ElseIf String.IsNullOrEmpty(targetSubKeyPath) OrElse String.IsNullOrWhiteSpace(targetSubKeyPath) Then
                Throw New ArgumentNullException("targetSubKeyPath")

            Else

                Dim sourceRegistryKey As RegistryKey = Nothing
                Dim targetRegistryKey As RegistryKey = Nothing

                Try
                    sourceRegistryKey = GetRootKey(sourceRootKeyName).OpenSubKey(GetSubKeyPath(sourceSubKeyPath), writable:=False)
                    Using sourceRegistryKey

                        CreateSubKey(rootKeyName:=GetRootKeyName(sourceRootKeyName), subKeyPath:=GetSubKeyPath(sourceSubKeyPath))

                        targetRegistryKey = GetRootKey(targetRootKeyName).OpenSubKey(GetSubKeyPath(targetSubKeyPath), writable:=True)
                        Using targetRegistryKey
                            CopySubKeys(sourceRegistryKey, targetRegistryKey)
                        End Using

                    End Using

                    DeleteSubKey(GetRootKeyName(sourceRootKeyName), GetSubKeyPath(sourceSubKeyPath))

                Catch ex As Exception
                    Throw

                Finally
                    If sourceRegistryKey IsNot Nothing Then
                        sourceRegistryKey.Close()
                    End If
                    If targetRegistryKey IsNot Nothing Then
                        targetRegistryKey.Close()
                    End If

                End Try

            End If

        End Sub

        ''' <summary>
        ''' Moves a registry key tree to another registry path.
        ''' </summary>
        ''' <param name="sourceFullKeyPath">The source registry key full path.</param>
        ''' <param name="targetFullKeyPath">The target registry key full path.</param>
        Public Shared Sub MoveKeyTree(ByVal sourceFullKeyPath As String,
                                      ByVal targetFullKeyPath As String)

            MoveKeyTree(sourceRootKeyName:=GetRootKeyName(sourceFullKeyPath),
                        sourceSubKeyPath:=GetSubKeyPath(sourceFullKeyPath),
                        targetRootKeyName:=GetRootKeyName(targetFullKeyPath),
                        targetSubKeyPath:=GetSubKeyPath(targetFullKeyPath))

        End Sub

        ''' <summary>
        ''' Moves the sub-keys of the specified registry key.
        ''' </summary>
        ''' <param name="sourceRootKeyName">The source registry rootkey name.</param>
        ''' <param name="sourceSubKeyPath">The source registry subkey path.</param>
        ''' <param name="targetRootKeyName">The target registry rootkey name.</param>
        ''' <param name="targetSubKeyPath">The target registry subkey path.</param>
        Public Shared Sub MoveSubKeys(ByVal sourceRootKeyName As String,
                                      ByVal sourceSubKeyPath As String,
                                      ByVal targetRootKeyName As String,
                                      ByVal targetSubKeyPath As String)

            Dim sourceRegistryKey As RegistryKey = Nothing
            Dim targetRegistryKey As RegistryKey = Nothing

            Try
                CreateSubKey(GetRootKeyName(targetRootKeyName), GetSubKeyPath(targetSubKeyPath))

                sourceRegistryKey = GetRootKey(sourceRootKeyName).OpenSubKey(GetSubKeyPath(sourceSubKeyPath), writable:=False)
                targetRegistryKey = GetRootKey(targetRootKeyName).OpenSubKey(GetSubKeyPath(targetSubKeyPath), writable:=True)

                CopySubKeys(sourceRegistryKey, targetRegistryKey)
                DeleteSubKey(GetRootKeyName(sourceRootKeyName), GetSubKeyPath(sourceSubKeyPath))

            Catch ex As Exception
                Throw

            Finally
                If sourceRegistryKey IsNot Nothing Then
                    sourceRegistryKey.Close()
                End If
                If targetRegistryKey IsNot Nothing Then
                    targetRegistryKey.Close()
                End If

            End Try

        End Sub

        ''' <summary>
        ''' Moves the sub-keys of the specified registry key.
        ''' </summary>
        ''' <param name="sourceFullKeyPath">The source registry key full path.</param>
        ''' <param name="targetFullKeyPath">The target registry key full path.</param>
        Public Shared Sub MoveSubKeys(ByVal sourceFullKeyPath As String,
                                      ByVal targetFullKeyPath As String)

            MoveSubKeys(sourceRootKeyName:=GetRootKeyName(sourceFullKeyPath),
                        sourceSubKeyPath:=GetSubKeyPath(sourceFullKeyPath),
                        targetRootKeyName:=GetRootKeyName(targetFullKeyPath),
                        targetSubKeyPath:=GetSubKeyPath(targetFullKeyPath))

        End Sub

#End Region

#Region " Registry Path Formatting "

        ''' <summary>
        ''' Gets the root <see cref="RegistryKey"/> of a registry path.
        ''' </summary>
        ''' <returns>The root <see cref="RegistryKey"/> of a registry path.</returns>
        Public Shared Function GetRootKey(ByVal registryPath As String) As RegistryKey

            Select Case registryPath.ToUpper.Split("\"c).First

                Case "HKCR", "HKEY_CLASSES_ROOT"
                    Return Registry.ClassesRoot

                Case "HKCC", "HKEY_CURRENT_CONFIG"
                    Return Registry.CurrentConfig

                Case "HKCU", "HKEY_CURRENT_USER"
                    Return Registry.CurrentUser

                Case "HKLM", "HKEY_LOCAL_MACHINE"
                    Return Registry.LocalMachine

                Case "HKEY_PERFORMANCE_DATA"
                    Return Registry.PerformanceData

                Case Else
                    Return Nothing

            End Select

        End Function

        ''' <summary>
        ''' Gets the root key name of a registry path.
        ''' </summary>
        ''' <returns>The root key name of a registry path.</returns>
        Public Shared Function GetRootKeyName(ByVal registryPath As String) As String

            Select Case registryPath.ToUpper.Split("\"c).FirstOrDefault

                Case "HKCR", "HKEY_CLASSES_ROOT"
                    Return "HKEY_CLASSES_ROOT"

                Case "HKCC", "HKEY_CURRENT_CONFIG"
                    Return "HKEY_CURRENT_CONFIG"

                Case "HKCU", "HKEY_CURRENT_USER"
                    Return "HKEY_CURRENT_USER"

                Case "HKLM", "HKEY_LOCAL_MACHINE"
                    Return "HKEY_LOCAL_MACHINE"

                Case "HKEY_PERFORMANCE_DATA"
                    Return "HKEY_PERFORMANCE_DATA"

                Case Else
                    Return String.Empty

            End Select

        End Function

        ''' <summary>
        ''' Gets the subkey path of a registry path.
        ''' </summary>
        ''' <returns>The root <see cref="RegistryKey"/> of a key-path.</returns>
        Public Shared Function GetSubKeyPath(ByVal registryPath As String) As String

            Select Case String.IsNullOrEmpty(GetRootKeyName(registryPath))

                Case True
                    Return registryPath.TrimStart("\"c).TrimEnd("\"c)

                Case Else
                    Return registryPath.Substring(registryPath.IndexOf("\"c)).TrimStart("\"c).TrimEnd("\"c)

            End Select

        End Function

#End Region

#Region " Find "

        ''' <summary>
        ''' Finds on a registry path all the subkey names that matches the specified criteria.
        ''' </summary>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="subKeyName">The subkey name to find.</param>
        ''' <param name="matchFullSubKeyName">If set to <c>true</c>, matches all the subkey name, otherwise matches a part of the name.</param>
        ''' <param name="ignoreCase">If set to <c>true</c>, performs a non-sensitive stringcase comparison.</param>
        ''' <param name="searchOption">The search mode.</param>
        ''' <returns>IEnumerable(Of RegInfo).</returns>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath or subKeyName</exception>
        Public Shared Function FindSubKey(ByVal rootKeyName As String,
                                          ByVal subKeyPath As String,
                                          ByVal subKeyName As String,
                                          ByVal matchFullSubKeyName As Boolean,
                                          ByVal ignoreCase As Boolean,
                                          ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            ElseIf String.IsNullOrEmpty(subKeyName) OrElse String.IsNullOrWhiteSpace(subKeyName) Then
                Throw New ArgumentNullException("subKeyName")

            End If

            Dim reg As RegistryKey = Nothing
            Try
                reg = GetRootKey(rootKeyName).OpenSubKey(GetSubKeyPath(subKeyPath), writable:=False)
                Using reg

                    If matchFullSubKeyName Then

                        If ignoreCase Then
                            Return (From registryPath As String In CollectSubKeys(reg, searchOption)
                                    Where GetSubKeyPath(registryPath).
                                          Remove(0, subKeyPath.Length).
                                          ToLower.
                                          Split("\"c).
                                          Contains(subKeyName.ToLower)
                                    Select New RegInfo With
                                           {
                                               .RootKeyName = GetRootKeyName(registryPath),
                                               .SubKeyPath = GetSubKeyPath(registryPath)
                                           }).ToArray

                        Else
                            Return (From registryPath As String In CollectSubKeys(reg, searchOption)
                                    Where GetSubKeyPath(registryPath).
                                          Remove(0, subKeyPath.Length).
                                          Split("\"c).
                                          Contains(subKeyName)
                                    Select New RegInfo With
                                           {
                                               .RootKeyName = GetRootKeyName(registryPath),
                                               .SubKeyPath = GetSubKeyPath(registryPath)
                                           }).ToArray

                        End If ' ignoreCase

                    Else ' not matchFullSubKeyName
                        If ignoreCase Then
                            Return (From registryPath As String In CollectSubKeys(reg, searchOption)
                                   Where GetSubKeyPath(registryPath).
                                         Remove(0, subKeyPath.Length).
                                         ToLower.
                                         Contains(subKeyName.ToLower)
                                    Select New RegInfo With
                                           {
                                               .RootKeyName = GetRootKeyName(registryPath),
                                               .SubKeyPath = GetSubKeyPath(registryPath)
                                           }).ToArray

                        Else
                            Return (From registryPath As String In CollectSubKeys(reg, searchOption)
                                   Where GetSubKeyPath(registryPath).
                                         Remove(0, subKeyPath.Length).
                                         Contains(subKeyName)
                                    Select New RegInfo With
                                           {
                                               .RootKeyName = GetRootKeyName(registryPath),
                                               .SubKeyPath = GetSubKeyPath(registryPath)
                                           }).ToArray

                        End If ' ignoreCase

                    End If ' matchFullSubKeyName

                End Using

            Catch ex As Exception
                Throw

            Finally
                If reg IsNot Nothing Then
                    reg.Close()
                End If

            End Try

        End Function

        ''' <summary>
        ''' Finds on a registry path all the subkey names that matches the specified criteria.
        ''' </summary>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <param name="subKeyName">The subkey name to find.</param>
        ''' <param name="matchFullSubKeyName">If set to <c>true</c>, matches all the subkey name, otherwise matches a part of the name.</param>
        ''' <param name="ignoreCase">If set to <c>true</c>, performs a non-sensitive stringcase comparison.</param>
        ''' <param name="searchOption">The search mode.</param>
        ''' <returns>IEnumerable(Of RegInfo).</returns>
        Public Shared Function FindSubKey(ByVal fullKeyPath As String,
                                          ByVal subKeyName As String,
                                          ByVal matchFullSubKeyName As Boolean,
                                          ByVal ignoreCase As Boolean,
                                          ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

            Return FindSubKey(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath), subKeyName, matchFullSubKeyName, ignoreCase, searchOption)

        End Function

        ''' <summary>
        ''' Finds on a registry path all the value names that matches the specified criteria.
        ''' </summary>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="valueName">The value name to find.</param>
        ''' <param name="matchFullValueName">If set to <c>true</c>, matches all the value name, otherwise matches a part of the name.</param>
        ''' <param name="ignoreCase">If set to <c>true</c>, performs a non-sensitive stringcase comparison.</param>
        ''' <param name="searchOption">The search mode.</param>
        ''' <returns>IEnumerable(Of RegInfo).</returns>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath or subKeyName</exception>
        Public Shared Function FindValue(ByVal rootKeyName As String,
                                         ByVal subKeyPath As String,
                                         ByVal valueName As String,
                                         ByVal matchFullValueName As Boolean,
                                         ByVal ignoreCase As Boolean,
                                         ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            ElseIf String.IsNullOrEmpty(valueName) OrElse String.IsNullOrWhiteSpace(valueName) Then
                Throw New ArgumentNullException("valueName")

            End If

            Dim reg As RegistryKey = Nothing
            Try
                reg = GetRootKey(rootKeyName).OpenSubKey(GetSubKeyPath(subKeyPath), writable:=False)
                Using reg

                    If matchFullValueName Then

                        If ignoreCase Then
                            Return (From regInfo As reginfo In CollectValues(reg, searchOption)
                                    Where regInfo.ValueName.ToLower.Equals(valueName.ToLower)).ToArray

                        Else
                            Return (From regInfo As reginfo In CollectValues(reg, searchOption)
                                    Where regInfo.ValueName.Equals(valueName)).ToArray

                        End If ' ignoreCase

                    Else ' not matchFullValueName
                        If ignoreCase Then
                            Return (From regInfo As reginfo In CollectValues(reg, searchOption)
                                    Where regInfo.ValueName.ToLower.Contains(valueName.ToLower)).ToArray

                        Else
                            Return (From regInfo As reginfo In CollectValues(reg, searchOption)
                                    Where regInfo.ValueName.Contains(valueName)).ToArray

                        End If ' ignoreCase

                    End If ' matchFullValueName

                End Using

            Catch ex As Exception
                Throw

            Finally
                If reg IsNot Nothing Then
                    reg.Close()
                End If

            End Try

        End Function

        ''' <summary>
        ''' Finds on a registry path all the value names that matches the specified criteria.
        ''' </summary>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <param name="valueName">The value name to find.</param>
        ''' <param name="matchFullValueName">If set to <c>true</c>, matches all the value name, otherwise matches a part of the name.</param>
        ''' <param name="ignoreCase">If set to <c>true</c>, performs a non-sensitive stringcase comparison.</param>
        ''' <param name="searchOption">The search mode.</param>
        ''' <returns>IEnumerable(Of RegInfo).</returns>
        Public Shared Function FindValue(ByVal fullKeyPath As String,
                                         ByVal valueName As String,
                                         ByVal matchFullValueName As Boolean,
                                         ByVal ignoreCase As Boolean,
                                         ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

            Return FindValue(GetRootKeyName(fullKeyPath), GetSubKeyPath(fullKeyPath), valueName, matchFullValueName, ignoreCase, searchOption)

        End Function

        ''' <summary>
        ''' Finds on a registry path all the values that contains data that matches the specified criteria.
        ''' </summary>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="valueData">The data to find.</param>
        ''' <param name="matchFullData">If set to <c>true</c>, matches all the data, otherwise matches a part of the data.</param>
        ''' <param name="ignoreCase">If set to <c>true</c>, performs a non-sensitive stringcase comparison (for String data).</param>
        ''' <param name="searchOption">The search mode.</param>
        ''' <returns>IEnumerable(Of RegInfo).</returns>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath or subKeyName</exception>
        Public Shared Function FindValueData(ByVal rootKeyName As String,
                                             ByVal subKeyPath As String,
                                             ByVal valueData As String,
                                             ByVal matchFullData As Boolean,
                                             ByVal ignoreCase As Boolean,
                                             ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            ElseIf String.IsNullOrEmpty(valueData) OrElse String.IsNullOrWhiteSpace(valueData) Then
                Throw New ArgumentNullException("valueData")

            End If

            Dim reg As RegistryKey = Nothing
            Try
                reg = GetRootKey(rootKeyName).OpenSubKey(GetSubKeyPath(subKeyPath), writable:=False)
                Using reg

                    If matchFullData Then

                        If ignoreCase Then
                            Return (From regInfo As reginfo In CollectValueDatas(reg, searchOption).ToArray
                                    Where Not String.IsNullOrEmpty(regInfo.ValueData.ToString) AndAlso
                                          regInfo.ValueData.ToString.ToLower.Equals(valueData.ToLower))

                        Else
                            Return (From regInfo As reginfo In CollectValueDatas(reg, searchOption).ToArray
                                    Where Not String.IsNullOrEmpty(regInfo.ValueData.ToString) AndAlso
                                          regInfo.ValueData.ToString.Equals(valueData))

                        End If ' ignoreCase

                    Else ' not matchFullData
                        If ignoreCase Then
                            Return (From regInfo As reginfo In CollectValueDatas(reg, searchOption).ToArray
                                    Where Not String.IsNullOrEmpty(regInfo.ValueData.ToString) AndAlso
                                          regInfo.ValueData.ToString.ToLower.Contains(valueData.ToLower))

                        Else
                            Return (From regInfo As reginfo In CollectValueDatas(reg, searchOption).ToArray
                                    Where Not String.IsNullOrEmpty(regInfo.ValueData.ToString) AndAlso
                                          regInfo.ValueData.ToString.Contains(valueData))

                        End If ' ignoreCase

                    End If ' matchFullData

                End Using

            Catch ex As Exception
                Throw

            Finally
                If reg IsNot Nothing Then
                    reg.Close()
                End If

            End Try

        End Function

#End Region

#Region " Authentication "

        ''' <summary>
        ''' Modifies the user permissions of a registry key.
        ''' </summary>
        ''' <param name="rootKeyName">The rootkey name.</param>
        ''' <param name="subKeyPath">The subkey path.</param>
        ''' <param name="userAccess">The user access.</param>
        ''' <returns><c>true</c> if operation succeeds, <c>false</c> otherwise.</returns>
        ''' <exception cref="System.ArgumentNullException">rootKeyName or subKeyPath</exception>
        Public Shared Function SetUserAccessKey(ByVal rootKeyName As String,
                                                ByVal subKeyPath As String,
                                                ByVal userAccess() As ReginiUserAccess) As Boolean

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")
            Else

                Dim tmpFilePath As String = Path.Combine(Path.GetTempPath(), "Regini.ini")

                Dim permissionString As String =
                    String.Format("[{0}]",
                                  String.Join(" "c, userAccess.Cast(Of Integer)))

                Try
                    Using textFile As New StreamWriter(path:=tmpFilePath, append:=False, encoding:=Encoding.Default)

                        textFile.WriteLine(String.Format("""{0}\{1}"" {2}",
                                                         GetRootKeyName(rootKeyName),
                                                         GetSubKeyPath(subKeyPath),
                                                         permissionString))

                    End Using ' TextFile

                    Using proc As New Process With {
                        .StartInfo = New ProcessStartInfo() With {
                               .FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "Regini.exe"),
                               .Arguments = ControlChars.Quote & tmpFilePath & ControlChars.Quote,
                               .CreateNoWindow = True,
                               .WindowStyle = ProcessWindowStyle.Hidden,
                               .UseShellExecute = False
                            }
                        }

                        With proc
                            .Start()
                            .WaitForExit()
                        End With

                        Return Not CBool(proc.ExitCode)

                    End Using

                Catch ex As Exception
                    Throw

                End Try

            End If

        End Function

        ''' <summary>
        ''' Modifies the user permissions of a registry key.
        ''' </summary>
        ''' <param name="fullKeyPath">The registry key full path.</param>
        ''' <param name="userAccess">The user access.</param>
        ''' <returns><c>true</c> if operation succeeds, <c>false</c> otherwise.</returns>
        Public Shared Function SetUserAccessKey(ByVal fullKeyPath As String,
                                                ByVal userAccess() As ReginiUserAccess) As Boolean

            Return SetUserAccessKey(rootKeyName:=GetRootKeyName(fullKeyPath),
                                    subKeyPath:=GetSubKeyPath(fullKeyPath),
                                    userAccess:=userAccess)

        End Function

#End Region

#End Region

#Region " Private Methods "

#Region " Collectors "

        ''' <summary>
        ''' Collects the subkeys on the specified registry path.
        ''' </summary>
        ''' <param name="sourceRegistryKey">The source registry key.</param>
        ''' <param name="searchOption">The search mode.</param>
        ''' <returns>IEnumerable(Of System.String).</returns>
        Private Shared Iterator Function CollectSubKeys(ByVal sourceRegistryKey As RegistryKey,
                                                        ByVal searchOption As SearchOption) As IEnumerable(Of String)

            For Each subKeyName As String In sourceRegistryKey.GetSubKeyNames

                Yield String.Format("{0}\{1}", sourceRegistryKey.Name, subKeyName)

                If searchOption = searchOption.AllDirectories Then

                    For Each registryPath As String In CollectSubKeys(sourceRegistryKey.OpenSubKey(subKeyName, writable:=False), searchOption.AllDirectories)
                        Yield registryPath
                    Next registryPath

                End If

            Next subKeyName

        End Function

        ''' <summary>
        ''' Collects the values on the specified registry path.
        ''' </summary>
        ''' <param name="sourceRegistryKey">The source registry key.</param>
        ''' <param name="searchOption">The search mode.</param>
        ''' <returns>IEnumerable(Of RegInfo).</returns>
        Private Shared Iterator Function CollectValues(ByVal sourceRegistryKey As RegistryKey,
                                                       ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

            Select Case searchOption

                Case searchOption.TopDirectoryOnly
                    For Each valueName As String In sourceRegistryKey.GetValueNames

                        Yield New reginfo With
                            {
                                .RootKeyName = GetRootKeyName(sourceRegistryKey.Name),
                                .SubKeyPath = GetSubKeyPath(sourceRegistryKey.Name),
                                .ValueName = valueName
                            }

                    Next valueName

                Case searchOption.AllDirectories
                    For Each registryPath As String In CollectSubKeys(sourceRegistryKey, searchOption)

                        Dim reg As RegistryKey = GetRootKey(registryPath).OpenSubKey(GetSubKeyPath(registryPath), writable:=False)

                        For Each valueName As String In reg.GetValueNames

                            Yield New reginfo With
                                {
                                    .RootKeyName = GetRootKeyName(reg.Name),
                                    .SubKeyPath = GetSubKeyPath(reg.Name),
                                    .ValueName = valueName
                                }

                        Next valueName

                        reg.Close()

                    Next registryPath

            End Select

        End Function

        ''' <summary>
        ''' Collects the value datas on the specified registry path.
        ''' </summary>
        ''' <param name="sourceRegistryKey">The source registry key.</param>
        ''' <param name="searchOption">The search mode.</param>
        ''' <returns>IEnumerable(Of RegInfo).</returns>
        Private Shared Iterator Function CollectValueDatas(ByVal sourceRegistryKey As RegistryKey,
                                                           ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

            Select Case searchOption

                Case searchOption.TopDirectoryOnly
                    For Each valueName As String In sourceRegistryKey.GetValueNames

                        Yield New reginfo With
                            {
                                .RootKeyName = GetRootKeyName(sourceRegistryKey.Name),
                                .SubKeyPath = GetSubKeyPath(sourceRegistryKey.Name),
                                .ValueName = valueName,
                                .ValueData = GetValueData(rootKeyName:=.RootKeyName,
                                                          subKeyPath:=.SubKeyPath,
                                                          valueName:=.ValueName,
                                                          registryValueOptions:=RegistryValueOptions.None)
                            }

                    Next valueName

                Case searchOption.AllDirectories
                    For Each registryPath As String In CollectSubKeys(sourceRegistryKey, searchOption)

                        Dim reg As RegistryKey = GetRootKey(registryPath).OpenSubKey(GetSubKeyPath(registryPath), writable:=False)

                        For Each valueName As String In reg.GetValueNames

                            Yield New reginfo With
                                {
                                    .RootKeyName = GetRootKeyName(reg.Name),
                                    .SubKeyPath = GetSubKeyPath(reg.Name),
                                    .ValueName = valueName,
                                    .ValueData = GetValueData(rootKeyName:=.RootKeyName,
                                                              subKeyPath:=.SubKeyPath,
                                                              valueName:=.ValueName,
                                                              registryValueOptions:=RegistryValueOptions.None)
                                }
                        Next valueName

                        reg.Close()

                    Next registryPath

            End Select

        End Function

#End Region

#End Region

    End Class

End Namespace

#End Region