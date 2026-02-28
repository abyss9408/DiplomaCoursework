<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" culture="auto" uiCulture="auto"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style6 {
        width: 230px;
        text-align: right;
    }
    .auto-style8 {
        font-size: medium;
    }
    .auto-style9 {
        text-align: center;
        height: 55px;
    }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <table class="nav-justified">
        <tr>
            <td class="auto-style9">
                <span class="auto-style8">Search products:</span>
    <asp:TextBox ID="txtSearch" runat="server" Width="408px" Height="40px"></asp:TextBox>
            &nbsp;
    <asp:Button ID="btnSearch" runat="server" Height="40px" Text="Search" Width="130px" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td class="text-center">
                Categories:
    <asp:LinkButton ID="linkbtnPhones" runat="server" OnClick="linkbtnPhones_Click">Phones</asp:LinkButton>
                &nbsp;|
    <asp:LinkButton ID="linkbtnCameras" runat="server" OnClick="linkbtnCameras_Click">Cameras</asp:LinkButton>
                &nbsp;|
    <asp:LinkButton ID="linkbtnRouters" runat="server" OnClick="linkbtnRouters_Click">Routers</asp:LinkButton>
                &nbsp;|
    <asp:LinkButton ID="linkbtnHeadset" runat="server" OnClick="linkbtnHeadset_Click">Headsets</asp:LinkButton>
                &nbsp;|
    <asp:LinkButton ID="linkbtnKeyboard" runat="server" OnClick="linkbtnKeyboard_Click">Keyboards</asp:LinkButton>
                &nbsp;|
    <asp:LinkButton ID="linkbtnMouse" runat="server" OnClick="linkbtnMouse_Click">Mouse</asp:LinkButton>
                &nbsp;|
    <asp:LinkButton ID="linkbtnMonitor" runat="server" OnClick="linkbtnMonitor_Click">Monitors</asp:LinkButton>
            &nbsp;|
    <asp:LinkButton ID="linkbtnViewAllProducts" runat="server" Font-Bold="True" Font-Size="Small" OnClick="linkbtnViewAllProducts_Click">View All Products</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="text-center">
                <asp:Label ID="lbl_Message" runat="server" Font-Size="Large"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <table class="nav-justified">
        <tr>
            <td class="auto-style6">
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
            <td>
    <asp:DataList ID="DataList1" runat="server" DataKeyField="ProductID" DataSourceID="SqlDataSource1" RepeatColumns="5" RepeatDirection="Horizontal" OnItemCommand="DataList1_ItemCommand" Width="1067px">
        <ItemTemplate>
            &nbsp;ID:<asp:Label ID="ProductIDLabel" runat="server" Text='<%# Eval("ProductID") %>' />
            <br />
            &nbsp;<asp:Label ID="Product_NameLabel" runat="server" Text='<%# Eval("Product_Name") %>' />
            <br />
&nbsp;<asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Product_Image") %>' Height="206px" Width="210px" />
            <br />
            Stock:
            <asp:Label ID="StockLabel" runat="server" Text='<%# Eval("Stock") %>' />
            <br />
            Price:
            <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("Price", "{0:c}") %>' />
            <br />
            Category:
            <asp:Label ID="CategoryLabel" runat="server" Text='<%# Eval("Category") %>' />
            <br />
            <asp:Button ID="btnPurchase" runat="server" Text="View"  CommandArgument='<%# Eval("ProductID") %>' CommandName="viewdetails" CausesValidation="False"/>
            <br />
            <br />
        </ItemTemplate>
    </asp:DataList>
            </td>
        </tr>
        </table>
    <br />
    <div class="text-center">
    </div>
    <br />
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KawanConnection %>" SelectCommand="SELECT * FROM [Products]"></asp:SqlDataSource>
    <br />
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:KawanConnection %>" SelectCommand="SELECT * FROM [Products] WHERE ([Category] = @Category)">
        <SelectParameters>
            <asp:QueryStringParameter Name="Category" QueryStringField="cat" Type="String" />
        </SelectParameters>
</asp:SqlDataSource>
    <br />
    <asp:SqlDataSource ID="sdsSearchBar" runat="server" ConnectionString="<%$ ConnectionStrings:KawanConnection %>" SelectCommand="SELECT * FROM [Products] WHERE (([Product_Name] LIKE '%' + @Product_Name + '%') OR ([Category] LIKE '%' + @Category + '%'))">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtSearch" Name="Product_Name" PropertyName="Text" />
            <asp:ControlParameter ControlID="txtSearch" Name="Category" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>

