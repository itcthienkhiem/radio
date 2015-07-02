'Imports VIETBAIT.DICOM.BASE
Imports VIETBAIT.DICOMHelper


Public Class PDataTFPDU
    Inherits PDU

    Sub New (ByVal p As Byte)
        MyBase.PDUType = p
    End Sub
    Sub New(ByVal PreCtxID As Byte, ByVal IsCommand As Boolean, ByVal IsLast As Boolean)
        MyClass.New(&H4)
        MyClass.PDVContent.PresentationContextID = PreCtxID
        MyClass.PDVContent.Command(IsCommand)
        MyClass.PDVContent.Last(IsLast)
    End Sub
    Sub New (ByVal u As UnparsePDU)
        Parse (u)
    End Sub

    Public PDVContent As New PDVItem
    'Public hashDataElement As Hashtable


    Sub Parse(ByVal u As UnparsePDU)
        Try
            MyBase.PDUType = u.PDUType
            MyBase.Length = u.Length
            PDVContent.Length = PDVContent.GetLength(u.buffer, 0)
            PDVContent.Parse(u.buffer, 4)
            'Ultility.ParseToHashtable(u.buffer, u.Length, 6, PDVContent.DataElementHashTable)
            Debug.WriteLine("PAUSE")
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
        End Try

    End Sub

    Overloads Function CreateByteBuff() As Byte()
        Dim tmp() As Byte = {}

        Dim BB(Length + 6 - 1) As Byte
        BB (0) = PDUType
        BB (1) = &H0

        tmp = PDVContent.CreateByteBuffer()
        Array.Resize (BB, tmp.Length + 6)
        Array.Copy (tmp, 0, BB, 6, tmp.Length)
        Length = (PDVContent.Length + 4)
        tmp = BitConverter.GetBytes (Length)
        Array.Reverse (tmp)
        Array.Copy (tmp, 0, BB, 2, 4)
        Return BB
    End Function
    Overloads Function CreateByteBuff(ByVal ht As Hashtable) As Byte()
        Dim tmp() As Byte = {}

        Dim BB(Length + 6 - 1) As Byte
        BB(0) = PDUType
        BB(1) = &H0

        tmp = PDVContent.CreateByteBuffer()
        Array.Resize(BB, tmp.Length + 6)
        Array.Copy(tmp, 0, BB, 6, tmp.Length)
        Length = (PDVContent.Length + 4)
        tmp = BitConverter.GetBytes(Length)
        Array.Reverse(tmp)
        Array.Copy(tmp, 0, BB, 2, 4)
        Return BB
    End Function
    Overloads Function CreateByteBuff (ByVal PreCtxID As Byte, ByVal IsCommand As Boolean, ByVal IsLast As Boolean) _
        As Byte()
        MyClass.PDUType = &H4
        MyClass.PDVContent.PresentationContextID = PreCtxID
        MyClass.PDVContent.Command (IsCommand)
        MyClass.PDVContent.Last (IsLast)
        Dim tmp As Byte() = MyClass.CreateByteBuff()
        Return tmp
    End Function
    
    'Overloads Function CreateBufferFromHashtable(ByVal PreCtxID As Byte, ByVal IsCommand As Boolean, ByVal IsLast As Boolean, ByVal ht As Hashtable) _
    '    As Byte()
    '    MyClass.PDUType = &H4
    '    MyClass.PDVContent.PresentationContextID = PreCtxID
    '    MyClass.PDVContent.Command(IsCommand)
    '    MyClass.PDVContent.Last(IsLast)
    '    MyClass.PDVContent.hashDataElement = ht
    '    Dim tmp As Byte() = MyClass.PDVContent.CreateByteBuffer()
    '    Return tmp
    'End Function
End Class