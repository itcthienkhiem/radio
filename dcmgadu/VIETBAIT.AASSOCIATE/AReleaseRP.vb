Imports VIETBAIT.DICOMPDU


Public Class AReleaseRP
    Inherits PDU

    Sub New()
        MyBase.PDUType = &H6
        MyBase.Length = &H4
    End Sub


    Function CreateByteBuff() As Byte()
        Dim bb(9) As Byte
        bb(0) = CByte(&H6)
        bb(1) = CByte(0)
        bb(2) = CByte(0)
        bb(3) = CByte(0)
        bb(4) = CByte(0)
        bb(5) = CByte(4)
        bb(6) = CByte(0)
        bb(7) = CByte(0)
        bb(8) = CByte(0)
        bb(9) = CByte(0)
        Return bb

    End Function
End Class