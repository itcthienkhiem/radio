Public Class HardKey
    Private AppType As String
    Public GenKey As String
    Public RegKey As String
    Public pwd As String = ""
    Public Sub New(ByVal _AppType As String)
        Utils.chk.AppType = _AppType
        Dim t As New Threading.Thread(AddressOf Checking)
        t.Start()
        Me.AppType = _AppType
        GenKey = GetGenKey()
        RegKey = GetRegKey()
        Try
            If Not IsNothing(t) AndAlso t.ThreadState = Threading.ThreadState.Running Then t.Abort()
            Utils.chk.Close()
        Catch ex As Exception
            Utils.chk.Close()
        End Try
    End Sub
    Private Sub Checking()
        'If (RegConfiguration.sDbnull(RegConfiguration.GetSettings("VBITJSC", "DICOM", "FIRSTIME"), "") <> "") Then
        '    Return
        'Else
        Utils.chk.ShowDialog()
        'End If
    End Sub

    Private Function GetRegKey() As String
        Try
            If GenKey = "" Then Return ""
            Dim sv_oEncrypt As New VietBaIT.Encrypt("Rijndael")
            sv_oEncrypt.SaltIVFile = Application.StartupPath & "\data.dat"
            'sv_oEncrypt.sPwd = pwd
            Dim Reval As String
            Reval = sv_oEncrypt.Mahoa(GenKey).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            If Reval.Length > 20 Then
                Reval = Reval.Substring(0, 20)
            End If
            Return Reval.ToUpper
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function sGetRegKey(ByVal Key1 As String, ByVal subKey1 As String, ByVal Subkey2 As String) As String
        Return GetSetting(Key1, subKey1, Subkey2)
    End Function
    Private Function GetGenKey() As String
        Dim oWMI
        Dim s As String = ""
        Dim wmiObject
        Dim Reval As String = ""

        Dim sv_oEncrypt As New VietBaIT.Encrypt("Rijndael")
        sv_oEncrypt.SaltIVFile = Application.StartupPath & "\data.dat"
        If AppType.Trim.ToUpper = "HIS" OrElse AppType.Trim.ToUpper = "GOLFMAN" OrElse AppType.Trim.ToUpper = "DICOMVIEWER" OrElse AppType.Trim.ToUpper = "LABLINK" OrElse AppType.Trim.ToUpper = "RISLINK" Then
        Else
            Return ""
        End If
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
            Dim DicomViewer As String
            Dim RISLINK As String
            Dim LABLINK As String
            LABLINK = s
            DicomViewer = sv_oEncrypt.Mahoa(s).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            RISLINK = sv_oEncrypt.Mahoa(DicomViewer).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            If DicomViewer.Length > 20 Then
                DicomViewer = DicomViewer.Substring(0, 20)
            End If
            If RISLINK.Length > 20 Then
                RISLINK = RISLINK.Substring(0, 20)
            End If
            Utils.bHasFound = True
            If AppType.Trim.ToUpper = "DICOMVIEWER" OrElse AppType.Trim.ToUpper = "GOLFMAN" OrElse AppType.Trim.ToUpper = "HIS" Then
                Return DicomViewer
            ElseIf AppType.Trim.ToUpper = "RISLINK" Then
                Return RISLINK
            Else
                Return LABLINK
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Class
