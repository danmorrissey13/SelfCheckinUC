Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports Microsoft.VisualBasic.Strings
Imports System.IO
Imports System.Security.Cryptography

Public Class clsMobilePhoto
    Protected CON_STR As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString '"ConnectionName1"]
    Protected CN As SqlConnection
    Public mstrUserName As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split("\").Last()
    Private mstrIP As String = ""
    Protected Sub BASE_SetConSQL(ByVal Open As Boolean)
        Try
            If Open Then
                If IsNothing(CN) Then
                    CN = New SqlClient.SqlConnection(CON_STR)
                End If
                If CN.State = ConnectionState.Closed Then
                    CN.Open()
                End If
            Else
                If Not IsNothing(CN) Then
                    If CN.State = ConnectionState.Open Then CN.Close()
                End If
            End If
        Catch ex As Exception
            Throw New Exception("Base.SetCon Error: " & ex.Message)
        End Try
    End Sub
    Public Function getImageIDs() As DataTable

        'usp_MDCN_naham_ID_InsertBulk
        Dim strRetVal As String = ""

        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_GetImageIDs"
                da.SelectCommand = cmd
                da.Fill(dt)
                dt.TableName = "IDs"
                Return dt

            End With
        Catch ex As Exception
            Throw New Exception("Error in getImageIDs  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try

    End Function
    'Private Sub SaveImage()
    '    If FileUpload1.HasFile Then
    '        Dim imagefilelenth As Integer = FileUpload1.PostedFile.ContentLength
    '        Dim imgarray As Byte() = New Byte(imagefilelenth - 1) {}
    '        Dim image As HttpPostedFile = FileUpload1.PostedFile
    '        image.InputStream.Read(imgarray, 0, imagefilelenth)
    '        Dim con As SqlConnection = New SqlConnection("Data Source=localhost;Initial Catalog=myDatabase;Integrated Security=True")
    '        Dim cmd As SqlCommand = New SqlCommand("Insert into Images (ImageName, Image) values (@ImageName, @Image)", con)
    '        con.Open()
    '        cmd.Parameters.AddWithValue("@ImageName", SqlDbType.VarChar).Value = TextBox1.Text
    '        cmd.Parameters.AddWithValue("@Image", SqlDbType.Image).Value = imgarray
    '        cmd.ExecuteNonQuery()
    '        BindGrid()
    '    End If
    'End Sub

End Class
