﻿Imports Aspose.Diagram
Imports System
Public Class ReadUserdefinedCellsOfShape
    Public Shared Sub Run()
        ' ExStart:ReadUserdefinedCellsOfShape
        ' The path to the documents directory.
        Dim dataDir As String = RunExamples.GetDataDir_UserDefinedCells()

        ' Load source Visio diagram
        Dim diagram As New Diagram(dataDir & Convert.ToString("Drawing1.vsdx"))
        ' Get page by name
        Dim page As Page = diagram.Pages.GetPage("Page-1")
        ' Get shape by id
        Dim shape As Shape = page.Shapes.GetShape(1)
        ' Extract user defined cells of the shape
        For Each user As User In shape.Users
            Console.WriteLine(user.Name + ": " + user.Value.Val)
        Next
        ' ExEnd:ReadUserdefinedCellsOfShape
    End Sub
End Class
