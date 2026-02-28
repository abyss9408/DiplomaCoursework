<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMain.master" AutoEventWireup="true" CodeFile="AdminOrders.aspx.cs" Inherits="AdminOrders" %>

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
        .auto-style5 {
            font-size: large;
            text-decoration: underline;
            text-align: center;
        }
    .auto-style6 {
        font-size: medium;
    }
    .auto-style7 {
        font-size: large;
        text-align: center;
    }
    .auto-style8 {
        font-size: large;
        text-decoration: underline;
        text-align: center;
        height: 32px;
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
            <td class="auto-style8"><strong>Orders List</strong></td>
        </tr>
        <tr>
            <td class="auto-style7"><span class="auto-style6">Total Revenue: </span>
                <asp:Label ID="lbl_Revenue" runat="server" CssClass="auto-style6"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text-center">Clear by OrderID:<asp:TextBox runat="server" ID="txtClearOrderByOrderID" MaxLength="50"></asp:TextBox>
&nbsp;<asp:Button ID="btn_ClearOrderbyOrderID" runat="server" Text="Clear" OnClick="btn_ClearOrderbyOrderID_Click" />
            </td>
        </tr>
        <tr>
            <td class="text-center">
                <asp:Label ID="lbl_Message" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gv_Orders" runat="server" AutoGenerateColumns="False" CellPadding="3" DataKeyNames="OrderID" DataSourceID="sdsOrders" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1" Height="185px" Width="1319px">
        <Columns>
            <asp:BoundField DataField="OrderID" HeaderText="OrderID" ReadOnly="True" SortExpression="OrderID" >
            </asp:BoundField>
            <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" >
            </asp:BoundField>
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" >
            </asp:BoundField>
            <asp:BoundField DataField="Grand_Total" HeaderText="Grand_Total" SortExpression="Grand_Total" >
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
    <table class="nav-justified">
        <tr>
            <td class="auto-style5"><strong>Orders Items</strong></td>
        </tr>
        </table>
    <br />
    <asp:GridView ID="gv_OrdersItems" runat="server" AutoGenerateColumns="False" CellPadding="3" DataSourceID="sdsOrdersItems" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1" Height="185px" Width="1318px">
        <Columns>
            <asp:BoundField DataField="OrderID" HeaderText="OrderID" SortExpression="OrderID">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="ProductID" HeaderText="ProductID" SortExpression="ProductID">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Total_Price" HeaderText="Total Price" SortExpression="Total_Price">
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
    <br />
    <asp:SqlDataSource ID="sdsOrders" runat="server" ConnectionString="<%$ ConnectionStrings:KawanConnection %>" SelectCommand="SELECT * FROM [Orders] ORDER BY [Date]" DeleteCommand="DELETE FROM [Orders] WHERE [OrderID] = @OrderID" InsertCommand="INSERT INTO [Orders] ([OrderID], [UserID], [Date], [Grand_Total]) VALUES (@OrderID, @UserID, @Date, @Grand_Total)" UpdateCommand="UPDATE [Orders] SET [UserID] = @UserID, [Date] = @Date, [Grand_Total] = @Grand_Total WHERE [OrderID] = @OrderID">
        <DeleteParameters>
            <asp:Parameter Name="OrderID" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="OrderID" Type="String" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="Date" Type="DateTime" />
            <asp:Parameter Name="Grand_Total" Type="Decimal" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="Date" Type="DateTime" />
            <asp:Parameter Name="Grand_Total" Type="Decimal" />
            <asp:Parameter Name="OrderID" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsOrdersItems" runat="server" ConnectionString="<%$ ConnectionStrings:KawanConnection %>" SelectCommand="SELECT * FROM [Orders_Products]"></asp:SqlDataSource>
    <br />
    <br />
    <br />
</asp:Content>

