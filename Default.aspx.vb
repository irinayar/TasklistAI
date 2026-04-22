Imports System.Data
Imports System.Data.SqlClient
Imports InterSystems.Data.CacheClient
Partial Class _Default
    Inherits System.Web.UI.Page
    Public myConnection As SqlConnection
    Public myConnect As SqlConnection
    Public myConn As SqlConnection
    Public myConnt As SqlConnection
    Public empID As String
    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not Request("unit") Is Nothing AndAlso Request("unit").ToString.Trim <> "" Then
            Session("Unit") = cleanText(Request("unit").ToString)
        ElseIf Session("Unit") Is Nothing OrElse Session("Unit").ToString.Trim = "" Then
            Session("Unit") = ConfigurationManager.AppSettings("unit").ToString  'default unit
        End If
        unit.Text = Session("Unit").ToString.Trim
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ourdb As String = String.Empty
        Dim ourdbstr As String = System.Configuration.ConfigurationManager.ConnectionStrings.Item("MySqlConnection").ToString
        Dim ourdbpr As String = System.Configuration.ConfigurationManager.ConnectionStrings.Item("MySqlConnection").ProviderName.ToString
        Dim er As String = String.Empty
        'check if ourdb exists
        If Not DatabaseConnected(ourdbstr, ourdbpr, er) Then
            'create new empty ourdb
            Dim ourdbcase As String = ConfigurationManager.AppSettings("ourdbcase").ToString.Trim
            er = CreateNewOURdbOnNewServer(ourdbstr, ourdbpr, ourdbcase)
            If er.Contains("ERROR!!") Then
                MessageBox.Show("Creation of the operational database with connection string from the web.config crashed!  " & er & "  Create the empty operational database with connection string from the web.config manually and try running this page again.", "The operational database is not created - Error", "NoDatabaseError", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Error, Controls_Msgbox.MessageDefaultButton.OK)
                Exit Sub
            End If
            If DatabaseConnected(ourdbstr, ourdbpr, er) Then
                'create db tables
                Try
                    er = UpdateOURdbToCurrentVersion(ourdbstr, ourdbpr)
                Catch ex As Exception
                    er = "ERROR!! " & ex.Message
                End Try
                'If Not er.Contains("ERROR!!") Then
                '    'success!
                '    Response.Redirect("Default.aspx")
                'Else
                '    MessageBox.Show("Creation of the operational database with connection string from the web.config completed with errors! Do not close the page! Contact your database administrator, check permissions and try running this page again.", "The operational database is not created - Error", "NoDatabaseError", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Error, Controls_Msgbox.MessageDefaultButton.OK)
                '    Exit Sub
                'End If
                If er.Contains("ERROR!!") Then
                    MessageBox.Show("Creation of the operational database with connection string from the web.config completed with errors!  " & er & "  Do not close the page! Contact your database administrator, check permissions and try running this page again.", "The operational database is not created - Error", "NoDatabaseError", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Error, Controls_Msgbox.MessageDefaultButton.OK)
                    Exit Sub
                End If
            Else
                MessageBox.Show("Creation of the operational database with connection string from the web.config crashed! " & er & " Do not close the page! Create the empty operational database with connection string from the web.config manually and refresh this page!", "The operational database is not created - Error", "NoDatabaseError", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Error, Controls_Msgbox.MessageDefaultButton.OK)
                Exit Sub
            End If
        End If
        Dim tbl As String = "ourpermits"
        If ourdbpr.StartsWith("InterSystems.Data.") Then
            tbl = CorrectSQLforCache(tbl)
        End If
        If Not TableExists(tbl) Then
            Try
                er = UpdateOURdbToCurrentVersion(ourdbstr, ourdbpr)
            Catch ex As Exception
                er = "ERROR!! " & ex.Message
                MessageBox.Show("Creation of the operational database with connection string from the web.config completed with errors! " & er & " Do not close the page! Contact your database administrator, check permissions and try running this page again.", "The operational database is not created - Error", "NoDatabaseError", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Error, Controls_Msgbox.MessageDefaultButton.OK)
                Exit Sub
            End Try
        End If
        Session("tasklist") = "yes"
        Session("admin") = ""
        Session("logon") = ""
        Session("Application") = ConfigurationManager.AppSettings("ourapplication").ToString
        Session("OURConnString") = ""
        Session("OURConnProvider") = ""
        Session("UserConnString") = ""
        Session("UserConnProvider") = ""
        Session("ChangePassword") = False
        Session("tn") = 0
        Session("UserDB") = ""
        Session("admin") = ""
        Session("Access") = ""
        Session("Group2") = ""
        Session("Topic") = ""
        Session("Version") = ""
        Session("calndr") = ""
        Session("Assignments") = Nothing
        Session("FromSite") = ""
        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        Session("URL") = url.Substring(0, url.LastIndexOf("/")) & "/"
        Session("UnitWEB") = ""
        Dim SiteFor As String = ConfigurationManager.AppSettings("SiteFor").ToString
        'If Not Request("team") Is Nothing AndAlso Request("team").ToString.Trim <> "" Then
        '    Session("Unit") = Request("team").ToString.Trim
        'ElseIf Session("Unit") Is Nothing OrElse Session("Unit").ToString.Trim = "" Then
        '    Session("Unit") = ConfigurationManager.AppSettings("unit").ToString  'default unit
        'End If
        unit.Text = Session("Unit").ToString.Trim
        Session("UnitName") = ""
        Session("UnitIndx") = ""
        LabelVersion.Text = "Version " & ConfigurationManager.AppSettings("version").ToString '& " - " & SiteFor
        Session("Version") = ConfigurationManager.AppSettings("version").ToString
        LblInvalid.Text = SiteFor
        Session("WEBOUR") = ConfigurationManager.AppSettings("webour").ToString
        Session("WEBHELPDESK") = ConfigurationManager.AppSettings("webhelpdesk").ToString
        Session("PAGETTL") = ConfigurationManager.AppSettings("pagettl").ToString

        Session("SupportEmail") = ConfigurationManager.AppSettings("supportemail").ToString
        If Not IsPostBack Then
            Dim retEmail = " Confirmation: copy of your " & SendHTMLEmail("", "Checking if email is working", "Email is working!", "support@oureports.net", Session("SupportEmail"))
            WriteToAccessLog("email", "Got email from " & "support@oureports.net" & " to " & Session("SupportEmail") & ", subject: " & "Checking if email is working" & ", body: " & "Email is working! " & retEmail, 100)
            If retEmail.StartsWith("ERROR!!") Then
                MessageBox.Show(retEmail, "Checking if email is working", "", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Error)
            Else
                'MessageBox.Show(retEmail, "Checking if email is working", "", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Information)
            End If
        End If


        Session("webinstall") = ConfigurationManager.AppSettings("webinstall").ToString
        Session("dbinstall") = ConfigurationManager.AppSettings("dbinstall").ToString
        If Not ConfigurationManager.AppSettings("UnitAuthenticate") Is Nothing Then
            Session("UnitAuthenticate") = ConfigurationManager.AppSettings("UnitAuthenticate").ToString
        Else
            Session("UnitAuthenticate") = "NO"
        End If
        Head1.Title = Session("PAGETTL")
        If Not Session("PAGETTL") Is Nothing AndAlso Session("PAGETTL").ToString.Length > 0 Then
            LabelPageTtl.Text = Session("PAGETTL")
        End If
        Dim ret As String = String.Empty
        'Logged off
        If Not Request("logoff") Is Nothing Then
            ' Session("Application") = "Tasklist"
            Session("logon") = Request("logoff")
            DeleteTempFiles()
            'Session("Application") = ""
            Session("logon") = ""
        End If
        If Not Request("msg") Is Nothing AndAlso Request("msg") = "DemoUserNotChanged" Then
            ret = "Demo user should Not be changed!"
        End If

        If Not Request("msg") Is Nothing AndAlso Request("msg") = "SessionExpired" Then
            ret = "Session Expired ..."
        End If

        If Not Request("msg") Is Nothing AndAlso Request("msg") = "WrongLogonPassword" Then
            ret = "Wrong Logon Or Password ..."
        End If
        If Not Session("PasswordChanged") Is Nothing AndAlso Session("PasswordChanged") <> "" Then
            ret = "Password has been changed ..."
            Session("PasswordChanged") = ""
        End If
        Dim pw As String = Request("pass")
        If pw = "help" AndAlso Not Request("ln") Is Nothing AndAlso Request("ln").Trim <> "" Then
            'assign rights and redirect to HelpDesk
            btLogin_Click(sender, e)
        End If
        unit.Focus()
        If Not Request("logon") Is Nothing AndAlso Request("logon").Trim <> "" Then
            Session("logon") = cleanText(Request("logon"))
            Logon.Text = Session("logon")
            Pass.Focus()
        End If

        If Not Request("tn") Is Nothing AndAlso Request("tn").Trim <> "" Then
            Session("tn") = CInt(cleanText(Request("tn"))).ToString
        End If
        If Not Request("vr") Is Nothing AndAlso Request("vr").Trim <> "" Then
            Session("Version") = cleanText(Request("vr")).Replace("|", " ")
        End If

        If pw <> "" Then
            SetPassword("Pass", pw)
        End If
        'If Not IsPostBack Then
        'Logon.Focus()
        'unit.Focus()
        'End If
        If ret <> String.Empty Then _
            MessageBox.Show(ret, "Page Load ", "Logon", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)

        If Session("logon") = "demo" AndAlso pw = "demo" Then
            btLogin_Click(sender, e)
        End If
        If Session("logon") = "tasklist" AndAlso pw = "test" Then  'AndAlso Session("UnitName") = "TASKLIST1"
            btLogin_Click(sender, e)
        End If


        ''---------------------------------------------------------------------------------------------------------------------------------------
        ''Create db. Uncomment for installation:
        'Session("OURConnString") = System.Configuration.ConfigurationManager.ConnectionStrings.Item("mySQLconnection").ToString
        'Session("OURConnProvider") = System.Configuration.ConfigurationManager.ConnectionStrings.Item("mySQLconnection").ProviderName.ToString
        'ret = UpdateOURdbToCurrentVersion(Session("OURConnString"), Session("OURConnProvider"))
        ''----------------------------------------------------------------------------------------------------------------------------------------

    End Sub
    Private Sub DeleteTempFiles()
        Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() & "Temp\"
        Dim FileSpecs As String = Session("Application") & "_" & Session("logon").ToString & "_*.*"
        DeleteFiles(path, FileSpecs)
    End Sub
    Private Sub btLogin_Click(sender As Object, e As EventArgs) Handles btLogin.Click
        Dim unitname As String = String.Empty
        Dim logon As String = String.Empty
        Dim password As String = String.Empty
        Dim ret As String = String.Empty
        Session("admin") = "user"
        Dim issuper As String = ""
        LblInvalid0.Visible = False
        Dim erro As String = String.Empty
        Try
            'checking texts
            If Not Request("unit") Is Nothing AndAlso Request("unit").Trim <> "" Then
                unitname = cleanText(Request("unit"))
            Else
                unitname = ""
            End If
            Session("UnitName") = unitname
            If Not Request("logon") Is Nothing AndAlso Request("logon").Trim <> "" Then
                logon = cleanText(Request("logon"))
            Else
                logon = ""
            End If
            If Not Request("pass") Is Nothing AndAlso Request("pass").Trim <> "" Then
                password = cleanText(Request("pass"))
            Else
                password = ""
            End If
            password = cleanText(Request("pass"))

            'TODO check texts
            If logon = "" Or password = "" Then
                ret = "Logon And password should Not be empty."
                'LblInvalid.Text = ret
                WriteToAccessLog(Session("logon"), ret, 0)
                Exit Try
            ElseIf logon <> Request("logon").ToString Then
                ret = "Illegal character found In logon."
                'LblInvalid.Text = ret
                WriteToAccessLog(Session("logon"), ret, 0)
                Exit Try
            ElseIf password <> Request("Pass").ToString Then
                ret = "Illegal character found In password."
                'LblInvalid.Text = ret
                WriteToAccessLog(Session("logon"), ret, 0)
                Exit Try
            Else
                ret = FixDatetimeInOURPermits()
                If ret <> "" Then
                    WriteToAccessLog(logon, "DateTime zeros fixed in OURPermits.", 1)
                End If
                'pass checks
                Dim auto As String = "Public"  'autorization 
                Dim auth As Boolean = False   'authentication
                Dim pass As String = Now.ToShortDateString
                Session("OURConnString") = System.Configuration.ConfigurationManager.ConnectionStrings.Item("mySQLconnection").ToString
                Session("OURConnProvider") = System.Configuration.ConfigurationManager.ConnectionStrings.Item("mySQLconnection").ProviderName.ToString

                'authentication  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                auth = False
                If Not Session("UnitAuthenticate") Is Nothing AndAlso Session("UnitAuthenticate") = "OK" Then
                    auth = UnitLogin.UnitAuthenticate(unitname, logon, password, erro)
                Else
                    auth = Login.OURAuthenticate(unitname, logon, password, "OURPermits", issuper, erro)
                End If


                If (auth) Then   'authenticated
                    Dim mSQL As String = String.Empty
                    Dim dvprm As DataView
                    'check and save the user connection
                    Dim userconnstr As String = String.Empty
                    Dim userconnprv As String = String.Empty
                    If Not Request("ConnStr") Is Nothing AndAlso cleanText(Request("ConnStr")) <> Request("ConnStr").ToString.Trim Then
                        ret = "Illegal character found. Please retype Connection String."
                        Exit Try
                    End If
                    If Not Request("ConnStr") Is Nothing AndAlso cleanText(Request("ConnStr")) = Request("ConnStr").ToString.Trim AndAlso cleanText(Request("ConnStr")).Length > 0 Then
                        userconnstr = Request("ConnStr").ToString.Trim
                        Dim userconnstrnouser As String = String.Empty
                        If userconnstr.ToUpper.IndexOf("PASSWORD") > 0 Then
                            userconnstrnouser = userconnstr.Substring(0, userconnstr.ToUpper.IndexOf("PASSWORD"))
                        End If
                        If userconnstr.ToUpper.IndexOf("PWD") > 0 Then
                            userconnstrnouser = userconnstr.Substring(0, userconnstr.ToUpper.IndexOf("PWD"))
                        End If
                        If userconnstrnouser.ToUpper.IndexOf("USER ID") > 0 Then
                            userconnstrnouser = userconnstrnouser.Substring(0, userconnstrnouser.ToUpper.IndexOf("USER ID"))
                        End If

                        mSQL = "SELECT * FROM OURPermits WHERE (Unit='" & unitname & "' AND NetId='" & logon & "' AND localpass='" & password & "' AND ConnStr LIKE '%" & userconnstrnouser.Trim.Replace(" ", "%") & "%')"
                        dvprm = mRecords(mSQL, erro)
                        If Not dvprm Is Nothing AndAlso dvprm.Count > 0 AndAlso dvprm.Table.Rows.Count = 1 Then
                            userconnprv = dvprm.Table.Rows(0)("ConnPrv").ToString.Trim
                        End If
                        Session("UserConnString") = userconnstr
                        Session("UserConnProvider") = userconnprv
                        Session("Unit") = dvprm.Table.Rows(0)("Unit").ToString.Trim
                        Session("Access") = dvprm.Table.Rows(0)("Access").ToString.Trim
                        Session("Group2") = dvprm.Table.Rows(0)("Group2").ToString.Trim
                        'Session("Application") = "Tasklist"
                        Session("UnitName") = Session("Unit")
                        If userconnstr.ToUpper.IndexOf("SERVER") < 0 AndAlso userconnstr.ToUpper.IndexOf("DATA SOURCE") < 0 Then
                            ret = "Server/Data Source has not been found In the connection String. Please correct Connection String."
                            Exit Try
                        End If
                        If userconnstr.ToUpper.IndexOf("USER ID") < 0 AndAlso userconnstr.ToUpper.IndexOf("UID") < 0 Then
                            ret = "User ID has Not been found In the connection String. Please correct Connection String."
                            Exit Try
                        End If
                        If userconnstr.ToUpper.IndexOf("PASSWORD") < 0 AndAlso userconnstr.ToUpper.IndexOf("PWD") < 0 Then
                            ret = "Password has Not been found In the connection String. Please correct Connection String."
                            Exit Try
                        End If

                        If CheckBox1.Checked Then
                            'save Connection info 
                            userconnstr = Session("UserConnString")
                            userconnprv = Session("UserConnProvider")
                            'remove password
                            If userconnstr.ToUpper.IndexOf("PASSWORD") > 0 Then
                                userconnstr = userconnstr.Substring(0, userconnstr.ToUpper.IndexOf("PASSWORD"))
                            End If
                            If userconnstr.ToUpper.IndexOf("PWD") > 0 Then
                                userconnstr = userconnstr.Substring(0, userconnstr.ToUpper.IndexOf("PWD"))
                            End If
                            'update OURPERMITS
                            ret = ExequteSQLquery("UPDATE OURPERMITS Set ConnStr='" & userconnstr & "',ConnPrv='" & userconnprv & "' WHERE NetID='" & Session("logon") & "' AND Application='" & Session("Application").ToString.Trim & "'")
                            If ret = "Query executed fine." Then
                                WriteToAccessLog(Session("logon"), "ConnStr updated: " & userconnstr, 1)
                                ret = ""
                            Else
                                Exit Try
                            End If
                        End If
                    ElseIf cleanText(Request("ConnStr")) = "" Then
                        If Not System.Configuration.ConfigurationManager.ConnectionStrings.Item("userSQLconnection") Is Nothing Then
                            'connection to user database from web.config
                            userconnstr = System.Configuration.ConfigurationManager.ConnectionStrings.Item("userSQLconnection").ToString
                            userconnprv = System.Configuration.ConfigurationManager.ConnectionStrings.Item("userSQLconnection").ProviderName.ToString
                            Session("UserConnString") = userconnstr
                            Session("UserConnProvider") = userconnprv
                        Else 'connection to user database from OURPermits
                            Dim dv As DataView = mRecords("SELECT * FROM OURPERMITS WHERE NetID='" & Session("logon") & "' AND Application='" & Session("Application").ToString.Trim & "'")
                            If Not IsDBNull(dv.Table.Rows(0)("ConnStr")) Then
                                userconnstr = cleanText(dv.Table.Rows(0)("ConnStr"))
                            End If
                            If Not IsDBNull(dv.Table.Rows(0)("ConnPrv")) Then
                                userconnprv = cleanText(dv.Table.Rows(0)("ConnPrv"))
                            End If
                            Session("UserConnString") = userconnstr
                            Session("UserConnProvider") = userconnprv
                            If userconnstr = "" OrElse userconnprv = "" Then
                                ret = "Connection string to user database has not been found. Please enter proper Connection String and select Database type."
                            ElseIf Session("logon") = "demo" Then
                                userconnstr = userconnstr & " Password=Demo!@#4"
                                Session("UserConnString") = userconnstr
                                Session("UserConnProvider") = userconnprv
                            Else
                                LblInvalid.Text = "Add password to your database into Connection String."
                                Dim userconnstrnopass As String = userconnstr
                                If userconnstrnopass.IndexOf("Password") > 0 Then
                                    userconnstrnopass = userconnstrnopass.Substring(0, userconnstrnopass.IndexOf("Password")).Trim
                                End If
                                ConnStr.Text = userconnstrnopass & " Password="
                                dropdownDatabases.SelectedValue = userconnprv
                                trConnection.Visible = True
                                trProvider.Visible = True
                                btUserConnection.Visible = False
                                Exit Try
                            End If
                            Session("Unit") = dv.Table.Rows(0)("Unit").ToString.Trim
                            Session("Access") = dv.Table.Rows(0)("Access").ToString.Trim
                            Session("Group2") = dv.Table.Rows(0)("Group2").ToString.Trim
                            'Session("Application") = "Tasklist"
                            Session("UnitName") = Session("Unit")
                        End If
                    End If



                    'autorization !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    'check if first login
                    Dim userEmail As String = String.Empty
                    Dim isTeamAdmin As String = String.Empty
                    Dim Group2 As String = String.Empty
                    If issuper = "super" Then
                        auto = "super"
                        isTeamAdmin = "TEAMADMIN"
                    ElseIf Session("UnitAuthenticate") <> "OK" Then
                        auto = Login.OURAuthorize(unitname, "OURPermits", logon, password, Session("Application").ToString, "", userconnstr, userconnprv, userEmail, issuper, isTeamAdmin, Group2)
                    Else
                        'sample of custom autorization if needed
                        auto = UnitLogin.UnitAuthorize(True, Session("Unit"), "OURPermits", "RoleApp", "Email", "NetId", "Localpass", logon, password, userEmail)
                    End If
                    If auto = "super" OrElse auto = "admin" OrElse auto = "user" OrElse auto = "public" Then
                        Session("admin") = auto
                        Session("logon") = logon
                        If auto = "public" Then Session("logon") = "public"
                        'Session("Application") = "Tasklist"
                        Session("UserConnString") = userconnstr
                        Session("UserConnProvider") = userconnprv
                        Session("Unit") = unitname
                        Session("UnitName") = Session("Unit")
                        Session("Access") = isTeamAdmin
                        Session("Group2") = Group2

                        If userEmail = password Then
                            WriteToAccessLog(Session("logon"), "First time login successful", 0)
                            Session("ChangePassword") = True
                            'Dim url As String = "confirm.aspx?unit=" & Session("Unit") & "&connstr=" & Session("OURConnString") & "&connprv=" & Session("OURConnProvider") & "&email=" & userEmail
                            Dim url As String = "confirm.aspx?unit=" & Session("Unit") & "&connstr=" & Session("UserConnString") & "&connprv=" & Session("UserConnProvider") & "&email=" & userEmail
                            Response.Redirect(url)
                        Else
                            WriteToAccessLog(Session("logon"), "Login successful", 0)
                            'ret = FixDatetimeInOURPermissions()
                            If ret <> "" Then
                                WriteToAccessLog(logon, "DateTime zeros fixed in OURPermissions.", 1)
                            End If
                        End If
                        'check connection to user database
                        Dim er As String = String.Empty
                        If DatabaseConnected(Session("UserConnString"), Session("UserConnProvider"), er) Then
                            'Response.Redirect("ListOfReports.aspx")
                            DeleteTempFiles()
                            Response.Redirect("HelpDesk.aspx")
                        Else
                            ret = "Connection to user database can not be open..."
                            Exit Try
                            WriteToAccessLog(logon, "Connection to database cannot be open.", 1)
                        End If
                    Else
                        ret = "You do not have permissions to use this system... "
                        'LblInvalid.Text = ret
                        WriteToAccessLog(Session("logon"), ret, 0)
                    End If
                Else
                    ret = "User was not found. Wrong logon or password..."
                    'LblInvalid.Text = ret
                    WriteToAccessLog(Session("logon"), ret & erro, 0)
                End If
            End If
        Catch ex As Exception
            ret = ret & "  " & ex.Message
        End Try
        If ret <> String.Empty Then
            MessageBox.Show(ret, "Logon ", "Logon", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)
            'LblInvalid.Text = ret
            WriteToAccessLog(Session("logon"), ret, 2)
        End If
    End Sub
    Protected Sub btRegister_Click(sender As Object, e As EventArgs) Handles btRegister.Click
        Response.Redirect("TeamRegistration.aspx")
    End Sub

    Protected Sub btForgot_Click(sender As Object, e As EventArgs) Handles btForgot.Click
        Dim ret As String = String.Empty
        Dim useremail As String = String.Empty
        Dim passwd As String = String.Empty
        If Session("logon") = "" Then
            ret = "Request to Forgotten Password. Logon should not be empty."
            'LblInvalid.Text = ret
            MessageBox.Show(ret, "Forgotten Password", "ForgottenPassword", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)
            ret = WriteToAccessLog(Session("logon"), ret, 0)
            'Logon.Focus()
            Exit Sub
        Else
            Dim listofpermits As DataView
            listofpermits = mRecords("SELECT * FROM OURPERMITS WHERE (NetId='" & Session("logon") & "')  AND (Application='" & Session("Application").ToString.Trim & "')")
            If listofpermits Is Nothing OrElse listofpermits.Table Is Nothing OrElse listofpermits.Table.Rows.Count <> 1 Then
                ret = "Request to Forgotten Password. Wrong Logon."
                MessageBox.Show(ret, "Forgotten Password", "ForgottenPassword", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)
                ret = WriteToAccessLog(Session("logon"), ret, 0)
                Exit Sub
            End If
            If Not IsDBNull(listofpermits.Table.Rows(0)("Email")) Then
                useremail = listofpermits.Table.Rows(0)("Email").ToString
                passwd = listofpermits.Table.Rows(0)("localpass").ToString
            Else
                ret = "Request to Forgotten Password. Email cannot be empty."
                MessageBox.Show(ret, "Forgotten Password", "ForgottenPassword", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)
                ret = WriteToAccessLog(Session("logon"), ret, 0)
                Exit Sub
            End If
            'TODO make temp. password
            ret = SendHTMLEmail("", "Password Reminder", "This email has been sent by OUR Project Management in response to your request to remind you the password. Use temporary password  " & passwd & ". You must change it in your first login.", useremail, Session("SupportEmail"))
            MessageBox.Show(ret, "Forgotten Password", "ForgottenPassword", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)
        End If
    End Sub
    Protected Sub btChange_Click(sender As Object, e As EventArgs) Handles btChange.Click
        Dim ret As String = ""
        If Request("logon") = "" Then
            ret = "Request to change password. Logon should not be empty."
            MessageBox.Show(ret, "Change Password", "ChangePassword", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)
            ret = WriteToAccessLog(Session("logon"), ret, 0)
            Return
        End If
        Dim sql As String = "Select * From OURPermits Where NetId='" & Request("logon") & "'"
        Dim err As String = ""
        Dim dv As DataView = mRecords(sql, err)  'from OUR db
        If err <> "" OrElse dv Is Nothing OrElse dv.Table Is Nothing OrElse dv.Table.Rows.Count = 0 Then
            If err = "" Then
                ret = "Request to change password. User not found..."
                MessageBox.Show(ret, "Change Password", "ChangePassword", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)

            Else
                ret = "Request to change password. Error occurred: " & err
                MessageBox.Show(ret, "Change Password", "ChangePassword", Controls_Msgbox.Buttons.OK, Controls_Msgbox.MessageIcon.Warning, Controls_Msgbox.MessageDefaultButton.PostOK)
            End If
            ret = WriteToAccessLog(Session("logon"), "Request to change password. " & ret, 0)
            Logon.Text = ""
            Logon.Focus()
            Return
        End If
        Session("ChangePassword") = True
        Dim tbl As DataTable = dv.Table
        Dim pswd As String = tbl.Rows(0)("localpass").ToString
        Dim sUnit As String = tbl.Rows(0)("Unit").ToString
        Dim connstr As String = tbl.Rows(0)("Connstr").ToString
        Dim connprv As String = tbl.Rows(0)("ConnPrv").ToString
        Dim email As String = tbl.Rows(0)("Email").ToString
        Dim url As String = "confirm.aspx?unit=" & sUnit & "&connstr=" & connstr & "&connprv=" & connprv & "&email=" & email

        Response.Redirect(url)

    End Sub
    Protected Sub dropdownDatabases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropdownDatabases.SelectedIndexChanged
        If dropdownDatabases.Text.StartsWith("Intersystems") Then
            dropdownDatabases.ToolTip = "Server = yorserver; Port = yourportas1972; Namespace = yournamespace; User ID = youruser; Password = yourpassword"
        ElseIf dropdownDatabases.Text = "Oracle" Then
            dropdownDatabases.ToolTip = "data source=yourserver:yourport/yourdatabase;user id=youruser;password=yourpassword"
        Else
            dropdownDatabases.ToolTip = "Server=yourserver; Database=yourdatabase; User ID=youruser; Password=yourpassword"
        End If
        ConnStr.ToolTip = dropdownDatabases.ToolTip
    End Sub

    Private Sub btUserConnection_Click(sender As Object, e As EventArgs) Handles btUserConnection.Click
        trProvider.Visible = True
        trConnection.Visible = True
        trSaveConnection.Visible = True
        btUserConnection.Visible = False
        'Logon.Focus()
        unit.Focus()
    End Sub
    Sub SetPassword(id As String, psword As String)
        Dim sb As New System.Text.StringBuilder("")
        Dim cs As ClientScriptManager = Me.ClientScript
        With sb
            .Append("<script language='JavaScript'>")
            .Append("function SetPassword()")
            .Append("{")
            .Append("var txt = document.getElementById('" & id & "');")
            .Append("txt.value='" & psword & "';")
            .Append("var MyControl = document.getElementById('ConnStr');")
            .Append("if (MyControl != null)")
            .Append("{")
            .Append("MyControl.focus();")
            .Append("if (MyControl.createTextRange)")
            .Append("{")
            .Append("var range = MyControl.createTextRange();")
            .Append("range.moveStart('character',MyControl.value.length);")
            .Append("range.collapse(false);")
            .Append("range.select();")
            .Append("}")
            .Append("else")
            .Append("{")
            .Append("MyControl.selectionStart=MyControl.selectionEnd=MyControl.value.length;")
            .Append("}")

            .Append("}")
            .Append("}")
            .Append("window.onload = SetPassword;")
            .Append("</script>")
        End With
        cs.RegisterStartupScript(Me.GetType, "SetPassword", sb.ToString())
    End Sub
    Private Sub ClearText(CtlId As String)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType(), CtlId, "javascript:ClearTextbox('" & CtlId & "')", True)
    End Sub
    Private Sub MessageBox_MessageResulted(sender As Object, e As Controls_Msgbox.MsgBoxEventArgs) Handles MessageBox.MessageResulted
        If e.Tag = "ForgottenPassword" OrElse e.Tag = "Logon" OrElse e.Tag = "ChangePassword" Then

            Response.Redirect("Default.aspx")
        End If
    End Sub
    Private Sub lnkDemo_Click(sender As Object, e As EventArgs) Handles lnkDemo.Click
        'Threading.Thread.Sleep(1000)
        Response.Redirect(Session("WEBOUR").ToString & "Default.aspx?logon=demo&pass=demo")
    End Sub
    Protected Sub lnkPDF_Click(sender As Object, e As EventArgs) Handles lnkPDF.Click
        Response.Redirect(Session("WEBOUR").ToString & "OnlineUserReporting.pdf#page=4")
    End Sub
    Private Sub TreeView1_SelectedNodeChanged(sender As Object, e As EventArgs) Handles TreeView1.SelectedNodeChanged
        Dim node As WebControls.TreeNode = TreeView1.SelectedNode
        Dim url As String = node.Value
        Response.Redirect(url)
    End Sub

End Class

