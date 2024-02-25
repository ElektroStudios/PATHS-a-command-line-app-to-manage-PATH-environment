' ***********************************************************************
' Author   : Elektro
' Modified : 11-June-2015
' ***********************************************************************
' <copyright file="PathUtil.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Path Util "

Namespace Tools

    ''' <summary>
    ''' Contains related PATH and PATHEXT registry tools.
    ''' </summary>
    Public NotInheritable Class PathUtil

#Region " Properties "

        ''' <summary>
        ''' Gets the registry path of the Environment subkey for the current user.
        ''' </summary>
        ''' <value>The registry path of the Environment subkey for the current user.</value>
        Public Shared ReadOnly Property EnvironmentPathCurrentUser As String
            Get
                Return "HKEY_CURRENT_USER\Environment"
            End Get
        End Property

        ''' <summary>
        ''' Gets the registry path of the Environment subkey for local machine.
        ''' </summary>
        ''' <value>The registry path of the Environment subkey for local machine.</value>
        Public Shared ReadOnly Property EnvironmentPathAllUsers As String
            Get
                Return "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment"
            End Get
        End Property

        ''' <summary>
        ''' Gets the default data of the PATH registry value of a 32-Bit Windows.
        ''' </summary>
        ''' <value>The default data of the PATH registry value of a 32-Bit Windows.</value>
        Public Shared ReadOnly Property DefaultPathDataWin32 As String
            Get
                Return "C:\Windows;C:\Windows\System32;C:\Windows\System32\Wbem;C:\Windows\System32\WindowsPowerShell\v1.0"
            End Get
        End Property

        ''' <summary>
        ''' Gets the default data of the PATH registry value of a 64-Bit Windows.
        ''' </summary>
        ''' <value>The default data of the PATH registry value of a 64-Bit Windows.</value>
        Public Shared ReadOnly Property DefaultPathDataWin64 As String
            Get
                Return "C:\Windows;C:\Windows\System32;C:\Windows\System32\Wbem;C:\Windows\SysWOW64;C:\Windows\System32\WindowsPowerShell\v1.0"
            End Get
        End Property

        ''' <summary>
        ''' Gets the default data of the PATHEXt registry value.
        ''' </summary>
        ''' <value>The default data of the PATHEXt registry value.</value>
        Public Shared ReadOnly Property DefaultPathExtData As String
            Get
                Return ".COM;.EXE;.BAT;.CMD;.VBS;.VBE;.JS;.JSE"
            End Get
        End Property

        ''' <summary>
        ''' Gets the registry export string format.
        ''' </summary>
        ''' <value>The registry export string format.</value>
        Private Shared ReadOnly Property ExportStringFormat As String
            Get
                Return "Windows Registry Editor Version 5.00{0}{0}" &
                       "[HKEY_CURRENT_USER\Environment]{0}" &
                       """PATH""=""{1}""{0}" &
                       """PATHEXT""=""{2}""{0}{0}" &
                       "[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment]{0}" &
                       """PATH""=""{3}""{0}" &
                       """PATHEXT""=""{4}"""
            End Get
        End Property

#End Region

#Region " Enumerations "

        ''' <summary>
        ''' Specifies the registry user mode.
        ''' </summary>
        Public Enum UserMode

            ''' <summary>
            ''' The current user (HKCU).
            ''' </summary>
            Current = 0

            ''' <summary>
            ''' local machine (HKLM).
            ''' </summary>
            AllUsers = 1

        End Enum

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="PathUtil"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Gets the default data of the PATH value for the registry of the specified user (as String).
        ''' </summary>
        ''' <returns>The default data of the PATH value for the registry of the specified user.</returns>
        Public Shared Function GetDefaultPathDataString() As String

            If Not Environment.Is64BitOperatingSystem Then
                Return DefaultPathDataWin32
            Else
                Return DefaultPathDataWin64
            End If

        End Function

        ''' <summary>
        ''' Gets the default data of the PATH value for the registry of the specified user (as Enumerable).
        ''' </summary>
        ''' <returns>The default data of the PATH value for the registry of the specified user.</returns>
        Public Shared Function GetDefaultPathDataList() As IEnumerable(Of String)

            If Not Environment.Is64BitOperatingSystem Then
                Return DefaultPathDataWin32.Split({";"c}, StringSplitOptions.RemoveEmptyEntries)
            Else
                Return DefaultPathDataWin64.Split({";"c}, StringSplitOptions.RemoveEmptyEntries)
            End If

        End Function

        ''' <summary>
        ''' Gets the data of the PATH value on the registry of the specified user (as String).
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <returns>The data of the PATH value on the registry of the specified user.</returns>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Function GetPathDataString(ByVal userMode As UserMode) As String

            Select Case userMode

                Case PathUtil.UserMode.Current
                    Return RegEdit.GetValueData(Of String)(fullKeyPath:=EnvironmentPathCurrentUser, valueName:="PATH")

                Case PathUtil.UserMode.AllUsers
                    Return RegEdit.GetValueData(Of String)(fullKeyPath:=EnvironmentPathAllUsers, valueName:="PATH")

                Case Else
                    Throw New ArgumentException(message:="Unexpected enumeration value.", paramName:="userMode")

            End Select

        End Function

        ''' <summary>
        ''' Gets the data of the PATH value on the registry of the specified user (as Enumerable).
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <returns>The data of the PATH value on the registry of the specified user.</returns>
        Public Shared Function GetPathDataList(ByVal userMode As UserMode) As IEnumerable(Of String)

            Dim path As String = GetPathDataString(userMode)

            If Not String.IsNullOrEmpty(path) Then
                Return path.Split({";"c}, StringSplitOptions.RemoveEmptyEntries)

            Else
                Return Enumerable.Empty(Of String)

            End If

        End Function

        ''' <summary>
        ''' Gets the data of the PATHEXT value on the registry of the specified user (as String).
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <returns>The data of the PATHEXT value on the registry of the specified user.</returns>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Function GetPathExtDataString(ByVal userMode As UserMode) As String

            Select Case userMode

                Case PathUtil.UserMode.Current
                    Return RegEdit.GetValueData(Of String)(fullKeyPath:=EnvironmentPathCurrentUser, valueName:="PATHEXT")

                Case PathUtil.UserMode.AllUsers
                    Return RegEdit.GetValueData(Of String)(fullKeyPath:=EnvironmentPathAllUsers, valueName:="PATHEXT")

                Case Else
                    Throw New ArgumentException(message:="Unexpected enumeration value.", paramName:="userMode")

            End Select

        End Function

        ''' <summary>
        ''' Gets data of the data of the PATHEXT value on the registry of the specified user (as Enumerable).
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <returns>The data of the PATHEXT value on the registry of the specified user.</returns>
        Public Shared Function GetPathExtDataList(ByVal userMode As UserMode) As IEnumerable(Of String)

            Dim pathExt As String = GetPathExtDataString(userMode)

            If Not String.IsNullOrEmpty(pathExt) Then
                Return pathExt.Split({";"c}, StringSplitOptions.RemoveEmptyEntries)

            Else
                Return Enumerable.Empty(Of String)

            End If

        End Function

        ''' <summary>
        ''' Determines whether the PATH value exists on the registry of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <returns><c>true</c> if PATH value exists, <c>false</c> otherwise.</returns>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Function PathExists(ByVal userMode As UserMode) As Boolean

            Select Case userMode

                Case PathUtil.UserMode.Current
                    Return RegEdit.ExistValue(fullKeyPath:=EnvironmentPathCurrentUser, valueName:="PATH")

                Case PathUtil.UserMode.AllUsers
                    Return RegEdit.ExistValue(fullKeyPath:=EnvironmentPathAllUsers, valueName:="PATH")

                Case Else
                    Throw New ArgumentException(message:="Unexpected enumeration value.", paramName:="userMode")

            End Select

        End Function

        ''' <summary>
        ''' Determines whether the PATHEXT value exists on the registry of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <returns><c>true</c> if PATHEXT value exists, <c>false</c> otherwise.</returns>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Function PathExtExists(ByVal userMode As UserMode) As Boolean

            Select Case userMode

                Case PathUtil.UserMode.Current
                    Return RegEdit.ExistValue(fullKeyPath:=EnvironmentPathCurrentUser, valueName:="PATHEXT")

                Case PathUtil.UserMode.AllUsers
                    Return RegEdit.ExistValue(fullKeyPath:=EnvironmentPathAllUsers, valueName:="PATHEXT")

                Case Else
                    Throw New ArgumentException(message:="Unexpected enumeration value.", paramName:="userMode")

            End Select

        End Function

        ''' <summary>
        ''' Exports the PATH and PATHEXT values to a target registry file.
        ''' </summary>
        ''' <param name="filepath">The filepath.</param>
        ''' <exception cref="Exception"></exception>
        Public Shared Sub Export(ByVal filepath As String)

            Try
                IO.File.WriteAllText(filepath,
                                     String.Format(ExportStringFormat,
                                                   Environment.NewLine,
                                                   GetPathDataString(UserMode.Current),
                                                   GetPathExtDataString(UserMode.Current),
                                                   GetPathDataString(UserMode.AllUsers),
                                                   GetPathExtDataString(UserMode.AllUsers)),
                                     encoding:=System.Text.Encoding.Unicode)

            Catch ex As Exception
                Throw

            End Try

        End Sub

        ''' <summary>
        ''' Creates a PATH value on the registry of the specified user and optionally fills the value with the specified data.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Sub CreatePath(ByVal userMode As UserMode,
                                     Optional data As String = "")

            Try
                Select Case userMode

                    Case PathUtil.UserMode.Current
                        RegEdit.CreateValue(Of String)(fullKeyPath:=EnvironmentPathCurrentUser, valueName:="PATH", valueData:=data)

                    Case PathUtil.UserMode.AllUsers
                        RegEdit.CreateValue(Of String)(fullKeyPath:=EnvironmentPathAllUsers, valueName:="PATH", valueData:=data)

                    Case Else
                        Throw New ArgumentException(message:="Unexpected enumeration value.", paramName:="userMode")

                End Select

            Catch ex As Exception
                Throw

            End Try

        End Sub

        ''' <summary>
        ''' Creates a PATHEXT value on the registry of the specified user and optionally fills the value with the specified data..
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Sub CreatePathExt(ByVal userMode As UserMode,
                                        Optional data As String = "")

            Try
                Select Case userMode

                    Case PathUtil.UserMode.Current
                        RegEdit.CreateValue(Of String)(fullKeyPath:=EnvironmentPathCurrentUser, valueName:="PATHEXT", valueData:=data)

                    Case PathUtil.UserMode.AllUsers
                        RegEdit.CreateValue(Of String)(fullKeyPath:=EnvironmentPathAllUsers, valueName:="PATHEXT", valueData:=data)

                    Case Else
                        Throw New ArgumentException(message:="Unexpected enumeration value.", paramName:="userMode")

                End Select

            Catch ex As Exception
                Throw

            End Try

        End Sub

        ''' <summary>
        ''' Adds a directory into the PATH registry value of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="directory">The directory path.</param>
        ''' <exception cref="ArgumentException">Directory contains invalid character(s).;directory</exception>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Sub AddDirectory(ByVal userMode As UserMode,
                                       ByVal directory As String)

            If directory.Any(Function(c As Char) IO.Path.GetInvalidPathChars.Contains(c)) Then
                Throw New ArgumentException(message:="Directory contains invalid character(s).", paramName:="directory")

            Else

                Select Case userMode

                    Case PathUtil.UserMode.Current
                        RegEdit.CreateValue(Of String)(fullKeyPath:=EnvironmentPathCurrentUser, valueName:="PATH",
                                                       valueData:=String.Join(";"c, GetPathDataList(userMode).Concat({directory}).Distinct).Trim(";"c))

                    Case PathUtil.UserMode.AllUsers
                        RegEdit.CreateValue(Of String)(fullKeyPath:=EnvironmentPathAllUsers, valueName:="PATH",
                                                       valueData:=String.Join(";"c, GetPathDataList(userMode).Concat({directory}).Distinct).Trim(";"c))

                    Case Else
                        Throw New ArgumentException(message:="Unexpected enumeration value.", paramName:="userMode")

                End Select

            End If

        End Sub

        ''' <summary>
        ''' Adds a file extension into the PATHEXT registry value of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="extension">The file extension.</param>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Sub AddExtension(ByVal userMode As UserMode,
                                       ByVal extension As String)

            If Not extension.StartsWith("."c) Then ' Fix extension.
                extension.Insert(0, "."c)
            End If

            Select Case userMode

                Case PathUtil.UserMode.Current
                    RegEdit.CreateValue(Of String)(fullKeyPath:=EnvironmentPathCurrentUser, valueName:="PATHEXT",
                                                   valueData:=String.Join(";"c, GetPathExtDataList(userMode).Concat({extension})).Trim(";"c))

                Case PathUtil.UserMode.AllUsers
                    RegEdit.CreateValue(Of String)(fullKeyPath:=EnvironmentPathAllUsers, valueName:="PATHEXT",
                                                   valueData:=String.Join(";"c, GetPathExtDataList(userMode).Concat({extension})).Trim(";"c))

                Case Else
                    Throw New ArgumentException(message:="Unexpected enumeration value.", paramName:="userMode")

            End Select

        End Sub

        ''' <summary>
        ''' Deletes a directory from the PATH registry value of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="directory">The directory path.</param>
        Public Shared Sub DeleteDirectory(ByVal userMode As UserMode,
                                          ByVal directory As String)

            Dim dirs As IEnumerable(Of String) =
                From dir As String In GetPathDataList(userMode)
                Where Not dir.ToLower.Equals(directory, StringComparison.OrdinalIgnoreCase)

            CreatePath(userMode, data:=String.Join(";"c, dirs))

        End Sub

        ''' <summary>
        ''' Deletes a directory from the PATH registry value of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="index">The directory index, 0 = First.</param>
        ''' <exception cref="IndexOutOfRangeException">Directory index is out of range.</exception>
        Public Shared Sub DeleteDirectory(ByVal userMode As UserMode,
                                          ByVal index As Integer)

            Dim dirs As List(Of String) = GetPathDataList(userMode).ToList

            If (dirs.Count > index) Then
                dirs.RemoveAt(index)
            Else
                Throw New IndexOutOfRangeException(Message:="Directory index is out of range.")
            End If

            CreatePath(userMode, data:=String.Join(";"c, dirs))

        End Sub

        ''' <summary>
        ''' Deletes a file extension from the PATHEXT registry value of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="extension">The file extension.</param>
        Public Shared Sub DeleteExtension(ByVal userMode As UserMode,
                                          ByVal extension As String)

            If Not extension.StartsWith("."c) Then ' Fix extension.
                extension.Insert(0, "."c)
            End If

            Dim exts As IEnumerable(Of String) =
                From ext As String In GetPathExtDataList(userMode)
                Where Not ext.ToLower.Equals(extension, StringComparison.OrdinalIgnoreCase)

            CreatePath(userMode, data:=String.Join(";"c, exts))

        End Sub

        ''' <summary>
        ''' Deletes a file extension from the PATHEXT registry value of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="index">The file extension index, 0 = First.</param>
        ''' <exception cref="IndexOutOfRangeException">File extension index is out of range.</exception>
        Public Shared Sub DeleteExtension(ByVal userMode As UserMode,
                                          ByVal index As Integer)

            Dim exts As List(Of String) = GetPathExtDataList(userMode).ToList

            If (exts.Count > index) Then
                exts.RemoveAt(index)
            Else
                Throw New IndexOutOfRangeException(Message:="File extension index is out of range.")
            End If

            CreatePathExt(userMode, data:=String.Join(";"c, exts))

        End Sub

        ''' <summary>
        ''' Determines whether the PATH registry value of the specified user contains a directory.
        ''' </summary>
        ''' <param name="usermode">The usermode.</param>
        ''' <param name="directory">The directory path.</param>
        ''' <returns><c>true</c> if contains the specified directory; <c>false</c> otherwise.</returns>
        Public Shared Function ContainsDirectory(ByVal usermode As UserMode,
                                                 ByVal directory As String) As Boolean

            Dim dirs As IEnumerable(Of String) = GetPathDataList(usermode)

            If dirs IsNot Nothing Then
                Return dirs.Any(Function(dir As String) dir.Equals(directory, StringComparison.OrdinalIgnoreCase))
            Else
                Return False
            End If

        End Function

        ''' <summary>
        ''' Determines whether the PATHEXT registry value of the specified user contains a directory.
        ''' </summary>
        ''' <param name="usermode">The usermode.</param>
        ''' <param name="extension">The file extension.</param>
        ''' <returns><c>true</c> if contains the specified file extension; <c>false</c> otherwise.</returns>
        Public Shared Function ContainsExtension(ByVal usermode As UserMode,
                                                 ByVal extension As String) As Boolean

            If Not extension.StartsWith("."c) Then ' Fix extension.
                extension.Insert(0, "."c)
            End If

            Dim exts As IEnumerable(Of String) = GetPathExtDataList(usermode)

            If exts IsNot Nothing Then
                Return exts.Any(Function(ext As String) ext.Equals(extension, StringComparison.OrdinalIgnoreCase))
            Else
                Return False
            End If

        End Function

#End Region

    End Class

End Namespace

#End Region