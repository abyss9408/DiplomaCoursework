<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMain.master" AutoEventWireup="true" CodeFile="AdminAddProduct.aspx.cs" Inherits="AdminAddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 204px;
        }
        .auto-style7 {
            width: 204px;
            height: 201px;
        }
        .auto-style8 {
            height: 201px;
        }
        .auto-style9 {
            width: 204px;
            height: 55px;
        }
        .auto-style10 {
            height: 55px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="nav-justified">
        <tr>
            <td class="auto-style9">Product ID</td>
            <td class="auto-style10">
                <asp:Label ID="lbl_ProdID" runat="server" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style9">Product Name</td>
            <td class="auto-style10">
                <asp:TextBox ID="txt_ProdName" runat="server" Width="502px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter a Product Name" ForeColor="Red" ControlToValidate="txt_ProdName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style7">Product Desc</td>
            <td class="auto-style8">
                <asp:TextBox ID="txt_ProdDesc" runat="server" Height="154px" TextMode="MultiLine" Width="525px" MaxLength="400"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_ProductDesc" runat="server" ControlToValidate="txt_ProdDesc" ErrorMessage="Enter a description" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style9">Unit Price</td>
            <td class="auto-style10">
                <asp:TextBox ID="txt_UnitPrice" runat="server" Width="510px" MaxLength="20" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_UnitPrice" runat="server" ControlToValidate="txt_UnitPrice" ErrorMessage="Enter a Unit Price" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style9">Stock Level</td>
            <td class="auto-style10">
                <asp:TextBox ID="txt_StockLevel" runat="server" MaxLength="50" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_StockLevel" ErrorMessage="Enter a stock level" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style9">Product Image</td>
            <td class="auto-style10">
                <asp:FileUpload ID="FileUpload1" runat="server" Height="38px" />
            </td>
        </tr>
        <tr>
            <td class="auto-style9">Category</td>
            <td class="auto-style10">
                <asp:DropDownList ID="DropDownListCategory" runat="server" Height="52px" Width="201px">
                    <asp:ListItem>Camera</asp:ListItem>
                    <asp:ListItem>Router</asp:ListItem>
                    <asp:ListItem>Monitor</asp:ListItem>
                    <asp:ListItem>Keyboard</asp:ListItem>
                    <asp:ListItem>Headset</asp:ListItem>
                    <asp:ListItem>Phone</asp:ListItem>
                    <asp:ListItem>Mouse</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style9"></td>
            <td class="auto-style10">
                <asp:Label ID="lbl_Result" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td>
                <asp:Button ID="btn_Insert" runat="server" OnClick="btn_Insert_Click" Text="Insert" />
                <asp:Button ID="btn_ViewProduct" runat="server" Text="View Product List" CausesValidation="false" OnClick="btn_ViewProduct_Click"/>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

