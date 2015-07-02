Public Class frm_CheckingKey

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
     

    End Sub
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

                Select Case AppType.ToUpper
                    Case "DROC"
                        ChangeText(Label4, "DROC")
                    Case "DICOMVIEWER"
                        ChangeText(Label4, "DICOMViewer")
                    Case "RISLINK"
                        ChangeText(Label4, "RISLink")
                    Case "GOLFMAN"
                        ChangeText(Label4, "GOLFMan")
                    Case "HIS"
                        ChangeText(Label4, "HISLink")
                    Case Else
                        ChangeText(Label4, "LABLink")
                End Select
                ChangeText(Label2, "Hệ thống đang kiểm tra giấy phép sử dụng phần mềm " & AppType.ToUpper & ". Xin vui lòng chờ đợi...")
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
        
    Private Sub frm_CheckingKey_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Application.DoEvents()
        _ar = BeginInvoke(New StartupCallback(AddressOf Startup))
    End Sub

   
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class