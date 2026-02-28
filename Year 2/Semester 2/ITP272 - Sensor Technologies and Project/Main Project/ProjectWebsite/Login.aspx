<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Heading Starts Here -->
    <div class="page-heading header-text">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <p style="margin: auto; width: 50%; padding: 10px; color: black; font-weight: bold;">Username: 
                  <asp:TextBox ID="tbUsername" runat="server" BorderStyle="None"></asp:TextBox>
              </p>             
              <br />
              <p style="margin: auto; width: 50%; padding: 10px; color: black; font-weight: bold;"> Password:
                  <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" BorderStyle="None"></asp:TextBox>
              </p>
              
              <br />
              <br />
             
              <br />
              <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" BorderStyle="Solid" BackColor="#00CCFF" />
              <br />
              <asp:Label ID="lbl_Error" runat="server" ForeColor="Red"></asp:Label>
          </div>
        </div>
      </div>
    </div>
    <!-- Heading Ends Here -->

    

</asp:Content>

