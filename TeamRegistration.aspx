<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TeamRegistration.aspx.vb" Inherits="TeamRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Team Registration</title>
    <style type="text/css">
        .auto-style1 {
            width: 531px;
            width: 20%;
           
        }
        .auto-style3 {
            height: 29px;
        }
        .auto-style4 {
            width: 86%;
        }
        .auto-style5 {
            width: 531px;
            width: 20%;
            height: 29px;
        }
        .auto-style6 {
            width: 531px;
            width: 20%;
            height: 34px;
        }
        .auto-style7 {
            height: 34px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />   
        <div style="font-size: x-large; font-style: normal; font-weight: bold; background-color: #CCCCCC; text-align:left; height: 40px; line-height:40px; width:100%;">
             <asp:Label ID="LabelPageTtl" runat="server" Text="Project Management"></asp:Label>
         </div>
        <div align="center">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/TeamAdmin.aspx?unitdb=yes" Visible="False" Enabled="False">Users</asp:HyperLink>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; 
        <asp:HyperLink ID="HyperLinkHelp" runat="server" NavigateUrl="~/OnlineUserReporting.pdf" Target="_blank" Enabled="False" Visible="False">Help</asp:HyperLink>&nbsp;&nbsp;
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/index.aspx" Enabled="False" Visible="False">Back</asp:HyperLink>            
         <br />
            <h1>Team Registration</h1>
            <h2><asp:Label ID="Label29" runat="server" Text="Free" Visible="False"></asp:Label></h2>
            <br />
              <asp:Label ID="Label31" runat="server" Font-Italic="True" BackColor="White" ForeColor="#999999" Height="32px" Text="Free Project Management" Font-Bold="True" Font-Names="Tahoma" Font-Size="18px"></asp:Label>
 <br />
            <p>&nbsp;
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       
            <asp:HyperLink ID="HyperLinkClient" runat="server" NavigateUrl="~/ShowBusinessProposal.aspx" Target="_top" Font-Names="Arial" Enabled="False" Visible="False">OUR Business Proposal</asp:HyperLink>
        </p>
                         <br />
        </div>
        <div align="center">
            <table id="tblInput" class="auto-style4" >                    
                    <tr id="tr1" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label30" runat="server" Text="Read*:" ForeColor="#FF3300"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:CheckBox ID="chkTermsAndCond" runat="server" Text=" " AutoPostBack="True" BorderColor="#FF3300" />
            &nbsp;<asp:HyperLink ID="HyperLinkTermsAndCond" runat="server" NavigateUrl="~/TaskListTermsAndConditions.pdf" Target="_blank" Font-Names="Arial" Font-Size="Small">I had read and agreed to Terms and Conditions</asp:HyperLink>
                        </td>
                    </tr>
                    <tr id="trText3" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="lblText3" runat="server" Text="Team name*:" ForeColor="#FF3300"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:TextBox ID="txtUnit" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                <tr id="tr12" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label23" runat="server" Text="Unit contact info:"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:Label ID="Label25" runat="server" Text="unit phone:"></asp:Label>                    
                            
                            <asp:TextBox ID="txtUnitPhone" runat="server" Width="30%"></asp:TextBox>
                            &nbsp;<asp:Label ID="Label26" runat="server" Text="unit email:"></asp:Label>
                            <asp:TextBox ID="txtUnitEmail" runat="server" Width="30%"></asp:TextBox>
                        </td>
                </tr>
                <tr id="tr14" runat ="server"  align="left">
                        <td class="auto-style6">
                            <asp:Label ID="Label27" runat="server" Text="Unit official:"></asp:Label>
                        </td>
                        <td class="auto-style7">
                            <asp:Label ID="Label28" runat="server" Text="title and name:"></asp:Label>  
                            <asp:TextBox ID="txtUnitBossName" runat="server" Width="66%"></asp:TextBox>                          
                        </td>
                </tr>
                <tr id="tr13" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label24" runat="server" Text="Unit address:"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:TextBox ID="txtUnitAddress" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                   
                <tr id="trOURdb" runat="server"  align="left">
                        <td class="auto-style5" >
                            <asp:Label ID="Label1" runat="server" Text="OURdb Connection String:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="txtOURdb" runat="server" Width="98%" ></asp:TextBox>
                        </td>
                    </tr>
                     <tr id="trOURprv" runat="server"  align="left">
                        <td class="auto-style5" >
                            <asp:Label ID="Label2" runat="server" Text="OURdb Connection Provider:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                                       <asp:DropDownList runat="server" Font-Size="Smaller" ID="ddOURConnPrv" AutoPostBack="True" >
                                           <asp:ListItem Value="System.Data.SqlClient">SQL Server</asp:ListItem>
                                           <asp:ListItem Value="Npgsql">PostgreSQL</asp:ListItem>
                                           <asp:ListItem Value="MySql.Data.MySqlClient">MySQL</asp:ListItem>
                                           <asp:ListItem Value="Oracle.ManagedDataAccess.Client">Oracle</asp:ListItem>
                                           <asp:ListItem Value="InterSystems.Data.IRISClient">Intersystems IRIS</asp:ListItem>
                                           <asp:ListItem Value="InterSystems.Data.CacheClient">Intersystems Cache</asp:ListItem>
                                       </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="tr6" runat="server"  align="left">
                        <td class="auto-style5" >
                            <asp:Label ID="Label6" runat="server" Text="Unit Database Connection Provider:" ForeColor="Black"></asp:Label>
                        </td>
                        <td class="auto-style3">
                                       <asp:DropDownList runat="server" Font-Size="Smaller" ID="ddUserConnPrv" AutoPostBack="True" >
                                           <asp:ListItem Value="System.Data.SqlClient">SQL Server</asp:ListItem>
                                           <asp:ListItem Value="Npgsql">PostgreSQL</asp:ListItem>
                                           <asp:ListItem Value="MySql.Data.MySqlClient">MySQL</asp:ListItem>
                                           <asp:ListItem Value="Oracle.ManagedDataAccess.Client">Oracle</asp:ListItem>
                                           <asp:ListItem Value="InterSystems.Data.IRISClient">Intersystems IRIS</asp:ListItem>
                                           <asp:ListItem Value="InterSystems.Data.CacheClient">Intersystems Cache</asp:ListItem>                                           
                                       </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="tr5" runat="server"  align="left">
                        <td class="auto-style5" >
                            <asp:Label ID="Label5" runat="server" Text="Unit Database Connection String:" ForeColor="Black"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="txtUserConnStr" runat="server" Width="98%" ></asp:TextBox>
                        </td>
                    </tr>
                    
                <tr id="tr4" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label4" runat="server" Text="Start Date:"></asp:Label>
                        </td>
                        <td class="td2">
                             <div id="divDate" class="inline" runat="server" align="left" style="width: 195px; padding-left: 5px;" >
                                      <asp:TextBox id="Date1" runat="server" Width="100%" ></asp:TextBox>
                                      <ajaxToolkit:CalendarExtender ID="ceDate1" runat ="server"  TargetControlID="Date1" Format="M/d/yyyy" TodaysDateFormat="M/d/yyyy" />
                             </div>
                        </td>
                    </tr>
                    <tr id="tr8" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label8" runat="server" Text="End Date:"></asp:Label>
                        </td>
                        <td class="td2">
                            <div id="divDate2" class="inline" runat="server" align="left" style="width: 195px; padding-left:5px;" >
                                   <asp:TextBox id="Date2" runat  ="server" Width="100%" ></asp:TextBox>
                                   <ajaxToolkit:CalendarExtender ID="ceDate2" runat ="server"  TargetControlID="Date2" Format="M/d/yyyy" TodaysDateFormat="M/d/yyyy" />
                                </div>
                        </td>
                    </tr>
                    <tr id="tr9" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label11" runat="server" Text="Did OUReports Agent contact you?"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:CheckBox ID="chkOURAgent" runat="server" AutoPostBack="True" Checked="True" Text="yes" />
                            /no&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label13" runat="server" Text="agent name:"></asp:Label>
                            <asp:TextBox id="txtAgentName" runat="server" Width="164px" ></asp:TextBox>
                            <asp:Label ID="Label14" runat="server" Text="cell phone:"></asp:Label>
                            <asp:TextBox id="txtAgentPhone" runat="server" Width="118px" ></asp:TextBox>
                            <asp:Label ID="Label15" runat="server" Text="email:"></asp:Label>
                            <asp:TextBox id="txtAgentEmail" runat="server" Width="201px" ></asp:TextBox>
                        </td>
                    </tr>
                   <tr id="tr10" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label12" runat="server" Text="How did OUReports Agent help you?:"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:DropDownList ID="ddAgentHelps" runat="server" Height="26px">
                                <asp:ListItem Value="web">provided the OUReports.com web address to you</asp:ListItem>
                                <asp:ListItem Value="demo">+supported you through Demo</asp:ListItem>
                                <asp:ListItem Value="sign">+supported you signing the agreement</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                <tr id="trUnitWeb" runat ="server"  align="left" >
                        <td class="auto-style1">
                            <asp:Label ID="lblText2" runat="server" Text="Unit OUReports URL:"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:TextBox ID="txtUnitWeb" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                 <tr id="trModels" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label3" runat="server" Text="Distribution Model:"></asp:Label>
                        </td>
                        <td class="td2">
                             <asp:DropDownList ID="ddModels" runat="server">
                                 <asp:ListItem Value="Unit OUReports on OUR server">Unit OUReports on OUR server (Direct customer)</asp:ListItem>                                                             
                                 <asp:ListItem Value="Unit OUReports on UNIT server">Unit OUReports on UNIT server (Vendor)</asp:ListItem>
                                 <asp:ListItem Value="OURweb-OURdb">OURweb-OURdb (individual users)</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="tr11" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label16" runat="server" Text="Team admin contact info*:" ForeColor="#FF3300"></asp:Label>
                        </td>
                        <td class="td2">                            
                            <asp:Label ID="Label17" runat="server" Text="name:"></asp:Label>&nbsp;<asp:TextBox id="txtName" runat="server" Width="267px" ></asp:TextBox>
                            <asp:Label ID="Label18" runat="server" Text="cell phone:"></asp:Label>
                            <asp:TextBox id="txtPhone" runat="server" Width="166px" ></asp:TextBox>&nbsp;<asp:Label ID="Label19" runat="server" Text="email:"></asp:Label>
                            <asp:TextBox id="txtEmail" runat="server" Width="233px" ></asp:TextBox>
                        </td>
                    </tr>
                     <tr id="trText1" runat="server"  align="left">
                        <td class="auto-style5" >                            
                            <asp:Label ID="lblText1" runat="server" Text="Team admin logon and password*:" ForeColor="#FF3300"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:Label ID="Label20" runat="server" Text="logon:" ToolTip="Best: email or cell phone"></asp:Label>
                            <asp:TextBox ID="txtLogon" runat="server" Width="31%" ToolTip="Best: email or cell phone" ></asp:TextBox>&nbsp;<asp:Label ID="Label21" runat="server" Text="password:"></asp:Label>
                            <asp:TextBox ID="txtPassword" runat="server" Width="19%"  TextMode="Password" ToolTip="password" ></asp:TextBox><asp:Label ID="Label22" runat="server" Text="repeat:"></asp:Label>
                            <asp:TextBox ID="txtRepeat" runat="server" Width="26%"  TextMode="Password" ToolTip="Repeat password" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="tr2" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label32" runat="server" Text="Topic*" ForeColor="#FF3300"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:TextBox ID="txtTopic" runat="server" Width="98%" ToolTip="Team Subject" ForeColor="#666666">topic</asp:TextBox>
                        </td>
                    </tr>
                    <tr id="tr7" runat ="server"  align="left">
                        <td class="auto-style1">
                            <asp:Label ID="Label7" runat="server" Text="Comments:"></asp:Label>
                        </td>
                        <td class="td2">
                            <asp:TextBox ID="txtComments" runat="server" Width="98%"></asp:TextBox>
                        </td>
                    </tr>                   
                    
              </table>
        <ucMsgBox:Msgbox id="MessageBox" runat ="server" > </ucMsgBox:Msgbox> 
        <br />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="Back to Units" Enabled="False" Visible="False" />
    
            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSave" runat="server" Text="Save" />
   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLinkUnitOURWeb" runat="server" NavigateUrl="~/OnlineUserReporting.pdf" Target="_blank" Font-Names="Arial" Font-Size="Small" Visible="False">Unit OUReports</asp:HyperLink>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLinkSeeProposal" runat="server" NavigateUrl="~/OnlineUserReporting.pdf" Target="_blank" Font-Names="Arial" Font-Size="Small" Visible="False">See OUR Business Proposal</asp:HyperLink>
            
            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnUpdate" runat="server" Text="Update to current version" Visible="False" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      </div>

    <p>
        <asp:Label ID="Label9" runat="server" ForeColor="Gray"></asp:Label>
        </p>

        <p>
            &nbsp;</p>
        <p>
            <asp:Label ID="Label10" runat="server" Font-Italic="True" Font-Size="X-Small" Text="Unit index #"></asp:Label>
        </p>

    </form>
    </body>
</html>
