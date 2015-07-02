Imports System.Text
Imports VIETBAIT.DICOMPDU


Public Class PreContextItem
    Inherits ItemClass

    Sub New()
        MyBase.New(&H20)
    End Sub

    Dim PreCtxIDValue As Byte
    Dim ResultValue As Byte
    Public AbSyntax As New AbstractSyntaxSubItem
    Public TransSyntax As New ArrayList

    Public Property PreCtxID() As Byte
        Get
            Return PreCtxIDValue
        End Get
        Set(ByVal value As Byte)
            PreCtxIDValue = value
        End Set
    End Property

    Public Property Result() As Byte
        Get
            Return ResultValue
        End Get
        Set(ByVal value As Byte)
            ResultValue = value
        End Set
    End Property

    Function CreatePreCtxItemAC() As Byte()
        Dim bb(7) As Byte

        bb(0) = &H21
        bb(1) = 0
        bb(4) = PreCtxID
        bb(5) = 0
        bb(6) = Result
        bb(7) = 0
        Dim tmp As Byte() = TransSyntax(0).CreateTransferSyntaxSubItem()
        MyBase.ItemLength = TransSyntax(0).ItemLength + 4 + 4
        bb(3) = MyBase.ItemLength And &HFF
        bb(2) = (MyBase.ItemLength >> 8) And &HFF
        Array.Resize(bb, MyBase.ItemLength + 4)
        Array.Copy(tmp, 0, bb, 8, tmp.Length)
        Return bb
    End Function

    Function CreatePreCtxItemRQ() As Byte()
        Dim bb(7) As Byte

        bb(0) = &H20
        bb(1) = 0
        bb(4) = PreCtxID
        bb(5) = 0
        bb(6) = 0
        bb(7) = 0
        ItemLength = 4
        Dim tmp As Byte() = AbSyntax.CreateAbstractSyntaxSubItem()
        Array.Resize(bb, ItemLength + 4 + tmp.Length)
        Array.Copy(tmp, 0, bb, ItemLength + 4, tmp.Length)
        ItemLength += tmp.Length
        tmp = TransSyntax(0).CreateTransferSyntaxSubItem()
        Array.Resize(bb, ItemLength + 4 + tmp.Length)
        Array.Copy(tmp, 0, bb, ItemLength + 4, tmp.Length)
        ItemLength += tmp.Length

        bb(3) = MyBase.ItemLength And &HFF
        bb(2) = (MyBase.ItemLength >> 8) And &HFF

        Return bb
    End Function

    Public Sub PreCtxRQParse(ByVal bb() As Byte)
        Dim tmp() As Byte

        PreCtxID = bb(0)
        Dim len As Byte = MyBase.ItemLength - 4
        ReDim tmp(len - 1)
        Dim sublen As UInt16 = 0
        Dim sublenarray(1) As Byte
        Array.Copy(bb, 4, tmp, 0, len)
        Do
            Array.Copy(tmp, sublen, tmp, 0, tmp.Length - sublen)
            Array.Resize(tmp, tmp.Length - sublen)
            Array.Copy(tmp, 2, sublenarray, 0, 2)
            Array.Reverse(sublenarray)
            sublen = BitConverter.ToUInt16(sublenarray, 0)
            Dim tmp1(sublen - 1) As Byte
            Array.Copy(tmp, 4, tmp1, 0, sublen)

            Dim SubItemType As Byte = tmp(0)
            Select Case SubItemType
                Case &H30
                    AbSyntax = New AbstractSyntaxSubItem
                    AbSyntax.ItemLength = sublen
                    AbSyntax.AbstractSyntaxName = Encoding.ASCII.GetString(tmp1)
                Case &H40
                    Dim TS As New TransferSyntaxSubItem
                    TS.ItemLength = sublen
                    TS.TransferSyntaxName = Encoding.ASCII.GetString(tmp1)
                    TransSyntax.Add(TS)
            End Select

            sublen += 4
        Loop While (tmp.Length > sublen)

    End Sub
End Class