Imports VIETBAIT.DICOM.BASE
Imports VIETBAIT.DICOMHelper
Imports VIETBAIT.DICOMPDU
Imports VIETBAIT.DICOMHelper.Ultility
Imports VIETBAIT.DICOMHelper.CommandFieldConst

Public Class N_CREATE
    Inherits Command    

    Dim NumberOfCopiesVal As String
    Dim ImageDisplayFormatVal As String
    Dim FilmSizeIDVal As String
    Dim BorderDensityVal As String
    Dim ConfigInforVal As String

    Public Property NumberOfCopies() As String
        Get
            Return NumberOfCopiesVal
        End Get
        Set(ByVal value As String)
            NumberOfCopiesVal = value
        End Set
    End Property

    Public Property ImageDisplayFormat() As String
        Get
            Return ImageDisplayFormatVal
        End Get
        Set(ByVal value As String)
            ImageDisplayFormatVal = value
        End Set
    End Property

    Public Property FilmSizeID() As String
        Get
            Return FilmSizeIDVal
        End Get
        Set(ByVal value As String)
            FilmSizeIDVal = value
        End Set
    End Property

    Public Property BorderDensity() As String
        Get
            Return BorderDensityVal
        End Get
        Set(ByVal value As String)
            BorderDensityVal = value
        End Set
    End Property

    Public Property ConfigurationInfor() As String
        Get
            Return ConfigInforVal
        End Get
        Set(ByVal value As String)
            ConfigInforVal = value
        End Set
    End Property

    Function CreateNCREATERQCmd(ByVal PreCtxID As Byte) As Byte()
        Dim NCCMDPDU As New PDataTFPDU(&H4)
        'Group Length Dataset
        'CFPDU.PDVContent.AddDataSet(CUInt(&H5A), CUShort(0), CUShort(Command.CommandGroupLengthTag))

        'Affected SOP Class Dataset
        NCCMDPDU.PDVContent.AddDataSet(CStr(MyClass.AffectedSOPClassUID), CUInt(DICOMTag.AffectedSOPClassUIDTag))

        'Command Field Dataset
        NCCMDPDU.PDVContent.AddDataSet(CUShort(N_CREATE_RQ), (DICOMTag.CommandFieldTag))

        'Message ID Dataset
        NCCMDPDU.PDVContent.AddDataSet(CUShort(MyBase.MessageID), (DICOMTag.MessageIDTag))

        'DataSet Type
        NCCMDPDU.PDVContent.AddDataSet(CUShort(MyClass.DataSetType), (DICOMTag.DataSetTypeTag))

        NCCMDPDU.PDVContent.CreateGroupLengthDataset(DICOMTag.CommandGroupLengthTag)

        Return NCCMDPDU.CreateByteBuff(PreCtxID, True, True)

    End Function

    Function NCREATEFilmSessionDataRQ(ByVal PreCtxID As Byte) As Byte()
        Dim NCDATApdu As New PDataTFPDU(&H4)

        'NCpdu.PDVContent.AddDataSet(CUInt(0), CUInt(FilmSessionGroupLengthTag))
        NCDATApdu.PDVContent.AddDataSet(CStr(NumberOfCopies), CUInt(DICOMTag.NumberOfCopiesTag))
        NCDATApdu.PDVContent.CreateGroupLengthDataset(&H2000)
        Return NCDATApdu.CreateByteBuff(PreCtxID, False, True)
    End Function

    Function NCREATEFilmBoxDataRQ(ByVal PreCtxID As Byte) As Byte()
        Dim NCpdu As New PDataTFPDU(&H4)
        Dim PDVTMP As New PDVItem
        'NCpdu.PDVContent.AddDataSet(CUInt(0), CUInt(FilmBoxGroupLengthTag))
        NCpdu.PDVContent.AddDataSet(CStr(ImageDisplayFormat), CUInt(DICOMTag.ImageDisplayFormatTag))
        NCpdu.PDVContent.AddDataSet(CStr(FilmSizeID), CUInt(DICOMTag.FilmSizeIDTag))
        NCpdu.PDVContent.AddDataSet(CStr(BorderDensity), CUInt(DICOMTag.BorderDensityTag))
        NCpdu.PDVContent.AddDataSet(CStr(MyClass.ConfigurationInfor), CUInt(DICOMTag.ConfigurationInforTag))
        NCpdu.PDVContent.AddDelimiterDataSet(&H20100500)
        NCpdu.PDVContent.AddDelimiterDataSet(&HFFFEE000UI)
        PDVTMP.AddDataSet(CStr(BasicFilmSessionSOPClassUID), &H81150)
        PDVTMP.AddDataSet(CStr("1.2.826.0.1.3680043.2.1211.9.1"), &H81155)
        PDVTMP.CreateGroupLengthDataset(&H8)
        NCpdu.PDVContent.DataElements.AddRange(PDVTMP.DataElements)
        NCpdu.PDVContent.AddZeroLenDataSet(&HFFFEE00DUI)
        NCpdu.PDVContent.AddZeroLenDataSet(&HFFFEE0DDUI)
        NCpdu.PDVContent.CreateGroupLengthDataset(&H2010)

        Return NCpdu.CreateByteBuff(PreCtxID, False, True)
    End Function

    'Sub NCREATEFilmBoxDataRSPParse()
End Class