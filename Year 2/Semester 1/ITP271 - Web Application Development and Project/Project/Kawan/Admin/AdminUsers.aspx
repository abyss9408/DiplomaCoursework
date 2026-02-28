<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMain.master" AutoEventWireup="true" CodeFile="AdminUsers.aspx.cs" Inherits="AdminUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            height: 65px;
            width: 837px;
        }
        .auto-style3 {
            text-align: center;
            height: 65px;
        }
        .auto-style4 {
            text-align: center;
            font-size: large;
            text-decoration: underline;
        }
    .auto-style5 {
        font-size: medium;
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
<asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click1" Text="Logout" />
            </td>
        </tr>
    </table>
    <table class="nav-justified">
        <tr>
            <td class="auto-style4"><strong>Users</strong></td>
        </tr>
    </table>
<span class="auto-style5">Filter by status:</span>
<asp:DropDownList ID="ddlStatusFilter" runat="server" AutoPostBack="True" Height="28px" OnSelectedIndexChanged="ddpStatusFilter_SelectedIndexChanged" Width="146px">
    <asp:ListItem>All users</asp:ListItem>
    <asp:ListItem>Active</asp:ListItem>
    <asp:ListItem>Inactive</asp:ListItem>
    <asp:ListItem>Blacklisted</asp:ListItem>
</asp:DropDownList>
    <br />
    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" DataSourceID="sdsUsers" CellPadding="3" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1" Width="1483px">
        <Columns>
            <asp:BoundField DataField="UserID" HeaderText="UserID" ReadOnly="True" SortExpression="UserID" >
            </asp:BoundField>
            <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" >
            </asp:BoundField>
            <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
            <asp:BoundField DataField="Last_Login" HeaderText="Last_Login" SortExpression="Last_Login" >
            </asp:BoundField>
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" >
            </asp:BoundField>
            <asp:BoundField DataField="Full_Name" HeaderText="Full_Name" SortExpression="Full_Name" >
            </asp:BoundField>
            <asp:BoundField DataField="Date_Of_Birth" HeaderText="Date_Of_Birth" SortExpression="Date_Of_Birth" >
            </asp:BoundField>
            <asp:BoundField DataField="Salutation" HeaderText="Salutation" SortExpression="Salutation" >
            </asp:BoundField>
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" >
            </asp:BoundField>
            <asp:BoundField DataField="Phone_Number" HeaderText="Phone_Number" SortExpression="Phone_Number" >
            </asp:BoundField>
            <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" >
            </asp:BoundField>
            <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" >
            </asp:BoundField>
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" >
            </asp:BoundField>
            <asp:BoundField DataField="State_Province" HeaderText="State_Province" SortExpression="State_Province" >
            </asp:BoundField>
            <asp:BoundField DataField="Zip_Postal_Code" HeaderText="Zip_Postal_Code" SortExpression="Zip_Postal_Code" >
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
    <asp:SqlDataSource ID="sdsUsers" runat="server" ConnectionString="<%$ ConnectionStrings:KawanConnection %>" SelectCommand="SELECT [UserID], [Username], [Last_Login], [Status], [Full_Name], [Date_Of_Birth], [Salutation], [Email], [Phone_Number], [Country], [Address], [City], [State_Province], [Zip_Postal_Code], [Password] FROM [Users]"></asp:SqlDataSource>
    <br />
<asp:SqlDataSource ID="sdsFilterByStatus" runat="server" ConnectionString="<%$ ConnectionStrings:KawanConnection %>" SelectCommand="SELECT [UserID], [Username], [Password], [Last_Login], [Status], [Full_Name], [Date_Of_Birth], [Salutation], [Email], [Phone_Number], [Country], [Address], [City], [State_Province], [Zip_Postal_Code] FROM [Users] WHERE ([Status] = @Status)">
    <SelectParameters>
        <asp:ControlParameter ControlID="ddlStatusFilter" Name="Status" PropertyName="SelectedValue" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
<br />
    Total number of users:
    <asp:Label ID="lbl_NoOfUsers" runat="server" Font-Bold="True"></asp:Label>
    <br />
    <br />
    Blacklist/Unblacklist User ID:
    <asp:TextBox ID="txt_UserToBL" runat="server" Width="170px" MaxLength="50"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btn_Blacklist" runat="server" OnClick="btn_Blacklist_Click" Text="Blacklist" />
    <asp:Button ID="btn_Unblacklist" runat="server" OnClick="btn_Unblacklist_Click" Text="Unblacklist/Reactivate" />
    <br />
    <asp:Label ID="lbl_BlacklistMsg" runat="server"></asp:Label>
    <br />
</asp:Content>

