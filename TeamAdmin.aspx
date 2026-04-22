<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TeamAdmin.aspx.vb" Inherits="TeamAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Team administration</title>
     <style type="text/css">
        .style1
        {
            height: 23px;
            width: 557px;
        }
        .style2
        {
            height: 27px;
            width: 450px
            /*width: 557px;*/
        }
        .style3
        {
            height: 23px;
            width: 109px;
        }
        .style4
        {
            height: 27px;
            width: 200px;
        }
        .auto-style1 {
            height: 23px;
            width: 520px;
        }
.ticketbutton 
{
  width: 80px;
  height: 25px;
  font-size: 12px;
  border-radius: 5px;
  border-style :solid;
  border-color: #4e4747 ;
  color: black;
  border-width: 1px;
  /*background-color: ButtonFace;*/
  background-image: linear-gradient(to bottom, rgba(158, 188, 250,0),rgba(158, 188, 250,1));

  padding: 3px;
  margin:5px;
  z-index: 9999; 
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
         .auto-style2 {
             width: 524px;
         }
         .auto-style3 {
             width: 76%;
         }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
        <div align="left">
              <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/HelpDesk.aspx">Task List</asp:HyperLink> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Default.aspx">Log off</asp:HyperLink>
                        
            <br /><br />
            <asp:Label ID="Label1" runat="server" Text="Team/Topic Members" Font-Size="Larger" Font-Bold="True" ForeColor="Gray"></asp:Label>
        </div>
        <div>
            <table Width="40%">
            <tr>
                <td align="left" Width="50%" style="font-weight: bold; color: #ffffff; font-family: Arial; height: 30px;
                    background-color: Gray; font-size:small;">
                    <div class="auto-style2" >   
                      Topics:&nbsp; 
                      <asp:DropDownList id="ddTopics" runat="server" ForeColor="black" AutoPostBack="True"></asp:DropDownList>
                      &nbsp;&nbsp;                     
                    </div>    
                    </td>
                    <td align="left" Width="50%" style="font-weight: bold; color: #ffffff; font-family: Arial; height: 30px;
                    background-color: Gray; font-size:small;">
                    <div class="auto-style2" > 
                        <asp:Label ID="lbNewTopic" runat="server" ForeColor="White" Text="New topic:"></asp:Label>
                        &nbsp;   <asp:TextBox ID="txtTopic" runat="server" Visible="true" width="336px"></asp:TextBox>
                      &nbsp;&nbsp;          
                        <asp:LinkButton ID="btAddTopic" runat="server" TabIndex="-1" ToolTip="Add new topic for team">add</asp:LinkButton>
                    </div>
                    </td>
            </tr>
        </table>
        </div>
        <div >     
            <table Width="40%">
                <tr height="30px">                   
                    
                    <td Width="50%" align="left" style="font-weight: bold; color: #ffffff; font-family: Arial; background-color: Gray; font-size:small;" class="auto-style1">
                        <asp:Label ID="Label2" runat="server" ForeColor="White" Text="Search:"></asp:Label>
                         &nbsp;&nbsp
                        <asp:TextBox ID="SearchText" runat="server" Visible="true" width="200px"></asp:TextBox>
                        <asp:Button ID="ButtonSearch" runat="server" CssClass="ticketbutton" Text="Search" Visible="true" valign="center"/>
                    </td>
                    <td Width="50%" align="center">                      
                       
                        <asp:LinkButton ID="btnRegistration" runat="server" TabIndex="-1" ToolTip="New user Registration.">new team member registration</asp:LinkButton>
                       &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp 
                    </td>
                    
                </tr>
            </table>
                     
        </div> 
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" BackColor="WhiteSmoke" Font-Names="Arial" Font-Size="Small" AllowPaging="True" PageSize="30">
            <AlternatingRowStyle BackColor="#f0f0f0" />
            <RowStyle BackColor="White" />   
            <Columns>
                <%--<asp:BoundField DataField="Indx" HtmlEncode="False" DataFormatString="<a target='_blank' href='UserDefinition.aspx?indx={0}'>edit</a>" />--%>
                 <asp:BoundField DataField="Indx" HtmlEncode="False" DataFormatString="<a href='TeamMemberRegistration.aspx?indx={0}'>edit</a>" />
            </Columns>
        </asp:GridView>
        
    </form>
</body>
</html>
