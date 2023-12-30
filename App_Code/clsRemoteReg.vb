Imports System.Security.Cryptography
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class clsRemoteReg
    Inherits clsBase

    'Public oEncrypt As New clsAESencrypt
    Public Structure structHOO
        Public DOW As Integer
        Public DateDiff As Integer
        Public StartTimeMil_str As String
        Public StartTimeMil_int As Integer
        Public StartHour_str As String
        Public StartMin_str As String
        Public StartHour_int As Integer
        Public StartMin_int As Integer
        Public EndHour_int As Integer
        Public EndMin_int As Integer
        Public EndHour_str As String
        Public EndMin_str As String
    End Structure '

    '============================================================
    '  MAKE NON-ENCRYPTED VERSION
    '===========================================================
    Public Function RCGetNextCheckedIn() As String
        Dim dt As DataTable = MyBase.RCGetNext
        Dim dr As DataRow

        Dim bFName As Byte()
        Dim bLName As Byte()
        Dim bDOB As Byte()
        Dim bCell As Byte()




        Dim strQ As String = ""
        'Dim aryQ As String()
        'Dim bQ As Byte()
        'Dim bKey As Byte()
        'Dim bIV As Byte()


        Dim strFName As String
        Dim strLName As String
        Dim strDOB As String
        Dim strCell As String

        dr = dt.Rows(0)
        'For Each dr In dt.Rows
        bFName = CType(dr("FName"), Byte())
        'strFName = DecryptToString(bFName)

        bLName = CType(dr("LName"), Byte())
        'strLName = DecryptToString(bLName)

        bCell = CType(dr("CellPhone"), Byte())
        'strCell = DecryptToString(bCell)

        bDOB = CType(dr("DOB"), Byte())
        'strDOB = DecryptToString(bDOB)

        strQ = strFName & "|" & strLName & "|" & strDOB & "|" & strCell & "|" & dr("Q1") & "|" & dr("Q2") & "|" & dr("Q3") & "|" & dr("Q4") & "|" & dr("Q5") & "|" & dr("Q6") & "|" & dr("Q7") & "|" & dr("Q8")

        'Next
        Return strQ
    End Function
    Private Function getStruct_HoursOfOperation(ByVal datDate As Date) As structHOO
        Dim Hoo As New structHOO
        Dim thisDate As Date = Now.Date
        Dim intDateDiff As Long = DateDiff(DateInterval.Day, thisDate, datDate)
        Dim intDow As Integer = Weekday(datDate)
        Dim timeNow = System.TimeZoneInfo.ConvertTime(Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        Dim strTimeNow As String = Format(timeNow, "HH:mm")
        Dim strTimeNowMil As String = ChangeToMilitaryTime(strTimeNow)
        Dim intTimeNowMil As Integer = CInt(strTimeNowMil)
        Dim strL As String = Left(strTimeNowMil, 2)
        Dim strR As String = Right(strTimeNowMil, 2)
        Dim intHourCur As Integer = CInt(strL)
        Dim intMinCur As Integer = CInt(strR)
        Dim intHourStart As Integer
        Dim intHourStart2 As Integer
        Dim intHourEnd As Integer
        Dim intMinEnd As Integer
        'Dim startTime = Format(timeNow.AddHours(3), "HH:mm")
        '----------------------------------------------------------------------------------
        Select Case intMinCur
            Case 1 To 15
                intMinCur = 15
            Case 16 To 30
                intMinCur = 30
            Case 31 To 45
                intMinCur = 45
            Case > 45
                intMinCur = 45
        End Select
        '----------------------------------------------------------------------------------
        Select Case intDow
            Case 1
                intHourStart = 0 ' !!! Sundays 10-3
                intHourEnd = 15
                Select Case intDateDiff
                    Case 0
                        intHourStart = 10 ' For current day - start time begins at noon - three hours from the start of operations
                        If intHourStart < (intHourCur + 3) Then intHourStart = intHourCur + 3 '!! Start time begins three hours from current time
                    Case 1 ' When reserving from the previous day, start time begins at the start of operations (0900) unless it's currently after 1800 the previous day
                        intHourStart = 10
                        If intHourCur > 18 Then intHourStart = 10 '!! When reserving time for a Sun from after 1800 on the prev day, (NOT - changed:) start time begins three hours from start of operations
                    Case Else
                End Select
            Case 7 '!! Saturday 10-7
                intHourEnd = 19
                Select Case intDateDiff
                    Case 0
                        intHourStart = 10 ' For current day - start time begins at noon - three hours from the start of operations
                        If intHourStart < (intHourCur + 3) Then intHourStart = intHourCur + 3 '!! Start time begins three hours from current time
                    Case 1 ' When reserving from the previous day, start time begins at the start of operations (1000) unless it's currently after 1800 the previous day
                        intHourStart = 10
                        If intHourCur > 18 Then intHourStart = 10 '!! When reserving time for a Sat from after 1800 on the prev day,  (NOT - changed:) start time begins three hours from start of operations
                    Case Else
                End Select
            Case Else ' Weekdays - 8-9
                intHourEnd = 21
                intDow = 0 ' Make all weekdays 0 DOW
                Select Case intDateDiff
                    Case 0
                        intHourStart = 8 '!! For current day - start time begins at noon - three hours from the start of operations
                        If intHourStart < (intHourCur + 3) Then intHourStart2 = intHourCur + 3 '!! Start time begins three hours from current time
                        If intHourStart2 > intHourStart Then intHourStart = 0
                    Case 1
                        intHourStart = 8
                        If intHourCur >= 18 Then intHourStart = 8
                    Case Else
                        intHourStart = 8
                End Select
        End Select
        '----------------------------------------------------------------------------------
        With Hoo
            .DateDiff = intDateDiff
            .StartHour_int = intHourStart
            .StartMin_int = intMinCur
            .StartTimeMil_int = intHourStart & intMinCur
            .StartTimeMil_str = Right("00" & intHourStart, 2) & Right("00" & intMinCur, 2)
        End With
        Return Hoo
    End Function

    Private Function getStruct_HoursOfOperation(ByVal datDate As Date, ByVal intFacilID As Integer) As structHOO
        Dim Hoo As New structHOO

        Dim thisDate As Date = Now.Date
        Dim intDateDiff As Long = DateDiff(DateInterval.Day, thisDate, datDate)
        Dim intDow As Integer = Weekday(datDate)
        Dim timeNow = System.TimeZoneInfo.ConvertTime(Now, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        Dim strTimeNow As String = Format(timeNow, "HH:mm")
        Dim strTimeNowMil As String = ChangeToMilitaryTime(strTimeNow)
        Dim intTimeNowMil As Integer = CInt(strTimeNowMil)
        Dim strL As String = Left(strTimeNowMil, 2)
        Dim strR As String = Right(strTimeNowMil, 2)
        Dim intHourCur As Integer = CInt(strL)
        Dim intMinCur As Integer = CInt(strR)
        Dim dt As DataTable = GetHoursOfOperation(intFacilID, intDow)
        Dim dr As DataRow = dt.Rows(0)
        Dim strHourStart As String
        Dim strHourEnd As String
        Dim intHourStart As Integer
        Dim intHourEnd As Integer
        'Dim startTime = Format(timeNow.AddHours(3), "HH:mm")
        '----------------------------------------------------------------------------------
        Select Case intMinCur
            Case 1 To 15
                intMinCur = 15
            Case 16 To 30
                intMinCur = 30
            Case 31 To 45
                intMinCur = 45
            Case > 45
                intMinCur = 45
        End Select
        strHourStart = dr("Start")
        strHourEnd = dr("End")
        strHourStart = Left(strHourStart, 2)
        strHourEnd = Left(strHourEnd, 2)
        intHourStart = CInt(strHourStart)
        intHourEnd = CInt(strHourEnd)
        If intDateDiff = 0 Then
            'For the same day, must be at least three hours before arrival time
            If intHourStart < (intHourCur + 3) Then
                intHourStart = intHourCur + 3 '
                If intHourStart > intHourEnd Then
                    intHourStart = 0
                    intHourEnd = 0
                End If
            End If
        End If

        '----------------------------------------------------------------------------------
        With Hoo
            .DateDiff = intDateDiff
            .StartHour_int = intHourStart
            .StartMin_int = intMinCur
            .StartTimeMil_int = intHourStart & intMinCur
            .StartTimeMil_str = Right("00" & intHourStart, 2) & Right("00" & intMinCur, 2)
            .EndHour_int = intHourEnd
        End With
        Return Hoo
    End Function


    Public Function GetString_AvailHoursByDate(ByVal ptID As Integer, ByVal datDate As Date, ByVal intFacilID As Integer) As String
        Dim Hoo As New structHOO
        Hoo = getStruct_HoursOfOperation(datDate, intFacilID)

        Dim dt As DataTable = GetTable_AvailHoursByDay(datDate, intFacilID)
        Dim strDate As String = datDate.ToShortDateString
        Dim intPtID As String
        Dim strHours As String
        Dim datTime As DateTime
        Dim strHour As String
        Dim strHrMil As String
        Dim intHrMil As Integer


        Dim strTime As String
        Dim i As Integer

        Dim strL As String
        Dim strR As String

        Dim dr As DataRow = dt.Rows(0)
        Dim strVal As String
        Dim strTimeFirst As String

        Dim blnlPtID As Boolean '= ptID <> "0"
        Dim blnHours As Boolean
        Dim i2 As Integer = 2

        Dim blnStartMatch As Boolean
        'For i = 0 To dt.Columns.Count - 2
        For i = 2 To dt.Columns.Count - 2
            strHrMil = dt.Columns(i).ColumnName
            intHrMil = CInt(strHrMil)
            blnStartMatch = (Hoo.StartTimeMil_str = strHrMil)
            If blnStartMatch Then blnHours = True
            intPtID = "0" & dr(strHrMil)
            blnlPtID = (intPtID = ptID)
            strL = Left(strHrMil, 2)
            strR = Right(strHrMil, 2)

            strTime = strL & ":" & strR
            strDate = datDate.ToShortDateString & " " & strTime
            'strDate += " " & strHrMil
            datTime = CDate(strDate)
            strHour = datTime.ToShortTimeString.ToString()
            strVal = "" & dr(strHrMil)

            If blnHours Then
                If (strVal = "") Or (blnlPtID) Then
                    If i2 = 2 Then
                        strHours += "<option value=" & Chr(34) & strHrMil & Chr(34) & " selected=" & Chr(34) & "selected" & Chr(34) & ">" & strHour & "</option>"
                        strTimeFirst = strHour
                        i2 += 1
                    Else
                        strHours += "<option value=" & Chr(34) & strHrMil & Chr(34) & ">" & strHour & "</option>"
                    End If

                End If
            End If
        Next
        strHours += "|" & datDate.ToShortDateString & "|" & strTimeFirst
        Return strHours

    End Function
    Public Function CheckTimeSlot(ByVal intPtID As Integer, ByVal strDate As String, ByVal strCol As String, ByVal strFacilID As String) As Boolean
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim retVal As String
        Dim ptID As Integer
        Dim blnMatch As Boolean
        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_RS_GetTimeSlot"
                .Parameters.Add(New SqlParameter("@date", strDate))
                .Parameters.Add(New SqlParameter("@col", strCol))
                .Parameters.Add(New SqlParameter("@facilID", strFacilID))
            End With
            da.SelectCommand = cmd
            da.Fill(dt)
            dr = dt.Rows(0)
            ptID = CInt("0" & dr(strCol))
            blnMatch = ptID = intPtID
            Return blnMatch
        Catch ex As Exception
            Throw New Exception("Error in base.CheckTimeSlot  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try

    End Function
    Public Function CheckOpenTimeSlot(ByVal strDate As String, ByVal strCol As String, ByVal intFacilID As Integer) As Boolean
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim retVal As String
        Dim strPtID As String
        Dim blnMatch As Boolean
        Dim strTbl As String = ""
        Select Case intFacilID
            Case 1
                strTbl = "UCSCH_MMA_Sched"
            Case 2
                strTbl = "UCSCH_AH_Sched"
            Case 3
                strTbl = "UCSCH_WMA_Sched"
        End Select
        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_UCSCH_GetTimeSlot"
                .Parameters.Add(New SqlParameter("@date", strDate))
                .Parameters.Add(New SqlParameter("@col", strCol))
                .Parameters.Add(New SqlParameter("@tbl", strTbl))
            End With
            da.SelectCommand = cmd
            da.Fill(dt)
            dr = dt.Rows(0)
            strPtID = "" & dr(strCol)
            blnMatch = strPtID = ""
            Return blnMatch
        Catch ex As Exception
            Throw New Exception("Error in base.CheckTimeSlot  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try

    End Function
    Private Function GetHoursOfOperation(ByVal intFacil As Integer, ByVal intDOW As Integer) As DataTable
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_UCSCH_GetHoursOfOperation"
                .Parameters.Add(New SqlParameter("@facilID", intFacil))
                .Parameters.Add(New SqlParameter("@dow", intDOW))
            End With
            da.SelectCommand = cmd
            da.Fill(dt)
            dt.TableName = "Hours"
            Return dt
        Catch ex As Exception
            Throw New Exception("Error in base.GetHoursOfOperation  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try

    End Function

    Private Function GetTable_AvailHoursByDay(ByVal datDate As Date, ByVal intFacilID As Integer) As DataTable
        Dim strRetVal As String = ""
        Dim intDateDiff As Long = DateDiff(DateInterval.Day, Now, datDate)
        Dim intDow As Integer = Weekday(datDate)
        Dim strSproc As String

        '==========================================
        ' HOURS OF OPERATION MON-SAT ONLY Q15min
        '------------------------------------------
        '   M-F: Return 0700-2100
        '   SAT: return 0900-2100
        '------------------------------------------
        '   SAME DAY RESTRICTIONS: Can't schedule anything less than three hours in advance same day.
        '   PREV DAY RESTRICTIONS: After 1800, business hours next day start at 1000 (M-F) and noon on SAT.
        '------------------------------------------
        Select Case intDow
            Case 1, 7
            Case Else
                intDow = 0
        End Select

        Select Case intFacilID
            Case 1
                strSproc = "usp_UCSCH_MMA_Sched_GetAvailHours"
            Case 2
                strSproc = "usp_UCSCH_AH_Sched_GetAvailHours"
            Case 3
                strSproc = "usp_UCSCH_WMA_Sched_GetAvailHours"
        End Select
        '------------------------------------------
        '--  DATE CODE:
        '  Sunday-current day = 10
        '  Sunday-day before = 11
        '  Sat-current day = 60
        '  Sat-day before = 61
        '  Weekday-current day = 0
        '  Weekday-day before = 1
        '==========================================
        Dim strDateDiff As String = CStr(intDateDiff)
        Dim strDow As String = CStr(intDow)
        Dim strDateCode As String = strDateDiff & strDow
        Dim intDateCode As Integer = CInt(strDateCode)

        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = strSproc
                .Parameters.Add(New SqlParameter("@date", datDate))
                .Parameters.Add(New SqlParameter("@dow", intDow))
            End With
            da.SelectCommand = cmd
            da.Fill(dt)
            dt.TableName = "Hours"
            Return dt
        Catch ex As Exception
            Throw New Exception("Error in base.NahamSaveID  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try

    End Function
    Public Sub Cancel_TimeSlot(ByVal strDate As String, ByVal strSelTime As String)

        Dim cmd As New SqlCommand
        Dim dt As New DataTable

        Dim SQL As String
        SQL = "UPDATE RS_Sched SET [" & strSelTime & "] = NULL  WHERE [Date] = '" & strDate & "'"

        Try

            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_RS_CancelTimeSlot"
                .Parameters.Add(New SqlParameter("@SQL", SQL))
                .ExecuteNonQuery()
            End With

        Catch ex As Exception
            Throw New Exception("Error in base.Update_TimeSlot  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try

    End Sub

    Public Sub Update_TimeSlot(ByVal intPtID As Integer, ByVal strPrevDate As String, ByVal strDate As String, ByVal strPrevTime As String, ByVal strSelTime As String, ByVal intFacil As Integer)

        Dim cmd As New SqlCommand
        Dim dt As New DataTable

        Dim SQL1 As String
        Dim SQL2 As String
        Dim strTbl As String
        If strPrevTime = "0000" Then Exit Sub
        Select Case intFacil
            Case 1
                strTbl = "UCSCH_MMA_Sched"
            Case 2
                strTbl = "UCSCH_AH_Sched"
            Case 3
                strTbl = "UCSCH_WMA_Sched"
        End Select
        SQL1 = "UPDATE " & strTbl & " SET [" & strPrevTime & "] = NULL  WHERE [Date] = '" & strPrevDate & "'"

        SQL2 = "UPDATE " & strTbl & " SET [" & strSelTime & "] = " & intPtID & " WHERE [Date] = '" & strDate & "'"
        Try

            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_UCSCH_UpdateTimeSlot2"
                .Parameters.Add(New SqlParameter("@SQL1", SQL1))
                .Parameters.Add(New SqlParameter("@SQL2", SQL2))
                .ExecuteNonQuery()
            End With

        Catch ex As Exception
            Throw New Exception("Error in base.Update_TimeSlot  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try

    End Sub

    Public Sub Reset_TimeSlot(ByVal strDate As String, ByVal strSelTime As String, ByVal intFacilID As Integer)
        Dim cmd As New SqlCommand
        Dim dt As New DataTable
        Dim SQL As String
        strSelTime = ChangeToMilitaryTime(strSelTime)
        Dim strTbl As String
        Select Case intFacilID
            Case 1
                strTbl = "MMA"
            Case 2
                strTbl = "AH"
            Case 3
                strTbl = "WMA"
        End Select
        SQL = "UPDATE UCSCH_" & strTbl & "_Sched Set [" & strSelTime & "] = NULL  WHERE [Date] = '" & strDate & "'"
        Try

            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_UCSCH_ResetTimeSlot"
                .Parameters.Add(New SqlParameter("@SQL", SQL))
                .ExecuteNonQuery()
            End With

        Catch ex As Exception
            Throw New Exception("Error in base.Reset_TimeSlot  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try

    End Sub
    Public Function UpdatePatientAppt(ByVal intPtID As Integer, ByVal intFacilID As Integer, ByVal strDate As String, ByVal strTime As String) As Integer
        Dim cmd As New SqlCommand
        Dim retVal As Integer
        Try
            Call BASE_SetConSQL(True)
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_UCSCH_Patient_ConfirmAppt"
                .Parameters.Add(New SqlParameter("@ID", intPtID))
                .Parameters.Add(New SqlParameter("@facilID", intFacilID))
                .Parameters.Add(New SqlParameter("@SchedDate", strDate))
                .Parameters.Add(New SqlParameter("@SchedTime", strTime))
                retVal = .ExecuteNonQuery
            End With
        Catch ex As Exception
            Throw New Exception("Error in clsPreReg.UpdatePatientAppt  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)
        End Try
    End Function

    Public Function ChangeToMilitaryTime(ByVal stime As String) As String
        'change  0545 PM to 1745  or 0915 AM to 0915
        'Removes the AM/PM and changes hour to 24 hour format
        'These are how the columns are named in the table
        If stime = "" Then Return "0000"
        Dim strTime As String() = stime.Split(":")
        Dim strHour As String = strTime(0)
        Dim strMin As String = strTime(1).Substring(0, 2)
        Dim intHour As Integer = CInt(strHour)
        Dim strAMPM As String = Right(stime, 2)
        If IsNumeric(strAMPM) Then
            strHour = stime.Replace(":", "")
            strHour = Right("0" & strHour, 4)
        Else
            If strAMPM = "PM" Then
                If intHour <> 12 Then
                    intHour += 12
                    'Else
                    '    intHour = 0
                End If
            Else
                If intHour = 12 Then intHour = 0
            End If
            strHour = Right("000" & intHour, 2) & strMin
        End If

        Return strHour
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
    'Public Function RemoteCheckin_SaveIncoming(ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String,
    '                                           ByVal strCellCarrier As String, ByVal strQ2 As String, ByVal strQ3 As String,
    '                                           ByVal strQ4 As String, ByVal strQ5 As String, ByVal strQ6 As String, ByVal strQ7 As String, ByVal strQ8 As String) As String
    '    Dim strRetVal As String
    '    strRetVal = MyBase.RemoteCheckin_Save(strFName, strLName, strDOB, strCell, strQ2, strQ3, strQ4, strQ5, strQ6, strQ7, strQ8)
    'End Function


    Public Function RemoteCheckin_Save_UC(ByVal intFacilID As Integer, ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String, ByVal strEmail As String, ByVal strStreet As String,
                                          ByVal strApt As String, ByVal strCity As String, ByVal strState As String, ByVal strZip As String, ByVal strInsCarrier As String,
                                         ByVal strInsPolicy As String, ByVal strInsGroup As String, ByVal strInsCarrier2 As String, ByVal strInsPolicy2 As String, ByVal strInsGroup2 As String,
                                         ByVal strPName As String, ByVal strPDOB As String, ByVal strPCell As String, ByVal strEcLastName As String, ByVal strEcFirstName As String,
                                         ByVal strEcCell As String, ByVal strNokLastName As String, ByVal strNokFirstName As String, ByVal strNokCell As String, ByVal strQ1 As String,
                                         ByVal strQ2 As String) As Integer
        'CovidQSave
        Dim strRetVal As String = "ok"
        Dim strErr As String = ""
        Dim intRC_ID As Integer
        intRC_ID = MyBase.RemoteCheckinSave_UC(intFacilID, strFName, strLName, strDOB, strCell, strEmail, strStreet, strApt, strCity, strState, strZip, strInsCarrier,
                                         strInsPolicy, strInsGroup, strInsCarrier2, strInsPolicy2, strInsGroup2, strPName, strPDOB, strPCell,
                                         strEcLastName, strEcFirstName, strEcCell, strNokLastName, strNokFirstName, strNokCell, strQ1, strQ2)
        Return intRC_ID

    End Function

    Public Function RC_InsCard_Start() As String
        Dim strSessionID = MyBase.RemoteCheckinSave_UC_Session
    End Function
    Public Function saveImgToDB(ByVal postedFile As HttpPostedFile, ByVal RC_ID As Integer, ByVal strSide As String, strPrimSec As String, ByVal strInptOutpt As String) As Integer
        Dim id As Integer
        'Dim sGUID As String = System.Guid.NewGuid.ToString()

        Dim strImageName As String = postedFile.FileName
        Dim lngth As Integer = postedFile.ContentLength
        Dim pic As Byte() = New Byte(lngth - 1) {}
        postedFile.InputStream.Read(pic, 0, lngth)

        'usp_MDCN_naham_ID_InsertBulk
        Dim strRetVal As String = ""

        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim dt As New DataTable

        Try
            Call BASE_SetConSQL(True)


            '        Dim id As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            With cmd
                .Connection = CN
                .CommandType = CommandType.StoredProcedure
                .CommandText = "usp_RC_SaveCardImgToDB"
                .Parameters.Add(New SqlParameter("@ImgName", strImageName))
                .Parameters.Add(New SqlParameter("@imgBinary", pic))
                .Parameters.Add(New SqlParameter("@ImgSide", strSide))
                .Parameters.Add(New SqlParameter("@ID_RC", RC_ID))
                .Parameters.Add(New SqlParameter("@primSec", strPrimSec))
                .Parameters.Add(New SqlParameter("@inptOutpt", strInptOutpt))
                '.Parameters.Add(New SqlParameter("@sGUID", sGUID))
                'strRetVal = .ExecuteNonQuery()
                id = .ExecuteScalar()
            End With
        Catch ex As Exception
            Throw New Exception("Error in saveToDB  Error = " & ex.Message)
        Finally
            cmd.Dispose()
            Call BASE_SetConSQL(False)

        End Try
        Return id
    End Function



    '============================================================
    '  MAKE NON-ENCRYPTED VERSION
    '===========================================================
    'Public Function RCUCGetNextCheckedIn() As String
    '    Dim dt As DataTable = MyBase.RCGetNext
    '    Dim dr As DataRow

    '    Dim bFName As Byte()
    '    Dim bLName As Byte()
    '    Dim bDOB As Byte()
    '    Dim bCell As Byte()

    '    Dim bStreet As Byte()
    '    Dim bApt As Byte()
    '    Dim bCity As Byte()
    '    Dim bState As Byte()
    '    Dim bZip As Byte()
    '    Dim bPName As Byte()
    '    Dim bPCell As Byte()
    '    Dim bPDOB As Byte()




    '    Dim strQ As String = ""
    '    'Dim aryQ As String()
    '    'Dim bQ As Byte()
    '    'Dim bKey As Byte()
    '    'Dim bIV As Byte()


    '    Dim strFName As String
    '    Dim strLName As String
    '    Dim strDOB As String
    '    Dim strCell As String

    '    Dim strAddr As String
    '    Dim strApt As String
    '    Dim strCity As String
    '    Dim strState As String
    '    Dim strZip As String
    '    Dim strPName As String
    '    Dim strPCell As String
    '    Dim strPDOB As String

    '    dr = dt.Rows(0)
    '    'For Each dr In dt.Rows
    '    bFName = CType(dr("FName"), Byte())
    '    strFName = DecryptToString(bFName)

    '    bLName = CType(dr("LName"), Byte())
    '    strLName = DecryptToString(bLName)

    '    bCell = CType(dr("CellPhone"), Byte())
    '    strCell = DecryptToString(bCell)

    '    bDOB = CType(dr("Street"), Byte())
    '    strDOB = DecryptToString(bStreet)

    '    bDOB = CType(dr("Apt"), Byte())
    '    strDOB = DecryptToString(bApt)

    '    bDOB = CType(dr("City"), Byte())
    '    strDOB = DecryptToString(bCity)

    '    bDOB = CType(dr("State"), Byte())
    '    strDOB = DecryptToString(bState)

    '    bDOB = CType(dr("Zip"), Byte())
    '    strDOB = DecryptToString(bZip)

    '    bDOB = CType(dr("PName"), Byte())
    '    strDOB = DecryptToString(bPCell)

    '    bDOB = CType(dr("PDOB"), Byte())
    '    strDOB = DecryptToString(bPDOB)

    '    bDOB = CType(dr("PCell"), Byte())
    '    strDOB = DecryptToString(bCell)



    '    strQ = strFName & "|" & strLName & "|" & strDOB & "|" & strCell & "|" & dr("QCons") & "|" & dr("QFin") & "|" & dr("Q1") & "|" & dr("Q2") & "|" & dr("Q3") & "|" & dr("Q4") & "|" & dr("Q5") & "|" & dr("Q6")
    '    'Next
    '    Return strQ
    'End Function


    'Public Function RemoteCheck_SaveEnc(ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String, ByVal strCarrierEmail As String, ByVal strDelim As String) As String
    '    Dim strRetVal As String = ""
    '    Dim aryQuest As String()

    '    'Dim bFName As Byte() = EncryptStringToBytes_Aes(strFName, oKey.Key, oKey.IV)
    '    'Dim bLName As Byte() = EncryptStringToBytes_Aes(strLName, oKey.Key, oKey.IV)
    '    'Dim bDOB As Byte() = EncryptStringToBytes_Aes(strDOB, oKey.key, oKey.IV)
    '    'Dim bCell As Byte() = EncryptStringToBytes_Aes(strCell, oKey.Key, oKey.IV)

    '    Dim bFName As Byte() '= EncryptString(strFName)
    '    Dim bLName As Byte() '= EncryptString(strLName)
    '    Dim bDOB As Byte() '= EncryptString(strDOB)
    '    Dim bCell As Byte() '= EncryptString(strCell)


    '    Dim strQuest As String = ""
    '    Dim i1 As Integer
    '    Dim strQA As String = ""
    '    Dim strQB As String = ""
    '    Dim strQ1 As String = ""
    '    Dim strQ2 As String = ""
    '    Dim strQ3 As String = ""
    '    Dim strQ4 As String = ""
    '    Dim strQ5 As String = ""
    '    Dim strQ6 As String = ""
    '    Dim strQ7 As String = ""
    '    Dim strQ8 As String = ""
    '    Dim strQ9 As String = ""
    '    Dim strQ10 As String = ""
    '    Dim strQ11 As String = ""
    '    Dim strQ12 As String = ""

    '    aryQuest = strDelim.Split("|")
    '    For i1 = 0 To UBound(aryQuest)
    '        strQuest = aryQuest(i1)
    '        Select Case i1
    '            Case 0
    '                strQA = strQuest
    '            Case 1
    '                strQB = strQuest
    '            Case 2
    '                strQ1 = strQuest
    '            Case 3
    '                strQ2 = strQuest
    '            Case 4
    '                strQ3 = strQuest
    '            Case 5
    '                strQ4 = strQuest
    '            Case 6
    '                strQ5 = strQuest
    '            Case 7
    '                strQ6 = strQuest
    '            Case 8
    '                strQ7 = strQuest
    '            Case 9
    '                strQ8 = strQuest
    '            Case 10
    '                strQ9 = strQuest
    '            Case 11
    '                strQ10 = strQuest
    '            Case 12
    '                strQ11 = strQuest
    '            Case 13
    '                strQ12 = strQuest
    '        End Select
    '    Next
    '    strRetVal = MyBase.RemoteCheckinSaveEnc(bFName, bLName, bDOB, bCell, strCarrierEmail, strQA, strQB, strQ1, strQ2, strQ3, strQ4, strQ5, strQ6, strQ7, strQ8, strQ9, strQ10, strQ11, strQ12)
    '    Return strRetVal
    'End Function

    'Public Function RemoteCheckUC_SaveEncTEST(ByVal strFName As String) As String
    '    Dim strRetVal As String
    '    Dim bFName As Byte()
    '    'Using myAes As Aes = Aes.Create()
    '    '    bFName = oEncrypt.EncryptStringToBytes(strFName, myAes.Key, myAes.IV)
    '    'End Using
    '    strRetVal = MyBase.sessionCreateRecordDB()

    '    Return strRetVal
    'End Function

    'Public Function RemoteCheckUC_SaveEnc_TEST(ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String, ByVal strCellCarrier As String, ByVal strStreet As String, ByVal strApt As String, ByVal strCity As String, ByVal strState As String, ByVal strZip As String, ByVal strInsCarrier As String, ByVal strInsPolicy As String, ByVal strInsGroup As String, ByVal strPName As String, ByVal strPDOB As String, ByVal strPCell As String, ByVal strDelim As String) As String
    '    Dim strRetVal As String = ""
    '    Dim aryQuest As String()

    '    Dim bFName As Byte()
    '    Dim bLName As Byte()
    '    Dim bDOB As Byte()
    '    Dim bCell As Byte()

    '    Dim bCellCarrier As Byte()
    '    Dim bStreet As Byte()
    '    Dim bApt As Byte()
    '    Dim bCity As Byte()

    '    Dim bState As Byte()
    '    Dim bZip As Byte()
    '    Dim bInsCarrier As Byte() '= EncryptString(strInsCarrier)
    '    Dim bInsPolicy As Byte() '= EncryptString(strInsPolicy)
    '    Dim bGInsroup As Byte() '= EncryptString(strInsGroup)

    '    Dim bPName As Byte() '= EncryptString(strPName)
    '    Dim bPDOB As Byte() '= EncryptString(strPDOB)
    '    Dim bPCell As Byte() '= EncryptString(strPCell)

    '    Using myAes As Aes = Aes.Create()
    '        bFName = oEncrypt.EncryptStringToBytes(strFName, myAes.Key, myAes.IV)
    '        bLName = oEncrypt.EncryptStringToBytes(strLName, myAes.Key, myAes.IV)
    '        bDOB = oEncrypt.EncryptStringToBytes(strDOB, myAes.Key, myAes.IV)
    '        bCell = oEncrypt.EncryptStringToBytes(strCell, myAes.Key, myAes.IV)
    '        bStreet = oEncrypt.EncryptStringToBytes(strStreet, myAes.Key, myAes.IV)
    '        bApt = oEncrypt.EncryptStringToBytes(strApt, myAes.Key, myAes.IV)
    '        bCity = oEncrypt.EncryptStringToBytes(strCity, myAes.Key, myAes.IV)
    '        bState = oEncrypt.EncryptStringToBytes(strState, myAes.Key, myAes.IV)
    '        bZip = oEncrypt.EncryptStringToBytes(strZip, myAes.Key, myAes.IV)
    '        bInsCarrier = oEncrypt.EncryptStringToBytes(strInsCarrier, myAes.Key, myAes.IV)
    '        bInsPolicy = oEncrypt.EncryptStringToBytes(strInsPolicy, myAes.Key, myAes.IV)
    '        bGInsroup = oEncrypt.EncryptStringToBytes(strInsGroup, myAes.Key, myAes.IV)
    '        bPName = oEncrypt.EncryptStringToBytes(strPName, myAes.Key, myAes.IV)
    '        bPDOB = oEncrypt.EncryptStringToBytes(strPDOB, myAes.Key, myAes.IV)
    '        bPCell = oEncrypt.EncryptStringToBytes(strPCell, myAes.Key, myAes.IV)
    '    End Using

    '    Dim strQuest As String = ""
    '    Dim i1 As Integer
    '    Dim strQConsent As String = ""
    '    Dim strQFin As String = ""
    '    Dim strQ1 As String = ""
    '    Dim strQ2 As String = ""
    '    Dim strQ3 As String = ""
    '    Dim strQ4 As String = ""
    '    Dim strQ5 As String = ""
    '    Dim strQ6 As String = ""


    '    aryQuest = strDelim.Split("|")
    '    For i1 = 0 To UBound(aryQuest)
    '        strQuest = aryQuest(i1)
    '        Select Case i1
    '            Case 0
    '                strQConsent = strQuest
    '            Case 1
    '                strQFin = strQuest
    '            Case 2
    '                strQ1 = strQuest
    '            Case 3
    '                strQ2 = strQuest
    '            Case 4
    '                strQ3 = strQuest
    '            Case 5
    '                strQ4 = strQuest
    '            Case 6
    '                strQ5 = strQuest
    '            Case 7
    '                strQ6 = strQuest
    '        End Select
    '    Next
    '    strRetVal = "Success"
    '    strRetVal = MyBase.RemoteCheckinSaveEncUC(bFName, bLName, bDOB, bCell, strCellCarrier, bStreet, bApt, bCity, bState, bZip, bPName, bPDOB, bPCell, strInsCarrier, strInsPolicy, strInsGroup, strQConsent, strQFin, strQ1, strQ2, strQ3, strQ4, strQ5, strQ6)
    '    Return strRetVal
    'End Function

    'Public Function RemoteCheckUC_SaveEnc(ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String, ByVal strCellCarrier As String, ByVal strStreet As String, ByVal strApt As String, ByVal strCity As String, ByVal strState As String, ByVal strZip As String, ByVal strInsCarrier As String, ByVal strInsPolicy As String, ByVal strInsGroup As String, ByVal strPName As String, ByVal strPDOB As String, ByVal strPCell As String, ByVal strDelim As String) As String
    '    Dim strRetVal As String = ""
    '    Dim aryQuest As String()

    '    'Dim bFName As Byte() = EncryptStringToBytes_Aes(strFName, oKey.Key, oKey.IV)
    '    'Dim bLName As Byte() = EncryptStringToBytes_Aes(strLName, oKey.Key, oKey.IV)
    '    'Dim bDOB As Byte() = EncryptStringToBytes_Aes(strDOB, oKey.key, oKey.IV)
    '    'Dim bCell As Byte() = EncryptStringToBytes_Aes(strCell, oKey.Key, oKey.IV)

    '    'Dim bFName As Byte() = EncryptString(strFName)
    '    'Dim bLName As Byte() = EncryptString(strLName)
    '    'Dim bDOB As Byte() = EncryptString(strDOB)
    '    'Dim bCell As Byte() = EncryptString(strCell)

    '    Dim bFName As Byte() '= EncryptString(strFName)
    '    Dim bLName As Byte() ' = EncryptString(strLName)
    '    Dim bDOB As Byte() '= EncryptString(strDOB)
    '    Dim bCell As Byte() '= EncryptString(strCell)

    '    Dim bCellCarrier As Byte() '= EncryptString(strCellCarrier)
    '    Dim bStreet As Byte() ' = EncryptString(strStreet)
    '    Dim bApt As Byte() '= EncryptString(strApt)
    '    Dim bCity As Byte() '= EncryptString(strCity)

    '    Dim bState As Byte() ' = EncryptString(strState)
    '    Dim bZip As Byte() '= EncryptString(strZip)
    '    Dim bInsCarrier As Byte() ' = EncryptString(strInsCarrier)
    '    Dim bInsPolicy As Byte() ' = EncryptString(strInsPolicy)
    '    Dim bGInsroup As Byte() ' = EncryptString(strInsGroup)

    '    Dim bPName As Byte() '= EncryptString(strPName)
    '    Dim bPDOB As Byte() '= EncryptString(strPDOB)
    '    Dim bPCell As Byte() ' = EncryptString(strPCell)

    '    'Dim bQConsent As Byte() = EncryptString(strQConsent)
    '    'Dim bVConsent As Byte() = EncryptString(strVConsent)

    '    'Dim bQTreat As Byte() = EncryptString(strQTreat)
    '    'Dim bVTreat As Byte() = EncryptString(strVTreat)


    '    Dim strQuest As String = ""
    '    Dim i1 As Integer
    '    Dim strQConsent As String = ""
    '    Dim strQFin As String = ""
    '    Dim strQ1 As String = ""
    '    Dim strQ2 As String = ""
    '    Dim strQ3 As String = ""
    '    Dim strQ4 As String = ""
    '    Dim strQ5 As String = ""
    '    Dim strQ6 As String = ""


    '    aryQuest = strDelim.Split("|")
    '    For i1 = 0 To UBound(aryQuest)
    '        strQuest = aryQuest(i1)
    '        Select Case i1
    '            Case 0
    '                strQConsent = strQuest
    '            Case 1
    '                strQFin = strQuest
    '            Case 2
    '                strQ1 = strQuest
    '            Case 3
    '                strQ2 = strQuest
    '            Case 4
    '                strQ3 = strQuest
    '            Case 5
    '                strQ4 = strQuest
    '            Case 6
    '                strQ5 = strQuest
    '            Case 7
    '                strQ6 = strQuest
    '        End Select
    '    Next
    '    strRetVal = "Success"
    '    'strRetVal = MyBase.RemoteCheckinSaveEncUC(bFName, bLName, bDOB, bCell, strCellCarrier, bStreet, bApt, bCity, bState, bZip, bPName, bPDOB, bPCell, strInsCarrier, strInsPolicy, strInsGroup, strQConsent, strQFin, strQ1, strQ2, strQ3, strQ4, strQ5, strQ6)
    '    Return strRetVal
    'End Function

    'Public Function RemoteCheckUC_SaveEnc_TEST(ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCell As String, ByVal strCellCarrier As String, ByVal strStreet As String, ByVal strApt As String, ByVal strCity As String, ByVal strState As String, ByVal strZip As String, ByVal strInsCarrier As String, ByVal strInsPolicy As String, ByVal strInsGroup As String, ByVal strPName As String, ByVal strPDOB As String, ByVal strPCell As String, ByVal strDelim As String) As String
    '    Dim aryQuest As String()
    '    Dim strQuest As String = ""
    '    Dim i1 As Integer
    '    Dim strQConsent As String = ""
    '    Dim strQFin As String = ""
    '    Dim strQ1 As String = ""
    '    Dim strQ2 As String = ""
    '    Dim strQ3 As String = ""
    '    Dim strQ4 As String = ""
    '    Dim strQ5 As String = ""
    '    Dim strQ6 As String = ""
    '    Dim strRetVal As String
    '    aryQuest = strDelim.Split("|")
    '    For i1 = 0 To UBound(aryQuest)
    '        strQuest = aryQuest(i1)
    '        Select Case i1
    '            Case 0
    '                strQConsent = strQuest
    '            Case 1
    '                strQFin = strQuest
    '            Case 2
    '                strQ1 = strQuest
    '            Case 3
    '                strQ2 = strQuest
    '            Case 4
    '                strQ3 = strQuest
    '            Case 5
    '                strQ4 = strQuest
    '            Case 6
    '                strQ5 = strQuest
    '            Case 7
    '                strQ6 = strQuest
    '        End Select
    '    Next
    '    strRetVal = "Success"
    '    'strRetVal = MyBase.RemoteCheckinSave_UC(bFName, bLName, bDOB, bCell, strCellCarrier, bStreet, bApt, bCity, bState, bZip, bPName, bPDOB, bPCell, strInsCarrier, strInsPolicy, strInsGroup, strQConsent, strQFin, strQ1, strQ2, strQ3, strQ4, strQ5, strQ6)
    '    Return strRetVal

    'End Function


    'Private Shared Function EncryptStringToBytes_Aes(ByVal plainText As String, ByVal Key As Byte(), ByVal IV As Byte()) As Byte()
    '    ' Check arguments.
    '    If Equals(plainText, Nothing) OrElse plainText.Length <= 0 Then Throw New ArgumentNullException("plainText")
    '    If Key Is Nothing OrElse Key.Length <= 0 Then Throw New ArgumentNullException("Key")
    '    If IV Is Nothing OrElse IV.Length <= 0 Then Throw New ArgumentNullException("IV")
    '    Dim encrypted As Byte()


    '    ' Create an Aes object
    '    ' with the specified key and IV.
    '    Using aesAlg As Aes = Aes.Create()
    '        aesAlg.Key = Key
    '        aesAlg.IV = IV

    '        ' Create an encryptor to perform the stream transform.
    '        Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)


    '        ' Create the streams used for encryption.
    '        Using msEncrypt As MemoryStream = New MemoryStream()

    '            Using csEncrypt As CryptoStream = New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)

    '                Using swEncrypt As StreamWriter = New StreamWriter(csEncrypt)
    '                    'Write all data to the stream.
    '                    swEncrypt.Write(plainText)
    '                End Using

    '                encrypted = msEncrypt.ToArray()
    '            End Using
    '        End Using
    '    End Using


    '    ' Return the encrypted bytes from the memory stream.
    '    Return encrypted
    'End Function
    'Public Function SaveKey() As String
    '    Dim strRetVal As String = ""
    '    'strRetVal = RC_SaveKey(oKey)
    '    Return strRetVal
    'End Function


    'Private Shared Function DecryptStringFromBytes_Aes(ByVal cipherText As Byte(), ByVal Key As Byte(), ByVal IV As Byte()) As String
    '    ' Check arguments.
    '    If cipherText Is Nothing OrElse cipherText.Length <= 0 Then Throw New ArgumentNullException("cipherText")
    '    If Key Is Nothing OrElse Key.Length <= 0 Then Throw New ArgumentNullException("Key")
    '    If IV Is Nothing OrElse IV.Length <= 0 Then Throw New ArgumentNullException("IV")

    '    ' Declare the string used to hold
    '    ' the decrypted text.
    '    Dim plaintext As String = Nothing


    '    ' Create an Aes object
    '    ' with the specified key and IV.
    '    Using aesAlg As Aes = Aes.Create()
    '        aesAlg.Key = Key
    '        aesAlg.IV = IV

    '        ' Create a decryptor to perform the stream transform.
    '        Dim decryptor As ICryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)


    '        ' Create the streams used for decryption.
    '        Using msDecrypt As MemoryStream = New MemoryStream(cipherText)

    '            Using csDecrypt As CryptoStream = New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)

    '                Using srDecrypt As StreamReader = New StreamReader(csDecrypt)

    '                    ' Read the decrypted bytes from the decrypting stream
    '                    ' and place them in a string.
    '                    plaintext = srDecrypt.ReadToEnd()
    '                End Using
    '            End Using
    '        End Using
    '    End Using

    '    Return plaintext
    'End Function
    'Public Function GetAndReturnString(ByVal s As String) As String
    '    Dim original As String = s


    '    ' Create a new instance of the Aes
    '    ' class.  This generates a new key and initialization
    '    ' vector (IV).
    '    Using myAes As Aes = Aes.Create()

    '        ' Encrypt the string to an array of bytes.
    '        Dim encrypted As Byte() = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV)

    '        ' Decrypt the bytes to a string.
    '        Dim roundtrip As String = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV)
    '        Return roundtrip
    '        'Display the original data and the decrypted data.
    '        'Console.WriteLine("Original:   {0}", original)
    '        'Console.WriteLine("Round Trip: {0}", roundtrip)
    '    End Using

    'End Function

End Class
