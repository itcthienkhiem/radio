Imports VIETBAIT.DICOMHelper
Imports VIETBAIT.DICOMPDU


Public Class N_ACTION
    Inherits Command
    Dim ActionTypeIDVal As UInt16

    Public Property ActionTypeID() As UShort
        Get
            Return ActionTypeIDVal
        End Get
        Set(ByVal value As UShort)
            ActionTypeIDVal = value
        End Set
    End Property

    Function CreateNACTIONRQCmd(ByVal PreCtxID As Byte) As Byte()
        Dim NACMDPDU As New PDataTFPDU(&H4)
        'Group Length Dataset
        'CFPDU.PDVContent.AddDataSet(CUInt(&H5A), CUShort(0), CUShort(Command.CommandGroupLengthTag))

        'Requested SOP Class Dataset
        NACMDPDU.PDVContent.AddDataSet(CStr(MyClass.RequestedSOPClassUID), CUInt(DICOMTag.RequestedSOPClassUIDTag))

        'Command Field Dataset
        NACMDPDU.PDVContent.AddDataSet(CUShort(CommandFieldConst.N_ACTION_RQ), CUInt(DICOMTag.CommandFieldTag))

        'Message ID Dataset
        NACMDPDU.PDVContent.AddDataSet(CUShort(MyBase.MessageID), CUInt(DICOMTag.MessageIDTag))

        'DataSet Type
        NACMDPDU.PDVContent.AddDataSet(CUShort(VIETBAIT.DICOMHelper.CommandFieldConst.NoDataSet), CUInt(DICOMTag.DataSetTypeTag))

        'Requested SOP Class Instance UID
        NACMDPDU.PDVContent.AddDataSet(CStr(MyClass.RequestedSOPInstanceUID), CUInt(DICOMTag.RequestedSOPInstanceUIDTag))

        NACMDPDU.PDVContent.AddDataSet(CUShort(MyClass.ActionTypeID), CUInt(DICOMTag.ActionTypeIDTag))


        NACMDPDU.PDVContent.CreateGroupLengthDataset(DICOMTag.CommandGroupLengthTag)

        Return NACMDPDU.CreateByteBuff(PreCtxID, True, True)

    End Function
End Class