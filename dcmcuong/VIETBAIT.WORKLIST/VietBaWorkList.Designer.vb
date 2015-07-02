Imports System.IO
Imports System

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VietBaWorkList
    Inherits System.ServiceProcess.ServiceBase

    'UserService overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' The main entry point for the process
    <MTAThread()> _
    <System.Diagnostics.DebuggerNonUserCode()> _
    Shared Sub Main()

        'Try
        '    If checker.CheckLicense Then
        '    Else
        '        Threading.Thread.CurrentThread.Abort()
        '    End If
        'Catch ex As Exception
        '    Threading.Thread.CurrentThread.Abort()
        'End Try


        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ' More than one NT Service may run within the same process. To add
        ' another service to this process, change the following line to
        ' create a second service object. For example,
        '
        '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        '

        Dim pc As Process = Process.GetCurrentProcess()
        'Directory.SetCurrentDirectory(pc.MainModule.FileName.Substring(0, pc.MainModule.FileName.LastIndexOf("\")))
        Dim _
            StartupPath As String = _
                pc.MainModule.FileName.Substring(0, pc.MainModule.FileName.LastIndexOf(Path.DirectorySeparatorChar))
        Directory.SetCurrentDirectory(StartupPath)

        ServicesToRun = New System.ServiceProcess.ServiceBase() {New VietBaWorkList}

        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Timer = New System.Timers.Timer
        CType(Me.Timer, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Timer
        '
        Me.Timer.Interval = 1000
        '
        'VietBaWorkList
        '
        Me.ServiceName = "VietBa Dicom Worklist"
        CType(Me.Timer, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Timer As System.Timers.Timer

End Class
