Imports System.Threading
Imports System.Timers
Imports VIETBAIT.WORKLIST.HIS
Imports VIETBAIT.WORKLIST.WorkList


Public Class VietBaWorkList
    Dim sThreadReading As Thread
    Dim cls As New MyTcpListener

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Protected Overrides Sub OnStart(ByVal args() As String)
        'Thread.Sleep(10000)
        'Dim applicationName As String = "vietbaworklist"

        'Dim checker As New VIETBAIT.LicenseChecker.checker()
        'checker.ApplicationName = applicationName
        'If checker.CheckLicense() Then
        'Else
        '    Thread.CurrentThread.Abort()
        'End If
        Me.Timer.Interval = 5000
        Me.Timer.Start()

    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        Me.Timer.Stop()
    End Sub

    Private Sub Timer_Elapsed (ByVal sender As Object, ByVal e As ElapsedEventArgs) Handles Timer.Elapsed
        If Not iStart Then
            Timer.Enabled = False
            Try
                InitCommunication()
                iStart = True
            Catch ex As Exception

            End Try
            Me.Timer.Interval = 1000000
            Timer.Enabled = True
        End If
        If iStart Then
            Me.Timer.Interval = 1000000
            cls.FMain()
            Me.Timer.Interval = 100
        End If
    End Sub

    Public Sub resettimer()
        Me.Timer.Interval = 1
    End Sub
End Class
