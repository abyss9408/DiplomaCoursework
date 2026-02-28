<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="UnauthorizedErrorPage.aspx.cs" Inherits="UnauthorizedErrorPage" %>

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
        <td class="auto-style1"><strong>Error 401: Unauthorized</strong></td>
    </tr>
    <tr>
        <td class="text-center">Sorry, your request could not be processed.<br />
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Return" />
        </td>
    </tr>
</table>
</asp:Content>

