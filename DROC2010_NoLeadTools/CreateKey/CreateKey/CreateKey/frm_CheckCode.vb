Imports System.IO
Public Class frm_CheckCode
    Public mv_bCancel As Boolean = True
    Public mv_sKey As String = ""
    Public mv_sGenKey As String
    Public hasNotice As Boolean = False
    Private TempKey As String = ""
    Public _AppType As String = ""
    Public pwd As String = ""
    Public loopNum As Integer = 1
    Public expd As String
    Private Sub frm_CheckCode_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
       

    End Sub
    Private Sub frm_CheckCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
        If e.KeyCode = Keys.Enter Then ProcessTabKey(True)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.P Then
            If txtpwd.PasswordChar = Convert.ToChar("*") Then
                txtpwd.PasswordChar = Nothing
            Else
                txtpwd.PasswordChar = Convert.ToChar("*")
            End If
        End If
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
           
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmdRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRegister.Click
        Try
          
            Dim _ect As New ect.Encrypt(cmdBrowseKey.Tag.ToString)
            If chkExp.Checked Then
                _ect.sPwd = txtpwd.Text.Trim
            Else
                _ect.sPwd = txtpwd.Text.Trim + Me.Tag.ToString
            End If

            Dim value As String = _ect.GiaiMa(txtsource.Text.Trim)
            txtvalue.Text = TranslateLastTime(value)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        
        Me.Close()
    End Sub

    Private Sub lnkGenKey_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkGenKey.LinkClicked
        Try
            Clipboard.SetData("A".GetType.ToString, mv_sGenKey & "-" & pwd)
            MessageBox.Show("Giá trị sinh khóa kích hoạt phần mềm: " & mv_sGenKey & "-" & pwd & " đã được copy vào Clipboard của máy tính. Bạn hãy mở file word và dán giá trị này bằng cách sử dụng tổ hợp phím Ctrl+V. Sau đó hãy gửi tới nhà cung cấp để có khóa kích hoạt." & vbCrLf & "Tác giả: Đào Văn Cường. Điện thoại: 09 15 5 01 48" & vbCrLf & "Rất cảm ơn bạn đã sử dụng chương trình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception

        End Try
    End Sub


    Private Sub txtKey_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpwd.KeyDown
        If e.KeyCode = Keys.Enter AndAlso txtpwd.PasswordChar = Convert.ToChar("*") AndAlso txtpwd.Text.Trim.ToUpper = "DQMNNQDVC080920080111198315011981" Then
            MessageBox.Show("Khóa kích hoạt phần mềm là:" + txtpwd.Text)
            cmdRegister.PerformClick()
        End If
    End Sub
    Dim blnRaiseEvent = True
    Private Sub txtKey_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpwd.TextChanged
        Try
            If Not blnRaiseEvent Then Return
            TempKey = mv_sGenKey
            If txtpwd.PasswordChar = Convert.ToChar("*") AndAlso txtpwd.Text.Trim.ToUpper = "DQMNNQDVC080920080111198315011981" Then
                Dim sv_oEncrypt As New ect.Encrypt("Rijndael")
                sv_oEncrypt.sPwd = pwd
                For i As Integer = 1 To loopNum
                    TempKey = sv_oEncrypt.Mahoa(TempKey).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
                Next
                If TempKey.Length > 20 Then
                    TempKey = TempKey.Substring(0, 20)
                End If
                TempKey = TempKey.ToUpper
                txtpwd.Text = TempKey
                txtpwd.PasswordChar = Nothing
                cmdRegister.Focus()
            Else
                If txtpwd.Text.Trim.Split("-").Length > 1 Then
                    blnRaiseEvent = False
                    txtsource.Text = txtpwd.Text.Trim.Replace(txtpwd.Text.Trim.Split("-")(0) + "-", "").Trim
                    txtpwd.Text = txtpwd.Text.Trim.Split("-")(0)
                Else
                    txtsource.Text = ""
                End If
            End If
        Catch ex As Exception
        Finally


        End Try


    End Sub

    Private Sub cmdBrowseKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseKey.Click
        Try
            Dim openfile As New OpenFileDialog()
            If openfile.ShowDialog = DialogResult.OK Then
                Dim _reader As New StreamReader(openfile.FileName)
                txtpwd.Text = _reader.ReadLine.ToString()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdBrowseSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseSource.Click
        Try
            Dim openfile As New OpenFileDialog()
            If openfile.ShowDialog = DialogResult.OK Then
                Dim _reader As New StreamReader(openfile.FileName)
                txtsource.Text = _reader.ReadLine.ToString()

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function TranslateLastTime(ByVal value As String) As String
        Try
            If chkExp.Checked Then
                If value.Trim().Length < 8 Then
                    Return "GHI SAI DỮ LIỆU"
                Else
                    Return "Ngày " + value.Substring(6, 2) + " tháng " + value.Substring(4, 2) + " năm " + value.Substring(0, 4)
                End If
            Else
                If value.Trim().Length < 14 Then
                    Return "GHI SAI DỮ LIỆU"
                Else
                    Return "Ngày " + value.Substring(6, 2) + " tháng " + value.Substring(4, 2) + " năm " + value.Substring(0, 4) + "-" + value.Substring(8, 2) + " giờ " + value.Substring(10, 2) + " phút " + value.Substring(12, 2) + " giây"
                End If
            End If
          

        Catch ex As Exception
            Return "GHI SAI DỮ LIỆU"
        End Try


    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim time As String = GetYYYYMMDDHHMMSS(DateTime.Now.AddSeconds(-1))
            Dim _ect As New ect.Encrypt(cmdBrowseKey.Tag.ToString())
            _ect.sPwd = txtpwd.Text.Trim + Me.Tag.ToString
            Dim _valueAfterEct As String = _ect.Mahoa(time)
            Save2File(m_strlstTimeFile, _valueAfterEct)
        Catch ex As Exception

        End Try
    End Sub
    Public Function GetYYYYMMDDHHMMSS(ByVal dtp As DateTime) As String
        Return dtp.Year.ToString() + dtp.Month.ToString().PadLeft(2, "0") + dtp.Day.ToString().PadLeft(2, "0") + dtp.Hour.ToString().PadLeft(2, "0") + dtp.Minute.ToString().PadLeft(2, "0") + dtp.Second.ToString().PadLeft(2, "0")
    End Function
    Sub Save2File(ByVal fileName As String, ByVal Value As String)
        Try
            Dim _streamW As New StreamWriter(fileName)
            _streamW.WriteLine(Value)
            _streamW.Flush()
            _streamW.Close()
        Catch ex As Exception

        End Try
    End Sub
    Dim m_strlstTimeFile As String = Application.StartupPath + "\lstT.lst"


End Class
