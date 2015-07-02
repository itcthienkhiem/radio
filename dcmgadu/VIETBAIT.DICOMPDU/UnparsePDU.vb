Imports System.IO


Public Class UnparsePDU
    Inherits PDU

    Sub New(ByVal s As Stream, ByVal buff As Byte())
        If (buff Is Nothing) Then
            ReDim buff(9)
        ElseIf (buff.Length < 6) Then
            ReDim buff(9)
        End If
        ReadFully(s, buff, 0, 6)
        MyBase.PDUType = buff(0)
        Dim arrayLen(3) As Byte
        Array.Copy(buff, 2, arrayLen, 0, 4)
        Array.Reverse(arrayLen)
        MyBase.Length = BitConverter.ToUInt32(arrayLen, 0)  '((buff (2) And &HFF) << 24) Or ((buff (3) And &HFF) << 16) Or ((buff (4) And &HFF) << 8) Or ((buff (5) And &HFF) << 0)

        If buff.Length < 6 + MyBase.Length Then
            ReDim MyClass.Buff(MyBase.Length - 1)
            Array.Copy(buff, MyClass.Buff, 6)
        Else
            MyClass.Buff = buff
        End If
        ReadFully(s, MyClass.Buff, 0, MyBase.Length)
    End Sub

    Private BuffVal() As Byte

    Public Property Buff() As Byte()
        Get
            Return BuffVal
        End Get
        Set(ByVal value As Byte())
            BuffVal = value
        End Set
    End Property

    Public Function buffer() As Byte()
        Return buff
    End Function

    Function IsLast(ByVal off As UInt32) As Boolean
        If (buffer(off) And &H2) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub ReadFully(ByVal s As Stream, ByRef b As Byte(), ByVal offset As UInt32, ByVal len As UInt32)
        Dim n As UInt32 = 0
        Try
            While n < len
                Dim count As UInt32 = s.Read(b, offset + n, len - n)
                If count > 0 Then
                    n += count
                Else
                    Exit While
                End If
            End While
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            s.Close()
        End Try
    End Sub
End Class