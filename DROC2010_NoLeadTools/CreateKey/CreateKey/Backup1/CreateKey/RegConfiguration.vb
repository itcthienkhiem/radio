Public Class RegConfiguration
    Public Sub New()

    End Sub
    Public Shared Sub SaveSettings(ByVal Key1 As String, ByVal Key2 As String, ByVal Key3 As String, ByVal Value As String)
        SaveSetting(Key1, Key2, Key3, Value)
    End Sub
    Public Shared Function GetSettings(ByVal Key1 As String, ByVal Key2 As String, ByVal Key3 As String) As String
        Return GetSetting(Key1, Key2, Key3)
    End Function

    Public Shared Sub DeleteSettings(ByVal Key1 As String, ByVal Key2 As String, ByVal Key3 As String)
        DeleteSetting(Key1, Key2, Key3)
    End Sub
    Public Shared Function ToInt(ByVal value As Boolean) As Integer
        If value Then
            Return 1
        Else
            Return 0
        End If
    End Function
    Public Shared Function ToBool(ByVal value As Integer) As Boolean
        If value = 1 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function ToBool(ByVal value As String) As Boolean
        If value = "1" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function ToInt(ByVal value As String, Optional ByVal isColor As Boolean = False) As Integer
        If Not IsNumeric(value) Then Return 0
        Return CInt(value)
    End Function
    Public Shared Function intDbnull(ByVal value As String, Optional ByVal isColor As Boolean = False) As Integer
        If isColor Then
            If Not IsNumeric(value) OrElse IsDBNull(value) OrElse IsNothing(value) Then Return Color.Black.ToArgb
            Return CInt(value)
        Else
            If Not IsNumeric(value) OrElse IsDBNull(value) OrElse IsNothing(value) Then Return 0
            Return CInt(value)
        End If
    End Function
    Public Shared Function intDbnull(ByVal value As Object, Optional ByVal Reval As Integer = 0) As Integer

        If Not IsNumeric(value) OrElse IsDBNull(value) OrElse IsNothing(value) Then
            Return Reval
        Else
            Return CInt(value)
        End If
    End Function
    Public Shared Function sDbnull(ByVal value As Object, Optional ByVal Reval As String = "") As String

        If IsDBNull(value) OrElse IsNothing(value) Then
            Return Reval
        Else
            Return value.ToString()
        End If
    End Function
    Public Shared Function ColorFromRGB(ByVal RGB As Integer) As Color
        Try
            Return Color.FromArgb(RGB)
        Catch ex As Exception
            Return Color.Black
        End Try

    End Function
End Class

