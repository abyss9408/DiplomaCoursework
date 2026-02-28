<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMain.master" AutoEventWireup="true" CodeFile="AdminLogs.aspx.cs" Inherits="AdminLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 837px;
            height: 65px;
        }
        .auto-style3 {
            text-align: center;
            height: 65px;
        }
        .auto-style4 {
            font-size: large;
            text-align: center;
            text-decoration: underline;
            height: 32px;
        }
        .auto-style5 {
            text-align: center;
            height: 60px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="nav-justified">
        <tr>
            <td class="auto-style2">Welcome
    <asp:Label ID="lblAdminUser" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
            </td>
            <td class="auto-style3">
    <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" />
                </td>
        </tr>
    </table>
    &nbsp;<table class="nav-justified">
        <tr>
            <td class="auto-style4"><strong>Users Login Logs</strong></td>
        </tr>
        <tr>
            <td class="auto-style5">
    <asp:Button ID="btnClearLogs" runat="server" OnClick="btnClearLogs_Click" Text="Clear Logs" />
            </td>
        </tr>
        <tr>
            <td class="auto-style5">
    Filter by UserID:<asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                <br />
                <asp:LinkButton ID="Lbtn_ListAll" runat="server" OnClick="Lbtn_ListAll_Click">List all login records</asp:LinkButton>
            </td>
        </tr>
    </table>
    <br />
    <asp:Label ID="lbl_Filter_Error" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CellPadding="3" DataKeyNames="LoginSuccessID" DataSourceID="sdsLogs" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1" Width="1326px">
    <Columns>
        <asp:BoundField DataField="LoginSuccessID" HeaderText="LoginSuccessID" ReadOnly="True" SortExpression="LoginSuccessID" >
        <HeaderStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" >
        <HeaderStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Login_Universal_Timestamp" HeaderText="Login_Universal_Timestamp" SortExpression="Login_Universal_Timestamp" >
        <HeaderStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Login_Local_Timestamp" HeaderText="Login_Local_Timestamp" SortExpression="Login_Local_Timestamp" >
        <HeaderStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Computer_Name" HeaderText="Computer_Name" SortExpression="Computer_Name" >
        <HeaderStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="IP_Address" HeaderText="IP_Address" SortExpression="IP_Address" >
        <HeaderStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    </Columns>
    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F1F1F1" />
    <SortedAscendingHeaderStyle BackColor="#594B9C" />
    <SortedDescendingCellStyle BackColor="#CAC9C9" />
    <SortedDescendingHeaderStyle BackColor="#33276A" />
</asp:GridView>
    <br />
    <asp:SqlDataSource ID="sdsLogs" runat="server" ConnectionString="<%$ ConnectionStrings:KawanConnection %>" SelectCommand="SELECT * FROM [User_login_logs]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsFilterUserID" runat="server" ConnectionString="<%$ ConnectionStrings:KawanConnection %>" SelectCommand="SELECT * FROM [User_login_logs] WHERE ([UserID] = @UserID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtUserID" Name="UserID" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <br />
    <br />
</asp:Content>

