Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Collections
Imports System.Timers
Imports Microsoft.VisualBasic

Class MyTcpListener

    'Private Shared client As New TcpClient

    Public Sub FMain(ByVal client As TcpClient)

        Dim cls As New clsPatient


        'server = Nothing
        Try
            ' Set the TcpListener on port 104.
            'Dim port As Int32 = 104
            'Dim localAddr As IPAddress = IPAddress.Parse("127.0.0.1")

            'server = New TcpListener(localAddr, port)

            ' Start listening for client requests.
            'server.Start()

            ' Buffer for reading data

            ' Perform a blocking call to accept requests.
            ' You could also user server.AcceptSocket() here.



            ' Get a stream object for reading and writing

            'AddHandler ARTIMER.Elapsed, AddressOf OnTimerElapsed

            ' Get a stream object for reading and writing

            Select Case MS
                Case "STA1"
                    If AARequest Then
                        MS = "STA4"
                        stream = client.GetStream()
                    Else
                        MS = "STA2"
                        stream = client.GetStream()
                    End If

                Case "STA2"
                    u = New UnparsePDU(stream, bytes)
                    If u.PDUType = &H1 Then
                        AARQPDU = New AAssociateRQ
                        AARQPDU.PDUType = &H1
                        AARQPDU.Length = u.Length
                        AARQPDU.AAssociateRQParse(u.buffer)
                        CurMaxLength = AARQPDU.UserInfoItemVar.MaxPDULength
                        MS = "STA3"
                    End If

                Case "STA3"
                    Dim AAACPDU As New AAssociateAC
                    Dim UserInfoStr As UserInfoSubItem
                    AAACPDU.PDUType = &H2
                    AAACPDU.CalledAET = AARQPDU.CalledAETittle
                    AAACPDU.CallingAET = AARQPDU.CallingAETittle
                    AAACPDU.AppCtxItemVar.AppContextName = AARQPDU.AppCtxItemVar.AppContextName
                    AAACPDU.PreCtxItemVar.PreCtxID = AARQPDU.PreCtxItems(0).PreCtxID
                    AAACPDU.PreCtxItemVar.Result = Accepted

                    Dim TS As New TransferSyntaxSubItem
                    TS.TransferSyntaxName = ImplicitLittleEndian
                    AAACPDU.PreCtxItemVar.TransSyntax.Add(TS)
                    UserInfoStr = New UserInfoSubItem(&H51)
                    If AARQPDU.UserInfoItemVar.MaxPDULength > 0 Then
                        If AARQPDU.UserInfoItemVar.MaxPDULength < 16384 Then
                            UserInfoStr.SetItemData(CUInt(16384))
                        Else
                            UserInfoStr.SetItemData(CUInt(CurMaxLength))
                            ReDim bytes(CurMaxLength - 1)
                        End If

                    Else
                        UserInfoStr.SetItemData(CUInt(16383))
                    End If
                    AAACPDU.UserInfoItemVar.AddUserData(UserInfoStr)
                    UserInfoStr = New UserInfoSubItem(&H52)
                    UserInfoStr.SetItemData(ImplementationClassUID)
                    AAACPDU.UserInfoItemVar.AddUserData(UserInfoStr)
                    UserInfoStr = New UserInfoSubItem(&H55)
                    UserInfoStr.SetItemData(ImplementationVersionName)
                    AAACPDU.UserInfoItemVar.AddUserData(UserInfoStr)
                    Dim sentmsg() As Byte = AAACPDU.CreateByteBuff()
                    stream.Write(sentmsg, 0, sentmsg.Length)
                    MS = "STA6"
                    bytes = Nothing
                Case "STA4"
                    If client.Connected Then
                        AARQPDU = New AAssociateRQ
                        AARQPDU.PDUType = 1
                        AARQPDU.CalledAETittle = CalledAETString
                        AARQPDU.CallingAETittle = CallingAETString
                        AARQPDU.AppCtxItemVar.AppContextName = DICOMApplicationContextName
                        Dim pc As New PreContextItem
                        If PreCtxID < 255 Then
                            PreCtxID += 2
                        End If
                        pc.PreCtxID = PreCtxID
                        pc.AbSyntax.AbstractSyntaxName = BasicGrayScalePrintManagementMetaSOPClassUID
                        Dim TS As New TransferSyntaxSubItem
                        TS.TransferSyntaxName = ImplicitLittleEndian
                        pc.TransSyntax.Add(TS)
                        AARQPDU.PreCtxItems.Add(pc)
                        pc = New PreContextItem
                        If PreCtxID < 255 Then
                            PreCtxID += 2
                        Else
                            PreCtxID = 1
                        End If
                        pc.PreCtxID = PreCtxID
                        pc.AbSyntax.AbstractSyntaxName = BasicAnnotationBoxSOPClassUID
                        TS = New TransferSyntaxSubItem
                        TS.TransferSyntaxName = ImplicitLittleEndian
                        pc.TransSyntax.Add(TS)
                        AARQPDU.PreCtxItems.Add(pc)
                        Dim userinfostr As New UserInfoSubItem(&H51)
                        AARQPDU.UserInfoItemVar.MaxPDULength = &H20000
                        AARQPDU.UserInfoItemVar.ImplementationClassUID = ImplementationClassUID
                        userinfostr.SetItemData(AARQPDU.UserInfoItemVar.MaxPDULength)
                        AARQPDU.UserInfoItemVar.AddUserData(userinfostr)
                        userinfostr = New UserInfoSubItem(&H52)
                        userinfostr.SetItemData(ImplementationClassUID)
                        AARQPDU.UserInfoItemVar.AddUserData(userinfostr)
                        userinfostr = New UserInfoSubItem(&H55)
                        userinfostr.SetItemData(ImplementationVersionName)
                        AARQPDU.UserInfoItemVar.AddUserData(userinfostr)

                        Dim sentmsg As Byte() = AARQPDU.CreateByteBuff()
                        stream.Write(sentmsg, 0, sentmsg.Length)
                        MS = "STA5"
                    End If
                Case "STA5"
                    u = New UnparsePDU(stream, bytes)
                    Select Case u.PDUType
                        Case 2
                            Dim nget As New N_GET
                            nget.RequestedSOPClassUID = PrinterSOPClassUID
                            nget.RequestedSOPInstanceUID = PrinterSOPInstanceUID
                            nget.MessageID = bMessageID
                            nget.DataSetType = NoDataSet
                            If PreCtxID < 255 Then
                                PreCtxID += 2
                            Else
                                PreCtxID = 1
                            End If
                            Dim sentmsg() As Byte = nget.CreateNGETRQCmd(PreCtxID)
                    End Select
                Case "STA6"
                    u = New UnparsePDU(stream, bytes)
                    Select Case u.PDUType
                        Case 1
                        Case 2
                        Case 3
                            Dim AA As New AAbort
                            AA.Source = SCPInitAbort
                            AA.Reason = UnexpectedPDU
                            Dim sentmsg() As Byte = AA.CreateByteBuff
                            stream.Write(sentmsg, 0, 10)
                        Case 4
                            Dim ParsedPDU As New PDataTFPDU(&H4)
                            ParsedPDU.Parse(u)
                            If (ParsedPDU.PDVContent.IsCommand) Then
                                Dim Idx As Int32 = ParsedPDU.PDVContent.DataTagSearch(CommandFieldTag)

                                If Idx < 0 Then
                                    Throw New System.Exception("Lỗi trong Data Command")
                                Else
                                    Dim de As New DataElement
                                    de = ParsedPDU.PDVContent.DataElements(Idx)
                                    Select Case BitConverter.ToUInt16(de.DataValue, 0)
                                        Case C_STORE_RQ
                                            Dim cscmd As New CSTORE

                                            Idx = ParsedPDU.PDVContent.DataTagSearch(DataSetTypeTag)
                                            If Idx < 0 Then
                                                Throw New System.Exception("Lỗi trong Data Command")
                                            Else
                                                de = ParsedPDU.PDVContent.DataElements(Idx)
                                                If BitConverter.ToUInt16(de.DataValue, 0) <> NoDataSet Then


                                                    Dim u1 As New UnparsePDU(stream, bytes)

                                                    Dim u2 As UnparsePDU = u1
                                                    While (Not u2.IsLast(5))

                                                        u2 = New UnparsePDU(stream, bytes)
                                                        Array.Resize(u1.Buff, u1.Length + u2.Length - 6)
                                                        Array.Copy(u2.Buff, 6, u1.Buff, u1.Length, u2.Length - 6)
                                                        u1.Length = u1.Buff.Length
                                                        u1.Buff(5) = u2.Buff(5)     'replace MCH
                                                        Dim lentmp() As Byte = BitConverter.GetBytes(u1.buffer.Length - 4)
                                                        Array.Reverse(lentmp)
                                                        Array.Copy(lentmp, u1.Buff, 4)
                                                    End While
                                                    Dim DataPDU As New PDataTFPDU(&H4)
                                                    DataPDU.Parse(u1)

                                                    'Code here
                                                    Dim filemf As New FileMetaFormat
                                                    Dim path As String = "D:\ab1.dcm"
                                                    'DataMatrix(0) chua Row
                                                    'DataMatrix(1) chua Col
                                                    'DataMatrix(2) chua Matrix (Row x Col) cua diem anh
                                                    Dim DataMatrix As New ArrayList
                                                    DataMatrix = filemf.CreateFile(DataPDU.PDVContent.DataElements, path)

                                                    'Create C-STORE-RSP
                                                    cscmd.Parse(ParsedPDU.PDVContent)
                                                    cscmd.MessageIDBeingRespondedTo = cscmd.MessageID
                                                    cscmd.DataSetType = NoDataSet
                                                    cscmd.Status = 0
                                                    Dim sentmsg As Byte() = cscmd.CreateCSTORERSPCMD(ParsedPDU.PDVContent.PresentationContextID)
                                                    stream.Write(sentmsg, 0, sentmsg.Length)
                                                End If
                                            End If
                                        Case C_FIND_RQ
                                            Dim cfcmd As New CFIND

                                            Idx = ParsedPDU.PDVContent.DataTagSearch(DataSetTypeTag)
                                            If Idx < 0 Then
                                                Throw New System.Exception("Lỗi trong Data Command")
                                            Else
                                                de = ParsedPDU.PDVContent.DataElements(Idx)
                                                If BitConverter.ToUInt16(de.DataValue, 0) <> NoDataSet Then
                                                    Dim u1 As New UnparsePDU(stream, bytes)
                                                    Dim DataPDU As New PDataTFPDU(&H4)
                                                    DataPDU.Parse(u1)
                                                    cfcmd.Parse(ParsedPDU.PDVContent)

                                                    'Your Code here
                                                    gPatientName = " "
                                                    Me.GetFindInfor(de, DataPDU, Idx, gPatientName, gPID, gStudyDate)
                                                    cls.CreateMessage(cfcmd, ParsedPDU, stream, gPID, gPatientName)


                                                End If
                                            End If
                                    End Select
                                End If
                            End If
                        Case 5
                            MS = "STA8"
                        Case 6
                            Dim AA As New AAbort
                            AA.Source = SCPInitAbort
                            AA.Reason = UnexpectedPDU
                            Dim sentmsg() As Byte = AA.CreateByteBuff
                            stream.Write(sentmsg, 0, 10)
                            MS = "STA13"

                    End Select
                Case "STA7"
                    u = New UnparsePDU(stream, bytes)
                    Select Case u.PDUType
                        Case 5
                            MS = "STA9"
                    End Select
                Case "STA8"
                    Select Case u.PDUType
                        Case 5
                            Dim ReleasePDU As New AReleaseRP
                            Dim sentmsg() As Byte = ReleasePDU.CreateByteBuff()
                            stream.Write(sentmsg, 0, sentmsg.Length)
                            MS = "STA13"
                        Case Else
                            MS = "STA13"

                    End Select

                Case "STA9"
                    Dim ReleasePDU As New AReleaseRP
                    Dim sentmsg() As Byte = ReleasePDU.CreateByteBuff()
                    stream.Write(sentmsg, 0, sentmsg.Length)
                    MS = "STA13"
                Case "STA13"
                    u = New UnparsePDU(stream, bytes)
                    Select Case u.PDUType
                        Case 1
                            Dim AA As New AAbort
                            AA.Source = SCPInitAbort
                            AA.Reason = UnexpectedPDU
                            stream.Write(AA.CreateByteBuff, 0, 10)
                        Case Else

                            MS = "STA1"
                            gPatientName = " "


                            'Case 0
                            '    MS = "STA1"
                            '    gPatientName = " "
                            'Case 1
                            '    Dim AA As New AAbort
                            '    AA.Source = SCPInitAbort
                            '    AA.Reason = UnexpectedPDU
                            '    stream.Write(AA.CreateByteBuff, 0, 10)
                            'Case 2
                            'Case 3
                            'Case 4
                            'Case 5
                            'Case 6
                            '    MS = "STA13"
                            'Case 7
                            '    'client.Close()
                            '    MS = "STA1"
                            '    gPatientName = " "

                    End Select
            End Select
            If Not client.Connected Then
                Select Case MS
                    Case "STA13"
                    Case "STA2"
                End Select
                'client.Close()
                MS = "STA1"
            End If
        Catch e As Exception
            'Console.WriteLine("SocketException: {0}", e)
            MS = "STA1"
            gPatientName = " "
        Finally
            'server.Stop()
        End Try


    End Sub 'Main

    Private Sub GetFindInfor(ByVal de As DataElement, ByVal DataPDU As PDataTFPDU, ByVal idx As Int32, _
                             ByRef sPatientName As String, ByRef sPID As String, ByRef sStudyDate As String)

        ' Xu ly PatientName
        idx = DataPDU.PDVContent.DataTagSearch(PatientNameTag)
        If idx < 0 Then
            Throw New System.Exception("Lỗi trong CFIND Data Set")
        Else
            de = New DataElement
            de = DataPDU.PDVContent.DataElements(idx)
            If Not de.DataValue Is Nothing Then
                Dim PatientName = System.Text.Encoding.ASCII.GetString(de.DataValue)
                If PatientName Is System.DBNull.Value Or PatientName = "" Then
                    sPatientName = " "
                Else
                    sPatientName = PatientName
                End If
            End If

        End If

        ' Xu ly PID
        Dim patientID As String
        idx = DataPDU.PDVContent.DataTagSearch(PIDTag)
        If idx < 0 Then
            Throw New System.Exception("Lỗi trong CFIND Data Set")
        Else
            de = New DataElement
            de = DataPDU.PDVContent.DataElements(idx)
            If Not de.DataValue Is Nothing Then
                patientID = System.Text.Encoding.ASCII.GetString(de.DataValue)
                If patientID Is System.DBNull.Value Or patientID = "" Then
                    patientID = " "
                Else
                    sPID = patientID
                End If
            End If
        End If

        ' Xu ly StudyDate
        'Dim pStudyDate As String
        'idx = DataPDU.PDVContent.DataTagSearch(StudyDateTag)
        'If idx < 0 Then
        '    Throw New System.Exception("Lỗi trong CFIND Data Set")
        'Else
        '    de = New DataElement
        '    de = DataPDU.PDVContent.DataElements(idx)
        '    If Not de.DataValue Is Nothing Then
        '        pStudyDate = System.Text.Encoding.ASCII.GetString(de.DataValue)
        '        If pStudyDate Is System.DBNull.Value Or pStudyDate = "" Then
        '            pStudyDate = " "
        '        Else
        '            sStudyDate = pStudyDate
        '        End If
        '    End If
        'End If

    End Sub
End Class 'MyTcpListener 