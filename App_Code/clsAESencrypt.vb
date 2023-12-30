Imports System
Imports System.IO
Imports System.Security.Cryptography
Public Class clsAESencrypt


    Public Shared myAes As Aes = Aes.Create()
    Public Shared mKey As Byte()
    Public Shared mIV As Byte()

    Public Sub New()
        mKey = myAes.Key
        mIV = myAes.IV
        myAes.Key = mKey
        myAes.IV = mIV
    End Sub




    Public Shared Function EncryptStringToBytesNew(ByVal plainText As String) As Byte()
        ' Check arguments.
        If Equals(plainText, Nothing) OrElse plainText.Length <= 0 Then Throw New ArgumentNullException("plainText")
        If myAes.Key Is Nothing OrElse myAes.Key.Length <= 0 Then Throw New ArgumentNullException("Key")
        If myAes.IV Is Nothing OrElse myAes.IV.Length <= 0 Then Throw New ArgumentNullException("IV")
        Dim encrypted As Byte()


        ' Create an Aes object
        ' with the specified key and IV.
        Using aesAlg As Aes = Aes.Create()


            ' Create an encryptor to perform the stream transform.
            Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(mKey, mIV)


            ' Create the streams used for encryption.
            Using msEncrypt As MemoryStream = New MemoryStream()

                Using csEncrypt As CryptoStream = New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)

                    Using swEncrypt As StreamWriter = New StreamWriter(csEncrypt)
                        'Write all data to the stream.
                        swEncrypt.Write(plainText)
                    End Using

                    encrypted = msEncrypt.ToArray()
                End Using
            End Using
        End Using


        ' Return the encrypted bytes from the memory stream.
        Return encrypted
    End Function

    Public Shared Function EncryptStringToBytes(ByVal plainText As String, ByVal Key As Byte(), ByVal IV As Byte()) As Byte()
        ' Check arguments.
        If Equals(plainText, Nothing) OrElse plainText.Length <= 0 Then Throw New ArgumentNullException("plainText")
        If Key Is Nothing OrElse Key.Length <= 0 Then Throw New ArgumentNullException("Key")
        If IV Is Nothing OrElse IV.Length <= 0 Then Throw New ArgumentNullException("IV")
        Dim encrypted As Byte()


        ' Create an Aes object
        ' with the specified key and IV.
        Using aesAlg As Aes = Aes.Create()
            aesAlg.Key = Key
            aesAlg.IV = IV

            ' Create an encryptor to perform the stream transform.
            Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)


            ' Create the streams used for encryption.
            Using msEncrypt As MemoryStream = New MemoryStream()

                Using csEncrypt As CryptoStream = New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)

                    Using swEncrypt As StreamWriter = New StreamWriter(csEncrypt)
                        'Write all data to the stream.
                        swEncrypt.Write(plainText)
                    End Using

                    encrypted = msEncrypt.ToArray()
                End Using
            End Using
        End Using


        ' Return the encrypted bytes from the memory stream.
        Return encrypted
    End Function

    Public Shared Function DecryptBytesToString(ByVal cipherText As Byte(), ByVal Key As Byte(), ByVal IV As Byte()) As String
        ' Check arguments.
        If cipherText Is Nothing OrElse cipherText.Length <= 0 Then Throw New ArgumentNullException("cipherText")
        If Key Is Nothing OrElse Key.Length <= 0 Then Throw New ArgumentNullException("Key")
        If IV Is Nothing OrElse IV.Length <= 0 Then Throw New ArgumentNullException("IV")

        ' Declare the string used to hold
        ' the decrypted text.
        Dim plaintext As String = Nothing


        ' Create an Aes object
        ' with the specified key and IV.
        Using aesAlg As Aes = Aes.Create()
            aesAlg.Key = Key
            aesAlg.IV = IV

            ' Create a decryptor to perform the stream transform.
            Dim decryptor As ICryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)


            ' Create the streams used for decryption.
            Using msDecrypt As MemoryStream = New MemoryStream(cipherText)

                Using csDecrypt As CryptoStream = New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)

                    Using srDecrypt As StreamReader = New StreamReader(csDecrypt)

                        ' Read the decrypted bytes from the decrypting stream
                        ' and place them in a string.
                        plaintext = srDecrypt.ReadToEnd()
                    End Using
                End Using
            End Using
        End Using

        Return plaintext
    End Function
    Public Function GetAndReturnString(ByVal s As String) As String
        Dim original As String = s


        ' Create a new instance of the Aes
        ' class.  This generates a new key and initialization
        ' vector (IV).
        Using myAes As Aes = Aes.Create()

            ' Encrypt the string to an array of bytes.
            Dim encrypted As Byte() = EncryptStringToBytes(original, myAes.Key, myAes.IV)

            ' Decrypt the bytes to a string.
            Dim roundtrip As String = DecryptBytesToString(encrypted, myAes.Key, myAes.IV)
            Return roundtrip
            'Display the original data and the decrypted data.
            'Console.WriteLine("Original:   {0}", original)
            'Console.WriteLine("Round Trip: {0}", roundtrip)
        End Using

    End Function

End Class
