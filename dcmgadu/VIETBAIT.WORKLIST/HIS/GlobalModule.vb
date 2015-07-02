Imports System.Data.SqlClient
Imports System.Net.Sockets
Imports System.Net
Imports VIETBAIT.DICOMPDU
Imports VIETBAIT.WORKLIST.WorkList
Imports VIETBAIT.AASSOCIATE


Namespace HIS
    Module GlobalModule

#Region "Attributies"

        Public gv_WLConn As New SqlConnection
        Public gv_sConnString As String
        Public gv_sBranchID As String
        Public gv_sDBName As String
        Public gv_sDBAdrr As String
        'Public iStep As Byte = 0
        Public server As TcpListener
        Public tblConfig As New DataTable
        Public tblFind As New DataTable

        Public iStart As Boolean = False
        Public clTemp As TcpClient

        Private RegPath As String = "HKEY_LOCAL_MACHINE\SOFTWARE\VIETBA\ServiceConfig\"
        Public AETITLE As String
        Public IPAddr As String
        Public DelayTime As String
        'Public ConfigPath As String
        Public DataBaseADr As String
        Public DataBaseID As String
        Public UserName As String
        Public Password As String


        
        Public PrinterReferenceInstanceUID As String

        Public Const ImplementationVersionName As String = "3.0.0"
        Public Const Accepted As Byte = 0
        Public CalledAETString As String = "VIETBA          "
        Public CallingAETString As String = "UP-DF550        "
        Public Const UnicodeCharset As String = "ISO_IR 192"
        Public Const SCUInitAbort As Byte = 0
        Public Const SCPInitAbort As Byte = 2
        Public Const ReasonNotSpecified As Byte = 0
        Public Const UnrecognizedPDU As Byte = 1
        Public Const UnexpectedPDU = 2
        Public Const UnrecognizedPDUParameter = 4
        Public Const UnexpectedPDUParameter = 5
        Public Const InvalidPDUParameterValue = 6


        Public ARELEASERQPDU() As Byte = {&H5, &H0, &H4, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
        Public ARELEASERPPDU() As Byte = {&H6, &H0, &H4, &H0, &H0, &H0, &H0, &H0, &H0, &H0}


        

        Public Const LOW As UInt16 = 2
        Public Const MEDIUM As UInt16 = 0
        Public Const HIGH As UInt16 = 1



        

        ' Patient Info
        Public Const PatientNameTag As UInt32 = &H100010
        Public Const PIDTag As UInt32 = &H100020
        Public Const StudyDateTag As UInt32 = &H80020


        Public AARQPDU As New AAssociateRQ
        Public u As UnparsePDU
        Public stream As NetworkStream
        Public MS As String = "STA1"
        Public RevLen As Int32 = 6
        Public ReadStep As Byte = 0
        Public CurMaxLength As UInt32 = 16384
        Public bytes(CurMaxLength - 1) As Byte
        Public AARequest As Boolean = False

        Public PreCtxID As Byte = 1
        Public bMessageID As Byte = 1

        Public gPatientName As String
        Public gPID As String
        Public gStudyDate As String

        Public GotIt As Boolean = False

#End Region

#Region "Public Method"

        ''' <summary>
        ''' Hàm loại bỏ dấu "` ' ? ~ ." trong chuỗi để truyền vào thiết bị
        ''' </summary>
        ''' <param name="s">Chuỗi cần loại bỏ dấu</param>
        ''' <returns>Chuỗi đã bỏ dấu</returns>
        ''' <remarks></remarks>
        Public Function Bodau(ByVal s As String) As String
            Dim i As Integer
            Dim ch As String
            If Trim(s) <> "" Then
                For i = 1 To Len(s)
                    ch = Mid(s, i, 1)
                    Select Case ch
                        Case "â", "ă", "ấ", "ầ", "ậ", "ẫ", "ẩ", "ắ", "ằ", "ẵ", "ẳ", "ặ", "á", "à", "ả", "ã", "ạ"
                            Mid(s, i, 1) = "a"
                        Case "Â", "Ă", "Ấ", "Ầ", "Ậ", "Ẫ", "Ẩ", "Ắ", "Ằ", "Ẵ", "Ẳ", "Ặ", "Á", "À", "Ả", "Ã", "Ạ"
                            Mid(s, i, 1) = "A"
                        Case "ó", "ò", "ỏ", "õ", "ọ", "ô", "ố", "ồ", "ổ", "ỗ", "ộ", "ơ", "ớ", "ờ", "ợ", "ở", "ỡ"
                            Mid(s, i, 1) = "o"
                        Case "Ó", "Ò", "Ỏ", "Õ", "Ọ", "Ô", "Ố", "Ồ", "Ổ", "Ỗ", "Ộ", "Ơ", "Ớ", "Ờ", "Ợ", "Ở", "Ỡ"
                            Mid(s, i, 1) = "O"
                        Case "ư", "ứ", "ừ", "ự", "ử", "ữ", "ù", "ú", "ủ", "ũ", "ụ"
                            Mid(s, i, 1) = "u"
                        Case "Ư", "Ứ", "Ừ", "Ự", "Ử", "Ữ", "Ù", "Ú", "Ủ", "Ũ", "Ụ"
                            Mid(s, i, 1) = "U"
                        Case "ê", "ế", "ề", "ệ", "ể", "ễ", "è", "é", "ẻ", "ẽ", "ẹ"
                            Mid(s, i, 1) = "e"
                        Case "Ê", "Ế", "Ề", "Ệ", "Ể", "Ễ", "È", "É", "Ẻ", "Ẽ", "Ẹ"
                            Mid(s, i, 1) = "E"
                        Case "í", "ì", "ị", "ỉ", "ĩ"
                            Mid(s, i, 1) = "i"
                        Case "Í", "Ì", "Ỉ", "Ĩ", "Ị"
                            Mid(s, i, 1) = "I"
                        Case "ý", "ỳ", "ỵ", "ỷ", "ỹ"
                            Mid(s, i, 1) = "y"
                        Case "Ý", "Ỳ", "Ỵ", "Ỷ", "Ỹ"
                            Mid(s, i, 1) = "Y"
                        Case "đ"
                            Mid(s, i, 1) = "d"
                        Case "Đ"
                            Mid(s, i, 1) = "D"
                    End Select
                Next
            End If
            Bodau = s
        End Function

        ''' <summary>
        ''' Khởi tạo kết nối
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub InitCommunication()

            Dim risConfig As New CONFIG.Config()

            If iStart Then
                Exit Sub
            End If

            server = Nothing
            Dim port As Int32 = 104
            Try
                ' Set the TcpListener on port 
                IPAddr = risConfig.GetValueFromKey("wlipaddress")
                port = risConfig.GetValueFromKey("wlserverport")
                AETITLE = risConfig.GetValueFromKey("wlserveraetitle")
                DelayTime = risConfig.GetValueFromKey("wldelaytime")
                DataBaseADr = risConfig.GetValueFromKey("risservername")
                DataBaseID = risConfig.GetValueFromKey("risdatabase")
                UserName = risConfig.GetValueFromKey("risusername")
                Password = risConfig.GetValueFromKey("rispassword")

            Catch ex As Exception
                IPAddr = "127.0.0.1"
                port = "104"
                AETITLE = "VIETBA"
                DelayTime = "1000"
                DataBaseADr = "127.0.0.1"
                DataBaseID = "JCLV"
                UserName = "1"
                Password = "1"

            End Try
            Dim localAddr As IPAddress = IPAddress.Parse(IPAddr)
            Dim ds As New DataSet

            Dim cls As New clsPatient
            If Not cls.spResetDBConnect Then
                'If Not cls.KhoiTaoKetNoi Then
                Exit Sub
            End If

            Try
                server = New TcpListener(localAddr, port)
                server.Start()
            Catch ex As Exception
                Console.WriteLine(ex.ToString)
            End Try


        End Sub

#End Region

    End Module
End Namespace