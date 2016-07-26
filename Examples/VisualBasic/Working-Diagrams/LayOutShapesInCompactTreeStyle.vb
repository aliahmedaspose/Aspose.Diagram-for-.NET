Imports Microsoft.VisualBasic
Imports System.IO
Imports Aspose.Diagram
Imports Aspose.Diagram.AutoLayout

Namespace Diagrams
    Public Class LayOutShapesInCompactTreeStyle
        Public Shared Sub Run()
            ' ExStart:LayOutShapesInCompactTreeStyle
            ' The path to the documents directory.
            Dim dataDir As String = RunExamples.GetDataDir_Diagrams()

            Dim fileName As String = "LayOutShapesInCompactTreeStyle.vdx"
            Dim diagram As New Diagram(dataDir & fileName)

            Dim compactTreeOptions As New LayoutOptions()
            compactTreeOptions.LayoutStyle = LayoutStyle.CompactTree
            compactTreeOptions.EnlargePage = True

            compactTreeOptions.Direction = LayoutDirection.DownThenRight
            diagram.Layout(compactTreeOptions)
            diagram.Save(dataDir & "sample_down_right.vdx", SaveFileFormat.VDX)

            diagram = New Diagram(dataDir & fileName)
            compactTreeOptions.Direction = LayoutDirection.DownThenLeft
            diagram.Layout(compactTreeOptions)
            diagram.Save(dataDir & "sample_down_left.vdx", SaveFileFormat.VDX)

            diagram = New Diagram(dataDir & fileName)
            compactTreeOptions.Direction = LayoutDirection.RightThenDown
            diagram.Layout(compactTreeOptions)
            diagram.Save(dataDir & "sample_right_down.vdx", SaveFileFormat.VDX)

            diagram = New Diagram(dataDir & fileName)
            compactTreeOptions.Direction = LayoutDirection.LeftThenDown
            diagram.Layout(compactTreeOptions)
            diagram.Save(dataDir & "sample_left_down.vdx", SaveFileFormat.VDX)
            ' ExEnd:LayOutShapesInCompactTreeStyle
        End Sub
    End Class
End Namespace