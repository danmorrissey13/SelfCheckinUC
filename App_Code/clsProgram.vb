Imports System
Imports System.Text
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json

Public Class clsProgram
    Private Shared Sub Main(ByVal args As String())
        'Dim consumerKey As String = "#####"
        'Dim consumerSecret As String = "*****"
        'Dim accessToken As String
        'Dim byte1 As Byte() = Encoding.ASCII.GetBytes("grant_type=client_credentials")
        'Dim bearerReq As HttpWebRequest = TryCast(WebRequest.Create("https://api.byu.edu/token"), HttpWebRequest)
        'bearerReq.Accept = "application/json"
        'bearerReq.Method = "POST"
        'bearerReq.ContentType = "application/x-www-form-urlencoded"
        'bearerReq.ContentLength = byte1.Length
        'bearerReq.KeepAlive = False
        'bearerReq.Headers.Add("Authorization", "Basic " & Convert.ToBase64String(Encoding.Default.GetBytes(consumerKey & ":" & consumerSecret)))
        'Dim newStream As Stream = bearerReq.GetRequestStream()
        'newStream.Write(byte1, 0, byte1.Length)
        'Dim bearerResp As WebResponse = bearerReq.GetResponse()

        'Using reader = New StreamReader(bearerResp.GetResponseStream(), Encoding.UTF8)
        '    Dim response = reader.ReadToEnd()
        '    Dim bearer As clsBearer = JsonConvert.DeserializeObject(Of clsBearer)(response)
        '    accessToken = bearer.access_token
        'End Using

        'Console.WriteLine(accessToken)
        'Console.Read()
    End Sub
    Public Function GetToken() As String
        Dim consumerKey As String = "#####"
        Dim consumerSecret As String = "*****"
        Dim accessToken As String
        Dim byte1 As Byte() = Encoding.ASCII.GetBytes("grant_type=client_credentials")
        Dim bearerReq As HttpWebRequest = TryCast(WebRequest.Create("https://api.byu.edu/token"), HttpWebRequest)
        bearerReq.Accept = "application/json"
        bearerReq.Method = "POST"
        bearerReq.ContentType = "application/x-www-form-urlencoded"
        bearerReq.ContentLength = byte1.Length
        bearerReq.KeepAlive = False
        bearerReq.Headers.Add("Authorization", "Basic " & Convert.ToBase64String(Encoding.Default.GetBytes(consumerKey & ":" & consumerSecret)))
        Dim newStream As Stream = bearerReq.GetRequestStream()
        newStream.Write(byte1, 0, byte1.Length)
        Dim bearerResp As WebResponse = bearerReq.GetResponse()

        Using reader = New StreamReader(bearerResp.GetResponseStream(), Encoding.UTF8)
            Dim response = reader.ReadToEnd()
            Dim bearer As clsBearer = JsonConvert.DeserializeObject(Of clsBearer)(response)
            accessToken = bearer.Access_token
        End Using
        Return accessToken
        'Console.WriteLine(accessToken)
        'Console.Read()

    End Function
End Class
