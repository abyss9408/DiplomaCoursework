<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PageNotFoundErrorPage.aspx.cs" Inherits="PageNotFoundErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .auto-style1 {
        font-size: large;
        text-align: center;
        color: #FF0000;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="nav-justified">
    <tr>
        <td class="auto-style1"><strong>Error 401: Page not found</strong></td>
    </tr>
    <tr>
        <td class="text-center">
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Return" />
        </td>
    </tr>
</table>
</asp:Content>

