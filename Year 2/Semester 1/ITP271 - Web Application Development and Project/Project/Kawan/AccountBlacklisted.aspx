<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AccountBlacklisted.aspx.cs" Inherits="AccountBlacklisted" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="nav-justified">
        <tr>
            <td class="text-center"><asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Your account has been blacklisted."></asp:Label>
                <br />
                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Size="Small" Text="To appeal, send an email to xxx@gmail.com "></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text-center">
                <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" />
            </td>
        </tr>
    </table>
</asp:Content>

