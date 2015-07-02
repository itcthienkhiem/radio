Imports System.Net.Sockets
Imports System.Data.SqlClient
Imports VIETBAIT.WORKLIST.WorkList
Imports VIETBAIT.DICOMPDU

Namespace HIS
    Public Class clsPatient
        Public Function spGetPatientInfo(ByVal pPatient_ID As String, _
                                          ByVal pPatientName As String, _
                                          ByVal pFromDate As String, _
                                          ByVal pToDate As String, _
                                          ByVal pLoai As Int16, _
                                          ByRef dt As DataTable) As Boolean
            Dim cmd As New SqlCommand
            Try
                With cmd
                    .Connection = gv_WLConn
                    .CommandText = "spGetTestList"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add("pPatientID", SqlDbType.NVarChar, 20).Direction = ParameterDirection.Input
                    .Parameters("pPatientID").Value = pPatient_ID

                    .Parameters.Add("pPatientName", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Input
                    .Parameters("pPatientName").Value = pPatientName

                    .Parameters.Add("pFromDate", SqlDbType.NVarChar, 15).Direction = ParameterDirection.Input
                    .Parameters("pFromDate").Value = pFromDate

                    .Parameters.Add("pToDate", SqlDbType.NVarChar, 15).Direction = ParameterDirection.Input
                    .Parameters("pToDate").Value = pToDate

                    .Parameters.Add("pLoai", SqlDbType.SmallInt).Direction = ParameterDirection.Input
                    .Parameters("pLoai").Value = pLoai
                End With
                Dim adt As New SqlDataAdapter(cmd)
                adt.Fill(dt)
                Return True
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
                Return False
            End Try

        End Function

        Sub CreateMessage(ByVal cfcmd As CFIND, ByVal ParsedPDU As PDataTFPDU, ByVal stream As NetworkStream, _
                           ByVal sPID As String, ByVal sPatientName As String, ByVal CallingAETString As String)

            Try
                Dim dt As New DataTable
                Dim iCount, iPos As Int32
                Dim sPosition, pSex As String
                Dim DateNow As String = Format(Now, "yyyyMMdd")
                Dim TimeNow As String = Format(Now, "HHmmss")
                Dim cfmessagecmd As Byte() = {}
                Dim cfmessagedata As Byte() = {}
                Dim cfmessage() As Byte

                Dim DateNow2 = Format(Now.Day, "0#") & "/" & Format(Now.Month, "0#") & "/" & Now.Year

                If sPID Is DBNull.Value Then sPID = " "
                If sPID = "" Then sPID = " "

                If sPatientName Is DBNull.Value Then sPatientName = " "
                If sPatientName = "" Then sPatientName = " "


                If Me.spGetPatientInfo(sPID, sPatientName, DateNow2, DateNow2, 1, dt) Then
                    For iCount = 0 To dt.Rows.Count - 1
                        sPatientName = Bodau(dt.Rows(iCount).Item("Nosign_Name"))
                        If sPatientName.Length Mod 2 <> 0 Then
                            sPatientName = sPatientName & " "
                        End If
                        sPID = dt.Rows(iCount).Item("BARCODE")
                        Dim Patient_ID As String = dt.Rows(iCount).Item("Patient_ID") '& "1"
                        If sPID.Length Mod 2 <> 0 Then
                            sPID = sPID & " "
                        End If
                        If Patient_ID.Length Mod 2 = 1 Then
                            Patient_ID = String.Format("0{0}", Patient_ID)
                        End If
                        If dt.Rows(iCount).Item("SEX") = 0 Then
                            pSex = "F "
                        Else
                            pSex = "M "
                        End If

                        cfcmd.DataSetType = 0
                        cfcmd.Status = &HFF00
                        cfcmd.MessageIDBeingRespondedTo = cfcmd.MessageID
                        cfmessagecmd = cfcmd.CreateCFRSPCMD(ParsedPDU.PDVContent.PresentationContextID, True)

                        cfmessagedata = cfcmd.CreateCFRSPData(ParsedPDU.PDVContent, sPID, sPatientName, Patient_ID, pSex, _
                                                  DateNow, TimeNow, "VIETBAIT", True, dt.Rows(iCount).Item("YEAR_BIRTH").ToString, "CT", Patient_ID, CallingAETString)
                        'cfmessagedata = cfcmd.CreateCFRSPData(ParsedPDU.PDVContent.PresentationContextID, sPID, sPatientName, sPID, pSex, _
                        '                           DateNow, TimeNow, "VIETBAIT", True, dt.Rows(iCount).Item("YEAR_BIRTH").ToString)
                        If cfmessage Is Nothing Then
                            iPos = 0
                        Else
                            iPos = cfmessage.Length
                        End If
                        Array.Resize(cfmessage, iPos + cfmessagecmd.Length + cfmessagedata.Length)

                        If iPos = 0 Then
                            Array.Copy(cfmessagecmd, cfmessage, cfmessagecmd.Length)
                        Else
                            Array.Copy(cfmessagecmd, 0, cfmessage, iPos, cfmessagecmd.Length)
                        End If
                        Array.Copy(cfmessagedata, 0, cfmessage, iPos + cfmessagecmd.Length, cfmessagedata.Length)

                    Next
                    If Not cfmessage Is Nothing Then
                        stream.Write(cfmessage, 0, cfmessage.Length)
                    End If

                    cfcmd.Status = 0
                    cfcmd.DataSetType = VIETBAIT.DICOMHelper.CommandFieldConst.NoDataSet
                    cfmessage = cfcmd.CreateCFRSPCMD(ParsedPDU.PDVContent.PresentationContextID, True)
                    stream.Write(cfmessage, 0, cfmessage.Length)
                End If
            Catch ex As Exception
                Console.WriteLine(ex.ToString())
                Throw ex
            End Try

        End Sub

        Function spResetDBConnect() As Boolean
            Try

                If gv_WLConn.State = ConnectionState.Closed Then
                    gv_WLConn.ConnectionString = " packet size=4096;data source=" & DataBaseADr & _
                                                 ";persist security info=False;initial catalog= " & DataBaseID & _
                                                 " ;uid=" & UserName & ";pwd=" & Password
                    gv_WLConn.Open()
                End If

                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class
End Namespace