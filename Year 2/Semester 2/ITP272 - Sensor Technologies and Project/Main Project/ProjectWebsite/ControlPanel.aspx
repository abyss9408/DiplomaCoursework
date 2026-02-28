<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ControlPanel.aspx.cs" Inherits="ControlPanel" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Page Content -->
    <!-- Heading Starts Here -->
    <div class="page-heading header-text">
      <div class="container">
        <div class="row">
          <div class="col-md-4">

          </div>
          <div class="col-md-4">
              <h1>Control Panel</h1>
            <p><a href="Default.aspx">Home</a> / <span>Control Panel</span></p>
            </div>
            <div class="col-md-4">
                <img src="gif/control panel.gif" alt="" style="width:50%;height:50%;"/>
            </div>
        </div>
      </div>
    </div>
    <!-- Heading Ends Here -->

    <!-- Services Starts Here -->
    <div class="services-section">
      <div class="container">
          <div class="section-heading">
              <span>Control Panel</span>           
            </div>
          
              <br />
          Control House: <asp:DropDownList ID="ddlAdminControl" runat="server" DataSourceID="SqlDataSource1" DataTextField="House_Id" DataValueField="House_Id" Visible="False"></asp:DropDownList>
          <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT * FROM [House]"></asp:SqlDataSource>
          
        <div class="row">
            <div class="col-md-12">
                <h2>Control Lights</h2>
                <table class="w-100">
                <tr>
                    <td>
                <asp:TextBox ID="tbLights" runat="server" Height="105px" ReadOnly="True" TextMode="MultiLine" Width="321px" BorderStyle="Solid"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                <asp:Button ID="btnSwitchOnLights" runat="server" Text="Switch on Lights" OnClick="btnSwitchOnLights_Click" BorderStyle="Solid" BackColor="Lime" BorderWidth="2px" />          
                <asp:Button ID="btnSwitchOffLights" runat="server" Text="Switch off Lights" OnClick="btnSwitchOffLights_Click" BorderStyle="Solid" BackColor="Lime" BorderWidth="2px" />          
            
                    </td>
                </tr>
                <tr>
                    <td>Lights Mode:
            <asp:Label ID="lblLightsMode" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
            
                <asp:Button ID="btnLightsAuto" runat="server" Text="Auto Mode" OnClick="btnLightsAuto_Click" BorderStyle="Solid" BackColor="Lime" BorderWidth="2px" />          
            
                <asp:Button ID="btnLightsManual" runat="server" Text="Manual Mode" OnClick="btnLightsManual_Click" BorderStyle="Solid" BackColor="Lime" BorderWidth="2px" />          
            
                    </td>
                </tr>
            </table>
                            <br />
            <br />
            <br />
                </div>
        </div>
      </div>
        <div class="row">
            <div class="col-md-12">
                <h2>Control Windows</h2>
                <table class="w-100">
                <tr>
                    <td>
                <asp:TextBox ID="tbWindows" runat="server" Height="105px" ReadOnly="True" TextMode="MultiLine" Width="321px" BorderStyle="Solid"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
            
                <asp:Button ID="btnOpenWindows" runat="server" Text="Open Windows" OnClick="btnOpenWindows_Click" BorderStyle="Solid" BackColor="#0099FF" BorderWidth="2px" />          
                <asp:Button ID="btnCloseWindows" runat="server" Text="Close Windows" OnClick="btnCloseWindows_Click" BorderStyle="Solid" BackColor="#0099FF" BorderWidth="2px" />          
            
                    </td>
                </tr>
                <tr>
                    <td>Windows Mode:
            <asp:Label ID="lblWindowsMode" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
            
                <asp:Button ID="btnWindowsAuto" runat="server" Text="Auto Mode" OnClick="btnWindowsAuto_Click" BorderStyle="Solid" BackColor="#0099FF" BorderWidth="2px" />          
            
                <asp:Button ID="btnWindowsManual" runat="server" Text="Manual Mode" OnClick="btnWindowsManual_Click" BorderStyle="Solid" BackColor="#0099FF" BorderWidth="2px" />          
            
                    </td>
                </tr>
            </table>
                <br />
            <br />
            <br />
                </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <h2>Enable/Disable RFID</h2>
                <table class="w-100">
                <tr>
                    <td>
                        Status:
            <asp:Label ID="lblRfidStatus" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                 
            
                        <asp:Button ID="btnEnableRfid" runat="server" BackColor="#33CC33" BorderStyle="Solid" OnClick="btnEnableRfid_Click" Text="Enable" />
                        <asp:Button ID="btnDisableRfid" runat="server" BackColor="Red" BorderStyle="Solid" OnClick="btnDisableRfid_Click" Text="Disable" />
                 
            
                    </td>
                </tr>
                </table>
                <br />
            <br />
            <br />
                </div>
        </div>
    </div>
    <!-- Services Ends Here -->


   

    
    


    


    
</asp:Content>

