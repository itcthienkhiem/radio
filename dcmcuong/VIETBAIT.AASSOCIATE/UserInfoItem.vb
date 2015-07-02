
Imports System.Text
Imports VIETBAIT.DICOMPDU


Public Class UserInfoSubItem
    Inherits ItemClass

    Sub New(ByVal value As Byte)
        MyBase.New(value)
    End Sub

    Dim ItemDataVal As Byte()

    Public Property ItemData() As Byte()
        Get
            Return ItemDataVal
        End Get
        Set(ByVal value As Byte())
            MyBase.ItemLength = value.Length
            ItemDataVal = value
        End Set
    End Property

    Public Overloads Sub SetItemData(ByVal value As String)
        ItemData = Encoding.ASCII.GetBytes(value)
    End Sub

    Public Overloads Sub SetItemData(ByVal value As UInt32)
        ItemData = BitConverter.GetBytes(value)
        Array.Reverse(ItemData)
    End Sub

    Public Function CreateUserInfoBuffer() As Byte()
        Dim tmp(MyBase.ItemLength + 4 - 1) As Byte
        tmp(0) = ItemType
        tmp(1) = 0
        Dim tmp1() As Byte = BitConverter.GetBytes(MyBase.ItemLength)
        Array.Reverse(tmp1)
        Array.Copy(tmp1, 0, tmp, 2, 2)
        Array.Copy(ItemData, 0, tmp, 4, MyBase.ItemLength)
        Return tmp
    End Function
End Class

Public Class UserInfoItem
    Inherits ItemClass

    Sub New()
        MyBase.New(&H50)
    End Sub

    Public Const MaxPDULengthType As Byte = &H51
    Public Const ImpClassUIDType As Byte = &H52
    Public UserDataArray As New ArrayList
    Dim MaxPDULengthVal As UInt32
    Dim ImpUIDVal As String

    Public Property MaxPDULength() As UInt32
        Get
            Return MaxPDULengthVal
        End Get
        Set(ByVal value As UInt32)
            MaxPDULengthVal = value
        End Set
    End Property

    Public Property ImplementationClassUID() As String
        Get
            Return ImpUIDVal
        End Get
        Set(ByVal value As String)
            ImpUIDVal = value
        End Set
    End Property

    Public Sub AddUserData(ByVal value As UserInfoSubItem)
        UserDataArray.Add(value)
    End Sub

    Public Function CreateUserInfoSubItem() As Byte()
        Dim tmp() As Byte = {}
        Dim tmp1() As Byte = {}
        Dim CurrentLen As UInt16

        For i As Byte = 0 To UserDataArray.Count - 1
            CurrentLen = tmp1.Length
            tmp = UserDataArray(i).CreateUserInfoBuffer()
            Array.Resize(tmp1, CurrentLen + tmp.Length)
            Array.Copy(tmp, 0, tmp1, CurrentLen, tmp.Length)
        Next i
        MyBase.ItemLength = tmp1.Length
        tmp = BitConverter.GetBytes(MyBase.ItemLength)
        Array.Reverse(tmp)
        Dim BB(MyBase.ItemLength + 4 - 1) As Byte
        BB(0) = ItemType
        BB(1) = 0
        Array.Copy(tmp, 0, BB, 2, 2)
        Array.Copy(tmp1, 0, BB, 4, MyBase.ItemLength)
        Return BB
    End Function

    Public Sub UserInfoSubItemParse(ByVal bb() As Byte)
        Dim lenarray(1) As Byte

        'MyBase.ItemLength = 
        Dim tmp(MyBase.ItemLength - 1) As Byte
        Dim StructLen As UInt16 = 0
        Array.Copy(bb, tmp, MyBase.ItemLength)
        Do
            Array.Copy(tmp, StructLen, tmp, 0, tmp.Length - StructLen)
            Array.Resize(tmp, tmp.Length - StructLen)
            Dim structtmp As New UserInfoSubItem(tmp(0))
            Array.Copy(tmp, 2, lenarray, 0, 2)
            Array.Reverse(lenarray)
            structtmp.ItemLength = BitConverter.ToUInt16(lenarray, 0)
            Dim Datatmp(structtmp.ItemLength - 1) As Byte
            Array.Copy(tmp, 4, Datatmp, 0, structtmp.ItemLength)
            structtmp.ItemData = Datatmp
            AddUserData(structtmp)
            StructLen = structtmp.ItemLength + 4
            Select Case structtmp.ItemType
                Case MaxPDULengthType
                    Array.Reverse(structtmp.ItemData)
                    MaxPDULength = BitConverter.ToUInt32(structtmp.ItemData, 0)
                Case ImpClassUIDType
                    ImplementationClassUID = Encoding.ASCII.GetString(structtmp.ItemData)
            End Select
        Loop While (tmp.Length > StructLen)

    End Sub
End Class