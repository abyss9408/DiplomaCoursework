<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ProductDetails.aspx.cs" Inherits="ProductDetails" culture="auto" uiCulture="auto"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 52%;
            height: 213px;
        }
        .auto-style3 {
            width: 148px;
        }
        .auto-style4 {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:Button ID="btn_Back" runat="server" Text="Back" OnClick="btn_Back_Click" />
    <h2>
        PRODUCT DETAILS</h2>
    <table class="auto-style2">
        <tr>
            <td class="auto-style3" rowspan="6">
                <asp:Image ID="img_Product" runat="server" Height="200px" Width="212px" />
            </td>
            <td>
                &nbsp; Name:
                <asp:Label ID="lbl_ProdName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp; Description:
                <asp:Label ID="lbl_ProdDetails" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                &nbsp; Stock:
                <asp:Label ID="lbl_ProdStock" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp; Price: <asp:Label ID="lbl_Price" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp; Quantity:
                <asp:Button ID="btnSubtractQuantity" runat="server" Height="39px" OnClick="btnSubtractQuantity_Click" Text="-" Width="50px" />
                <asp:TextBox ID="txtQuantity" runat="server" Width="53px" MaxLength="3" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);">1</asp:TextBox>
                <asp:Button ID="btnAddQuantity" runat="server" Height="39px" OnClick="btnAddQuantity_Click" Text="+" Width="50px" />
                <br />
                <asp:Button ID="btnAddProductToCart" runat="server" OnClick="btnAddProductToCart_Click" Text="Add To Cart" />
                <asp:Button ID="btnWishList" runat="server" OnClick="btnWishList_Click" Text="Wishlist" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_Error" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    Product ID =
<asp:Label ID="lbl_ProdID" runat="server"></asp:Label>
    <br />
</asp:Content>

