Imports System.Management
Imports System.IO

Public Class MHardKey
    Private AppType As String
    Public GenKey As String
    Public RegKey As String
    Public pwd As String = ""
    Public loopNum As Integer = 1
    Delegate Sub _CheckKing()
    Public Sub New(ByVal _AppType As String, ByVal _loopNum As Integer, ByVal ShowCheckingKey As Boolean)
        'MessageBox.Show("OK")
        loopNum = _loopNum
        Utils.MFS.AppType = _AppType
        'Dim t As New Threading.Thread(AddressOf Checking)
        'If ShowCheckingKey Then
        '    t.Start()
        'End If
        '_ShowForm(Utils.MFS)
        Application.DoEvents()
        Me.AppType = _AppType
        GenKey = Security.HardWare.Value(_AppType)
        RegKey = Security.HardWare.GetKey(GenKey)
        Utils.bHasFound = True
        Try
            If ShowCheckingKey Then
                'If Not IsNothing(t) AndAlso t.IsAlive Then
                '    t.Abort()
                'End If
                Utils.MFS.Close()
            End If
        Catch ex As Exception
            Utils.MFS.Close()
        End Try
    End Sub

    Private Sub Checking()
        _ShowForm(Utils.MFS)
       

        'End If
    End Sub
    Delegate Sub ShowForm(ByVal f As Form)
    Sub _ShowForm(ByVal f As Form)
        Try
            If f.InvokeRequired Then
                f.Invoke(New ShowForm(AddressOf _ShowForm), New Object() {f})
            Else
                Utils.MFS.Show()
                Application.DoEvents()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Public Sub AutoGenKey(ByVal FENfolder As String, ByVal productKey As String)

        Dim FENFile As String = FENfolder & "\ltlruntimelic.fen"
        Dim RTMFile As String = FENfolder & "\ltlrtlic.rtm"
        Dim LICPathFile As String = FENfolder & "\LicPath.txt"
        Dim lstValues As New List(Of String)
        Try
            Dim sv_oEncrypt As New ect.Encrypt()
            sv_oEncrypt.UpdateAlgName(sv_oEncrypt.AlgName)
            If Not File.Exists(FENFile) Then
                Dim arrFENFiles As String() = Directory.GetFiles(Application.StartupPath, "*.FEN")
                If arrFENFiles.Length <= 0 Then
                    MessageBox.Show("Không tồn tại FEN file. Đề nghị liên hệ qua Email để được hỗ trợ chi tiết")
                    Return
                Else
                    FENFile = arrFENFiles(0)
                End If
            End If
            Using reader As New StreamReader(FENFile)
                While reader.Peek() > 0
                    Dim _lineN As String = reader.ReadLine
                    If Not _lineN Is Nothing Then
                        Dim arrValues As String()
                        arrValues = _lineN.Split(",")
                        If arrValues.Length = 2 Then
                            sv_oEncrypt.sPwd = productKey.Trim()
                            lstValues.Add(arrValues(0) & "," & sv_oEncrypt.Mahoa(arrValues(1)))
                        End If

                    End If
                End While

            End Using

            Using writer As New StreamWriter(RTMFile)
                For Each sVal As String In lstValues
                    writer.WriteLine(sVal)
                Next
                writer.Flush()
                writer.Close()
            End Using
            'WriteLicPath
            Using writer As New StreamWriter(LICPathFile)
                For Each sVal As String In lstValues
                    writer.WriteLine("ltlrtlic.rtm")
                Next
                writer.Flush()
                writer.Close()
            End Using

        Catch ex As Exception

        End Try
    End Sub

End Class
