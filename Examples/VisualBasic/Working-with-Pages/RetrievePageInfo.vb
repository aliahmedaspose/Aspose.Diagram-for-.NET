Imports Microsoft.VisualBasic
Imports System.IO
Imports Aspose.Diagram
Imports System

Namespace Diagrams
    Public Class RetrievePageInfo
        Public Shared Sub Run()
            ' ExStart:RetrievePageInfo
            ' The path to the documents directory.
            Dim dataDir As String = RunExamples.GetDataDir_VisioPages()

            ' Call the diagram constructor to load diagram from a VDX file
            Dim vdxDiagram As New Diagram(dataDir & "RetrievePageInfo.vdx")

            For Each page As Aspose.Diagram.Page In vdxDiagram.Pages
                ' Checks if current page is a background page
                If page.Background = Aspose.Diagram.BOOL.True Then
                    ' Display information about the background page
                    Console.WriteLine("Background Page ID : " & page.ID)
                    Console.WriteLine("Background Page Name : " & page.Name)
                Else
                    ' Display information about the foreground page
                    Console.WriteLine(Constants.vbLf & "Page ID : " & page.ID)
                    Console.WriteLine("Universal Name : " & page.NameU)
                    Console.WriteLine("ID of the Background Page : " & page.BackPage.ToString())
                End If
            Next page
            ' ExEnd:RetrievePageInfo
        End Sub
    End Class
End Namespace