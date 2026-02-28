<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMain.master" AutoEventWireup="true" CodeFile="AdminFeedback.aspx.cs" Inherits="AdminFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 837px;
            height: 65px;
        }
        .auto-style3 {
            width: 100%;
            margin-bottom: 0px;
        }
        .auto-style4 {
            text-align: center;
            height: 65px;
        }
        .auto-style5 {
            font-size: large;
            text-decoration: underline;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="auto-style3">
        <tr>
            <td class="auto-style2">Welcome
    <asp:Label ID="lblAdminUser" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
            </td>
            <td class="auto-style4">
    <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click1" Text="Logout" />
            </td>
        </tr>
    </table>
    <table class="nav-justified">
        <tr>
            <td class="auto-style5"><strong>Feedback List</strong></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="FeedbackID" DataSourceID="sdsFeedback" CellPadding="3" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1" Width="1483px">
        <Columns>
            <asp:BoundField DataField="FeedbackID" HeaderText="Feedback ID" ReadOnly="True" SortExpression="FeedbackID" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="UserID" HeaderText="User ID" SortExpression="UserID" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Feedback_Subject" HeaderText="Feedback Subject" SortExpression="Feedback_Subject" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Feedback_Desc" HeaderText="Feedback Desc" SortExpression="Feedback_Desc" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="sdsFeedback" runat="server" ConnectionString="<%$ ConnectionStrings:KawanConnection %>" SelectCommand="SELECT * FROM [Feedback]" DeleteCommand="DELETE FROM [Feedback] WHERE [FeedbackID] = @FeedbackID" InsertCommand="INSERT INTO [Feedback] ([FeedbackID], [UserID], [Feedback_Subject], [Feedback_Desc]) VALUES (@FeedbackID, @UserID, @Feedback_Subject, @Feedback_Desc)" UpdateCommand="UPDATE [Feedback] SET [UserID] = @UserID, [Feedback_Subject] = @Feedback_Subject, [Feedback_Desc] = @Feedback_Desc WHERE [FeedbackID] = @FeedbackID">
        <DeleteParameters>
            <asp:Parameter Name="FeedbackID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FeedbackID" Type="Int32" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="Feedback_Subject" Type="String" />
            <asp:Parameter Name="Feedback_Desc" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="Feedback_Subject" Type="String" />
            <asp:Parameter Name="Feedback_Desc" Type="String" />
            <asp:Parameter Name="FeedbackID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    &nbsp;<table class="nav-justified">
        <tr>
            <td class="text-center">
    <asp:Button ID="btnClearFeedback" runat="server" OnClick="btnClearFeedback_Click" Text="Clear Feedback" />
            </td>
        </tr>
    </table>
    <br />
    <br />
</asp:Content>

