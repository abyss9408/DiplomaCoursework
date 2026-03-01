<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Heading Starts Here -->
    <div class="page-heading header-text">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
              <h2>Sign Up</h2>
            <p style="margin: auto; width: 50%; padding: 10px; color: black; font-weight: bold;">Username: 
                  <asp:TextBox ID="tbUsername" runat="server" BorderStyle="None" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter your Username" ControlToValidate="tbUsername"></asp:RequiredFieldValidator>
              </p>             
                            <p style="margin: auto; width: 50%; padding: 10px; color: black; font-weight: bold;"> Password:
                  <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" BorderStyle="None" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter your password" ControlToValidate="tbPassword"></asp:RequiredFieldValidator>
              </p>
              <p style="margin: auto; width: 50%; padding: 10px; color: black; font-weight: bold;"> Confirm Password:
                  <asp:TextBox ID="tbConfirmPassword" runat="server" TextMode="Password" BorderStyle="None" MaxLength="50"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Confirm your password" ControlToValidate="tbConfirmPassword"></asp:RequiredFieldValidator>
                  <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbPassword" ControlToValidate="tbConfirmPassword" ErrorMessage="Passwords do not match"></asp:CompareValidator>
              </p>
              <p style="margin: auto; width: 50%; padding: 10px; color: black; font-weight: bold;"> Email:
                  <asp:TextBox ID="tbEmail" runat="server" TextMode="Email" BorderStyle="None" MaxLength="50"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbEmail" ErrorMessage="Enter Email"></asp:RequiredFieldValidator>
              </p>
              <p style="margin: auto; width: 50%; padding: 10px; color: black; font-weight: bold;">Name: 
                  <asp:TextBox ID="tbName" runat="server" BorderStyle="None" MaxLength="50" onkeydown="return(event.keyCode>=65 && event.keyCode<=90 || event.keyCode==32 || event.keyCode==8)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Enter your Name" ControlToValidate="tbName"></asp:RequiredFieldValidator>
              </p> 
              <p style="margin: auto; width: 50%; padding: 10px; color: black; font-weight: bold;"> Phone Number:
                  <asp:TextBox ID="tbPhoneNumber" runat="server" BorderStyle="None" MaxLength="10" onkeydown="return (!(event.keyCode>=65 && event.keyCode<=90 ) && event.keyCode!=32);"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbPhoneNumber" ErrorMessage="Enter Phone Number"></asp:RequiredFieldValidator>
              </p>
              <p style="margin: auto; width: 50%; padding: 10px; color: black; font-weight: bold;"> Address:
                  <asp:TextBox ID="tbAddress" runat="server" TextMode="MultiLine" BorderStyle="None" Height="29px" Width="216px" MaxLength="200"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbAddress" ErrorMessage="Enter Address"></asp:RequiredFieldValidator>
              </p>
              <p style="margin: auto; width: 50%; padding: 10px; color: black; font-weight: bold;"> Postal Code:
                  <asp:TextBox ID="tbPostalCode" runat="server" BorderStyle="None" MaxLength="10" onkeydown="return (!(event.keyCode>=65 && event.keyCode<=90 ) && event.keyCode!=32);"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbPostalCode" ErrorMessage="Enter Postal Code"></asp:RequiredFieldValidator>
              </p>
              <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" OnClick="btnSignUp_Click" BorderStyle="Solid" BackColor="#0099FF" />
              <br />
              <asp:Label ID="lbl_Error" runat="server" ForeColor="Red"></asp:Label>
          </div>
        </div>
      </div>
    </div>
    <!-- Heading Ends Here -->

    

</asp:Content>

