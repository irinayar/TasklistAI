Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Newtonsoft.Json.Linq

Partial Class TeamRegistration
    Inherits System.Web.UI.Page
    Private indx As String
    Private lblSqlServer As String
    Private lblCache As String
    Private lblOracle As String
    Private Sub TeamRegistration_Init(sender As Object, e As EventArgs) Handles Me.Init
        'If Session Is Nothing OrElse Session("admin") Is Nothing OrElse Session("admin").ToString = "" Then
        '    Response.Redirect("~/Default.aspx?msg=SessionExpired")
        'End If
        Session("Application") = ConfigurationManager.AppSettings("ourapplication").ToString
        Session("org") = "team"
        Session("OURConnString") = System.Configuration.ConfigurationManager.ConnectionStrings.Item("mySQLconnection").ToString
        Session("OURConnProvider") = System.Configuration.ConfigurationManager.ConnectionStrings.Item("mySQLconnection").ProviderName.ToString
        Session("UserConnString") = System.Configuration.ConfigurationManager.ConnectionStrings.Item("mySQLconnection").ToString
        Session("UserConnProvider") = System.Configuration.ConfigurationManager.ConnectionStrings.Item("mySQLconnection").ProviderName.ToString
        Session("WEBOUR") = ConfigurationManager.AppSettings("weboureports").ToString
        HyperLink1.NavigateUrl = Session("WEBOUR") & "index.aspx"
        lblSqlServer = "Server=yourserver; Database=yourdatabase; User ID=youruserid; Password=yourpassword"
        lblCache = "Server = yourserver; Port = yourport; Namespace = yournamespace; User ID = youruser; Password = yourpassword"
        lblOracle = "data source=yourserver:yourport/yourdatabase;user id=youruser;password=yourpassword"
        If Not Request("org") Is Nothing Then
            Session("org") = Request("org").ToString
        Else
            Session("org") = "team"
        End If
        'it will be saved in Prop1 field of OURUnits table
        If Request("org") Is Nothing AndAlso Session("org") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        ElseIf Session("org") = "team" Then

        End If
        btnUpdate.Enabled = False
        btnUpdate.Visible = False
        btnSave.Enabled = False
        btnSave.Visible = False
        Date1.Enabled = False
        Date2.Enabled = False
        Dim i As Integer
        Dim err As String = String.Empty
        Dim selprov As String = String.Empty
        Dim seluprov As String = String.Empty
        If Not Request("indx") Is Nothing Then
            indx = Request("indx").ToString
        ElseIf Not Session("UnitIndx") Is Nothing Then
            indx = Session("UnitIndx").ToString.Trim
        Else
            indx = ""
            btnUpdate.Enabled = False
            btnUpdate.Visible = False
            chkTermsAndCond.Checked = False
        End If
        Session("UnitIndx") = indx

        'txtLogon.Text = Session("logon").ToString.Trim
        If indx = "" Then
            'new unit
            Label10.Text = "new unit"
            txtUserConnStr.Text = Session("UserConnString")  ' lblSqlServer
            ddUserConnPrv.SelectedValue = "MySql.Data.MySqlClient"
            btnUpdate.Enabled = False
            btnUpdate.Visible = False

            HyperLinkSeeProposal.Enabled = False
            HyperLinkSeeProposal.Visible = False
            HyperLinkUnitOURWeb.Enabled = False
            HyperLinkUnitOURWeb.Visible = False

            Date1.Text = DateToString(CDate(Now()))
            Date2.Text = DateToString(CDate(DateAndTime.DateAdd(DateInterval.Day, 3600, Now())))  '~10 years
            txtComments.Text = "On " & Date1.Text & " "
        Else
            btnUpdate.Enabled = False
            btnUpdate.Visible = False

            txtLogon.Enabled = False
            Session("UnitIndx") = indx
            Label10.Text = "Unit index #" & indx
            Dim mSql As String = "SELECT * FROM OURUnits WHERE Indx='" & indx & "'"
            Dim dt As DataTable = mRecords(mSql, err).Table  'Data for report by SQL statement from the OURdb database
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                Response.Redirect("Default.aspx?msg=User is not found.")
                Exit Sub
            Else
                txtUnitWeb.Text = dt.Rows(0)("UnitWeb").ToString.Trim
                txtUnit.Text = dt.Rows(0)("Unit").ToString.Trim
                txtOURdb.Text = dt.Rows(0)("OURConnStr").ToString.Trim
                txtUserConnStr.Text = dt.Rows(0)("UserConnStr").ToString.Trim
                txtComments.Text = dt.Rows(0)("Comments").ToString.Trim & " | On " & DateToString(CDate(Now())) & " edited by " & txtLogon.Text

                For i = 0 To ddModels.Items.Count - 1
                    If ddModels.Items(i).Value = dt.Rows(0)("DistrMode").ToString.Trim.Replace(" ", "") Then
                        ddModels.Items(i).Selected = True
                    End If
                Next
                For i = 0 To ddOURConnPrv.Items.Count - 1
                    If ddOURConnPrv.Items(i).Value = dt.Rows(0)("OURConnPrv").ToString.Trim Then
                        ddOURConnPrv.Items(i).Selected = True
                        selprov = ddOURConnPrv.Items(i).Text.ToUpper
                    End If
                Next
                For i = 0 To ddUserConnPrv.Items.Count - 1
                    If ddUserConnPrv.Items(i).Value = dt.Rows(0)("UserConnPrv").ToString.Trim Then
                        ddUserConnPrv.Items(i).Selected = True
                        seluprov = ddUserConnPrv.Items(i).Text.ToUpper
                    End If
                Next
                Date1.Text = dt.Rows(0)("StartDate").ToString.Trim
                Date2.Text = dt.Rows(0)("EndDate").ToString.Trim

                Session("CurrentEndDate") = dt.Rows(0)("EndDate").ToString.Trim
                Session("Unit") = dt.Rows(0)("Unit").ToString.Trim
                Session("UnitDB") = dt.Rows(0)("UserConnStr").ToString.Trim
            End If
        End If

        For i = 0 To ddOURConnPrv.Items.Count - 1
            If ddOURConnPrv.Items(i).Text.ToUpper = "SQL SERVER" AndAlso ConfigurationManager.AppSettings("SQLServerProv").ToString <> "OK" Then
                ddOURConnPrv.Items(i).Enabled = False
                ddOURConnPrv.Items(i).Selected = False
            ElseIf selprov = "" AndAlso ddOURConnPrv.Items(i).Text.ToUpper = "SQL SERVER" AndAlso ConfigurationManager.AppSettings("SqlServerProv").ToString = "OK" Then
                ddOURConnPrv.Text = ddOURConnPrv.Items(i).Text
                ddOURConnPrv.Items(i).Selected = True
                selprov = "SQL"
            End If
            If ddOURConnPrv.Items(i).Text.ToUpper.Contains("CACHE") AndAlso ConfigurationManager.AppSettings("CacheProv").ToString <> "OK" Then
                ddOURConnPrv.Items(i).Enabled = False
                ddOURConnPrv.Items(i).Selected = False
            ElseIf selprov = "" AndAlso ddOURConnPrv.Items(i).Text.ToUpper.Contains("CACHE") AndAlso ConfigurationManager.AppSettings("CacheProv").ToString = "OK" Then
                ddOURConnPrv.Text = ddOURConnPrv.Items(i).Text
                ddOURConnPrv.Items(i).Selected = True
                selprov = "CACHE"
            End If
            If ddOURConnPrv.Items(i).Text.ToUpper.Contains("IRIS") AndAlso ConfigurationManager.AppSettings("IRISProv").ToString <> "OK" Then
                ddOURConnPrv.Items(i).Enabled = False
                ddOURConnPrv.Items(i).Selected = False
            ElseIf selprov = "" AndAlso ddOURConnPrv.Items(i).Text.ToUpper.Contains("IRIS") AndAlso ConfigurationManager.AppSettings("IRISProv").ToString = "OK" Then
                ddOURConnPrv.Text = ddOURConnPrv.Items(i).Text
                ddOURConnPrv.Items(i).Selected = True
                selprov = "IRIS"
            End If
            If ddOURConnPrv.Items(i).Text.ToUpper = "MYSQL" AndAlso ConfigurationManager.AppSettings("MySqlProv").ToString <> "OK" Then
                ddOURConnPrv.Items(i).Enabled = False
                ddOURConnPrv.Items(i).Selected = False
            ElseIf selprov = "" AndAlso ddOURConnPrv.Items(i).Text.ToUpper = "MYSQL" AndAlso ConfigurationManager.AppSettings("MySqlProv").ToString = "OK" Then
                ddOURConnPrv.Text = ddOURConnPrv.Items(i).Text
                ddOURConnPrv.Items(i).Selected = True
                selprov = "MYSQL"
            End If
            If ddOURConnPrv.Items(i).Text.ToUpper = "ORACLE" AndAlso ConfigurationManager.AppSettings("Oracle").ToString <> "OK" Then
                ddOURConnPrv.Items(i).Enabled = False
                ddOURConnPrv.Items(i).Selected = False
            ElseIf selprov = "" AndAlso ddOURConnPrv.Items(i).Text.ToUpper = "ORACLE" AndAlso ConfigurationManager.AppSettings("Oracle").ToString = "OK" Then
                ddOURConnPrv.Text = ddOURConnPrv.Items(i).Text
                ddOURConnPrv.Items(i).Selected = True
                selprov = "ORACLE"
            End If
            If ddOURConnPrv.Items(i).Text.ToUpper = "CSV FILES" AndAlso ConfigurationManager.AppSettings("CSVProv").ToString <> "OK" Then
                ddOURConnPrv.Items(i).Enabled = False
                ddOURConnPrv.Items(i).Selected = False

            End If
        Next

        For i = 0 To ddUserConnPrv.Items.Count - 1
            If ddUserConnPrv.Items(i).Text.ToUpper = "SQL SERVER" AndAlso ConfigurationManager.AppSettings("SQLServerProv").ToString <> "OK" Then
                ddUserConnPrv.Items(i).Enabled = False
                ddUserConnPrv.Items(i).Selected = False
            ElseIf seluprov = "" AndAlso ddUserConnPrv.Items(i).Text.ToUpper = "SQL SERVER" AndAlso ConfigurationManager.AppSettings("SqlServerProv").ToString = "OK" Then
                ddUserConnPrv.Text = ddUserConnPrv.Items(i).Text
                ddUserConnPrv.Items(i).Selected = True
                seluprov = "SQL"
            End If
            If ddUserConnPrv.Items(i).Text.ToUpper.Contains("CACHE") AndAlso ConfigurationManager.AppSettings("CacheProv").ToString <> "OK" Then
                ddUserConnPrv.Items(i).Enabled = False
                ddUserConnPrv.Items(i).Selected = False
            ElseIf seluprov = "" AndAlso ddUserConnPrv.Items(i).Text.ToUpper.Contains("CACHE") AndAlso ConfigurationManager.AppSettings("CacheProv").ToString = "OK" Then
                ddUserConnPrv.Text = ddUserConnPrv.Items(i).Text
                ddUserConnPrv.Items(i).Selected = True
                seluprov = "CACHE"
            End If
            If ddUserConnPrv.Items(i).Text.ToUpper.Contains("IRIS") AndAlso ConfigurationManager.AppSettings("IRISProv").ToString <> "OK" Then
                ddUserConnPrv.Items(i).Enabled = False
                ddUserConnPrv.Items(i).Selected = False
            ElseIf seluprov = "" AndAlso ddUserConnPrv.Items(i).Text.ToUpper.Contains("IRIS") AndAlso ConfigurationManager.AppSettings("IRISProv").ToString = "OK" Then
                ddUserConnPrv.Text = ddUserConnPrv.Items(i).Text
                ddUserConnPrv.Items(i).Selected = True
                seluprov = "IRIS"
            End If
            If ddUserConnPrv.Items(i).Text.ToUpper = "MYSQL" AndAlso ConfigurationManager.AppSettings("MySqlProv").ToString <> "OK" Then
                ddUserConnPrv.Items(i).Enabled = False
                ddUserConnPrv.Items(i).Selected = False
            ElseIf seluprov = "" AndAlso ddUserConnPrv.Items(i).Text.ToUpper = "MYSQL" AndAlso ConfigurationManager.AppSettings("MySqlProv").ToString = "OK" Then
                ddUserConnPrv.Text = ddUserConnPrv.Items(i).Text
                ddUserConnPrv.Items(i).Selected = True
                seluprov = "MYSQL"
            End If
            If ddUserConnPrv.Items(i).Text.ToUpper = "ORACLE" AndAlso ConfigurationManager.AppSettings("Oracle").ToString <> "OK" Then
                ddUserConnPrv.Items(i).Enabled = False
                ddUserConnPrv.Items(i).Selected = False
            ElseIf seluprov = "" AndAlso ddUserConnPrv.Items(i).Text.ToUpper = "ORACLE" AndAlso ConfigurationManager.AppSettings("Oracle").ToString = "OK" Then
                ddUserConnPrv.Text = ddUserConnPrv.Items(i).Text
                ddUserConnPrv.Items(i).Selected = True
                seluprov = "ORACLE"
            End If
            If ddUserConnPrv.Items(i).Text.ToUpper = "CSV FILES" AndAlso ConfigurationManager.AppSettings("CSVProv").ToString <> "OK" Then
                ddUserConnPrv.Items(i).Enabled = False
                ddUserConnPrv.Items(i).Selected = False

            End If
        Next
        If Not Request("res") Is Nothing AndAlso Request("res").ToString.Trim <> "" Then
            Label9.Text = cleanText(Request("res").ToString)
        End If
    End Sub

    Private Sub TeamRegistration_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack AndAlso txtPassword.Text.Trim = "" Then
            txtPassword.BorderColor = Color.Red
        End If
        If IsPostBack AndAlso txtRepeat.Text.Trim = "" Then
            txtRepeat.BorderColor = Color.Red
        End If
        If Session("org") = "company" OrElse Session("org") = "vendor" Then
            txtOURdb.Text = ConfigurationManager.AppSettings("unitOURdbConnStr").ToString
            ddOURConnPrv.SelectedValue = System.Configuration.ConfigurationManager.ConnectionStrings.Item("mySQLconnection").ProviderName.ToString
            ddModels.SelectedValue = "Unit OUReports on OUR server"
            txtUnitWeb.Text = "DataAI.link/subfolder - will be assigned after registration"
            txtUnitWeb.Enabled = False
            trOURdb.Visible = False
            trOURprv.Visible = False
            trModels.Visible = False
            tr9.Visible = False
            tr10.Visible = False
            tr12.Visible = False
        ElseIf Session("org") = "team" Then
            txtOURdb.Text = Session("OURConnString") 'ConfigurationManager.AppSettings("unitOURdbConnStr").ToString
            txtUserConnStr.Text = Session("UserConnString")
            ddOURConnPrv.SelectedValue = Session("OURConnProvider") 'System.Configuration.ConfigurationManager.ConnectionStrings.Item("mySQLconnection").ProviderName.ToString
            ddUserConnPrv.SelectedValue = Session("UserConnProvider")
            ddModels.SelectedValue = "Unit OUReports on OUR server"
            txtUnitWeb.Text = "DataAI.link/subfolder - will be assigned after registration"
            txtUnitWeb.Enabled = False
            trUnitWeb.Visible = False
            trOURdb.Visible = False
            trOURprv.Visible = False
            trModels.Visible = False
            tr9.Visible = False
            tr10.Visible = False
            tr12.Visible = False
            tr14.Visible = False
            tr13.Visible = False
            tr5.Visible = False
            tr6.Visible = False
            tr4.Visible = False
            tr8.Visible = False
        ElseIf Session("org") = "vendor" Then
            ddModels.SelectedValue = "Unit OUReports on UNIT server"
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'SAMPLE CLOSING WINDOWS FROM CODE BEHIND: - DO NOT DELETE!!!
        'Response.Write("<script language='javascript'> { window.close(); }</script>")
        Response.Redirect("Default.aspx")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'check textboxes
        Dim ret As String = String.Empty
        Try
            If cleanText(txtUnit.Text.Trim) <> txtUnit.Text.Trim Then
                ret = "Illegal character found in Unit text."
                Exit Try
            End If
            If Label10.Text = "new unit" AndAlso HasRecords("SELECT * FROM ourunits WHERE UCASE(Unit)='" & txtUnit.Text.ToUpper & "'") Then
                ret = "Team is already registered. Select another team name to register the new team or contact the team admin to add you as user to this team Task List..."
                Exit Try
            End If

            If cleanText(txtTopic.Text.Trim) <> txtTopic.Text.Trim Then
                ret = "Illegal character found in Topic text."
                Exit Try
            End If
            If HasRecords("SELECT * FROM ourunits WHERE UCASE(Unit)='" & txtUnit.Text.ToUpper & "' And UCASE(Prop2)='" & txtTopic.Text.ToUpper & "'") Then
                ret = "Topic for this team is already registered. Select another topic to register the new topic and team or contact the team admin to add you as user to this team topic..."
                Exit Try
            End If

            If cleanText(txtLogon.Text.Trim) <> txtLogon.Text.Trim Then
                ret = "Illegal character found in Logon text."
                Exit Try
            End If
            If HasRecords("SELECT * FROM OURPERMITS WHERE (NetId='" & txtLogon.Text & "') AND (Unit='" & txtUnit.Text & "')") Then
                ret = "Logon is already used. Select another logon or unit name to register the new team or contact team admin to add you as user to this team..."
                Exit Try
            End If


            If cleanText(txtPassword.Text.Trim) <> txtPassword.Text.Trim Then
                ret = "Illegal character found in Password text."
                Exit Try
            End If
            If cleanText(txtRepeat.Text.Trim) <> txtRepeat.Text.Trim Then
                ret = "Illegal character found in Password Repeat text."
                Exit Try
            End If
            If cleanText(txtName.Text.Trim) <> txtName.Text.Trim Then
                ret = "Illegal character found in Name text."
                Exit Try
            End If
            If cleanText(txtPhone.Text.Trim) <> txtPhone.Text.Trim Then
                ret = "Illegal character found in Phone text."
                Exit Try
            End If
            If cleanText(txtEmail.Text.Trim) <> txtEmail.Text.Trim Then
                ret = "Illegal character found in Email text."
                Exit Try
            End If
            If cleanText(txtUnitWeb.Text.Trim) <> txtUnitWeb.Text.Trim Then
                ret = "Illegal character found in Unit Web text."
                Exit Try
            End If

            If cleanText(txtUnitBossName.Text.Trim) <> txtUnitBossName.Text.Trim Then
                ret = "Illegal character found in Unit Official Title and Name text."
                Exit Try
            End If
            If cleanText(txtUnitPhone.Text.Trim) <> txtUnitPhone.Text.Trim Then
                ret = "Illegal character found in Unit Phone text."
                Exit Try
            End If
            If cleanText(txtUnitEmail.Text.Trim) <> txtUnitEmail.Text.Trim Then
                ret = "Illegal character found in Unit Email text."
                Exit Try
            End If
            If cleanText(txtOURdb.Text.Trim) <> txtOURdb.Text.Trim Then
                ret = "Illegal character found in OUR db connection string."
                Exit Try
            End If
            If cleanText(txtUserConnStr.Text.Trim) <> txtUserConnStr.Text.Trim Then
                ret = "Illegal character found in User db connection string."
                Exit Try
            End If
            If cleanText(txtComments.Text.Trim) <> txtComments.Text.Trim Then
                ret = "Illegal character found in Comments."
                Exit Try
            End If
            If cleanText(txtAgentName.Text.Trim) <> txtAgentName.Text.Trim Then
                ret = "Illegal character found in Agent Name text."
                Exit Try
            End If
            If cleanText(txtAgentPhone.Text.Trim) <> txtAgentPhone.Text.Trim Then
                ret = "Illegal character found in Agent Phone text."
                Exit Try
            End If
            If cleanText(txtAgentEmail.Text.Trim) <> txtAgentEmail.Text.Trim Then
                ret = "Illegal character found in Agent Email text."
                Exit Try
            End If

            If txtPassword.Text <> txtRepeat.Text Then
                ret = "Password and Repeat Password do not match."
                Exit Try
            End If
            If txtLogon.Text.Trim = "" OrElse txtPassword.Text.Trim = "" OrElse txtName.Text.Trim = "" OrElse txtPhone.Text.Trim = "" OrElse txtEmail.Text.Trim = "" Then
                ret = "Fill out all form. Fields should not be empty."
                Exit Try
            End If

            'TODO add more checking for fields as needed
            If txtUserConnStr.Text = lblSqlServer OrElse txtUserConnStr.Text = lblCache OrElse txtUserConnStr.Text = lblOracle Then
                txtUserConnStr.BorderColor = Color.Red
                ret = "Fill out proper connection string (server, database/namespace, user id, etc...)."
                Exit Try
            End If

            'TODO check connections to databases
            Dim er As String = String.Empty
            'If Not DatabaseConnected(txtOURdb.Text, ddOURConnPrv.SelectedValue, er) Then
            '    ret = "Connection to ourdb database can not been open... Wrong connection string: " & txtOURdb.Text
            '    Label9.Text = ret
            '    WriteToAccessLog(txtLogon.Text, "Connection to ourdb cannot be open:" & txtOURdb.Text, 1)
            '    Exit Try
            'End If
            'If Not DatabaseConnected(txtUserConnStr.Text, ddUserConnPrv.SelectedValue, er) Then
            '    ret = "Connection to user database can not been open... Wrong connection string: " & txtUserConnStr.Text
            '    Label9.Text = ret
            '    WriteToAccessLog(txtLogon.Text, "Connection to user cannot be open:" & txtUserConnStr.Text, 1)
            '    Exit Try
            'End If

            'Session("logon") = txtLogon.Text
            Dim ourdb As String = txtOURdb.Text.Trim
            If ourdb.ToUpper.IndexOf("USER ID") > 0 Then ourdb = ourdb.Substring(0, ourdb.ToUpper.IndexOf("USER ID")).Trim
            If ourdb.ToUpper.IndexOf("PASSWORD") > 0 Then ourdb = ourdb.Substring(0, ourdb.ToUpper.IndexOf("PASSWORD")).Trim
            Dim userdb As String = txtUserConnStr.Text.Trim
            If userdb.ToUpper.IndexOf("USER ID") > 0 Then userdb = userdb.Substring(0, userdb.ToUpper.IndexOf("USER ID")).Trim
            If userdb.ToUpper.IndexOf("PASSWORD") > 0 Then userdb = userdb.Substring(0, userdb.ToUpper.IndexOf("PASSWORD")).Trim


            'insert or update OURUnits
            Dim sqlt As String = String.Empty
            Dim n As Integer = 0
            Dim agentindx As String = Session("agent")
            Dim sqlag As String = String.Empty
            If indx = "" Then
                'check if unit/db exist
                'Dim hasrecord As Boolean = HasRecords("SELECT * FROM OURUnits WHERE UserConnStr LIKE '%" & userdb.Replace(" ", "%") & "%' AND Unit='" & txtUnit.Text.Trim & "'")
                Dim hasrecord As Boolean = HasRecords("SELECT * FROM OURUnits WHERE Unit='" & txtUnit.Text.Trim & "'")
                If hasrecord = False Then  'no that unit in OURUnits
                    txtComments.Text = txtComments.Text & " added by " & txtLogon.Text & ", agreed to Terms and Conditions."
                    'restore user id in conn strings
                    Dim ourdb1 As String = txtOURdb.Text.Trim
                    If ourdb1.ToUpper.IndexOf("PASSWORD") > 0 Then ourdb1 = ourdb1.Substring(0, ourdb1.ToUpper.IndexOf("PASSWORD")).Trim
                    Dim userdb1 As String = txtUserConnStr.Text.Trim
                    If userdb1.ToUpper.IndexOf("PASSWORD") > 0 Then userdb1 = userdb1.Substring(0, userdb1.ToUpper.IndexOf("PASSWORD")).Trim

                    'insert
                    'sqlt = "INSERT INTO OURUnits SET Unit='" & txtUnit.Text.Trim & "', DistrMode='" & ddModels.SelectedValue & "', UnitWeb='" & txtUnitWeb.Text & "' , Comments='" & txtComments.Text & "' , StartDate='" & Date1.Text & "' , EndDate='" & Date2.Text & "',  "
                    'sqlt = sqlt & " OURConnStr='" & ourdb1 & "', OURConnPrv='" & ddOURConnPrv.SelectedValue & "', UserConnStr='" & userdb1 & "', UserConnPrv='" & ddUserConnPrv.SelectedValue & "',"
                    'sqlt = sqlt & " Official='" & txtUnitBossName.Text.Trim & "', Address='" & txtUnitAddress.Text & "', Phone='" & txtUnitPhone.Text & "', Email='" & txtUnitEmail.Text & "', Prop1='" & Session("org") & "', Prop2='" & txtTopic.Text.Trim & "'"
                    'If agentindx <> "" Then
                    '    sqlt = sqlt & ", Agent='" & agentindx & "'"
                    'End If

                    'er = ExequteSQLquery(sqlt)  'unit registered
                    Dim sFields As String = String.Empty
                    Dim sValues As String = String.Empty

                    sFields = "Unit,DistrMode,UnitWeb,Comments,StartDate,EndDate,OURConnStr,OURConnPrv,UserConnStr,UserConnPrv,Official,Address,Phone,Email,Prop1"
                    sValues = "'" & txtUnit.Text.Trim & "',"
                    sValues &= "'" & ddModels.SelectedValue & "',"
                    sValues &= "'" & txtUnitWeb.Text & "',"
                    sValues &= "'" & txtComments.Text & "',"
                    sValues &= "'" & Date1.Text & "',"
                    sValues &= "'" & Date2.Text & "',"
                    sValues &= "'" & ourdb1 & "',"
                    sValues &= "'" & ddOURConnPrv.SelectedValue & "',"
                    sValues &= "'" & userdb1 & "',"
                    sValues &= "'" & ddUserConnPrv.SelectedValue & "',"
                    sValues &= "'" & txtUnitBossName.Text.Trim & "',"
                    sValues &= "'" & txtUnitAddress.Text & "',"
                    sValues &= "'" & txtUnitPhone.Text & "',"
                    sValues &= "'" & txtUnitEmail.Text & "',"
                    sValues &= "'" & Session("org") & "'"

                    sqlt = "INSERT INTO OURUnits (" & sFields & ") VALUES (" & sValues & ")"

                    er = ExequteSQLquery(sqlt)  'unit registered in original site
                    If er = "Query executed fine." Then
                        ret = "Team has been registered."
                        WriteToAccessLog(txtLogon.Text, "Team " & txtUnit.Text & " has been registered:" & txtUserConnStr.Text, 1)
                        'MessageBox.Show("Unit has been registered", "New Unit Registration", "NewUnitRegistration", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)

                        ''add activity into OURActivity table
                        Dim dtunit As DataTable = mRecords("SELECT * FROM OURUnits WHERE OURConnStr LIKE '%" & ourdb.Trim.Replace(" ", "%") & "%' AND UserConnStr LIKE '%" & userdb.Trim.Replace(" ", "%") & "%'").Table
                        Dim unitindx As String = String.Empty

                        'insert or update siteadmin, who fill out the form, into OURPermits in Unit OURdb
                        Dim hasrec As Boolean = HasRecords("SELECT * FROM OURPERMITS WHERE (NetId='" & txtLogon.Text & "') AND (ConnStr LIKE '%" & userdb1.Trim.Replace(" ", "%") & "%') AND (Application='" & Session("Application").ToString.Trim & "')", txtOURdb.Text.Trim, ddOURConnPrv.SelectedValue)
                        If hasrec Then
                            ret = "The logon is not available. Please select a different one."
                            WriteToAccessLog(txtLogon.Text, "Unit " & txtUnit.Text & " #" & unitindx & ". The logon is not available. Please select a different one. " & txtUserConnStr.Text, 1)
                            Exit Try
                        End If

                        ret = RegisterTeamAdmin("TEAMADMIN", "friendly", txtLogon.Text, txtPassword.Text, txtName.Text, txtUnit.Text, Session("Application").ToString, "admin", "", "All", "", txtEmail.Text, "site registration", userdb1, ddUserConnPrv.SelectedValue, DateToString(Date1.Text), DateToString(Date2.Text), txtPhone.Text, txtOURdb.Text.Trim, ddOURConnPrv.SelectedValue)
                        Session("email") = txtEmail.Text
                        'send email
                        Dim webhelpdesk As String = ConfigurationManager.AppSettings("webhelpdesk").ToString
                        SendHTMLEmail("", "Team " & txtUnit.Text & " has been registered", "Team " & txtUnit.Text & " has been registered at " & webhelpdesk & " with teamadmin " & txtLogon.Text, Session("email"), Session("SupportEmail"))

                        tr11.Visible = False
                        trText1.Visible = False
                        txtUserConnStr.Enabled = False
                        ddUserConnPrv.Enabled = False
                        btnSave.Visible = False
                        btnSave.Enabled = False
                        txtComments.Enabled = False

                    Else
                        Dim rt As String = "Request to add Team crashed. Team #" & txtUnit.Text & " has not been registered."
                        MessageBox.Show(rt, "No Unit", "NoUnit", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)
                        Exit Sub
                    End If

                    'TODO message about long time for data analysis
                    'Make Initial Reports
                    Session("email") = txtEmail.Text
                    Session("UserEndDate") = DateToString(Date2.Text)
                    'Dim rtn As String = MakeInitialReports(Session("logon"), Session("email"), Session("UserEndDate"), Session("UserConnString"), Session("UserConnProvider"), er)
                    Response.Redirect("Default.aspx")

                Else
                    ret = "Request to add Team crashed. Team with the same name is already registered."
                    Exit Try
                    'MessageBox.Show(rt, "Double Unit", "DoubleUnit", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)
                End If

            Else 'existing Unit
                'TODO update redirect to UserDefinition.aspx

                Exit Sub

            End If

        Catch ex As Exception
            ret = ret & ex.Message
        End Try
        MessageBox.Show(ret, "Unit Registration", "UnitRegistration", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)
        WriteToAccessLog(txtLogon.Text, ret, 11)
        Label9.Text = ret

    End Sub

    'Private Sub btnDeleteUser_Click(sender As Object, e As EventArgs) Handles btnDeleteUser.Click
    '    Dim ret As String = "Request to disable Unit. Unit #" & Session("UnitIndx") & " will be permanently disabled along with Unit users and reports. Please confirm."
    '    MessageBox.Show(ret, "Disable Unit", "DisableUnit", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)

    'End Sub
    Private Sub MessageBox_MessageResulted(sender As Object, e As Controls_Msgbox.MsgBoxEventArgs) Handles MessageBox.MessageResulted
        If e.Tag = "DisableUnit" Then
            Dim duedate As String = DateToString(Now)
            Dim sqlt As String = String.Empty
            Dim ret As String = String.Empty
            'disable active at this moment users and report permissions
            sqlt = "UPDATE OURPermits SET EndDate='" & duedate & "' WHERE Unit='" & Session("Unit") & "' AND ConnStr='" & Session("UnitDB") & "' AND (EndDate > '" & duedate & "')"
            ret = "Users in OURPermits updated with result: " & ExequteSQLquery(sqlt)

            sqlt = "UPDATE OURPermissions SET OpenTo='" & duedate & "' FROM OURPermissions INNER JOIN OURReportInfo ON (OURPermissions.Param1=OURReportInfo.ReportId) INNER JOIN OURPermits ON (OURPermissions.NetId=OURPermits.NetId) WHERE OURPermits.Unit='" & Session("Unit") & "' AND OURReportInfo.ReportDB='" & Session("UnitDB") & "' AND (OURPermissions.OpenTo > '" & duedate & "')"
            ret = ret & " -Reports in OURPermissions updated with result: " & ExequteSQLquery(sqlt)

            'disable Unit
            sqlt = "UPDATE OURUnits SET EndDate='" & duedate & "'  WHERE Indx='" & Session("UnitIndx") & "'"
            ret = ret & " -Unit updated with result: " & ExequteSQLquery(sqlt)

            WriteToAccessLog(txtLogon.Text, "Unit #" & Session("UnitIndx") & " disabled with result: " & ret, 11)
            Response.Redirect("UnitsAdmin.aspx")
        ElseIf e.Tag = "UpdateUnit" Then
            'update Unit OURdb to current version
            Dim ret As String = String.Empty
            'ret = UpdateOURdbToCurrentVersion(txtOURdb.Text.Trim, ddOURConnPrv.SelectedValue)
            Label9.Text = ret
            'update Unit Web and web.config
            'TODO
            WriteToAccessLog(txtLogon.Text, "Unit #" & Session("UnitIndx") & " updated to current version with result: " & ret, 11)
            Response.Redirect("UnitDefinition.aspx?indx=" & Session("UnitIndx") & "&res=" & cleanText(ret.Replace("<br/>", " | ")))
        ElseIf e.Tag = "UninstallUnitOURdb" Then
            'Uninstall Unit's OURdb
            Dim ret As String = String.Empty
            ret = UninstallOURTablesClasses(txtOURdb.Text.Trim, ddOURConnPrv.SelectedValue)
            Label9.Text = ret
            'delete Unit Web
            'TODO
            WriteToAccessLog(txtLogon.Text, "Unit #" & Session("UnitIndx") & " OURdb tables uninstalled with result: " & ret, 11)
            Response.Redirect("UnitDefinition.aspx?indx=" & Session("UnitIndx") & "&res=" & cleanText(ret.Replace("<br/>", " | ")))
        ElseIf e.Tag = "DoubleUnit" Then
            txtUnit.Focus()
        End If
    End Sub
    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If txtOURdb.Text.IndexOf("Password") < 0 Then
            txtOURdb.Text = txtOURdb.Text & " Password="
            txtOURdb.BorderColor = Color.Red
            txtOURdb.Focus()
        Else
            Dim ret As String = "Request to update the Unit. Unit #" & Session("UnitIndx") & " will be updated to the current version of OUReports. Please confirm."
            MessageBox.Show(ret, "Update Unit", "UpdateUnit", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)
        End If

    End Sub

    Private Sub ddUserConnPrv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddUserConnPrv.SelectedIndexChanged
        If ddUserConnPrv.SelectedItem.Text.StartsWith("Intersystems") Then
            txtUserConnStr.Text = "Server = yourserver; Port = yourport; Namespace = yournamespace; User ID = youruser; Password = yourpassword"
        ElseIf ddUserConnPrv.SelectedItem.Text = "Oracle" Then
            txtUserConnStr.Text = "data source=yourserver:yourport/yourdatabase;user id=youruser;password=yourpassword"
        Else
            txtUserConnStr.Text = "Server=yourserver; Database=yourdatabase; User ID=youruser; Password=yourpassword"
        End If
    End Sub

    Private Sub chkTermsAndCond_CheckedChanged(sender As Object, e As EventArgs) Handles chkTermsAndCond.CheckedChanged
        If chkTermsAndCond.Checked Then
            btnSave.Enabled = True
            btnSave.Visible = True
        Else
            btnSave.Enabled = False
            btnSave.Visible = False
        End If
    End Sub
End Class

