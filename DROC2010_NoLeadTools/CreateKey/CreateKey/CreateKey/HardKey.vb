Imports System.Management
Imports System.IO

Public Class HardKey
    Private AppType As String
    Public GenKey As String
    Public RegKey As String
    Public pwd As String = ""
    Public loopNum As Integer = 1
    Delegate Sub _CheckKing()
    Public Sub New(ByVal _AppType As String, ByVal _loopNum As Integer, ByVal ShowCheckingKey As Boolean)
        'MessageBox.Show("OK")
        loopNum = _loopNum
        Utils.chk.AppType = _AppType
        Dim t As New Threading.Thread(AddressOf Checking)
        If ShowCheckingKey Then
            t.Start()
        End If

        Application.DoEvents()
        Me.AppType = _AppType
        GenKey = GetGenKey()
        RegKey = GetRegKey()
        Try
            If ShowCheckingKey Then
                If Not IsNothing(t) AndAlso t.ThreadState = Threading.ThreadState.Running Then t.Abort()
                Utils.chk.Close()
            End If

        Catch ex As Exception
            Utils.chk.Close()
        End Try
    End Sub

    Private Sub Checking()
        Try
            If Not Utils.chk.InvokeRequired Then
                Utils.chk.ShowDialog()
            Else
                Utils.chk.BeginInvoke(New _CheckKing(AddressOf Checking))
            End If
        Catch ex As Exception

        End Try

        'End If
    End Sub

    Private Function GetRegKey() As String
        Try
            If GenKey = "" Then Return ""
            Dim sv_oEncrypt As New ect.Encrypt("Rijndael")
            sv_oEncrypt.SaltIVFile = Application.StartupPath & "\data.dat"
            sv_oEncrypt.sPwd = pwd
            Dim Reval As String = GenKey
            For i As Integer = 1 To loopNum
                Reval = sv_oEncrypt.Mahoa(Reval).Replace("\", "").Replace("+", "").Replace("-", "").Replace("*", "").Replace("/", "").ToUpper
            Next
            If Reval.Length > 500 Then
                Reval = Reval.Substring(0, 500)
            End If
            Return Reval.ToUpper
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function sGetRegKey(ByVal Key1 As String, ByVal subKey1 As String, ByVal Subkey2 As String) As String
        Return GetSetting(Key1, subKey1, Subkey2)
    End Function
    Public Sub AutoGenKey(ByVal FENfolder As String, ByVal productKey As String)

        Dim FENFile As String = FENfolder & "\ltlruntimelic.fen"
        Dim RTMFile As String = FENfolder & "\ltlrtlic.rtm"
        Dim LICPathFile As String = FENfolder & "\LicPath.txt"
        Dim lstValues As New List(Of String)

        Try
            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            If Not File.Exists(FENFile) Then
                Dim arrFENFiles As String() = Directory.GetFiles(Application.StartupPath, "*.FEN")
                If arrFENFiles.Length <= 0 Then
                    ShowLeadtoolsMessage()
                    Return
                Else
                    FENFile = arrFENFiles(0)
                End If
            End If

            If File.Exists(FENFile) Then
                Using reader As New StreamReader(FENFile)
                    While reader.Peek() > 0
                        Dim _lineN As String = reader.ReadLine
                        If Not _lineN Is Nothing Then
                            Dim arrValues As String()
                            arrValues = _lineN.Split(",")
                            If arrValues.Length = 2 Then
                                sv_oEncrypt.sPwd = productKey.Trim()
                                lstValues.Add(arrValues(0) & "," & sv_oEncrypt.Mahoa(arrValues(1)))
                            End If

                        End If
                    End While

                End Using
                If lstValues.Count > 0 Then
                    Using writer As New StreamWriter(RTMFile)
                        For Each sVal As String In lstValues
                            writer.WriteLine(sVal)
                        Next
                        writer.Flush()
                        writer.Close()
                    End Using
                    'WriteLicPath
                    Using writer As New StreamWriter(LICPathFile)
                        For Each sVal As String In lstValues
                            writer.WriteLine("ltlrtlic.rtm")
                        Next
                        writer.Flush()
                        writer.Close()
                    End Using
                End If
            End If
        Catch ex As Exception

        End Try


    End Sub
    Sub ShowLeadtoolsMessage()
        MessageBox.Show("LeadTools is not licsened.  Contact LEAD Technologies, Inc. at (704) 332-5532 to order a new licsence.", "LEADTOOLS for .NET Evalutation Notice")
    End Sub

        

    Private Function GetGenKey() As String
        Dim oWMI
        Dim s As String = ""
        Dim wmiObject
        Dim Reval As String = ""
        pwd = ""
        Dim sv_oEncrypt As New ect.Encrypt("Rijndael")
        sv_oEncrypt.SaltIVFile = Application.StartupPath & "\data.dat"
        If AppType.Trim.ToUpper = "DROC" OrElse AppType.Trim.ToUpper = "HIS" OrElse AppType.Trim.ToUpper = "GOLFMAN" OrElse AppType.Trim.ToUpper = "DICOMVIEWER" OrElse AppType.Trim.ToUpper = "LABLINK" OrElse AppType.Trim.ToUpper = "RISLINK" Then
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
            If Not s Is Nothing And Not IsDBNull(s) Then
                If s.Length > 500 Then
                    s = s.Substring(0, 500)
                End If
            End If

            sv_oEncrypt.sPwd = pwd
            pwd = sv_oEncrypt.Mahoa(pwd)
            sv_oEncrypt.sPwd = pwd
            Dim DicomViewer As String
            Dim RISLINK As String
            Dim LABLINK As String
            Dim HISLINK As String
            Dim DROC As String
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
            Utils.bHasFound = True
            If AppType.Trim.ToUpper = "DICOMVIEWER" OrElse AppType.Trim.ToUpper = "GOLFMAN" OrElse AppType.Trim.ToUpper = "HISLINK" Then
                Return DicomViewer
            ElseIf AppType.Trim.ToUpper = "RISLINK" Then
                Return RISLINK
            ElseIf AppType.Trim.ToUpper = "DROC" Then
                Return DROC
            Else
                Return LABLINK
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Class
