'PS 3.8-2008
'Page 38
'Table 9-17
Imports System.Text
Imports VIETBAIT.DICOMPDU


Public Class AAssociateAC
    Inherits PDU
    Const ProtocolVerValue As UInt16 = &H1
    Dim CalledAETValue, CallingAETValue As String
    Public AppCtxItemVar As New AppContextItem
    Public PreCtxItemVar As New ArrayList
    Public UserInfoItemVar As New UserInfoItem
    Public Sub New()
        PDUType = &H2
    End Sub
    Sub New(ByVal calledAETitle As String, ByVal callingAETitle As String)
        MyClass.New()
        CalledAET = calledAETitle
        CallingAET = callingAETitle
    End Sub
    Public ReadOnly Property ProtocolVer() As UInt16
        Get
            Return ProtocolVerValue
        End Get
    End Property

    Public Property CalledAET() As String
        Get
            Return CalledAETValue
        End Get
        Set(ByVal value As String)
            CalledAETValue = value
        End Set
    End Property

    Public Property CallingAET() As String
        Get
            Return CallingAETValue
        End Get
        Set(ByVal value As String)
            CallingAETValue = value
        End Set
    End Property


    Function CreateByteBuff() As Byte()
        Dim BB(73) As Byte
        Try
            Dim tmp() As Byte = {}
            Dim i As Byte
            Dim BBLen As UInt32
            Array.Resize(BB, 74)
            BB(0) = PDUType
            BB(1) = &H0
            tmp = BitConverter.GetBytes(MyClass.ProtocolVer)
            Array.Reverse(tmp)
            Array.Copy(tmp, 0, BB, 6, 2)
            BB(8) = 0
            BB(9) = 0
            tmp = Encoding.ASCII.GetBytes(CalledAET)
            Array.Copy(tmp, 0, BB, 10, 16)
            tmp = Encoding.ASCII.GetBytes(CallingAET)
            Array.Copy(tmp, 0, BB, 26, 16)
            For i = 42 To 73
                BB(i) = 0
            Next i
            BBLen = BB.Length
            tmp = AppCtxItemVar.CreateAppCtxItem()
            Array.Resize(BB, BBLen + tmp.Length)
            Array.Copy(tmp, 0, BB, BBLen, tmp.Length)
            For Each p As PreContextItem In PreCtxItemVar
                tmp = p.CreatePreCtxItemAC()
                BBLen = BB.Length
                Array.Resize(BB, BBLen + tmp.Length)
                Array.Copy(tmp, 0, BB, BBLen, tmp.Length)
            Next
            tmp = UserInfoItemVar.CreateUserInfoSubItem()
            BBLen = BB.Length
            Array.Resize(BB, BBLen + tmp.Length)
            Array.Copy(tmp, 0, BB, BBLen, tmp.Length)
            BBLen = BB.Length
            MyClass.Length = BBLen - 6
            tmp = BitConverter.GetBytes(MyClass.Length)
            Array.Reverse(tmp)
            Array.Copy(tmp, 0, BB, 2, 4)
            Return BB
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
End Class