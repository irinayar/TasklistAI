Imports System.Data
Imports System.Data.SqlClient
Imports OracleInternal.Secure
Partial Class TeamMemberRegistration
    Inherits System.Web.UI.Page
    Private Sub TeamMemberRegistration_Init(sender As Object, e As EventArgs) Handles Me.Init
        'Dim userid As String = Request("uid")
        If Session Is Nothing OrElse Session("admin") Is Nothing OrElse Session("admin").ToString = "" Then
            Response.Redirect("~/Default.aspx?msg=SessionExpired")
        End If
        Dim i As Integer
        'ddTopics
        If Not IsPostBack Then
            Dim du As DataView
            du = mRecords("SELECT * FROM ourunits WHERE Unit='" & Session("Unit") & "' ORDER BY Prop2")
            If du Is Nothing OrElse du.Count = 0 OrElse du.Table.Rows.Count = 0 Then
                Session("unitindx") = 8
                Session("unitname") = "OUR"
                ddTopics.Items.Clear()
                ddTopics.Items.Add("All")
                ddTopics.SelectedIndex = 0
                ddTopics.Text = "All"
                ddTopics.Visible = False
                ddTopics.Enabled = False
            Else
                Session("unitindx") = du.Table.Rows(0)("Indx")
                Session("unitname") = du.Table.Rows(0)("Unit")
                ddTopics.Visible = True
                ddTopics.Items.Clear()
                If Session("Group2").ToString.Trim = "" OrElse Session("Group2").ToString.Trim = "All" Then
                    Dim li As New ListItem
                    li.Value = "TEAMADMIN"
                    li.Text = "Team admin"
                    ddRoles.Items.Add(li)
                    ddTopics.Items.Add("All")
                    'ddTopics.SelectedIndex = 0
                    'ddTopics.Text = "All"
                    For i = 0 To du.Table.Rows.Count - 1
                        ddTopics.Items.Add(du.Table.Rows(i)("Prop2").ToString.Trim)
                        'If Session("Topic").Trim = du.Table.Rows(i)("Prop2").ToString.Trim Then
                        '    ddTopics.SelectedIndex = i
                        '    ddTopics.Text = du.Table.Rows(i)("Prop2").ToString
                        'End If
                    Next
                Else
                    Session("Topic") = Session("Group2").ToString.Trim
                    ddTopics.Items.Add(Session("Topic"))
                    'ddTopics.SelectedIndex = 0
                    ddTopics.Text = Session("Topic")
                    ddTopics.Enabled = False
                End If
            End If
        End If
        Dim err As String = String.Empty
        Dim mSql As String = String.Empty
        Dim indx As String = ""
        If Session("Unit") = "CSV" OrElse Session("Unit") = "OUR" Then
            btnSave.Visible = False
            btnSave.Enabled = False
            btnDeleteUser.Visible = False
            btnDeleteUser.Visible = False
        End If
        If Not Request("indx") Is Nothing Then
            indx = Request("indx").ToString
        Else
            'new user
            indx = ""
            btnDeleteUser.Enabled = False
            btnDeleteUser.Visible = False
            txtLogon.Enabled = False
        End If
        Session("UserIndx") = indx

        mSql = "SELECT * FROM OURUnits WHERE (Unit='" & Session("Unit") & "') AND (UserConnStr LIKE '%" & Session("UserDB").ToString.Trim.Replace(" ", "%") & "%') "

        Dim dtu As DataTable = mRecords(mSql, err).Table
        Dim ourconnstr As String = String.Empty
        Dim ourconnprv As String = String.Empty
        Dim userconnprv As String = String.Empty
        Dim strtdate As String = String.Empty
        Dim enddate As String = String.Empty
        If Not dtu Is Nothing AndAlso dtu.Rows.Count > 0 Then
            ourconnstr = dtu.Rows(0)("OURConnStr").ToString.Trim
            ourconnprv = dtu.Rows(0)("OURConnPrv").ToString.Trim
            txtConnStr.Text = dtu.Rows(0)("UserConnStr").ToString.Trim
            userconnprv = dtu.Rows(0)("UserConnPrv").ToString.Trim
            strtdate = DateToString(dtu.Rows(0)("StartDate").ToString.Trim)
            enddate = DateToString(dtu.Rows(0)("EndDate").ToString.Trim)
            Session("UnitEndDate") = enddate
        End If

        If indx = "" Then
            'new user
            txtLogon.Enabled = False
            txtLogon.ReadOnly = False
            txtUnit.Text = Session("Unit")
            For i = 0 To ddConnPrv.Items.Count - 1
                If ddConnPrv.Items(i).Value = userconnprv.Trim Then
                    ddConnPrv.Items(i).Selected = True
                End If
            Next
            txtComments.Text = "added by " & Session("logon").ToString
            txtStartDate.Text = strtdate
            txtEndDate.Text = enddate
        Else
            'existing user
            txtLogon.Enabled = False
            txtLogon.ReadOnly = True

            mSql = "SELECT * FROM OURPermits WHERE Indx='" & indx & "'"

            Dim dt As DataTable = mRecords(mSql, err).Table  'Data for report by SQL statement from the OURdb database
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                Response.Redirect("Default.aspx?msg=User is not found.")
                Exit Sub
            Else
                txtLogon.Text = dt.Rows(0)("NetId").ToString.Trim
                txtName.Text = dt.Rows(0)("Name").ToString.Trim
                txtUnit.Text = dt.Rows(0)("Unit").ToString.Trim
                txtEmail.Text = dt.Rows(0)("Email").ToString.Trim
                txtConnStr.Text = dt.Rows(0)("ConnStr").ToString.Trim
                txtComments.Text = dt.Rows(0)("Comments").ToString.Trim
                If txtComments.Text.Trim = "site registration" Then
                    btnDeleteUser.Enabled = False
                    btnDeleteUser.Visible = False
                End If
                txtStartDate.Text = dt.Rows(0)("StartDate").ToString.Trim
                txtEndDate.Text = dt.Rows(0)("EndDate").ToString.Trim
                If dt.Rows(0)("PERMIT").ToString.ToUpper = "friendly".ToUpper Then
                    chkFriendlyNames.Checked = True
                Else
                    chkFriendlyNames.Checked = False
                End If
                For i = 0 To ddTopics.Items.Count - 1
                    If ddTopics.Items(i).Value = dt.Rows(0)("Group2").ToString.Trim Then
                        ddTopics.Items(i).Selected = True
                    End If
                Next
                For i = 0 To ddRoles.Items.Count - 1
                    If ddRoles.Items(i).Value = dt.Rows(0)("ACCESS").ToString.Trim Then
                        ddRoles.Items(i).Selected = True
                    End If
                Next
                For i = 0 To ddRights.Items.Count - 1
                    If ddRights.Items(i).Value = dt.Rows(0)("RoleApp").ToString.Trim Then
                        ddRights.Items(i).Selected = True
                    End If
                Next
                For i = 0 To ddConnPrv.Items.Count - 1
                    If ddConnPrv.Items(i).Value = dt.Rows(0)("ConnPrv").ToString.Trim Then
                        ddConnPrv.Items(i).Selected = True
                    End If
                Next
            End If
        End If
        'Provider dropdown
        'Dim i As Integer = 0
        Dim selprov As String = String.Empty
        For i = 0 To ddConnPrv.Items.Count - 1
            If ddConnPrv.Items(i).Text.ToUpper = "SQL SERVER" AndAlso ConfigurationManager.AppSettings("SQLServerProv").ToString <> "OK" Then
                ddConnPrv.Items(i).Enabled = False
                ddConnPrv.Items(i).Selected = False
            End If
            If ddConnPrv.Items(i).Text.ToUpper.Contains("CACHE") AndAlso ConfigurationManager.AppSettings("CacheProv").ToString <> "OK" Then
                ddConnPrv.Items(i).Enabled = False
                ddConnPrv.Items(i).Selected = False
            End If
            If ddConnPrv.Items(i).Text.ToUpper.Contains("IRIS") AndAlso ConfigurationManager.AppSettings("IRISProv").ToString <> "OK" Then
                ddConnPrv.Items(i).Enabled = False
                ddConnPrv.Items(i).Selected = False
            End If
            If ddConnPrv.Items(i).Text.ToUpper = "MYSQL" AndAlso ConfigurationManager.AppSettings("MySqlProv").ToString <> "OK" Then
                ddConnPrv.Items(i).Enabled = False
                ddConnPrv.Items(i).Selected = False
            End If
            If ddConnPrv.Items(i).Text.ToUpper = "ORACLE" AndAlso ConfigurationManager.AppSettings("Oracle").ToString <> "OK" Then
                ddConnPrv.Items(i).Enabled = False
                ddConnPrv.Items(i).Selected = False
            End If
            If ddConnPrv.Items(i).Text.ToUpper = "CSV FILES" AndAlso ConfigurationManager.AppSettings("CSVProv").ToString <> "OK" Then
                ddConnPrv.Items(i).Enabled = False
                ddConnPrv.Items(i).Selected = False
            End If
        Next
    End Sub

    Private Sub TeamMemberRegistration_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session Is Nothing OrElse Session("admin") Is Nothing OrElse Session("admin").ToString = "" Then
            Response.Redirect("~/Default.aspx?msg=SessionExpired")
        End If
        ddTopics.SelectedValue = Session("Topic")
        ddTopics.Text = Session("Topic")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("TeamAdmin.aspx?unitdb=yes")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'check textboxes
        Dim ret As String = String.Empty
        Dim fr As String = String.Empty
        Dim acss As String = String.Empty
        Dim rol As String = String.Empty
        Dim prv As String = String.Empty
        Dim constr As String = String.Empty
        Try
            If cleanText(txtName.Text.Trim) <> txtName.Text.Trim Then
                ret = "Illegal character found in Name."
                Exit Try
            End If
            If cleanText(txtUnit.Text.Trim) <> txtUnit.Text.Trim Then
                ret = "Illegal character found in Unit."
                Exit Try
            End If
            If cleanText(txtEmail.Text.Trim) <> txtEmail.Text.Trim Then
                ret = "Illegal character found in Email."
                Exit Try
            End If
            txtLogon.Text = txtEmail.Text.Trim
            If cleanText(txtConnStr.Text.Trim) <> txtConnStr.Text.Trim Then
                ret = "Illegal character found in Connection String."
                Exit Try
            End If
            If cleanText(txtComments.Text.Trim) <> txtComments.Text.Trim Then
                ret = "Illegal character found in Comments."
                Exit Try
            End If
            'update OURPermits
            If chkFriendlyNames.Checked Then
                fr = "friendly"
            End If
            acss = ddRoles.SelectedValue
            rol = ddRights.SelectedValue
            prv = ddConnPrv.SelectedValue
            constr = Session("UserConnString").ToString.Substring(0, Session("UserConnString").ToString.IndexOf("Password")).Trim
            If Session("UserIndx") = "" Then
                Dim hasrecord As Boolean = HasRecords("SELECT * FROM OURPermits WHERE NetId='" & txtLogon.Text & "' AND Unit='" & txtUnit.Text & "' AND Application='" & Session("Application").ToString.Trim & "'")
                If hasrecord = False Then

                    Dim sqlt As String = "INSERT INTO OURPermits (Unit,Email,Name,Comments,ConnStr,ConnPrv,NetId,localpass," & FixReservedWords("Access", prv) & ",PERMIT,RoleApp,StartDate,EndDate,Application,Group2"
                    If txtUnit.Text.Trim = "CSV" Then
                        sqlt = sqlt & ",Group3"
                    End If
                    sqlt = sqlt & ") VALUES ("
                    sqlt = sqlt & "'" & txtUnit.Text & "','" & txtEmail.Text & "','" & txtName.Text & "','" & txtComments.Text & "','" & constr & "','" & prv & "','" & txtLogon.Text & "','" & txtEmail.Text & "','" & acss & "','" & fr & "','" & rol & "','"
                    If prv = "Oracle.ManagedDataAccess.Client" Then
                        sqlt = sqlt & DateToStringFormat(CDate(txtStartDate.Text), "", "dd-MMM-yy") & "','" & DateToStringFormat(CDate(txtEndDate.Text), "", "dd-MMM-yy")
                    Else
                        sqlt = sqlt & txtStartDate.Text & "','" & txtEndDate.Text
                    End If
                    sqlt = sqlt & "','" & Session("Application").ToString.Trim & "','" & ddTopics.Text & "'"

                    If txtUnit.Text.Trim = "CSV" Then
                        sqlt = sqlt & ",'CSV'"
                    End If
                    sqlt = sqlt & ")"
                    ret = ExequteSQLquery(sqlt)
                    WriteToAccessLog(Session("logon"), "User created with result: " & ret, 11)
                    'send email
                    Dim pagettl As String = ConfigurationManager.AppSettings("pagettl").ToString
                    Dim adminemail = ConfigurationManager.AppSettings("supportemail").ToString
                    Dim webhelpdesk As String = ConfigurationManager.AppSettings("webhelpdesk").ToString
                    If ret = "Query executed fine." Then
                        ret = SendHTMLEmail("", "User with logon " & txtLogon.Text & " has been registered at " & pagettl & " for team " & txtUnit.Text, "User with logon " & txtLogon.Text & " has been registered in " & pagettl & " at " & webhelpdesk & " for team " & txtUnit.Text & " with the first time password equal to the email. You will be asked to change the password after entering the first time.", txtEmail.Text, adminemail)
                    Else
                        ret = SendHTMLEmail("", "Registration crashed of the User " & txtLogon.Text & " at " & pagettl & " for team " & txtUnit.Text, "Registration of the User " & txtLogon.Text & " at " & pagettl & " crashed with result " & ret, adminemail, adminemail)
                        lblError.Text = "Request to add User #" & txtLogon.Text & " crashed. Error: " & ret & ". Contact your system administrator."
                        lblError.Visible = True
                        MessageBox.Show("Request to add User  " & txtLogon.Text & "  crashed.", "Error", "Error", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)
                        Exit Sub
                    End If

                Else
                    Dim rt As String = "Request to add User crashed. User #" & txtLogon.Text & " is already there."
                    lblError.Text = "Request to add User crashed. User with email " & txtLogon.Text & " is already there."
                    lblError.Visible = True
                    MessageBox.Show(rt, "Double User", "DoubleUser", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)
                    Exit Sub
                End If

            Else
                Dim sqlt As String = "UPDATE OURPermits SET Unit='" & txtUnit.Text & "', Email='" & txtEmail.Text & "', NetId='" & txtEmail.Text & "', Name='" & txtName.Text & "' , Comments='" & txtComments.Text & "', Group2='" & ddTopics.Text & "',  "
                sqlt = sqlt & " ACCESS='" & acss & "', PERMIT='" & fr & "', RoleApp='" & rol & "'  WHERE Indx='" & Session("UserIndx") & "'"
                ret = ExequteSQLquery(sqlt)
                If txtEndDate.Text > Session("UnitEndDate") Then
                    sqlt = "UPDATE OURPermits SET EndDate='" & Session("UnitEndDate") & "'"
                    ret = ret & ", End Date corrected. " & ExequteSQLquery(sqlt)
                End If

                WriteToAccessLog(Session("logon"), "User updated with result: " & ret, 11)
            End If
        Catch ex As Exception
            ret = ret & ex.Message
        End Try
        Response.Redirect("TeamAdmin.aspx?unitdb=yes")
    End Sub

    Private Sub btnDeleteUser_Click(sender As Object, e As EventArgs) Handles btnDeleteUser.Click
        Dim ret As String = "Request to delete User. User #" & Session("UserIndx") & " will be permanently deleted. Please confirm."
        MessageBox.Show(ret, "Disable User", "DisableUser", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)

    End Sub
    Private Sub MessageBox_MessageResulted(sender As Object, e As Controls_Msgbox.MsgBoxEventArgs) Handles MessageBox.MessageResulted
        If e.Tag = "DisableUser" Then
            Dim duedate As String = DateToString(Now)
            Dim sqlt As String = "DELETE FROM OURPermits  WHERE Indx='" & Session("UserIndx") & "'"
            Dim ret As String = ExequteSQLquery(sqlt)
            WriteToAccessLog(Session("logon"), "User #" & Session("UserIndx") & " disabled with result: " & ret, 11)
            'Response.Write("<script language='javascript'> { window.close(); }</script>")
            Response.Redirect("TeamAdmin.aspx?unitdb=yes")
        ElseIf e.Tag = "DoubleUser" Then
            txtEmail.Focus()
        End If
    End Sub

    Private Sub ddTopics_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddTopics.SelectedIndexChanged
        Session("Topic") = ddTopics.SelectedValue
        Session("TopicIndx") = ddTopics.SelectedIndex
    End Sub
End Class
