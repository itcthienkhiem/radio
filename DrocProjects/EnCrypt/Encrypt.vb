
Imports System.IO
Imports System.Security.Cryptography
Imports System.Threading
Imports System.Text
Imports System.Windows.Forms

Public Class Encrypt
    Dim f As New Form1
    Public sPwd As String = f.Tag.ToString
    Public Fam_PWD As String = f.Tag.ToString
    Public DBPWD As String = f.lbldbpwd.Tag.ToString
    Public pSum As String = f.lblpSum.Tag.ToString
    Public Sum As String = f.lblSum.Tag.ToString
    Public AlgName As String = f.lblAlgorithsm.Tag.ToString

    Public ZAP As String = f.lblWYSIWYG.Tag.ToString
    Public CAM As String = f.lblCropAndMark.Tag.ToString
    Public MOC As String = f.lblFreeMemoryOnCell.Tag.ToString
    Public B16 As String = f.lblAllowBurn.Tag.ToString

    Public BPath As String = f.lblBurnPath.Tag.ToString
    Public CamPath As String = f.lblCam.Tag.ToString
    Public MOCPath As String = f.lblFMOC.Tag.ToString
    Public ZapPath As String = f.lblZap.Tag.ToString

    Public AllowSum As String = f.lblAllowSum.Tag.ToString

    Public AllowPSum As String = f.lblAllowpSum.Tag.ToString
    ' This routine creates a new symmetric algorithm object of the chosen type.
    Public Sub New(ByVal strCryptoName As String)
        DBPWD = f.lbldbpwd.Tag.ToString
        ' The shared Create method of the abstract symmetric algorithm base class
        ' implements a factory design for the creation of its concrete classes.
        crpSym = SymmetricAlgorithm.Create(strCryptoName)
        ' Initialize the byte arrays to the proper length for the 
        ' instantiated crypto class.
        ReDimByteArrays()
    End Sub
    Public Sub New()
        DBPWD = f.lbldbpwd.Tag.ToString
        ' The shared Create method of the abstract symmetric algorithm base class
        ' implements a factory design for the creation of its concrete classes.
        
    End Sub
    Public Sub UpdateAlgName(ByVal strCryptoName As String)
        crpSym = SymmetricAlgorithm.Create(strCryptoName)
        ' Initialize the byte arrays to the proper length for the 
        ' instantiated crypto class.
        ReDimByteArrays()
    End Sub
    Private abytIV() As Byte
    Private abytKey() As Byte
    Private abytSalt() As Byte
    Private crpSym As SymmetricAlgorithm
    Private strPassword As String = f.Tag.ToString
    Private strSaltIVFile As String = ""
    Private strSourceFile As String = ""

    Public Property Password() As String
        Get
            Return strPassword
        End Get
        Set(ByVal Value As String)
            strPassword = Value
        End Set
    End Property

    Public Property SaltIVFile() As String
        Get
            Return strSaltIVFile
        End Get
        Set(ByVal Value As String)
            If File.Exists(Value) Then
                strSaltIVFile = Value
            Else
                Throw New FileNotFoundException("Không tìm thấy file data.dat")
            End If
        End Set
    End Property

    ' This routine decrypts a file.
    Public Function DecryptString(ByVal strCipherText As String) As String

        ' If the password is an empty string assume the user has not checked the 
        ' "Advanced" CheckBox or has not entered a password and thus is not using
        ' a password-derived key. In such a case the symmetric algorithm object 
        ' will just use its default values.
        If strPassword <> "" Then
            OpenSaltIVFileAndSetKeyIV()
        End If

        Dim uEncode As New UnicodeEncoding
        Dim aEncode As New ASCIIEncoding
        Dim bytCipherText() As Byte = Convert.FromBase64String(strCipherText)
        Dim stmPlainText As New MemoryStream
        Dim stmCipherText As New MemoryStream(bytCipherText)

        ' Read in the encrypted file and decrypt.
        Dim csDecrypted As New CryptoStream(stmCipherText, crpSym.CreateDecryptor(), _
            CryptoStreamMode.Read)
        ' Create a StreamWriter to write to the temp file.
        Dim swWriter As New StreamWriter(stmPlainText)
        ' Read the decrypted stream into a StreamReader.
        Dim srReader As New StreamReader(csDecrypted)

        Try
            ' Write out the decrypted stream.
            swWriter.Write(srReader.ReadToEnd)
        Catch expCrypto As CryptographicException
            Throw New CryptographicException
        Finally
            ' Close and clean up.
            swWriter.Close()
            csDecrypted.Close()
        End Try
        Return uEncode.GetString(stmPlainText.ToArray())
        'SwapFiles(True)
    End Function

    ' This routine encrypts a file.
    Public Function EncryptString(ByVal strPlainText As String) As String

        ' If the password is an empty string assume the user has not checked the 
        ' "Advanced" CheckBox and thus is not using a password-derived key. In such
        ' a case the symmetric algorithm object will just its default values.
        If strPassword <> "" Then
            OpenSaltIVFileAndSetKeyIV()
        End If

        ' Create a FileStream object to read in the source file.
        'Dim fsInput As New FileStream(strSourceFile, FileMode.Open, FileAccess.Read)

        ' Create a byte array from the FileStream object by reading in the 
        ' source file.
        Dim uEncode As New UnicodeEncoding
        Dim aEncode As New ASCIIEncoding
        'Store plaintext as a byte array
        Dim abytInput() As Byte = uEncode.GetBytes(strPlainText)
        'Create a memory stream for holding encrypted text
        Dim stmCipherText As New MemoryStream

        ' Create a FileStream object to write to a temp file.
        'Dim fsCipherText As New FileStream("temp.dat", FileMode.Create, FileAccess.Write)
        'fsCipherText.SetLength(0)

        ' Create a Crypto Stream that transforms the file stream using the chosen 
        ' encryption and writes it to the output FileStream object.
        Dim csEncrypted As New CryptoStream(stmCipherText, crpSym.CreateEncryptor(), _
            CryptoStreamMode.Write)

        ' Pass in the unencrypted source file byte array and write out 
        ' the encrypted bytes to the temp.dat file. Thus, the logic flow is:
        ' abytInput ----> Encryption ----> fsCipherText.
        csEncrypted.Write(abytInput, 0, abytInput.Length)

        ' When the bytes are all written it's important to indicate to the crypto 
        ' stream that you are through using it, and thus to finish processing any 
        ' bytes remaining in the buffer used by the crypto stream. Typically this 
        ' involves padding the last output block to a complete multiple of the crypto 
        ' object's block size (for Rijndael this is 16 bytes, or 128 bits), 
        ' encrypting it, and then writing this final block to the memory stream.
        csEncrypted.FlushFinalBlock()

        ' Clean up. There is no need to call fsCipherText.Close() because closing the
        ' crypto stream automatically encloses the stream that was passed into it.
        csEncrypted.Close()
        Return Convert.ToBase64String(stmCipherText.ToArray())
        'SwapFiles(False)
    End Function

    ' This routine opens the .dat file, reads in the salt and IV, and then
    ' sets the crypto object's key and IV.
    Private Sub OpenSaltIVFileAndSetKeyIV()

        ' Initialize the byte arrays to the proper length for the 
        ' instantiated crypto class.
        ReDimByteArrays()

        ' Create a Filestream object to read in the contents of the .dat file
        ' that contains the salt and IV.
        Dim fsKey As New FileStream(strSaltIVFile, FileMode.Open, FileAccess.Read)
        fsKey.Read(abytSalt, 0, abytSalt.Length)
        fsKey.Read(abytIV, 0, abytIV.Length)
        fsKey.Close()

        ' Derive the key from the salted password.
        Dim pdb As New PasswordDeriveBytes(strPassword, abytSalt)
        ' Get the same amount of bytes as the current abytKey length as set in 
        ' ReDimByteArrays().
        abytKey = pdb.GetBytes(abytKey.Length)


        ' Assign the byte arrays to the Key and IV properties of the instantiated
        ' symmetric crypto class. 
        crpSym.Key = abytKey
        crpSym.IV = abytIV

    End Sub

    ' This routine redimensions the byte arrays to the proper length for the 
    ' instantiated crypto class.
    Private Sub ReDimByteArrays()

        If crpSym.GetType Is GetType(System.Security.Cryptography.RijndaelManaged) Then
            ' The Key byte array size was retrieved via the LegalKeySizes property 
            ' of the crypto object. See the Debug.WriteLine statements that follow. 
            ' Keep in mind that the array size is always one more than the upper 
            ' bound, which you use to initialize the array. So the ReDim sizes are 
            ' 1 less than the legal key sizes acquired above.
            ReDim abytKey(31)
            ' A good rule-of-thumb is to make the salt 1/2 the length of the key. For
            ' more information on what "salt" is, see SetKeyIVAndSaveToFile().
            ReDim abytSalt(15)
            ' There is no "LegalIVSizes" property like there is for key sizes. 
            ' Therefore, to figure out the valid IV byte array length you can do the
            ' following:
            '       crpSym.GenerateIV()
            '       abytIV = crpSym.IV
            '       Debug.WriteLine("Valid abytIV.Length=" & abytIV.Length.ToString)
            ReDim abytIV(15)
        Else
            ReDim abytKey(23)
            ReDim abytSalt(11)
            ReDim abytIV(7)
        End If
    End Sub
    Public Function Mahoa(ByVal chuoi As String) As String
        Try
            Dim crpSample As New Encrypt(AlgName)
            Dim s As String = Application.StartupPath & "\data.dat"
            With crpSample
                .SaltIVFile = s
                .Password = sPwd
            End With

            Return crpSample.EncryptString(chuoi).Replace("'", "0")
        Catch expCrypto As Exception
            Return "EXCEPTION"
        End Try
    End Function
    Public Function GiaiMa(ByVal chuoi As String) As String
        Try
            Dim crpSample As New Encrypt(AlgName)
            Dim s As String = Application.StartupPath & "\data.dat"
            With crpSample
                .SaltIVFile = s
                .Password = sPwd
            End With

            Return crpSample.DecryptString(chuoi).Replace("'", "0")
        Catch expCrypto As Exception
            Return "EXCEPTION"
        End Try
    End Function




End Class
