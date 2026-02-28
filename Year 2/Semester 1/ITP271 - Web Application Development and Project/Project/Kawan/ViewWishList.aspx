<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ViewWishList.aspx.cs" Inherits="ViewWishList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        You have
        <asp:Label ID="lbl_NoOfProducts" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
&nbsp;items in your wishlist.
    <br />
    <br />
<asp:GridView ID="gv_WishList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" Height="255px" OnRowDeleting="gv_WishList_RowDeleting" Width="616px">
    <Columns>
        <asp:BoundField DataField="productid" HeaderText="Product ID" />
        <asp:BoundField DataField="productname" HeaderText="Product Name" />
        <asp:ImageField DataImageUrlField="productimage" HeaderText="Product Image">
            <ControlStyle Height="100px" Width="100px" />
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:ImageField>
        <asp:BoundField DataField="price" HeaderText="Price" />
        <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
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
        <asp:Button ID="btn_Clear" runat="server" OnClick="btn_Clear_Click" Text="Clear Wishlist" />
        <asp:Button ID="btn_Products" runat="server" PostBackUrl="~/Products.aspx" Text="Back to Shopping" />
    <br />
</asp:Content>

