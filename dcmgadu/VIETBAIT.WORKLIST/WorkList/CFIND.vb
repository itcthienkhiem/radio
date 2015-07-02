Imports System.Text
Imports System.Data.SqlClient
Imports VIETBAIT.DICOM.BASE
Imports VIETBAIT.DICOMHelper
Imports VIETBAIT.DICOMPDU
Imports VIETBAIT.WORKLIST.HIS

Namespace WorkList
    Public Class CFIND
        Inherits Command


        Function CreateCFRSPCMD (ByVal PreCtxID As Byte, ByVal IsLastFrag As Boolean) As Byte()
            Dim CFPDU As New PDataTFPDU (&H4)
            'Group Length Dataset            

            'Affected SOP Class Dataset
            CFPDU.PDVContent.AddDataSet (CStr (MyClass.AffectedSOPClassUID), (2))

            'Command Field Dataset
            CFPDU.PDVContent.AddDataSet (CUShort (CommandFieldConst.C_FIND_RSP), (&H100))

            'Message ID being responded to Dataset
            CFPDU.PDVContent.AddDataSet (CUShort (MyBase.MessageIDBeingRespondedTo), (&H120))

            'DataSet Type
            CFPDU.PDVContent.AddDataSet (CUShort (MyClass.DataSetType), (&H800))

            'Status DataSet
            CFPDU.PDVContent.AddDataSet (CUShort (Status), (&H900))

            CFPDU.PDVContent.CreateGroupLengthDataset(DICOMTag.CommandGroupLengthTag)
           
            Dim tmp As Byte() = CFPDU.CreateByteBuff(PreCtxID, True, IsLastFrag)
            Return tmp
        End Function

        Overloads Function CreateCFRSPData(ByVal pdvWorklist As PDVItem, ByVal AccessionNumber As String, _
                                 ByVal PatientName As String, ByVal PID As String, _
                                 ByVal sex As String, ByVal ScheduledDate As String, ByVal ScheduledTime As String, _
                                 ByVal SPSDesc As String, ByVal IsLastData As Boolean, ByVal DOB As String, _
                                 ByVal Modality As String) As Byte()

            'Dim arrTest As New ArrayList
            Dim tmp() As Byte = {}
            Dim PDVTMP() As Byte = {}
            Dim CFPDU As New PDataTFPDU(&H4)
            Dim cmdlen As UInt32 = 0
            Dim sCodeMeaning As String
            Dim cmd As New SqlCommand
            Dim dt As New DataTable
            With CFPDU.PDVContent
                Try
                    With cmd
                        .Connection = gv_WLConn
                        .CommandText = "spGetTestListFromPID"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add("@pID", SqlDbType.NVarChar, 20).Direction = ParameterDirection.Input
                        .Parameters("@pID").Value = PID

                    End With
                    Dim adt As New SqlDataAdapter(cmd)
                    adt.Fill(dt)
                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
                End Try
                'SOP Common

                .AddDataSet(CStr(UnicodeCharset), &H80005)
                .AddDataSet(DICOMHelper.SOP.GeneralPurposeWorklistInformationModelFindClassUID & "." & MessageIDBeingRespondedTo.ToString("D"), &H80016)
                Debug.WriteLine(DICOMHelper.SOP.GeneralPurposeWorklistInformationModelFindClassUID & "." & MessageIDBeingRespondedTo.ToString("D"))

                .AddDataSet(DICOMHelper.SOP.GeneralPurposeWorklistInformationModelFindInstanceUID & "." & Format(Now, "yyyyMMdd.hmmss." & (MessageIDBeingRespondedTo + 1).ToString("D")), &H80018)
                Debug.WriteLine(DICOMHelper.SOP.GeneralPurposeWorklistInformationModelFindInstanceUID & "." & Format(Now, "yyyyMMdd.hmmss." & (MessageIDBeingRespondedTo + 1).ToString("D")))
                .AddDataSet(CStr(AccessionNumber), &H80050)
                'If pdvWorklist.DataElementHashTable.Contains(&H80090) Then

                'End If
                .AddZeroLenDataSet(&H80090)
                'If pdvWorklist.DataElementHashTable.Contains(&H81110) Then

                'End If
                .AddZeroLenDataSet(&H81110)
                'If pdvWorklist.DataElementHashTable.Contains(&H81120) Then

                'End If
                .AddZeroLenDataSet(&H81120)



                'Patient Identification
                .AddDataSet(CStr(PatientName), &H100010)
                .AddDataSet(CStr(PID), &H100020)
                'Patient Demographic                 
                If DOB.Length > 4 Then
                    .AddDataSet(CStr(DOB.Substring(6, 4) & DOB.Substring(3, 2) & DOB.Substring(0, 2)), &H100030)
                Else
                    .AddDataSet(CStr(DOB & "0101"), &H100030)
                End If
                .AddDataSet(CStr(sex), &H100040)
                'If pdvWorklist.DataElementHashTable.Contains(&H101030) Then

                'End If
                .AddZeroLenDataSet(&H101030)
                'If pdvWorklist.DataElementHashTable.Contains(&H102000) Then

                'End If
                .AddZeroLenDataSet(&H102000)
                If pdvWorklist.DataElementHashTable.Contains(&H102110) Then

                End If
                .AddZeroLenDataSet(&H102110)
                If pdvWorklist.DataElementHashTable.Contains(&H1021C0) Then

                End If
                .AddZeroLenDataSet(&H1021C0)

                .AddDataSet(DICOMHelper.SOP.GeneralPurposeWorklistInformationModelFindInstanceUID & "." & Format(Now, "yyyyMMdd.hmmss." & (MessageIDBeingRespondedTo + 1).ToString("D")), &H20000D)

                If pdvWorklist.DataElementHashTable.Contains(&H321032) Then

                End If
                .AddZeroLenDataSet(&H321032)
                .AddDataSet(CStr("UNKNOWN"), &H321060)

                If pdvWorklist.DataElementHashTable.Contains(&H380010) Then

                End If
                .AddZeroLenDataSet(&H380010)
                If pdvWorklist.DataElementHashTable.Contains(&H380050) Then

                End If
                .AddZeroLenDataSet(&H380050)
                If pdvWorklist.DataElementHashTable.Contains(&H380300) Then

                End If
                .AddZeroLenDataSet(&H380300)
                If pdvWorklist.DataElementHashTable.Contains(&H380500) Then

                End If
                .AddZeroLenDataSet(&H380500)

                .AddDelimiterDataSet(&H400100)
                .AddDelimiterDataSet(&HFFFEE000UI)

                .AddDataSet(CStr(Modality), &H80060)
                If pdvWorklist.DataElementHashTable.Contains(&H321070) Then .AddZeroLenDataSet(&H321070)

                .AddDataSet(CStr(CallingAETString), &H400001)
                .AddDataSet(CStr(ScheduledDate), &H400002)
                .AddDataSet(CStr(ScheduledTime), &H400003)
                If pdvWorklist.DataElementHashTable.Contains(&H400006) Then

                End If
                .AddZeroLenDataSet(&H400006)
                dt = Nothing
                If Not dt Is Nothing Then
                    If Not IsDBNull(dt) Then
                        If dt.Rows.Count > 0 Then
                            .AddDelimiterDataSet(&H400008)
                            For Each row As DataRow In dt.Rows
                                .AddDelimiterDataSet(&HFFFEE000UI)
                                .AddDataSet(CStr(row("CodeValue")), &H80100)
                                .AddDataSet(CStr("VIETBAIT"), &H80102)
                                .AddDataSet(CStr(row("CodeMeaning")), &H80104)
                                .AddZeroLenDataSet(&HFFFEE00DUI)
                            Next
                            .AddZeroLenDataSet(&HFFFEE0DDUI)
                        Else
                            .AddDataSet(CStr("UNKNOWN"), &H400007UI)
                        End If
                    Else
                        .AddDataSet(CStr("UNKNOWN"), &H400007UI)
                    End If
                Else
                    .AddDataSet(CStr("UNKNOWN"), &H400007UI)
                End If
                .AddDataSet(CStr(PID), &H400009)
                If pdvWorklist.DataElementHashTable.Contains(&H400010) Then

                End If
                .AddZeroLenDataSet(&H400010)
                If pdvWorklist.DataElementHashTable.Contains(&H400011) Then

                End If
                .AddZeroLenDataSet(&H400011)
                If pdvWorklist.DataElementHashTable.Contains(&H400012) Then

                End If
                .AddZeroLenDataSet(&H400012)
                .AddZeroLenDataSet(&HFFFEE00DUI)
                .AddZeroLenDataSet(&HFFFEE0DDUI)

                .AddDataSet(CStr(AccessionNumber), &H401001)
                If pdvWorklist.DataElementHashTable.Contains(&H401003) Then

                End If
                .AddZeroLenDataSet(&H401003)
                If pdvWorklist.DataElementHashTable.Contains(&H401004) Then

                End If
                .AddZeroLenDataSet(&H401004)
                If pdvWorklist.DataElementHashTable.Contains(&H403001) Then

                End If
                .AddZeroLenDataSet(&H403001)


            End With

            tmp = CFPDU.CreateByteBuff(pdvWorklist.PresentationContextID, False, IsLastData)
            Return tmp
        End Function

        'Function SPSModule(ByVal htWorklist As Hashtable, ByVal dtSPSCode As DataTable) As List(Of DataElement)
        '    Dim arraySPSModule As New List(Of DataElement)
        '    If Not IsDBNull(dtSPSCode) Then
        '        If dtSPSCode.Rows.Count > 0 Then
        '            Dim de = New DataElement(&H400008UI, &HFFFFFFFFUI)
        '            arraySPSModule.Add(de)
        '            For Each row As DataRow In dtSPSCode.Rows
        '                Dim htCode As Hashtable
        '                'Dim de As DataElement
        '                de = New DataElement(&HFFFEE000UI, &HFFFFFFFFUI)
        '                arraySPSModule.Add(de)
        '                de = New DataElement(&H80100, CStr(row("CodeValue")))
        '                arraySPSModule.Add(de)
        '                de = New DataElement(&H80102, Encoding.ASCII.GetBytes("VIETBAIT"))
        '                arraySPSModule.Add(de)
        '                de = New DataElement(&HFFFEE00DUI, &H0)
        '                arraySPSModule.Add(de)
        '            Next
        '            de = New DataElement(&HFFFEE0DDUI, &H0)
        '            arraySPSModule.Add(de)
        '        Else
        '            Dim de = New DataElement(&H400007UI, Encoding.ASCII.GetBytes("UNKNOWN "))
        '            arraySPSModule.Add(de)
        '        End If
        '    End If
        '    Return arraySPSModule
        'End Function

        Overloads Function CreateCFRSPData(ByVal PreCtxID As Byte, ByVal AccessionNumber As String, ByVal PatientName As String, ByVal PID As String, _
                                  ByVal sex As String, ByVal ScheduledDate As String, ByVal ScheduledTime As String, _
                                  ByVal SPSDesc As String, ByVal IsLastData As Boolean, ByVal DOB As String) As Byte()

            'Dim arrTest As New ArrayList
            Dim tmp() As Byte = {}
            Dim PDVTMP() As Byte = {}
            Dim CFPDU As New PDataTFPDU(&H4)
            Dim cmdlen As UInt32 = 0
            Dim sCodeMeaning As String
            Dim cmd As New SqlCommand
            Dim dt As New DataTable
            With CFPDU.PDVContent
                Try
                    With cmd
                        .Connection = gv_WLConn
                        .CommandText = "spGetTestListFromPID"
                        .CommandType = CommandType.StoredProcedure
                        .Parameters.Add("@pID", SqlDbType.NVarChar, 20).Direction = ParameterDirection.Input
                        .Parameters("@pID").Value = PID

                    End With
                    Dim adt As New SqlDataAdapter(cmd)
                    adt.Fill(dt)

                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Exclamation)

                End Try

                .AddDataSet(CStr("ISO_IR 192"), &H80005)
                .AddDataSet(CStr("1.2.840.10008.5.1.4.31.1"), &H80016)
                .AddDataSet(CStr("1.2.826.0.1.3680043.2.1545.4.3.0.0"), &H80018)

                '.AddDataSet(CStr("DUNG"), CUShort(&H8), CUShort(&H90))

                '.AddDelimiterDataSet(&H8, &H1110)
                '.AddDelimiterDataSet(&HFFFE, &HE000)
                '.AddDataSet(CStr("1.2.840.10008.5.1.4.32.1"), CUShort(&H8), CUShort(&H1150))
                '.AddDataSet(CStr("1.3.46.670589.16.12.2.1.176.5360.62132.20070126.102459.1"), CUShort(&H8), _
                '             CUShort(&H1155))
                '.AddZeroLenDataSet(&HFFFE, &HE00D)
                '.AddZeroLenDataSet(&HFFFE, &HE0DD)

                .AddDataSet(CStr(ScheduledDate), &H80020)
                .AddDataSet(CStr(ScheduledTime), &H80030)
                .AddDataSet(CStr(AccessionNumber), &H80050)
                .AddDataSet(CStr(PatientName), &H100010)
                .AddDataSet(CStr(PID), &H100020)
                If DOB.Length > 4 Then
                    .AddDataSet(CStr(DOB.Substring(6, 4) & DOB.Substring(3, 2) & DOB.Substring(0, 2)), &H100030)
                Else
                    .AddDataSet(CStr(DOB) & "0101", &H100030)
                End If


                .AddDataSet(sex, &H100040)
                '.AddDataSet(CStr("F"), CUShort(&H10), CUShort(&H40))
                '.AddDataSet(CStr("180"), CUShort(&H10), CUShort(&H1020))
                '.AddDataSet(CStr("80"), CUShort(&H10), CUShort(&H1030))
                '.AddDataSet(CStr("1.3.46.670589.16.12.2.1.176.5360.62132.20070126.102459.1"), CUShort(&H20), CUShort(&HD))
                '.AddDataSet(CStr("DUNG"), CUShort(&H32), CUShort(&H1032))
                '.AddDataSet(CStr("ward1"), CUShort(&H32), CUShort(&H1033))
                '.AddDataSet(CStr("Chest PA"), CUShort(&H32), CUShort(&H1060))

                .AddDelimiterDataSet(&H400100)
                .AddDelimiterDataSet(&HFFFEE000UI)
                .AddDataSet(CStr("DR"), &H80060)
                .AddZeroLenDataSet(&H321070)
                .AddDataSet(CStr(CallingAETString), &H400001)
                .AddDataSet(CStr(ScheduledDate), &H400002)
                .AddDataSet(CStr(ScheduledTime), &H400003)
                .AddZeroLenDataSet(&H400006)
                '.AddDataSet(CStr("Chest PA"), CUShort(&H40), CUShort(&H7))
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        .AddDelimiterDataSet(&H400008)
                        '.AddDelimiterDataSet(&HFFFE, &HE000)
                        For Each row As DataRow In dt.Rows
                            .AddDelimiterDataSet(&HFFFEE000UI)
                            .AddDataSet(CStr(row("CodeValue")), &H80100)
                            .AddDataSet(CStr("VIETBAIT"), (&H80102))
                            .AddDataSet(CStr(row("CodeMeaning")), (&H80104))
                            .AddZeroLenDataSet(&HFFFEE00DUI)
                        Next

                        '.AddZeroLenDataSet(&HFFFE, &HE00D)
                        .AddZeroLenDataSet(&HFFFEE0DDUI)
                    End If
                End If
                
                '.AddDataSet(CStr("100"), CUShort(&H40), CUShort(&H9))
                '.AddDataSet(CStr("Bach Mai"), CUShort(&H40), CUShort(&H10))
                .AddDataSet(CStr("CDHA"), (&H400011))
                .AddZeroLenDataSet(&H400012)
                .AddZeroLenDataSet(&HFFFEE00DUI)
                .AddZeroLenDataSet(&HFFFEE0DDUI)

                'Add body part
                '.AddDataSet("CHESTPA", CUShort(&H40), CUShort(&H1001))
                '.AddDataSet(CStr("Chest PA"), CUShort(&H8), CUShort(&H104))


                '.AddDataSet(CStr("1"), CUShort(&H40), CUShort(&H1001))
                .AddZeroLenDataSet(&H401002)
            End With
            CFPDU.PDUType = &H4
            CFPDU.PDVContent.PresentationContextID = PreCtxID
            CFPDU.PDVContent.Command(False)
            CFPDU.PDVContent.Last(IsLastData)
            tmp = CFPDU.CreateByteBuff()
            Return tmp
        End Function
    End Class
End Namespace