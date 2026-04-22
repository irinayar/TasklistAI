<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TeamMemberRegistration.aspx.vb" Inherits="TeamMemberRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Team Member Registration</title>
    <style type="text/css">
        .auto-style1 {
            width: 531px;
            width: 20%;
           
        }
        .auto-style3 {
            height: 29px;
        }
        .auto-style4 {
            width: 68%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" /> 
         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/HelpDesk.aspx">Task List</asp:HyperLink> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Default.aspx">Log off</asp:HyperLink>
        <div align="center">
            <h2>&nbsp;Team Member Registration for <%=Session("UnitName") %></h2>
             <h3><asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label></h3>
        </div>
               <div align="center">
            <table id="tblInput" class="auto-style4" >
                     <tr id="trText1" runat="server"  align="left" hidden="hidden">
                        <td class="auto-style1" >
                            <asp:Label ID="lblText1" runat="server" Text="Logon:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="txtLogon" runat="server" Width="90%" Enabled="False" ReadOnly="True" ></asp:TextBox>
                        </td>
                    </tr>
                   <tr id="trText2" runat ="server"  align="left" >
                        <td class="auto-style1">
                            <asp:Label ID="lblText2" runat="server" Text="Name*:" ToolTip="for information only"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:TextBox ID="txtName" runat="server" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                   <tr id="trText3" runat ="server"  align="left" hidden="hidden">
                        <td class="auto-style1">
                            <asp:Label ID="lblText3" runat="server" Text="Unit:"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:TextBox ID="txtUnit" runat="server" Width="90%" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                <tr id="tr1" runat ="server"  align="left" >
                        <td class="auto-style1">
                            <asp:Label ID="Label1" runat="server" Text="Email*:" ToolTip="It will be used as user logon!"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:TextBox ID="txtEmail" runat="server" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                   <tr id="tr2" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label2" runat="server" Text="Role:"></asp:Label>
                        </td>
                        <td class="td2" >
                            <asp:DropDownList ID="ddRoles" runat="server">
                                <asp:ListItem Value="TOPICADMIN">Topic admin</asp:ListItem>
                                <asp:ListItem Value="user">Team member</asp:ListItem>                               
                            </asp:DropDownList>
                        </td>
                    </tr>
                <tr id="tr3" runat ="server"  align="left" hidden="hidden">
                        <td class="auto-style1">
                            <asp:Label ID="Label3" runat="server" Text="Edit Friendly Names:"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:CheckBox ID="chkFriendlyNames" runat="server" />
                        </td>
                    </tr>
                   <tr id="tr4" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label4" runat="server" Text="Access:"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:DropDownList ID="ddRights" runat="server" Height="26px">                                
                                <asp:ListItem Value="admin">see all team/topic tasks</asp:ListItem>
                                <asp:ListItem Value="user">see his own tasks only</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                <tr id="tr10" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label10" runat="server" Text="Topic:"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:DropDownList ID="ddTopics" runat="server" Height="26px">                                
                                <asp:ListItem Value="All">All</asp:ListItem>                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="tr5" runat="server"  align="left" hidden="hidden">
                        <td class="auto-style1" >
                            <asp:Label ID="Label5" runat="server" Text="Connection String:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="txtConnStr" runat="server" Width="90%" Enabled="False" ReadOnly="True" ></asp:TextBox>
                        </td>
                    </tr>
                     <tr id="tr6" runat="server"  align="left" hidden="hidden">
                        <td class="auto-style1" >
                            <asp:Label ID="Label6" runat="server" Text="Connection Provider:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                                       <asp:DropDownList runat="server" Font-Size="Smaller" ID="ddConnPrv" AutoPostBack="True" Enabled="False" >
                                           <asp:ListItem Value="System.Data.SqlClient">SQL Server</asp:ListItem>
                                           <asp:ListItem Value="MySql.Data.MySqlClient">MySQL</asp:ListItem>
                                           <asp:ListItem Value="Npgsql">PostgreSQL</asp:ListItem>
                                           <asp:ListItem Value="Oracle.ManagedDataAccess.Client">Oracle</asp:ListItem>
                                           <asp:ListItem Value="InterSystems.Data.IRISClient">Intersystems IRIS</asp:ListItem>                                           
                                           <asp:ListItem Value="InterSystems.Data.CacheClient">Intersystems Cache</asp:ListItem>
                                           
                                       </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="tr7" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label7" runat="server" Text="Comments:"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:TextBox ID="txtComments" runat="server" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="tr8" runat ="server"  align="left" hidden="hidden">
                        <td class="auto-style1">
                            <asp:Label ID="Label8" runat="server" Text="Start Date:"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:TextBox ID="txtStartDate" runat="server" Width="25%" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="tr9" runat ="server"  align="left" hidden="hidden">
                        <td class="auto-style1">
                            <asp:Label ID="Label9" runat="server" Text="End Date:"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:TextBox ID="txtEndDate" runat="server" Width="25%" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
              </table>
        <ucMsgBox:Msgbox id="MessageBox" runat ="server" > </ucMsgBox:Msgbox> 
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSave" runat="server" Text="Save" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:Button ID="btnDeleteUser" runat="server" Text="Disable User" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      </div>

    </form>
    <p>
&nbsp;</p>
</body>
</html>
