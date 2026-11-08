' ***********************************************************************
' Author   : Elektro
' Modified : 08-November-2026
' ***********************************************************************
' <copyright file="TestCategories.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Test Categories "

''' <summary>
''' Contains the names of the test categories used in this test project.
''' <para></para>
''' Tests without category are pure logic tests that do not depend on the Windows registry nor on external processes.
''' </summary>
Friend NotInheritable Class TestCategories

    ''' <summary>
    ''' Tests that read and write the Windows registry (always inside a temporary sandbox key under HKCU).
    ''' </summary>
    Friend Const Registry As String = "Registry"

    ''' <summary>
    ''' Tests that launch the PATHS.exe process. These tests never run commands that modify the registry.
    ''' </summary>
    Friend Const EndToEnd As String = "EndToEnd"

    ''' <summary>
    ''' Prevents a default instance of the <see cref="TestCategories"/> class from being created.
    ''' </summary>
    Private Sub New()
    End Sub

End Class

#End Region
