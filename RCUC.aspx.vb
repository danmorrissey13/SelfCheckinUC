Imports System.Web.Services.WebService
Imports System.Data
Imports System.Data.SqlClient
Public Class RCUC
    Inherits System.Web.UI.Page
    Private mstrSession As String
    Private mstrFacilID As String
    Private oFacil As clsFacil

    Public Sub New()
    End Sub
    'Test ajax method =========================================================
    <System.Web.Services.WebMethod()>
    Public Shared Function GetCurrentTime(ByVal name As String) As String
        Return "Hello " & name & Environment.NewLine & "The Current Time is: " &
            DateTime.Now.ToString()
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function TestWebMethod(ByVal strFN As String) As String
        Return "Hello " & strFN & Environment.NewLine & "The Current Time is: " &
            DateTime.Now.ToString()
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function SubmitRemoteCheckinUC(ByVal intFacilID As Integer, ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String, ByVal strEmail As String, ByVal strStreet As String,
                                          ByVal strApt As String, ByVal strCity As String, ByVal strState As String, ByVal strZip As String, ByVal strInsCarrier As String,
                                                 ByVal strInsPolicy As String, ByVal strInsGroup As String, ByVal strInsCarrier2 As String, ByVal strInsPolicy2 As String,
                                                 ByVal strInsGroup2 As String, ByVal strPName As String, ByVal strPDOB As String, ByVal strPCell As String, ByVal strEcLastName As String,
                                                 ByVal strEcFirstName As String, ByVal strEcCell As String, ByVal strEcEmail As String, ByVal strNoklastName As String, ByVal strNokFirstName As String,
                                                 ByVal strNokCell As String, ByVal strQ1 As String, ByVal strQ2 As String) As String

        Dim oRC As New clsRemoteReg
        Dim strRetVal As String

        strRetVal = oRC.RemoteCheckinSave_UC(intFacilID, strFName, strLName, strDOB, strCell, strEmail, strStreet, strApt, strCity, strState, strZip, strInsCarrier, strInsPolicy, strInsGroup, strInsCarrier2, strInsPolicy2,
                                             strInsGroup2, strPName, strPDOB, strPCell, strEcLastName, strEcFirstName, strEcCell, strNoklastName, strNokFirstName, strNokCell,
                                             strQ1, strQ2)

        Return strRetVal
    End Function

    '==============================
    '<System.Web.Services.WebMethod()>
    'Public Shared Function SubmitRemoteCheckinUC(ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String, ByVal strStreet As String,
    '                                      ByVal strApt As String, ByVal strCity As String, ByVal strState As String, ByVal strZip As String, ByVal strInsCarrier As String, ByVal strInsPolicy As String, ByVal strInsGroup As String,
    '                                      ByVal strInsCarrier2 As String, ByVal strInsPolicy2 As String, ByVal strInsGroup2 As String, ByVal strPName As String, ByVal strPDOB As String, ByVal strPCell As String, ByVal strQ1 As String, ByVal strQ2 As String,
    '                                      ByVal strQ3 As String, ByVal strQ4 As String, ByVal strQ5 As String, ByVal strQ6 As String, ByVal strQ7 As String, ByVal strQ8 As String) As String
    'Dim oRC As New clsRemoteReg
    '    Dim strRetVal As String

    '    'strRetVal = oRC.RemoteCheckUC_SaveEnc(strFN, strLName, strDOB, strCell, strCarrier, strStreet, strApt, strCity, strState, strZip, strIns, strPolicy, strGroup, strParent, strPCell, strPDOB, strDelim)
    '    strRetVal = oRC.RemoteCheckinSave_UC(strFName, strLName, strDOB, strCell, strStreet, strApt, strCity, strState, strZip, strInsCarrier, strInsPolicy, strInsGroup, strInsCarrier2, strInsPolicy2, strInsGroup2, strPName, strPDOB, strPCell, strQ1, strQ2, strQ3, strQ4, strQ5, strQ6, strQ7, strQ8)
    '
    '    Return strRetVal
    'End Function

    '<System.Web.Services.WebMethod()>
    'Public Shared Function sessionCreateRecordDB() As String
    '    Dim oRC As New clsRemoteReg
    '    Dim strVal As String = ""
    '    Dim strRetVal As String = "New Record Created"
    '    Dim strSessionID = "" & oRC.RC_InsCard_Start.ToString
    '    Return strSessionID
    'End Function
    Private Function SaveImages(ByVal intRC_ID As Integer) As Integer
        Dim oRC As New clsRemoteReg
        Dim imgFile As HttpPostedFile = file1F.PostedFile
        If (imgFile IsNot Nothing) AndAlso (imgFile.ContentLength > 0) Then
            Call oRC.saveImgToDB(imgFile, intRC_ID, "Front", "Prim", "OutPt")
        End If
        imgFile = file1B.PostedFile
        If (imgFile IsNot Nothing) AndAlso (imgFile.ContentLength > 0) Then
            Call oRC.saveImgToDB(imgFile, intRC_ID, "Back", "Prim", "OutPt")
        End If
        imgFile = file2F.PostedFile
        If (imgFile IsNot Nothing) AndAlso (imgFile.ContentLength > 0) Then
            Call oRC.saveImgToDB(imgFile, intRC_ID, "Front", "Sec", "OutPt")
        End If
        imgFile = file2B.PostedFile
        If (imgFile IsNot Nothing) AndAlso (imgFile.ContentLength > 0) Then
            Call oRC.saveImgToDB(imgFile, intRC_ID, "Back", "Sec", "OutPt")
        End If

    End Function

    Private Function SubmitRemoteCheckinUC2() As Integer
        Dim intRC_ID As Integer
        Dim intFacilID As Integer
        Dim oRC As New clsRemoteReg

        'Dim filename As String = Path.GetFileName(fil.PostedFile.FileName)
        'Dim contentType As String = FileUpload1.PostedFile.ContentType
        'Using fs As Stream = FileUpload1.PostedFile.InputStream


        'End Using
        intFacilID = "0" + Session("facilID")
        hidFacilID.Value = intFacilID

        Dim strFName As String = txtFName.Value
        Dim strLName As String = txtLName.Value
        Dim strDOB As String = ddlMonth.Value & "/" & ddlDay.Value & "/" & ddlYear.Value
        Dim strEmail As String = "" & txtEmail.Value
        Dim strCell As String = "" & txtCell.Value
        Dim strStreet As String = "" & txtStreet.Value
        Dim strApt As String = "" & txtApt.Value
        Dim strCity As String = "" & txtCity.Value
        Dim strState As String = "" & txtState.Value
        Dim strZip As String = "" & txtZip.Value
        Dim strInsCarrier As String = "" & txtInsCarrier.Value
        Dim strInsPolicy As String = "" & txtInsPolicy.Value
        Dim strInsGroup As String = "" & txtInsGroup.Value
        Dim strInsCarrier2 As String = "" & txtInsCarrier2.Value
        Dim strInsPolicy2 As String = "" & txtInsPolicy2.Value
        Dim strInsGroup2 As String = "" & txtInsGroup2.Value
        Dim strPName As String = "" & txtPName.Value
        Dim strPDOB As String = "" & ddlMonthP.Value & "/" & ddlDayP.Value & "/" & ddlYearP.Value
        Dim strPCell As String = "" & txtPCell.Value
        Dim strEcLastName As String = "" & txtEcLastName.Value
        Dim strEcFirstName As String = "" & txtEcFirstName.Value
        Dim strEcCell As String = "" & txtEcCell.Value
        'Dim strEcEmail As String = "" & txtEcEmail.Value
        Dim strNokLastName As String = "" & txtNokLastName.Value
        Dim strNokFirstName As String = "" & txtNokFirstName.Value
        Dim strNokCell As String = "" & txtNokCell.Value
        Dim strQConsTreat As String = "" & Replace(lblConsTreat.InnerText, "<b>", "")
        strQConsTreat = Replace(strQConsTreat, "</b>", "")
        Dim strQConsFin As String = "" & Replace(lblConsFin.InnerText, "<b>", "")
        strQConsFin = Replace(strQConsFin, "</b>", "")
        'Dim strQ3 As String = "" & Replace(lblQ3.InnerText, "<b>", "")
        'strQ3 = Replace(strQ3, "</b>", "")
        ''Dim strQ4 As String = "" & Replace(lblQ4.InnerText, "<b>", "")
        ''strQ4 = Replace(strQ4, "</b>", "")
        'Dim strQ5 As String = "" & Replace(lblQ5.InnerText, "<b>", "")
        'strQ5 = Replace(strQ5, "</b>", "")
        'Dim strQ6 As String = "" & Replace(lblQ6.InnerText, "<b>", "")
        'strQ6 = Replace(strQ6, "</b>", "")
        'Dim strQ7 As String = "" & Replace(lblQ7.InnerText, "<b>", "")
        'strQ7 = Replace(strQ7, "</b>", "")
        'Dim strQ8 As String = "" & Replace(lblQ8.InnerText, "<b>", "")
        'strQ8 = Replace(strQ8, "</b>", "")



        Dim strAConsTreat As String = " No"
        Dim strAConsFin As String = " No"
        Dim strA3 As String = " No"

        Dim strA5 As String = " No"
        Dim strA6 As String = " No"
        Dim strA7 As String = " No"
        Dim strA8 As String = " No"
        'Dim strError As String

        If radConsTreatY.Checked Then strAConsTreat = " Yes" ' Else strAConsTreat = "No"
        If radConsFinY.Checked Then strAConsFin = " Yes" ' Else strAConsFin = "No"


        strQConsTreat += strAConsTreat
        strQConsFin += strAConsFin

        intRC_ID = oRC.RemoteCheckinSave_UC(intFacilID, strFName, strLName, strDOB, strCell, strEmail, strStreet, strApt, strCity, strState, strZip,
                                             strInsCarrier, strInsPolicy, strInsGroup, strInsCarrier2, strInsPolicy2, strInsGroup2,
                                             strPName, strPDOB, strPCell, strEcLastName, strEcFirstName, strEcCell, strNokLastName,
                                             strNokFirstName, strNokCell, strQConsTreat, strQConsFin)
        Return intRC_ID
    End Function

    Private Sub RCUC_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim intRC_ID As Integer
        Dim sGUID As String '= System.Guid.NewGuid.ToString()
        Dim oRC As New clsRemoteReg
        Dim strError As String = ""
        ' Try

        Dim aryConStr As String() = oRC.ARY_CON_STR
        '=====  RETURNS DELIMITED CONNECTION STRING STRING - THEN SPLIT INTO ARRAY  (eliminate the last array element - it's the password, unless for temp purposes)
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        'Data Source=tcp:sql2k1901.discountasp.net;Initial Catalog=SQL2019_1027839_ptpthdev;User ID=SQL2019_1027839_ptpthdev_user;Password=WrePriPre66!;
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        'Array:
        '0 - Data Source=tcp:sql2k1901.discountasp.net;  (server)
        '1 - Initial Catalog=SQL2019_1027839_ptpthdev;   (db)
        '2 - User ID=SQL2019_1027839_ptpthdev_user;      (login)
        '3 - Password=WrePriPre66!;                      (password)  - don't send to web page unless needed
        '=====================================================================================================================================================
        Dim retVal As String = oRC.TestDBCon
        retVal = retVal.Replace(vbCrLf, "....")
        If retVal <> "OK" Then
            retVal += ": " & aryConStr(0) & ", " & aryConStr(1) & ", " & aryConStr(2) & ", " & aryConStr(3)
            Response.Redirect("DBError.aspx?dbe=" & retVal)
        End If
        If Not IsPostBack Then
            sGUID = System.Guid.NewGuid.ToString()
            Session("SessionID") = sGUID
            mstrSession = Session("SessionID").ToString
            'first post
            Me.EnableViewState = True
            ddlYear.EnableViewState = True
            ddlYearP.EnableViewState = True

            mstrFacilID = "" & Request.QueryString("fid")
            Session("facilID") = mstrFacilID
            oFacil = New clsFacil(CInt(mstrFacilID))

            With oFacil
                imgFacilLogo.Src = "assets/img/" & .LogoImage
                lblFacilPhone.Text = "If you have any questions, please call the Urgent Care desk at " & .Phone
            End With
        End If
        'Catch ex As Exception

        ' End Try
        If strError <> "" Then
            Response.Redirect("GenErr.aspx?generr=" & strError)
        End If
    End Sub

    'Private Sub LoadDemographicsFromSession()

    '    'checks to see if user has arrived by hitting back button
    '    '  If so, load from session, then call function to NULL out time slot, since the calendar page will
    '    '  default to the earliest time of currernt day
    '    Dim aryDOB As String()
    '    Dim strDOB As String = Session("dob")
    '    Dim strConsTreat As String = Session("ConsTreat")
    '    Dim strConsFin As String = Session("ConsFin")
    '    mstrFacilID = Session("facilID")
    '    'radConsTreatY.Checked = (Session("ConsTreat") = "Yes")
    '    'radConsFinY.Checked = (Session("ConsFin") = "Yes")

    '    'radConsTreatN.Checked = (Session("ConsTreat") = "No")
    '    'radConsFinN.Checked = (Session("ConsFin") = "No")
    '    If strDOB.Length > 0 Then
    '        aryDOB = strDOB.Split("/")
    '        ddlMonth.Value = aryDOB(0)
    '        ddlDay.Value = aryDOB(1)
    '        ddlYear.Value = aryDOB(2)
    '    End If
    '    first_name.Text = Session("first_name")
    '    last_name.Text = Session("last_name")
    '    'txtDOB.Text = Session("dob")
    '    txtPhone.Text = Session("phone")
    '    Dim strDate As String = Session("SelDate")
    '    Dim strTime As String = Session("SelTime")
    '    oPR.Reset_TimeSlot(strDate, strTime, mstrFacilID)
    'End Sub


    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.ServerClick
        'cmdSubmit
        '  Load carriers from DB only on initial page load
        '===============================================================================================
        Dim intRC_ID As Integer

        Dim sGUID As String '= System.Guid.NewGuid.ToString()

        ' If Not IsPostBack Then display.Enabled = False

        If IsNothing(Session("SessionID")) Then
            'first post
            Me.EnableViewState = True
            ddlYear.EnableViewState = True
            ddlYearP.EnableViewState = True
            sGUID = System.Guid.NewGuid.ToString()
            Session("SessionID") = sGUID
            mstrSession = Session("SessionID").ToString
            ' hidSubmitFlag.Value = "" 'only has value when submit is clicked first
            'mstrSession = System.Web.HttpContext.Current.Session("SessionID")
        Else
            'this is a postback via form submit
            intRC_ID = SubmitRemoteCheckinUC2()
            Call SaveImages(intRC_ID)
            'hidSubmitFlag.Value = ""
            Session.Remove("SessionID")

            ' Response.Redirect("RemoteCheckIn.aspx")
            Response.Redirect("RCPostSave.aspx?saved=true")

        End If

    End Sub

End Class