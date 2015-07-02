
Public Class ItemClass
    Dim ItemTypeVal As Byte
    Dim ItemLengthVal As UInt16

    Sub New(ByVal value As Byte)
        ItemTypeVal = value
    End Sub

    Public ReadOnly Property ItemType() As Byte
        Get
            Return ItemTypeVal
        End Get
    End Property

    Public Property ItemLength() As UInt16
        Get
            Return ItemLengthVal
        End Get
        Set(ByVal value As UInt16)
            ItemLengthVal = value
        End Set
    End Property
End Class