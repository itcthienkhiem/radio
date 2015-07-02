Imports System.Text
Imports VIETBAIT.DICOMPDU


Public Class AppContextItem
    Inherits ItemClass

    Sub New()
        MyBase.New(&H10)
    End Sub

    Dim AppContextNameString As String

    Public Property AppContextName() As String
        Get
            Return AppContextNameString
        End Get
        Set(ByVal value As String)
            AppContextNameString = value
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
    Public Function CreateAppCtxItem() As Byte()
        Dim BB(3) As Byte
        Dim tmp() As Byte = {}
        'Dim c() As Char
        MyBase.ItemLength = AppContextName.Length
        tmp = BitConverter.GetBytes(MyBase.ItemLength)
        Array.Reverse(tmp)
        Array.Resize(BB, MyBase.ItemLength + 4)
        BB(0) = ItemType
        BB(1) = 0
        Array.Copy(tmp, 0, BB, 2, 2)
        'ReDim tmp(c.Length - 1)
        tmp = Encoding.ASCII.GetBytes(AppContextNameString)
        Array.Copy(tmp, 0, BB, 4, MyBase.ItemLength)
        Return BB
    End Function
End Class