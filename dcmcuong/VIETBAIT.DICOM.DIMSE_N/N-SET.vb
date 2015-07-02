'N-SET ATTRIBUTES
'Attribute Name Tag Usage SCU/SCP
'Image Box Position (2020,0010) M/M
'Basic Grayscale Image Sequence (2020,0110) M/M
'>Samples Per Pixel (0028,0002) M/M
'>Photometric Interpretation (0028,0004) M/M
'>Rows (0028,0010) M/M
'>Columns (0028,0011) M/M
'>Pixel Aspect Ratio (0028,0034) MC/M
'(Required if the aspect ration is
'not 1\1))
'>Bits Allocated (0028,0100) M/M
'>Bits Stored (0028,0101) M/M
'>High Bit (0028,0102) M/M
'>Pixel Representation (0028,0103) M/M
'>Pixel Data (7FE0,0010) M/M
'Polarity (2020,0020) U/M
'Magnification Type (2010,0060) U/U
'Smoothing Type (2010,0080) U/U
'Min Density (2010,0120) U/U
'Max Density (2010,0130) U/U
'Configuration Information (2010,0150) U/U
'Requested Image Size (2020,0030) U/U
'Requested Decimate/Crop Behavior (2020,0040) U/U
Imports System.IO
Imports VIETBAIT.DICOM.BASE
Imports VIETBAIT.DICOMHelper
Imports VIETBAIT.DICOMPDU
Imports VIETBAIT.DICOMHelper.CommandFieldConst

Public Class N_SET
    Inherits Command
    Public Const ImageBoxPositionTag As UInt32 = &H20200010
    Public Const BasicGrayscaleImageSequenceTag As UInt32 = &H20200110
    Public Const SamplesPerPixelTag As UInt32 = &H280002
    Public Const PhotometricInterpretationTag As UInt32 = &H280004


    Dim ImagePosVal As UInt16
    Dim SamplesPerPixelVal As UInt16
    Dim PhotometricInterpretationVal As String
    Dim RowsVal, ColVal, BitAllocatedVal, BitStoredVal, HighBitVal, PixelRepVal As UInt16
    Public PixelData() As Byte

    Public Property ImagePosition() As UInt16
        Get
            Return ImagePosVal
        End Get
        Set(ByVal value As UInt16)
            ImagePosVal = value
        End Set
    End Property

    Public Property SamplesPerPixel() As UInt16
        Get
            Return SamplesPerPixelVal
        End Get
        Set(ByVal value As UInt16)
            SamplesPerPixelVal = value
        End Set
    End Property

    Public Property PhotometricInterpretation() As String
        Get
            Return PhotometricInterpretationVal
        End Get
        Set(ByVal value As String)
            PhotometricInterpretationVal = value
        End Set
    End Property

    Public Property Rows() As UInt16
        Get
            Return RowsVal
        End Get
        Set(ByVal value As UInt16)
            RowsVal = value
        End Set
    End Property

    Public Property Columns() As UInt16
        Get
            Return ColVal
        End Get
        Set(ByVal value As UInt16)
            ColVal = value
        End Set
    End Property

    Public Property BitAllocated() As UInt16
        Get
            Return BitAllocatedVal
        End Get
        Set(ByVal value As UInt16)
            BitAllocatedVal = value
        End Set
    End Property

    Public Property BitStored() As UInt16
        Get
            Return BitStoredVal
        End Get
        Set(ByVal value As UInt16)
            BitStoredVal = value
        End Set
    End Property

    Public Property HighBit() As UInt16
        Get
            Return HighBitVal
        End Get
        Set(ByVal value As UInt16)
            HighBitVal = value
        End Set
    End Property

    Public Property PixelRepresentation() As UInt16
        Get
            Return PixelRepVal
        End Get
        Set(ByVal value As UInt16)
            PixelRepVal = value
        End Set
    End Property

    Function CreateNSETRQCmd(ByVal PreCtxID As Byte) As Byte()
        Dim NSCMDPDU As New PDataTFPDU(&H4)
        'Group Length Dataset
        'CFPDU.PDVContent.AddDataSet(CUInt(&H5A), CUShort(0), CUShort(Command.CommandGroupLengthTag))

        'Requested SOP Class Dataset
        NSCMDPDU.PDVContent.AddDataSet(CStr(RequestedSOPClassUID), (DICOMTag.RequestedSOPClassUIDTag))

        'Command Field Dataset
        NSCMDPDU.PDVContent.AddDataSet(CUShort(N_SET_RQ), (DICOMTag.CommandFieldTag))

        'Message ID Dataset
        NSCMDPDU.PDVContent.AddDataSet(CUShort(MyBase.MessageID), (DICOMTag.MessageIDTag))

        'DataSet Type
        NSCMDPDU.PDVContent.AddDataSet(CUShort(MyClass.DataSetType), (DICOMTag.DataSetTypeTag))

        'Requested SOP Class Instance UID
        NSCMDPDU.PDVContent.AddDataSet(CStr(MyClass.RequestedSOPInstanceUID), CUInt(DICOMTag.RequestedSOPInstanceUIDTag))


        NSCMDPDU.PDVContent.CreateGroupLengthDataset(DICOMTag.CommandGroupLengthTag)

        Return NSCMDPDU.CreateByteBuff(PreCtxID, True, True)

    End Function

    Function CreateNSETDatasetBuffer(ByVal FileDataElements As ArrayList) As Byte()
        'Dim tmppdv As New PDVItem
        'tmppdv.AddDataSet(CUShort(ImagePosition), CUInt(ImageBoxPositionTag))
        'tmppdv.AddDelimiterDataSet(&H20200110)
        'tmppdv.AddDelimiterDataSet(&HFFFEE000UI)
        'Dim de As New DataElement
        'Dim BasicGrayDatasets As New ArrayList
        'For Each de In FileDataElements
        '    If (de.TagGroup = &H28 And de.TagElement <= &H103) Or de.TagGroup = &H7FE0 Then
        '        tmppdv.DataElements.Add(de)
        '    End If
        'Next
        'tmppdv.AddZeroLenDataSet(&HFFFEE00DUI)
        'tmppdv.AddZeroLenDataSet(&HFFFEE0DDUI)
        'Return tmppdv.CreateDataElementsBuffer()

        Return Nothing
    End Function

    Function CreateNSETBuffer(ByVal FileDataElements As ArrayList) As ArrayList
        Dim tmp() As Byte = CreateNSETDatasetBuffer(FileDataElements)
        Dim tmp2 As New ArrayList
        Dim pos As UInt32 = 0
        If tmp.Length > CurMaxLength - 12 Then
            Do
                If pos < tmp.Length Then
                    Dim tmp1() As Byte
                    If tmp.Length - pos > CurMaxLength - 12 Then
                        ReDim tmp1(CurMaxLength - 12 - 1)
                        Array.Copy(tmp, pos, tmp1, 0, CurMaxLength - 12)
                        pos += CurMaxLength - 12

                    Else
                        ReDim tmp1(tmp.Length - pos - 1)
                        Array.Copy(tmp, pos, tmp1, 0, tmp.Length - pos)
                        pos = tmp.Length - 1
                    End If
                    tmp2.Add(tmp1)
                End If

            Loop While (pos < tmp.Length - 1)
        Else
            tmp2.Add(tmp)
        End If
        Return tmp2
    End Function

    Protected Property CurMaxLength() As Integer
        Get
            Throw New NotImplementedException()
        End Get
        Set(ByVal value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Sub WriteTo(ByVal os As Stream, ByVal arrayofbytes As ArrayList, ByVal PreCtxID As Byte)
        Dim tmp(3) As Byte
        Dim tmp1() As Byte
        Dim NS As New PDataTFPDU(&H4)
        If arrayofbytes.Count > 1 Then
            For i As UInt16 = 0 To arrayofbytes.Count - 2
                NS.PDVContent.Command(False)
                NS.PDVContent.Last(False)
                NS.PDVContent.PresentationContextID = PreCtxID
                Dim buff() As Byte = NS.PDVContent.CreateByteBuffer(arrayofbytes(i), 4)
                tmp = BitConverter.GetBytes(buff.Length)
                Array.Reverse(tmp)
                ReDim tmp1(buff.Length + 6 - 1)
                tmp1(0) = NS.PDUType
                tmp1(1) = CByte(0)
                Array.Copy(tmp, 0, tmp1, 2, 4)
                Array.Copy(buff, 0, tmp1, 6, buff.Length)
                os.Write(tmp1, 0, tmp1.Length)
            Next i
        End If
        NS.PDVContent.Command(False)
        NS.PDVContent.Last(True)
        NS.PDVContent.PresentationContextID = PreCtxID
        Dim buff0() As Byte = NS.PDVContent.CreateByteBuffer(arrayofbytes(arrayofbytes.Count - 1), 4)
        tmp = BitConverter.GetBytes(buff0.Length)
        Array.Reverse(tmp)
        ReDim tmp1(buff0.Length + 6 - 1)
        tmp1(0) = NS.PDUType
        tmp1(1) = CByte(0)
        Array.Copy(tmp, 0, tmp1, 2, 4)
        Array.Copy(buff0, 0, tmp1, 6, buff0.Length)
        os.Write(tmp1, 0, tmp1.Length)
    End Sub
End Class