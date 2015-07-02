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
                                 ByVal Modality As String, ByVal Patient_ID As String, ByVal CallingAETString As String) As Byte()

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
                        .Parameters("@pID").Value = AccessionNumber
                        .Parameters.Add("@pAETITLE", SqlDbType.NVarChar, 20).Direction = ParameterDirection.Input
                        .Parameters("@pAETITLE").Value = CallingAETString

                    End With
                    Dim adt As New SqlDataAdapter(cmd)
                    adt.Fill(dt)
                    'If dt.Rows.Count = 0 Then Exit Function
                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
                End Try
                'SOP Common

                .AddDataSet(CStr(UnicodeCharset), &H80005UI)
                .AddDataSet(DICOMHelper.SOP.GeneralPurposeWorklistInformationModelFindClassUID & "." & Patient_ID, &H80016UI)
                'Debug.WriteLine(DICOMHelper.SOP.GeneralPurposeWorklistInformationModelFindClassUID & "." & Patient_ID)

                .AddDataSet(DICOMHelper.SOP.GeneralPurposeWorklistInformationModelFindInstanceUID & "." & Patient_ID, &H80018UI)
                'Debug.WriteLine(DICOMHelper.SOP.GeneralPurposeWorklistInformationModelFindInstanceUID & "." & Format(Now, "yyyyMMdd.") & "01")
                .AddDataSet(CStr(AccessionNumber), &H80050UI)
                If pdvWorklist.DataElementHashTable.Contains(&H80090UI) Then .AddZeroLenDataSet(&H80090UI)
                If pdvWorklist.DataElementHashTable.Contains(&H81110UI) Then .AddZeroLenDataSet(&H81110UI)
                If pdvWorklist.DataElementHashTable.Contains(&H81120UI) Then .AddZeroLenDataSet(&H81120UI)



                'Patient Identification
                .AddDataSet(CStr(PatientName), &H100010UI)
                .AddDataSet(CStr(PID), &H100020UI)
                'Patient Demographic                 
                If DOB.Length > 4 Then
                    .AddDataSet(CStr(DOB.Substring(6, 4) & DOB.Substring(3, 2) & DOB.Substring(0, 2)), &H100030UI)
                Else
                    .AddDataSet(CStr(DOB & "0101"), &H100030UI)
                End If
                .AddDataSet(CStr(sex), &H100040UI)
                If pdvWorklist.DataElementHashTable.Contains(&H101030UI) Then .AddZeroLenDataSet(&H101030UI)
                If pdvWorklist.DataElementHashTable.Contains(&H102000UI) Then .AddZeroLenDataSet(&H102000UI)
                If pdvWorklist.DataElementHashTable.Contains(&H102110UI) Then .AddZeroLenDataSet(&H102110UI)
                If pdvWorklist.DataElementHashTable.Contains(&H1021C0UI) Then .AddZeroLenDataSet(&H1021C0UI)

                .AddDataSet(DICOMHelper.SOP.GeneralPurposeWorklistInformationModelFindInstanceUID & "." & Patient_ID, &H20000D)

                If pdvWorklist.DataElementHashTable.Contains(&H321032UI) Then .AddZeroLenDataSet(&H321032)
                .AddDataSet(CStr("UNKNOWN"), &H321060)

                If pdvWorklist.DataElementHashTable.Contains(&H380010UI) Then .AddZeroLenDataSet(&H380010UI)
                If pdvWorklist.DataElementHashTable.Contains(&H380050UI) Then .AddZeroLenDataSet(&H380050UI)
                If pdvWorklist.DataElementHashTable.Contains(&H380300UI) Then .AddZeroLenDataSet(&H380300UI)
                If pdvWorklist.DataElementHashTable.Contains(&H380500UI) Then .AddZeroLenDataSet(&H380500UI)

                .AddDelimiterDataSet(&H400100UI)
                .AddDelimiterDataSet(&HFFFEE000UI)

                .AddDataSet(CStr(Modality), &H80060UI)
                If pdvWorklist.DataElementHashTable.Contains(&H321070UI) Then .AddZeroLenDataSet(&H321070UI)

                .AddDataSet(CStr(CallingAETString), &H400001UI)
                .AddDataSet(CStr(ScheduledDate), &H400002UI)
                .AddDataSet(CStr(ScheduledTime), &H400003UI)
                If pdvWorklist.DataElementHashTable.Contains(&H400006UI) Then .AddZeroLenDataSet(&H400006UI)

                If Not IsDBNull(dt) Then
                    If dt.Rows.Count > 0 Then
                        .AddDelimiterDataSet(&H400008UI)
                        Dim comment As String = String.Empty
                        For Each row As DataRow In dt.Rows
                            .AddDelimiterDataSet(&HFFFEE000UI)
                            .AddDataSet(CStr(row("CodeValue")), &H80100UI)
                            .AddDataSet(CStr("VIETBAIT"), &H80102UI)
                            Dim sData As String = CStr(row("CodeMeaning"))
                            comment = comment + IIf(comment = "", sData, vbCrLf + sData)
                            .AddDataSet(sData, &H80104UI)
                            .AddZeroLenDataSet(&HFFFEE00DUI)
                        Next
                        .AddZeroLenDataSet(&HFFFEE0DDUI)
                        If (comment.Length > 63) Then
                            comment = comment.Substring(0, 60)
                        End If
                        .AddDataSet(comment, &H400007UI)
                    Else
                        .AddDataSet(CStr("UNKNOWN"), &H400007UI)
                    End If
                End If
                .AddDataSet(CStr(PID), &H400009UI)
                If pdvWorklist.DataElementHashTable.Contains(&H400010UI) Then .AddZeroLenDataSet(&H400010UI)
                If pdvWorklist.DataElementHashTable.Contains(&H400011UI) Then .AddZeroLenDataSet(&H400011UI)
                If pdvWorklist.DataElementHashTable.Contains(&H400012UI) Then .AddZeroLenDataSet(&H400012UI)
                .AddZeroLenDataSet(&HFFFEE00DUI)
                .AddZeroLenDataSet(&HFFFEE0DDUI)
                .AddDataSet(CStr(AccessionNumber), &H401001UI)
                If pdvWorklist.DataElementHashTable.Contains(&H401003UI) Then .AddZeroLenDataSet(&H401003UI)
                If pdvWorklist.DataElementHashTable.Contains(&H401004UI) Then .AddZeroLenDataSet(&H401004UI)
                If pdvWorklist.DataElementHashTable.Contains(&H403001UI) Then .AddZeroLenDataSet(&H403001UI)
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