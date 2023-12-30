
' Unmerged change from project '1_App_Code' 
' Before:
' Imports Microsoft.VisualBasic
' After:
' Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
' Unmerged change from project '1_App_Code' 
' Before:
' Imports System.Collections.Generic
' After:
' Imports Microsoft.VisualBasic


Public Class clsFacil
    Implements IDisposable
    Protected CON_STR As String = ConfigurationManager.ConnectionStrings("PROD_Saratogaprod_Connection").ConnectionString
    'Protected CON_STR As String = ConfigurationManager.ConnectionStrings("PROD_Saratogadev_Connection").ConnectionString
    Protected CN As SqlConnection

    Public mstrUserName As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split("\").Last()
    Private mstrIP As String = ""
    Public ARY_CON_STR As String() = CON_STR.Split(";")

    Private mintFacilID As Integer
    Private mstrFacilName As String
    Private mstrFacilDesc As String
    Private mstrFacilSubTitle As String
    Private mstrAddr As String
    Private mstrCity As String



    Private mstrState As String
    Private mstrZip As String
    Private mstrPhone As String
    Private mstrEmail As String
    Private mstrLogoImg As String

    Public ReadOnly Property FcilID() As String
        Get
            Return mintFacilID
        End Get
    End Property
    Public Property Name() As String
        Get
            Return mstrFacilName
        End Get
        Set(ByVal value As String)
            mstrFacilName = value
        End Set
    End Property
    Public Property Desc() As String
        Get
            Return mstrFacilDesc
        End Get
        Set(ByVal value As String)
            mstrFacilDesc = value
        End Set
    End Property
    Public Property SubTitle() As String
        Get
            Return mstrFacilSubTitle
        End Get
        Set(ByVal value As String)
            mstrFacilSubTitle = value
        End Set
    End Property
    Public Property Addr() As String
        Get
            Return mstrAddr
        End Get
        Set(ByVal value As String)
            mstrAddr = value
        End Set
    End Property
    Public Property City() As String
        Get
            Return mstrCity
        End Get
        Set(ByVal value As String)
            mstrCity = value
        End Set
    End Property
    Public Property ST() As String
        Get
            Return mstrState
        End Get
        Set(ByVal value As String)
            mstrState = value
        End Set
    End Property
    Public Property State() As String
        Get
            Return mstrState
        End Get
        Set(ByVal value As String)
            mstrState = value
        End Set
    End Property
    Public Property Zip() As String
        Get
            Return mstrZip
        End Get
        Set(ByVal value As String)
            mstrZip = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return mstrEmail
        End Get
        Set(ByVal value As String)
            mstrEmail = value
        End Set
    End Property

    Public Property Phone() As String
        Get
            Return mstrPhone
        End Get
        Set(ByVal value As String)
            mstrPhone = value
        End Set
    End Property
    Public Property LogoImage() As String
        Get
            Return mstrLogoImg
        End Get
        Set(ByVal value As String)
            mstrLogoImg = value
        End Set
    End Property
    Public Sub New(ByVal intFacilID As Integer)
        mintFacilID = intFacilID
        Dim dt As DataTable = LoadFacil(intFacilID)

        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            mstrFacilName = "" & dr("FacilName")
            mstrFacilDesc = "" & dr("FacilDesc")
            mstrFacilSubTitle = "" & dr("FacilSubtitle")
            mstrPhone = "" & dr("Phone")
            mstrEmail = "" & dr("Email")
            mstrAddr = "" & dr("Addr")
            mstrCity = "" & dr("City")
            mstrState = "" & dr("State")
            mstrZip = "" & dr("Zip")
            mstrLogoImg = "" & dr("LogoImg")
        End If

    End Sub
    Public Function LoadFacil(ByVal intFacilID As Integer) As DataTable
        '====================================================================================

        '====================================================================================
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_UCSCH_GetFacil"
                .Parameters.Add(New SqlParameter("@facilID", intFacilID))
            End With
            da.SelectCommand = cmd
            da.Fill(dt)
            dt.TableName = "Facil"
            Return dt
        Catch ex As Exception
            Throw New Exception("Error in base.LoadFacil.  " & ex.Message)
        Finally

            cmd.Dispose()
            Call BASE_SetConSQL(False)
        End Try

    End Function

    Public Function TestDBCon() As String
        Dim retVal As String
        Try
            Call BASE_SetConSQL(True)
            Call BASE_SetConSQL(False)
            retVal = "OK"
        Catch ex As Exception
            retVal = ex.Message
        End Try

        Return retVal
    End Function
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

    Protected Overridable Overloads Sub Dispose(disposing As Boolean)
        On Error Resume Next
        If disposing Then
            ' dispose managed resources
            CN.Close()

        End If

        ' free native resources 

    End Sub 'Dispose

    Public Overloads Sub Dispose() Implements IDisposable.Dispose

        Dispose(True)
        GC.SuppressFinalize(Me)

    End Sub 'Dispose
End Class
