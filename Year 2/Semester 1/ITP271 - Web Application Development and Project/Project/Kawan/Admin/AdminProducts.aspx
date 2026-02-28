<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMain.master" AutoEventWireup="true" CodeFile="AdminProducts.aspx.cs" Inherits="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style20 {
            height: 36px;
            font-size: large;
            text-decoration: underline;
            text-align: center;
        }
        .auto-style24 {
            height: 60px;
            text-align: center;
            font-size: medium;
        }
        .auto-style26 {
            height: 65px;
            width: 837px;
        }
        .auto-style27 {
            height: 65px;
            text-align: center;
        }
        .auto-style28 {
            text-align: center;
            font-size: large;
            }
        .auto-style29 {
            font-size: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="nav-justified">
        <tr>
            <td class="auto-style26">Welcome
    <asp:Label ID="lblAdminUser" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
            </td>
            <td class="auto-style27">
    <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click1" Text="Logout" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table class="nav-justified">
        <tr>
            <td class="auto-style20"><strong>Add Product</strong></td>
        </tr>
        <tr>
            <td class="auto-style24">
    <asp:Button ID="btn_AddProduct" runat="server" Text="Add Product" OnClick="btn_AddProduct_Click" Height="45px" Width="219px" CssClass="auto-style29" />
            </td>
        </tr>
    </table>
    <br />
    <table class="nav-justified">
        <tr>
            <td class="auto-style28"><strong>Products List<br />
                </strong>
                <strong>
                <asp:Label ID="lbl_Error" runat="server" CssClass="auto-style29" ForeColor="Red"></asp:Label>
                </strong>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gv_Product" runat="server" AutoGenerateColumns="False" CellPadding="3" DataKeyNames="ProductID" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1" Height="198px" Width="1286px" OnRowUpdating="gv_Product_RowUpdating" OnRowCancelingEdit="gv_Product_RowCancelingEdit" OnRowDeleting="gv_Product_RowDeleting" OnRowEditing="gv_Product_RowEditing">
        <Columns>
            <asp:TemplateField HeaderText="Product ID" SortExpression="ProductID">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProductID") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Product Name" SortExpression="Product_Name">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="40" Text='<%# Bind("Product_Name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Product_Name") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Product Image">
                <EditItemTemplate>
                    <asp:Image ID="Image2" runat="server" Height="75px" ImageUrl='<%# Eval("Product_Image") %>' Width="75px" />
                    <asp:FileUpload ID="FileUpload2" runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" Height="75px" ImageUrl='<%# Eval("Product_Image") %>' Width="75px" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Product Desc" SortExpression="Product_Desc">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Height="106px" MaxLength="300" Text='<%# Bind("Product_Desc") %>' TextMode="MultiLine" Width="351px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Product_Desc") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Stock" SortExpression="Stock">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" MaxLength="5" Text='<%# Bind("Stock") %>' onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Stock") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price" SortExpression="Price">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Price") %>' MaxLength="7" ></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category" SortExpression="Category">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Category") %>' MaxLength="50"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
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
&nbsp;<asp:SqlDataSource ID="sdsProducts" runat="server" ConnectionString="<%$ ConnectionStrings:KawanConnection %>" SelectCommand="SELECT * FROM [Products]" DeleteCommand="DELETE FROM [Products] WHERE [ProductID] = @ProductID" InsertCommand="INSERT INTO [Products] ([ProductID], [Product_Name], [Product_Image], [Product_Desc], [Stock], [Price], [Category]) VALUES (@ProductID, @Product_Name, @Product_Image, @Product_Desc, @Stock, @Price, @Category)" UpdateCommand="UPDATE [Products] SET [Product_Name] = @Product_Name, [Product_Image] = @Product_Image, [Product_Desc] = @Product_Desc, [Stock] = @Stock, [Price] = @Price, [Category] = @Category WHERE [ProductID] = @ProductID">
        <DeleteParameters>
            <asp:Parameter Name="ProductID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ProductID" Type="Int32" />
            <asp:Parameter Name="Product_Name" Type="String" />
            <asp:Parameter Name="Product_Image" Type="String" />
            <asp:Parameter Name="Product_Desc" Type="String" />
            <asp:Parameter Name="Stock" Type="Int32" />
            <asp:Parameter Name="Price" Type="Decimal" />
            <asp:Parameter Name="Category" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Product_Name" Type="String" />
            <asp:Parameter Name="Product_Image" Type="String" />
            <asp:Parameter Name="Product_Desc" Type="String" />
            <asp:Parameter Name="Stock" Type="Int32" />
            <asp:Parameter Name="Price" Type="Decimal" />
            <asp:Parameter Name="Category" Type="String" />
            <asp:Parameter Name="ProductID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
    <br />
</asp:Content>

