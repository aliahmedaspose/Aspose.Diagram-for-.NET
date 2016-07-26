﻿Imports Aspose.Diagram
Imports System
Imports System.IO
Imports System.ComponentModel
Imports System.Runtime.InteropServices

''' <summary>
''' A utility class that converts a document to XPS using Aspose.Diagram and then sends to the XpsPrint API.
''' </summary>
Public Class XpsPrintHelper
    ''' <summary>
    ''' No ctor.
    ''' </summary>
    Private Sub New()
    End Sub

    ' ExStart:XpsPrint_PrintDocument
    ''' <summary>
    ''' Sends an Aspose.Diagram document to a printer using the XpsPrint API.
    ''' </summary>
    ''' <param name="diagram"></param>
    ''' <param name="printerName"></param>
    ''' <param name="jobName">Job name. Can be null.</param>
    ''' <param name="isWait">True to wait for the job to complete. False to return immediately after submitting the job.</param>
    ''' <exception cref="Exception">Thrown if any error occurs.</exception>
    Public Shared Sub Print(diagram As Diagram, printerName As String, jobName As String, isWait As Boolean)
        If diagram Is Nothing Then
            Throw New ArgumentNullException("document")
        End If

        ' Use Aspose.Diagram to convert the document to XPS and store in a memory stream.
        Dim stream As New MemoryStream()
        diagram.Save(stream, SaveFileFormat.XPS)
        stream.Position = 0

        Print(stream, printerName, jobName, isWait)
    End Sub
    ' ExEnd:XpsPrint_PrintDocument

    ' ExStart:XpsPrint_PrintStream
    ''' <summary>
    ''' Sends a stream that contains a document in the XPS format to a printer using the XpsPrint API.
    ''' Has no dependency on Aspose.Diagram, can be used in any project.
    ''' </summary>
    ''' <param name="stream"></param>
    ''' <param name="printerName"></param>
    ''' <param name="jobName">Job name. Can be null.</param>
    ''' <param name="isWait">True to wait for the job to complete. False to return immediately after submitting the job.</param>
    ''' <exception cref="Exception">Thrown if any error occurs.</exception>
    Public Shared Sub Print(stream As Stream, printerName As String, jobName As String, isWait As Boolean)
        If stream Is Nothing Then
            Throw New ArgumentNullException("stream")
        End If
        If printerName Is Nothing Then
            Throw New ArgumentNullException("printerName")
        End If

        ' Create an event that we will wait on until the job is complete.
        Dim completionEvent As IntPtr = CreateEvent(IntPtr.Zero, True, False, Nothing)
        If completionEvent = IntPtr.Zero Then
            Throw New Win32Exception()
        End If

        Try
            Dim job As IXpsPrintJob
            Dim jobStream As IXpsPrintJobStream
            job = Nothing
            jobStream = Nothing

            StartJob(printerName, jobName, completionEvent, job, jobStream)

            CopyJob(stream, job, jobStream)

            If isWait Then
                WaitForJob(completionEvent)
                CheckJobStatus(job)
            End If
        Finally
            If completionEvent <> IntPtr.Zero Then
                CloseHandle(completionEvent)
            End If
        End Try
    End Sub
    ' ExEnd:XpsPrint_PrintStream

    Private Shared Sub StartJob(printerName As String, jobName As String, completionEvent As IntPtr, ByRef job As IXpsPrintJob, ByRef jobStream As IXpsPrintJobStream)
        Dim result As Integer = StartXpsPrintJob(printerName, jobName, Nothing, IntPtr.Zero, completionEvent, Nothing, _
            0, job, jobStream, IntPtr.Zero)
        If result <> 0 Then
            Throw New Win32Exception(result)
        End If
    End Sub

    Private Shared Sub CopyJob(stream As Stream, job As IXpsPrintJob, jobStream As IXpsPrintJobStream)
        Try
            Dim buff As Byte() = New Byte(4095) {}
            While True
                Dim read As UInteger = CUInt(stream.Read(buff, 0, buff.Length))
                If read = 0 Then
                    Exit While
                End If

                Dim written As UInteger
                jobStream.Write(buff, read, written)

                If read <> written Then
                    Throw New Exception("Failed to copy data to the print job stream.")
                End If
            End While

            ' Indicate that the entire document has been copied.
            jobStream.Close()
        Catch generatedExceptionName As Exception
            ' Cancel the job if we had any trouble submitting it.
            job.Cancel()
            Throw
        End Try
    End Sub

    Private Shared Sub WaitForJob(completionEvent As IntPtr)
        Const INFINITE As Integer = -1
        Select Case WaitForSingleObject(completionEvent, INFINITE)
            Case WAIT_RESULT.WAIT_OBJECT_0
                ' Expected result, do nothing.
                Exit Select
            Case WAIT_RESULT.WAIT_FAILED
                Throw New Win32Exception()
            Case Else
                Throw New Exception("Unexpected result when waiting for the print job.")
        End Select
    End Sub

    Private Shared Sub CheckJobStatus(job As IXpsPrintJob)
        Dim jobStatus As XPS_JOB_STATUS
        job.GetJobStatus(jobStatus)
        Select Case jobStatus.completion
            Case XPS_JOB_COMPLETION.XPS_JOB_COMPLETED
                ' Expected result, do nothing.
                Exit Select
            Case XPS_JOB_COMPLETION.XPS_JOB_FAILED
                Throw New Win32Exception(jobStatus.jobStatus)
            Case Else
                Throw New Exception("Unexpected print job status.")
        End Select
    End Sub

    ' HANDLE
    ' HANDLE
    <DllImport("XpsPrint.dll", EntryPoint:="StartXpsPrintJob")> _
    Private Shared Function StartXpsPrintJob(<MarshalAs(UnmanagedType.LPWStr)> printerName As String, <MarshalAs(UnmanagedType.LPWStr)> jobName As String, <MarshalAs(UnmanagedType.LPWStr)> outputFileName As String, progressEvent As IntPtr, completionEvent As IntPtr, <MarshalAs(UnmanagedType.LPArray)> printablePagesOn As Byte(), _
        printablePagesOnCount As UInt32, ByRef xpsPrintJob As IXpsPrintJob, ByRef documentStream As IXpsPrintJobStream, printTicketStream As IntPtr) As Integer
    End Function
    ' This is actually "out IXpsPrintJobStream", but we don' T use it and just want to pass null, hence IntPtr.
    <DllImport("Kernel32.dll", SetLastError:=True)> _
    Private Shared Function CreateEvent(lpEventAttributes As IntPtr, bManualReset As Boolean, bInitialState As Boolean, lpName As String) As IntPtr
    End Function

    Private Declare Auto Function WaitForSingleObject Lib "Kernel32.dll" (handle As IntPtr, milliseconds As Int32) As WAIT_RESULT

    <DllImport("Kernel32.dll", SetLastError:=True)> _
    Private Shared Function CloseHandle(hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
End Class
''' <summary>
''' This interface definition is HACKED.
''' 
''' It appears that the IID for IXpsPrintJobStream specified in XpsPrint.h as 
''' MIDL_INTERFACE("7a77dc5f-45d6-4dff-9307-d8cb846347ca") is not correct and the RCW cannot return it.
''' But the returned object returns the parent ISequentialStream inteface successfully.
''' 
''' So the hack is that we obtain the ISequentialStream interface but work with it as 
''' with the IXpsPrintJobStream interface. 
''' </summary>
' This is IID of ISequenatialSteam.
<Guid("0C733A30-2A1C-11CE-ADE5-00AA0044773D")> _
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
Interface IXpsPrintJobStream
    ' ISequentualStream methods.
    Sub Read(<MarshalAs(UnmanagedType.LPArray)> pv As Byte(), cb As UInteger, ByRef pcbRead As UInteger)
    Sub Write(<MarshalAs(UnmanagedType.LPArray)> pv As Byte(), cb As UInteger, ByRef pcbWritten As UInteger)
    ' IXpsPrintJobStream methods.
    Sub Close()
End Interface

<Guid("5ab89b06-8194-425f-ab3b-d7a96e350161")> _
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
Interface IXpsPrintJob
    Sub Cancel()
    Sub GetJobStatus(ByRef jobStatus As XPS_JOB_STATUS)
End Interface

<StructLayout(LayoutKind.Sequential)> _
Structure XPS_JOB_STATUS
    Public jobId As UInt32
    Public currentDocument As Int32
    Public currentPage As Int32
    Public currentPageTotal As Int32
    Public completion As XPS_JOB_COMPLETION
    Public jobStatus As Int32
    ' UInt32
End Structure

Enum XPS_JOB_COMPLETION
    XPS_JOB_IN_PROGRESS = 0
    XPS_JOB_COMPLETED = 1
    XPS_JOB_CANCELLED = 2
    XPS_JOB_FAILED = 3
End Enum

Enum WAIT_RESULT
    WAIT_OBJECT_0 = 0
    WAIT_ABANDONED = &H80
    WAIT_TIMEOUT = &H102
    WAIT_FAILED = -1
    ' 0xFFFFFFFF
End Enum
