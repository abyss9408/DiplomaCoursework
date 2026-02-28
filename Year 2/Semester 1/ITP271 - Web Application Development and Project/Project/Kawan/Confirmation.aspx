<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Confirmation.aspx.cs" Inherits="Confirmation" %>

<%@ Register assembly="Recaptcha.Web" namespace="Recaptcha.Web.UI.Controls" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lbl_Error" runat="server" ForeColor="Red"></asp:Label>
    <span class="auto-style2">
    <br />
    <br />
    Order ID:
    <asp:Label ID="lbl_OrderID" runat="server"></asp:Label>
    </span>
    <br />
    <br />
    <span class="auto-style2">Order Date:
    <asp:Label ID="lbl_Date" runat="server"></asp:Label>
    <br />
    <br />
    Order Summary<br />
    <asp:GridView ID="gv_Checkout" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" Height="263px" ShowFooter="True" Width="618px">
        <Columns>
            <asp:BoundField DataField="productid" HeaderText="Product ID" />
            <asp:BoundField DataField="productname" HeaderText="Product Name" />
            <asp:ImageField DataImageUrlField="productimage" HeaderText="Product">
                <ControlStyle Height="100px" Width="100px" />
            </asp:ImageField>
            <asp:BoundField DataField="price" HeaderText="Price" />
            <asp:BoundField DataField="quantity" HeaderText="Quantity" />
            <asp:BoundField DataField="totalprice" HeaderText="Total Price" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" Height="40px" />
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
    Payment Details<br />
    <br />
    Debit/Credit Card Number:
    <asp:TextBox ID="txtCardNumber" runat="server" Width="287px" MaxLength="16" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
    <br />
    <br />
    CVV:
    <asp:TextBox ID="txtCVV" runat="server" Width="139px" MaxLength="3" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btn_Purchase" runat="server" OnClick="btn_Purchase_Click" Text="Purchase" />
    <br />
    <br />
    <asp:Label ID="lbl_Prompt" runat="server" Text="Are you sure you want to place your order? No refunds after 3 days of purchase." Visible="False"></asp:Label>
    <br />
    <cc1:Recaptcha ID="Recaptcha1" runat="server" Visible="False" />
    <br />
    <asp:Button ID="btn_Confirm" runat="server" OnClick="btn_Confirm_Click" Text="Confirm" Visible="False" />
    &nbsp;<asp:Button ID="btn_Cancel" runat="server" OnClick="btn_Cancel_Click" Text="Cancel" Visible="False" />
    <br />
    </span>
</asp:Content>

