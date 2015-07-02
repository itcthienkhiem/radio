Imports System.Text
Imports VIETBAIT.DICOMPDU


Public Class AbstractSyntaxSubItem
    Inherits ItemClass

    Sub New()
        MyBase.New(&H30)
    End Sub

    Dim AbstractSyntaxNameString As String

    Public Property AbstractSyntaxName() As String
        Get
            Return AbstractSyntaxNameString
        End Get
        Set(ByVal value As String)
            AbstractSyntaxNameString = value
        End Set
    End Property

    'Sub AppCtxItemParse(ByVal ByteBuff As Byte())
    '    Dim tmp(2) As Byte
    '    Array.Copy(ByteBuff, tmp, 4)
    '    Array.Reverse(tmp)
    '    ItemLen = BitConverter.ToUInt32(tmp, 0)
    '    PreCtxID = ByteBuff(4)
    '    ReDim PDV(ItemLen - 2)
    '    Array.Copy(ByteBuff, 5, PDV, 0, ItemLen - 1)

    'End Sub
    Public Function CreateAbstractSyntaxSubItem() As Byte()
        Dim tmp() As Byte = {}

        MyBase.ItemLength = AbstractSyntaxName.Length
        tmp = BitConverter.GetBytes(MyBase.ItemLength)
        Array.Reverse(tmp)
        Dim BB(MyBase.ItemLength + 4 - 1) As Byte
        BB(0) = ItemType
        BB(1) = 0
        Array.Copy(tmp, 0, BB, 2, 2)
        tmp = Encoding.ASCII.GetBytes(AbstractSyntaxName)
        Array.Copy(tmp, 0, BB, 4, MyBase.ItemLength)
        Return BB
    End Function
End Class