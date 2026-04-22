Imports System.Data
Imports System.Data.SqlClient

Partial Class TeamAdmin
    Inherits System.Web.UI.Page

    Private Sub TeamAdmin_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim i As Integer = 0
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
                    ddTopics.Items.Add("All")
                    ddTopics.SelectedIndex = 0
                    ddTopics.Text = "All"
                    For i = 0 To du.Table.Rows.Count - 1
                        ddTopics.Items.Add(du.Table.Rows(i)("Prop2").ToString.Trim)
                        If Session("Topic").Trim = du.Table.Rows(i)("Prop2").ToString.Trim Then
                            ddTopics.SelectedIndex = i
                            ddTopics.Text = du.Table.Rows(i)("Prop2").ToString
                        End If
                    Next
                Else
                    Session("Topic") = Session("Group2").ToString.Trim
                    ddTopics.Items.Add(Session("Topic"))
                    ddTopics.SelectedIndex = 0
                    ddTopics.Text = Session("Topic")
                    ddTopics.Enabled = False
                    lbNewTopic.Visible = False
                    txtTopic.Visible = False
                    txtTopic.Enabled = False
                    btAddTopic.Visible = False
                    btAddTopic.Enabled = False
                End If
            End If
        End If
    End Sub
    Private Sub TeamAdmin_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session Is Nothing OrElse Session("admin") Is Nothing OrElse Session("admin").ToString = "" Then
            Response.Redirect("~/Default.aspx?msg=SessionExpired")
        End If
        Dim er As String = String.Empty
        Dim mSql As String = String.Empty
        Dim userconnstrnopass As String = Session("UserConnString")
        If userconnstrnopass.IndexOf("Password") > 0 Then userconnstrnopass = userconnstrnopass.Substring(0, userconnstrnopass.IndexOf("Password")).Trim()
        Dim srch As String = SearchText.Text.Trim

        If (Session("admin") = "admin" OrElse Session("admin") = "super") AndAlso (Session("Access").ToString.Trim = "TEAMADMIN" Or Session("Access").ToString.Trim = "TOPICADMIN") Then
            mSql = "SELECT Unit AS Team,Group2 AS Topic,Name,Email,[Access] as Roles,RoleApp as Rights,NetId as Logon,Comments,Indx FROM OURPermits WHERE (Unit='" & Session("Unit") & "') AND (ConnStr LIKE '%" & userconnstrnopass.Trim.Replace(" ", "%") & "%') AND (Application='" & Session("Application").ToString.Trim & "') " 'AND (Group2='" & Session("Topic") & "')"
        End If
        If ddTopics.Text <> "All" Then
            mSql = mSql & " AND Group2='" & ddTopics.Text.Trim & "' "
        End If
        If srch <> "" Then
            mSql = mSql & " AND ((NetId LIKE '%" & srch & "%') OR (Name LIKE '%" & srch & "%') OR (Unit LIKE '%" & srch & "%') OR (Email LIKE '%" & srch & "%') OR (Comments LIKE '%" & srch & "%')) "
        End If
        mSql = mSql & "  ORDER BY Unit,Group2,Name "

        'Dim db As String = GetDataBase(Session("OURConnString"), Session("OURConnProvider"))
        'mSql = mSql.Replace("FROM OURPermits ", "FROM " & db & ".ourpermits ")

        Dim dvu As DataView = mRecords(mSql, er)

        'If dvu Is Nothing OrElse dvu.Table Is Nothing Then
        '    WriteToAccessLog(Session("logon"), "Users are not returned: " & er & ", SQL query: " & mSql, 3)
        'Else
        '    WriteToAccessLog(Session("logon"), "Users return: " & er & " " & dvu.Table.Rows.Count.ToString & ", SQL query: " & mSql, 3)
        'End If


        Dim i As Integer = 0
        For i = 0 To dvu.Table.Rows.Count - 1
            'Roles
            If dvu.Table.Rows(i)("Roles").ToString = "TEAMADMIN" Then
                dvu.Table.Rows(i)("Roles") = "Team admin - can start new topic, can add topic members"
            ElseIf dvu.Table.Rows(i)("Roles").ToString = "TOPICADMIN" Then
                dvu.Table.Rows(i)("Roles") = "Topic admin - cannot start new topic, can add topic members"
            Else
                dvu.Table.Rows(i)("Roles") = "Team member - cannot start new topic, cannot add topic members"
            End If
            'Rights
            If dvu.Table.Rows(i)("Rights").ToString = "admin" Then
                dvu.Table.Rows(i)("Rights") = "Access to all team/topic tasks"
            Else
                dvu.Table.Rows(i)("Rights") = "Access to his own tasks only"
            End If
        Next

        GridView1.DataSource = dvu
        GridView1.Visible = True
        GridView1.DataBind()
    End Sub
    Private Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        GridView1.DataBind()
    End Sub

    Private Sub btnRegistration_Click(sender As Object, e As EventArgs) Handles btnRegistration.Click
        Response.Redirect("TeamMemberRegistration.aspx")
    End Sub
    Protected Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        'Dim srch As String = SearchText.Text.Trim
        'If srch = "" Then
        '    Exit Sub
        'End If

    End Sub
    Protected Sub btAddTopic_Click(sender As Object, e As EventArgs) Handles btAddTopic.Click
        If txtTopic.Text.Trim <> cleanText(txtTopic.Text.Trim) Then
            'LblInvalid.Text = ret
            WriteToAccessLog(Session("logon"), "Illegal character found in topic name.", 0)
            txtTopic.Text = "Illegal character found in topic name!"
            Exit Sub
        ElseIf txtTopic.Text = "Illegal character found in topic name!" Then
            txtTopic.Text = ""
            Exit Sub
        ElseIf txtTopic.Text = "This topic is already there!" Then
            txtTopic.Text = ""
            Exit Sub
        Else
            'insert in OURunits
            Dim ret As String = String.Empty
            If Not HasRecords("SELECT * FROM ourunits WHERE Prop1='team' AND Unit='" & Session("Unit") & " AND Topic='" & txtTopic.Text.Trim & "'") Then
                Dim dt As DataTable = mRecords("SELECT * FROM ourunits WHERE  Prop1='team' AND Unit='" & Session("Unit") & "'").Table
                Dim mRow As DataRow
                mRow = dt.Rows(0)
                dt.Rows(0)("Prop2") = txtTopic.Text.Trim
                'insert in OURunits
                ret = InsertRowIntoTable("ourunits", mRow, dt, Session("OURConnString"), Session("OURConnProvider"), Session("UserConnString"), Session("UserConnProvider"))
                Session("Topic") = txtTopic.Text.Trim
            Else
                txtTopic.Text = "This topic is already there!"
                WriteToAccessLog(Session("logon"), "This topic is already there: " & txtTopic.Text.Trim, 0)
                Exit Sub
            End If
        End If
        Response.Redirect("TeamAdmin.aspx")
    End Sub

    Private Sub ddTopics_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddTopics.SelectedIndexChanged
        Session("Topic") = ddTopics.SelectedValue
        Session("TopicIndx") = ddTopics.SelectedIndex
        Response.Redirect("~/TeamAdmin.aspx")
    End Sub

End Class
