' ***********************************************************************
' Author   : Elektro
' Modified : 08-November-2026
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

#Region " Imports "

Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Imports Microsoft.Win32

#End Region

#Region " Path Util "

Namespace Tools

    ''' <summary>
    ''' Contains related PATH and PATHEXT registry tools.
    ''' </summary>
    Public NotInheritable Class PathUtil

#Region " Constants "

        ''' <summary>
        ''' The registry path of the Environment subkey for the current user.
        ''' </summary>
        Private Const DefaultEnvironmentKeyCurrentUser As String =
            "HKEY_CURRENT_USER\Environment"

        ''' <summary>
        ''' The registry path of the Environment subkey for local machine.
        ''' </summary>
        Private Const DefaultEnvironmentKeyAllUsers As String =
            "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment"

        ''' <summary>
        ''' The name of the PATH registry value.
        ''' </summary>
        Public Const PathValueName As String = "PATH"

        ''' <summary>
        ''' The name of the PATHEXT registry value.
        ''' </summary>
        Public Const PathExtValueName As String = "PATHEXT"

        ''' <summary>
        ''' The character used to separate the entries of PATH and PATHEXT values.
        ''' </summary>
        Public Const EntrySeparator As Char = ";"c

        ''' <summary>
        ''' The maximum length of a line written in a registry script file
        ''' (the same wrapping limit that is used by RegEdit.exe).
        ''' </summary>
        Private Const RegScriptMaxLineLength As Integer = 80

#End Region

#Region " Fields "

        ''' <summary>
        ''' The registry path of the Environment subkey that is used for <see cref="UserMode.Current"/>.
        ''' <para></para>
        ''' This is only redirected by unit tests, to operate on a sandbox registry key.
        ''' </summary>
        Private Shared environmentKeyCurrentUser As String = DefaultEnvironmentKeyCurrentUser

        ''' <summary>
        ''' The registry path of the Environment subkey that is used for <see cref="UserMode.AllUsers"/>.
        ''' <para></para>
        ''' This is only redirected by unit tests, to operate on a sandbox registry key.
        ''' </summary>
        Private Shared environmentKeyAllUsers As String = DefaultEnvironmentKeyAllUsers

        ''' <summary>
        ''' The characters that are not allowed in a directory path of a PATH entry (Windows rules, plus the entry separator).
        ''' <para></para>
        ''' Defined explicitly instead of using <see cref="Path.GetInvalidPathChars()"/>, whose result depends on the runtime.
        ''' </summary>
        Private Shared ReadOnly InvalidDirectoryChars As Char() =
            Enumerable.Range(0, 32).Select(Function(i As Integer) Convert.ToChar(i)).
                       Concat({""""c, "<"c, ">"c, "|"c, "*"c, "?"c, EntrySeparator}).
                       ToArray()

        ''' <summary>
        ''' The characters that are not allowed in a file extension of a PATHEXT entry (Windows rules, plus the entry separator).
        ''' <para></para>
        ''' Defined explicitly instead of using <see cref="Path.GetInvalidFileNameChars()"/>, whose result depends on the runtime.
        ''' </summary>
        Private Shared ReadOnly InvalidExtensionChars As Char() =
            InvalidDirectoryChars.Concat({"\"c, "/"c, ":"c}).ToArray()

#End Region

#Region " Properties "

        ''' <summary>
        ''' Gets the registry path of the Environment subkey for the current user.
        ''' </summary>
        ''' <value>The registry path of the Environment subkey for the current user.</value>
        Public Shared ReadOnly Property EnvironmentPathCurrentUser As String
            Get
                Return environmentKeyCurrentUser
            End Get
        End Property

        ''' <summary>
        ''' Gets the registry path of the Environment subkey for local machine.
        ''' </summary>
        ''' <value>The registry path of the Environment subkey for local machine.</value>
        Public Shared ReadOnly Property EnvironmentPathAllUsers As String
            Get
                Return environmentKeyAllUsers
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
        ''' Gets the default data of the PATHEXT registry value.
        ''' </summary>
        ''' <value>The default data of the PATHEXT registry value.</value>
        Public Shared ReadOnly Property DefaultPathExtData As String
            Get
                Return ".COM;.EXE;.BAT;.CMD;.VBS;.VBE;.JS;.JSE"
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

#Region " Types "

        ''' <summary>
        ''' Represents the result of a clean operation over a list of PATH or PATHEXT entries.
        ''' </summary>
        Public NotInheritable Class CleanResult

            ''' <summary>
            ''' Gets the resulting entries, in their original order.
            ''' </summary>
            ''' <value>The resulting entries, in their original order.</value>
            Public ReadOnly Property Entries As ReadOnlyCollection(Of String)

            ''' <summary>
            ''' Gets the duplicated entries that were removed.
            ''' </summary>
            ''' <value>The duplicated entries that were removed.</value>
            Public ReadOnly Property RemovedDuplicates As ReadOnlyCollection(Of String)

            ''' <summary>
            ''' Gets the missing (not found) directory entries that were removed.
            ''' </summary>
            ''' <value>The missing (not found) directory entries that were removed.</value>
            Public ReadOnly Property RemovedMissing As ReadOnlyCollection(Of String)

            ''' <summary>
            ''' Initializes a new instance of the <see cref="CleanResult"/> class.
            ''' </summary>
            ''' <param name="entries">The resulting entries.</param>
            ''' <param name="removedDuplicates">The duplicated entries that were removed.</param>
            ''' <param name="removedMissing">The missing directory entries that were removed.</param>
            Friend Sub New(entries As IList(Of String),
                           removedDuplicates As IList(Of String),
                           removedMissing As IList(Of String))

                Me.Entries = New ReadOnlyCollection(Of String)(entries)
                Me.RemovedDuplicates = New ReadOnlyCollection(Of String)(removedDuplicates)
                Me.RemovedMissing = New ReadOnlyCollection(Of String)(removedMissing)

            End Sub

        End Class

#End Region

#Region " P/Invoking "

        ''' <summary>
        ''' Platform Invocation methods (P/Invoke), access unmanaged code.
        ''' </summary>
        Private NotInheritable Class NativeMethods

            ''' <summary>
            ''' Handle that represents all top-level windows of the system.
            ''' </summary>
            Friend Shared ReadOnly HwndBroadcast As New IntPtr(&HFFFF)

            ''' <summary>
            ''' Message sent to all top-level windows when a system-wide setting changes.
            ''' </summary>
            Friend Const WmSettingChange As Integer = &H1A

            ''' <summary>
            ''' The function returns without waiting for the time-out period to elapse
            ''' if the receiving thread appears to not respond or "hangs".
            ''' </summary>
            Friend Const SmtoAbortIfHung As Integer = &H2

            ''' <summary>
            ''' Prevents a default instance of the <see cref="NativeMethods"/> class from being created.
            ''' </summary>
            Private Sub New()
            End Sub

            ''' <summary>
            ''' Sends the specified message to one or more windows.
            ''' </summary>
            ''' <param name="hWnd">A handle to the window whose window procedure will receive the message.</param>
            ''' <param name="msg">The message to be sent.</param>
            ''' <param name="wParam">Any additional message-specific information.</param>
            ''' <param name="lParam">Any additional message-specific information.</param>
            ''' <param name="fuFlags">The behavior of this function.</param>
            ''' <param name="uTimeout">The duration of the time-out period, in milliseconds.</param>
            ''' <param name="lpdwResult">The result of the message processing.</param>
            ''' <returns>A nonzero value if the function succeeds; otherwise zero.</returns>
            <DllImport("user32.dll", EntryPoint:="SendMessageTimeoutW", CharSet:=CharSet.Unicode, SetLastError:=True)>
            Friend Shared Function SendMessageTimeout(hWnd As IntPtr,
                                                      msg As Integer,
                                                      wParam As IntPtr,
                                                      lParam As String,
                                                      fuFlags As Integer,
                                                      uTimeout As Integer,
                                                      <Out> ByRef lpdwResult As IntPtr) As IntPtr
            End Function

        End Class

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="PathUtil"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods (Registry) "

        ''' <summary>
        ''' Gets the default data of the PATH value (as String).
        ''' </summary>
        ''' <returns>The default data of the PATH value.</returns>
        Public Shared Function GetDefaultPathDataString() As String

            Return If(Environment.Is64BitOperatingSystem, DefaultPathDataWin64, DefaultPathDataWin32)

        End Function

        ''' <summary>
        ''' Gets the default data of the PATH value (as Enumerable).
        ''' </summary>
        ''' <returns>The default data of the PATH value.</returns>
        Public Shared Function GetDefaultPathDataList() As IEnumerable(Of String)

            Return SplitEntries(GetDefaultPathDataString())

        End Function

        ''' <summary>
        ''' Gets the raw (not expanded) data of the PATH value on the registry of the specified user (as String).
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <returns>
        ''' The raw data of the PATH value on the registry of the specified user,
        ''' or <see cref="String.Empty"/> if the value does not exist.
        ''' </returns>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Function GetPathDataString(userMode As UserMode) As String

            Return If(ReadValue(GetEnvironmentKeyPath(userMode), PathValueName), String.Empty)

        End Function

        ''' <summary>
        ''' Gets the raw (not expanded) data of the PATH value on the registry of the specified user (as Enumerable).
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <returns>The data of the PATH value on the registry of the specified user.</returns>
        Public Shared Function GetPathDataList(userMode As UserMode) As IEnumerable(Of String)

            Return SplitEntries(GetPathDataString(userMode))

        End Function

        ''' <summary>
        ''' Gets the raw (not expanded) data of the PATHEXT value on the registry of the specified user (as String).
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <returns>
        ''' The raw data of the PATHEXT value on the registry of the specified user,
        ''' or <see cref="String.Empty"/> if the value does not exist.
        ''' </returns>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Function GetPathExtDataString(userMode As UserMode) As String

            Return If(ReadValue(GetEnvironmentKeyPath(userMode), PathExtValueName), String.Empty)

        End Function

        ''' <summary>
        ''' Gets the raw (not expanded) data of the PATHEXT value on the registry of the specified user (as Enumerable).
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <returns>The data of the PATHEXT value on the registry of the specified user.</returns>
        Public Shared Function GetPathExtDataList(userMode As UserMode) As IEnumerable(Of String)

            Return SplitEntries(GetPathExtDataString(userMode))

        End Function

        ''' <summary>
        ''' Determines whether the PATH value exists on the registry of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <returns><see langword="True"/> if PATH value exists, <see langword="False"/> otherwise.</returns>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Function PathExists(userMode As UserMode) As Boolean

            Return ReadValue(GetEnvironmentKeyPath(userMode), PathValueName) IsNot Nothing

        End Function

        ''' <summary>
        ''' Determines whether the PATHEXT value exists on the registry of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <returns><see langword="True"/> if PATHEXT value exists, <see langword="False"/> otherwise.</returns>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Function PathExtExists(userMode As UserMode) As Boolean

            Return ReadValue(GetEnvironmentKeyPath(userMode), PathExtValueName) IsNot Nothing

        End Function

        ''' <summary>
        ''' Exports the PATH and PATHEXT values to a target registry script file.
        ''' </summary>
        ''' <param name="filepath">The target registry script file path.</param>
        ''' <exception cref="ArgumentNullException">filepath</exception>
        Public Shared Sub Export(filepath As String)

            If String.IsNullOrWhiteSpace(filepath) Then
                Throw New ArgumentNullException(NameOf(filepath))
            End If

            Dim text As String =
                BuildExportText(ReadValue(GetEnvironmentKeyPath(UserMode.Current), PathValueName),
                                ReadValue(GetEnvironmentKeyPath(UserMode.Current), PathExtValueName),
                                ReadValue(GetEnvironmentKeyPath(UserMode.AllUsers), PathValueName),
                                ReadValue(GetEnvironmentKeyPath(UserMode.AllUsers), PathExtValueName))

            File.WriteAllText(filepath, text, Encoding.Unicode)

        End Sub

        ''' <summary>
        ''' Creates (or replaces) the PATH value on the registry of the specified user,
        ''' and optionally fills the value with the specified data.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="data">The value data.</param>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Sub CreatePath(userMode As UserMode,
                                     Optional data As String = "")

            WriteValue(GetEnvironmentKeyPath(userMode), PathValueName, data)

        End Sub

        ''' <summary>
        ''' Creates (or replaces) the PATHEXT value on the registry of the specified user,
        ''' and optionally fills the value with the specified data.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="data">The value data.</param>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Sub CreatePathExt(userMode As UserMode,
                                        Optional data As String = "")

            WriteValue(GetEnvironmentKeyPath(userMode), PathExtValueName, data)

        End Sub

        ''' <summary>
        ''' Adds a directory into the PATH registry value of the specified user.
        ''' <para></para>
        ''' If the directory already exists in the PATH, the PATH is not modified.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="directory">The directory path.</param>
        ''' <exception cref="ArgumentException">Directory contains invalid character(s).;directory</exception>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Sub AddDirectory(userMode As UserMode,
                                       directory As String)

            Dim entries As List(Of String) = AddDirectoryEntry(GetPathDataList(userMode), directory)
            CreatePath(userMode, JoinEntries(entries))

        End Sub

        ''' <summary>
        ''' Adds a file extension into the PATHEXT registry value of the specified user.
        ''' <para></para>
        ''' If the file extension already exists in the PATHEXT, the PATHEXT is not modified.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="extension">The file extension (the leading dot is optional).</param>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Public Shared Sub AddExtension(userMode As UserMode,
                                       extension As String)

            Dim entries As List(Of String) = AddExtensionEntry(GetPathExtDataList(userMode), extension)
            CreatePathExt(userMode, JoinEntries(entries))

        End Sub

        ''' <summary>
        ''' Deletes a directory from the PATH registry value of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="directory">The directory path.</param>
        Public Shared Sub DeleteDirectory(userMode As UserMode,
                                          directory As String)

            Dim entries As List(Of String) = RemoveDirectoryEntry(GetPathDataList(userMode), directory)
            CreatePath(userMode, JoinEntries(entries))

        End Sub

        ''' <summary>
        ''' Deletes a directory from the PATH registry value of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="index">The zero-based directory index, 0 = First.</param>
        ''' <exception cref="ArgumentOutOfRangeException">Directory index is out of range.</exception>
        Public Shared Sub DeleteDirectory(userMode As UserMode,
                                          index As Integer)

            Dim entries As List(Of String) = RemoveEntryAt(GetPathDataList(userMode), index)
            CreatePath(userMode, JoinEntries(entries))

        End Sub

        ''' <summary>
        ''' Deletes a file extension from the PATHEXT registry value of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="extension">The file extension (the leading dot is optional).</param>
        Public Shared Sub DeleteExtension(userMode As UserMode,
                                          extension As String)

            Dim entries As List(Of String) = RemoveExtensionEntry(GetPathExtDataList(userMode), extension)
            CreatePathExt(userMode, JoinEntries(entries))

        End Sub

        ''' <summary>
        ''' Deletes a file extension from the PATHEXT registry value of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="index">The zero-based file extension index, 0 = First.</param>
        ''' <exception cref="ArgumentOutOfRangeException">File extension index is out of range.</exception>
        Public Shared Sub DeleteExtension(userMode As UserMode,
                                          index As Integer)

            Dim entries As List(Of String) = RemoveEntryAt(GetPathExtDataList(userMode), index)
            CreatePathExt(userMode, JoinEntries(entries))

        End Sub

        ''' <summary>
        ''' Determines whether the PATH registry value of the specified user contains a directory.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="directory">The directory path.</param>
        ''' <returns><see langword="True"/> if contains the specified directory; <see langword="False"/> otherwise.</returns>
        Public Shared Function ContainsDirectory(userMode As UserMode,
                                                 directory As String) As Boolean

            Return ContainsDirectoryEntry(GetPathDataList(userMode), directory)

        End Function

        ''' <summary>
        ''' Determines whether the PATHEXT registry value of the specified user contains a file extension.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="extension">The file extension (the leading dot is optional).</param>
        ''' <returns><see langword="True"/> if contains the specified file extension; <see langword="False"/> otherwise.</returns>
        Public Shared Function ContainsExtension(userMode As UserMode,
                                                 extension As String) As Boolean

            Return ContainsExtensionEntry(GetPathExtDataList(userMode), extension)

        End Function

        ''' <summary>
        ''' Notifies all the top-level windows that the environment variables have changed,
        ''' so new processes started from them (e.g. from Explorer) see the updated PATH and PATHEXT.
        ''' </summary>
        Public Shared Sub NotifyEnvironmentChanged()

            Dim result As IntPtr = IntPtr.Zero
            NativeMethods.SendMessageTimeout(NativeMethods.HwndBroadcast, NativeMethods.WmSettingChange,
                                             IntPtr.Zero, "Environment",
                                             NativeMethods.SmtoAbortIfHung, 5000, result)

        End Sub

#End Region

#Region " Public Methods (Entry Manipulation) "

        ''' <summary>
        ''' Splits the data of a PATH or PATHEXT value into its entries.
        ''' <para></para>
        ''' Entries are trimmed, and empty or white-space entries are discarded.
        ''' </summary>
        ''' <param name="data">The value data. Can be <see langword="Nothing"/>.</param>
        ''' <returns>The entries of the value data.</returns>
        Public Shared Function SplitEntries(data As String) As IEnumerable(Of String)

            If String.IsNullOrEmpty(data) Then
                Return Enumerable.Empty(Of String)()
            End If

            Return (From entry As String In data.Split({EntrySeparator}, StringSplitOptions.RemoveEmptyEntries)
                    Let trimmed As String = entry.Trim()
                    Where trimmed.Length <> 0
                    Select trimmed).ToArray()

        End Function

        ''' <summary>
        ''' Joins the specified entries into the data of a PATH or PATHEXT value.
        ''' </summary>
        ''' <param name="entries">The entries.</param>
        ''' <returns>The value data.</returns>
        ''' <exception cref="ArgumentNullException">entries</exception>
        Public Shared Function JoinEntries(entries As IEnumerable(Of String)) As String

            If entries Is Nothing Then
                Throw New ArgumentNullException(NameOf(entries))
            End If

            Return String.Join(EntrySeparator, entries)

        End Function

        ''' <summary>
        ''' Validates a directory path to be added into a PATH value.
        ''' </summary>
        ''' <param name="directory">The directory path.</param>
        ''' <exception cref="ArgumentNullException">directory</exception>
        ''' <exception cref="ArgumentException">Directory is empty.;directory</exception>
        ''' <exception cref="ArgumentException">Directory contains invalid character(s).;directory</exception>
        Public Shared Sub ValidateDirectory(directory As String)

            If directory Is Nothing Then
                Throw New ArgumentNullException(NameOf(directory))
            End If

            If String.IsNullOrWhiteSpace(directory) Then
                Throw New ArgumentException("Directory is empty.", NameOf(directory))
            End If

            If directory.IndexOfAny(InvalidDirectoryChars) <> -1 Then
                Throw New ArgumentException("Directory contains invalid character(s).", NameOf(directory))
            End If

        End Sub

        ''' <summary>
        ''' Normalizes a file extension to be stored in a PATHEXT value
        ''' (trims it and adds the leading dot if missing).
        ''' </summary>
        ''' <param name="extension">The file extension.</param>
        ''' <returns>The normalized file extension.</returns>
        ''' <exception cref="ArgumentNullException">extension</exception>
        ''' <exception cref="ArgumentException">File extension is empty.;extension</exception>
        ''' <exception cref="ArgumentException">File extension contains invalid character(s).;extension</exception>
        Public Shared Function NormalizeExtension(extension As String) As String

            If extension Is Nothing Then
                Throw New ArgumentNullException(NameOf(extension))
            End If

            Dim trimmed As String = extension.Trim()
            If trimmed.TrimStart("."c).Length = 0 Then
                Throw New ArgumentException("File extension is empty.", NameOf(extension))
            End If

            If trimmed.IndexOfAny(InvalidExtensionChars) <> -1 OrElse
               trimmed.Any(Function(c As Char) Char.IsWhiteSpace(c)) Then
                Throw New ArgumentException("File extension contains invalid character(s).", NameOf(extension))
            End If

            Return If(trimmed.StartsWith(".", StringComparison.Ordinal), trimmed, $".{trimmed}")

        End Function

        ''' <summary>
        ''' Normalizes a directory path for comparison purposes:
        ''' expands environment variables, removes surrounding quotes and removes trailing directory separators.
        ''' </summary>
        ''' <param name="directory">The directory path.</param>
        ''' <returns>The normalized directory path.</returns>
        ''' <exception cref="ArgumentNullException">directory</exception>
        Public Shared Function NormalizeDirectoryForComparison(directory As String) As String

            If directory Is Nothing Then
                Throw New ArgumentNullException(NameOf(directory))
            End If

            Dim normalized As String = Environment.ExpandEnvironmentVariables(directory.Trim()).Trim(""""c).Trim()
            normalized = normalized.TrimEnd("\"c, "/"c)

            ' Keep the separator of a drive root ("C:" and "C:\" are not the same path).
            If normalized.EndsWith(":", StringComparison.Ordinal) Then
                normalized = $"{normalized}\"
            End If

            Return normalized

        End Function

        ''' <summary>
        ''' Determines whether two PATH entries refer to the same directory
        ''' (case-insensitive, with environment variables expanded and ignoring trailing separators).
        ''' </summary>
        ''' <param name="directory1">The first directory path.</param>
        ''' <param name="directory2">The second directory path.</param>
        ''' <returns><see langword="True"/> if both entries refer to the same directory; <see langword="False"/> otherwise.</returns>
        Public Shared Function DirectoriesEqual(directory1 As String, directory2 As String) As Boolean

            Return String.Equals(NormalizeDirectoryForComparison(directory1),
                                 NormalizeDirectoryForComparison(directory2),
                                 StringComparison.OrdinalIgnoreCase)

        End Function

        ''' <summary>
        ''' Determines whether two PATHEXT entries refer to the same file extension
        ''' (case-insensitive and ignoring the leading dot).
        ''' </summary>
        ''' <param name="extension1">The first file extension.</param>
        ''' <param name="extension2">The second file extension.</param>
        ''' <returns><see langword="True"/> if both entries refer to the same file extension; <see langword="False"/> otherwise.</returns>
        ''' <exception cref="ArgumentNullException">extension1 or extension2</exception>
        Public Shared Function ExtensionsEqual(extension1 As String, extension2 As String) As Boolean

            If extension1 Is Nothing Then
                Throw New ArgumentNullException(NameOf(extension1))
            End If
            If extension2 Is Nothing Then
                Throw New ArgumentNullException(NameOf(extension2))
            End If

            Return String.Equals(extension1.Trim().TrimStart("."c),
                                 extension2.Trim().TrimStart("."c),
                                 StringComparison.OrdinalIgnoreCase)

        End Function

        ''' <summary>
        ''' Determines whether the specified PATH entries contain a directory.
        ''' </summary>
        ''' <param name="entries">The PATH entries.</param>
        ''' <param name="directory">The directory path.</param>
        ''' <returns><see langword="True"/> if the entries contain the directory; <see langword="False"/> otherwise.</returns>
        ''' <exception cref="ArgumentNullException">entries or directory</exception>
        Public Shared Function ContainsDirectoryEntry(entries As IEnumerable(Of String),
                                                      directory As String) As Boolean

            If entries Is Nothing Then
                Throw New ArgumentNullException(NameOf(entries))
            End If
            If directory Is Nothing Then
                Throw New ArgumentNullException(NameOf(directory))
            End If

            Return entries.Any(Function(entry As String) DirectoriesEqual(entry, directory))

        End Function

        ''' <summary>
        ''' Determines whether the specified PATHEXT entries contain a file extension.
        ''' </summary>
        ''' <param name="entries">The PATHEXT entries.</param>
        ''' <param name="extension">The file extension (the leading dot is optional).</param>
        ''' <returns><see langword="True"/> if the entries contain the file extension; <see langword="False"/> otherwise.</returns>
        ''' <exception cref="ArgumentNullException">entries or extension</exception>
        Public Shared Function ContainsExtensionEntry(entries As IEnumerable(Of String),
                                                      extension As String) As Boolean

            If entries Is Nothing Then
                Throw New ArgumentNullException(NameOf(entries))
            End If
            If extension Is Nothing Then
                Throw New ArgumentNullException(NameOf(extension))
            End If

            Return entries.Any(Function(entry As String) ExtensionsEqual(entry, extension))

        End Function

        ''' <summary>
        ''' Appends a directory to the specified PATH entries, if it is not already contained.
        ''' </summary>
        ''' <param name="entries">The PATH entries.</param>
        ''' <param name="directory">The directory path.</param>
        ''' <returns>A new list with the resulting entries.</returns>
        ''' <exception cref="ArgumentNullException">entries or directory</exception>
        ''' <exception cref="ArgumentException">Directory contains invalid character(s).;directory</exception>
        Public Shared Function AddDirectoryEntry(entries As IEnumerable(Of String),
                                                 directory As String) As List(Of String)

            If entries Is Nothing Then
                Throw New ArgumentNullException(NameOf(entries))
            End If
            ValidateDirectory(directory)

            Dim trimmed As String = directory.Trim()
            Dim result As New List(Of String)(entries)
            If Not ContainsDirectoryEntry(result, trimmed) Then
                result.Add(trimmed)
            End If
            Return result

        End Function

        ''' <summary>
        ''' Appends a file extension to the specified PATHEXT entries, if it is not already contained.
        ''' </summary>
        ''' <param name="entries">The PATHEXT entries.</param>
        ''' <param name="extension">The file extension (the leading dot is optional).</param>
        ''' <returns>A new list with the resulting entries.</returns>
        ''' <exception cref="ArgumentNullException">entries or extension</exception>
        ''' <exception cref="ArgumentException">File extension contains invalid character(s).;extension</exception>
        Public Shared Function AddExtensionEntry(entries As IEnumerable(Of String),
                                                 extension As String) As List(Of String)

            If entries Is Nothing Then
                Throw New ArgumentNullException(NameOf(entries))
            End If

            Dim normalized As String = NormalizeExtension(extension)
            Dim result As New List(Of String)(entries)
            If Not ContainsExtensionEntry(result, normalized) Then
                result.Add(normalized)
            End If
            Return result

        End Function

        ''' <summary>
        ''' Removes all the occurrences of a directory from the specified PATH entries.
        ''' </summary>
        ''' <param name="entries">The PATH entries.</param>
        ''' <param name="directory">The directory path.</param>
        ''' <returns>A new list with the resulting entries.</returns>
        ''' <exception cref="ArgumentNullException">entries or directory</exception>
        Public Shared Function RemoveDirectoryEntry(entries As IEnumerable(Of String),
                                                    directory As String) As List(Of String)

            If entries Is Nothing Then
                Throw New ArgumentNullException(NameOf(entries))
            End If
            If directory Is Nothing Then
                Throw New ArgumentNullException(NameOf(directory))
            End If

            Return entries.Where(Function(entry As String) Not DirectoriesEqual(entry, directory)).ToList()

        End Function

        ''' <summary>
        ''' Removes all the occurrences of a file extension from the specified PATHEXT entries.
        ''' </summary>
        ''' <param name="entries">The PATHEXT entries.</param>
        ''' <param name="extension">The file extension (the leading dot is optional).</param>
        ''' <returns>A new list with the resulting entries.</returns>
        ''' <exception cref="ArgumentNullException">entries or extension</exception>
        Public Shared Function RemoveExtensionEntry(entries As IEnumerable(Of String),
                                                    extension As String) As List(Of String)

            If entries Is Nothing Then
                Throw New ArgumentNullException(NameOf(entries))
            End If
            If extension Is Nothing Then
                Throw New ArgumentNullException(NameOf(extension))
            End If

            Return entries.Where(Function(entry As String) Not ExtensionsEqual(entry, extension)).ToList()

        End Function

        ''' <summary>
        ''' Removes the entry at the specified zero-based index.
        ''' </summary>
        ''' <param name="entries">The PATH or PATHEXT entries.</param>
        ''' <param name="index">The zero-based entry index, 0 = First.</param>
        ''' <returns>A new list with the resulting entries.</returns>
        ''' <exception cref="ArgumentNullException">entries</exception>
        ''' <exception cref="ArgumentOutOfRangeException">Entry index is out of range.;index</exception>
        Public Shared Function RemoveEntryAt(entries As IEnumerable(Of String),
                                             index As Integer) As List(Of String)

            If entries Is Nothing Then
                Throw New ArgumentNullException(NameOf(entries))
            End If

            Dim result As New List(Of String)(entries)
            If index < 0 OrElse index >= result.Count Then
                Throw New ArgumentOutOfRangeException(NameOf(index), index, "Entry index is out of range.")
            End If

            result.RemoveAt(index)
            Return result

        End Function

        ''' <summary>
        ''' Cleans the specified PATH entries: removes duplicated entries (keeping the first occurrence)
        ''' and entries of directories that does not exist. The order of the remaining entries is preserved.
        ''' </summary>
        ''' <param name="entries">The PATH entries.</param>
        ''' <param name="directoryExists">
        ''' A function that determines whether a directory exists.
        ''' It receives the directory path with its environment variables already expanded.
        ''' </param>
        ''' <returns>The clean result.</returns>
        ''' <exception cref="ArgumentNullException">entries or directoryExists</exception>
        Public Shared Function CleanDirectoryEntries(entries As IEnumerable(Of String),
                                                     directoryExists As Func(Of String, Boolean)) As CleanResult

            If entries Is Nothing Then
                Throw New ArgumentNullException(NameOf(entries))
            End If
            If directoryExists Is Nothing Then
                Throw New ArgumentNullException(NameOf(directoryExists))
            End If

            Dim kept As New List(Of String)
            Dim duplicates As New List(Of String)
            Dim missing As New List(Of String)
            Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

            For Each entry As String In entries
                If Not seen.Add(NormalizeDirectoryForComparison(entry)) Then
                    duplicates.Add(entry)

                ElseIf Not directoryExists(Environment.ExpandEnvironmentVariables(entry).Trim(""""c)) Then
                    missing.Add(entry)

                Else
                    kept.Add(entry)

                End If
            Next entry

            Return New CleanResult(kept, duplicates, missing)

        End Function

        ''' <summary>
        ''' Cleans the specified PATHEXT entries: removes duplicated entries (keeping the first occurrence).
        ''' The order of the remaining entries is preserved.
        ''' </summary>
        ''' <param name="entries">The PATHEXT entries.</param>
        ''' <returns>The clean result.</returns>
        ''' <exception cref="ArgumentNullException">entries</exception>
        Public Shared Function CleanExtensionEntries(entries As IEnumerable(Of String)) As CleanResult

            If entries Is Nothing Then
                Throw New ArgumentNullException(NameOf(entries))
            End If

            Dim kept As New List(Of String)
            Dim duplicates As New List(Of String)

            For Each entry As String In entries
                If kept.Any(Function(ext As String) ExtensionsEqual(ext, entry)) Then
                    duplicates.Add(entry)
                Else
                    kept.Add(entry)
                End If
            Next entry

            Return New CleanResult(kept, duplicates, New List(Of String))

        End Function

        ''' <summary>
        ''' Determines the registry value kind to use when writing a PATH or PATHEXT value,
        ''' preserving the kind of the existing value whenever possible.
        ''' </summary>
        ''' <param name="existingKind">
        ''' The kind of the existing value, or <see cref="RegistryValueKind.None"/> if the value does not exist.
        ''' </param>
        ''' <param name="data">The data to write.</param>
        ''' <returns>
        ''' <see cref="RegistryValueKind.String"/> if the existing value is <c>REG_SZ</c> and
        ''' the data does not contain environment variables; otherwise <see cref="RegistryValueKind.ExpandString"/>.
        ''' </returns>
        Public Shared Function ResolveValueKind(existingKind As RegistryValueKind,
                                                data As String) As RegistryValueKind

            Dim containsVariables As Boolean = data IsNot Nothing AndAlso data.IndexOf("%"c) <> -1

            If existingKind = RegistryValueKind.String AndAlso Not containsVariables Then
                Return RegistryValueKind.String
            End If

            Return RegistryValueKind.ExpandString

        End Function

        ''' <summary>
        ''' Builds the content of a registry script file (*.reg) that contains the specified PATH and PATHEXT values.
        ''' <para></para>
        ''' The values are written as <c>REG_EXPAND_SZ</c> (<c>hex(2)</c>),
        ''' so environment variables (e.g. <c>%SystemRoot%</c>) and special characters are preserved.
        ''' <para></para>
        ''' A <see langword="Nothing"/> value data means that the value does not exist, and it is omitted.
        ''' </summary>
        ''' <param name="currentUserPath">The data of the current user's PATH value.</param>
        ''' <param name="currentUserPathExt">The data of the current user's PATHEXT value.</param>
        ''' <param name="allUsersPath">The data of the local machine PATH value.</param>
        ''' <param name="allUsersPathExt">The data of the local machine PATHEXT value.</param>
        ''' <returns>The content of the registry script file.</returns>
        Public Shared Function BuildExportText(currentUserPath As String,
                                               currentUserPathExt As String,
                                               allUsersPath As String,
                                               allUsersPathExt As String) As String

            Dim nl As String = Environment.NewLine
            Dim sb As New StringBuilder()
            sb.Append($"Windows Registry Editor Version 5.00{nl}{nl}")

            sb.Append($"[{DefaultEnvironmentKeyCurrentUser}]{nl}")
            If currentUserPath IsNot Nothing Then
                sb.Append($"{FormatRegExpandStringValue(PathValueName, currentUserPath)}{nl}")
            End If
            If currentUserPathExt IsNot Nothing Then
                sb.Append($"{FormatRegExpandStringValue(PathExtValueName, currentUserPathExt)}{nl}")
            End If
            sb.Append(nl)

            sb.Append($"[{DefaultEnvironmentKeyAllUsers}]{nl}")
            If allUsersPath IsNot Nothing Then
                sb.Append($"{FormatRegExpandStringValue(PathValueName, allUsersPath)}{nl}")
            End If
            If allUsersPathExt IsNot Nothing Then
                sb.Append($"{FormatRegExpandStringValue(PathExtValueName, allUsersPathExt)}{nl}")
            End If

            Return sb.ToString()

        End Function

        ''' <summary>
        ''' Formats a <c>REG_EXPAND_SZ</c> value line (<c>"Name"=hex(2):...</c>) of a registry script file,
        ''' wrapping it into multiple lines in the same way as RegEdit.exe does.
        ''' </summary>
        ''' <param name="valueName">The value name.</param>
        ''' <param name="data">The value data.</param>
        ''' <returns>The formatted value line(s).</returns>
        ''' <exception cref="ArgumentNullException">valueName or data</exception>
        Public Shared Function FormatRegExpandStringValue(valueName As String, data As String) As String

            If String.IsNullOrEmpty(valueName) Then
                Throw New ArgumentNullException(NameOf(valueName))
            End If
            If data Is Nothing Then
                Throw New ArgumentNullException(NameOf(data))
            End If

            Dim escapedName As String = valueName.Replace("\", "\\").Replace("""", "\""")
            Dim bytes As Byte() = Encoding.Unicode.GetBytes($"{data}{ControlChars.NullChar}")

            Dim sb As New StringBuilder()
            Dim line As New StringBuilder($"""{escapedName}""=hex(2):")

            For i As Integer = 0 To bytes.Length - 1
                ' Wrap the line when the next "xx," token does not fit (keeping room for the trailing "\").
                If line.Length + 3 > RegScriptMaxLineLength - 1 Then
                    sb.Append($"{line}\{Environment.NewLine}")
                    line.Clear().Append("  ")
                End If

                line.Append(bytes(i).ToString("x2", CultureInfo.InvariantCulture))
                If i < bytes.Length - 1 Then
                    line.Append(","c)
                End If
            Next i

            sb.Append(line.ToString())
            Return sb.ToString()

        End Function

#End Region

#Region " Friend Methods (Unit Testing) "

        ''' <summary>
        ''' Redirects the registry Environment subkeys used by this class.
        ''' <para></para>
        ''' This is intended ONLY for unit testing, to operate on a sandbox registry key instead of the real environment.
        ''' </summary>
        ''' <param name="currentUserKeyPath">The registry path used for <see cref="UserMode.Current"/>.</param>
        ''' <param name="allUsersKeyPath">The registry path used for <see cref="UserMode.AllUsers"/>.</param>
        ''' <exception cref="ArgumentNullException">currentUserKeyPath or allUsersKeyPath</exception>
        Friend Shared Sub OverrideEnvironmentKeys(currentUserKeyPath As String, allUsersKeyPath As String)

            If String.IsNullOrWhiteSpace(currentUserKeyPath) Then
                Throw New ArgumentNullException(NameOf(currentUserKeyPath))
            End If
            If String.IsNullOrWhiteSpace(allUsersKeyPath) Then
                Throw New ArgumentNullException(NameOf(allUsersKeyPath))
            End If

            environmentKeyCurrentUser = currentUserKeyPath
            environmentKeyAllUsers = allUsersKeyPath

        End Sub

        ''' <summary>
        ''' Restores the real registry Environment subkeys used by this class.
        ''' </summary>
        Friend Shared Sub RestoreEnvironmentKeys()

            environmentKeyCurrentUser = DefaultEnvironmentKeyCurrentUser
            environmentKeyAllUsers = DefaultEnvironmentKeyAllUsers

        End Sub

#End Region

#Region " Private Methods "

        ''' <summary>
        ''' Gets the registry path of the Environment subkey for the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <returns>The registry path of the Environment subkey.</returns>
        ''' <exception cref="ArgumentException">Unexpected enumeration value.;userMode</exception>
        Private Shared Function GetEnvironmentKeyPath(userMode As UserMode) As String

            Select Case userMode
                Case UserMode.Current
                    Return environmentKeyCurrentUser

                Case UserMode.AllUsers
                    Return environmentKeyAllUsers

                Case Else
                    Throw New ArgumentException("Unexpected enumeration value.", NameOf(userMode))
            End Select

        End Function

        ''' <summary>
        ''' Opens the specified registry key.
        ''' </summary>
        ''' <param name="keyPath">The full registry key path.</param>
        ''' <param name="writable">If <see langword="True"/>, opens the key with write access.</param>
        ''' <returns>The opened registry key.</returns>
        ''' <exception cref="InvalidOperationException">The registry key does not exist.</exception>
        Private Shared Function OpenKey(keyPath As String, writable As Boolean) As RegistryKey

            Dim rootKey As RegistryKey = RegEdit.GetRootKey(keyPath)
            If rootKey Is Nothing Then
                Throw New InvalidOperationException($"Unrecognized registry root key: ""{keyPath}"".")
            End If

            Dim key As RegistryKey = rootKey.OpenSubKey(RegEdit.GetSubKeyPath(keyPath), writable)
            If key Is Nothing Then
                Throw New InvalidOperationException($"The registry key does not exist: ""{keyPath}"".")
            End If

            Return key

        End Function

        ''' <summary>
        ''' Reads the raw (not expanded) data of a string registry value.
        ''' </summary>
        ''' <param name="keyPath">The full registry key path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <returns>The raw value data, or <see langword="Nothing"/> if the value does not exist.</returns>
        ''' <exception cref="InvalidOperationException">The registry value is not a string value.</exception>
        Private Shared Function ReadValue(keyPath As String, valueName As String) As String

            Using key As RegistryKey = OpenKey(keyPath, writable:=False)
                Dim data As Object = key.GetValue(valueName, Nothing, RegistryValueOptions.DoNotExpandEnvironmentNames)
                If data Is Nothing Then
                    Return Nothing
                End If

                Dim text As String = TryCast(data, String)
                If text Is Nothing Then
                    Throw New InvalidOperationException($"The registry value ""{valueName}"" in ""{keyPath}"" is not a string value.")
                End If
                Return text
            End Using

        End Function

        ''' <summary>
        ''' Writes (creates or replaces) a string registry value, preserving its value kind whenever possible.
        ''' </summary>
        ''' <param name="keyPath">The full registry key path.</param>
        ''' <param name="valueName">The value name.</param>
        ''' <param name="data">The value data.</param>
        Private Shared Sub WriteValue(keyPath As String, valueName As String, data As String)

            Dim safeData As String = If(data, String.Empty)

            Using key As RegistryKey = OpenKey(keyPath, writable:=True)
                Dim existingKind As RegistryValueKind = RegistryValueKind.None
                If key.GetValue(valueName, Nothing, RegistryValueOptions.DoNotExpandEnvironmentNames) IsNot Nothing Then
                    existingKind = key.GetValueKind(valueName)
                End If

                key.SetValue(valueName, safeData, ResolveValueKind(existingKind, safeData))
            End Using

        End Sub

#End Region

    End Class

End Namespace

#End Region
