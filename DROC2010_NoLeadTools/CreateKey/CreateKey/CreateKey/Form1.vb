Imports System.Management
Imports System.IO
Class CreateKey
    Private ChuoiGoc As String = ""
    Dim DROC As String = ""
    Dim DicomViewer As String = ""
    Dim RISLINK As String = ""
    Dim LABLINK As String = ""
    Dim HISLINK As String = ""
    Dim pwd As String = ""
    Dim _loopNum As Integer = 1
    Dim _raiseEvent As Boolean = True
    Private Sub CreateKey_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
        If e.KeyCode = Keys.Enter Then ProcessTabKey(True)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadKey()
        txtKey.ReadOnly = True
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
        Dim s As String = ""
        Dim Reval As String = ""
        Dim sv_oEncrypt As New ect.Encrypt("Rijndael")
        pwd = ""
        Try
            'oWMI = GetObject("Winmgmts:{impersonationLevel=impersonate}").InstancesOf("Win32_LogicalDisk")
            'For Each wmiObject In oWMI
            '    If wmiObject.DeviceID = "C:" Then
            '        s = wmiObject.VolumeSerialNumber
            '    End If
            'Next
            'oWMI = GetObject("Winmgmts:{impersonationLevel=impersonate}").InstancesOf("Win32_Processor")
            'For Each wmiObject In oWMI
            '    If Not IsDBNull(wmiObject.ProcessorID) And Not IsNothing(wmiObject.ProcessorID) Then
            '        If Trim(wmiObject.ProcessorID) <> "" Then
            '            s &= RegConfiguration.sDbnull(wmiObject.ProcessorID, "").Trim
            '        End If
            '    End If
            'Next
            Using mc As New ManagementClass("win32_processor")
                Dim moc As ManagementObjectCollection = mc.GetInstances()

                For Each mo As ManagementObject In moc
                    If s = "" Then
                        s &= mo.Properties("processorID").Value.ToString().Trim
                        pwd &= "@@@pid@@@" & mo.Properties("processorID").Value.ToString().Trim
                    End If
                Next
            End Using

            Using dsk As New ManagementObject("Win32_LogicalDisk.DeviceID=""C:""")
                dsk.Get()
                s &= dsk("VolumeSerialNumber").ToString().Trim
                pwd &= "@@@vsn@@@" & dsk("VolumeSerialNumber").ToString().Trim
            End Using
            Using searcher As New ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia")

                For Each wmi_HD As ManagementObject In searcher.Get()
                    ' get the hardware serial no.
                    If Not wmi_HD("SerialNumber") Is Nothing Then
                        s &= "@@@ser@@@" & wmi_HD("SerialNumber").ToString().Trim
                        pwd &= "@@@ser@@@" & wmi_HD("SerialNumber").ToString().Trim
                    End If

                Next wmi_HD
            End Using
            'oWMI = GetObject("Winmgmts:{impersonationLevel=impersonate}").InstancesOf("Win32_PhysicalMedia")
            'For Each wmiObject In oWMI
            '    If Not IsDBNull(wmiObject.SerialNumber) And Not IsNothing(wmiObject.SerialNumber) Then
            '        If Trim(wmiObject.SerialNumber) <> "" Then
            '            s &= RegConfiguration.sDbnull(wmiObject.SerialNumber, "").Trim
            '            pwd &= RegConfiguration.sDbnull(wmiObject.SerialNumber, "").Trim
            '        End If
            '    End If
            'Next
            If Not s Is Nothing And Not IsDBNull(s) Then
                If s.Length > 500 Then
                    s = s.Substring(0, 500)
                End If
            End If
            sv_oEncrypt.sPwd = pwd
            pwd = sv_oEncrypt.Mahoa(pwd)

            sv_oEncrypt.sPwd = pwd
            txtpwd.Text = pwd
            LABLINK = s
            DicomViewer = sv_oEncrypt.Mahoa(s).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            RISLINK = sv_oEncrypt.Mahoa(DicomViewer).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            HISLINK = sv_oEncrypt.Mahoa(RISLINK).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            DROC = sv_oEncrypt.Mahoa(HISLINK).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            If DicomViewer.Length > 500 Then
                DicomViewer = DicomViewer.Substring(0, 500)
            End If
            If RISLINK.Length > 500 Then
                RISLINK = RISLINK.Substring(0, 500)
            End If
            If HISLINK.Length > 500 Then
                HISLINK = HISLINK.Substring(0, 500)
            End If
            If DROC.Length > 500 Then
                DROC = DROC.Substring(0, 500)
            End If
            If optLis.Checked Then
                txtValue.Text = s
            ElseIf optRISlink.Checked Then
                txtValue.Text = RISLINK
            ElseIf optDcmViewer.Checked Then
                txtValue.Text = DicomViewer
            Else
                txtValue.Text = DROC
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
            If System.IO.File.Exists(Application.StartupPath & "\RegKey.dat") Then
                System.IO.File.Delete(Application.StartupPath & "\RegKey.dat")
            End If
            MessageBox.Show("Đã xóa khóa của ứng dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub txtValue_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtValue.TextChanged
        Try
            If (Not _raiseEvent) Then Return
            If (txtValue.Text.Contains("-")) Then
                _raiseEvent = False
                Dim s As String = txtValue.Text.Trim
                txtValue.Text = s.Split("-")(0)
                _raiseEvent = True
                txtpwd.Text = s.Split("-")(1)
            End If
            Dim sv_oEncrypt As New ect.Encrypt("Rijndael")
            sv_oEncrypt.sPwd = txtpwd.Text.Trim

            Dim _temp As String = txtValue.Text.Trim
            For i As Integer = 1 To _loopNum
                _temp = sv_oEncrypt.Mahoa(_temp).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            Next
            txtKey.Text = _temp
            If Not txtKey.Text Is Nothing And Not IsDBNull(txtKey.Text) Then
                If txtKey.Text.Length > 500 Then
                    txtKey.Text = txtKey.Text.Substring(0, 500)
                End If
            End If
            sv_oEncrypt.sPwd = txtKey.Text.ToUpper
            txtKey.Text = txtKey.Text.ToUpper + "-" + sv_oEncrypt.Mahoa(getYYYYMMDD(dtpExpd.Value))
            AddText()
        Catch ex As Exception

        End Try

    End Sub
    Function getYYYYMMDD(ByVal dtm As DateTime) As String
        Try
            Return dtm.Year.ToString + Strings.Right("00" + dtm.Month.ToString, 2) + Strings.Right("00" + dtm.Day.ToString, 2)
        Catch ex As Exception
            Return "20121231"
        End Try
    End Function
    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDcmViewer.CheckedChanged, optLis.CheckedChanged, optRISlink.CheckedChanged
        If optLis.Checked Then
            _loopNum = 0
            txtValue.Text = LABLINK
        ElseIf optDcmViewer.Checked Then
            _loopNum = 1
            txtValue.Text = DicomViewer
        ElseIf optRISlink.Checked Then
            _loopNum = 2
            txtValue.Text = RISLINK
        Else
            _loopNum = 4
            txtValue.Text = DROC
        End If
        '---------------------------------------------------------------
        'If optDcmViewer.Checked Then
        '    txtValue.MaxLength = 30
        'Else
        '    txtValue.MaxLength = 20
        'End If
        txtKey.ReadOnly = True

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
        ElseIf optDcmViewer.Checked Then
            If GetSetting("VietBaJSC", "DICOM", "REGKEY") = "" Then
                cmdDel.Enabled = False
            Else
                cmdDel.Enabled = True
            End If
        Else
            If GetSetting("VietBaJSC", "DROC", "REGKEY") = "" Then
                cmdDel.Enabled = False
            Else
                cmdDel.Enabled = True
            End If
        End If
    End Sub

    Private Sub optLis_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optLis.CheckedChanged
    End Sub

    Private Sub optDROC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDROC.CheckedChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpwd.TextChanged
        txtValue_TextChanged(txtValue, e)
    End Sub

    Private Sub dtpExpd_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpExpd.ValueChanged
        txtValue_TextChanged(txtValue, e)
    End Sub

    

    Private Sub cmdCopy2Clipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopy2Clipboard.Click
        Clipboard.SetData("A".GetType.ToString, txtKey.Text.Trim())
    End Sub

 
  
    Private Sub chkSaveMemory_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        txtValue_TextChanged(txtValue, e)
    End Sub
    Sub AddText()
        Try
            If optDROC.Checked Then
                txtKey.Text = txtKey.Text
            End If
        Catch ex As Exception

        End Try
    End Sub
    
End Class
