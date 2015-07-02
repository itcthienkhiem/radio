Imports System.IO
Public Class PreContextItemAC
    Dim ItemType As Byte
    Dim ItemLen As UInt16
    Dim PreCtxID As Byte
    Dim ResultReason As Byte = 0 'Accept
    Dim TransferSyntax As AppContextItem
    Public Sub GetAppCtxLen(ByVal Buff As Byte())
        ItemLen = Buff(2) * 256 + Buff(3)
    End Sub
    Public Sub GetAppCtx(ByVal buff As Byte())
        Dim sTemp As String = String.Empty
        Dim i As Byte
        Dim c As Char()
        GetAppCtxLen(buff)
        ReDim c(ItemLen - 1)
        For i = 0 To ItemLen - 1
            c(i) = Chr(buff(i + 5))
        Next
        'AppContext = String.Concat(c)
    End Sub
    Public Sub WriteTo(ByVal os As Stream)
        Dim buff As Byte()
        ReDim buff(ItemLen + 3)
        buff(0) = ItemType
        buff(1) = CByte(&H0)
        buff(2) = CByte(ItemLen)
        buff(3) = CByte(ItemLen >> 8)
        'Dim c As Char() = AppContext.ToCharArray
        'Dim i As Byte
        'For i = 0 To AppContext.Length - 1
        '    buff(i + 4) = CByte(Asc(c(i)))
        'Next i
        If os.CanWrite Then
            os.Write(buff, 0, buff.Length)
        Else
            Console.WriteLine("Sorry.  You cannot write to this NetworkStream.")
        End If

    End Sub
End Class
