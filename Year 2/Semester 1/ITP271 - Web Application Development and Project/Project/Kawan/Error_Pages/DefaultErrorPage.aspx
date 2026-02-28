<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="DefaultErrorPage.aspx.cs" Inherits="DefaultErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .auto-style1 {
        text-align: center;
        font-size: large;
        color: #FF0000;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="nav-justified">
    <tr>
        <td class="auto-style1"><strong>Error: Something went wrong!</strong></td>
    </tr>
    <tr>
        <td class="text-center">
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Return" />
        </td>
    </tr>
</table>
</asp:Content>

