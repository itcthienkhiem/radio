Imports VIETBAIT.DICOMHelper.CommandFieldConst
Imports VIETBAIT.DICOMHelper
Imports VIETBAIT.DICOMPDU

'N-GET ATTRIBUTES
'Attribute Name Tag Usage SCU/SCP
'Printer Configuration Sequence (2000,001E) U/M
'>SOP Classes Supported (0008,115A) -/M
'>Maximum Memory Allocation (2000,0061) -/M
'>Memory Bit Depth (2000,00A0) -/M
'>Printing Bit Depth (2000,00A1) -/M
'>Media Installed Sequence (2000,00A2) -/M
'>>Item Number (0020,0019) -/M
'>>Medium Type (2000,0030) -/M
'>>Film Size ID (2010,0050) -/M
'>>Min Density (2010,0120) -/MC Required if Sequence is Present and Min Density is known
'>>Max Density (2010,0130) -/M
'>Other Media Available Sequence (2000,00A4) -/M
'>>Medium Type (2000,0030) -/M
'>>Film Size ID (2010,0050) -/M
'>>Min Density (2010,0120) -/MC
'Required if Sequence is
'Present and Min Density is
'known
'>>Max Density (2010,0130) -/M
'>Supported Image Display Formats
'Sequence
'(2000,00A8) -/M
'>>Rows (0028,0010) -/MC
'Required if all Image Boxes in
'the Display Format have the
'same number of rows and
'columns
'>>Columns (0028,0011) -/MC
'Required if all Image Boxes in
'the Display Format have the
'same number of rows and
'columns
'>>Image Display Format (2010,0010) -/M
'>>Film Orientation (2010,0040) -/M
'>>Film Size ID (2010,0050) -/M
'>>Printer Resolution ID (2010,0052) -/M
'>>Printer Pixel Spacing (2010,0376) -/M
'>>Requested Image Size Flag (2020,00A0) -/M
'>Default Printer Resolution ID (2010,0054) -/M
'>Default Magnification Type (2010,00A6) -/M
'PS 3.4-2008
'Page 159
'- Standard -
'>Other Magnification Types Available (2010,00A7) -/M
'>Default Smoothing Type (2010,00A8) -/M
'>Other Smoothing Types Available (2010,00A9) -/M
'>Configuration Information Description (2010,0152) -/M
'>Maximum Collated Films (2010,0154) -/M
'>Decimate/Crop Result (2020,00A2) -/M
'>Manufacturer (0008,0070) -/M
'>Manufacturer Model Name (0008,1090) -/M
'>Printer Name (2110,0030) -/M

Public Class N_GET
    Inherits Command
    Public Const PrinterConfigurationSequenceTag As UInt32 = &H2000001E
    Public Const SOPClassSupportedTag As UInt32 = &H8115A
    Public Const MaxMemoryAllocationTag As UInt32 = &H20000061
    Public Const MemoryBitDepthTag As UInt32 = &H200000A0
    Public Const PrintingBitDepthTag As UInt32 = &H200000A1
    Public Const MediaInstallededSequenceTag As UInt32 = &H200000A2


    Function CreateNGETRQCmd(ByVal PreCtxID As Byte) As Byte()
        Dim NGCMDPDU As New PDataTFPDU(&H4)
        'Group Length Dataset
        'NGCMDPDU.PDVContent.AddDataSet(CUInt(&H5A), CUShort(0), CUShort(Command.CommandGroupLengthTag))

        'Requested SOP Class Dataset
        NGCMDPDU.PDVContent.AddDataSet(CStr(MyClass.RequestedSOPClassUID), CUInt(DICOMTag.RequestedSOPClassUIDTag))

        'Command Field Dataset
        NGCMDPDU.PDVContent.AddDataSet(CUShort(N_GET_RQ), CUInt(DICOMTag.CommandFieldTag))

        'Message ID Dataset
        NGCMDPDU.PDVContent.AddDataSet(CUShort(MyBase.MessageID), CUInt(DICOMTag.MessageIDTag))

        'DataSet Type
        NGCMDPDU.PDVContent.AddDataSet(CUShort(MyClass.DataSetType), CUInt(DICOMTag.DataSetTypeTag))

        'Requested SOP Class Instance UID
        NGCMDPDU.PDVContent.AddDataSet(CStr(MyClass.RequestedSOPInstanceUID), CUInt(DICOMTag.RequestedSOPInstanceUIDTag))
        NGCMDPDU.PDVContent.CreateGroupLengthDataset(DICOMTag.CommandGroupLengthTag)
        Return NGCMDPDU.CreateByteBuff(PreCtxID, True, True)
    End Function
End Class