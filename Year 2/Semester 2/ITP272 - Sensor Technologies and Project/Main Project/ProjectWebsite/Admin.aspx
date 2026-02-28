<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

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
            <h1>Administration</h1>
            <p><a href="Default.aspx">Home</a> / <span>Administration</span></p>
          </div>
        </div>
      </div>
    </div>
    <!-- Heading Ends Here -->

    <!-- Services Starts Here -->
    <div class="services-section">
      <div class="container">
          <div class="section-heading">
              <span>Administration</span>             
            </div>
          <asp:DropDownList ID="ddlAdminOption" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDisplayUser_SelectedIndexChanged">
              <asp:ListItem>Assign RFID</asp:ListItem>
              <asp:ListItem>Display House 1</asp:ListItem>
              <asp:ListItem>Display House 2</asp:ListItem>
              <asp:ListItem>Display House 3</asp:ListItem>
              </asp:DropDownList>
          
          <asp:PlaceHolder ID="AssignRFID" runat="server">
              <div class="row">                        
            <div class="col-md-12">
                <h2>Users List</h2> 
                <br />
                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource9">
              <Columns>
                  <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                  <asp:BoundField DataField="House_Id" HeaderText="House_Id" SortExpression="House_Id" />
                  <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
                  <asp:BoundField DataField="Account_Type" HeaderText="Account_Type" SortExpression="Account_Type" />
                  <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                  <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                  <asp:BoundField DataField="Phone_Number" HeaderText="Phone_Number" SortExpression="Phone_Number" />
                  <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                  <asp:BoundField DataField="Postal_code" HeaderText="Postal_code" SortExpression="Postal_code" />
              </Columns>
          </asp:GridView>
          <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT [Id], [House_Id], [Username], [Account_Type], [Email], [Name], [Phone_Number], [Address], [Postal_code] FROM [Login]"></asp:SqlDataSource>      
                <asp:DropDownList ID="ddlAssignRFID" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource6" DataTextField="Valid_Rfid_Value" DataValueField="Valid_Rfid_Value"></asp:DropDownList>
          <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT * FROM [Valid_Rfid] WHERE ([Used] = @Used)">
              <SelectParameters>
                  <asp:Parameter DefaultValue="No" Name="Used" Type="String" />
              </SelectParameters>
          </asp:SqlDataSource>
                <br />
                <br />
                User ID: <asp:TextBox ID="tbAssignRFID" runat="server"></asp:TextBox>
                <br />
                <br />
                <br />
                <br />
                <asp:Button ID="btnAssign" runat="server" Text="Assign" OnClick="btnAssign_Click" BackColor="#33CC33" BorderStyle="Solid" />
                <br />
                <br />
                <br />
            </div>
                  </div>           
          </asp:PlaceHolder>         
          <asp:PlaceHolder ID="User1" runat="server" Visible="False">
              <asp:DropDownList ID="ddlHouse1Filter" runat="server" DataSourceID="SqlDataSource5" DataTextField="Light_Date_Occured" DataValueField="Light_Date_Occured" AutoPostBack="True"></asp:DropDownList>
          <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT DISTINCT [Light_Date_Occured] FROM [LightSensor]"></asp:SqlDataSource>
              <div class="row">                        
            <div class="col-md-6">
                <h2>Light Sensor data (house 1)</h2> 
                <br />
                <asp:Chart ID="LightChart" runat="server" DataSourceID="SqlDataSource1" Width="411px" Height="304px">
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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT * FROM [LightSensor] WHERE ([Light_Date_Occured] = @Light_Date_Occured)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlHouse1Filter" Name="Light_Date_Occured" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
                </asp:SqlDataSource>
            </div>           
            
            <div class="col-md-6">
                <h2>Water Sensor data (house 1)</h2>
            <br />
            <asp:Chart ID="WaterChart" runat="server" DataSourceID="SqlDataSource3" Height="304px" Width="411px">
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
                    <asp:ControlParameter ControlID="ddlHouse1Filter" Name="Water_Date_Occured" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <h2>RFID data (house 1)</h2>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Rfid_Id" DataSourceID="SqlDataSource2">
                    <Columns>
                        <asp:BoundField DataField="Rfid_Id" HeaderText="Rfid_Id" InsertVisible="False" ReadOnly="True" SortExpression="Rfid_Id" />
                        <asp:BoundField DataField="House_Id" HeaderText="House_Id" SortExpression="House_Id" />
                        <asp:BoundField DataField="Rfid_Date_Occured" HeaderText="Rfid_Date_Occured" SortExpression="Rfid_Date_Occured" />
                        <asp:BoundField DataField="Rfid_Time_Occured" HeaderText="Rfid_Time_Occured" SortExpression="Rfid_Time_Occured" />
                        <asp:BoundField DataField="Rfid_Value" HeaderText="Rfid_Value" SortExpression="Rfid_Value" />
                        <asp:BoundField DataField="Rfid_Status" HeaderText="Rfid_Status" SortExpression="Rfid_Status" />
                    </Columns>
                </asp:GridView>
            <br />
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT * FROM [RFID] WHERE ([Rfid_Date_Occured] = @Rfid_Date_Occured)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlHouse1Filter" Name="Rfid_Date_Occured" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
                </asp:SqlDataSource>   
            </div>
            <div class="col-md-6">
                  <h2>Energy Saved (Watts)(house 1)</h2>
                  <br />
                  <asp:Chart ID="EnergyChart1" runat="server" DataSourceID="SqlDataSource4">
                      <Series>
                          <asp:Series Name="Energy Saved ($)" XValueMember="Date" YValueMembers="Energy_Saved"></asp:Series>
                      </Series>
                      <ChartAreas>
                          <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                      </ChartAreas>
                  </asp:Chart>
          <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT * FROM [EnergySaving]"></asp:SqlDataSource>                 
            </div>          
            
        </div>
        <div class="row">
            <div class="col-md-6">
                <h2>Money Saved ($)(house 1)</h2>
                <br />
                <asp:Chart ID="MoneyChart1" runat="server" DataSourceID="SqlDataSource4">
                    <Series>
                        <asp:Series Name="Series1" XValueMember="Date" YValueMembers="Money_Saved"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>
            <div class="col-md-6">
            </div>
        </div>
          </asp:PlaceHolder>
          
          <asp:PlaceHolder ID="User2" runat="server" Visible="False">
              <div class="row">        
            <div class="col-md-6">
            
                <h2>Light Sensor data (house 2)</h2> 
                <br />
                <asp:Chart ID="LightChart2" runat="server" Width="411px" Height="304px">
                <series>
                    <asp:Series ChartType="Spline" IsXValueIndexed="True" Name="Light Value">
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
            </div>           
            
            <div class="col-md-6">
                <h2>Water Sensor data (house 2)</h2>
            <br />
            <asp:Chart ID="WaterChart2" runat="server" Height="304px" Width="411px">
                  <Series>
                      <asp:Series Name="Water Value" ChartType="Spline">
                      </asp:Series>
                  </Series>
                  <ChartAreas>
                      <asp:ChartArea Name="ChartArea1">
                      </asp:ChartArea>
                  </ChartAreas>
                  </asp:Chart>
            
            </div>
            
            
        </div>
          <div class="row">
            <div class="col-md-6">
                <h2>RFID data (house 2)</h2>
            <br />
            
            
            <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT count(*) as Taps, [Rfid_Value] FROM [RFID] GROUP BY [Rfid_Value]"></asp:SqlDataSource>   
            </div>
              <div class="col-md-6">
                  <h2>Motion Data (house 2)</h2>
                  <br />
            
            <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:SmartHomeDBConnectionString %>" SelectCommand="SELECT * FROM [MotionSensor]"></asp:SqlDataSource>
            </div>          
            
        </div>
          </asp:PlaceHolder>
          <asp:PlaceHolder ID="User3" runat="server" Visible="False">
              <div class="row">        
            <div class="col-md-6">
            
                <h2>Light Sensor data (house 3)</h2> 
                <br />
                <asp:Chart ID="LightChart3" runat="server" Width="411px" Height="304px">
                <series>
                    <asp:Series ChartType="Spline" IsXValueIndexed="True" Name="Light Value">
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
            </div>           
            
            <div class="col-md-6">
                <h2>Water Sensor data (house 3)</h2>
            <br />
            <asp:Chart ID="WaterChart3" runat="server" Height="304px" Width="411px">
                  <Series>
                      <asp:Series Name="Water Value" ChartType="Spline">
                      </asp:Series>
                  </Series>
                  <ChartAreas>
                      <asp:ChartArea Name="ChartArea1">
                      </asp:ChartArea>
                  </ChartAreas>
                  </asp:Chart>
            
            </div>
            
            
        </div>
          <div class="row">
            <div class="col-md-6">
                <h2>RFID data (house 3)</h2>
            <br />
            

            </div>
              <div class="col-md-6">
                  <h2>Motion Data (house 3)</h2>
                  <br />


            </div>          
            
        </div>
          </asp:PlaceHolder>
      </div>    
    </div>
    <!-- Services Ends Here -->


    


    
    


    


    
</asp:Content>

