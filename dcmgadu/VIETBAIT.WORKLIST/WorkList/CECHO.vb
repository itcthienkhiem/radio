Imports VIETBAIT.DICOMHelper.CommandFieldConst
Imports VIETBAIT.DICOMPDU


Namespace WorkList
    Public Class CECHO
        Inherits Command
        Public Const PatientNameTag As UInt32 = &H100010

        Function CreateCECHORSPCMD(ByVal PreCtxID As Byte) As Byte()
            Dim CEPDU As New PDataTFPDU(&H4)

            'Affected SOP Class Dataset
            CEPDU.PDVContent.AddDataSet(CStr(MyClass.AffectedSOPClassUID), 2)

            'Command Field Dataset
            CEPDU.PDVContent.AddDataSet(CUShort(C_ECHO_RSP), &H100)

            'Message ID being responded to Dataset
            CEPDU.PDVContent.AddDataSet(CUShort(MyBase.MessageIDBeingRespondedTo), (&H120))

            'DataSet Type
            CEPDU.PDVContent.AddDataSet(CUShort(MyClass.DataSetType), &H800)

            'Status DataSet
            CEPDU.PDVContent.AddDataSet(CUShort(Status), &H900)

            CEPDU.PDVContent.CreateGroupLengthDataset(0)

            Return CEPDU.CreateByteBuff(PreCtxID, True, True)
        End Function
    End Class
End Namespace