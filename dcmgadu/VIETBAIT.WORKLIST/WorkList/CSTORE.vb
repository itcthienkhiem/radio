Imports VIETBAIT.DICOMPDU

Namespace WorkList
    Public Class CSTORE
        Inherits Command

        Function CreateCSTORERSPCMD(ByVal PreCtxID As Byte) As Byte()
            Dim CFPDU As New PDataTFPDU(&H4)
            'Group Length Dataset
            'CFPDU.PDVContent.AddDataSet(CUInt(&H80), CUShort(0), CUShort(0))

            'Affected SOP Class Dataset
            CFPDU.PDVContent.AddDataSet(CStr(MyClass.AffectedSOPClassUID), 2)

            'Command Field Dataset
            CFPDU.PDVContent.AddDataSet(CUShort(VIETBAIT.DICOMHelper.CommandFieldConst.C_STORE_RSP), &H100)

            'Message ID being responded to Dataset
            CFPDU.PDVContent.AddDataSet(CUShort(MyBase.MessageIDBeingRespondedTo), (&H120))

            'DataSet Type
            CFPDU.PDVContent.AddDataSet(CUShort(MyClass.DataSetType), (&H800))

            'Status DataSet
            CFPDU.PDVContent.AddDataSet(CUShort(Status), (&H900))

            'Affected SOP Class Instance UID
            CFPDU.PDVContent.AddDataSet(CStr(MyClass.AffectedSOPInstanceUID), (&H1000))

            CFPDU.PDVContent.CreateGroupLengthDataset(0)


            Return CFPDU.CreateByteBuff(PreCtxID, True, True)
        End Function
    End Class
End Namespace