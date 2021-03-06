Imports System.Net.Sockets
Imports System.Timers
Imports System.Net
Imports System.Text
Imports VIETBAIT.DICOMHelper
Imports VIETBAIT.DICOMHelper.CommandFieldConst
Imports VIETBAIT.DICOMHelper.DICOMTag
Imports VIETBAIT.AASSOCIATE
Imports VIETBAIT.WORKLIST.HIS
Imports VIETBAIT.DICOMPDU
Imports VIETBAIT.DICOM.BASE

Namespace WorkList
    Class MyTcpListener
        Private Shared client As New TcpClient
        Private Shared ARTIMER As Timer

        Public Sub FMain() 'ByVal client As TcpClient)
            Dim txtAET, txtIPAddress, txtPort, txtPath As String
            txtAET = CallingAETString
            txtIPAddress = "192.168.1.172"
            txtPort = "104"
            txtPath = "c:\AB1.DCM"

            Dim cls As New clsPatient
            'GotIt = True
            If GotIt Then
                AARequest = True
                GotIt = False
            End If

            Try
                ARTIMER = New Timer (5000)
                AddHandler ARTIMER.Elapsed, AddressOf OnTimerElapsed

                ' Get a stream object for reading and writing
                While True
                    Select Case GlobalModule.MS
                        Case "STA1"
                            If AARequest Then
                                client = New TcpClient
                                client.Connect (IPAddress.Parse (txtIPAddress), CUInt (txtPort))
                                CalledAETString = txtAET
                                GlobalModule.MS = "STA4"
                                stream = client.GetStream()
                            Else
                                client = server.AcceptTcpClient()
                                GlobalModule.MS = "STA2"
                                ARTIMER.Stop()
                                ARTIMER.Start()
                                stream = client.GetStream()
                            End If

                        Case "STA2"
                            u = New UnparsePDU (stream, bytes)
                            If u.PDUType = &H1 Then
                                AARQPDU = New AAssociateRQ (u.Length)
                                'AARQPDU.PDUType = &H1
                                'AARQPDU.Length = u.Length
                                AARQPDU.AAssociateRQParse (u.buffer)
                                CurMaxLength = AARQPDU.UserInfoItemVar.MaxPDULength
                                ARTIMER.Stop()
                                GlobalModule.MS = "STA3"
                            End If

                        Case "STA3"
                            Dim AAACPDU As New AAssociateAC (AARQPDU.CalledAETittle, AARQPDU.CallingAETittle)
                            Dim UserInfoStr As UserInfoSubItem

                            CallingAETString = AARQPDU.CallingAETittle
                            AAACPDU.AppCtxItemVar.AppContextName = AARQPDU.AppCtxItemVar.AppContextName
                            For Each p As PreContextItem In AARQPDU.PreCtxItems
                                Dim p_ac As New PreContextItem
                                p_ac.PreCtxID = p.PreCtxID
                                p_ac.Result = Accepted
                                Dim TS As New TransferSyntaxSubItem
                                TS.TransferSyntaxName = SOP.ImplicitLittleEndian
                                p_ac.TransSyntax.Add (TS)
                                AAACPDU.PreCtxItemVar.Add (p_ac)
                            Next
                            UserInfoStr = New UserInfoSubItem (&H51)
                            If AARQPDU.UserInfoItemVar.MaxPDULength > 0 Then
                                If AARQPDU.UserInfoItemVar.MaxPDULength < 16384 Then
                                    UserInfoStr.SetItemData (CUInt (16384))
                                Else
                                    UserInfoStr.SetItemData (CUInt (CurMaxLength))
                                    ReDim bytes(CurMaxLength - 1)
                                End If
                            Else
                                UserInfoStr.SetItemData (CUInt (16383))
                            End If
                            AAACPDU.UserInfoItemVar.AddUserData (UserInfoStr)

                            UserInfoStr = New UserInfoSubItem (&H52)
                            UserInfoStr.SetItemData(SOP.ImplementationClassUID)
                            AAACPDU.UserInfoItemVar.AddUserData (UserInfoStr)
                            UserInfoStr = New UserInfoSubItem (&H55)
                            UserInfoStr.SetItemData (ImplementationVersionName)
                            AAACPDU.UserInfoItemVar.AddUserData (UserInfoStr)

                            Dim sentmsg() As Byte = AAACPDU.CreateByteBuff()
                            stream.Write (sentmsg, 0, sentmsg.Length)
                            GlobalModule.MS = "STA6"
                            bytes = Nothing
                        Case "STA4"
                            If client.Connected Then
                                AARQPDU = New AAssociateRQ
                                AARQPDU.PDUType = 1
                                AARQPDU.CalledAETittle = CalledAETString
                                AARQPDU.CallingAETittle = CallingAETString
                                AARQPDU.AppCtxItemVar.AppContextName = SOP.DICOMApplicationContextName
                                Dim pc As New PreContextItem

                                pc.PreCtxID = PreCtxID
                                pc.AbSyntax.AbstractSyntaxName = SOP.BasicGrayScalePrintManagementMetaSOPClassUID
                                Dim TS As New TransferSyntaxSubItem
                                TS.TransferSyntaxName = SOP.ImplicitLittleEndian
                                pc.TransSyntax.Add (TS)
                                AARQPDU.PreCtxItems.Add (pc)
                                pc = New PreContextItem


                                pc.PreCtxID = PreCtxID + 2
                                pc.AbSyntax.AbstractSyntaxName = SOP.BasicAnnotationBoxSOPClassUID
                                TS = New TransferSyntaxSubItem
                                TS.TransferSyntaxName = SOP.ImplicitLittleEndian
                                pc.TransSyntax.Add (TS)
                                AARQPDU.PreCtxItems.Add (pc)
                                Dim userinfostr As New UserInfoSubItem (&H51)
                                CurMaxLength = &H20000
                                ReDim bytes(CurMaxLength - 1)
                                AARQPDU.UserInfoItemVar.MaxPDULength = CurMaxLength
                                AARQPDU.UserInfoItemVar.ImplementationClassUID = SOP.ImplementationClassUID
                                userinfostr.SetItemData (AARQPDU.UserInfoItemVar.MaxPDULength)
                                AARQPDU.UserInfoItemVar.AddUserData (userinfostr)
                                userinfostr = New UserInfoSubItem (&H52)
                                userinfostr.SetItemData(SOP.ImplementationClassUID)
                                AARQPDU.UserInfoItemVar.AddUserData (userinfostr)
                                userinfostr = New UserInfoSubItem (&H55)
                                userinfostr.SetItemData (ImplementationVersionName)
                                AARQPDU.UserInfoItemVar.AddUserData (userinfostr)

                                Dim sentmsg As Byte() = AARQPDU.CreateByteBuff()
                                stream.Write (sentmsg, 0, sentmsg.Length)
                                GlobalModule.MS = "STA5"
                            End If
                        Case "STA5"
                            u = New UnparsePDU (stream, bytes)
                            Select Case u.PDUType
                                Case 2
                                    GlobalModule.MS = "STA6"
                            End Select
                        Case "STA6"
                            If AARequest Then
                                'Dim nget As New N_GET
                                'nget.RequestedSOPClassUID = PrinterSOPClassUID
                                'nget.RequestedSOPInstanceUID = PrinterSOPInstanceUID
                                'nget.MessageID = bMessageID
                                'nget.DataSetType = NoDataSet


                                'Dim sentmsg() As Byte
                                ''sentmsg = nget.CreateNGETRQCmd(PreCtxID)
                                ''stream.Write(sentmsg, 0, sentmsg.Length)
                                'u = New UnparsePDU (stream, bytes)
                                'u = New UnparsePDU (stream, bytes)
                                'Select Case u.PDUType
                                '    Case 4
                                '        Dim NCREATE As New N_CREATE
                                '        NCREATE.AffectedSOPClassUID = SOP.BasicFilmSessionSOPClassUID
                                '        bMessageID += 1
                                '        NCREATE.MessageID = bMessageID
                                '        NCREATE.DataSetType = &H100
                                '        sentmsg = NCREATE.CreateNCREATERQCmd (PreCtxID)
                                '        stream.Write (sentmsg, 0, sentmsg.Length)
                                '        NCREATE.NumberOfCopies = "1 "
                                '        sentmsg = NCREATE.NCREATEFilmSessionDataRQ (PreCtxID)
                                '        stream.Write (sentmsg, 0, sentmsg.Length)

                                'End Select
                                'u = New UnparsePDU (stream, bytes)
                                ''u = New UnparsePDU(stream, bytes)
                                'Select Case u.PDUType
                                '    Case 4
                                '        Dim NCREATE As New N_CREATE
                                '        NCREATE.AffectedSOPClassUID = Ultility.BasicFilmBoxSOPClassUID
                                '        bMessageID += 1
                                '        NCREATE.MessageID = bMessageID
                                '        NCREATE.DataSetType = &H100
                                '        sentmsg = NCREATE.CreateNCREATERQCmd (PreCtxID)
                                '        stream.Write (sentmsg, 0, sentmsg.Length)
                                '        NCREATE.ImageDisplayFormat = "STANDARD\1,1"
                                '        NCREATE.FilmSizeID = "8INX10IN"
                                '        NCREATE.BorderDensity = "BLACK "
                                '        NCREATE.ConfigurationInfor = "128"
                                '        sentmsg = NCREATE.NCREATEFilmBoxDataRQ (PreCtxID)
                                '        stream.Write (sentmsg, 0, sentmsg.Length)

                                'End Select
                                'u = New UnparsePDU (stream, bytes)

                                'u = New UnparsePDU (stream, bytes)

                                'Select Case u.PDUType


                                '    Case 4
                                '        Dim NCreateDataRSP As New PDataTFPDU (&H4)
                                '        NCreateDataRSP.Parse (u)

                                '        Dim de As DataElement = NCreateDataRSP.PDVContent.DataTagSearch(&H81155)

                                '        If de Is Nothing Then
                                '            Throw New Exception("Lỗi trong Data Command")
                                '        Else

                                '            PrinterReferenceInstanceUID = Encoding.ASCII.GetString(de.DataValue)
                                '        End If


                                '        Dim NSET As New N_SET
                                '        NSET.RequestedSOPClassUID = SOP.BasicGrayScaleImageSOPClassUID
                                '        bMessageID += 1
                                '        NSET.MessageID = bMessageID
                                '        NSET.DataSetType = &H100
                                '        NSET.RequestedSOPInstanceUID = PrinterReferenceInstanceUID
                                '        sentmsg = NSET.CreateNSETRQCmd (PreCtxID)


                                '        stream.Write (sentmsg, 0, sentmsg.Length)
                                '        NSET.ImagePosition = 1
                                '        Dim arrayofdataset As New ArrayList
                                '        Dim fs As New FileMetaFormat
                                '        arrayofdataset = fs.ReadDCMFile (txtPath)
                                '        NSET.WriteTo (stream, NSET.CreateNSETBuffer (arrayofdataset), PreCtxID)

                                'End Select


                                'u = New UnparsePDU (stream, bytes)
                                'Select Case u.PDUType
                                '    Case 4
                                '        Dim NACTION As New N_ACTION
                                '        NACTION.RequestedSOPClassUID = SOP.BasicFilmBoxSOPClassUID
                                '        bMessageID += 1
                                '        NACTION.MessageID = bMessageID
                                '        NACTION.DataSetType = CommandFieldConst.NoDataSet
                                '        NACTION.RequestedSOPInstanceUID = "1.2.826.0.1.3680043.2.1211.9.1.1"
                                '        NACTION.ActionTypeID = CUShort (&H1)
                                '        sentmsg = NACTION.CreateNACTIONRQCmd (PreCtxID)
                                '        stream.Write (sentmsg, 0, sentmsg.Length)
                                'End Select
                                'u = New UnparsePDU (stream, bytes)
                                'Select Case u.PDUType
                                '    Case 4
                                '        Dim NDELETE As New N_DELETE
                                '        NDELETE.RequestedSOPClassUID = SOP.BasicFilmSessionSOPClassUID
                                '        bMessageID += 1
                                '        NDELETE.MessageID = bMessageID
                                '        NDELETE.DataSetType = CommandFieldConst.NoDataSet
                                '        NDELETE.RequestedSOPInstanceUID = "1.2.826.0.1.3680043.2.1211.9.1"
                                '        sentmsg = NDELETE.CreateNDELETERQCmd (PreCtxID)
                                '        stream.Write (sentmsg, 0, sentmsg.Length)
                                'End Select
                                'u = New UnparsePDU (stream, bytes)
                                'Select Case u.PDUType
                                '    Case 4
                                '        Dim ARRQ As New AReleaseRQ
                                '        sentmsg = ARRQ.CreateByteBuff
                                '        stream.Write (sentmsg, 0, sentmsg.Length)
                                '        GlobalModule.MS = "STA7"
                                'End Select

                            Else
                                u = New UnparsePDU (stream, bytes)
                                Select Case u.PDUType
                                    Case 1
                                    Case 2
                                    Case 3
                                        Dim AA As New AAbort
                                        AA.Source = SCPInitAbort
                                        AA.Reason = UnexpectedPDU
                                        Dim sentmsg() As Byte = AA.CreateByteBuff
                                        stream.Write (sentmsg, 0, 10)
                                    Case 4
                                        Dim ParsedPDU As New PDataTFPDU (&H4)
                                        ParsedPDU.Parse (u)
                                        If (ParsedPDU.PDVContent.IsCommand) Then


                                            If Not ParsedPDU.PDVContent.DataElementHashTable.ContainsKey(DICOMTag.CommandFieldTag) Then

                                                Throw New Exception("Lỗi trong Data Command")
                                            Else
                                                Dim d As Byte() = ParsedPDU.PDVContent.DataElementHashTable(DICOMTag.CommandFieldTag)
                                                
                                                Select Case BitConverter.ToUInt16(d, 0)
                                                    Case C_ECHO_RQ
                                                        Dim cecmd As New CECHO

                                                        'Create C-STORE-RSP
                                                        cecmd.Parse(ParsedPDU.PDVContent)
                                                        cecmd.MessageIDBeingRespondedTo = cecmd.MessageID
                                                        cecmd.DataSetType = CommandFieldConst.NoDataSet
                                                        cecmd.Status = 0
                                                        Dim sentmsg As Byte() = cecmd.CreateCECHORSPCMD(ParsedPDU.PDVContent.PresentationContextID)
                                                        stream.Write(sentmsg, 0, sentmsg.Length)

                                                    Case C_STORE_RQ
                                                        Dim cscmd As New CSTORE

                                                        Dim de As DataElement = ParsedPDU.PDVContent.DataTagSearch(DataSetTypeTag)
                                                        If de Is Nothing Then
                                                            Throw New Exception("Lỗi trong Data Command")
                                                        Else

                                                            If BitConverter.ToUInt16(de.DataValue, 0) <> CommandFieldConst.NoDataSet Then
                                                                Dim u1 As New UnparsePDU(stream, bytes)
                                                                Dim u2 As UnparsePDU = u1
                                                                While (Not u2.IsLast(5))
                                                                    u2 = New UnparsePDU(stream, bytes)
                                                                    Array.Resize(u1.Buff, u1.Length + u2.Length - 6)
                                                                    Array.Copy(u2.Buff, 6, u1.Buff, u1.Length, _
                                                                                u2.Length - 6)
                                                                    u1.Length = u1.Buff.Length
                                                                    u1.Buff(5) = u2.Buff(5)
                                                                    'replace MCH
                                                                    Dim lentmp() As Byte = _
                                                                            BitConverter.GetBytes(u1.buffer.Length - 4)
                                                                    Array.Reverse(lentmp)
                                                                    Array.Copy(lentmp, u1.Buff, 4)
                                                                End While
                                                                Dim DataPDU As New PDataTFPDU(&H4)
                                                                DataPDU.Parse(u1)

                                                                'Code here
                                                                Dim filemf As New FileMetaFormat
                                                                Dim path As String = "C:\ab1.dcm"
                                                                'DataMatrix(0) chua Row
                                                                'DataMatrix(1) chua Col
                                                                'DataMatrix(2) chua Matrix (Row x Col) cua diem anh
                                                                Dim DataMatrix As New ArrayList
                                                                DataMatrix = _
                                                                    filemf.CreateFile(DataPDU.PDVContent.DataElements, _
                                                                                       path)

                                                                'Create C-STORE-RSP
                                                                cscmd.Parse(ParsedPDU.PDVContent)
                                                                cscmd.MessageIDBeingRespondedTo = cscmd.MessageID
                                                                cscmd.DataSetType = CommandFieldConst.NoDataSet
                                                                cscmd.Status = 0
                                                                Dim _
                                                                    sentmsg As Byte() = _
                                                                        cscmd.CreateCSTORERSPCMD( _
                                                                                                  ParsedPDU.PDVContent. _
                                                                                                     PresentationContextID)
                                                                stream.Write(sentmsg, 0, sentmsg.Length)
                                                                GotIt = True
                                                            End If
                                                        End If
                                                    Case C_FIND_RQ
                                                        Dim cfcmd As New CFIND

                                                        Dim de = ParsedPDU.PDVContent.DataTagSearch(DataSetTypeTag)
                                                        If de Is Nothing Then
                                                            Throw New Exception("Lỗi trong Data Command")
                                                        Else

                                                            If BitConverter.ToUInt16(de.DataValue, 0) <> CommandFieldConst.NoDataSet Then
                                                                Dim u1 As New UnparsePDU(stream, bytes)
                                                                Dim DataPDU As New PDataTFPDU(&H4)
                                                                DataPDU.Parse(u1)
                                                                cfcmd.Parse(ParsedPDU.PDVContent)

                                                                'Your Code here
                                                                gPatientName = " "
                                                                gPID = " "

                                                                Me.GetFindInfor(de, DataPDU, gPatientName, gPID, gStudyDate)
                                                                cls.CreateMessage(cfcmd, DataPDU, stream, gPID, gPatientName, CallingAETString.Trim())
                                                            End If
                                                        End If
                                                End Select
                                            End If
                                        End If
                                    Case 5
                                        GlobalModule.MS = "STA8"
                                    Case 6
                                        Dim AA As New AAbort
                                        AA.Source = SCPInitAbort
                                        AA.Reason = UnexpectedPDU
                                        Dim sentmsg() As Byte = AA.CreateByteBuff
                                        stream.Write (sentmsg, 0, 10)
                                        GlobalModule.MS = "STA13"

                                End Select
                            End If
                        Case "STA7"
                            u = New UnparsePDU (stream, bytes)
                            Select Case u.PDUType
                                Case 5
                                    GlobalModule.MS = "STA9"
                                Case 6
                                    ARTIMER.Stop()
                                    client.Close()
                                    GlobalModule.MS = "STA1"
                                    AARequest = False
                                    'frmPrint.ShowDialog()
                            End Select
                        Case "STA8"
                            Select Case u.PDUType
                                Case 5
                                    Dim ReleasePDU As New AReleaseRP
                                    Dim sentmsg() As Byte = ReleasePDU.CreateByteBuff()
                                    stream.Write (sentmsg, 0, sentmsg.Length)
                                    ARTIMER.Stop()
                                    ARTIMER.Start()
                                    GlobalModule.MS = "STA13"
                                Case Else
                                    ARTIMER.Stop()
                                    ARTIMER.Start()
                                    GlobalModule.MS = "STA13"
                            End Select

                        Case "STA9"
                            Dim ReleasePDU As New AReleaseRP
                            Dim sentmsg() As Byte = ReleasePDU.CreateByteBuff()
                            stream.Write (sentmsg, 0, sentmsg.Length)
                            GlobalModule.MS = "STA13"
                            ARTIMER.Stop()
                            ARTIMER.Start()
                        Case "STA13"
                            u = New UnparsePDU (stream, bytes)
                            Select Case u.PDUType
                                Case 1
                                    Dim AA As New AAbort
                                    AA.Source = SCPInitAbort
                                    AA.Reason = UnexpectedPDU
                                    stream.Write (AA.CreateByteBuff, 0, 10)
                                Case 2
                                Case 3
                                Case 4
                                Case 5
                                Case 6
                                    GlobalModule.MS = "STA13"
                                Case 7
                                    ARTIMER.Stop()
                                    client.Close()
                                    GlobalModule.MS = "STA1"
                                    Exit While
                            End Select


                    End Select
                    If Not client.Connected Then
                        Select Case GlobalModule.MS
                            Case "STA13"
                            Case "STA2"
                        End Select
                        GlobalModule.MS = "STA1"
                        Exit While
                    End If
                End While
            Catch e As Exception
                GlobalModule.MS = "STA1"
                gPatientName = " "
            Finally
            End Try
        End Sub

        Private Shared Sub OnTimerElapsed (ByVal source As Object, ByVal e As ElapsedEventArgs)
            ARTIMER.Stop()
            client.Close()
            AARequest = False
            GlobalModule.MS = "STA1"
        End Sub

        Private Sub GetFindInfor(ByVal de As DataElement, ByVal DataPDU As PDataTFPDU, _
                                  ByRef sPatientName As String, ByRef sPID As String, ByRef sStudyDate As String)

            ' Xu ly PatientName
            de = DataPDU.PDVContent.DataTagSearch(PatientNameTag)
            If de Is Nothing Then
                Throw New Exception("Lỗi trong CFIND Data Set")
            Else

                If Not de.DataValue Is Nothing Then
                    Dim PatientName = Encoding.ASCII.GetString(de.DataValue)
                    If PatientName Is DBNull.Value Or PatientName = "" Then
                        sPatientName = " "
                    Else
                        sPatientName = PatientName
                    End If
                End If

            End If
            Dim patientID As String
            de = DataPDU.PDVContent.DataTagSearch(PIDTag)
            If de Is Nothing Then
                Throw New Exception("Lỗi trong CFIND Data Set")
            Else

                If Not de.DataValue Is Nothing Then
                    patientID = Encoding.ASCII.GetString(de.DataValue)
                    If patientID Is DBNull.Value Or patientID = "" Then
                        patientID = " "
                    Else
                        sPID = patientID
                    End If
                End If
            End If
        End Sub
    End Class
End Namespace