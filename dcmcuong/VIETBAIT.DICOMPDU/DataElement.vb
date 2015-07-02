
Public Class DataElement
    Dim DataTagValue As UInt32
    Dim TagGroupValue As UInt16
    Dim TagElementValue As UInt16
    Dim ValueLength As UInt32
    Public DataValue() As Byte


    Public Property DataTag() As UInt32
        Get
            Return DataTagValue
        End Get
        Set(ByVal value As UInt32)
            DataTagValue = value
            TagGroupValue = CUShort(value >> 16)
            TagElementValue = CUShort(value And &HFFFFUI)
        End Set
    End Property

    Public Property TagGroup() As UInt16
        Get
            Return TagGroupValue
        End Get
        Set(ByVal value As UInt16)
            TagGroupValue = value
            DataTagValue = (DataTagValue And &HFFFFUI) + (CUInt(value) << 16)
        End Set
    End Property

    Public Property TagElement() As UInt16
        Get
            Return TagElementValue
        End Get
        Set(ByVal value As UInt16)
            TagElementValue = value
            DataTagValue = (DataTagValue And &HFFFF0000UI) + value
        End Set
    End Property

    Public Property Length() As UInt32
        Get
            Return ValueLength
        End Get
        Set(ByVal value As UInt32)
            ValueLength = value
        End Set
    End Property

    Sub SetLength()
        If DataValue Is Nothing Then
            ValueLength = 0
        Else
            ValueLength = DataValue.Length
        End If

    End Sub

    Function CreateByteBuff() As Byte()
        Dim ByteBuff(7) As Byte
        Dim tmp() As Byte
        tmp = BitConverter.GetBytes(TagGroup)
        [Array].Copy(tmp, 0, ByteBuff, 0, 2)
        tmp = BitConverter.GetBytes(TagElement)
        [Array].Copy(tmp, 0, ByteBuff, 2, 2)
        tmp = BitConverter.GetBytes(ValueLength)
        [Array].Copy(tmp, 0, ByteBuff, 4, tmp.Length)
        If (ValueLength = &HFFFFFFFFUL) Then
            ValueLength = 0
        End If
        If (ValueLength > 0) Then
            Array.Resize(ByteBuff, MyClass.Length + 8)
            [Array].Copy(DataValue, 0, ByteBuff, 8, ValueLength)
        End If
        Return ByteBuff
    End Function
End Class