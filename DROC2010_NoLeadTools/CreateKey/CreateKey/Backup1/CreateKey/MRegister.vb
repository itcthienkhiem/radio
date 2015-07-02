Imports System.IO

Public Class MRegKey
    Public mv_bCancel As Boolean = True
    Public mv_sKey As String = ""
    Public mv_sGenKey As String
    Public hasNotice As Boolean = False
    Private TempKey As String = ""
    Public _AppType As String = ""
    Public pwd As String = ""
    Public loopNum As Integer = 1
    Public expd As String
    Dim sv_oEncrypt As New ect.Encrypt("Rijndael")
    Private Sub MRegKey_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If cmdNext.Visible = True Then
            hasNotice = False

        Else
            If mv_bCancel AndAlso Not hasNotice Then
                MessageBox.Show("Thank for your trial using!" & vbCrLf & "For more information, please contact via email: vnmedilab@gmail.com ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

    End Sub
    Private Sub MRegKey_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
        If e.KeyCode = Keys.Enter Then ProcessTabKey(True)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.P Then
            If txtKey.PasswordChar = Convert.ToChar("*") Then
                txtKey.PasswordChar = Nothing
            Else
                txtKey.PasswordChar = Convert.ToChar("*")
            End If
        End If
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.textBoxDescription.Text = "This feature only displays when your software is not registered or expired" & System.Environment.NewLine & "for having license, please following the below steps:" & System.Environment.NewLine & "- Step1 :Click the link Send customer information.After that send this information to the us." & System.Environment.NewLine & "- Step 2:Get license from us in the file (License.gps). You can select the file by clicking at the button Open" & System.Environment.NewLine & "- Step3 3:Click Register to finish. " & System.Environment.NewLine & "Thanks for your care about our product. " & vbCrLf & "Email: vnmedilab@gmail.com"
            hasNotice = False
            'lnkGenKey.Text = "Gửi thông tin người sử dụng:" & mv_sGenKey & "-" & pwd
            lnkGenKey.Text = "Send customer information"
            txtKey.Focus()
            'Me.Text = "Đăng ký khóa cho:" & _AppType
            'If _AppType.Trim.ToUpper = "HIS" OrElse _AppType.Trim.ToUpper = "GOLFMAN" OrElse _AppType.Trim.ToUpper = "DICOMVIEWER" OrElse _AppType.Trim.ToUpper = "LABLINK" OrElse _AppType.Trim.ToUpper = "RISLINK" OrElse _AppType.Trim.ToUpper = "DROC" Then
            'Else
            '    MessageBox.Show("Mã ứng dụng bạn truyền đã bị sai. Liên hệ với người tạo ra nó để biết thêm chi tiết" & vbCrLf & "Tác giả: Đào Văn Cường. Điện thoại: 09 15 15 01 48" & vbCrLf & "Rất cảm ơn bạn đã sử dụng chương trình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    hasNotice = True
            '    Me.Close()
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmdRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRegister.Click
        Try
            'If txtExpDate.Text.Trim = "" Then
            '    MessageBox.Show("Invalid license. Please check again or you can contact us via: vnmedilab@gmail.com for more supports", "warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    txtKey.Focus()
            '    Return
            'End If
            If txtKey.Text.Trim.Equals(mv_sKey) Then
                mv_bCancel = False
                expd = txtExpDate.Text.Trim
                pnlReg.Visible = False
                cmdRegister.Visible = False
                cmdClose.Visible = False
                cmdNext.Visible = True
                Me.AcceptButton = cmdNext
                lblRegSuccess.Visible = True
                lblRegSuccess.Text = "Registered successfully. Click continue to start using system"
                'MessageBox.Show("Chúc mừng bạn đã đăng ký thành công. Hãy nhấn nút OK để bắt đầu vào chương trình" & vbCrLf & "Mọi vướng mắc trong quá trình sử dụng, bạn có thể gửi email cho chúng tôi để được giải đáp." & vbCrLf & "Xin trân trọng cám ơn!" & vbCrLf & "Email: vnmedilab@gmail.com", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'RegConfiguration.SaveSettings("VBITJSC", "DICOM", "FIRSTIME", "1")
                hasNotice = True
                'Me.Close()
            Else
                MessageBox.Show("Invalid license. Please check again or you can contact us via: vnmedilab@gmail.com for more supports", "warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtKey.Focus()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        If cmdNext.Visible = True Then
            hasNotice = False
            Me.Close()
        Else
            MessageBox.Show("For more information, please contact via email: vnmedilab@gmail.com ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            hasNotice = True
            Me.Close()
        End If
      
    End Sub

    Private Sub lnkGenKey_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkGenKey.LinkClicked
        Try
            Clipboard.SetData("A".GetType.ToString, mv_sGenKey)
            'MessageBox.Show("Giá trị sinh khóa kích hoạt phần mềm: " & mv_sGenKey & "-" & pwd & " đã được copy vào Clipboard của máy tính. Bạn hãy mở file word và dán giá trị này bằng cách sử dụng tổ hợp phím Ctrl+V. Sau đó hãy gửi tới nhà cung cấp để có khóa kích hoạt." & vbCrLf & "Email: vnmedilab@gmail.com" & vbCrLf & "Rất cảm ơn bạn đã sử dụng chương trình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            MessageBox.Show("Open notepad or microsoftword, then use Ctrl+V to paste customer information. Give this information to us. We will give you license as soon as possible. Thanks", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception

        End Try
    End Sub


    Private Sub txtKey_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKey.KeyDown
        If e.KeyCode = Keys.Enter AndAlso txtKey.PasswordChar = Convert.ToChar("*") AndAlso txtKey.Text.Trim.ToUpper = sv_oEncrypt.Fam_PWD Then
            MessageBox.Show("Your activate key is:" + txtKey.Text)
            cmdRegister.PerformClick()
        End If
    End Sub
    Dim blnRaiseEvent = True
    Private Sub txtKey_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKey.TextChanged
        Try
            If Not blnRaiseEvent Then Return
            TempKey = mv_sGenKey
            If txtKey.PasswordChar = Convert.ToChar("*") AndAlso txtKey.Text.Trim.ToUpper = sv_oEncrypt.Fam_PWD Then

                sv_oEncrypt.sPwd = pwd
                For i As Integer = 1 To loopNum
                    TempKey = sv_oEncrypt.Mahoa(TempKey).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
                Next
                If TempKey.Length > 150 Then
                    TempKey = TempKey.Substring(0, 150)
                End If
                TempKey = TempKey.ToUpper
                txtKey.Text = TempKey
                txtKey.PasswordChar = Nothing
                cmdRegister.Focus()
            Else
                'If txtKey.Text.Trim.Split("-").Length > 1 Then
                '    blnRaiseEvent = False
                '    txtExpDate.Text = txtKey.Text.Trim.Replace(txtKey.Text.Trim.Split("-")(0) + "-", "").Trim
                '    txtKey.Text = txtKey.Text.Trim.Split("-")(0)
                'Else
                '    txtExpDate.Text = ""
                'End If
            End If
        Catch ex As Exception
        Finally


        End Try


    End Sub

    Private Sub cmdBrowseLicfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseLicfile.Click
        Dim _OpenFileDialog As New OpenFileDialog
        _OpenFileDialog.Filter = "License file|*.lic"
        _OpenFileDialog.Multiselect = False
        If _OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Using reader As New StreamReader(_OpenFileDialog.FileName)
                Dim obj As Object = reader.ReadToEnd
                If Not obj Is Nothing Then
                    txtKey.Text = obj.ToString.Trim
                End If
            End Using
        End If
    End Sub

    Private Sub cmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        hasNotice = True
        Me.Close()
    End Sub

    Private Sub cmdClose2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose2.Click
        cmdClose_Click(cmdClose, e)
    End Sub
End Class
