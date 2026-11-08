' ***********************************************************************
' Author   : Elektro
' Modified : 08-November-2026
' ***********************************************************************
' <copyright file="AssemblyAttributes.vb" company="Elektro Studios">
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

#End Region

' The registry tests redirect the shared (static) registry keys of PathUtil to a sandbox key,
' so the tests of this assembly must never run in parallel.
<Assembly: DoNotParallelize>
