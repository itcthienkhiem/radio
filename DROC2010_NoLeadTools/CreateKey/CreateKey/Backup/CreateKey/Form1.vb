Class CreateKey
    Private ChuoiGoc As String = ""
    Dim DicomViewer As String
    Dim RISLINK As String
    Dim LABLINK As String
    Dim pwd As String = ""
    Private Sub CreateKey_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
        If e.KeyCode = Keys.Enter Then ProcessTabKey(True)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadKey()
        txtKey.ReadOnly = True
        Dim reg As New clsRegistry.clsRegistry
        If optLis.Checked Then
            If GetSetting("VietBaJSC", "LABLINK", "REGKEY") = "" Then
                cmdDel.Enabled = False
            Else
                cmdDel.Enabled = True
            End If

        ElseIf optRISlink.Checked Then
            If GetSetting("VietBaJSC", "RISLINK", "REGKEY") = "" Then
                cmdDel.Enabled = False
            Else
                cmdDel.Enabled = True
            End If
        Else
            If GetSetting("VietBaJSC", "DICOM", "REGKEY") = "" Then
                cmdDel.Enabled = False
            Else
                cmdDel.Enabled = True
            End If
        End If

    End Sub
    Private Sub LoadKey()
        Dim oWMI
        Dim s As String = ""
        Dim wmiObject
        Dim Reval As String = ""
        Dim sv_oEncrypt As New VietBaIT.Encrypt("Rijndael")

        Try
            'oWMI = GetObject("Winmgmts:{impersonationLevel=impersonate}").InstancesOf("Win32_LogicalDisk")
            'For Each wmiObject In oWMI
            '    If wmiObject.DeviceID = "C:" Then
            '        s = wmiObject.VolumeSerialNumber
            '    End If
            'Next
            oWMI = GetObject("Winmgmts:{impersonationLevel=impersonate}").InstancesOf("Win32_Processor")
            For Each wmiObject In oWMI
                If Not IsDBNull(wmiObject.ProcessorID) And Not IsNothing(wmiObject.ProcessorID) Then
                    If Trim(wmiObject.ProcessorID) <> "" Then
                        s &= wmiObject.ProcessorID
                    End If
                End If
            Next

            oWMI = GetObject("Winmgmts:{impersonationLevel=impersonate}").InstancesOf("Win32_PhysicalMedia")
            For Each wmiObject In oWMI
                If Not IsDBNull(wmiObject.SerialNumber) And Not IsNothing(wmiObject.SerialNumber) Then
                    If Trim(wmiObject.SerialNumber) <> "" Then
                        s &= wmiObject.SerialNumber
                        pwd &= wmiObject.SerialNumber
                    End If
                End If
            Next
            If Not s Is Nothing And Not IsDBNull(s) Then
                If s.Length > 20 Then
                    s = s.Substring(0, 20)
                End If
            End If
            sv_oEncrypt.sPwd = pwd
            LABLINK = s
            DicomViewer = sv_oEncrypt.Mahoa(s).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            RISLINK = sv_oEncrypt.Mahoa(DicomViewer).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            If DicomViewer.Length > 20 Then
                DicomViewer = DicomViewer.Substring(0, 20)
            End If
            If RISLINK.Length > 20 Then
                RISLINK = RISLINK.Substring(0, 20)
            End If
            If optLis.Checked Then
                txtValue.Text = s
            ElseIf optRISlink.Checked Then
                txtValue.Text = RISLINK
            Else
                txtValue.Text = DicomViewer
            End If
            Return
        Catch ex As Exception
            txtValue.Text = "150181KKyBB011183"
            'Reval = sv_oEncrypt.Mahoa("150181KKyBB011183").Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "")
            'If Not Reval Is Nothing And Not IsDBNull(Reval) Then
            '    If Reval.Length > 20 Then
            '        Reval = Reval.Substring(0, 20)
            '    End If
            'End If
            'txtKey.Text = Reval.ToUpper
        End Try
    End Sub

    Private Sub cmdRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRegister.Click
        Try
            Dim reg As New clsRegistry.clsRegistry
            If optLis.Checked Then
                SaveSetting("VietBaJSC", "LABLINK", "REGKEY", txtKey.Text.Trim)
                If GetSetting("VietBaJSC", "LABLINK", "REGKEY") = "" Then
                    cmdDel.Enabled = False
                Else
                    cmdDel.Enabled = True
                End If
            ElseIf optRISlink.Checked Then
                SaveSetting("VietBaJSC", "RISLINK", "REGKEY", txtKey.Text.Trim)
                If GetSetting("VietBaJSC", "RISLINK", "REGKEY") = "" Then
                    cmdDel.Enabled = False
                Else
                    cmdDel.Enabled = True
                End If
            Else
                SaveSetting("VietBaJSC", "DICOM", "REGKEY", txtKey.Text.Trim)
                If GetSetting("VietBaJSC", "DICOM", "REGKEY") = "" Then
                    cmdDel.Enabled = False
                Else
                    cmdDel.Enabled = True
                End If
            End If

            MessageBox.Show("Đã tạo khóa thành công cho ứng dụng của bạn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        Try
            Dim reg As New clsRegistry.clsRegistry
            If optLis.Checked Then
                DeleteSetting("VietBaJSC", "LABLINK", "REGKEY")
                If GetSetting("VietBaJSC", "LABLINK", "REGKEY") = "" Then
                    cmdDel.Enabled = False
                Else
                    cmdDel.Enabled = True
                End If
            ElseIf optRISlink.Checked Then
                DeleteSetting("VietBaJSC", "RISLINK", "REGKEY")
                If GetSetting("VietBaJSC", "RISLINK", "REGKEY") = "" Then
                    cmdDel.Enabled = False
                Else
                    cmdDel.Enabled = True
                End If
            Else
                Try
                    DeleteSetting("VBITJSC", "DICOM", "FIRSTIME")
                Catch ex As Exception
                End Try

                DeleteSetting("VietBaJSC", "DICOM", "REGKEY")
                If GetSetting("VietBaJSC", "DICOM", "REGKEY") = "" Then
                    cmdDel.Enabled = False
                Else
                    cmdDel.Enabled = True
                End If
            End If

            MessageBox.Show("Đã xóa khóa của ứng dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub txtValue_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtValue.TextChanged
        Dim sv_oEncrypt As New VietBaIT.Encrypt("Rijndael")
        'sv_oEncrypt.sPwd = pwd
        txtKey.Text = sv_oEncrypt.Mahoa(txtValue.Text.Trim).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "")
        If Not txtKey.Text Is Nothing And Not IsDBNull(txtKey.Text) Then
            If txtKey.Text.Length > 20 Then
                txtKey.Text = txtKey.Text.Substring(0, 20)
            End If
        End If
        txtKey.Text = txtKey.Text.ToUpper
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged, optLis.CheckedChanged, optRISlink.CheckedChanged
        If optLis.Checked Then
            txtValue.Text = LABLINK
        ElseIf optRISlink.Checked Then
            txtValue.Text = RISLINK
        Else
            txtValue.Text = DicomViewer
        End If
        '---------------------------------------------------------------
        If RadioButton2.Checked Then
            txtValue.MaxLength = 30
        Else
            txtValue.MaxLength = 20
        End If
        txtKey.ReadOnly = True
        Dim reg As New clsRegistry.clsRegistry
        If optLis.Checked Then
            If GetSetting("VietBaJSC", "LABLINK", "REGKEY") = "" Then
                cmdDel.Enabled = False
            Else
                cmdDel.Enabled = True
            End If
        ElseIf optRISlink.Checked Then
            If GetSetting("VietBaJSC", "RISLINK", "REGKEY") = "" Then
                cmdDel.Enabled = False
            Else
                cmdDel.Enabled = True
            End If
        Else
            If GetSetting("VietBaJSC", "DICOM", "REGKEY") = "" Then
                cmdDel.Enabled = False
            Else
                cmdDel.Enabled = True
            End If
        End If
    End Sub

    Private Sub optLis_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optLis.CheckedChanged
    End Sub
End Class
