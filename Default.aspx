<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

  
<head id="Head1" runat="server">
    <title>Project Management</title> 
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
        .auto-style2 {
            height: 30px;
            width: 320px;
        }
        .auto-style5 {
            width: 100%;
        }
        .auto-style6 {
            margin-bottom: 0px;
        }
        .auto-style7 {
            width: 160px;
        }
        .auto-style8 {
            height: 30px;
            width: 250px;
        }
.modal
{
    position: fixed;
    z-index: 2147483647;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: #f8f8d3;
    opacity: 0.8;
}
.center
{
    z-index: 2147483647;
    margin: 300px auto;
    padding-left:25px;
    padding-top:10px;
    width: 130px;
    background-color:#f8f8d3;
    border-radius: 10px;
}
.center img
{
    height: 100px;
    width: 100px;
}
.demo {
  background-color: transparent; 
  color:blue;
  font-family: Arial; 
  font-size: 20px; 
  font-style: normal; 
  font-variant: normal; 
  font-weight: 400; 
  letter-spacing: normal; 
  orphans: 2; 
  text-align: left; 
  text-decoration: underline; 
  text-indent: 0px; 
  text-transform: none; 
  -webkit-text-stroke-width: 0px; 
  white-space: normal;
  word-spacing: 0px;  
}

        .auto-style9 {
            position: fixed;
            z-index: 2147483647;
            height: 100%;
            width: 100%;
            top: -253px;
            opacity: 0.8;
            left: -3px;
            background-color: #f8f8d3;
        }

.NodeStyle
{
    color: #0066FF;
    font-size:12px;
    font-weight:normal;
    text-decoration:none;
}
.NodeStyle:hover
{
    text-decoration:underline;
    color:darkblue;
}
    </style>
</head>

<body>

 <div>
       
         <form id="frmLogon" runat="server">
       
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />      
        <asp:UpdatePanel ID="udpDefault" runat ="server" >
            <ContentTemplate>               
                 <script type="text/javascript" src="Controls/Javascripts/OUR.js"></script>
                        <div>
           <table>
      <tr>
           <td colspan="3" style="font-size:x-large; font-style:normal; font-weight:bold; background-color: #CCCCCC; vertical-align:middle; text-align: left; height: 40px;">
              <asp:Label ID="LabelPageTtl" runat="server" Text="Project Management"></asp:Label>
          </td>
      </tr> 
        <tr>
            <td style="font-size: x-small; font-style: normal; font-weight: normal; background-color: #CCCCCC; vertical-align: top; text-align: left; width: 15%;">
                    <div id="tree" style="font-size: x-small; font-weight: normal; font-style: normal">
          
        <asp:TreeView ID="TreeView1"  runat="server" Width="100%" NodeIndent="10" Font-Names="Times New Roman"  EnableTheming="True" ImageSet="BulletedList">
          <Nodes>  
                 
            <asp:TreeNode Text="&lt;b&gt;Provider: DataAI.link&lt;/b&gt;"  Value="http://DataAI.link" ></asp:TreeNode>           

            <asp:TreeNode Text="&lt;b&gt;Documentation&lt;/b&gt;"  Value="~/TaskList.pdf" Expanded="True" ></asp:TreeNode>

          <%--  
<asp:TreeNode Text="&lt;b&gt;Demo&lt;/b&gt;"  Value="https://oureports.net/HelpDesk/Default.aspx?pass=test&logon=tasklist&unit=TASKLIST" Expanded="True" > </asp:TreeNode>

          --%>
<asp:TreeNode Text="&lt;b&gt;Demo&lt;/b&gt;"  Value="https://oureports.net/TaskList/Default.aspx?pass=test&logon=tasklist&unit=TASKLIST" Expanded="True" > </asp:TreeNode>
       
            <asp:TreeNode Text="&lt;b&gt;Contact us&lt;/b&gt;" Expanded="True" Value="ContactUs.aspx"></asp:TreeNode>
        </Nodes>
       <RootNodeStyle HorizontalPadding="2px" Font-Bold="True" />
       <NodeStyle CssClass="NodeStyle" />
       <ParentNodeStyle Font-Bold="True" />
     </asp:TreeView>
     
    </div>
            </td>
            <td width="5px"></td>
   <td id="main" style="width: 85%; text-align: left; vertical-align: top"> 
    <div style="text-align: center;width:100%">

                     <br />
                     <asp:Label ID="Label1" runat="server" Font-Italic="True" ForeColor="#999999" Height="22px" Text="It is time to put the Internet to work making the creation and processing of custom reports convenient, simple, and accessible for end users and administrators." ToolTip="It is time to put the Internet to work making the creation and processing of custom reports convenient, simple, and accessible for end users and administrators.
At OUReports, we can serve organizations from our cloud Web server or by installing our Web application on their own Web server. 
Our system requires only restricted access(reading permissions) to the database of the organization we are serving. Our application automatically analyzes data structure, generates a set of preliminary reports, and provides a simple interface for creating ad hoc reports and conducting statistical research. 
Any organization storing data in SQL Server, MySQL, or Cache Intersystems databases can use our system to quickly and easily generate  fast highly informational and statistical reports." Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Visible="False"></asp:Label>
                    
               <span style ="font-family:Tahoma;font-size:18px;font-weight:bold;color:Red" >
                    <asp:Label ID="LblInvalid0" runat="server"  Height="25px" Width="800px" ForeColor="#CC3300"></asp:Label>
              </span>                                 
                <br />
                    

                   <br/>
                <br />
                <asp:Label ID="LblInvalid" runat="server"  Height="25px" Width="800px" Font-Bold="True" Font-Names="Tahoma" Font-Size="20px" ForeColor="#CC3300"></asp:Label>&nbsp;
                 <br />
        <br />
              <asp:Label ID="Label31" runat="server" Font-Italic="True" BackColor="White" ForeColor="#999999" Height="32px" Text="Free Team Project Management" Font-Bold="True" Font-Names="Tahoma" Font-Size="18px"></asp:Label>
                 <br />
              </span>  
             <asp:LinkButton ID="lnkDemo" runat="server" TabIndex="-1" ToolTip="Demonstration Reports" Text="DEMO" CssClass="demo" Visible="false"></asp:LinkButton> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;    
             <asp:LinkButton ID="lnkPDF" runat="server" TabIndex="-1" ToolTip="Instruction, Documentation" Text="Introduction and Documentation" CssClass="demo" Visible="false"></asp:LinkButton> 
         <table border="0" cellpadding="1" cellspacing="0" width="100%">
              <tr>                                
                     <td align="center" colspan="2"> 
                                        <br />
                                        <asp:LinkButton ID="btRegister" runat="server" TabIndex="-1" ToolTip ="Register new team first." CssClass="NodeStyle" Font-Names="Tahoma" Font-Size="Small" Font-Bold="False" Font-Italic="True" Font-Underline="True">Register new team</asp:LinkButton>
                                  <br />
                     </td>
             </tr>  
             
             <tr>
                    <td align="center">
                        <h4 style="font-family: Arial">Please enter your logon and password:&nbsp; </h4>
                    </td>
              </tr>
              
              <tr>
                   <td align="center">                                      
                        <table border="0" cellpadding="0" cellspacing="0" class="auto-style5">
                              <tr style="border-color:#ffffff">
                                  <td  align="right" class="auto-style8">
                                      <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="12px" ForeColor="#CC3300" Text="Team*:&nbsp;"></asp:Label>
                                  </td>
                                  <td class="auto-style1">
                                      <asp:TextBox ID="unit" runat="server" TabIndex="0" CssClass="auto-style6" Width="161px"> </asp:TextBox>
                                      &nbsp;&nbsp;
                                     
                                  </td>
                             </tr>  

                             <tr style="border-color:#ffffff">
                                  <td  align="right" class="auto-style8">
                                      <asp:Label ID="lblLogOn" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="12px" ForeColor="#CC3300" Text="Logon*:&nbsp;"></asp:Label>
                                  </td>
                                  <td class="auto-style1">
                                      <asp:TextBox ID="Logon" runat="server" TabIndex="0" CssClass="auto-style6" Width="161px"></asp:TextBox>
                                      &nbsp;&nbsp;
                                      <asp:Linkbutton ID="btUserConnection" runat="server" tabindex="-1" Tooltip= "Define connection to user's database." text="Define User Connection" CssClass="NodeStyle" Font-Names="Tahoma" Font-Size="12px" Enabled="False" Visible="False" />
                                  </td>
                             </tr>                    
                       
                             <tr style="border-color:#ffffff">
                                  <td align="right"class="auto-style8">
                                       <span style ="font-family:Tahoma;font-size:12px; font-weight: bold; color: #CC3300;">&nbsp;Password*:&nbsp;</span></td>
                                  <td class="auto-style1">
                                       <input  name="Pass" id="Pass" runat="server" type="password" class="auto-style7"/> &nbsp;&nbsp;
                                     
                                  </td>
                             </tr>
                             <tr id="trProvider" runat="server" visible="false" style="border-color:#ffffff;">
                                  <td align="right" class="auto-style1">
                                       <span style ="font-family:Tahoma;font-size:12px; font-weight: bold; color: #CC3300;">User Database provider*:&nbsp;</span>
                                  </td>
                                  <td align ="left" class="auto-style8">
                                       <asp:DropDownList runat="server" Font-Size="Smaller" ID="dropdownDatabases" Width="165px" >
                                           <asp:ListItem Selected="True" Value="System.Data.SqlClient">SQL Server</asp:ListItem>
                                           <asp:ListItem Value="MySql.Data.MySqlClient">MySQL</asp:ListItem>
                                           <asp:ListItem Value="Npgsql">PostgreSQL</asp:ListItem>
                                           <asp:ListItem Value="Oracle.ManagedDataAccess.Client">Oracle</asp:ListItem>
                                           <asp:ListItem Value="InterSystems.Data.IRISClient">Intersystems IRIS</asp:ListItem>
                                           <asp:ListItem Value="InterSystems.Data.CacheClient">Intersystems Cache</asp:ListItem>
                                       </asp:DropDownList>
                                  </td>
                             </tr>
                             <tr id="trConnection" runat="server" visible="false" style="border-color:#ffffff; ">
                                  <td align="right" class="auto-style8" width="100%" >
                                        <span style ="font-family:Tahoma;font-size:12px; font-weight: bold; color: #CC3300;">&nbsp;User Connection String
                                       (add password)*:&nbsp;</span>
                                  </td>
                                  <td align ="left" class="auto-style1" >
                                       <asp:TextBox ID="ConnStr" runat="server" Width="756px"></asp:TextBox>
                                  </td>
                             </tr>
                           <tr id="trSaveConnection" runat="server" visible="false" style="border-color:#ffffff;">
                                  <td align="right" class="auto-style8">
                                       <span style ="font-family:Tahoma;font-size:12px;">&nbsp;&nbsp;</span>
                                  </td>
                                  <td align ="left" class="auto-style1">
                                          <asp:CheckBox ID="CheckBox1" runat="server" Font-Size="Smaller" Text="Save Connection Info for future use (password will not be saved for security reason)" />
                                  </td>
                             </tr>
                            <tr id="trLogIn" runat="server" style="border-color:#ffffff;visibility:visible">
                                <td class="auto-style8"> </td>
                               <td class="auto-style2">
                                <span style ="font-family:Tahoma;font-size:12px;"> 
                                 <font color="#930000" face="Arial" size="2">
                               <asp:Button ID="btLogin" runat="server" text="Login" />
                                </font>&nbsp;&nbsp;</span>
                           <%--     <span style ="font-family:Tahoma;font-size:12px;"><font color="#930000" face="Arial" size="2">--%>
                               <%--<asp:LinkButton ID="btRegister" runat="server" text="Register new team" </asp:LinkButton>--%>
                                   <%-- <asp:LinkButton ID="btRegister" runat="server" TabIndex="-1" ToolTip ="Sends password reminder by Email. Logon must be entered." CssClass="NodeStyle" Font-Names="Tahoma" Font-Size="12px">Register new team</asp:LinkButton>--%>
                                  </td>  
                            </tr>
                            <tr>
                                
                                    <td align="center" colspan="2"> 
                                        <br />
                                     <asp:LinkButton ID="btForgot" runat="server" TabIndex="-1" ToolTip ="Sends password reminder by Email. Logon must be entered." CssClass="NodeStyle" Font-Names="Tahoma" Font-Size="12px">Forgot Password ?</asp:LinkButton>
                                     
                                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                          <asp:LinkButton ID="btChange" runat="server" CssClass="NodeStyle" Font-Names="Tahoma" Font-Size="12px" TabIndex="-1" ToolTip="Change Password. Logon must be entered.">Change Password</asp:LinkButton>
                                      <br /><br />
                                      <span id="siteseal"><script async type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=SYhDKXg2IT7QXHzPEW7z7tmavANkr8vMDCiRmZvbKczmKBJ5Wj8eKl1EX00B"></script></span>
       

                              </td>
                             <%-- <tr>                                
                                    <td align="center" colspan="2"> 
                                        <br />
                                        <asp:LinkButton ID="btRegister" runat="server" TabIndex="-1" ToolTip ="Register new team." CssClass="NodeStyle" Font-Names="Tahoma" Font-Size="12px">Register new team</asp:LinkButton>
                                  </td>
                              </tr>  --%>                 
 
                        </table> 
                        <font
                               color="#800000" face="Arial" size="2"><b><font color="#930000" face="Arial">
                        <br />
                        </font></b></font> 
                       <br /><br />
                        <font color="#800000" face="Arial" size="2"><b><font color="#930000" face="Arial"><span style="font-family:Tahoma;font-size:14px;font-weight:bold;color:Red">
                        <asp:Label ID="Label2" runat="server" ForeColor="#999999" Height="22px" Text=" At OUReports, we can serve organizations from our cloud Web server, or by installing our Web application on their own Web server, or by deploying a OUReports Appliance. Our system requires only restricted access(reading permissions) to the database of the organization we are serving. Our application automatically analyzes data structure, generates a set of preliminary reports, and provides a simple interface for creating ad hoc reports and conducting statistical research. " ToolTip="It is time to put the Internet to work making the creation and processing of custom reports convenient, simple, and accessible for end users and administrators.
At OUReports, we can serve organizations from our cloud Web server, or by installing our Web application on their own Web server, or by deploying a OUReports Appliance. 
Our system requires only restricted access(reading permissions) to the database of the organization we are serving. Our application automatically analyzes data structure, generates a set of preliminary reports, and provides a simple interface for creating ad hoc reports and conducting statistical research. 
Any organization storing data in SQL Server, MySQL, or Cache Intersystems databases can use our system to quickly and easily generate  fast highly informational and statistical reports." Visible="False"></asp:Label>
                        </span></font></b></font>
                        <br />
                       <br />
                       <br />
                       <br />
                       <br />
                        <p>
                            <%--<a href="mailto:<%=Session("SupportEmail")%>" name="supportemail" tabindex ="-1"><font color="#0000ff" face="Arial Rounded MT Bold" size="1">Support team</font></a>--%>

                        </p>
                        <p align="left">
                            <asp:Label ID="LabelVersion" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="XX-Small" ForeColor="Black" Text="Version 4-00"></asp:Label>
                        </p>
                        </font></td>
              </tr>
         </table>  
        </div>
                  </td>
        </tr>
    </table>        
     

            <ucMsgBox:Msgbox id="MessageBox" runat ="server" > </ucMsgBox:Msgbox> 
                       
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btLogin" />
        </Triggers>
        </asp:UpdatePanel>   
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="udpDefault">
                <ProgressTemplate >
                <div class="modal">
                    <div class="center">
                       <asp:Image ID="imgProgress" runat="server"  ImageAlign="AbsMiddle" ImageUrl="~/Controls/Images/WaitImage2.gif" />
                       Please Wait...
                   </div>
                </div>
                </ProgressTemplate>
        </asp:UpdateProgress>                              
         </form>
   
   </div>   
     
 </body>
</html>
