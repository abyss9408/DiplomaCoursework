<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminLogin" %>

<%@ Register assembly="Recaptcha.Web" namespace="Recaptcha.Web.UI.Controls" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style45 {
            width: 675px;
            height: 55px;
            text-align: right;
        }
        .auto-style47 {
            height: 55px;
            width: 415px;
        }
        .auto-style48 {
            height: 55px;
        }
        .auto-style53 {
            width: 675px;
            height: 55px;
        }
        .auto-style55 {
            font-size: larger;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <br />
    <table class="nav-justified">
        <tr>
            <td class="text-center">
    <asp:LinkButton ID="LinkButton2" runat="server" Font-Size="Larger" PostBackUrl="~/Login/Login.aspx">User</asp:LinkButton>
&nbsp; <span class="auto-style55">|</span>&nbsp;
    <asp:LinkButton ID="LinkButton3" runat="server" Font-Size="Larger" PostBackUrl="~/Login/AdminLogin.aspx">Admin</asp:LinkButton>
            </td>
        </tr>
    </table>
    <br />
    <table class="nav-justified">
            <tr>
                <td class="auto-style45">
                    <asp:Label ID="Label4" runat="server" Text="Admin Username:"></asp:Label>
                </td>
                <td class="auto-style47">
        <asp:TextBox ID="txtAdminUser" runat="server" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAdminUser" runat="server" ControlToValidate="txtAdminUser" ErrorMessage="*Please enter your admin username" ForeColor="Red" ValidationGroup="AdminLogin"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style48"></td>
            </tr>
            <tr>
                <td class="auto-style45">
                    <asp:Label ID="Label5" runat="server" Text="Admin Password:"></asp:Label>
                </td>
                <td class="auto-style47">
        <asp:TextBox ID="txtAdminPass" runat="server" TextMode="Password" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAdminPass" runat="server" ControlToValidate="txtAdminPass" ErrorMessage="*Please enter your admin password" ForeColor="Red" ValidationGroup="AdminLogin"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style48"></td>
            </tr>
            <tr>
                <td class="auto-style53">&nbsp;</td>
                <td class="auto-style47">
                    <cc1:Recaptcha ID="Recaptcha1" runat="server" />
                </td>
                <td class="auto-style48">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style53"></td>
                <td class="auto-style47">
        <asp:Button ID="btnAdminLogin" runat="server" OnClick="btnAdminLogin_Click" Text="Admin Login" ValidationGroup="AdminLogin" />
        <asp:Label ID="lblValidAdmin" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style48"></td>
            </tr>
            </table>
        <br />
        <br />
        <br />
</asp:Content>

