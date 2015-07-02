
Public Class PDU
    Dim PDUTypeValue As Byte
    Dim PDULenValue As UInt32

    Public Property PDUType() As Byte
        Get
            Return PDUTypeValue
        End Get
        Set(ByVal value As Byte)
            PDUTypeValue = value
        End Set
    End Property

    Public Property Length() As UInt32
        Get
            Return PDULenValue
        End Get
        Set(ByVal value As UInt32)
            PDULenValue = value
        End Set
    End Property
End Class