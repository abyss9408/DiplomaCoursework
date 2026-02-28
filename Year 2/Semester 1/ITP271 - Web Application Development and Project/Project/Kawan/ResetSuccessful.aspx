<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ResetSuccessful.aspx.cs" Inherits="ResetSuccessful" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="nav-justified">
        <tr>
            <td class="text-center">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" Text="Your password has been reset."></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text-center">
                <asp:Button ID="btn_LoginPage" runat="server" PostBackUrl="~/Login/Login.aspx" Text="Go to login page" />
            </td>
        </tr>
    </table>
</asp:Content>

