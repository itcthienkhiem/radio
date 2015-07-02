Imports VIETBAIT.DICOMHelper
Imports VIETBAIT.DICOMPDU
Imports VIETBAIT.DICOMHelper.CommandFieldConst

Public Class N_DELETE
    Inherits Command

    Function CreateNDELETERQCmd(ByVal PreCtxID As Byte) As Byte()
        Dim NDCMDPDU As New PDataTFPDU(&H4)
        'Group Length Dataset
        'CFPDU.PDVContent.AddDataSet(CUInt(&H5A), CUShort(0), CUShort(Command.CommandGroupLengthTag))

        'Requested SOP Class Dataset
        NDCMDPDU.PDVContent.AddDataSet(CStr(MyClass.RequestedSOPClassUID), CUInt(DICOMTag.RequestedSOPClassUIDTag))

        'Command Field Dataset
        NDCMDPDU.PDVContent.AddDataSet(CUShort(N_DELETE_RQ), CUInt(DICOMTag.CommandFieldTag))

        'Message ID Dataset
        NDCMDPDU.PDVContent.AddDataSet(CUShort(MessageID), CUInt(DICOMTag.MessageIDTag))

        'DataSet Type
        NDCMDPDU.PDVContent.AddDataSet(CUShort(DataSetType), CUInt(DICOMTag.DataSetTypeTag))

        'Requested SOP Class Instance UID
        NDCMDPDU.PDVContent.AddDataSet(CStr(MyClass.RequestedSOPInstanceUID), CUInt(DICOMTag.RequestedSOPInstanceUIDTag))


        NDCMDPDU.PDVContent.CreateGroupLengthDataset(DICOMTag.CommandGroupLengthTag)

        Return NDCMDPDU.CreateByteBuff(PreCtxID, True, True)

    End Function
End Class