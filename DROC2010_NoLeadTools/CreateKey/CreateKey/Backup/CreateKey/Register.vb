Public Class RegKey
    Public mv_bCancel As Boolean = True
    Public mv_sKey As String = ""
    Public mv_sGenKey As String
    Public hasNotice As Boolean = False
    Private TempKey As String = ""
    Public _AppType As String = ""
    Public pwd As String = ""
    Private Sub RegKey_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If mv_bCancel AndAlso Not hasNotice Then
            MessageBox.Show("Bạn hãy liên hệ với nhà cung cấp để có khóa kích hoạt." & vbCrLf & "Tác giả: Đào Văn Cường. Điện thoại: 0904 648006" & vbCrLf & "Nguyễn Đức Hùng:0904110280" & vbCrLf & "Rất cảm ơn bạn đã sử dụng chương trình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
        
    End Sub
    Private Sub RegKey_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        hasNotice = False
        lnkGenKey.Text = "Giá trị sinh khóa:" & mv_sGenKey
        txtKey.Focus()
        Me.Text = "Đăng ký khóa cho:" & _AppType
        If _AppType.Trim.ToUpper = "HIS" OrElse _AppType.Trim.ToUpper = "GOLFMAN" OrElse _AppType.Trim.ToUpper = "DICOMVIEWER" OrElse _AppType.Trim.ToUpper = "LABLINK" OrElse _AppType.Trim.ToUpper = "RISLINK" Then
        Else
            MessageBox.Show("Mã ứng dụng bạn truyền đã bị sai. Liên hệ với người tạo ra nó để biết thêm chi tiết" & vbCrLf & "Tác giả: Đào Văn Cường. Điện thoại: 0904 648006" & vbCrLf & "Nguyễn Đức Hùng:0904110280" & vbCrLf & "Rất cảm ơn bạn đã sử dụng chương trình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            hasNotice = True
            Me.Close()
        End If
    End Sub

    Private Sub cmdRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRegister.Click
        If txtKey.Text.Trim.Equals(mv_sKey) Then
            mv_bCancel = False
            Dim reg As New clsRegistry.clsRegistry
            If _AppType.Trim.ToUpper = "DICOMVIEWER" OrElse _AppType.Trim.ToUpper = "GOLFMAN" OrElse _AppType.Trim.ToUpper = "HIS" Then
                SaveSetting("VietBaJSC", "DICOM", "REGKEY", txtKey.Text.Trim)
            ElseIf _AppType.Trim.ToUpper = "LABLINK" Then
                SaveSetting("VietBaJSC", "LABLINK", "REGKEY", txtKey.Text.Trim)
            Else
                SaveSetting("VietBaJSC", "RISLINK", "REGKEY", txtKey.Text.Trim)
            End If
            MessageBox.Show("Chúc mừng bạn đã đăng ký thành công. Hãy nhấn OK để bắt đầu vào chương trình" & vbCrLf & "VietBaIT hy vọng bạn sẽ thỏa mãn khi dùng sản phẩm này!" & vbCrLf & "Mọi vướng mắc trong quá trình sử dụng, bạn có thể gửi cho chúng tôi để được giải đáp." & vbCrLf & "Xin trân trọng cám ơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            RegConfiguration.SaveSettings("VBITJSC", "DICOM", "FIRSTIME", "1")
            hasNotice = True
            Me.Close()
        Else
            MessageBox.Show("Khóa bạn nhập không đúng. Xin hãy nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtKey.Focus()
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        MessageBox.Show("Bạn hãy liên hệ với nhà cung cấp để có khóa kích hoạt." & vbCrLf & "Tác giả: Đào Văn Cường. Điện thoại: 0904 648006" & vbCrLf & "Nguyễn Đức Hùng:0904110280" & vbCrLf & "Rất cảm ơn bạn đã sử dụng chương trình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        hasNotice = True
        Me.Close()
    End Sub

    Private Sub lnkGenKey_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkGenKey.LinkClicked
        Clipboard.SetData("A".GetType.ToString, mv_sGenKey)
        MessageBox.Show("Giá trị sinh khóa:" & mv_sGenKey & " đã được copy vào Clipboard của máy tính. Bạn hãy mở file word và dán giá trị này bằng cách sử dụng tổ hợp phím Ctrl+V. Sau đó hãy gửi tới nhà cung cấp để có khóa kích hoạt." & vbCrLf & "Tác giả: Đào Văn Cường. Điện thoại: 0904 648006" & vbCrLf & "Nguyễn Đức Hùng:0904110280" & vbCrLf & "Rất cảm ơn bạn đã sử dụng chương trình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub txtKey_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKey.KeyDown
        If e.KeyCode = Keys.Enter AndAlso txtKey.PasswordChar = Convert.ToChar("*") AndAlso txtKey.Text.Trim.ToUpper = "DVCNNQ01111983150181" Then
            MessageBox.Show("Khóa kích hoạt là:" + txtKey.Text)
            cmdRegister.PerformClick()
        End If
    End Sub

    Private Sub txtKey_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKey.TextChanged
        TempKey = ""
        If txtKey.PasswordChar = Convert.ToChar("*") AndAlso txtKey.Text.Trim.ToUpper = "DVCNNQ01111983150181" Then
            Dim sv_oEncrypt As New VietBaIT.Encrypt("Rijndael")
            TempKey = sv_oEncrypt.Mahoa(mv_sGenKey).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            If TempKey.Length > 20 Then
                TempKey = TempKey.Substring(0, 20)
            End If
            TempKey = TempKey.ToUpper
            txtKey.Text = TempKey
            txtKey.PasswordChar = Nothing
            cmdRegister.Focus()
        End If
    End Sub
End Class
