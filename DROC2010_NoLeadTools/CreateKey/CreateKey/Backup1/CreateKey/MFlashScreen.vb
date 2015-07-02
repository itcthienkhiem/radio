Public Class MFlashScreen

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()


    End Sub
    Public ReadOnly Property NewPwd4Hrk() As String
        Get
            Return lblProperty.Tag.ToString
        End Get

    End Property

    Delegate Sub ACT_ChangeText(ByVal lbl As Label, ByVal svalue As String)
    Public AppType As String = ""

    Private _ar As IAsyncResult
    Private Sub Startup()
        EndInvoke(_ar)
        Application.DoEvents()
        Try
            Do
                Application.DoEvents()
                'progressBar1.Value += 50
                Dim appName As String = ""
                Select Case AppType.ToUpper
                    Case "DICOMVIEWER"
                        ChangeText(Label4, "DICOMVIEWER")
                        appName = "DICOMVIEWER"
                    Case "XVIEW"
                        appName = "XVIEW"
                        ChangeText(Label4, "XVIEW")
                    Case "EFILM"
                        appName = "EFILM"
                        ChangeText(Label4, "EFILM")
                    Case Else
                        ChangeText(Label4, "UNKNOWN")
                End Select
                ChangeText(Label2, "We are checking your license of " & appName.ToUpper & ". Please wait for a few seconds...")
                Application.DoEvents()
            Loop Until Utils.bHasFound
            Close()
        Catch ex As Exception
        Finally
        End Try
    End Sub
    Sub ChangeText(ByVal lbl As Label, ByVal sVal As String)
        If Not lbl.InvokeRequired Then
            lbl.Text = sVal
        Else
            lbl.BeginInvoke(New ACT_ChangeText(AddressOf ChangeText), New Object() {lbl, sVal})
        End If
    End Sub
    Private Delegate Sub StartupCallback()

    Private Sub MFlashScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Application.DoEvents()
        _ar = BeginInvoke(New StartupCallback(AddressOf Startup))
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class