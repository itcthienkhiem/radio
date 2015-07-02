'7 Registry of DICOM File Meta Elements
'This section specifies the File Meta Elements needed to support the formatting of the File Meta
'Information of the DICOM File Format (See PS 3.10).
'Tag Name VR VM
'(0002,0000) File Meta Information Group Length UL 1
'(0002,0001) File Meta Information Version OB 1
'(0002,0002) Media Storage SOP Class UID UI 1
'(0002,0003) Media Storage SOP Instance UID UI 1
'(0002,0010) Transfer Syntax UID UI 1
'(0002,0012) Implementation Class UID UI 1
'(0002,0013) Implementation Version Name SH 1
'(0002,0016) Source Application Entity Title AE 1
'(0002,0100) Private Information Creator UID UI 1
'(0002,0102) Private Information OB 1
Imports System.IO
Imports System.Text
Imports VIETBAIT.DICOM.BASE
Imports VIETBAIT.WORKLIST.HIS
Imports VIETBAIT.DICOMPDU
Imports VIETBAIT.DICOMHelper.Ultility

Namespace WorkList
    Public Class FileMetaFormat
        Public Const FMIGroupLengthTag As UInt32 = &H20000
        Public Const FMIVersionTag As UInt32 = &H20001
        Public Const FMIMediaStorageSOPClassUIDTag As UInt32 = &H20002
        Public Const FMIMediaStorageSOPInstanceUIDTag As UInt32 = &H20003
        Public Const FMITransferSyntaxTag As UInt32 = &H20010
        Public Const FMIImplementationClassUIDTag As UInt32 = &H20012
        Public Const FMIImplementationVersionNameTag As UInt32 = &H20013
        Public Const FMISourceAETitleTag As UInt32 = &H20016
        Public Const FMIPrivateInformationCreatorUIDTag As UInt32 = &H20100
        Public Const FMIPrivateInformationTag As UInt32 = &H20102
        Public Const DCMPrefix As String = "DICM"

        Dim FilePreamble() As Byte = Array.CreateInstance(GetType(Byte), 128)
        Dim GroupLengthVal As UInt32
        Dim VersionVal As Byte()
        Dim MediaStorageSOPClassUIDVal As String
        Dim MediaStorageSOPInstanceUIDVal As String
        Dim TransferSyntaxVal As String
        Dim ImpClassUIDVal As String
        Dim ImpVersionNameVal As String
        Dim SourceAETVal As String
        Dim PrivateInforCreatorUIDVal As String
        Dim PrivateInforVal As Byte()
        Public DataElements As New ArrayList

        Public Property GroupLength() As UInt32
            Get
                Return GroupLengthVal
            End Get
            Set(ByVal value As UInt32)
                GroupLengthVal = value
            End Set
        End Property

        Public Property Version() As Byte()
            Get
                Return VersionVal
            End Get
            Set(ByVal value As Byte())
                VersionVal = value
            End Set
        End Property

        Public Property MediaStorageSOPInstanceUID() As String
            Get
                Return MyClass.MediaStorageSOPInstanceUIDVal
            End Get
            Set(ByVal value As String)
                MyClass.MediaStorageSOPInstanceUIDVal = value
            End Set
        End Property

        Public Property MediaStorageSOPClassUID() As String
            Get
                Return MyClass.MediaStorageSOPClassUIDVal
            End Get
            Set(ByVal value As String)
                MyClass.MediaStorageSOPClassUIDVal = value
            End Set
        End Property

        Public Property TransferSyntax() As String
            Get
                Return MyClass.TransferSyntaxVal
            End Get
            Set(ByVal value As String)
                MyClass.TransferSyntaxVal = value
            End Set
        End Property

        Public Property ImplementationClassUID() As String
            Get
                Return MyClass.ImpClassUIDVal
            End Get
            Set(ByVal value As String)
                MyClass.ImpClassUIDVal = value
            End Set
        End Property

        Public Property ImplementationVersionName() As String
            Get
                Return MyClass.ImpVersionNameVal
            End Get
            Set(ByVal value As String)
                MyClass.ImpVersionNameVal = value
            End Set
        End Property

        Public Property SourceAETitle() As String
            Get
                Return MyClass.SourceAETVal
            End Get
            Set(ByVal value As String)
                MyClass.SourceAETVal = value
            End Set
        End Property

        Public Property PrivateInformationCreatorUID() As String
            Get
                Return MyClass.PrivateInforCreatorUIDVal
            End Get
            Set(ByVal value As String)
                MyClass.PrivateInforCreatorUIDVal = value
            End Set
        End Property

        Public Property PrivateInformation() As Byte()
            Get
                Return MyClass.PrivateInforVal
            End Get
            Set(ByVal value As Byte())
                MyClass.PrivateInforVal = value
            End Set
        End Property

        Function CreateFile(ByVal ImageDataElements As List(Of DataElement), ByVal path As String) As ArrayList
            Dim Cols As UShort
            Dim Rows As UShort
            Dim PIXELMatrix(,) As UInt16
            Dim ReturnList As New ArrayList
            Dim f As FileStream = File.Create(path)
            Dim i As Byte
            Dim pos As UInt32
            For i = 0 To 127
                FilePreamble(i) = 0
            Next
            pos = 128
            f.Write(FilePreamble, 0, 128)
            Dim tmp() As Byte = Encoding.ASCII.GetBytes(DCMPrefix)

            f.Write(tmp, 0, tmp.Length)
            pos += tmp.Length

            Dim de As New DataElement
            AddDataSet(CUInt(0), FMIGroupLengthTag)
            AddDataSet(CUShort(&H100), CUInt(FMIVersionTag))
            AddDataSet(CStr(ImplicitLittleEndian), CUInt(FMITransferSyntaxTag))
            Dim len As UInt32 = 0
            For Each de In DataElements
                len += de.Length + 8
            Next
            len -= 12
            de = New DataElement
            de.DataValue = BitConverter.GetBytes(len)
            de.DataTag = FMIGroupLengthTag
            de.Length = 4
            DataElements.RemoveAt(0)
            DataElements.Insert(0, de)
            For i = 0 To ImageDataElements.Count - 1
                DataElements.Add(ImageDataElements(i))
            Next
            For Each de In DataElements
                Select Case de.DataTag
                    Case &H280010
                        Dim rowtmp() As Byte = de.DataValue
                        'Array.Reverse(rowtmp)
                        Rows = BitConverter.ToUInt16(rowtmp, 0)
                        ReturnList.Add(Rows)
                    Case &H280011
                        Dim coltmp() As Byte = de.DataValue
                        'Array.Reverse(coltmp)
                        Cols = BitConverter.ToUInt16(coltmp, 0)
                        ReturnList.Add(Cols)
                    Case &H7FE00010
                        Dim ii, jj As UInt16
                        Dim k As UInt32 = 0
                        ReDim PIXELMatrix(Rows - 1, Cols - 1)
                        Dim datatmp() As Byte = de.DataValue.Clone
                        For count As UInt32 = 0 To (de.Length / 2) - 1
                            Array.Reverse(datatmp, count * 2, 2)
                        Next
                        For jj = 0 To Cols - 1
                            For ii = 0 To Rows - 1
                                PIXELMatrix(ii, jj) = BitConverter.ToUInt16(datatmp, k)
                                k += 2
                            Next ii
                            'ReturnList.Add(PIXELMatrix)
                        Next jj
                        ReturnList.Add(PIXELMatrix)
                End Select
                f.Write(de.CreateByteBuff(), 0, de.Length + 8)
                pos += de.Length + 8
            Next
            f.Close()
            Return ReturnList
        End Function

        Function ReadDCMFile(ByVal path As String)
            Dim f As FileStream = File.OpenRead(path)
            Dim pos As UInt32 = 128 + DCMPrefix.Length - 1
            Dim b(127) As Byte
            Dim fbin As FileStream = File.Create("C:\B.BIN")

            f.Read(b, 0, 128)
            f.Read(b, 0, DCMPrefix.Length)

            Dim DataElements As New ArrayList
            While Not (f.Position = f.Length)
                Dim tmp(1) As Byte
                Dim de As New DataElement
                f.Read(tmp, 0, 2)
                'Array.Reverse(tmp)
                de.DataTag = BitConverter.ToUInt16(tmp, 0) << 16
                f.Read(tmp, 0, 2)
                'Array.Reverse(tmp)
                de.DataTag += BitConverter.ToUInt16(tmp, 0)
                'pos += 4
                'f.Position = pos
                ReDim tmp(3)
                f.Read(tmp, 0, 4)
                'Array.Reverse(tmp)
                de.Length = BitConverter.ToUInt32(tmp, 0)
                'pos += 4
                'f.Position = pos
                ReDim de.DataValue(de.Length - 1)
                f.Read(de.DataValue, 0, de.Length)
                If de.DataTag = &H280010 Or de.DataTag = &H280011 Then
                    Dim valuetmp() As Byte = de.DataValue.Clone
                    Array.Reverse(valuetmp)
                    fbin.Write(valuetmp, 0, de.Length)
                    Debug.Print(Format(BitConverter.ToUInt16(de.DataValue, 0), "D"))
                End If
                If de.DataTag = &H7FE00010 Then
                    Dim datatmp() As Byte = de.DataValue.Clone
                    For count As UInt32 = 0 To (de.Length / 2) - 1
                        Array.Reverse(datatmp, count * 2, 2)
                    Next

                    fbin.Write(datatmp, 0, datatmp.Length)

                End If
                DataElements.Add(de)
            End While
            fbin.Close()
            f.Close()
            Return DataElements
        End Function


        Public Overloads Sub AddDataSet(ByVal sData As String, ByVal DataTag As UInt32)
            Dim TagTmp As New DataElement
            TagTmp.DataTag = DataTag
            If Not String.IsNullOrEmpty(sData) Then

                If sData.Length Mod 2 = 1 Then
                    sData = sData & " "
                End If
                TagTmp.DataValue = Encoding.ASCII.GetBytes(sData)
                TagTmp.Length = sData.Length
            Else
                TagTmp.Length = 0
            End If
            DataElements.Add(TagTmp)
        End Sub

        Overloads Sub AddDataSet(ByVal iData As UInt16, ByVal DataTag As UInt32)
            Dim TagTmp As New DataElement
            TagTmp.DataTag = DataTag
            TagTmp.DataValue = BitConverter.GetBytes(CUShort(iData))
            TagTmp.Length = 2
            DataElements.Add(TagTmp)
        End Sub

        Overloads Sub AddDataSet(ByVal iData As UInt32, ByVal DataTag As UInt32)
            Dim TagTmp As New DataElement
            TagTmp.DataTag = DataTag
            TagTmp.DataValue = BitConverter.GetBytes(CUInt(iData))
            TagTmp.Length = 4
            DataElements.Add(TagTmp)
        End Sub
    End Class
End Namespace