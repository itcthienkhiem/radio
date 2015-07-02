Imports System.IO

Public Class RegKey
    Public mv_bCancel As Boolean = True
    Public mv_sKey As String = ""
    Public mv_sGenKey As String
    Public hasNotice As Boolean = False
    Private TempKey As String = ""
    Public _AppType As String = ""
    Public pwd As String = ""
    Public loopNum As Integer = 1
    Public expd As String
    Private B16Allowed As String = ""
    Private MSaverAllowed As String = ""
    Private PMSaverAllowed As String = ""
    Private ZAPAllowed As String = ""
    Private CAMAllowed As String = ""
    Private FMOCAllowed As String = ""
    Private Sub RegKey_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If mv_bCancel AndAlso Not hasNotice Then
            MessageBox.Show("Bạn hãy liên hệ với nhà cung cấp để có khóa kích hoạt phần mềm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)

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
        Try
            hasNotice = False
            lnkGenKey.Text = "Giá trị sinh khóa:" & mv_sGenKey & "-" & pwd
            txtKey.Focus()
            Me.Text = "Đăng ký khóa cho:" & _AppType
            If _AppType.Trim.ToUpper = "HIS" OrElse _AppType.Trim.ToUpper = "GOLFMAN" OrElse _AppType.Trim.ToUpper = "DICOMVIEWER" OrElse _AppType.Trim.ToUpper = "LABLINK" OrElse _AppType.Trim.ToUpper = "RISLINK" OrElse _AppType.Trim.ToUpper = "DROC" Then
            Else
                MessageBox.Show("Mã ứng dụng bạn truyền đã bị sai. Liên hệ với người tạo ra nó để biết thêm chi tiết", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                hasNotice = True
                Me.Close()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmdRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRegister.Click
        Try
            If txtExpDate.Text.Trim = "" Then
                MessageBox.Show("Khóa bạn nhập không đúng. Xin hãy nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtKey.Focus()
                Return
            End If
            If txtKey.Text.Trim.Equals(mv_sKey) Then
                mv_bCancel = False

                If _AppType.Trim.ToUpper = "DICOMVIEWER" OrElse _AppType.Trim.ToUpper = "GOLFMAN" OrElse _AppType.Trim.ToUpper = "HIS" Then
                    SaveSetting("VietBaJSC", "DICOM", "REGKEY", txtKey.Text.Trim)
                ElseIf _AppType.Trim.ToUpper = "LABLINK" Then
                    SaveSetting("VietBaJSC", "LABLINK", "REGKEY", txtKey.Text.Trim)
                Else
                    SaveSetting("VietBaJSC", "RISLINK", "REGKEY", txtKey.Text.Trim)
                End If
                expd = txtExpDate.Text.Trim
                MessageBox.Show("Nhấn OK để bắt đầu vào chương trình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'RegConfiguration.SaveSettings("VBITJSC", "DICOM", "FIRSTIME", "1")
                hasNotice = True
                Me.Close()
            Else
                MessageBox.Show("Khóa bạn nhập không đúng. Xin hãy nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtKey.Focus()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        MessageBox.Show("Bạn hãy liên hệ với nhà cung cấp để có khóa kích hoạt phần mềm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        hasNotice = True
        Me.Close()
    End Sub

    Private Sub lnkGenKey_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkGenKey.LinkClicked
        Try
            Clipboard.SetData("A".GetType.ToString, mv_sGenKey & "-" & pwd)
            MessageBox.Show("Giá trị sinh khóa kích hoạt phần mềm: " & mv_sGenKey & "-" & pwd & " đã được copy vào Clipboard của máy tính. Bạn hãy mở file word và nhấn tổ hợp phím Ctrl+V. Sau đó hãy gửi tới nhà cung cấp để có khóa kích hoạt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception

        End Try
    End Sub


    Private Sub txtKey_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKey.KeyDown
        If e.KeyCode = Keys.Enter AndAlso txtKey.PasswordChar = Convert.ToChar("*") AndAlso txtKey.Text.Trim.ToUpper = "DQMNNQDVC080920080111198315011981" Then
            MessageBox.Show("Khóa kích hoạt phần mềm là:" + txtKey.Text)
            cmdRegister.PerformClick()
        End If
    End Sub
    Dim blnRaiseEvent = True
    Private Sub txtKey_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKey.TextChanged
        Try
            If Not blnRaiseEvent Then Return
            TempKey = mv_sGenKey
            If txtKey.PasswordChar = Convert.ToChar("*") AndAlso txtKey.Text.Trim.ToUpper = "DQMNNQDVC080920080111198315011981" Then
                Dim sv_oEncrypt As New ect.Encrypt("Rijndael")
                sv_oEncrypt.sPwd = pwd
                For i As Integer = 1 To loopNum
                    TempKey = sv_oEncrypt.Mahoa(TempKey).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
                Next
                If TempKey.Length > 500 Then
                    TempKey = TempKey.Substring(0, 500)
                End If
                TempKey = TempKey.ToUpper
                txtKey.Text = TempKey
                txtKey.PasswordChar = Nothing
                cmdRegister.Focus()
            Else
                If txtKey.Text.Trim.Split("@@@$$$&&&").Length > 1 Then
                    blnRaiseEvent = False
                    Dim lstValues As New List(Of String)
                    lstValues = SplitString(txtKey.Text.Trim, "@@@$$$&&&")

                    txtExpDate.Text = lstValues(0).Replace(lstValues(0).Trim.Split("-")(0) + "-", "").Trim
                    txtKey.Text = lstValues(0).Trim.Split("-")(0)
                    CreateB16("B16", lstValues(1), txtKey.Text.Trim())
                    CreateMSaver("MSaver", lstValues(2), txtKey.Text.Trim())
                    CreatePMSaver("PMSaver", lstValues(3), txtKey.Text.Trim())
                    CreateZAP("ZAP", lstValues(4), txtKey.Text.Trim())
                    CreateCAM("CAM", lstValues(5), txtKey.Text.Trim())
                    CreateFMOC("FMOC", lstValues(6), txtKey.Text.Trim())

                Else
                    txtExpDate.Text = ""
                End If
            End If
        Catch ex As Exception
        Finally


        End Try


    End Sub

    Function SplitString(ByVal inputString As String, ByVal seperatorString As String) As List(Of String)
        Dim result As List(Of String) = New List(Of String)
        Dim temp = inputString.Split(seperatorString.ToCharArray)
        For Each s As String In temp
            If Not String.IsNullOrEmpty(s) Then result.Add(s)
        Next
        Return result
    End Function
   


    Function IsAllowed(ByVal pwd As String, ByVal val As String)
        Dim sv_oEncrypt As New ect.Encrypt()
        sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
        sv_oEncrypt.sPwd = pwd
        Dim _v As String = sv_oEncrypt.GiaiMa(val)
        Return Not _v.Contains("NOTALLOWED")
    End Function
    Private Sub CreateMSaver(ByVal pwd As String, ByVal val As String, ByVal key As String)
        Try
            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            Dim strFile As String = Application.StartupPath + "\" + sv_oEncrypt.Sum
            If IsAllowed(pwd, val) Then
                Dim _val = sv_oEncrypt.AllowSum
                sv_oEncrypt.sPwd = key + _val
                Dim SUMKey As String = sv_oEncrypt.Mahoa(_val)
                Using writer As New StreamWriter(strFile)
                    writer.WriteLine(SUMKey)
                    writer.Flush()
                    writer.Close()
                End Using
            Else
                try2delfile(strFile)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CreatePMSaver(ByVal pwd As String, ByVal val As String, ByVal key As String)
        Try

            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            Dim strFile As String = Application.StartupPath + "\" + sv_oEncrypt.pSum
            If IsAllowed(pwd, val) Then
                Dim _val = sv_oEncrypt.AllowPSum

                sv_oEncrypt.sPwd = key + _val
                Dim PSUMKey As String = sv_oEncrypt.Mahoa(_val)
                Using writer As New StreamWriter(strFile)
                    writer.WriteLine(PSUMKey)
                    writer.Flush()
                    writer.Close()
                End Using
            Else
                try2delfile(strFile)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CreateZAP(ByVal pwd As String, ByVal val As String, ByVal key As String)
        Try

            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            Dim strFile As String = Application.StartupPath + "\" + sv_oEncrypt.ZapPath
            If IsAllowed(pwd, val) Then
                Dim _val = sv_oEncrypt.ZAP


                sv_oEncrypt.sPwd = key + _val
                Dim ZAPKey As String = sv_oEncrypt.Mahoa(_val)
                Using writer As New StreamWriter(strFile)
                    writer.WriteLine(ZAPKey)
                    writer.Flush()
                    writer.Close()
                End Using
            Else
                try2delfile(strFile)
            End If


        Catch ex As Exception

        End Try
    End Sub
    Sub try2delfile(ByVal file As String)
        Try
            If System.IO.File.Exists(file) Then
                System.IO.File.Delete(file)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CreateCAM(ByVal pwd As String, ByVal val As String, ByVal key As String)
        Try

            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            Dim _val = sv_oEncrypt.CAM
            Dim strFile As String = Application.StartupPath + "\" + sv_oEncrypt.CamPath
            If IsAllowed(pwd, val) Then


                sv_oEncrypt.sPwd = key + _val
                Dim CAMKey As String = sv_oEncrypt.Mahoa(_val)
                Using writer As New StreamWriter(strFile)
                    writer.WriteLine(CAMKey)
                    writer.Flush()
                    writer.Close()
                End Using
            Else
                try2delfile(strFile)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CreateFMOC(ByVal pwd As String, ByVal val As String, ByVal key As String)
        Try

            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            Dim _val = sv_oEncrypt.MOC
            Dim strFile As String = Application.StartupPath + "\" + sv_oEncrypt.MOCPath
            If IsAllowed(pwd, val) Then


                sv_oEncrypt.sPwd = key + _val
                Dim MOCKey As String = sv_oEncrypt.Mahoa(_val)
                Using writer As New StreamWriter(strFile)
                    writer.WriteLine(MOCKey)
                    writer.Flush()
                    writer.Close()
                End Using
            Else
                try2delfile(strFile)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CreateB16(ByVal pwd As String, ByVal val As String, ByVal key As String)
        Try

            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            Dim _val = sv_oEncrypt.B16
            Dim strFile As String = Application.StartupPath + "\" + sv_oEncrypt.BPath
            If IsAllowed(pwd, val) Then


                sv_oEncrypt.sPwd = key + _val
                Dim BKey As String = sv_oEncrypt.Mahoa(_val)
                Using writer As New StreamWriter(strFile)
                    writer.WriteLine(BKey)
                    writer.Flush()
                    writer.Close()
                End Using
            Else
                try2delfile(strFile)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
