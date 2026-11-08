' ***********************************************************************
' Author   : Elektro
' Assembly : PATHS
' Modified : 08-November-2026
' ***********************************************************************
' <copyright file="Main.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports PATHS.Tools

#End Region

#Region " Main "

Namespace UserInterface

    ''' <summary>
    ''' The Main module.
    ''' </summary>
    Module Main

#Region " Data Members "

        ' Color for invalid PATH entries
        Private errorColor As ConsoleColor = ConsoleColor.Red

#End Region

#Region " Entry Point "

        ''' <summary>
        ''' Defines the entry point of the application.
        ''' </summary>
        Public Sub Main()

            ConsoleUtil.WriteColoredTextLine(HelpSection.ColorizedHelp.<Logo>.Value, {"*"c})
            ConsoleUtil.WriteColoredTextLine(HelpSection.ColorizedHelp.<Separator>.Value, {"*"c})

            Try
                ParseArguments()

            Catch ex As Exception When TypeOf ex Is UnauthorizedAccessException OrElse
                                       TypeOf ex Is Security.SecurityException
                Console.WriteLine()
                ConsoleUtil.WriteColoredTextLine(" *F12*[X] ERROR*F08*: *F07*Access denied. Administrator privileges are required to modify the local machine PATH or PATHEXT.*-F*", {"*"c})
                Environment.Exit(1)

            End Try

        End Sub

#End Region

#Region " Arguments "

        ''' <summary>
        ''' Parses the arguments of the application.
        ''' </summary>
        Private Sub ParseArguments()

            If My.Application.CommandLineArgs.Count = 0 Then
                ShowHelp()
                Environment.Exit(0)
            End If

            Dim pathIndex As Integer = 0

            ' Parsing of parameters without additional values required
            Select Case My.Application.CommandLineArgs(0).ToLower

                Case "/l", "/list" ' List
                    ListPath()
                    ListPathext()
                    Environment.Exit(0)

                Case "/c", "/clean" ' Clean
                    Clean()
                    ExitAfterModification()

                Case "/r", "/restore" ' Restore
                    RestoreDefaults()
                    ExitAfterModification()

                Case "/?", "/help" ' Help
                    ShowHelp()
                    Environment.Exit(0)

            End Select

            ' Parsing of parameters with additional values required
            For x As Integer = 0 To My.Application.CommandLineArgs.Count - 1

                Select Case My.Application.CommandLineArgs(x).ToLower

                    Case "/a", "/add" ' Add

                        ' Additional argument prevention check
                        If Not My.Application.CommandLineArgs.Count > (x + 1) _
                        OrElse My.Application.CommandLineArgs(x + 1) = String.Empty Then

                            DisplayError(1)

                        End If

                        Select Case My.Application.CommandLineArgs(x + 1).ToLower

                            Case "-user" ' Add to current user's Path

                                ' Additional argument prevention check
                                If Not My.Application.CommandLineArgs.Count = x + 3 _
                                OrElse My.Application.CommandLineArgs(x + 2) = String.Empty Then

                                    DisplayError(2)

                                End If

                                AddDirectoryCurrentUser(My.Application.CommandLineArgs(x + 2).Trim)

                            Case "-machine"   ' Add to Local Machine Path

                                ' Additional argument prevention check
                                If Not My.Application.CommandLineArgs.Count = x + 3 _
                                OrElse My.Application.CommandLineArgs(x + 2) = String.Empty Then

                                    DisplayError(3)

                                End If

                                AddDirectoryAllUsers(My.Application.CommandLineArgs(x + 2).Trim)

                            Case Else       ' Add to both

                                AddDirectoryCurrentUser(My.Application.CommandLineArgs(x + 1))
                                AddDirectoryAllUsers(My.Application.CommandLineArgs(x + 1))

                        End Select

                        ExitAfterModification()

                    Case "/d", "/del" ' Delete

                        ' Additional argument prevention check
                        If Not My.Application.CommandLineArgs.Count > (x + 1) _
                        OrElse My.Application.CommandLineArgs(x + 1) = String.Empty Then

                            DisplayError(4)

                        ElseIf Integer.TryParse(My.Application.CommandLineArgs(x + 1), pathIndex) Then

                            DisplayError(5)

                        End If

                        Select Case My.Application.CommandLineArgs(x + 1).ToLower

                            Case "-user" ' Delete from current user's Path

                                ' Additional argument prevention check
                                If Not My.Application.CommandLineArgs.Count = x + 3 _
                                OrElse My.Application.CommandLineArgs(x + 2) = String.Empty Then

                                    DisplayError(6)

                                End If

                                If Integer.TryParse(My.Application.CommandLineArgs(x + 2), pathIndex) Then
                                    DeleteDirectoryCurrentUser(pathIndex)
                                Else
                                    DeleteDirectoryCurrentUser(My.Application.CommandLineArgs(x + 2).Trim)
                                End If

                            Case "-machine"   ' Delete from Local Machine Path

                                ' Additional argument prevention check
                                If Not My.Application.CommandLineArgs.Count = x + 3 _
                                OrElse My.Application.CommandLineArgs(x + 2) = String.Empty Then

                                    DisplayError(7)

                                End If

                                If Integer.TryParse(My.Application.CommandLineArgs(x + 2), pathIndex) Then
                                    DeleteDirectoryAllUsers(pathIndex)
                                Else
                                    DeleteDirectoryAllUsers(My.Application.CommandLineArgs(x + 2).Trim)
                                End If

                            Case Else       ' Delete from both

                                DeleteDirectoryCurrentUser(My.Application.CommandLineArgs(x + 1).Trim)
                                DeleteDirectoryAllUsers(My.Application.CommandLineArgs(x + 1).Trim)

                        End Select

                        ExitAfterModification()

                    Case "/e", "/export" ' Export

                        ' Additional argument prevention check
                        If Not My.Application.CommandLineArgs.Count = x + 2 _
                        OrElse My.Application.CommandLineArgs(x + 1) = String.Empty Then

                            DisplayError(8)

                        End If

                        Export(My.Application.CommandLineArgs(x + 1))
                        Environment.Exit(0)

                    Case "/addext" ' Add extension to PATHEXT

                        ' Additional argument prevention check
                        If Not My.Application.CommandLineArgs.Count > (x + 1) _
                        OrElse My.Application.CommandLineArgs(x + 1) = String.Empty Then

                            DisplayError(9)

                        End If

                        Select Case My.Application.CommandLineArgs(x + 1).ToLower

                            Case "-user" ' Add to current user's PATHEXT

                                ' Additional argument prevention check
                                If Not My.Application.CommandLineArgs.Count = x + 3 _
                                OrElse My.Application.CommandLineArgs(x + 2) = String.Empty Then

                                    DisplayError(11)

                                End If

                                AddExtensionCurrentUser(My.Application.CommandLineArgs(x + 2).Trim)

                            Case "-machine"   ' Add to Local Machine PATHEXT

                                ' Additional argument prevention check
                                If Not My.Application.CommandLineArgs.Count = x + 3 _
                                OrElse My.Application.CommandLineArgs(x + 2) = String.Empty Then

                                    DisplayError(12)

                                End If

                                AddExtensionAllUsers(My.Application.CommandLineArgs(x + 2).Trim)

                            Case Else       ' Add to both

                                AddExtensionCurrentUser(My.Application.CommandLineArgs(x + 1))
                                AddExtensionAllUsers(My.Application.CommandLineArgs(x + 1))

                        End Select

                        ExitAfterModification()

                    Case "/delext" ' Delete extension from PATHEXT

                        ' Additional argument prevention check
                        If Not My.Application.CommandLineArgs.Count > (x + 1) _
                        OrElse My.Application.CommandLineArgs(x + 1) = String.Empty Then

                            DisplayError(10)

                        End If

                        Select Case My.Application.CommandLineArgs(x + 1).ToLower

                            Case "-user" ' Delete extension from current user's PATHEXT

                                ' Additional argument prevention check
                                If Not My.Application.CommandLineArgs.Count = x + 3 _
                                OrElse My.Application.CommandLineArgs(x + 2) = String.Empty Then

                                    DisplayError(13)

                                End If

                                DeleteExtensionCurrentUser(My.Application.CommandLineArgs(x + 2).Trim)

                            Case "-machine"   ' Delete extension from Local Machine PATHEXT

                                ' Additional argument prevention check
                                If Not My.Application.CommandLineArgs.Count = x + 3 _
                                OrElse My.Application.CommandLineArgs(x + 2) = String.Empty Then

                                    DisplayError(14)

                                End If

                                DeleteExtensionAllUsers(My.Application.CommandLineArgs(x + 2).Trim)

                            Case Else       ' Delete extension from both

                                DeleteExtensionCurrentUser(My.Application.CommandLineArgs(x + 1))
                                DeleteExtensionAllUsers(My.Application.CommandLineArgs(x + 1))

                        End Select

                        ExitAfterModification()

                End Select

            Next x ' Argument

            ShowHelp() ' Launch help 'cause the parameter can't be recognized
            Environment.Exit(1)

        End Sub

#End Region

#Region " PATH Methods "

        ''' <summary>
        ''' Adds a directory into the current user's PATH.
        ''' </summary>
        ''' <param name="directory">The directory.</param>
        Private Sub AddDirectoryCurrentUser(directory As String)

            AddDirectory(PathUtil.UserMode.Current, "current user's", directory)

        End Sub

        ''' <summary>
        ''' Adds a directory into the local machine PATH.
        ''' </summary>
        ''' <param name="directory">The directory.</param>
        Private Sub AddDirectoryAllUsers(directory As String)

            AddDirectory(PathUtil.UserMode.AllUsers, "local machine", directory)

        End Sub

        ''' <summary>
        ''' Adds a directory into the PATH of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="target">The display name of the target PATH (e.g. "current user's").</param>
        ''' <param name="directory">The directory.</param>
        Private Sub AddDirectory(userMode As PathUtil.UserMode, target As String, directory As String)

            If Not PathUtil.PathExists(userMode) Then
                PathUtil.CreatePath(userMode)
            End If

            Try
                PathUtil.ValidateDirectory(directory)

            Catch ex As ArgumentException
                ConsoleUtil.WriteColoredTextLine(" *F12*[X] ERROR*F08*: *F07*Directory contains invalid characters.*-F*", {"*"c})
                Return

            End Try

            If PathUtil.ContainsDirectory(userMode, directory) Then
                ConsoleUtil.WriteColoredTextLine($" *F12*[X] ERROR*F08*: *F07*""{directory}"" already exists in {target} PATH.*-F*", {"*"c})
                Return
            End If

            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Adding *F07*""{directory}"" into {target} PATH...*-F*", {"*"c})
            PathUtil.AddDirectory(userMode, directory)
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Directory successfully added into {target} PATH*-F*", {"*"c})
            Console.WriteLine()

        End Sub

        ''' <summary>
        ''' Deletes a directory from the current user's PATH.
        ''' </summary>
        ''' <param name="directory">The directory.</param>
        Private Sub DeleteDirectoryCurrentUser(directory As String)

            DeleteDirectory(PathUtil.UserMode.Current, "current user's", directory)

        End Sub

        ''' <summary>
        ''' Deletes a directory from the current user's PATH.
        ''' </summary>
        ''' <param name="entryIndex">The one-based entry index, as shown by the /List switch (1 = First).</param>
        Private Sub DeleteDirectoryCurrentUser(entryIndex As Integer)

            DeleteDirectory(PathUtil.UserMode.Current, "current user's", entryIndex)

        End Sub

        ''' <summary>
        ''' Deletes a directory from the local machine PATH.
        ''' </summary>
        ''' <param name="directory">The directory.</param>
        Private Sub DeleteDirectoryAllUsers(directory As String)

            DeleteDirectory(PathUtil.UserMode.AllUsers, "local machine", directory)

        End Sub

        ''' <summary>
        ''' Deletes a directory from the local machine PATH.
        ''' </summary>
        ''' <param name="entryIndex">The one-based entry index, as shown by the /List switch (1 = First).</param>
        Private Sub DeleteDirectoryAllUsers(entryIndex As Integer)

            DeleteDirectory(PathUtil.UserMode.AllUsers, "local machine", entryIndex)

        End Sub

        ''' <summary>
        ''' Deletes a directory from the PATH of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="target">The display name of the target PATH (e.g. "current user's").</param>
        ''' <param name="directory">The directory.</param>
        Private Sub DeleteDirectory(userMode As PathUtil.UserMode, target As String, directory As String)

            If Not PathUtil.PathExists(userMode) Then
                PathUtil.CreatePath(userMode)
            End If

            If Not PathUtil.ContainsDirectory(userMode, directory) Then
                ConsoleUtil.WriteColoredTextLine($" *F12*[X] ERROR*F08*: *F07*""{directory}"" doesn't exists in {target} PATH.*-F*", {"*"c})
                Return
            End If

            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Deleting *F07*""{directory}"" from {target} PATH...*-F*", {"*"c})
            PathUtil.DeleteDirectory(userMode, directory)
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Directory successfully deleted from {target} PATH.*-F*", {"*"c})
            Console.WriteLine()

        End Sub

        ''' <summary>
        ''' Deletes a directory from the PATH of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="target">The display name of the target PATH (e.g. "current user's").</param>
        ''' <param name="entryIndex">The one-based entry index, as shown by the /List switch (1 = First).</param>
        Private Sub DeleteDirectory(userMode As PathUtil.UserMode, target As String, entryIndex As Integer)

            If Not PathUtil.PathExists(userMode) Then
                PathUtil.CreatePath(userMode)
            End If

            Dim count As Integer = PathUtil.GetPathDataList(userMode).Count()

            If entryIndex < 1 OrElse entryIndex > count Then
                ConsoleUtil.WriteColoredTextLine($" *F12*[X] ERROR*F08*: *F07*Directory index *F06*{entryIndex} *F07*is out of range (valid range: 1 to {count}).*-F*", {"*"c})
                Return
            End If

            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Deleting index *F07*""{entryIndex}"" from {target} PATH...*-F*", {"*"c})
            PathUtil.DeleteDirectory(userMode, entryIndex - 1)
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Directory successfully deleted from {target} PATH.*-F*", {"*"c})
            Console.WriteLine()

        End Sub

        ''' <summary>
        ''' Exports PATH and PATHEXT to the specified file path.
        ''' </summary>
        ''' <param name="filePath">The file path.</param>
        Private Sub Export(filePath As String)

            If filePath.IndexOfAny(IO.Path.GetInvalidPathChars()) <> -1 Then
                ConsoleUtil.WriteColoredTextLine($" *F12*[X] ERROR*F08*: *F07*Cannot proceed because ""*F08*{filePath}*F07*"" contains invalid characters.*-F*", {"*"c})
                Environment.Exit(1)
            End If

            PathUtil.Export(filePath)
            ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*Export done.*-F*", {"*"c})

        End Sub

        ''' <summary>
        ''' Lists the PATH.
        ''' </summary>
        Private Sub ListPath()

            If Console.ForegroundColor = ConsoleColor.Red Then
                errorColor = ConsoleColor.Yellow
            End If

            ConsoleUtil.WriteColoredTextLine($" *F11*[+] *F14*Current User's PATH*F08*:*-F*{Environment.NewLine}", {"*"c})
            ListDirectories(PathUtil.GetPathDataList(PathUtil.UserMode.Current))

            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine($" *F11*[+] *F14*Local Machine PATH*F08*:*-F*{Environment.NewLine}", {"*"c})
            ListDirectories(PathUtil.GetPathDataList(PathUtil.UserMode.AllUsers))

        End Sub

        ''' <summary>
        ''' Lists the specified PATH entries with their one-based index.
        ''' <para></para>
        ''' Entries of directories that does not exist are printed with a dimmed color.
        ''' </summary>
        ''' <param name="dirs">The PATH entries.</param>
        Private Sub ListDirectories(dirs As IEnumerable(Of String))

            Dim dirIndex As Integer = 0

            For Each dir As String In dirs
                dirIndex += 1

                Dim exists As Boolean = IO.Directory.Exists(Environment.ExpandEnvironmentVariables(dir).Trim(""""c))
                Dim color As String = If(exists, "F07", "F08")
                ConsoleUtil.WriteColoredTextLine($"     *F06*{dirIndex:00} *F08*= *{color}*{dir}*-F*", {"*"c})
            Next dir

        End Sub

        ''' <summary>
        ''' Cleans the PATH and PATHEXT.
        ''' </summary>
        Private Sub Clean()

            Dim existsFunc As Func(Of String, Boolean) = AddressOf IO.Directory.Exists

            ' *******************
            ' current user's PATH
            ' *******************
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Processing current user's PATH*F08*...*-F*{Environment.NewLine}", {"*"c})
            Dim pathCurrentUser As PathUtil.CleanResult =
                PathUtil.CleanDirectoryEntries(PathUtil.GetPathDataList(PathUtil.UserMode.Current), existsFunc)
            PrintMissingDirectories(pathCurrentUser)

            ' ******************
            ' local machine PATH
            ' ******************
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Processing local machine PATH*F08*...*-F*{Environment.NewLine}", {"*"c})
            Dim pathAllUsers As PathUtil.CleanResult =
                PathUtil.CleanDirectoryEntries(PathUtil.GetPathDataList(PathUtil.UserMode.AllUsers), existsFunc)
            PrintMissingDirectories(pathAllUsers)

            ' **********************
            ' current user's PATHEXT
            ' **********************
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Processing current user's PATHEXT*F08*...*-F*{Environment.NewLine}", {"*"c})
            Dim pathExtCurrentUser As PathUtil.CleanResult =
                PathUtil.CleanExtensionEntries(PathUtil.GetPathExtDataList(PathUtil.UserMode.Current))

            ' *********************
            ' local machine PATHEXT
            ' *********************
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Processing local machine PATHEXT*F08*...*-F*{Environment.NewLine}", {"*"c})
            Dim pathExtAllUsers As PathUtil.CleanResult =
                PathUtil.CleanExtensionEntries(PathUtil.GetPathExtDataList(PathUtil.UserMode.AllUsers))

            ' ***************
            ' UPDATE REGISTRY
            ' ***************
            ' Only the values that exist are rewritten, to avoid creating empty values.
            If PathUtil.PathExists(PathUtil.UserMode.Current) Then
                PathUtil.CreatePath(PathUtil.UserMode.Current, PathUtil.JoinEntries(pathCurrentUser.Entries))
            End If
            If PathUtil.PathExists(PathUtil.UserMode.AllUsers) Then
                PathUtil.CreatePath(PathUtil.UserMode.AllUsers, PathUtil.JoinEntries(pathAllUsers.Entries))
            End If
            If PathUtil.PathExtExists(PathUtil.UserMode.Current) Then
                PathUtil.CreatePathExt(PathUtil.UserMode.Current, PathUtil.JoinEntries(pathExtCurrentUser.Entries))
            End If
            If PathUtil.PathExtExists(PathUtil.UserMode.AllUsers) Then
                PathUtil.CreatePathExt(PathUtil.UserMode.AllUsers, PathUtil.JoinEntries(pathExtAllUsers.Entries))
            End If

            ' ******
            ' REPORT
            ' ******
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Removed *F06*{pathCurrentUser.RemovedMissing.Count:00} *F07*not found directories  in current user's PATH.", {"*"c})
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Removed *F06*{pathAllUsers.RemovedMissing.Count:00} *F07*not found directories  in local machine  PATH.", {"*"c})
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Removed *F06*{pathCurrentUser.RemovedDuplicates.Count:00} *F07*duplicated directories in current user's PATH.", {"*"c})
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Removed *F06*{pathAllUsers.RemovedDuplicates.Count:00} *F07*duplicated directories in local machine  PATH.", {"*"c})
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Removed *F06*{pathExtCurrentUser.RemovedDuplicates.Count:00} *F07*duplicated extensions  in current user's PATHEXT.", {"*"c})
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Removed *F06*{pathExtAllUsers.RemovedDuplicates.Count:00} *F07*duplicated extensions  in local machine  PATHEXT.", {"*"c})
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine(" *F11*[+] *F07*PATH and PATHEXT were successfully cleaned.*-F*", {"*"c})

        End Sub

        ''' <summary>
        ''' Prints the missing directories that were removed by a clean operation.
        ''' </summary>
        ''' <param name="result">The clean result.</param>
        Private Sub PrintMissingDirectories(result As PathUtil.CleanResult)

            For Each dir As String In result.RemovedMissing
                ConsoleUtil.WriteColoredTextLine($" *F07*Missing Directory Deleted: *F08*""*F06*{dir}*F08*""*F07*.*-F*", {"*"c})
            Next dir

        End Sub

        ''' <summary>
        ''' Restores the defaults values for PATH and PATHEXT.
        ''' </summary>
        Private Sub RestoreDefaults()

            PathUtil.CreatePath(PathUtil.UserMode.Current, PathUtil.GetDefaultPathDataString)
            PathUtil.CreatePath(PathUtil.UserMode.AllUsers, PathUtil.GetDefaultPathDataString)

            PathUtil.CreatePathExt(PathUtil.UserMode.Current, PathUtil.DefaultPathExtData)
            PathUtil.CreatePathExt(PathUtil.UserMode.AllUsers, PathUtil.DefaultPathExtData)

            ConsoleUtil.WriteColoredTextLine(" *F11*[+] *F07*PATH and PATHEXT were successfully restored to defaults.*-F*", {"*"c})

        End Sub

#End Region

#Region " PATHEXT Methods "

        ''' <summary>
        ''' Adds a file extension into the current user's PATHEXT.
        ''' </summary>
        ''' <param name="ext">The file extension.</param>
        Private Sub AddExtensionCurrentUser(ext As String)

            AddExtension(PathUtil.UserMode.Current, "current user's", ext)

        End Sub

        ''' <summary>
        ''' Adds a file extension into the local machine PATHEXT.
        ''' </summary>
        ''' <param name="ext">The file extension.</param>
        Private Sub AddExtensionAllUsers(ext As String)

            AddExtension(PathUtil.UserMode.AllUsers, "local machine", ext)

        End Sub

        ''' <summary>
        ''' Adds a file extension into the PATHEXT of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="target">The display name of the target PATHEXT (e.g. "current user's").</param>
        ''' <param name="ext">The file extension.</param>
        Private Sub AddExtension(userMode As PathUtil.UserMode, target As String, ext As String)

            If Not PathUtil.PathExtExists(userMode) Then
                PathUtil.CreatePathExt(userMode)
            End If

            Dim normalized As String
            Try
                normalized = PathUtil.NormalizeExtension(ext)

            Catch ex As ArgumentException
                ConsoleUtil.WriteColoredTextLine($" *F12*[X] ERROR*F08*: *F07*""{ext}"" is not a valid file extension.*-F*", {"*"c})
                Return

            End Try

            If PathUtil.ContainsExtension(userMode, normalized) Then
                ConsoleUtil.WriteColoredTextLine($" *F12*[X] ERROR*F08*: *F07*""{normalized}"" already exists in {target} PATHEXT.*-F*", {"*"c})
                Return
            End If

            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Adding *F07*""{normalized}"" into {target} PATHEXT...*-F*", {"*"c})
            PathUtil.AddExtension(userMode, normalized)
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*File extension successfully added into {target} PATHEXT*-F*", {"*"c})
            Console.WriteLine()

        End Sub

        ''' <summary>
        ''' Deletes a file extension from the current user's PATHEXT.
        ''' </summary>
        ''' <param name="ext">The file extension.</param>
        Private Sub DeleteExtensionCurrentUser(ext As String)

            DeleteExtension(PathUtil.UserMode.Current, "current user's", ext)

        End Sub

        ''' <summary>
        ''' Deletes a file extension from the local machine PATHEXT.
        ''' </summary>
        ''' <param name="ext">The file extension.</param>
        Private Sub DeleteExtensionAllUsers(ext As String)

            DeleteExtension(PathUtil.UserMode.AllUsers, "local machine", ext)

        End Sub

        ''' <summary>
        ''' Deletes a file extension from the PATHEXT of the specified user.
        ''' </summary>
        ''' <param name="userMode">The user mode.</param>
        ''' <param name="target">The display name of the target PATHEXT (e.g. "current user's").</param>
        ''' <param name="ext">The file extension.</param>
        Private Sub DeleteExtension(userMode As PathUtil.UserMode, target As String, ext As String)

            If Not PathUtil.PathExtExists(userMode) Then
                PathUtil.CreatePathExt(userMode)
            End If

            If Not PathUtil.ContainsExtension(userMode, ext) Then
                ConsoleUtil.WriteColoredTextLine($" *F12*[X] ERROR*F08*: *F07*""{ext}"" doesn't exists in {target} PATHEXT.*-F*", {"*"c})
                Return
            End If

            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*Deleting *F07*""{ext}"" from {target} PATHEXT...*-F*", {"*"c})
            PathUtil.DeleteExtension(userMode, ext)
            ConsoleUtil.WriteColoredTextLine($" *F11*[i] *F07*File extension successfully deleted from {target} PATHEXT*-F*", {"*"c})
            Console.WriteLine()

        End Sub

        ''' <summary>
        ''' Lists the PATHEXT.
        ''' </summary>
        Private Sub ListPathext()

            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine($" *F11*[+] *F14*Current User's PATHEXT*F08*:*-F*{Environment.NewLine}", {"*"c})
            ListExtensions(PathUtil.GetPathExtDataList(PathUtil.UserMode.Current))

            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine($" *F11*[+] *F14*Local Machine PATHEXT*F08*:*-F*{Environment.NewLine}", {"*"c})
            ListExtensions(PathUtil.GetPathExtDataList(PathUtil.UserMode.AllUsers))

        End Sub

        ''' <summary>
        ''' Lists the specified PATHEXT entries with their one-based index.
        ''' </summary>
        ''' <param name="exts">The PATHEXT entries.</param>
        Private Sub ListExtensions(exts As IEnumerable(Of String))

            Dim extIndex As Integer = 0

            For Each ext As String In exts
                extIndex += 1
                ConsoleUtil.WriteColoredTextLine($"     *F06*{extIndex:00} *F08*= *F07*{ext}*-F*", {"*"c})
            Next ext

        End Sub

#End Region

#Region " Miscellaneous Methods "

        ''' <summary>
        ''' Shows the specified program error.
        ''' </summary>
        ''' <param name="errorIndex">error Index.</param>
        Private Sub DisplayError(errorIndex As Integer)

            Select Case errorIndex

                Case 1 ' /Add
                    Console.WriteLine(" [X] ERROR: Please provide a directory to add.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /add ""C:\Directory""")

                Case 2 ' /Add -user
                    Console.WriteLine(" [X] ERROR: Please provide a directory to add.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /add -user ""C:\Directory""")

                Case 3 ' /Add -machine
                    Console.WriteLine(" [X] ERROR: Please provide a directory to add.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /add -machine ""C:\Directory""")

                Case 4 ' /Del (String)
                    Console.WriteLine(" [X] ERROR: Please provide a directory to delete.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /del ""C:\Directory""")

                Case 5 ' /Del (Integer)
                    Console.WriteLine(" [X] ERROR: Deletion by Entry Index is only allowed with -user and -machine parameters.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /del -user 5")
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /del -machine 5")

                Case 6 ' /Del -user
                    Console.WriteLine(" [X] ERROR: Please provide a directory to delete.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /del -user ""C:\Directory""")

                Case 7 ' /Del -machine
                    Console.WriteLine(" [X] ERROR: Please provide a directory to delete.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /del -machine ""C:\Directory""")

                Case 8 ' /Export
                    Console.WriteLine(" [X] ERROR: Please provide a File to export the data.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /Export ""C:\Exported.reg""")

                Case 9 ' /AddExt 
                    Console.WriteLine(" [X] ERROR: Please provide a File-Extension to add.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /AddExt "".PY""")

                Case 10 ' /DelExt
                    Console.WriteLine(" [X] ERROR: Please provide a File-Extension to delete.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /DelExt "".RB""")

                Case 11 ' /AddExt -user
                    Console.WriteLine(" [X] ERROR: Please provide a File-Extension to add.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /AddExt -user "".PY""")

                Case 12 ' /AddExt -machine
                    Console.WriteLine(" [X] ERROR: Please provide a File-Extension to add.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /AddExt -machine "".PY""")

                Case 13 ' /DelExt -user
                    Console.WriteLine(" [X] ERROR: Please provide a File-Extension to delete.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /DelExt -user "".RB""")

                Case 14 ' /DelExt -machine
                    Console.WriteLine(" [X] ERROR: Please provide a File-Extension to delete.")
                    Console.WriteLine()
                    Console.WriteLine(" [i] EXAMPLE: PATHS.exe /DelExt -machine "".RB""")

                Case Else
                    Throw New ArgumentOutOfRangeException(NameOf(errorIndex), errorIndex, "Unexpected error index.")

            End Select

            Environment.Exit(1)

        End Sub

        ''' <summary>
        ''' Notifies the system about the environment changes, and terminates the process with exit code 0.
        ''' </summary>
        Private Sub ExitAfterModification()

            PathUtil.NotifyEnvironmentChanged()
            Environment.Exit(0)

        End Sub

        ''' <summary>
        ''' Shows the program help.
        ''' </summary>
        Private Sub ShowHelp()

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine(String.Format("    *F14*Author   *F08*:*F07* {0}", HelpSection.ColorizedHelp.<Author>.Value))
            sb.AppendLine(String.Format("    *F14*Copyright*F08*:*F07* {0}", HelpSection.ColorizedHelp.<Copyright>.Value))
            sb.AppendLine(String.Format("    *F14*Website  *F08*:*F07* {0}", HelpSection.ColorizedHelp.<Website>.Value))
            sb.AppendLine(HelpSection.ColorizedHelp.<Separator>.Value)
            sb.AppendLine(HelpSection.ColorizedHelp.<Syntax>.Value)
            sb.AppendLine(HelpSection.ColorizedHelp.<SyntaxExtra>.Value)
            sb.AppendLine(HelpSection.ColorizedHelp.<UsageExamples>.Value)
            ConsoleUtil.WriteColoredTextLine(sb.ToString, {"*"c})

        End Sub

#End Region

    End Module

End Namespace

#End Region