' ***********************************************************************
' Author   : Elektro
' Assembly : PATHS
' Modified : 09-April-2015
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
            ParseArguments()

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

            ' Parsing of parametters without aditional values required
            Select Case My.Application.CommandLineArgs(0).ToLower

                Case "/l", "/list" ' List
                    ListPath()
                    ListPathext()
                    Environment.Exit(0)

                Case "/c", "/clean" ' Clean
                    Clean()
                    Environment.Exit(0)

                Case "/r", "/restore" ' Restore
                    RestoreDefaults()
                    Environment.Exit(0)

                Case "/?", "/help" ' Help
                    ShowHelp()
                    Environment.Exit(0)

            End Select

            ' Parsing of parametters with aditional values required
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

                        Environment.Exit(0)

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

                        Environment.Exit(0)

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

                                    DisplayError(2)

                                End If

                                AddExtensionCurrentUser(My.Application.CommandLineArgs(x + 2).Trim)

                            Case "-machine"   ' Add to Local Machine PATHEXT

                                ' Additional argument prevention check
                                If Not My.Application.CommandLineArgs.Count = x + 3 _
                                OrElse My.Application.CommandLineArgs(x + 2) = String.Empty Then

                                    DisplayError(3)

                                End If

                                AddExtensionAllUsers(My.Application.CommandLineArgs(x + 2).Trim)

                            Case Else       ' Add to both

                                AddExtensionCurrentUser(My.Application.CommandLineArgs(x + 1))
                                AddExtensionAllUsers(My.Application.CommandLineArgs(x + 1))

                        End Select

                        Environment.Exit(0)

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

                        Environment.Exit(0)

                End Select

            Next x ' Argument

            ShowHelp() ' Launch help 'cause the parametter can't be recognized
            Environment.Exit(1)

        End Sub

#End Region

#Region " PATH Methods "

        ''' <summary>
        ''' Adds a directory into the current user's PATH.
        ''' </summary>
        ''' <param name="directory">The directory.</param>
        Private Sub AddDirectoryCurrentUser(ByVal directory As String)

            If Not PathUtil.PathExists(PathUtil.UserMode.Current) Then
                PathUtil.CreatePath(PathUtil.UserMode.Current)
            End If

            Select Case PathUtil.ContainsDirectory(PathUtil.UserMode.Current, directory)

                Case True
                    Console.WriteLine()
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F12*[X] ERROR*F08*: *F07*""{0}"" already exists in current user's PATH.*-F*", directory), {"*"c})

                Case Else
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Adding *F07*""{0}"" into current user's PATH...*-F*", directory), {"*"c})
                    Try
                        PathUtil.AddDirectory(PathUtil.UserMode.Current, directory)
                        ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*Directory successfully added into current user's PATH*-F*", {"*"c})
                    Catch ex As System.ArgumentException
                        ConsoleUtil.WriteColoredTextLine(" *F12*[X] ERROR*F08*: *F07*Directory contains invalid characters.*-F*", {"*"c})
                    End Try
                    Console.WriteLine()

            End Select

        End Sub

        ''' <summary>
        ''' Adds a directory into the local machine PATH.
        ''' </summary>
        ''' <param name="directory">The directory.</param>
        Private Sub AddDirectoryAllUsers(ByVal directory As String)

            If Not PathUtil.PathExists(PathUtil.UserMode.AllUsers) Then
                PathUtil.CreatePath(PathUtil.UserMode.AllUsers)
            End If

            Select Case PathUtil.ContainsDirectory(PathUtil.UserMode.AllUsers, directory)

                Case True
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F12*[X] ERROR*F08*: *F07*""{0}"" already exists in local machine PATH.*-F*", directory), {"*"c})

                Case Else
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Adding *F07*""{0}"" into local machine PATH...*-F*", directory), {"*"c})
                    Try
                        PathUtil.AddDirectory(PathUtil.UserMode.AllUsers, directory)
                        ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*Directory successfully added into local machine PATH*-F*", {"*"c})
                    Catch ex As System.ArgumentException
                        ConsoleUtil.WriteColoredTextLine(" *F12*[X] ERROR*F08*: *F07*Directory contains invalid characters.*-F*", {"*"c})
                    End Try
                    Console.WriteLine()

            End Select

        End Sub

        ''' <summary>
        ''' Deletes a directory from the current user's PATH.
        ''' </summary>
        ''' <param name="directory">The directory.</param>
        Private Sub DeleteDirectoryCurrentUser(ByVal directory As String)

            If Not PathUtil.PathExists(PathUtil.UserMode.Current) Then
                PathUtil.CreatePath(PathUtil.UserMode.Current)
            End If

            Select Case PathUtil.ContainsDirectory(PathUtil.UserMode.Current, directory)

                Case False
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F12*[X] ERROR*F08*: *F07*""{0}"" doesn't exists in current user's PATH.*-F*", directory), {"*"c})

                Case Else
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Deleting *F07*""{0}"" from current user's PATH...*-F*", directory), {"*"c})
                    PathUtil.DeleteDirectory(PathUtil.UserMode.Current, directory)
                    ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*Directory successfully deleted from current user's PATH.*-F*", {"*"c})
                    Console.WriteLine()

            End Select

        End Sub

        ''' <summary>
        ''' Deletes a directory from the current user's PATH.
        ''' </summary>
        ''' <param name="index">The directory index, 0 = First.</param>
        Private Sub DeleteDirectoryCurrentUser(ByVal index As Integer)

            If Not PathUtil.PathExists(PathUtil.UserMode.Current) Then
                PathUtil.CreatePath(PathUtil.UserMode.Current)
            End If

            Dim dirs As IEnumerable(Of String) = PathUtil.GetPathDataList(PathUtil.UserMode.Current)

            Select Case dirs.Count < (index + 1)

                Case True
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F12*[X] ERROR*F08*: *F07*Directory index *F06*{0} is out of range.*-F*", index), {"*"c})

                Case Else
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Deleting index *F07*""{0}"" from current user's PATH...*-F*", index), {"*"c})
                    PathUtil.DeleteDirectory(PathUtil.UserMode.Current, index)
                    ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*Directory successfully deleted from current user's PATH.*-F*", {"*"c})
                    Console.WriteLine()

            End Select

        End Sub

        ''' <summary>
        ''' Deletes a directory from the local machine PATH.
        ''' </summary>
        ''' <param name="directory">The directory.</param>
        Private Sub DeleteDirectoryAllUsers(ByVal directory As String)

            If Not PathUtil.PathExists(PathUtil.UserMode.AllUsers) Then
                PathUtil.CreatePath(PathUtil.UserMode.AllUsers)
            End If

            Select Case PathUtil.ContainsDirectory(PathUtil.UserMode.AllUsers, directory)

                Case False
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F12*[X] ERROR*F08*: *F07*""{0}"" doesn't exists in local machine PATH.*-F*", directory), {"*"c})

                Case Else
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Deleting *F07*""{0}"" from local machine PATH...*-F*", directory), {"*"c})
                    PathUtil.DeleteDirectory(PathUtil.UserMode.AllUsers, directory)
                    ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*Directory successfully deleted from local machine PATH.*-F*", {"*"c})
                    Console.WriteLine()

            End Select

        End Sub

        ''' <summary>
        ''' Deletes a directory from the local machine PATH.
        ''' </summary>
        ''' <param name="index">The directory index, 0 = First.</param>
        Private Sub DeleteDirectoryAllUsers(ByVal index As Integer)

            If Not PathUtil.PathExists(PathUtil.UserMode.AllUsers) Then
                PathUtil.CreatePath(PathUtil.UserMode.AllUsers)
            End If

            Dim dirs As IEnumerable(Of String) = PathUtil.GetPathDataList(PathUtil.UserMode.AllUsers)

            Select Case dirs.Count < (index + 1)

                Case True
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F12*[X] ERROR*F08*: *F07*Directory index *F06*{0} is out of range.*-F*", index), {"*"c})

                Case Else
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Deleting index *F07*""{0}"" from local machine PATH...*-F*", index), {"*"c})
                    PathUtil.DeleteDirectory(PathUtil.UserMode.AllUsers, index)
                    ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*Directory successfully deleted from local machine PATH.*-F*", {"*"c})
                    Console.WriteLine()

            End Select

        End Sub

        ''' <summary>
        ''' Exports PATH and PATHEXT to the specified file path.
        ''' </summary>
        ''' <param name="filePath">The file path.</param>
        Private Sub Export(ByVal filePath As String)

            If filePath.Any(Function(c As Char) IO.Path.GetInvalidPathChars.Contains(c)) Then
                ConsoleUtil.WriteColoredTextLine(String.Format(" *F12*[X] ERROR*F08*: *F07*Cannot proceed because ""*F08*{0}*F07*"" contains invalid characters.*-F*", filePath), {"*"c})
                Environment.Exit(1)
            End If

            PathUtil.Export(filePath)
            ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*Export done.*-F*", {"*"c})

        End Sub

        ''' <summary>
        ''' Lists the PATH.
        ''' </summary>
        Private Sub ListPath()

            Dim dirsCurrentUser As IEnumerable(Of String) = PathUtil.GetPathDataList(PathUtil.UserMode.Current)
            Dim dirsAllUsers As IEnumerable(Of String) = PathUtil.GetPathDataList(PathUtil.UserMode.AllUsers)
            Dim dirIndex As Integer = 0

            If Console.ForegroundColor = ConsoleColor.Red Then
                errorColor = ConsoleColor.Yellow
            End If

            ConsoleUtil.WriteColoredTextLine(" *F11*[+] *F14*Current User's PATH*F08*:*-F*" & Environment.NewLine, {"*"c})

            If dirsCurrentUser IsNot Nothing Then

                For Each dir As String In dirsCurrentUser

                    dirIndex += 1

                    If IO.Directory.Exists(dir) Then
                        ConsoleUtil.WriteColoredTextLine(String.Format("     *F06*{0} *F08*= *F07*{1}*-F*", dirIndex.ToString("00"), dir), {"*"c})
                    Else
                        ConsoleUtil.WriteColoredTextLine(String.Format("     *F06*{0} *F08*= *F08*{1}*-F*", dirIndex.ToString("00"), dir), {"*"c})

                    End If

                Next dir

                dirIndex = 0

            End If

            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine(" *F11*[+] *F14*Local Machine PATH*F08*:*-F*" & Environment.NewLine, {"*"c})

            If dirsAllUsers IsNot Nothing Then

                For Each dir As String In dirsAllUsers

                    dirIndex += 1

                    If IO.Directory.Exists(dir) Then
                        ConsoleUtil.WriteColoredTextLine(String.Format("     *F06*{0} *F08*= *F07*{1}*-F*", dirIndex.ToString("00"), dir), {"*"c})
                    Else
                        ConsoleUtil.WriteColoredTextLine(String.Format("     *F06*{0} *F08*= *F08*{1}*-F*", dirIndex.ToString("00"), dir), {"*"c})
                    End If

                Next dir

            End If

        End Sub

        ''' <summary>
        ''' Cleans the PATH and PATHEXT.
        ''' </summary>
        Private Sub Clean()

            Dim dirNotfoundtCurrentUserCount As Integer = 0
            Dim dirNotfoundAllUsersCount As Integer = 0

            Dim dirDupCurrentUserCount As Integer = 0
            Dim dirDupAllUsersCount As Integer = 0
            Dim extDupCurrentUserCount As Integer = 0
            Dim extDupAllUsersCount As Integer = 0

            Dim dirsCurrentPath As List(Of String) =
                PathUtil.GetPathDataList(PathUtil.UserMode.Current).
                Distinct(StringComparer.OrdinalIgnoreCase).
                OrderBy(Function(dir As String) dir).
                ToList()

            Dim dirsAllUsersPath As List(Of String) =
                PathUtil.GetPathDataList(PathUtil.UserMode.AllUsers).
                Distinct(StringComparer.OrdinalIgnoreCase).
                OrderBy(Function(dir As String) dir).
                ToList()

            Dim extsCurrentPath As List(Of String) =
                PathUtil.GetPathExtDataList(PathUtil.UserMode.Current).
                Distinct(StringComparer.OrdinalIgnoreCase).
                OrderBy(Function(ext As String) ext).
                ToList()

            Dim extsAllUsersPath As List(Of String) =
                PathUtil.GetPathExtDataList(PathUtil.UserMode.AllUsers).
                Distinct(StringComparer.OrdinalIgnoreCase).
                OrderBy(Function(ext As String) ext).
                ToList()

            ' *****************
            ' current user's PATH
            ' *****************
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*Processing current user's PATH*F08*...*-F*" & Environment.NewLine, {"*"c})

            dirDupCurrentUserCount = (PathUtil.GetPathDataList(PathUtil.UserMode.Current).Count - dirsCurrentPath.Count)

            For Each dir As String In dirsCurrentPath.ToArray

                If Not IO.Directory.Exists(dir) Then
                    dirsCurrentPath.Remove(dir)
                    dirNotfoundtCurrentUserCount += 1
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F07*Missing Directory Deleted: *F08*""*F06*{0}*F08*""*F07*.*-F*", dir), {"*"c})
                End If

            Next dir

            ' **************
            ' local machine PATH
            ' **************
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*Processing local machine PATH*F08*...*-F*" & Environment.NewLine, {"*"c})
            dirDupAllUsersCount += (PathUtil.GetPathDataList(PathUtil.UserMode.AllUsers).Count - dirsAllUsersPath.Count)

            For Each dir As String In dirsAllUsersPath.ToArray

                If Not IO.Directory.Exists(dir) Then
                    dirsAllUsersPath.Remove(dir)
                    dirNotfoundAllUsersCount += 1
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F07*Missing Directory Deleted: *F08*""*F06*{0}*F08*""*F07*.*-F*", dir), {"*"c})
                End If

            Next dir

            ' ********************
            ' current user's PATHEXT
            ' ********************
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*Processing current user's PATHEXT*F08*...*-F*" & Environment.NewLine, {"*"c})

            extDupCurrentUserCount += (PathUtil.GetPathExtDataList(PathUtil.UserMode.Current).Count - extsCurrentPath.Count)

            ' *****************
            ' local machine PATHEXT
            ' *****************
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*Processing local machine PATHEXT*F08*...*-F*" & Environment.NewLine, {"*"c})
            extDupAllUsersCount += (PathUtil.GetPathExtDataList(PathUtil.UserMode.AllUsers).Count - extsAllUsersPath.Count)

            ' ***************
            ' UPDAtE REGISTRY
            ' ***************
            PathUtil.CreatePath(PathUtil.UserMode.Current, String.Join(";"c, dirsCurrentPath))
            PathUtil.CreatePath(PathUtil.UserMode.AllUsers, String.Join(";"c, dirsAllUsersPath))
            PathUtil.CreatePathExt(PathUtil.UserMode.Current, String.Join(";"c, extsCurrentPath))
            PathUtil.CreatePathExt(PathUtil.UserMode.AllUsers, String.Join(";"c, extsAllUsersPath))

            ' ******
            ' REPORT
            ' ******
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Removed *F06*{0} *F07*not found directories  in current user's PATH.", dirNotfoundtCurrentUserCount.ToString("00")), {"*"c})
            ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Removed *F06*{0} *F07*not found directories  in local machine  PATH.", dirNotfoundAllUsersCount.ToString("00")), {"*"c})
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Removed *F06*{0} *F07*duplicated directories in current user's PATH.", dirDupCurrentUserCount.ToString("00")), {"*"c})
            ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Removed *F06*{0} *F07*duplicated directories in local machine  PATH.", dirDupAllUsersCount.ToString("00")), {"*"c})
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Removed *F06*{0} *F07*duplicated extensions  in current user's PATHEXT.", extDupCurrentUserCount.ToString("00")), {"*"c})
            ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Removed *F06*{0} *F07*duplicated extensions  in local machine  PATHEXT", extDupAllUsersCount.ToString("00")), {"*"c})
            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine(" *F11*[+] *F07*PATH and PATHEXT were successfully cleaned.*-F*", {"*"c})

        End Sub

        ''' <summary>
        ''' Restores the defaults values for PATH and PATHEXT.
        ''' </summary>
        Private Sub RestoreDefaults()

            PathUtil.CreatePath(PathUtil.UserMode.Current, PathUtil.GetDefaultPathDataString)
            PathUtil.CreatePath(PathUtil.UserMode.AllUsers, PathUtil.GetDefaultPathDataString)

            PathUtil.CreatePathExt(PathUtil.UserMode.Current, PathUtil.DefaultPathExtData)
            PathUtil.CreatePathExt(PathUtil.UserMode.AllUsers, PathUtil.DefaultPathExtData)

            Console.WriteLine(" [+] PATH and PATHEXT were successfully restored to defaults.")

        End Sub

#End Region

#Region " PATHEXT Methods "

        ''' <summary>
        ''' Adds a file extension into the current user's PATHEXT.
        ''' </summary>
        ''' <param name="ext">The file extension.</param>
        Private Sub AddExtensionCurrentUser(ByVal ext As String)

            If Not PathUtil.PathExtExists(PathUtil.UserMode.Current) Then
                PathUtil.CreatePathExt(PathUtil.UserMode.Current)
            End If

            Select Case PathUtil.ContainsExtension(PathUtil.UserMode.Current, ext)

                Case True
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F12*[X] ERROR*F08*: *F07*""{0}"" already exists in current user's PATHEXT.*-F*", ext), {"*"c})

                Case Else
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Adding *F07*""{0}"" into current user's PATHEXT...*-F*", ext), {"*"c})
                    PathUtil.AddExtension(PathUtil.UserMode.Current, ext)
                    ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*File extension successfully added into current user's PATHEXT*-F*", {"*"c})
                    Console.WriteLine()

            End Select

        End Sub

        ''' <summary>
        ''' Adds a file extension into the local machine PATHEXT.
        ''' </summary>
        ''' <param name="ext">The file extension.</param>
        Private Sub AddExtensionAllUsers(ByVal ext As String)

            If Not PathUtil.PathExtExists(PathUtil.UserMode.AllUsers) Then
                PathUtil.CreatePathExt(PathUtil.UserMode.AllUsers)
            End If

            Select Case PathUtil.ContainsExtension(PathUtil.UserMode.AllUsers, ext)

                Case True
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F12*[X] ERROR*F08*: *F07*""{0}"" already exists in local machine PATHEXT.*-F*", ext), {"*"c})

                Case Else
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Adding *F07*""{0}"" into local machine PATHEXT...*-F*", ext), {"*"c})
                    PathUtil.AddExtension(PathUtil.UserMode.AllUsers, ext)
                    ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*File extension successfully added into local machine PATHEXT*-F*", {"*"c})
                    Console.WriteLine()

            End Select

        End Sub

        ''' <summary>
        ''' Deletes a file extension from the current user's PATHEXT.
        ''' </summary>
        ''' <param name="ext">The file extension.</param>
        Private Sub DeleteExtensionCurrentUser(ByVal ext As String)

            If Not PathUtil.PathExtExists(PathUtil.UserMode.Current) Then
                PathUtil.CreatePathExt(PathUtil.UserMode.Current)
            End If

            Select Case PathUtil.ContainsExtension(PathUtil.UserMode.Current, ext)

                Case False
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F12*[X] ERROR*F08*: *F07*""{0}"" doesn't exists in current user's PATHEXT.*-F*", ext), {"*"c})

                Case Else
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Deleting *F07*""{0}"" from current user's PATHEXT...*-F*", ext), {"*"c})
                    PathUtil.DeleteExtension(PathUtil.UserMode.Current, ext)
                    ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*File extension successfully deleted from current user's PATHEXT*-F*", {"*"c})
                    Console.WriteLine()

            End Select

        End Sub

        ''' <summary>
        ''' Deletes a file extension from the local machine PATHEXT.
        ''' </summary>
        ''' <param name="ext">The file extension.</param>
        Private Sub DeleteExtensionAllUsers(ByVal ext As String)

            If Not PathUtil.PathExtExists(PathUtil.UserMode.AllUsers) Then
                PathUtil.CreatePathExt(PathUtil.UserMode.AllUsers)
            End If

            Select Case PathUtil.ContainsExtension(PathUtil.UserMode.AllUsers, ext)

                Case False
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F12*[X] ERROR*F08*: *F07*""{0}"" doesn't exists in local machine PATHEXT.*-F*", ext), {"*"c})

                Case Else
                    ConsoleUtil.WriteColoredTextLine(String.Format(" *F11*[i] *F07*Deleting *F07*""{0}"" from local machine PATHEXT...*-F*", ext), {"*"c})
                    PathUtil.DeleteExtension(PathUtil.UserMode.AllUsers, ext)
                    ConsoleUtil.WriteColoredTextLine(" *F11*[i] *F07*File extension successfully deleted from local machine PATHEXT*-F*", {"*"c})
                    Console.WriteLine()

            End Select

        End Sub

        ''' <summary>
        ''' Lists the PATHEXT.
        ''' </summary>
        Private Sub ListPathext()

            Dim pathExtCurrentUser As IEnumerable(Of String) = PathUtil.GetPathExtDataList(PathUtil.UserMode.Current)
            Dim pathExtAllUsers As IEnumerable(Of String) = PathUtil.GetPathExtDataList(PathUtil.UserMode.AllUsers)
            Dim extIndex As Integer = 0

            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine(" *F11*[+] *F14*Current User's PATHEXT*F08*:*-F*" & Environment.NewLine, {"*"c})

            If pathExtCurrentUser IsNot Nothing Then

                For Each ext As String In pathExtCurrentUser
                    extIndex += 1
                    ConsoleUtil.WriteColoredTextLine(String.Format("     *F06*{0} *F08*= *F07*{1}*-F*", extIndex.ToString("00"), ext), {"*"c})
                Next ext
                extIndex = 0

            End If

            Console.WriteLine()
            ConsoleUtil.WriteColoredTextLine(" *F11*[+] *F14*Local Machine PATHEXT*F08*:*-F*" & Environment.NewLine, {"*"c})

            If pathExtAllUsers IsNot Nothing Then

                For Each ext As String In pathExtAllUsers
                    extIndex += 1
                    ConsoleUtil.WriteColoredTextLine(String.Format("     *F06*{0} *F08*= *F07*{1}*-F*", extIndex.ToString("00"), ext), {"*"c})
                Next ext

            End If

        End Sub

#End Region

#Region " Miscellaneous Methods "

        ''' <summary>
        ''' Shows the specified program error.
        ''' </summary>
        ''' <param name="errorIndex">error Index.</param>
        Private Sub DisplayError(ByVal errorIndex As Integer)

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
                    Console.WriteLine(" [X] ERROR: Deletion by Entry Index is only allowed with -user and -machine parametters.")
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

                    MsgBox("[ DEBUG ] Error de syntaxis al parsear parámetro: ""ErrorIndex""")
                    Threading.Thread.Sleep(60)

            End Select

            Environment.Exit(1)

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