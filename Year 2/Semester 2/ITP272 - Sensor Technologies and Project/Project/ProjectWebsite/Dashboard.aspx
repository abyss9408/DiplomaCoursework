<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Page Content -->
    <!-- Heading Starts Here -->
    <div class="page-heading header-text">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <h1>Dashboard</h1>
            <p><a href="Default.aspx">Home</a> / <span>Dashboard</span></p>
              <img src="gif/dashboard_new.gif" alt="" style="opacity: 0.5;" />
          </div>
        </div>
      </div>
    </div>
    <!-- Heading Ends Here -->

    <!-- Services Starts Here -->
    <div class="services-section">
      <div class="container">
          <div class="section-heading">
              <span>Dashboard</span>   
              <br />    
              <asp:DropDownList ID="ddlDate" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="Light_Date_Occured" DataValueField="Light_Date_Occured">
                            </asp:DropDownList>     
              (For Light, Water and RFID sensors only)</div>
        <div class="row">
            <div class="col-md-6">
            <h2>Light Sensor data</h2>
                <table class="w-100">
                    <tr>
                        <td>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT DISTINCT [Light_Date_Occured] FROM [LightSensor]"></asp:SqlDataSource>
            <asp:Chart ID="LightChart" runat="server" DataSourceID="SqlDataSource1" Width="400px" Height="400px">
                <series>
                    <asp:Series ChartType="Spline" IsXValueIndexed="True" Name="Light Value" XValueMember="Light_Time_Occured" YValueMembers="Light_Value">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </chartareas>
                <Titles>
                    <asp:Title Name="Light Level Chart">
                    </asp:Title>
                </Titles>
            </asp:Chart>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT * FROM [LightSensor] WHERE ([Light_Date_Occured] = @Light_Date_Occured)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlDate" Name="Light_Date_Occured" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
            <div class="col-md-6">
                <h2>Water Sensor data</h2>
                <br />
            <asp:Chart ID="WaterChart" runat="server" DataSourceID="SqlDataSource3" Height="400px" Width="400px">
                  <Series>
                      <asp:Series Name="Series1" ChartType="Spline" XValueMember="Water_Time_Occured" YValueMembers="Water_Value">
                      </asp:Series>
                  </Series>
                  <ChartAreas>
                      <asp:ChartArea Name="ChartArea1">
                      </asp:ChartArea>
                  </ChartAreas>
                  </asp:Chart>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT * FROM [WaterSensor] WHERE ([Water_Date_Occured] = @Water_Date_Occured)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlDate" Name="Water_Date_Occured" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
          <div class="row">
            <div class="col-md-6">
                <h2>Money Saved ($)</h2>
                <br />
                  <asp:Chart ID="MoneyChart" runat="server" Height="400px" Width="400px" DataSourceID="SqlDataSource4">
                    <Series>
                        <asp:Series Name="Money Saved ($)" XValueMember="Date" YValueMembers="Money_Saved"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                
            </div>
            <div class="col-md-6">
                <h2>Energy Saved (Watts)</h2>
                <br />
                <asp:Chart ID="EnergyChart" runat="server" Height="400px" Width="400px" DataSourceID="SqlDataSource4">
                    <Series>
                        <asp:Series Name="Energy Saved (Watts)" XValueMember="Date" YValueMembers="Energy_Saved"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                    <Titles>
                        <asp:Title Name="Energy Saved (Watts)">
                        </asp:Title>
                    </Titles>
                </asp:Chart>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT * FROM [EnergySaving]"></asp:SqlDataSource>
            </div>
        </div>
          <div class="row">
              <div class="col-md-12">
                  <h2>RFID data</h2>
                <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Rfid_Id" DataSourceID="SqlDataSource5" Height="250px" Width="1142px">
                <Columns>
                    <asp:BoundField DataField="Rfid_Id" HeaderText="Rfid_Id" InsertVisible="False" ReadOnly="True" SortExpression="Rfid_Id" />
                    <asp:BoundField DataField="Rfid_Date_Occured" HeaderText="Rfid_Date_Occured" SortExpression="Rfid_Date_Occured" />
                    <asp:BoundField DataField="Rfid_Time_Occured" HeaderText="Rfid_Time_Occured" SortExpression="Rfid_Time_Occured" />
                    <asp:BoundField DataField="Rfid_Value" HeaderText="Rfid_Value" SortExpression="Rfid_Value" />
                    <asp:BoundField DataField="Rfid_Status" HeaderText="Rfid_Status" SortExpression="Rfid_Status" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT * FROM [RFID] WHERE ([Rfid_Date_Occured] = @Rfid_Date_Occured)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlDate" Name="Rfid_Date_Occured" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
                </asp:SqlDataSource>
              </div>
          </div>
      </div>
    </div>
    <!-- Services Ends Here -->


    


    
    


    


    
</asp:Content>

