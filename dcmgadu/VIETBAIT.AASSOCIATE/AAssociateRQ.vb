'PS 3.8-2008
'Page 38
'Table 9-11
Imports System.Text
Imports VIETBAIT.DICOMPDU


Public Class AAssociateRQ
    Inherits PDU

    Const ProtocolVersion As UInt16 = &H1
    Dim CalledAET As String
    Dim CallingAET As String
    Public AppCtxItemVar As New AppContextItem
    Public PreCtxItems As New ArrayList
    Public UserInfoItemVar As New UserInfoItem

    Sub New()
        PDUType = &H1
    End Sub
    Sub New(ByVal len As UInt32)
        MyClass.New()
        Length = len
    End Sub

    Sub New(ByVal len As UInt32, ByVal calledAET As String, ByVal callingAET As String)
        MyClass.New()
        Length = len
        CalledAETittle = calledAET
        CallingAETittle = callingAET
    End Sub

    Public Property CalledAETittle() As String
        Get
            Return CalledAET
        End Get
        Set(ByVal value As String)
            CalledAET = value
        End Set
    End Property

    Public Property CallingAETittle() As String
        Get
            Return CallingAET
        End Get
        Set(ByVal value As String)
            CallingAET = value
        End Set
    End Property

    Function CreateByteBuff() As Byte()
        Dim BB(73) As Byte
        Dim tmp() As Byte = {}
        Dim c() As Char
        Dim i As Byte
        Dim pos As UInt32

        BB(0) = PDUType
        BB(1) = &H0

        tmp = BitConverter.GetBytes(ProtocolVersion)
        Array.Reverse(tmp)
        Array.Copy(tmp, 0, BB, 6, 2)
        BB(8) = 0
        BB(9) = 0
        If CalledAETittle.Length < 16 Then
            CalledAETittle = CalledAETittle.PadRight(16)
        End If
        If CallingAETittle.Length < 16 Then
            CallingAETittle = CallingAETittle.PadRight(16)
        End If
        c = CalledAETittle.ToCharArray
        ReDim tmp(c.Length - 1)
        For i = 0 To c.Length - 1
            tmp(i) = Asc(c(i))
        Next
        Array.Copy(tmp, 0, BB, 10, 16)
        c = CallingAET.ToCharArray
        ReDim tmp(c.Length - 1)
        For i = 0 To c.Length - 1
            tmp(i) = Asc(c(i))
        Next
        Array.Copy(tmp, 0, BB, 26, 16)
        For i = 42 To 73
            BB(i) = 0
        Next i
        pos = 74
        Length = 74 - 6
        tmp = AppCtxItemVar.CreateAppCtxItem
        Array.Resize(BB, Length + 6 + tmp.Length)
        Array.Copy(tmp, 0, BB, Length + 6, tmp.Length)
        Length += tmp.Length
        Dim pc As New PreContextItem
        For Each pc In MyClass.PreCtxItems
            tmp = pc.CreatePreCtxItemRQ
            Array.Resize(BB, Length + 6 + tmp.Length)
            Array.Copy(tmp, 0, BB, Length + 6, tmp.Length)
            Length += tmp.Length
        Next

        tmp = MyClass.UserInfoItemVar.CreateUserInfoSubItem
        Array.Resize(BB, Length + 6 + tmp.Length)
        Array.Copy(tmp, 0, BB, Length + 6, tmp.Length)
        Length += tmp.Length

        tmp = BitConverter.GetBytes(Length)
        Array.Reverse(tmp)
        Array.Copy(tmp, 0, BB, 2, 4)

        Return BB
    End Function

    Sub AAssociateRQParse(ByVal bb() As Byte)
        Dim tmp(15) As Byte
        Dim tmp1(15) As Char
        CalledAET = Encoding.ASCII.GetString(bb, 4, 16)
        CallingAET = Encoding.ASCII.GetString(bb, 20, 16)
        Dim VariableItem(MyBase.Length - 74 + 6 - 1) As Byte
        Array.Copy(bb, 68, VariableItem, 0, MyBase.Length - 68)
        Dim len As UInt16 = 0
        Dim LenArray(1) As Byte
        Dim IType As Byte
        Do
            Array.Copy(VariableItem, len, VariableItem, 0, VariableItem.Length - len)
            Array.Resize(VariableItem, VariableItem.Length - len)
            IType = VariableItem(0)
            Array.Copy(VariableItem, 2, LenArray, 0, 2)
            Array.Reverse(LenArray)
            len = BitConverter.ToUInt16(LenArray, 0)
            Array.Resize(tmp, len)
            Array.Copy(VariableItem, 4, tmp, 0, len)
            Select Case IType
                Case &H10
                    AppCtxItemVar = New AppContextItem
                    AppCtxItemVar.ItemLength = len
                    AppCtxItemVar.AppContextName = Encoding.ASCII.GetString(tmp)
                Case &H20
                    Dim pc As New PreContextItem
                    pc.ItemLength = len
                    pc.PreCtxRQParse(tmp)
                    PreCtxItems.Add(pc)
                Case &H50
                    UserInfoItemVar = New UserInfoItem
                    UserInfoItemVar.ItemLength = len
                    UserInfoItemVar.UserInfoSubItemParse(tmp)
            End Select
            len += 4
        Loop While (VariableItem.Length > len)
    End Sub
End Class