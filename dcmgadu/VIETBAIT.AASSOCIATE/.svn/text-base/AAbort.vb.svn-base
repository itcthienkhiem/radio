Imports VIETBAIT.DICOMPDU


Public Class AAbort
    Inherits PDU

    Sub New()
        MyBase.PDUType = &H7
        MyBase.Length = &H4
    End Sub

    Dim SourceVal As Byte
    Dim ReasonVal As Byte

    Public Property Source() As Byte
        Get
            Return SourceVal
        End Get
        Set(ByVal value As Byte)
            SourceVal = value
        End Set
    End Property

    Public Property Reason() As Byte
        Get
            Return ReasonVal
        End Get
        Set(ByVal value As Byte)
            ReasonVal = value
        End Set
    End Property

    Sub Parse(ByVal u As UnparsePDU)
        MyClass.Source = u.buffer(8)
        MyClass.Reason = u.buffer(9)
    End Sub

    Function CreateByteBuff() As Byte()
        Dim bb(9) As Byte
        bb(0) = CByte(&H7)
        bb(1) = CByte(0)
        bb(2) = CByte(0)
        bb(3) = CByte(0)
        bb(4) = CByte(0)
        bb(5) = CByte(4)
        bb(6) = CByte(0)
        bb(7) = CByte(0)
        bb(8) = SourceVal
        bb(9) = ReasonVal
        Return bb

    End Function
End Class