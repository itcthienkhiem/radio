Imports System.Text
Imports VIETBAIT.DICOMPDU


Public Class TransferSyntaxSubItem
    Inherits ItemClass

    Sub New()
        MyBase.New(&H40)
    End Sub

    Private TransferSyntaxNameString As String

    Public Property TransferSyntaxName() As String
        Get
            Return TransferSyntaxNameString
        End Get
        Set(ByVal value As String)
            TransferSyntaxNameString = value
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
    Public Function CreateTransferSyntaxSubItem() As Byte()
        Dim BB(3) As Byte
        Dim tmp() As Byte = {}
        MyBase.ItemLength = TransferSyntaxName.Length
        tmp = BitConverter.GetBytes(MyBase.ItemLength)
        Array.Reverse(tmp)
        Array.Resize(BB, MyBase.ItemLength + 4)
        BB(0) = ItemType
        BB(1) = 0
        Array.Copy(tmp, 0, BB, 2, 2)
        Array.Resize(tmp, MyBase.ItemLength)
        tmp = Encoding.ASCII.GetBytes(TransferSyntaxName)
        Array.Copy(tmp, 0, BB, 4, MyBase.ItemLength)
        Return BB
    End Function
End Class