Imports System.Management
Imports System.IO
Class MCreateKey
    Private ChuoiGoc As String = ""
    Dim DROC As String = ""
    Dim MIPACS As String = ""
    Dim DicomViewer As String = ""
    Dim RISLINK As String = ""
    Dim LABLINK As String = ""
    Dim HISLINK As String = ""
    Dim pwd As String = ""
    Dim _loopNum As Integer = 1
    Dim _raiseEvent As Boolean = True
    Private Sub MCreateKey_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            Using mc As New ManagementClass("win32_processor")
                Dim moc As ManagementObjectCollection = mc.GetInstances()

                For Each mo As ManagementObject In moc
                    If s = "" Then
                        s &= mo.Properties("processorID").Value.ToString()
                        s &= mo.Properties("DeviceID").Value.ToString()
                        pwd &= "-pid-" & mo.Properties("processorID").Value.ToString()
                        pwd &= "-did-" & mo.Properties("DeviceID").Value.ToString()
                    End If
                Next
            End Using

            Using dsk As New ManagementObject("Win32_LogicalDisk.DeviceID=""C:""")
                dsk.Get()
                s &= dsk("VolumeSerialNumber").ToString()
                s &= dsk("DeviceID").ToString()
                pwd &= "-vsn-" & dsk("VolumeSerialNumber").ToString()
                pwd &= "-did-" & dsk("DeviceID").ToString()
            End Using
            If Not s Is Nothing And Not IsDBNull(s) Then
                If s.Length > 150 Then
                    s = s.Substring(0, 150)
                End If
            End If

            sv_oEncrypt.sPwd = Utils.MFS.NewPwd4Hrk & pwd
            pwd = sv_oEncrypt.Mahoa(pwd)
            sv_oEncrypt.sPwd = pwd
            txtpwd.Text = pwd
            LABLINK = s
            HISLINK = sv_oEncrypt.Mahoa(LABLINK).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            RISLINK = sv_oEncrypt.Mahoa(HISLINK).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            DicomViewer = sv_oEncrypt.Mahoa(RISLINK).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            DROC = sv_oEncrypt.Mahoa(DicomViewer).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            MIPACS = sv_oEncrypt.Mahoa(DROC).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            If DicomViewer.Length > 150 Then
                DicomViewer = DicomViewer.Substring(0, 150)
            End If
            If RISLINK.Length > 150 Then
                RISLINK = RISLINK.Substring(0, 150)
            End If
            If HISLINK.Length > 150 Then
                HISLINK = HISLINK.Substring(0, 150)
            End If
            If DROC.Length > 150 Then
                DROC = DROC.Substring(0, 150)
            End If
            If MIPACS.Length > 150 Then
                MIPACS = MIPACS.Substring(0, 150)
            End If
            

            If optLis.Checked Then
                txtValue.Text = s
            ElseIf optRISlink.Checked Then
                txtValue.Text = RISLINK
            ElseIf optDcmViewer.Checked Then
                txtValue.Text = DicomViewer
            ElseIf optMiPacs.Checked Then
                txtValue.Text = MIPACS
            ElseIf optDROC.Checked Then
                txtValue.Text = DROC
            End If
            Return
        Catch ex As Exception
            txtValue.Text = "150181KKyBB011183"
            
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
                If txtKey.Text.Length > 150 Then
                    txtKey.Text = txtKey.Text.Substring(0, 150)
                End If
            End If
            sv_oEncrypt.sPwd = txtKey.Text.ToUpper
            txtKey.Text = txtKey.Text.ToUpper + "-" + sv_oEncrypt.Mahoa(getYYYYMMDD(dtpExpd.Value))
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
    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDcmViewer.CheckedChanged, optLis.CheckedChanged, optRISlink.CheckedChanged, optMiPacs.CheckedChanged
        If optLis.Checked Then
            _loopNum = 0
            txtValue.Text = LABLINK
        ElseIf optDcmViewer.Checked Then
            _loopNum = 3
            txtValue.Text = DicomViewer
        ElseIf optRISlink.Checked Then
            _loopNum = 2
            txtValue.Text = RISLINK
        ElseIf optMiPacs.Checked Then
            _loopNum = 5
            txtValue.Text = MIPACS
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

    Private Sub cmdSaveMemory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveMemory.Click
        Try
            Dim _val = cmdSaveMemory.Tag.ToString
            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            Dim strFile As String = Application.StartupPath + "\" + sv_oEncrypt.Sum

            sv_oEncrypt.sPwd = txtKey.Text.Split("-")(0) + _val
            Dim SUMKey As String = sv_oEncrypt.Mahoa(_val)
            Using writer As New StreamWriter(strFile)
                writer.WriteLine(SUMKey)
                writer.Flush()
                writer.Close()
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdPrintMemorySaver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrintMemorySaver.Click
        Try
            Dim _val = cmdPrintMemorySaver.Tag.ToString
            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            Dim strFile As String = Application.StartupPath + "\" + sv_oEncrypt.pSum
            sv_oEncrypt.sPwd = txtKey.Text.Split("-")(0) + _val
            Dim PSUMKey As String = sv_oEncrypt.Mahoa(_val)
            Using writer As New StreamWriter(strFile)
                writer.WriteLine(PSUMKey)
                writer.Flush()
                writer.Close()
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdZAP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdZAP.Click
        Try
            Dim _val = cmdZAP.Tag.ToString
            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            Dim strFile As String = Application.StartupPath + "\" + sv_oEncrypt.ZapPath

            sv_oEncrypt.sPwd = txtKey.Text.Split("-")(0) + _val
            Dim ZAPKey As String = sv_oEncrypt.Mahoa(_val)
            Using writer As New StreamWriter(strFile)
                writer.WriteLine(ZAPKey)
                writer.Flush()
                writer.Close()
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdCAM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCAM.Click
        Try
            Dim _val = cmdCAM.Tag.ToString
            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            Dim strFile As String = Application.StartupPath + "\" + sv_oEncrypt.CamPath

            sv_oEncrypt.sPwd = txtKey.Text.Split("-")(0) + _val
            Dim CAMKey As String = sv_oEncrypt.Mahoa(_val)
            Using writer As New StreamWriter(strFile)
                writer.WriteLine(CAMKey)
                writer.Flush()
                writer.Close()
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdFreeMOC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFreeMOC.Click
        Try
            Dim _val = cmdFreeMOC.Tag.ToString
            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            Dim strFile As String = Application.StartupPath + "\" + sv_oEncrypt.MOCPath

            sv_oEncrypt.sPwd = txtKey.Text.Split("-")(0) + _val
            Dim MOCKey As String = sv_oEncrypt.Mahoa(_val)
            Using writer As New StreamWriter(strFile)
                writer.WriteLine(MOCKey)
                writer.Flush()
                writer.Close()
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdB16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdB16.Click
        Try
            Dim _val = cmdB16.Tag.ToString
            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            Dim strFile As String = Application.StartupPath + "\" + sv_oEncrypt.BPath

            sv_oEncrypt.sPwd = txtKey.Text.Split("-")(0) + _val
            Dim BKey As String = sv_oEncrypt.Mahoa(_val)
            Using writer As New StreamWriter(strFile)
                writer.WriteLine(BKey)
                writer.Flush()
                writer.Close()
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdCopy2Clipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopy2Clipboard.Click
        Clipboard.SetData("A".GetType.ToString, txtKey.Text.Trim())
    End Sub

    Private Sub cmdCreateLeadtoolsRuntimeLicense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreateLeadtoolsRuntimeLicense_0.Click
        Dim _OpenFileDialog As New OpenFileDialog
        Dim lstValues As New List(Of String)
        _OpenFileDialog.Multiselect = False
        If _OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)

            Using reader As New StreamReader(_OpenFileDialog.FileName)
                While reader.Peek() > 0
                    Dim _lineN As String = reader.ReadLine
                    If Not _lineN Is Nothing Then
                        Dim arrValues As String()
                        arrValues = _lineN.Split(",")
                        If arrValues.Length = 2 Then
                            sv_oEncrypt.sPwd = sv_oEncrypt.Fam_PWD
                            lstValues.Add(arrValues(0) & "," & sv_oEncrypt.Mahoa(arrValues(1)))
                        End If

                    End If
                End While

            End Using
            Dim _SaveFileDialog As New SaveFileDialog
            _SaveFileDialog.Filter = "FEN files|*.fen"
            _SaveFileDialog.FileName = "ltlruntimelic.fen"
            If _SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                Using writer As New StreamWriter(_SaveFileDialog.FileName)
                    For Each sVal As String In lstValues
                        writer.WriteLine(sVal)
                    Next
                    writer.Flush()
                    writer.Close()
                End Using
            End If

        End If
    End Sub

    Private Sub cmdCreateLeadtoolsRuntimeLicense_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreateLeadtoolsRuntimeLicense.Click
        Dim _OpenFileDialog As New OpenFileDialog
        Dim lstValues As New List(Of String)
        _OpenFileDialog.Filter = "FEN files|*.fen"
        _OpenFileDialog.Multiselect = False
        If _OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)

            Using reader As New StreamReader(_OpenFileDialog.FileName)
                While reader.Peek() > 0
                    Dim _lineN As String = reader.ReadLine
                    If Not _lineN Is Nothing Then
                        Dim arrValues As String()
                        arrValues = _lineN.Split(",")
                        If arrValues.Length = 2 Then
                            sv_oEncrypt.sPwd = txtKey.Text.Trim().Split("-")(0)
                            lstValues.Add(arrValues(0) & "," & sv_oEncrypt.Mahoa(arrValues(1)))
                        End If

                    End If
                End While

            End Using
            Dim _SaveFileDialog As New SaveFileDialog
            _SaveFileDialog.Filter = "rtm files|*.rtm"
            _SaveFileDialog.FileName = "ltlrtlic.rtm"
            If _SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                Using writer As New StreamWriter(_SaveFileDialog.FileName)
                    For Each sVal As String In lstValues
                        writer.WriteLine(sVal)
                    Next
                    writer.Flush()
                    writer.Close()
                End Using
            End If

        End If
    End Sub

    Private Sub cmdGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click
        Dim s As String = getYYYYMMDD(DateTime.Now) + "@@@" + txtpwd2open.Tag.ToString
        If s.Trim.ToUpper = txtpwd2open.Text.Trim.ToUpper Then
            pnlPrivate.Visible = False
            pnlpublic.Visible = True
        Else
            txtpwd2open.Focus()
        End If

    End Sub

    Private Sub cmdSaveto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveto.Click
        Dim _SaveFileDialog As New SaveFileDialog
        _SaveFileDialog.Filter = "License file|*.lic"
        _SaveFileDialog.FileName = "License.lic"
        If _SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Using writer As New StreamWriter(_SaveFileDialog.FileName)
                writer.WriteLine(txtKey.Text.Trim)
                writer.Flush()
                writer.Close()
            End Using
        End If
    End Sub
End Class
