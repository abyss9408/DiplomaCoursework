<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ViewShoppingCart.aspx.cs" Inherits="ViewShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    You have
    <asp:Label ID="lbl_NoOfProducts" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
&nbsp;items in your cart.<br />
    <br />
    <asp:GridView ID="gv_ShoppingCart" runat="server" AutoGenerateColumns="False" CellPadding="3" Height="385px" OnRowDeleting="gv_ShoppingCart_RowDeleting" ShowFooter="True" Width="676px" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1" >
        <Columns>
            <asp:BoundField DataField="productid" HeaderText="Product ID">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:ImageField DataImageUrlField="productimage" HeaderText="Product Image">
                <ControlStyle Height="100px" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:ImageField>
            <asp:BoundField DataField="productname" HeaderText="Product Name">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="price" HeaderText="Price" DataFormatString="{0:c}">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Quantity">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("quantity") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("quantity") %>' Width="209px" MaxLength="5"></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="totalprice" HeaderText="Total Price" DataFormatString="{0:c}">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" Height="50px" />
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
    <asp:Button ID="btn_Update" runat="server" Text="Update Cart" OnClick="btn_Update_Click" />
    <asp:Button ID="btn_Clear" runat="server" Text="Clear Cart" OnClick="btn_Clear_Click" />
    <asp:Button ID="btn_Checkout" runat="server" Text="Checkout" OnClick="btn_Checkout_Click" />
    <asp:Button ID="btn_Products" runat="server" PostBackUrl="~/Products.aspx" Text="Back to Shopping" />
    <br />
    <asp:Label ID="lbl_Error" runat="server" ForeColor="Red"></asp:Label>
    <br />
</asp:Content>

