<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="Welcome" %>

<%@ Register assembly="Recaptcha.Web" namespace="Recaptcha.Web.UI.Controls" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .auto-style2 {
        width: 189px;
            height: 62px;
        }
    .auto-style3 {
        width: 189px;
        height: 20px;
            text-align: right;
        }
    .auto-style4 {
        height: 20px;
    }
    .auto-style5 {
        width: 189px;
        height: 61px;
    }
    .auto-style6 {
        height: 61px;
            font-size: larger;
        }
    .auto-style7 {
        width: 189px;
        height: 47px;
            text-align: right;
        }
    .auto-style8 {
        height: 47px;
    }
        .auto-style9 {
            margin-left: 26px;
        }
        .auto-style10 {
            height: 62px;
        text-align: right;
    }
        .auto-style11 {
            width: 189px;
            height: 41px;
        }
        .auto-style12 {
            height: 41px;
        }
    .auto-style13 {
        width: 189px;
        height: 55px;
    }
    .auto-style14 {
        height: 55px;
    }
        .auto-style15 {
            font-size: large;
        }
        .auto-style16 {
            font-size: medium;
        }
        .auto-style17 {
            height: 41px;
            font-size: larger;
        }
        .auto-style18 {
            width: 189px;
            height: 41px;
            text-align: right;
        }
        .auto-style19 {
            font-size: 14px;
        }
        .auto-style20 {
            height: 41px;
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <br />
        <table class="nav-justified">
            <tr>
                <td class="auto-style2"><span class="auto-style16">Welcome</span> <asp:Label ID="lblUser" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label>
                    <span class="auto-style15">!</span><br />
                </td>
                <td class="auto-style10">
        <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" CssClass="auto-style9" CausesValidation="false"/>
                </td>
            </tr>
            <tr>
                <td class="auto-style11">User ID:
                    <asp:Label ID="lblUserID" runat="server" Font-Size="Large"></asp:Label>
                </td>
                <td class="auto-style12">
                    <asp:Label ID="lbl_LastLogin" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style11">&nbsp;</td>
                <td class="auto-style20">
                    <a class="twitter-timeline" href="https://twitter.com/Team_Kawan?ref_src=twsrc%5Etfw" width="300" height="300">Tweets by Team_Kawan</a> <script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script></td>
            </tr>
            <tr>
                <td class="auto-style11">&nbsp;</td>
                <td class="auto-style17">
                    <strong>Report bugs<br />
                    </strong><span class="auto-style19">Note: Abusing the bugs reporting system will get your account blacklisted.</span></td>
            </tr>
            <tr>
                <td class="auto-style18">Issue:</td>
                <td class="auto-style12">
                    <asp:TextBox ID="txt_Issue" runat="server" Height="123px" MaxLength="300" TextMode="MultiLine" Width="437px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_Issue" ErrorMessage="*Please specify the issue" ForeColor="Red" ValidationGroup="Issue"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style18">&nbsp;</td>
                <td class="auto-style12">
                    <asp:Button ID="btnSubmitIssue" runat="server" OnClick="btnSubmitIssue_Click" Text="Submit" ValidationGroup="Issue" />
                    <br />
                    <asp:Label ID="lbl_SupportError" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style6">
                    <strong>Feedback Form<br />
                    </strong><span class="auto-style19">Note: Same as the bugs reporting system</span></td>
            </tr>
            <tr>
                <td class="auto-style7">Subject:</td>
                <td class="auto-style8">
                    <asp:TextBox ID="txtSubject" runat="server" Width="440px" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubject" ErrorMessage="*Please enter a subject" ForeColor="Red" ValidationGroup="Feedback"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Description:</td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtDesc" runat="server" Height="189px" TextMode="MultiLine" Width="441px" MaxLength="400"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDesc" ErrorMessage="*Please enter some description" ForeColor="Red" ValidationGroup="Feedback"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style13"></td>
                <td class="auto-style14">
        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Feedback" />
                    <br />
        <asp:Label ID="lblSubmit" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
    
</asp:Content>

