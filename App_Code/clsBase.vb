Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports Microsoft.VisualBasic.Strings
Imports System.IO
Imports System.Security.Cryptography

Public Class clsBase


    Implements IDisposable
    Protected CON_STR As String = ConfigurationManager.ConnectionStrings("DEV_Saratogaprod_Connection").ConnectionString
    'Protected CON_STR As String = ConfigurationManager.ConnectionStrings("PROD_Saratogadev_Connection").ConnectionString


    '
    Protected CN As SqlConnection
    Public mstrUserName As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split("\").Last()
    Private mstrIP As String = ""
    Public ARY_CON_STR As String() = CON_STR.Split(";")
    'Data Source=tcp:sql2k1901.discountasp.net;Initial Catalog=SQL2019_1027839_ptpthdev;User ID=SQL2019_1027839_ptpthdev_user;Password=WrePriPre66!;

    'Data Source=tcp:sql2k1901.discountasp.net;
    'Initial Catalog=SQL2019_1027839_ptpthdev;
    'User ID=SQL2019_1027839_ptpthdev_user;
    'Password=WrePriPre66!;

    Public Sub New()
        'mstrUserName = "" & HttpContext.Current.User.Identity.Name.ToString
        ' mstrUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split("\").Last()
        If mstrUserName = "" Then
            mstrUserName = "Dan Morrissey"
        End If
        mstrIP = GetIPAddress()

    End Sub

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

    Public Function RemoteRegSave(ByVal intID As Integer, ByVal strFirstName As String, ByVal strLastName As String, ByVal strDOB As String, ByVal strCellphone As String, ByVal strWhyHere As String, ByVal strOrderInHand As String, ByVal strStatus As String, ByVal strColorCode As String,
                               ByVal strQ1 As String, ByVal strQ2 As String, ByVal strQ3 As String, ByVal strQ4 As String, ByVal strQ5 As String,
                               ByVal strCough As String, ByVal strMP As String, ByVal strFever As String, ByVal strSOB As String, ByVal strLOT As String,
                               ByVal strMG As String, ByVal strST As String, ByVal strHA As String, ByVal strExp As String,
                               ByVal strPUI As String, ByVal strSusp As String, ByVal strQuar As String) As String


        '
        Dim strCodeSeg As String = ""
        Dim cmd As New SqlCommand
        Dim now As DateTime = DateTime.Now
        Dim datDate As Date = now
        Dim datDateTime As DateTime = now
        Try

            strCodeSeg = "BASE_SetConSQL"
            Call BASE_SetConSQL(True)
            With cmd
                strCodeSeg = "Assign Connection"
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_RemoteReg_Save"
                strCodeSeg = "Assign Params"
                .Parameters.Add(New SqlParameter("@ID", intID))
                .Parameters.Add(New SqlParameter("@FirstName", strFirstName))
                .Parameters.Add(New SqlParameter("@LastName", strLastName))
                .Parameters.Add(New SqlParameter("@DOB", strDOB))
                .Parameters.Add(New SqlParameter("@Cellphone", strCellphone))
                .Parameters.Add(New SqlParameter("@WhyHere", strWhyHere))
                .Parameters.Add(New SqlParameter("@OrderInHand", strOrderInHand))
                .Parameters.Add(New SqlParameter("@Status", strStatus))
                .Parameters.Add(New SqlParameter("@ColorCode", strColorCode))
                .Parameters.Add(New SqlParameter("@Question1", strQ1))
                .Parameters.Add(New SqlParameter("@Question2", strQ2))
                '.Parameters.Add(New SqlParameter("@Question3", strQ3))
                '.Parameters.Add(New SqlParameter("@Question4", strQ4))
                '.Parameters.Add(New SqlParameter("@Question5", strQ5))
                '.Parameters.Add(New SqlParameter("@Cough", strCough))
                '.Parameters.Add(New SqlParameter("@MusclePain", strMP))
                '.Parameters.Add(New SqlParameter("@Fever", strFever))
                '.Parameters.Add(New SqlParameter("@ShortnessOfBreath", strSOB))
                '.Parameters.Add(New SqlParameter("@LossOfTasteSmell", strLOT))
                '.Parameters.Add(New SqlParameter("@MassGathering", strMG))
                '.Parameters.Add(New SqlParameter("@SoreThroat", strST))
                '.Parameters.Add(New SqlParameter("@Headache", strHA))
                '.Parameters.Add(New SqlParameter("@ExposureToCovid", strExp))
                '.Parameters.Add(New SqlParameter("@ExposureToPUI", strPUI))
                '.Parameters.Add(New SqlParameter("@SuspectedCovid", strSusp))
                '.Parameters.Add(New SqlParameter("@ToldToQuarantine", strQuar))
                strCodeSeg = ".ExecuteNonQuery()"
                .ExecuteNonQuery()
            End With
            'intLogID = cmd.ExecuteScalar()
            'Return intLogID
        Catch ex As Exception
            Throw New Exception("Error in base.RemoteRegSave.  CodeSeg = " & strCodeSeg & " Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try
        Return ""

    End Function

#Enable Warning BC42105 ' Function 'NahamParseIDs' doesn't return a value on all code paths. A null reference exception could occur at run time when the result is used.
    Private Sub NahamSaveIDsBulk(ByVal strIDs As String)

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
                .CommandText = "usp_MDCN_naham_ID_InsertBulk"
                .Parameters.Add(New SqlParameter("@NahamIDS", strIDs))
                strRetVal = .ExecuteNonQuery()
            End With
        Catch ex As Exception
            Throw New Exception("Error in base.NahamSaveID  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try

    End Sub
    Private Function NahamSaveID(ByVal strID As String) As String
        '===========================================================================================================================================================

        '-------------------------------------------------------------------'
        '--------------------------------------------------------------------

        '===========================================================================================================================================================
        Dim strRetVal As String = ""

        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_MDCN_naham_ID_Insert"
                .Parameters.Add(New SqlParameter("@NahamID", strID))
                strRetVal = .ExecuteNonQuery()
            End With
        Catch ex As Exception
            Throw New Exception("Error in base.NahamSaveID  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try
#Disable Warning BC42105 ' Function 'NahamSaveID' doesn't return a value on all code paths. A null reference exception could occur at run time when the result is used.
    End Function
#Enable Warning BC42105 ' Function 'NahamSaveID' doesn't return a value on all code paths. A null reference exception could occur at run time when the result is used.

    'Public Function RemoteCheckinSave(ByVal bFName As Byte(), ByVal bLName As Byte(), ByVal bDOB As Byte(), ByVal bCell As Byte(), ByVal strCarrierEmail As String, ByVal strQA As String, ByVal strQB As String,
    '                           ByVal strQ1 As String, ByVal strQ2 As String, ByVal strQ3 As String, ByVal strQ4 As String, ByVal strQ5 As String,
    '                           ByVal strQ6 As String, ByVal strQ7 As String, ByVal strQ8 As String, ByVal strQ9 As String, ByVal strQ10 As String,
    '                           ByVal strQ11 As String, ByVal strQ12 As String) As String
    '    '===========================================================================================================================================================

    '    '-------------------------------------------------------------------'
    '    '--------------------------------------------------------------------

    '    '===========================================================================================================================================================
    '    Dim cmd As New SqlCommand
    '    Dim strIP As String = GetIPAddress()
    '    Dim paramFN As SqlParameter
    '    Dim paramLN As SqlParameter
    '    Dim paramDOB As SqlParameter
    '    Dim paramCell As SqlParameter

    '    Dim i As Integer
    '    Dim StrParam As String
    '    Dim aryParam As String()
    '    Dim aryDelim As String()
    '    Try


    '        Call BASE_SetConSQL(True)
    '        With cmd

    '            .Connection = CN
    '            .CommandType = CommandType.StoredProcedure
    '            .CommandText = "usp_MDCN_RemoteCheckinSave"

    '            paramFN = .Parameters.Add("@FName", SqlDbType.VarBinary)
    '            paramFN.Value = bFName

    '            paramLN = .Parameters.Add("@LName", SqlDbType.VarBinary)
    '            paramLN.Value = bLName

    '            paramDOB = .Parameters.Add("@DOB", SqlDbType.VarBinary)
    '            paramDOB.Value = bDOB

    '            paramCell = .Parameters.Add("@CellpHONE", SqlDbType.VarBinary)
    '            paramCell.Value = bCell

    '            .Parameters.Add(New SqlParameter("@IPAddr", strIP))
    '            .Parameters.Add(New SqlParameter("@Q1", strQA))
    '            .Parameters.Add(New SqlParameter("@Q2", strQB))
    '            .Parameters.Add(New SqlParameter("@Q3", strQ1))
    '            .Parameters.Add(New SqlParameter("@Q4", strQ2))
    '            .Parameters.Add(New SqlParameter("@Q5", strQ3))
    '            .Parameters.Add(New SqlParameter("@Q6", strQ4))
    '            .Parameters.Add(New SqlParameter("@Q7", strQ5))
    '            .Parameters.Add(New SqlParameter("@Q8", strQ6))
    '            .Parameters.Add(New SqlParameter("@Q9", strQ7))
    '            .Parameters.Add(New SqlParameter("@Q10", strQ8))
    '            .Parameters.Add(New SqlParameter("@Q11", strQ9))
    '            .Parameters.Add(New SqlParameter("@Q12", strQ10))
    '            .Parameters.Add(New SqlParameter("@CarrierText", strCarrierEmail))




    '            '=========================================================================================
    '        'This function that is called from the browser as an ajax method which passes the patient 
    '        ' record to another method that saves it to the database and returns a string ("Checkin Successful" ) 
    '        ' which Is passed fro this method back to the calling client javascript so tht it knows how to respond 
    '        ' to the user regarding the success of the submission.  (see SubmitCheckin() javascript function in RC.aspx 
    '        '=========================================================================================

    '        'intLogID = cmd.ExecuteScalar()
    '        'Return intLogID
    '    Catch ex As Exception
    '        Throw New Exception("Error in base.RemoteCheckinSaveg  Error = " & ex.Message)
    '    Finally
    '        cmd.Dispose()
    '        Call BASE_SetConSQL(False)

    '    End Try
    '    Return "Checkin Successful"
    'End Function

    Public Function RemoteCheckinSaveEnc(ByVal bFName As Byte(), ByVal bLName As Byte(), ByVal bDOB As Byte(), ByVal bCell As Byte(), ByVal strCarrierEmail As String, ByVal strQA As String, ByVal strQB As String,
                               ByVal strQ1 As String, ByVal strQ2 As String, ByVal strQ3 As String, ByVal strQ4 As String, ByVal strQ5 As String,
                               ByVal strQ6 As String, ByVal strQ7 As String, ByVal strQ8 As String, ByVal strQ9 As String, ByVal strQ10 As String,
                               ByVal strQ11 As String, ByVal strQ12 As String) As String
        '===========================================================================================================================================================

        '-------------------------------------------------------------------'
        '--------------------------------------------------------------------

        '===========================================================================================================================================================
        Dim cmd As New SqlCommand
        Dim strIP As String = GetIPAddress()
        Dim paramFN As SqlParameter
        Dim paramLN As SqlParameter
        Dim paramDOB As SqlParameter
        Dim paramCell As SqlParameter
        Try


            Call BASE_SetConSQL(True)
            With cmd

                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_MDCN_RemoteCheckinSave"

                paramFN = .Parameters.Add("@FName", SqlDbType.VarBinary)
                paramFN.Value = bFName

                paramLN = .Parameters.Add("@LName", SqlDbType.VarBinary)
                paramLN.Value = bLName

                paramDOB = .Parameters.Add("@DOB", SqlDbType.VarBinary)
                paramDOB.Value = bDOB

                paramCell = .Parameters.Add("@CellpHONE", SqlDbType.VarBinary)
                paramCell.Value = bCell

                .Parameters.Add(New SqlParameter("@IPAddr", strIP))
                .Parameters.Add(New SqlParameter("@Q1", strQA))
                .Parameters.Add(New SqlParameter("@Q2", strQB))
                .Parameters.Add(New SqlParameter("@Q3", strQ1))
                .Parameters.Add(New SqlParameter("@Q4", strQ2))
                .Parameters.Add(New SqlParameter("@Q5", strQ3))
                .Parameters.Add(New SqlParameter("@Q6", strQ4))
                .Parameters.Add(New SqlParameter("@Q7", strQ5))
                .Parameters.Add(New SqlParameter("@Q8", strQ6))
                .Parameters.Add(New SqlParameter("@Q9", strQ7))
                .Parameters.Add(New SqlParameter("@Q10", strQ8))
                .Parameters.Add(New SqlParameter("@Q11", strQ9))
                .Parameters.Add(New SqlParameter("@Q12", strQ10))
                .Parameters.Add(New SqlParameter("@CarrierText", strCarrierEmail))
                .ExecuteNonQuery()
            End With
            'intLogID = cmd.ExecuteScalar()
            'Return intLogID
        Catch ex As Exception
            Throw New Exception("Error in base.RemoteCheckinSaveg  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try
        Return "Checkin Successful"
    End Function

    Public Function RemoteCheckinSaveEncUC(ByVal bFName As Byte(), ByVal bLName As Byte(), ByVal bDOB As Byte(), ByVal bCell As Byte(), ByVal strCellCarrier As String, ByVal bStreet As Byte(), ByVal bApt As Byte(), ByVal bCity As Byte(), ByVal bState As Byte(), ByVal bZip As Byte(), ByVal bPName As Byte(), ByVal bPDOB As Byte(), ByVal bPCell As Byte(), ByVal strInsCarrier As String, ByVal strInsPolicy As String, ByVal strInsGroup As String, ByVal strQConsent As String, ByVal strQFin As String,
                               ByVal strQ1 As String, ByVal strQ2 As String, ByVal strQ3 As String, ByVal strQ4 As String, ByVal strQ5 As String,
                               ByVal strQ6 As String) As String

        Dim cmd As New SqlCommand
        Dim strIP As String = GetIPAddress()
        Dim paramFN As SqlParameter
        Dim paramLN As SqlParameter
        Dim paramDOB As SqlParameter
        Dim paramCell As SqlParameter



        Try


            Call BASE_SetConSQL(True)
            With cmd

                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_MDCN_RemoteCheckinUCSaveUC"

                paramFN = .Parameters.Add("@FName", SqlDbType.VarBinary)
                paramFN.Value = bFName

                paramLN = .Parameters.Add("@LName", SqlDbType.VarBinary)
                paramLN.Value = bLName

                paramDOB = .Parameters.Add("@DOB", SqlDbType.VarBinary)
                paramDOB.Value = bDOB

                paramDOB = .Parameters.Add("@Cell", SqlDbType.VarBinary)
                paramDOB.Value = bCell

                .Parameters.Add(New SqlParameter("@CellCarrier", strCellCarrier))

                paramCell = .Parameters.Add("@Street", SqlDbType.VarBinary)
                paramCell.Value = bStreet

                paramCell = .Parameters.Add("Apt", SqlDbType.VarBinary)
                paramCell.Value = bApt

                paramCell = .Parameters.Add("Street", SqlDbType.VarBinary)
                paramCell.Value = bStreet

                paramCell = .Parameters.Add("City", SqlDbType.VarBinary)
                paramCell.Value = bCity

                paramCell = .Parameters.Add("State", SqlDbType.VarBinary)
                paramCell.Value = bState

                paramCell = .Parameters.Add("Zip", SqlDbType.VarBinary)
                paramCell.Value = bZip

                paramCell = .Parameters.Add("PName", SqlDbType.VarBinary)
                paramCell.Value = bPName

                paramCell = .Parameters.Add("PDOB", SqlDbType.VarBinary)
                paramCell.Value = bPDOB

                paramCell = .Parameters.Add("PCell", SqlDbType.VarBinary)
                paramCell.Value = bPCell


                .Parameters.Add(New SqlParameter("@IPAddr", strIP))
                .Parameters.Add(New SqlParameter("@InsCarrier", strInsCarrier))
                .Parameters.Add(New SqlParameter("@InsGroupr", strInsGroup))

                .Parameters.Add(New SqlParameter("@QCons", strQConsent))
                .Parameters.Add(New SqlParameter("@QFin", strQFin))
                .Parameters.Add(New SqlParameter("@Q1", strQ1))
                .Parameters.Add(New SqlParameter("@Q2", strQ2))
                .Parameters.Add(New SqlParameter("@Q3", strQ3))
                .Parameters.Add(New SqlParameter("@Q4", strQ4))
                .Parameters.Add(New SqlParameter("@Q5", strQ5))
                .Parameters.Add(New SqlParameter("@Q6", strQ6))


                .Parameters.Add(New SqlParameter("@CarrierText", strCellCarrier))
                .ExecuteNonQuery()
            End With
            'intLogID = cmd.ExecuteScalar()
            'Return intLogID
        Catch ex As Exception
            Throw New Exception("Error in base.RemoteCheckinSaveUC  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try
        Return "Checkin Successful"
    End Function

    Public Function RemoteCheckinSaveEncUCTEST(ByVal bFName As Byte())
        Dim cmd As New SqlCommand
        Dim strIP As String = GetIPAddress()
        Dim paramFN As SqlParameter
        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_MDCN_RemoteCheckinSaveUCTEST"
                paramFN = .Parameters.Add("@FName", SqlDbType.VarBinary)
                paramFN.Value = bFName
                .Parameters.Add(New SqlParameter("@IPAddr", strIP))
                .ExecuteNonQuery()
            End With
            'intLogID = cmd.ExecuteScalar()
            'Return intLogID
        Catch ex As Exception
            Throw New Exception("Error in base.RemoteCheckinSaveUCTEST  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try
        Return "Checkin Successful"
    End Function

    Public Function RemoteCheckinSave2(ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String, ByVal strQA As String, ByVal strQB As String,
                               ByVal strQ1 As String, ByVal strQ2 As String, ByVal strQ3 As String, ByVal strQ4 As String, ByVal strQ5 As String,
                               ByVal strQ6 As String, ByVal strQ7 As String, ByVal strQ8 As String, ByVal strQ9 As String, ByVal strQ10 As String,
                               ByVal strQ11 As String, ByVal strQ12 As String) As String
        '===========================================================================================================================================================

        '-------------------------------------------------------------------'
        '--------------------------------------------------------------------

        '===========================================================================================================================================================
        Dim cmd As New SqlCommand
        Dim strIP As String = GetIPAddress()
        Try


            Call BASE_SetConSQL(True)
            With cmd

                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_RemoteCheckinSave"
                'usp_MDCN_RemoteCheckinSave

                .Parameters.Add(New SqlParameter("@FName", strFName))
                .Parameters.Add(New SqlParameter("@LName", strLName))
                .Parameters.Add(New SqlParameter("@DOB", strDOB))
                .Parameters.Add(New SqlParameter("@CellPhone", strCell))
                .Parameters.Add(New SqlParameter("@IPAddr", strIP))
                .Parameters.Add(New SqlParameter("@Q1", strQA))
                .Parameters.Add(New SqlParameter("@Q2", strQB))
                .Parameters.Add(New SqlParameter("@Q3", strQ1))
                .Parameters.Add(New SqlParameter("@Q4", strQ2))
                .Parameters.Add(New SqlParameter("@Q5", strQ3))
                .Parameters.Add(New SqlParameter("@Q6", strQ4))
                .Parameters.Add(New SqlParameter("@Q7", strQ5))
                .Parameters.Add(New SqlParameter("@Q8", strQ6))
                .Parameters.Add(New SqlParameter("@Q9", strQ7))
                .Parameters.Add(New SqlParameter("@Q10", strQ8))
                .Parameters.Add(New SqlParameter("@Q11", strQ9))
                .Parameters.Add(New SqlParameter("@Q12", strQ10))
                .ExecuteNonQuery()
            End With
            'intLogID = cmd.ExecuteScalar()
            'Return intLogID
        Catch ex As Exception
            Throw New Exception("Error in base.RemoteCheckinSaveg  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try
        Return "Checking Successful"
    End Function

    Public Function RemoteCheckin_Save(ByVal intFacilID As String, ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String,
                                       ByVal strQ2 As String, ByVal strQ3 As String, ByVal strQ4 As String, ByVal strQ5 As String, ByVal strQ6 As String,
                                       ByVal strQ7 As String, ByVal strQ8 As String) As String
        'Dim aryDelim As String()
        'Dim aryParam As String()
        Dim strRetVal As String = ""
        Dim i As Integer = 0
        'Dim i2 As Integer = 0
        'aryDelim = Split(strDelim, "|")
        'Dim intParam As Integer = UBound(aryDelim)
        'Dim strParam As String = ""
        'Dim strLabel As String = ""
        'Dim strVal As String = ""
        Dim strIP As String = GetIPAddress()
        Dim strUser As String = mstrUserName
        Dim cmd As New SqlCommand
        Dim sGUID As String

        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_MDCN_RC_Incoming_Save"
                .Parameters.Add(New SqlParameter("@facilID", intFacilID))
                .Parameters.Add(New SqlParameter("@FName", strFName))
                .Parameters.Add(New SqlParameter("@LName", strLName))
                .Parameters.Add(New SqlParameter("@DOB", strDOB))
                .Parameters.Add(New SqlParameter("@CellPhone", strCell))
                '.Parameters.Add(New SqlParameter("@CellCarrier", strCellCarrier))
                '.Parameters.Add(New SqlParameter("@Q1", strQ1))
                .Parameters.Add(New SqlParameter("@Q2", strQ2))
                .Parameters.Add(New SqlParameter("@Q3", strQ3))
                .Parameters.Add(New SqlParameter("@Q4", strQ4))
                .Parameters.Add(New SqlParameter("@Q5", strQ5))
                .Parameters.Add(New SqlParameter("@Q6", strQ6))
                .Parameters.Add(New SqlParameter("@Q7", strQ7))
                .Parameters.Add(New SqlParameter("@Q8", strQ8))
                .Parameters.Add(New SqlParameter("@IPAddr", strIP))
                '.Parameters.Add(New SqlParameter("@Gender", strGender))
                .ExecuteNonQuery()
            End With
        Catch ex As Exception
            strRetVal = "RemoteCheckin_Save: " & ex.Message
            Return strRetVal
            ' Throw New Exception("Error in base.RemoteRegSave.  CodeSeg = " & strCodeSeg & " Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try
        Return ""

    End Function
    Public Function RemoteCheckinSave_UC_Session() As String
        '===========================================================================================================================================================
        Dim cmd As New SqlCommand
        Dim strIP As String = GetIPAddress()
        Dim intID As Integer
        Dim strRetVal As String = intID.ToString

        Try


            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_MDCN_RemoteCheckinSaveUC_Session"
                intID = .ExecuteScalar()
                strRetVal = intID.ToString
            End With
            'intLogID = cmd.ExecuteScalar()
            Return strRetVal
        Catch ex As Exception
            Throw New Exception("Error in base.RemoteCheckinSave_UC_Session  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try
        Return "Checking Successful"
    End Function

    'Public Function RemoteCheckinSave_UC(ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String, ByVal strStreet As String,
    '                                      ByVal strApt As String, ByVal strCity As String, ByVal strState As String, ByVal strZip As String, ByVal strInsCarrier As String,
    '                                     ByVal strInsPolicy As String, ByVal strInsGroup As String, ByVal strInsCarrier2 As String, ByVal strInsPolicy2 As String, ByVal strInsGroup2 As String,
    '                                     ByVal strPName As String, ByVal strPDOB As String, ByVal strPCell As String, ByVal strEcLastName As String, ByVal strEcFirstName As String,
    '                                     ByVal strEcCell As String, ByVal strEcEmail As String, ByVal strNokLastName As String, ByVal strNokFirstName As String, ByVal strNokCell As String,
    '                                     ByVal strQ1 As String,
    '                                     ByVal strQ2 As String,
    '                                     ByVal strQ3 As String, ByVal strQ5 As String, ByVal strQ6 As String,
    '                                     ByVal strQ7 As String, ByVal strQ8 As String) As Integer
    '    '===========================================================================================================================================================
    '    Dim cmd As New SqlCommand
    '    Dim strIP As String = GetIPAddress()
    '    Dim intRC_ID As Integer = 0
    '    Try


    '        Call BASE_SetConSQL(True)
    '        With cmd

    '            .Connection = CN
    '            .CommandType = CommandType.StoredProcedure
    '            .CommandText = "usp_MDCN_RemoteCheckinSaveUC"
    '            'usp_MDCN_RemoteCheckinSave

    '            .Parameters.Add(New SqlParameter("@FName", strFName))
    '            .Parameters.Add(New SqlParameter("@LName", strLName))
    '            .Parameters.Add(New SqlParameter("@DOB", strDOB))
    '            .Parameters.Add(New SqlParameter("@CellPhone", strCell))

    '            .Parameters.Add(New SqlParameter("@Street", strStreet))
    '            .Parameters.Add(New SqlParameter("@Apt", strApt))
    '            .Parameters.Add(New SqlParameter("@City", strCity))
    '            .Parameters.Add(New SqlParameter("@State", strState))
    '            .Parameters.Add(New SqlParameter("@Zip", strZip))

    '            .Parameters.Add(New SqlParameter("@InsCarrier", strInsCarrier))
    '            .Parameters.Add(New SqlParameter("@InsPolicy", strInsPolicy))
    '            .Parameters.Add(New SqlParameter("@InsGroup", strInsGroup))

    '            .Parameters.Add(New SqlParameter("@InsCarrier2", strInsCarrier2))
    '            .Parameters.Add(New SqlParameter("@InsPolicy2", strInsPolicy2))
    '            .Parameters.Add(New SqlParameter("@InsGroup2", strInsGroup2))

    '            .Parameters.Add(New SqlParameter("@PName", strPName))
    '            .Parameters.Add(New SqlParameter("@PDOB", strPDOB))
    '            .Parameters.Add(New SqlParameter("@PCell", strPCell))
    '            .Parameters.Add(New SqlParameter("@Q1", strQ1))
    '            .Parameters.Add(New SqlParameter("@Q2", strQ2))
    '            .Parameters.Add(New SqlParameter("@Q3", strQ3))
    '            .Parameters.Add(New SqlParameter("@Q5", strQ5))
    '            .Parameters.Add(New SqlParameter("@Q6", strQ6))
    '            .Parameters.Add(New SqlParameter("@Q7", strQ7))
    '            .Parameters.Add(New SqlParameter("@Q8", strQ8))
    '            .Parameters.Add(New SqlParameter("@IPAddr", strIP))

    '            .Parameters.Add(New SqlParameter("@ecLastName", strEcLastName))
    '            .Parameters.Add(New SqlParameter("@ecFirstName", strEcFirstName))
    '            .Parameters.Add(New SqlParameter("@ecCell", strEcCell))
    '            .Parameters.Add(New SqlParameter("@ecEmail", strEcEmail))
    '            .Parameters.Add(New SqlParameter("@nokLastName", strNokLastName))
    '            .Parameters.Add(New SqlParameter("@nokFirstName", strNokFirstName))
    '            .Parameters.Add(New SqlParameter("@nokCell", strNokCell))

    '            intRC_ID = .ExecuteScalar
    '        End With
    '        'intLogID = cmd.ExecuteScalar()
    '        'Return intLogID
    '    Catch ex As Exception
    '        Throw New Exception("Error in base.RemoteCheckinSave_UC  Error = " & ex.Message)
    '    Finally
    '        cmd.Dispose()
    '        Call BASE_SetConSQL(False)

    '    End Try
    '    Return intRC_ID
    'End Function


    'Public Function RemoteCheckinSave_UC(ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String, ByVal strStreet As String,
    '                                      ByVal strApt As String, ByVal strCity As String, ByVal strState As String, ByVal strZip As String, ByVal strInsCarrier As String,
    '                                     ByVal strInsPolicy As String, ByVal strInsGroup As String, ByVal strInsCarrier2 As String, ByVal strInsPolicy2 As String, ByVal strInsGroup2 As String,
    '                                     ByVal strPName As String, ByVal strPDOB As String, ByVal strPCell As String, ByVal strQ1 As String, ByVal strQ2 As String, ByVal strQ3 As String,
    '                                     ByVal strQ4 As String, ByVal strQ5 As String, ByVal strQ6 As String, ByVal strQ7 As String, ByVal strQ8 As String) As Integer
    '    '===========================================================================================================================================================
    '    Dim cmd As New SqlCommand
    '    Dim strIP As String = GetIPAddress()
    '    Dim intRC_ID As Integer = 0
    '    Try


    '        Call BASE_SetConSQL(True)
    '        With cmd

    '            .Connection = CN
    '            .CommandType = CommandType.StoredProcedure
    '            .CommandText = "usp_MDCN_RemoteCheckinSaveUC"
    '            'usp_MDCN_RemoteCheckinSave

    '            .Parameters.Add(New SqlParameter("@FName", strFName))
    '            .Parameters.Add(New SqlParameter("@LName", strLName))
    '            .Parameters.Add(New SqlParameter("@DOB", strDOB))
    '            .Parameters.Add(New SqlParameter("@CellPhone", strCell))
    '            '.Parameters.Add(New SqlParameter("@CellCarrier", strCellCarrier))

    '            .Parameters.Add(New SqlParameter("@Street", strStreet))
    '            .Parameters.Add(New SqlParameter("@Apt", strApt))
    '            .Parameters.Add(New SqlParameter("@City", strCity))
    '            .Parameters.Add(New SqlParameter("@State", strState))
    '            .Parameters.Add(New SqlParameter("@Zip", strZip))

    '            .Parameters.Add(New SqlParameter("@InsCarrier", strInsCarrier))
    '            .Parameters.Add(New SqlParameter("@InsPolicy", strInsPolicy))
    '            .Parameters.Add(New SqlParameter("@InsGroup", strInsGroup))

    '            .Parameters.Add(New SqlParameter("@InsCarrier2", strInsCarrier2))
    '            .Parameters.Add(New SqlParameter("@InsPolicy2", strInsPolicy2))
    '            .Parameters.Add(New SqlParameter("@InsGroup2", strInsGroup2))

    '            .Parameters.Add(New SqlParameter("@PName", strPName))
    '            .Parameters.Add(New SqlParameter("@PDOB", strPDOB))
    '            .Parameters.Add(New SqlParameter("@PCell", strPCell))
    '            .Parameters.Add(New SqlParameter("@Q1", strQ1))
    '            .Parameters.Add(New SqlParameter("@Q2", strQ2))
    '            .Parameters.Add(New SqlParameter("@Q3", strQ3))
    '            .Parameters.Add(New SqlParameter("@Q4", strQ4))
    '            .Parameters.Add(New SqlParameter("@Q5", strQ5))
    '            .Parameters.Add(New SqlParameter("@Q6", strQ6))
    '            .Parameters.Add(New SqlParameter("@Q7", strQ7))
    '            .Parameters.Add(New SqlParameter("@Q8", strQ8))
    '            .Parameters.Add(New SqlParameter("@IPAddr", strIP))
    '            '.Parameters.Add(New SqlParameter("@Gender", strGender))
    '            intRC_ID = .ExecuteScalar
    '        End With
    '        'intLogID = cmd.ExecuteScalar()
    '        'Return intLogID
    '    Catch ex As Exception
    '        Throw New Exception("Error in base.RemoteCheckinSave_UC  Error = " & ex.Message)
    '    Finally
    '        cmd.Dispose()
    '        Call BASE_SetConSQL(False)

    '    End Try
    '    Return intRC_ID
    'End Function

    'Public Function RemoteCheckinSave_UC(ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String, ByVal strStreet As String,
    '                                      ByVal strApt As String, ByVal strCity As String, ByVal strState As String, ByVal strZip As String, ByVal strInsCarrier As String,
    '                                     ByVal strInsPolicy As String, ByVal strInsGroup As String, ByVal strInsCarrier2 As String, ByVal strInsPolicy2 As String, ByVal strInsGroup2 As String,
    '                                     ByVal strPName As String, ByVal strPDOB As String, ByVal strPCell As String, ByVal strEcLastName As String, ByVal strEcFirstName As String,
    '                                     ByVal strEcCell As String, ByVal strEcEmail As String, ByVal strNokLastName As String, ByVal strNokFirstName As String, ByVal strNokCell As String,
    '                                     ByVal strQ1 As String, ByVal strQ2 As String, ByVal strQ3 As String, ByVal strQ5 As String, ByVal strQ6 As String,
    '                                     ByVal strQ7 As String, ByVal strQ8 As String) As Integer

    'Public Function RemoteCheckinSave_UC(ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String, ByVal strStreet As String,
    '                                      ByVal strApt As String, ByVal strCity As String, ByVal strState As String, ByVal strZip As String, ByVal strInsCarrier As String,
    '                                     ByVal strInsPolicy As String, ByVal strInsGroup As String, ByVal strInsCarrier2 As String, ByVal strInsPolicy2 As String, ByVal strInsGroup2 As String,
    '                                     ByVal strPName As String, ByVal strPDOB As String, ByVal strPCell As String, ByVal strEcLastName As String, ByVal strEcFirstName As String,
    '                                     ByVal strEcCell As String, ByVal strEcEmail As String, ByVal strNokLastName As String, ByVal strNokFirstName As String, ByVal strNokCell As String,
    '                                     ByVal strQ1 As String, ByVal strQ2 As String) As Integer
    Public Function RemoteCheckinSave_UC(ByVal intFacilID As Integer, ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String, ByVal strEmail As String, ByVal strStreet As String,
                                          ByVal strApt As String, ByVal strCity As String, ByVal strState As String, ByVal strZip As String, ByVal strInsCarrier As String,
                                         ByVal strInsPolicy As String, ByVal strInsGroup As String, ByVal strInsCarrier2 As String, ByVal strInsPolicy2 As String, ByVal strInsGroup2 As String,
                                         ByVal strPName As String, ByVal strPDOB As String, ByVal strPCell As String, ByVal strEcLastName As String, ByVal strEcFirstName As String,
                                         ByVal strEcCell As String, ByVal strNokLastName As String, ByVal strNokFirstName As String, ByVal strNokCell As String,
                                         ByVal strQ1 As String, ByVal strQ2 As String) As Integer
        '===========================================================================================================================================================
        Dim cmd As New SqlCommand
        Dim strIP As String = GetIPAddress()
        Dim intRC_ID As Integer = 0
        Try


            Call BASE_SetConSQL(True)
            With cmd

                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_RC_CheckinSave"
                'usp_MDCN_RemoteCheckinSave
                .Parameters.Add(New SqlParameter("@facilID", intFacilID))
                .Parameters.Add(New SqlParameter("@FName", strFName))
                .Parameters.Add(New SqlParameter("@LName", strLName))
                .Parameters.Add(New SqlParameter("@DOB", strDOB))
                .Parameters.Add(New SqlParameter("@CellPhone", strCell))
                .Parameters.Add(New SqlParameter("@Email", strEmail))
                .Parameters.Add(New SqlParameter("@Street", strStreet))
                .Parameters.Add(New SqlParameter("@Apt", strApt))
                .Parameters.Add(New SqlParameter("@City", strCity))
                .Parameters.Add(New SqlParameter("@State", strState))
                .Parameters.Add(New SqlParameter("@Zip", strZip))

                .Parameters.Add(New SqlParameter("@InsCarrier", strInsCarrier))
                .Parameters.Add(New SqlParameter("@InsPolicy", strInsPolicy))
                .Parameters.Add(New SqlParameter("@InsGroup", strInsGroup))

                .Parameters.Add(New SqlParameter("@InsCarrier2", strInsCarrier2))
                .Parameters.Add(New SqlParameter("@InsPolicy2", strInsPolicy2))
                .Parameters.Add(New SqlParameter("@InsGroup2", strInsGroup2))

                .Parameters.Add(New SqlParameter("@PName", strPName))
                .Parameters.Add(New SqlParameter("@PDOB", strPDOB))
                .Parameters.Add(New SqlParameter("@PCell", strPCell))


                .Parameters.Add(New SqlParameter("@ecLastName", strEcLastName))
                .Parameters.Add(New SqlParameter("@ecFirstName", strEcFirstName))
                .Parameters.Add(New SqlParameter("@ecCell", strEcCell))
                '.Parameters.Add(New SqlParameter("@ecEmail", strEcEmail))

                .Parameters.Add(New SqlParameter("@nokLastName", strNokLastName))
                .Parameters.Add(New SqlParameter("@nokFirstName", strNokFirstName))
                .Parameters.Add(New SqlParameter("@nokCell", strNokCell))

                .Parameters.Add(New SqlParameter("@Q1", strQ1))
                .Parameters.Add(New SqlParameter("@Q2", strQ2))
                .Parameters.Add(New SqlParameter("@IPAddr", strIP))

                intRC_ID = .ExecuteScalar
            End With
            'intLogID = cmd.ExecuteScalar()
            'Return intLogID
        Catch ex As Exception
            Throw New Exception("Error in base.RemoteCheckinSave_UC  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try
        Return intRC_ID
    End Function

    Public Function RC_Remove(ByVal ID_RC As Integer) As String
        '---------------------------------------------------------------------------------------------------------------
        '  Right now, this inserts the record into a temp table before deleting so the table can be repolulated for testing
        '  Edite the sproc for testing to comment out the insert statement 
        '---------------------------------------------------------------------------------------------------------------
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable
        Dim retVal As String

        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_MDCN_RemoteCheckinRemove"
                .Parameters.Add(New SqlParameter("@ID_RC", ID_RC))
                retVal = .ExecuteNonQuery
            End With

            Return retVal
        Catch ex As Exception
            Return "Error in base.RC_Remove.  " & ex.Message
        Finally

            cmd.Dispose()
            Call BASE_SetConSQL(False)
        End Try

    End Function

    Public Function RCUC_Remove(ByVal ID_RCUC As Integer) As String
        '---------------------------------------------------------------------------------------------------------------
        '  Right now, this inserts the record into a temp table before deleting so the table can be repolulated for testing
        '  Edite the sproc for testing to comment out the insert statement 
        '---------------------------------------------------------------------------------------------------------------
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable
        Dim retVal As String

        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_MDCN_RemoteCheckinRemoveUC"
                .Parameters.Add(New SqlParameter("@ID_RCUC", ID_RCUC))
                retVal = .ExecuteNonQuery
            End With

            Return retVal
        Catch ex As Exception
            Return "Error in base.RCUC_Remove.  " & ex.Message
        Finally

            cmd.Dispose()
            Call BASE_SetConSQL(False)
        End Try

    End Function


    Public Function RCGetNextTable() As DataTable
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
                .CommandText = "usp_MDCN_RCGetNext"
            End With
            da.SelectCommand = cmd
            da.Fill(dt)
            dt.TableName = "RC"
            Return dt
        Catch ex As Exception
            Throw New Exception("Error in base.RCGetNextTable.  " & ex.Message)
        Finally

            cmd.Dispose()
            Call BASE_SetConSQL(False)
        End Try

    End Function

    Public Function RCUCGetNextTable() As DataTable
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
                .CommandText = "usp_MDCN_RCUCGetNext"
            End With
            da.SelectCommand = cmd
            da.Fill(dt)
            dt.TableName = "RCUC"
            Return dt
        Catch ex As Exception
            Throw New Exception("Error in base.RCUCGetNextTable.  " & ex.Message)
        Finally

            cmd.Dispose()
            Call BASE_SetConSQL(False)
        End Try

    End Function

    '
    Public Function RCGetGenderTable() As DataTable

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
                .CommandText = "usp_MDCN_RC_GetRefGender"
            End With
            da.SelectCommand = cmd
            da.Fill(dt)
            dt.TableName = "RC"
            Return dt
        Catch ex As Exception
            Throw New Exception("Error in base.RCGetGenderTable.  " & ex.Message)
        Finally

            cmd.Dispose()
            Call BASE_SetConSQL(False)
        End Try

    End Function

    Public Function RCGetMobileCarrierTable() As DataTable

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
                .CommandText = "usp_MDCN_RCGetMobileCarriers"
            End With
            da.SelectCommand = cmd
            da.Fill(dt)
            dt.TableName = "RC"
            Return dt
        Catch ex As Exception
            Throw New Exception("Error in base.RCGetMobileCarrierTable.  " & ex.Message)
        Finally

            cmd.Dispose()
            Call BASE_SetConSQL(False)
        End Try

    End Function

    Public Function RCGetStatesTable() As DataTable

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
                .CommandText = "usp_GetStates"
            End With
            da.SelectCommand = cmd
            da.Fill(dt)
            dt.TableName = "ST"
            Return dt
        Catch ex As Exception
            Throw New Exception("Error in base.RCGetStatesTable.  " & ex.Message)
        Finally

            cmd.Dispose()
            Call BASE_SetConSQL(False)
        End Try

    End Function

    Public Function RCCGetNew() As String
        '===========================================================================================================================================================
        '  
        '  
        '    
        '-------------------------------------------------------------------'
        '--------------------------------------------------------------------
        '   
        '           
        '===========================================================================================================================================================
        Dim cmd As New SqlCommand
        Dim now As DateTime = DateTime.Now
        Dim datDate As Date = now
        Dim datDateTime As DateTime = now
        Dim intID As Integer = 0
        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_RCCGetNew"
                intID = cmd.ExecuteScalar()
            End With
        Catch ex As Exception
            Throw New Exception("ERROR:" & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)
        End Try
        Return ""
    End Function

    Public Function RCUCGetNew() As String
        '===========================================================================================================================================================
        '  
        '  
        '    
        '-------------------------------------------------------------------'
        '--------------------------------------------------------------------
        '   
        '           
        '===========================================================================================================================================================
        Dim cmd As New SqlCommand
        Dim now As DateTime = DateTime.Now
        Dim datDate As Date = now
        Dim datDateTime As DateTime = now
        Dim intID As Integer = 0
        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_RCUCGetNew"
                intID = cmd.ExecuteScalar()
            End With
        Catch ex As Exception
            Throw New Exception("ERROR in RCUCGetNew:" & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)
        End Try
        Return ""
    End Function

    Public Function RCGetNext() As DataTable
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
                .CommandText = "usp_MDCN_RCGetNext"
            End With
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw New Exception("Error in base.RCGetNext.  " & ex.Message)
        Finally

            cmd.Dispose()
            Call BASE_SetConSQL(False)
        End Try

    End Function

    Public Function RCUCGetNext() As DataTable
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
                .CommandText = "usp_MDCN_RCUCGetNext"
            End With
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw New Exception("Error in base.RCGUCetNext.  " & ex.Message)
        Finally

            cmd.Dispose()
            Call BASE_SetConSQL(False)
        End Try

    End Function

    Public Shared Function GetIPAddress() As String
        Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
        Dim sIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If String.IsNullOrEmpty(sIPAddress) Then
            Return context.Request.ServerVariables("REMOTE_ADDR")
        Else
            Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
            Return ipArray(0)
        End If
    End Function

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
