<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" culture="auto" uiCulture="auto"%>

<%@ Register assembly="Recaptcha.Web" namespace="Recaptcha.Web.UI.Controls" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style10 {
            width: 415px;
            height: 30px;
        }
        .auto-style44 {
            width: 675px;
            height: 30px;
        }
        .auto-style45 {
            width: 675px;
            height: 55px;
            text-align: right;
        }
        .auto-style47 {
        height: 55px;
        width: 415px;
        text-align: left;
    }
        .auto-style48 {
            height: 55px;
        }
        .auto-style53 {
            width: 675px;
            height: 55px;
        }
        .auto-style55 {
            height: 30px;
        }
        .auto-style56 {
            font-size: large;
        }
    .auto-style57 {
        font-size: larger;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">&nbsp;
    <br />
&nbsp;<table class="nav-justified">
        <tr>
            <td class="text-center">
<asp:LinkButton ID="LinkButton2" runat="server" Font-Size="Larger" PostBackUrl="~/Login/Login.aspx">User</asp:LinkButton>
&nbsp; <span class="auto-style57">|</span><span class="auto-style56"> </span>&nbsp;<asp:LinkButton ID="LinkButton3" runat="server" Font-Size="Larger" PostBackUrl="~/Login/AdminLogin.aspx">Admin</asp:LinkButton>
            </td>
        </tr>
    </table>
<br />
    <table class="nav-justified">
            <tr>
                <td class="auto-style45">
                    &nbsp;Username:</td>
                <td class="auto-style47"><asp:TextBox ID="txtUser" runat="server" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="txtUser" ErrorMessage="*Please enter your username" ForeColor="Red" ValidationGroup="UserLogin"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style48"></td>
            </tr>
            <tr>
                <td class="auto-style45">
                    Password:</td>
                <td class="auto-style47">
        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="*Please enter your password" ForeColor="Red" ControlToValidate="txtPass" ValidationGroup="UserLogin"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style48"></td>
            </tr>
            <tr>
                <td class="auto-style45">
                    &nbsp;</td>
                <td class="auto-style47">
                    <cc1:Recaptcha ID="Recaptcha1" runat="server" Theme="Default" />
                </td>
                <td class="auto-style48">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style53"></td>
                <td class="auto-style47">
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" ValidationGroup="UserLogin" />
        <asp:Label ID="lblValid" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td class="auto-style48"></td>
            </tr>
            <tr>
                <td class="auto-style44"></td>
                <td class="auto-style10">Don&#39;t have an account? Register
                    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Register.aspx">here</asp:LinkButton>
                    <br />
                    Forgot Password? Click
                    <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/ForgotPassword.aspx">here</asp:LinkButton>
&nbsp;to reset.</td>
                <td class="auto-style55"></td>
            </tr>
            </table>
        <br />
        <br />
        <br />
</asp:Content>

