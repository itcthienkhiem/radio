Public Class frm_CheckingKey

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        progressBar1.Minimum = 0
        progressBar1.Maximum = 500
        progressBar1.Step = 50
        progressBar1.Value = 0
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public AppType As String = ""
    Private _ar As IAsyncResult
    Private Sub Startup()
        EndInvoke(_ar)
        Application.DoEvents()
        Try
            Do
                Application.DoEvents()
                progressBar1.Value += 50

                Select Case AppType.ToUpper
                    Case "DICOMVIEWER"
                        Label4.Text = "DICOMViewer"
                    Case "RISLINK"
                        Label4.Text = "RISLink"
                    Case "GOLFMAN"
                        Label4.Text = "GOLFMan"
                    Case "HIS"
                        Label4.Text = "HIS"
                    Case Else
                        Label4.Text = "LABLink"
                End Select
                Label2.Text = "Hệ thống đang xác thực khóa ứng dụng. Xin vui lòng chờ đợi.."
                Application.DoEvents()
            Loop Until Utils.bHasFound
            Close()
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Delegate Sub StartupCallback()
        
    Private Sub frm_CheckingKey_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Application.DoEvents()
        _ar = BeginInvoke(New StartupCallback(AddressOf Startup))
    End Sub

   
End Class