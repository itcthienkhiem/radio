Imports System.Text
Imports VIETBAIT.DICOM.BASE


Public Class PDVItem
    Dim ItemLen As UInt32
    Dim PreCtxID As Byte
    Dim MessageCtlHeader As Byte
    Public DataElements As New ArrayList()
    'Public DataElementHashTable As Hashtable

    Public Property Length() As UInt32
        Get
            Return ItemLen
        End Get
        Set(ByVal value As UInt32)
            ItemLen = value
        End Set
    End Property

    Public Property PresentationContextID() As Byte
        Get
            Return PreCtxID
        End Get
        Set(ByVal value As Byte)
            PreCtxID = value
        End Set
    End Property

    Property MessageControlHeader() As Byte
        Get
            Return MessageCtlHeader
        End Get
        Set(ByVal value As Byte)
            MessageCtlHeader = value
        End Set
    End Property

    Function IsCommand() As Boolean

        If (MessageControlHeader And &H1) Then
            Return True
        Else
            Return False
        End If
    End Function

    Function IsLast() As Boolean

        If (MessageControlHeader And &H2) Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub Command(ByVal IsCom As Boolean)
        If IsCom Then
            MessageCtlHeader = MessageCtlHeader Or &H1
        Else
            MessageCtlHeader = MessageCtlHeader And &HFE
        End If
    End Sub

    Sub Last(ByVal IsLast As Boolean)
        If IsLast Then
            MessageCtlHeader = MessageCtlHeader Or &H2
        Else
            MessageCtlHeader = MessageCtlHeader And &HFD
        End If
    End Sub

    Function GetLength(ByVal b As Byte(), ByVal off As UInt32) As UInt32
        Dim tmp(3) As Byte
        Array.Copy(b, off, tmp, 0, 4)
        Array.Reverse(tmp)
        Return (BitConverter.ToUInt32(tmp, 0))
    End Function

    'Public Overloads Sub AddDataSet(ByVal sData As String, ByVal TagGroup As UInt16, ByVal TagElement As UInt16)
    '    Dim TagTmp As New DataElement
    '    TagTmp.DataTag = (TagGroup << 16) + TagElement

    '    If Not String.IsNullOrEmpty(sData) Then
    '        If sData.Length Mod 2 = 1 Then
    '            sData = sData & " "
    '        End If
    '        TagTmp.DataValue = Encoding.ASCII.GetBytes(sData)
    '        TagTmp.Length = sData.Length
    '    Else
    '        TagTmp.Length = 0
    '    End If
    '    DataElements.Add(TagTmp)
    'End Sub

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

    'Overloads Sub AddDataSet(ByVal iData As UInt16, ByVal TagGroup As UInt16, ByVal TagElement As UInt16)
    '    Dim TagTmp As New DataElement
    '    TagTmp.TagGroup = TagGroup
    '    TagTmp.TagElement = TagElement
    '    TagTmp.DataValue = BitConverter.GetBytes(CUShort(iData))
    '    TagTmp.Length = 2
    '    DataElements.Add(TagTmp)
    'End Sub

    'Overloads Sub AddDataSet(ByVal iData As UInt32, ByVal TagGroup As UInt16, ByVal TagElement As UInt16)
    '    Dim TagTmp As New DataElement
    '    TagTmp.TagGroup = TagGroup
    '    TagTmp.TagElement = TagElement
    '    TagTmp.DataValue = BitConverter.GetBytes(CUInt(iData))
    '    TagTmp.Length = 4
    '    DataElements.Add(TagTmp)
    'End Sub

    Overloads Sub AddDataSet(ByVal iData As UInt32, ByVal DataTag As UInt32)
        Dim TagTmp As New DataElement
        TagTmp.DataTag = DataTag
        TagTmp.DataValue = BitConverter.GetBytes(CUInt(iData))
        TagTmp.Length = 4
        DataElements.Add(TagTmp)
    End Sub

    Overloads Sub AddDataSet(ByVal iData As UInt16, ByVal DataTag As UInt32)
        Dim TagTmp As New DataElement
        TagTmp.DataTag = DataTag
        TagTmp.DataValue = BitConverter.GetBytes(CUShort(iData))
        TagTmp.Length = 2
        DataElements.Add(TagTmp)
    End Sub

    Sub AddDelimiterDataSet(ByVal _Tag As UInt32)

        Dim TagTmp As New DataElement

        TagTmp.DataTag = _Tag        
        TagTmp.Length = &HFFFFFFFFUI
        DataElements.Add(TagTmp)
    End Sub

    Sub AddZeroLenDataSet(ByVal _Tag As UInt32)
        Dim TagTmp As New DataElement
        TagTmp.DataTag = _Tag
        TagTmp.Length = &H0UI
        DataElements.Add(TagTmp)
    End Sub

    Sub CreateGroupLengthDataset(ByVal GroupLengthTag As UInt32)
        Dim de As New DataElement
        Dim GrLen As UInt32 = 0
        For Each de In DataElements
            If de.Length <> &HFFFFFFFFUI Then
                GrLen += de.Length + 8
            Else
                GrLen += 12
            End If
        Next
        de = New DataElement
        de.DataTag = GroupLengthTag        
        de.Length = 4
        de.DataValue = BitConverter.GetBytes(GrLen)
        DataElements.Insert(0, de)
    End Sub


    Function Parse(ByVal ByteBuff As Byte(), ByVal off As UInt16) As Byte()
        Dim pos As UInt32
        PreCtxID = ByteBuff(off)
        MessageCtlHeader = ByteBuff(off + 1)

        pos = off + 2
        While (pos < Length + off)
            If pos > Length + off - 8 Then
                Exit While
            End If
            Dim de As New DataElement

            de.DataTag = (BitConverter.ToUInt16(ByteBuff, pos) << 16) + BitConverter.ToUInt16(ByteBuff, pos + 2)            
            de.Length = BitConverter.ToUInt32(ByteBuff, pos + 4)
            Debug.WriteLine(Format(de.DataTag, "X8"))

            If (de.Length > 0) And (de.Length <> &HFFFFFFFFUI) Then
                If pos + de.Length + 8 > Length + off Then
                    Exit While
                End If
                ReDim de.DataValue(de.Length - 1)
                Array.Copy(ByteBuff, pos + 8, de.DataValue, 0, de.Length)
            Else
                de.Length = 0
            End If
            pos += de.Length + 8
            If (de.Length < 4) Then
                DataElements.Add(de)
            ElseIf (BitConverter.ToUInt32(de.DataValue, 0) <> &HE000FFFEUL) Then
                DataElements.Add(de)
            Else
                Dim pos1 As UInt32 = 8
                Dim len1 As UInt32 = BitConverter.ToUInt32(de.DataValue, 4)
                While (pos1 < len1 + 8)
                    Dim de1 As New DataElement
                    If BitConverter.ToUInt32(de.DataValue, pos1) = &HE00DFFFEUL Then
                        Exit While
                    End If
                    de1.DataTag = (BitConverter.ToUInt16(ByteBuff, pos) << 16) + BitConverter.ToUInt16(ByteBuff, pos + 2)                    
                    de1.Length = BitConverter.ToUInt32(de.DataValue, pos1 + 4)
                    Debug.WriteLine(Format(de1.DataTag, "X8"))
                    If de1.Length > 0 Then
                        ReDim de1.DataValue(de1.Length - 1)
                        Array.Copy(de.DataValue, pos1 + 8, de1.DataValue, 0, de1.Length)
                    End If
                    DataElements.Add(de1)
                    pos1 += de1.Length + 8
                End While
            End If

        End While
        If pos = Length + off Then
            Return Nothing
        Else
            Dim tmp(Length + off - pos - 1) As Byte
            Array.Copy(ByteBuff, pos, tmp, 0, Length + off - pos)
            Return tmp
        End If
    End Function

    
    Function CreateDataElementsBuffer() As Byte()
        Dim BB(4) As Byte
        Dim pos As UInt32 = 0
        Dim len As UInt32 = 0
        For Each de As DataElement In DataElements
            Debug.WriteLine(Format(de.DataTag, "X8"))
            Dim tmp1() As Byte = de.CreateByteBuff()
            len += de.Length + 8
            Array.Resize(BB, len)
            Array.Copy(tmp1, 0, BB, pos, tmp1.Length)
            pos += tmp1.Length
        Next
        Return BB
    End Function
    'Function CreateHashTableBuffer() As Byte()
    '    Dim BB(4) As Byte
    '    Dim pos As UInt32 = 0
    '    Dim len As UInt32 = 0
    '    Dim tagList As New ArrayList(hashDataElement.Keys)

    '    For Each _tag As UInt32 In tagList
    '        Debug.WriteLine(Format(_tag, "X8"))
    '        Dim de As New DataElement
    '        de.DataTag = _tag
    '        de.DataValue = hashDataElement(_tag)
    '        de.Length = de.DataValue.Length
    '        Dim tmp1() As Byte = de.CreateByteBuff()
    '        len += de.Length + 8
    '        Array.Resize(BB, len)
    '        Array.Copy(tmp1, 0, BB, pos, tmp1.Length)
    '        pos += tmp1.Length
    '    Next
    '    Return BB
    'End Function
    'Function CreateBufferFromHashtable() As Byte()
    '    Return CreateByteBuffer(CreateHashTableBuffer, 4)
    'End Function

    Overloads Function CreateByteBuffer() As Byte()
        Return CreateByteBuffer(CreateDataElementsBuffer, 4)
    End Function

    Overloads Function CreateByteBuffer(ByVal buffer As Byte(), ByVal off As UInt32) As Byte()
        Dim BB(off + 2 + buffer.Length - 1) As Byte

        BB(off) = PreCtxID
        BB(off + 1) = MessageCtlHeader
        Array.Copy(buffer, 0, BB, off + 2, buffer.Length)
        Length = buffer.Length + 2
        Dim tmp() As Byte = BitConverter.GetBytes(MyClass.Length)
        Array.Reverse(tmp)
        Array.Copy(tmp, BB, 4)
        Return BB
    End Function

    'Overloads Function DataTagSearch(ByVal DataTagKey As UInt16) As Int32
    '    Dim Index As UInt32
    '    Dim de As DataElement
    '    For Index = 0 To DataElements.Count - 1
    '        de = DataElements(Index)
    '        If de.TagElement = DataTagKey Then
    '            Return Index
    '        End If
    '    Next
    '    Return -1
    'End Function

    Overloads Function DataTagSearch(ByVal DataTagKey As UInt32) As Int32
        Dim Index As UInt32
        Dim de As DataElement
        For Index = 0 To DataElements.Count - 1
            de = DataElements(Index)
            If de.DataTag = DataTagKey Then
                Return Index
            End If
        Next
        Return -1
    End Function
End Class