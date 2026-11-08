' ***********************************************************************
' Author   : Elektro
' Modified : 08-November-2026
' ***********************************************************************
' <copyright file="PathUtilEntryTests.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections.Generic
Imports System.Linq

Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.Win32

Imports PATHS.Tools

#End Region

#Region " PathUtil Entry Tests "

''' <summary>
''' Pure logic tests for the PATH / PATHEXT entry manipulation methods of <see cref="PathUtil"/>.
''' <para></para>
''' These tests do not access the Windows registry.
''' </summary>
<TestClass>
Public NotInheritable Class PathUtilEntryTests

#Region " Constants "

    ''' <summary>
    ''' The name of a temporary environment variable used to test environment variable expansion.
    ''' </summary>
    Private Const TestVariableName As String = "PATHS_TESTS_DIR"

#End Region

#Region " Initialization / Cleanup "

    ''' <summary>
    ''' Defines the temporary environment variable used by the tests.
    ''' </summary>
    <TestInitialize>
    Public Sub TestInitialize()

        Environment.SetEnvironmentVariable(TestVariableName, "C:\Tools")

    End Sub

    ''' <summary>
    ''' Removes the temporary environment variable used by the tests.
    ''' </summary>
    <TestCleanup>
    Public Sub TestCleanup()

        Environment.SetEnvironmentVariable(TestVariableName, Nothing)

    End Sub

#End Region

#Region " Defaults "

    ''' <summary>
    ''' The default PATH data depends on the operating system bitness.
    ''' </summary>
    <TestMethod>
    Public Sub GetDefaultPathDataString_MatchesOperatingSystemBitness()

        Dim expected As String = If(Environment.Is64BitOperatingSystem, PathUtil.DefaultPathDataWin64, PathUtil.DefaultPathDataWin32)
        Assert.AreEqual(expected, PathUtil.GetDefaultPathDataString())

    End Sub

    ''' <summary>
    ''' The default PATH list is the split of the default PATH string.
    ''' </summary>
    <TestMethod>
    Public Sub GetDefaultPathDataList_IsSplitOfDefaultString()

        CollectionAssert.AreEqual(PathUtil.GetDefaultPathDataString().Split(";"c),
                                  PathUtil.GetDefaultPathDataList().ToArray())

    End Sub

    ''' <summary>
    ''' Only the 64-Bit default PATH contains the SysWOW64 directory.
    ''' </summary>
    <TestMethod>
    Public Sub DefaultPathData_OnlyWin64ContainsSysWow64()

        StringAssert.Contains(PathUtil.DefaultPathDataWin64, "C:\Windows\SysWOW64")
        Assert.IsFalse(PathUtil.DefaultPathDataWin32.Contains("SysWOW64"))

    End Sub

    ''' <summary>
    ''' The default PATHEXT contains the essential executable extensions, without duplicates.
    ''' </summary>
    <TestMethod>
    Public Sub DefaultPathExtData_ContainsEssentialExtensionsWithoutDuplicates()

        Dim exts As String() = PathUtil.SplitEntries(PathUtil.DefaultPathExtData).ToArray()

        CollectionAssert.Contains(exts, ".COM")
        CollectionAssert.Contains(exts, ".EXE")
        CollectionAssert.Contains(exts, ".BAT")
        CollectionAssert.Contains(exts, ".CMD")
        CollectionAssert.AllItemsAreUnique(exts)
        Assert.IsTrue(exts.All(Function(ext As String) ext.StartsWith(".", StringComparison.Ordinal)))

    End Sub

#End Region

#Region " SplitEntries / JoinEntries "

    ''' <summary>
    ''' Splitting a null data returns an empty sequence.
    ''' </summary>
    <TestMethod>
    Public Sub SplitEntries_Null_ReturnsEmpty()

        Assert.AreEqual(0, PathUtil.SplitEntries(Nothing).Count())

    End Sub

    ''' <summary>
    ''' Splitting an empty or blank data returns an empty sequence.
    ''' </summary>
    <TestMethod>
    <DataRow("")>
    <DataRow(";")>
    <DataRow(";;;")>
    <DataRow(" ; ;  ")>
    Public Sub SplitEntries_EmptyOrBlank_ReturnsEmpty(data As String)

        Assert.AreEqual(0, PathUtil.SplitEntries(data).Count())

    End Sub

    ''' <summary>
    ''' Splitting removes empty entries and trims the entries, preserving their order.
    ''' </summary>
    <TestMethod>
    Public Sub SplitEntries_RemovesEmptyEntriesAndTrims()

        Dim result As String() = PathUtil.SplitEntries(";C:\B; C:\A ;;%SystemRoot%;").ToArray()

        CollectionAssert.AreEqual({"C:\B", "C:\A", "%SystemRoot%"}, result)

    End Sub

    ''' <summary>
    ''' Splitting does not remove duplicated entries (that is the job of the clean operation).
    ''' </summary>
    <TestMethod>
    Public Sub SplitEntries_KeepsDuplicates()

        Assert.AreEqual(3, PathUtil.SplitEntries("C:\A;C:\A;c:\a").Count())

    End Sub

    ''' <summary>
    ''' Joining entries uses the semicolon separator.
    ''' </summary>
    <TestMethod>
    Public Sub JoinEntries_UsesSemicolon()

        Assert.AreEqual("C:\A;C:\B", PathUtil.JoinEntries({"C:\A", "C:\B"}))
        Assert.AreEqual(String.Empty, PathUtil.JoinEntries(Enumerable.Empty(Of String)()))

    End Sub

    ''' <summary>
    ''' Split and join are inverse operations for well-formed data.
    ''' </summary>
    <TestMethod>
    Public Sub SplitAndJoin_RoundTrip()

        Const data As String = "%SystemRoot%\system32;%SystemRoot%;C:\Program Files\Tool"
        Assert.AreEqual(data, PathUtil.JoinEntries(PathUtil.SplitEntries(data)))

    End Sub

    ''' <summary>
    ''' Joining a null sequence throws.
    ''' </summary>
    <TestMethod>
    Public Sub JoinEntries_Null_Throws()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.JoinEntries(Nothing))

    End Sub

#End Region

#Region " ValidateDirectory "

    ''' <summary>
    ''' Valid directories pass the validation.
    ''' </summary>
    <TestMethod>
    <DataRow("C:\Directory")>
    <DataRow("C:\Program Files (x86)\Tool")>
    <DataRow("%USERPROFILE%\bin")>
    <DataRow("\\server\share\bin")>
    <DataRow("relative\dir")>
    Public Sub ValidateDirectory_Valid_DoesNotThrow(directory As String)

        PathUtil.ValidateDirectory(directory)

    End Sub

    ''' <summary>
    ''' A null directory throws <see cref="ArgumentNullException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub ValidateDirectory_Null_Throws()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.ValidateDirectory(Nothing))

    End Sub

    ''' <summary>
    ''' An empty or blank directory throws <see cref="ArgumentException"/>.
    ''' </summary>
    <TestMethod>
    <DataRow("")>
    <DataRow("   ")>
    Public Sub ValidateDirectory_EmptyOrBlank_Throws(directory As String)

        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.ValidateDirectory(directory))

    End Sub

    ''' <summary>
    ''' A directory containing the entry separator or invalid path characters throws <see cref="ArgumentException"/>,
    ''' because it would corrupt the PATH value.
    ''' </summary>
    <TestMethod>
    <DataRow("C:\A;C:\B")>
    <DataRow("C:\Dir|Name")>
    <DataRow("C:\Dir<Name>")>
    <DataRow("C:\Dir""Name")>
    <DataRow("C:\Dir*")>
    <DataRow("C:\Dir?")>
    Public Sub ValidateDirectory_InvalidCharacters_Throws(directory As String)

        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.ValidateDirectory(directory))

    End Sub

    ''' <summary>
    ''' A directory containing control characters throws <see cref="ArgumentException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub ValidateDirectory_ControlCharacters_Throws()

        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.ValidateDirectory($"C:\Dir{ControlChars.Tab}Name"))
        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.ValidateDirectory($"C:\Dir{ControlChars.NullChar}"))

    End Sub

#End Region

#Region " NormalizeExtension "

    ''' <summary>
    ''' The leading dot is added when missing (regression: the original code discarded the result of String.Insert).
    ''' </summary>
    <TestMethod>
    <DataRow("PY", ".PY")>
    <DataRow(".PY", ".PY")>
    <DataRow("  .rb  ", ".rb")>
    <DataRow("ps1", ".ps1")>
    Public Sub NormalizeExtension_AddsLeadingDotAndTrims(input As String, expected As String)

        Assert.AreEqual(expected, PathUtil.NormalizeExtension(input))

    End Sub

    ''' <summary>
    ''' A null extension throws <see cref="ArgumentNullException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub NormalizeExtension_Null_Throws()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.NormalizeExtension(Nothing))

    End Sub

    ''' <summary>
    ''' Empty or invalid extensions throw <see cref="ArgumentException"/>.
    ''' </summary>
    <TestMethod>
    <DataRow("")>
    <DataRow("   ")>
    <DataRow(".")>
    <DataRow("..")>
    <DataRow(".PY;.RB")>
    <DataRow(".P Y")>
    <DataRow(".P*")>
    <DataRow(".P?")>
    <DataRow(".P\Y")>
    <DataRow(".P/Y")>
    <DataRow(".P:Y")>
    <DataRow(".P|Y")>
    Public Sub NormalizeExtension_EmptyOrInvalid_Throws(input As String)

        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.NormalizeExtension(input))

    End Sub

#End Region

#Region " Directory Comparison "

    ''' <summary>
    ''' Directory normalization trims, removes quotes and trailing separators, and expands environment variables.
    ''' </summary>
    <TestMethod>
    <DataRow("C:\Tools", "C:\Tools")>
    <DataRow("C:\Tools\", "C:\Tools")>
    <DataRow("C:\Tools\\", "C:\Tools")>
    <DataRow("C:\Tools/", "C:\Tools")>
    <DataRow("  C:\Tools  ", "C:\Tools")>
    <DataRow("""C:\Tools""", "C:\Tools")>
    <DataRow("%PATHS_TESTS_DIR%", "C:\Tools")>
    <DataRow("%PATHS_TESTS_DIR%\bin\", "C:\Tools\bin")>
    <DataRow("C:\", "C:\")>
    <DataRow("C:", "C:\")>
    Public Sub NormalizeDirectoryForComparison_ReturnsExpected(input As String, expected As String)

        Assert.AreEqual(expected, PathUtil.NormalizeDirectoryForComparison(input))

    End Sub

    ''' <summary>
    ''' An undefined environment variable is kept as-is.
    ''' </summary>
    <TestMethod>
    Public Sub NormalizeDirectoryForComparison_UndefinedVariable_IsKept()

        Assert.AreEqual("%PATHS_TESTS_UNDEFINED_VAR%\bin",
                        PathUtil.NormalizeDirectoryForComparison("%PATHS_TESTS_UNDEFINED_VAR%\bin\"))

    End Sub

    ''' <summary>
    ''' Directory equality is case-insensitive, ignores trailing separators and expands environment variables.
    ''' </summary>
    <TestMethod>
    <DataRow("C:\Tools", "c:\tools")>
    <DataRow("C:\Tools\", "C:\Tools")>
    <DataRow("%PATHS_TESTS_DIR%", "C:\TOOLS\")>
    <DataRow("""C:\Tools""", "C:\Tools")>
    Public Sub DirectoriesEqual_EquivalentPaths_ReturnsTrue(directory1 As String, directory2 As String)

        Assert.IsTrue(PathUtil.DirectoriesEqual(directory1, directory2))
        Assert.IsTrue(PathUtil.DirectoriesEqual(directory2, directory1))

    End Sub

    ''' <summary>
    ''' Different directories are not equal.
    ''' </summary>
    <TestMethod>
    <DataRow("C:\Tools", "C:\Tool")>
    <DataRow("C:\Tools", "D:\Tools")>
    <DataRow("C:\Tools", "C:\Tools\bin")>
    <DataRow("C:\", "C:\Windows")>
    Public Sub DirectoriesEqual_DifferentPaths_ReturnsFalse(directory1 As String, directory2 As String)

        Assert.IsFalse(PathUtil.DirectoriesEqual(directory1, directory2))

    End Sub

    ''' <summary>
    ''' Directory equality with a null argument throws (fail-fast).
    ''' </summary>
    <TestMethod>
    Public Sub DirectoriesEqual_Null_Throws()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.DirectoriesEqual(Nothing, "C:\"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.DirectoriesEqual("C:\", Nothing))

    End Sub

#End Region

#Region " Extension Comparison "

    ''' <summary>
    ''' Extension equality is case-insensitive and ignores the leading dot.
    ''' </summary>
    <TestMethod>
    <DataRow(".PY", ".py")>
    <DataRow(".PY", "PY")>
    <DataRow("py", " .Py ")>
    Public Sub ExtensionsEqual_EquivalentExtensions_ReturnsTrue(extension1 As String, extension2 As String)

        Assert.IsTrue(PathUtil.ExtensionsEqual(extension1, extension2))

    End Sub

    ''' <summary>
    ''' Different extensions are not equal.
    ''' </summary>
    <TestMethod>
    <DataRow(".PY", ".PYW")>
    <DataRow(".JS", ".JSE")>
    Public Sub ExtensionsEqual_DifferentExtensions_ReturnsFalse(extension1 As String, extension2 As String)

        Assert.IsFalse(PathUtil.ExtensionsEqual(extension1, extension2))

    End Sub

    ''' <summary>
    ''' Extension equality with a null argument throws (fail-fast).
    ''' </summary>
    <TestMethod>
    Public Sub ExtensionsEqual_Null_Throws()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.ExtensionsEqual(Nothing, ".PY"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.ExtensionsEqual(".PY", Nothing))

    End Sub

#End Region

#Region " Contains "

    ''' <summary>
    ''' Contains checks for directories use the directory comparison rules.
    ''' </summary>
    <TestMethod>
    Public Sub ContainsDirectoryEntry_UsesDirectoryComparison()

        Dim entries As String() = {"C:\Windows", "%PATHS_TESTS_DIR%\bin"}

        Assert.IsTrue(PathUtil.ContainsDirectoryEntry(entries, "c:\windows\"))
        Assert.IsTrue(PathUtil.ContainsDirectoryEntry(entries, "C:\Tools\bin"))
        Assert.IsFalse(PathUtil.ContainsDirectoryEntry(entries, "C:\Tools"))
        Assert.IsFalse(PathUtil.ContainsDirectoryEntry(Enumerable.Empty(Of String)(), "C:\Windows"))

    End Sub

    ''' <summary>
    ''' Contains checks for extensions accept the extension without the leading dot
    ''' (regression: the original code discarded the result of String.Insert).
    ''' </summary>
    <TestMethod>
    Public Sub ContainsExtensionEntry_AcceptsExtensionWithoutDot()

        Dim entries As String() = {".COM", ".EXE", ".PY"}

        Assert.IsTrue(PathUtil.ContainsExtensionEntry(entries, "PY"))
        Assert.IsTrue(PathUtil.ContainsExtensionEntry(entries, ".exe"))
        Assert.IsFalse(PathUtil.ContainsExtensionEntry(entries, ".RB"))

    End Sub

    ''' <summary>
    ''' Contains checks with null arguments throw (fail-fast).
    ''' </summary>
    <TestMethod>
    Public Sub ContainsEntry_Null_Throws()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.ContainsDirectoryEntry(Nothing, "C:\"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.ContainsDirectoryEntry({"C:\"}, Nothing))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.ContainsExtensionEntry(Nothing, ".PY"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.ContainsExtensionEntry({".PY"}, Nothing))

    End Sub

#End Region

#Region " Add "

    ''' <summary>
    ''' Adding a new directory appends it at the end, preserving the existing order.
    ''' </summary>
    <TestMethod>
    Public Sub AddDirectoryEntry_NewDirectory_IsAppended()

        Dim result As List(Of String) = PathUtil.AddDirectoryEntry({"C:\B", "C:\A"}, " C:\New ")

        CollectionAssert.AreEqual({"C:\B", "C:\A", "C:\New"}, result)

    End Sub

    ''' <summary>
    ''' Adding an existing directory (with different casing or trailing separator) does not duplicate it.
    ''' </summary>
    <TestMethod>
    <DataRow("C:\A")>
    <DataRow("c:\a")>
    <DataRow("C:\A\")>
    Public Sub AddDirectoryEntry_ExistingDirectory_IsNotDuplicated(directory As String)

        Dim result As List(Of String) = PathUtil.AddDirectoryEntry({"C:\A", "C:\B"}, directory)

        CollectionAssert.AreEqual({"C:\A", "C:\B"}, result)

    End Sub

    ''' <summary>
    ''' Adding a directory returns a new list and never modifies the source.
    ''' </summary>
    <TestMethod>
    Public Sub AddDirectoryEntry_DoesNotModifySource()

        Dim source As New List(Of String) From {"C:\A"}
        Dim result As List(Of String) = PathUtil.AddDirectoryEntry(source, "C:\B")

        Assert.AreNotSame(source, result)
        Assert.AreEqual(1, source.Count)

    End Sub

    ''' <summary>
    ''' Adding an invalid directory throws <see cref="ArgumentException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub AddDirectoryEntry_InvalidDirectory_Throws()

        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.AddDirectoryEntry({"C:\A"}, "C:\B;C:\C"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.AddDirectoryEntry(Nothing, "C:\B"))

    End Sub

    ''' <summary>
    ''' Adding an extension normalizes it (adds the leading dot).
    ''' </summary>
    <TestMethod>
    Public Sub AddExtensionEntry_NewExtension_IsNormalizedAndAppended()

        Dim result As List(Of String) = PathUtil.AddExtensionEntry({".COM", ".EXE"}, "py")

        CollectionAssert.AreEqual({".COM", ".EXE", ".py"}, result)

    End Sub

    ''' <summary>
    ''' Adding an existing extension does not duplicate it.
    ''' </summary>
    <TestMethod>
    <DataRow(".EXE")>
    <DataRow("exe")>
    Public Sub AddExtensionEntry_ExistingExtension_IsNotDuplicated(extension As String)

        Dim result As List(Of String) = PathUtil.AddExtensionEntry({".COM", ".EXE"}, extension)

        CollectionAssert.AreEqual({".COM", ".EXE"}, result)

    End Sub

    ''' <summary>
    ''' Adding an invalid extension throws.
    ''' </summary>
    <TestMethod>
    Public Sub AddExtensionEntry_InvalidExtension_Throws()

        Assert.ThrowsExactly(Of ArgumentException)(Sub() PathUtil.AddExtensionEntry({".COM"}, ".A;.B"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.AddExtensionEntry(Nothing, ".PY"))

    End Sub

#End Region

#Region " Remove "

    ''' <summary>
    ''' Removing a directory removes all its occurrences, using the directory comparison rules.
    ''' </summary>
    <TestMethod>
    Public Sub RemoveDirectoryEntry_RemovesAllOccurrences()

        Dim result As List(Of String) = PathUtil.RemoveDirectoryEntry({"C:\A", "C:\B", "c:\a\", "%PATHS_TESTS_DIR%"}, "C:\A")

        CollectionAssert.AreEqual({"C:\B", "%PATHS_TESTS_DIR%"}, result)

    End Sub

    ''' <summary>
    ''' Removing a directory by its expanded path also removes the entry that uses environment variables.
    ''' </summary>
    <TestMethod>
    Public Sub RemoveDirectoryEntry_MatchesExpandedEnvironmentVariables()

        Dim result As List(Of String) = PathUtil.RemoveDirectoryEntry({"C:\A", "%PATHS_TESTS_DIR%"}, "C:\Tools")

        CollectionAssert.AreEqual({"C:\A"}, result)

    End Sub

    ''' <summary>
    ''' Removing a directory that does not exist returns the same entries.
    ''' </summary>
    <TestMethod>
    Public Sub RemoveDirectoryEntry_NotFound_ReturnsSameEntries()

        CollectionAssert.AreEqual({"C:\A", "C:\B"}, PathUtil.RemoveDirectoryEntry({"C:\A", "C:\B"}, "C:\C"))

    End Sub

    ''' <summary>
    ''' Removing an extension accepts the extension without the leading dot.
    ''' </summary>
    <TestMethod>
    Public Sub RemoveExtensionEntry_AcceptsExtensionWithoutDot()

        Dim result As List(Of String) = PathUtil.RemoveExtensionEntry({".COM", ".PY", ".EXE", ".py"}, "PY")

        CollectionAssert.AreEqual({".COM", ".EXE"}, result)

    End Sub

    ''' <summary>
    ''' Removing with null arguments throws (fail-fast).
    ''' </summary>
    <TestMethod>
    Public Sub RemoveEntry_Null_Throws()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.RemoveDirectoryEntry(Nothing, "C:\"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.RemoveDirectoryEntry({"C:\"}, Nothing))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.RemoveExtensionEntry(Nothing, ".PY"))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.RemoveExtensionEntry({".PY"}, Nothing))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.RemoveEntryAt(Nothing, 0))

    End Sub

    ''' <summary>
    ''' Removing by zero-based index removes exactly that entry.
    ''' </summary>
    <TestMethod>
    <DataRow(0, "B;C")>
    <DataRow(1, "A;C")>
    <DataRow(2, "A;B")>
    Public Sub RemoveEntryAt_ValidIndex_RemovesThatEntry(index As Integer, expected As String)

        Assert.AreEqual(expected, PathUtil.JoinEntries(PathUtil.RemoveEntryAt({"A", "B", "C"}, index)))

    End Sub

    ''' <summary>
    ''' Removing by an out of range index throws <see cref="ArgumentOutOfRangeException"/>
    ''' (regression: a negative index was not validated).
    ''' </summary>
    <TestMethod>
    <DataRow(-1)>
    <DataRow(3)>
    <DataRow(Integer.MaxValue)>
    <DataRow(Integer.MinValue)>
    Public Sub RemoveEntryAt_OutOfRangeIndex_Throws(index As Integer)

        Assert.ThrowsExactly(Of ArgumentOutOfRangeException)(Sub() PathUtil.RemoveEntryAt({"A", "B", "C"}, index))

    End Sub

    ''' <summary>
    ''' Removing by index from an empty list throws <see cref="ArgumentOutOfRangeException"/>.
    ''' </summary>
    <TestMethod>
    Public Sub RemoveEntryAt_EmptyList_Throws()

        Assert.ThrowsExactly(Of ArgumentOutOfRangeException)(Sub() PathUtil.RemoveEntryAt(Enumerable.Empty(Of String)(), 0))

    End Sub

#End Region

#Region " Clean "

    ''' <summary>
    ''' Cleaning PATH entries preserves the original order (regression: the original code sorted the PATH alphabetically,
    ''' which changes the command resolution priority).
    ''' </summary>
    <TestMethod>
    Public Sub CleanDirectoryEntries_PreservesOrder()

        Dim entries As String() = {"C:\Z", "C:\A", "C:\M"}
        Dim result As PathUtil.CleanResult = PathUtil.CleanDirectoryEntries(entries, Function(dir As String) True)

        CollectionAssert.AreEqual(entries, result.Entries.ToArray())
        Assert.AreEqual(0, result.RemovedDuplicates.Count)
        Assert.AreEqual(0, result.RemovedMissing.Count)

    End Sub

    ''' <summary>
    ''' Cleaning PATH entries removes duplicates (keeping the first occurrence) using the directory comparison rules.
    ''' </summary>
    <TestMethod>
    Public Sub CleanDirectoryEntries_RemovesDuplicatesKeepingFirstOccurrence()

        Dim entries As String() = {"C:\A", "C:\B", "c:\a\", "%PATHS_TESTS_DIR%", "C:\Tools", "C:\B"}
        Dim result As PathUtil.CleanResult = PathUtil.CleanDirectoryEntries(entries, Function(dir As String) True)

        CollectionAssert.AreEqual({"C:\A", "C:\B", "%PATHS_TESTS_DIR%"}, result.Entries.ToArray())
        CollectionAssert.AreEqual({"c:\a\", "C:\Tools", "C:\B"}, result.RemovedDuplicates.ToArray())
        Assert.AreEqual(0, result.RemovedMissing.Count)

    End Sub

    ''' <summary>
    ''' Cleaning PATH entries removes the missing directories, checking them with the environment variables expanded
    ''' (regression: entries such as "%SystemRoot%\system32" were considered missing and deleted).
    ''' </summary>
    <TestMethod>
    Public Sub CleanDirectoryEntries_RemovesMissingDirectoriesUsingExpandedPaths()

        Dim existing As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {"C:\A", "C:\Tools"}
        Dim checkedPaths As New List(Of String)
        Dim exists As Func(Of String, Boolean) =
            Function(dir As String)
                checkedPaths.Add(dir)
                Return existing.Contains(dir)
            End Function

        Dim result As PathUtil.CleanResult =
            PathUtil.CleanDirectoryEntries({"C:\A", "C:\Missing", "%PATHS_TESTS_DIR%", """C:\A"""}, exists)

        CollectionAssert.AreEqual({"C:\A", "%PATHS_TESTS_DIR%"}, result.Entries.ToArray())
        CollectionAssert.AreEqual({"C:\Missing"}, result.RemovedMissing.ToArray())
        CollectionAssert.AreEqual({"""C:\A"""}, result.RemovedDuplicates.ToArray())
        CollectionAssert.Contains(checkedPaths, "C:\Tools")
        CollectionAssert.DoesNotContain(checkedPaths, "%PATHS_TESTS_DIR%")

    End Sub

    ''' <summary>
    ''' Duplicated entries are reported as duplicates even if the directory is missing, and the existence check is not repeated.
    ''' </summary>
    <TestMethod>
    Public Sub CleanDirectoryEntries_DuplicateOfMissing_IsReportedAsDuplicate()

        Dim calls As Integer = 0
        Dim result As PathUtil.CleanResult =
            PathUtil.CleanDirectoryEntries({"C:\Missing", "C:\MISSING"},
                                           Function(dir As String)
                                               calls += 1
                                               Return False
                                           End Function)

        Assert.AreEqual(0, result.Entries.Count)
        CollectionAssert.AreEqual({"C:\Missing"}, result.RemovedMissing.ToArray())
        CollectionAssert.AreEqual({"C:\MISSING"}, result.RemovedDuplicates.ToArray())
        Assert.AreEqual(1, calls)

    End Sub

    ''' <summary>
    ''' Cleaning PATH entries with null arguments throws (fail-fast).
    ''' </summary>
    <TestMethod>
    Public Sub CleanDirectoryEntries_Null_Throws()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.CleanDirectoryEntries(Nothing, Function(dir As String) True))
        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.CleanDirectoryEntries({"C:\"}, Nothing))

    End Sub

    ''' <summary>
    ''' Cleaning PATHEXT entries removes duplicates (case-insensitive) and preserves the order
    ''' (regression: the original code sorted PATHEXT alphabetically, which changes the extension priority).
    ''' </summary>
    <TestMethod>
    Public Sub CleanExtensionEntries_RemovesDuplicatesAndPreservesOrder()

        Dim result As PathUtil.CleanResult =
            PathUtil.CleanExtensionEntries({".EXE", ".COM", ".exe", ".BAT", "COM"})

        CollectionAssert.AreEqual({".EXE", ".COM", ".BAT"}, result.Entries.ToArray())
        CollectionAssert.AreEqual({".exe", "COM"}, result.RemovedDuplicates.ToArray())
        Assert.AreEqual(0, result.RemovedMissing.Count)

    End Sub

    ''' <summary>
    ''' Cleaning null PATHEXT entries throws (fail-fast).
    ''' </summary>
    <TestMethod>
    Public Sub CleanExtensionEntries_Null_Throws()

        Assert.ThrowsExactly(Of ArgumentNullException)(Sub() PathUtil.CleanExtensionEntries(Nothing))

    End Sub

#End Region

#Region " ResolveValueKind "

    ''' <summary>
    ''' A new value is written as REG_EXPAND_SZ (the Windows default kind for PATH).
    ''' </summary>
    <TestMethod>
    Public Sub ResolveValueKind_MissingValue_ReturnsExpandString()

        Assert.AreEqual(RegistryValueKind.ExpandString, PathUtil.ResolveValueKind(RegistryValueKind.None, "C:\A"))

    End Sub

    ''' <summary>
    ''' An existing REG_EXPAND_SZ value keeps its kind.
    ''' </summary>
    <TestMethod>
    Public Sub ResolveValueKind_ExistingExpandString_IsPreserved()

        Assert.AreEqual(RegistryValueKind.ExpandString, PathUtil.ResolveValueKind(RegistryValueKind.ExpandString, "C:\A"))

    End Sub

    ''' <summary>
    ''' An existing REG_SZ value keeps its kind when the data has no environment variables.
    ''' </summary>
    <TestMethod>
    Public Sub ResolveValueKind_ExistingStringWithoutVariables_IsPreserved()

        Assert.AreEqual(RegistryValueKind.String, PathUtil.ResolveValueKind(RegistryValueKind.String, "C:\A;C:\B"))
        Assert.AreEqual(RegistryValueKind.String, PathUtil.ResolveValueKind(RegistryValueKind.String, Nothing))

    End Sub

    ''' <summary>
    ''' An existing REG_SZ value is promoted to REG_EXPAND_SZ when the data has environment variables,
    ''' otherwise the variables would never be expanded by Windows.
    ''' </summary>
    <TestMethod>
    Public Sub ResolveValueKind_ExistingStringWithVariables_ReturnsExpandString()

        Assert.AreEqual(RegistryValueKind.ExpandString, PathUtil.ResolveValueKind(RegistryValueKind.String, "%SystemRoot%;C:\A"))

    End Sub

    ''' <summary>
    ''' Any other unexpected kind is replaced by REG_EXPAND_SZ.
    ''' </summary>
    <TestMethod>
    Public Sub ResolveValueKind_OtherKind_ReturnsExpandString()

        Assert.AreEqual(RegistryValueKind.ExpandString, PathUtil.ResolveValueKind(RegistryValueKind.MultiString, "C:\A"))
        Assert.AreEqual(RegistryValueKind.ExpandString, PathUtil.ResolveValueKind(RegistryValueKind.Binary, "C:\A"))

    End Sub

#End Region

End Class

#End Region
