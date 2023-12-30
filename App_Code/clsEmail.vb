Imports System.Net
Imports System.Net.Mail


Class clsEmail
    Private smtpAddress As String = "smtp.gmail.com"
    Private portNumber As Integer = 465
    Private enableSSL As Boolean = True
    Private emailFromAddress As String = "danmorrissey13@gmail.com"
    Private password As String = "M620z1331$"
    Private emailToAddress As String = "dan@marketing2go.biz"
    Private subject As String = "Hello"
    Private body As String = "Hello, This is Email sending test using gmail."

    'Private Shared Sub Main(ByVal args As String())
    '    SendEmail()
    'End Sub

    Public Sub SendEmail()
        Dim strErr As String
        'Try

        '    Using mail As New MailMessage ' = New MailMessage()
        '        mail.From = New MailAddress(emailFromAddress)
        '        mail.[To].Add(emailToAddress)
        '        mail.Subject = subject
        '        mail.Body = body
        '        mail.IsBodyHtml = True

        '        Using smtp As SmtpClient = New SmtpClient(smtpAddress, portNumber)
        '            'smtp.Credentials = New NetworkCredential("theshapeofwaterrestaurant@gmail.com", "Secure4now@")
        '            smtp.Credentials = New NetworkCredential(emailFromAddress, password)

        '            smtp.EnableSsl = enableSSL
        '            smtp.Send(mail)
        '        End Using
        '    End Using
        'Catch ex As Exception
        '    strErr = ex.Message
        'End Try

        Try
            Using mail As New MailMessage ' = New MailMessage()
                'mail.From = New MailAddress(emailFromAddress)
                'mail.[To].Add(emailToAddress)
                'mail.Subject = subject
                'mail.Body = body
                'mail.IsBodyHtml = True

                'Using smtp As SmtpClient = New SmtpClient(smtpAddress, portNumber)
                '    smtp.Credentials = New NetworkCredential(emailFromAddress, password)

                '    smtp.EnableSsl = enableSSL
                '    smtp.Send(mail)
                'End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
End Class

