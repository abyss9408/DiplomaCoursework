<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<%@ Register assembly="Recaptcha.Web" namespace="Recaptcha.Web.UI.Controls" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style4 {
            height: 38px;
        }
        .auto-style6 {
            height: 38px;
            text-align: right;
        }
        .auto-style7 {
            height: 74px;
        }
        .auto-style10 {
            text-align: right;
            height: 33px;
        }
        .auto-style11 {
            height: 33px;
        }
        .auto-style12 {
            text-align: right;
            height: 35px;
        }
        .auto-style13 {
            height: 35px;
        }
        .auto-style14 {
            text-align: right;
            height: 50px;
        }
        .auto-style15 {
            height: 50px;
        }
        .auto-style16 {
            height: 50px;
            width: 14px;
        }
        .auto-style17 {
            height: 38px;
            text-align: right;
            width: 14px;
        }
        .auto-style18 {
            text-align: right;
            height: 35px;
            width: 14px;
        }
        .auto-style19 {
            text-align: right;
            height: 33px;
            width: 14px;
        }
        .auto-style20 {
            text-align: right;
            height: 50px;
            width: 14px;
        }
        .auto-style22 {
            height: 74px;
            width: 14px;
        }
        .auto-style24 {
            height: 50px;
            text-align: center;
        }
        .auto-style31 {
            text-align: right;
            height: 52px;
        }
        .auto-style32 {
            text-align: right;
            height: 52px;
            width: 14px;
        }
        .auto-style33 {
            height: 52px;
        }
        .auto-style34 {
            text-align: right;
            height: 127px;
        }
        .auto-style35 {
            text-align: right;
            height: 127px;
            width: 14px;
        }
        .auto-style36 {
            height: 127px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="nav-justified">
        <tr>
            <td class="auto-style24">
                <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" Font-Size="Small" CausesValidation="false" PostBackUrl="~/Login/Login.aspx">Back to Login</asp:LinkButton>
            </td>
            <td class="auto-style16">
                &nbsp;</td>
            <td class="auto-style15">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style24">
                <asp:Label ID="Label1" runat="server" Text="Customer's Details:" Font-Bold="True" Font-Size="Large"></asp:Label>
            </td>
            <td class="auto-style16">
            </td>
            <td class="auto-style15"></td>
        </tr>
        <tr>
            <td class="auto-style6">
                <asp:Label ID="Label2" runat="server" Text="Full Name(As per stated on passport):"></asp:Label>
            </td>
            <td class="auto-style17">
                &nbsp;</td>
            <td class="auto-style4">
                <asp:TextBox ID="txtFullName" runat="server" Width="310px" MaxLength="50" onkeydown="return(event.keyCode>=65 && event.keyCode<=90 || event.keyCode==32 || event.keyCode==8)" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter your Full Name" ControlToValidate="txtFullName" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style12">
                <asp:Label ID="Label3" runat="server" Text="Date Of Birth:"></asp:Label>
            </td>
            <td class="auto-style18">
                &nbsp;</td>
            <td class="auto-style13">
                <asp:DropDownList ID="ddlDay" runat="server">
                    <asp:ListItem>Day</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>13</asp:ListItem>
                    <asp:ListItem>14</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>16</asp:ListItem>
                    <asp:ListItem>17</asp:ListItem>
                    <asp:ListItem>18</asp:ListItem>
                    <asp:ListItem>19</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>21</asp:ListItem>
                    <asp:ListItem>22</asp:ListItem>
                    <asp:ListItem>23</asp:ListItem>
                    <asp:ListItem>24</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>26</asp:ListItem>
                    <asp:ListItem>27</asp:ListItem>
                    <asp:ListItem>28</asp:ListItem>
                    <asp:ListItem>29</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>31</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlMonth" runat="server">
                    <asp:ListItem>Month</asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlYear" runat="server">
                    <asp:ListItem>Year</asp:ListItem>
                    <asp:ListItem>1950</asp:ListItem>
                    <asp:ListItem>1951</asp:ListItem>
                    <asp:ListItem>1952</asp:ListItem>
                    <asp:ListItem>1953</asp:ListItem>
                    <asp:ListItem>1954</asp:ListItem>
                    <asp:ListItem>1955</asp:ListItem>
                    <asp:ListItem>1956</asp:ListItem>
                    <asp:ListItem>1957</asp:ListItem>
                    <asp:ListItem>1958</asp:ListItem>
                    <asp:ListItem>1959</asp:ListItem>
                    <asp:ListItem>1960</asp:ListItem>
                    <asp:ListItem>1961</asp:ListItem>
                    <asp:ListItem>1962</asp:ListItem>
                    <asp:ListItem>1963</asp:ListItem>
                    <asp:ListItem>1964</asp:ListItem>
                    <asp:ListItem>1965</asp:ListItem>
                    <asp:ListItem>1966</asp:ListItem>
                    <asp:ListItem>1968</asp:ListItem>
                    <asp:ListItem>1969</asp:ListItem>
                    <asp:ListItem>1970</asp:ListItem>
                    <asp:ListItem>1971</asp:ListItem>
                    <asp:ListItem>1972</asp:ListItem>
                    <asp:ListItem>1973</asp:ListItem>
                    <asp:ListItem>1974</asp:ListItem>
                    <asp:ListItem>1975</asp:ListItem>
                    <asp:ListItem>1976</asp:ListItem>
                    <asp:ListItem>1977</asp:ListItem>
                    <asp:ListItem>1978</asp:ListItem>
                    <asp:ListItem>1979</asp:ListItem>
                    <asp:ListItem>1980</asp:ListItem>
                    <asp:ListItem>1981</asp:ListItem>
                    <asp:ListItem>1982</asp:ListItem>
                    <asp:ListItem>1983</asp:ListItem>
                    <asp:ListItem>1984</asp:ListItem>
                    <asp:ListItem>1985</asp:ListItem>
                    <asp:ListItem>1986</asp:ListItem>
                    <asp:ListItem>1987</asp:ListItem>
                    <asp:ListItem>1988</asp:ListItem>
                    <asp:ListItem>1989</asp:ListItem>
                    <asp:ListItem>1990</asp:ListItem>
                    <asp:ListItem>1991</asp:ListItem>
                    <asp:ListItem>1992</asp:ListItem>
                    <asp:ListItem>1993</asp:ListItem>
                    <asp:ListItem>1994</asp:ListItem>
                    <asp:ListItem>1995</asp:ListItem>
                    <asp:ListItem>1996</asp:ListItem>
                    <asp:ListItem>1997</asp:ListItem>
                    <asp:ListItem>1998</asp:ListItem>
                    <asp:ListItem>1999</asp:ListItem>
                    <asp:ListItem>2000</asp:ListItem>
                    <asp:ListItem>2001</asp:ListItem>
                    <asp:ListItem>2002</asp:ListItem>
                    <asp:ListItem>2003</asp:ListItem>
                    <asp:ListItem>2004</asp:ListItem>
                    <asp:ListItem>2005</asp:ListItem>
                    <asp:ListItem>2006</asp:ListItem>
                    <asp:ListItem>2007</asp:ListItem>
                    <asp:ListItem>2008</asp:ListItem>
                    <asp:ListItem>2009</asp:ListItem>
                    <asp:ListItem>2010</asp:ListItem>
                    <asp:ListItem>2011</asp:ListItem>
                    <asp:ListItem>2012</asp:ListItem>
                    <asp:ListItem>2013</asp:ListItem>
                    <asp:ListItem>2014</asp:ListItem>
                    <asp:ListItem>2015</asp:ListItem>
                    <asp:ListItem>2016</asp:ListItem>
                    <asp:ListItem>2017</asp:ListItem>
                    <asp:ListItem>2018</asp:ListItem>
                    <asp:ListItem>2019</asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlDay" ErrorMessage="Select a day(Date of Birth)" ForeColor="Red" Operator="NotEqual" ValueToCompare="Day"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlMonth" ErrorMessage="Select a Month(Date Of Birth)" ForeColor="Red" Operator="NotEqual" ValueToCompare="Month"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlYear" ErrorMessage="Select a Year(Date Of Birth)" ForeColor="Red" Operator="NotEqual" ValueToCompare="Year"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style10">
                <asp:Label ID="Label4" runat="server" Text="Prefer Salutation:"></asp:Label>
            </td>
            <td class="auto-style19">
                &nbsp;</td>
            <td class="auto-style11">
                <asp:DropDownList ID="ddlSalutation" runat="server">
                    <asp:ListItem>Mr</asp:ListItem>
                    <asp:ListItem>Miss</asp:ListItem>
                    <asp:ListItem>Mrs</asp:ListItem>
                    <asp:ListItem>Mdm</asp:ListItem>
                    <asp:ListItem>Dr</asp:ListItem>
                    <asp:ListItem>Professor</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:Label ID="Label7" runat="server" Text="Email Address:"></asp:Label>
            </td>
            <td class="auto-style20">
                </td>
            <td class="auto-style15">
                <asp:TextBox ID="txtEmail" runat="server" Width="305px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Enter your Email" ControlToValidate="txtEmail" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid Email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:Label ID="Label21" runat="server" Text="Phone Number:"></asp:Label>
            </td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <asp:TextBox ID="txtMobileNo" runat="server" Width="305px" MaxLength="9" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Enter your Phone Number" ControlToValidate="txtMobileNo" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Invalid Phone Number" ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:Label ID="Label8" runat="server" Text="Username:"></asp:Label>
            </td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <asp:TextBox ID="txtRegUser" runat="server" Width="305px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="Enter your Username" ControlToValidate="txtRegUser" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style31">
                <asp:Label ID="Label9" runat="server" Text="Password:"></asp:Label>
            </td>
            <td class="auto-style32">
                </td>
            <td class="auto-style33">
                <asp:TextBox ID="txtRegPass" runat="server" Width="305px" TextMode="Password" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Enter your Password" ControlToValidate="txtRegPass" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style34">
            </td>
            <td class="auto-style35">
                </td>
            <td class="auto-style36">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtRegPass" ErrorMessage="Password must contain at least &lt;br /&gt;- 8 characters &lt;/br&gt;- 1 digit (0-9)&lt;/br&gt;- 1 lower case character (a-z) &lt;br/&gt;- 1 upper case character (A-Z) &lt;/br&gt; - 1 special character (eg. !@#$%)" ForeColor="Red" ValidationExpression="(?=^.{8,}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:Label ID="Label10" runat="server" Text="Confirm Password:"></asp:Label>
            </td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <asp:TextBox ID="txtConfirmPass" runat="server" Width="305px" TextMode="Password" MaxLength="50"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtRegPass" ControlToValidate="txtConfirmPass" ErrorMessage="Passwords do not match" ForeColor="Red"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style24">
                <asp:Label ID="Label11" runat="server" Text="Billing Information:" Font-Bold="True" Font-Size="Large"></asp:Label>
            </td>
            <td class="auto-style16">
            </td>
            <td class="auto-style15"></td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:Label ID="Label12" runat="server" Text="Country:"></asp:Label>
            </td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <asp:DropDownList ID="ddlCountry" runat="server">
                    <asp:ListItem>Country</asp:ListItem>
                    <asp:ListItem>Afghanistan</asp:ListItem>
                    <asp:ListItem>Albania</asp:ListItem>
                    <asp:ListItem>Algeria</asp:ListItem>
                    <asp:ListItem>Andorra</asp:ListItem>
                    <asp:ListItem>Angola</asp:ListItem>
                    <asp:ListItem>Antigua and Barbuda</asp:ListItem>
                    <asp:ListItem>Argentina</asp:ListItem>
                    <asp:ListItem>Armenia</asp:ListItem>
                    <asp:ListItem>Australia</asp:ListItem>
                    <asp:ListItem>Austria</asp:ListItem>
                    <asp:ListItem>Azerbaijan</asp:ListItem>
                    <asp:ListItem>The Bahamas</asp:ListItem>
                    <asp:ListItem>Bahrain</asp:ListItem>
                    <asp:ListItem>Bangladesh</asp:ListItem>
                    <asp:ListItem>Barbados</asp:ListItem>
                    <asp:ListItem>Belarus</asp:ListItem>
                    <asp:ListItem>Belgium</asp:ListItem>
                    <asp:ListItem>Belize</asp:ListItem>
                    <asp:ListItem>Benin</asp:ListItem>
                    <asp:ListItem>Bhutan</asp:ListItem>
                    <asp:ListItem>Bolivia</asp:ListItem>
                    <asp:ListItem>Bosnia and Herzegovina</asp:ListItem>
                    <asp:ListItem>Botswana</asp:ListItem>
                    <asp:ListItem>Brazil</asp:ListItem>
                    <asp:ListItem>Brunei</asp:ListItem>
                    <asp:ListItem>Bulgaria</asp:ListItem>
                    <asp:ListItem>Burkina Faso</asp:ListItem>
                    <asp:ListItem>Burundi</asp:ListItem>
                    <asp:ListItem>Cabo Verde</asp:ListItem>
                    <asp:ListItem>Cambodia</asp:ListItem>
                    <asp:ListItem>Cameroon</asp:ListItem>
                    <asp:ListItem>Canada</asp:ListItem>
                    <asp:ListItem>Central African Republic</asp:ListItem>
                    <asp:ListItem>Chad</asp:ListItem>
                    <asp:ListItem>Chile</asp:ListItem>
                    <asp:ListItem>China</asp:ListItem>
                    <asp:ListItem>Colombia</asp:ListItem>
                    <asp:ListItem>Comoros</asp:ListItem>
                    <asp:ListItem>Congo, Democratic Republic of the</asp:ListItem>
                    <asp:ListItem>Congo, Republic of the</asp:ListItem>
                    <asp:ListItem>Costa Rica</asp:ListItem>
                    <asp:ListItem>Côte d’Ivoire</asp:ListItem>
                    <asp:ListItem>Croatia</asp:ListItem>
                    <asp:ListItem>Cuba</asp:ListItem>
                    <asp:ListItem>Cyprus</asp:ListItem>
                    <asp:ListItem>Czech Republic</asp:ListItem>
                    <asp:ListItem>Denmark</asp:ListItem>
                    <asp:ListItem>Djibouti</asp:ListItem>
                    <asp:ListItem>Dominica</asp:ListItem>
                    <asp:ListItem>Dominican Republic</asp:ListItem>
                    <asp:ListItem>East Timor (Timor-Leste)</asp:ListItem>
                    <asp:ListItem>Ecuador</asp:ListItem>
                    <asp:ListItem>Egypt</asp:ListItem>
                    <asp:ListItem>El Salvador</asp:ListItem>
                    <asp:ListItem>Equatorial Guinea</asp:ListItem>
                    <asp:ListItem>Eritrea</asp:ListItem>
                    <asp:ListItem>Estonia</asp:ListItem>
                    <asp:ListItem>Eswatini</asp:ListItem>
                    <asp:ListItem>Ethiopia</asp:ListItem>
                    <asp:ListItem>Fiji</asp:ListItem>
                    <asp:ListItem>Finland</asp:ListItem>
                    <asp:ListItem>France</asp:ListItem>
                    <asp:ListItem>Gabon</asp:ListItem>
                    <asp:ListItem>The Gambia</asp:ListItem>
                    <asp:ListItem>Georgia</asp:ListItem>
                    <asp:ListItem>Germany</asp:ListItem>
                    <asp:ListItem>Ghana</asp:ListItem>
                    <asp:ListItem>Greece</asp:ListItem>
                    <asp:ListItem>Grenada</asp:ListItem>
                    <asp:ListItem>Guatemala</asp:ListItem>
                    <asp:ListItem>Guinea</asp:ListItem>
                    <asp:ListItem>Guinea-Bissau</asp:ListItem>
                    <asp:ListItem>Guyana</asp:ListItem>
                    <asp:ListItem>Haiti</asp:ListItem>
                    <asp:ListItem>Honduras</asp:ListItem>
                    <asp:ListItem>Hungary</asp:ListItem>
                    <asp:ListItem>Iceland</asp:ListItem>
                    <asp:ListItem>India</asp:ListItem>
                    <asp:ListItem>Indonesia</asp:ListItem>
                    <asp:ListItem>Iran</asp:ListItem>
                    <asp:ListItem>Iraq</asp:ListItem>
                    <asp:ListItem>Ireland</asp:ListItem>
                    <asp:ListItem>Israel</asp:ListItem>
                    <asp:ListItem>Italy</asp:ListItem>
                    <asp:ListItem>Jamaica</asp:ListItem>
                    <asp:ListItem>Japan</asp:ListItem>
                    <asp:ListItem>Jordan</asp:ListItem>
                    <asp:ListItem>Kazakhstan</asp:ListItem>
                    <asp:ListItem>Kenya</asp:ListItem>
                    <asp:ListItem>Kiribati</asp:ListItem>
                    <asp:ListItem>Korea, North</asp:ListItem>
                    <asp:ListItem>Korea, South</asp:ListItem>
                    <asp:ListItem>Kosovo</asp:ListItem>
                    <asp:ListItem>Kuwait</asp:ListItem>
                    <asp:ListItem>Kyrgyzstan</asp:ListItem>
                    <asp:ListItem>Laos</asp:ListItem>
                    <asp:ListItem>Latvia</asp:ListItem>
                    <asp:ListItem>Lebanon</asp:ListItem>
                    <asp:ListItem>Lesotho</asp:ListItem>
                    <asp:ListItem>Liberia</asp:ListItem>
                    <asp:ListItem>Libya</asp:ListItem>
                    <asp:ListItem>Liechtenstein</asp:ListItem>
                    <asp:ListItem>Lithuania</asp:ListItem>
                    <asp:ListItem>Luxembourg</asp:ListItem>
                    <asp:ListItem>Madagascar</asp:ListItem>
                    <asp:ListItem>Malawi</asp:ListItem>
                    <asp:ListItem>Malaysia</asp:ListItem>
                    <asp:ListItem>Maldives</asp:ListItem>
                    <asp:ListItem>Mali</asp:ListItem>
                    <asp:ListItem>Malta</asp:ListItem>
                    <asp:ListItem>Marshall Islands</asp:ListItem>
                    <asp:ListItem>Mauritania</asp:ListItem>
                    <asp:ListItem>Mauritius</asp:ListItem>
                    <asp:ListItem>Mexico</asp:ListItem>
                    <asp:ListItem>Micronesia, Federated States of</asp:ListItem>
                    <asp:ListItem>Moldova</asp:ListItem>
                    <asp:ListItem>Monaco</asp:ListItem>
                    <asp:ListItem>Mongolia</asp:ListItem>
                    <asp:ListItem>Montenegro</asp:ListItem>
                    <asp:ListItem>Morocco</asp:ListItem>
                    <asp:ListItem>Mozambique</asp:ListItem>
                    <asp:ListItem>Myanmar (Burma)</asp:ListItem>
                    <asp:ListItem>Namibia</asp:ListItem>
                    <asp:ListItem>Nauru</asp:ListItem>
                    <asp:ListItem>Nepal</asp:ListItem>
                    <asp:ListItem>Netherlands</asp:ListItem>
                    <asp:ListItem>New Zealand</asp:ListItem>
                    <asp:ListItem>Nicaragua</asp:ListItem>
                    <asp:ListItem>Niger</asp:ListItem>
                    <asp:ListItem>Nigeria</asp:ListItem>
                    <asp:ListItem>North Macedonia</asp:ListItem>
                    <asp:ListItem>Norway</asp:ListItem>
                    <asp:ListItem>Oman</asp:ListItem>
                    <asp:ListItem>Pakistan</asp:ListItem>
                    <asp:ListItem>Palau</asp:ListItem>
                    <asp:ListItem>Panama</asp:ListItem>
                    <asp:ListItem>Papua New Guinea</asp:ListItem>
                    <asp:ListItem>Paraguay</asp:ListItem>
                    <asp:ListItem>Peru</asp:ListItem>
                    <asp:ListItem>Philippines</asp:ListItem>
                    <asp:ListItem>Poland</asp:ListItem>
                    <asp:ListItem>Portugal</asp:ListItem>
                    <asp:ListItem>Qatar</asp:ListItem>
                    <asp:ListItem>Romania</asp:ListItem>
                    <asp:ListItem>Russia</asp:ListItem>
                    <asp:ListItem>Rwanda</asp:ListItem>
                    <asp:ListItem>Saint Kitts and Nevis</asp:ListItem>
                    <asp:ListItem>Saint Lucia</asp:ListItem>
                    <asp:ListItem>Saint Vincent and the Grenadines</asp:ListItem>
                    <asp:ListItem>Samoa</asp:ListItem>
                    <asp:ListItem>San Marino</asp:ListItem>
                    <asp:ListItem>Sao Tome and Principe</asp:ListItem>
                    <asp:ListItem>Saudi Arabia</asp:ListItem>
                    <asp:ListItem>Senegal</asp:ListItem>
                    <asp:ListItem>Serbia</asp:ListItem>
                    <asp:ListItem>Seychelles</asp:ListItem>
                    <asp:ListItem>Sierra Leone</asp:ListItem>
                    <asp:ListItem>Singapore</asp:ListItem>
                    <asp:ListItem>Slovakia</asp:ListItem>
                    <asp:ListItem>Slovenia</asp:ListItem>
                    <asp:ListItem>Solomon Islands</asp:ListItem>
                    <asp:ListItem>Somalia</asp:ListItem>
                    <asp:ListItem>South Africa</asp:ListItem>
                    <asp:ListItem>Spain</asp:ListItem>
                    <asp:ListItem>Sri Lanka</asp:ListItem>
                    <asp:ListItem>Sudan</asp:ListItem>
                    <asp:ListItem>Sudan, South</asp:ListItem>
                    <asp:ListItem>Suriname</asp:ListItem>
                    <asp:ListItem>Sweden</asp:ListItem>
                    <asp:ListItem>Switzerland</asp:ListItem>
                    <asp:ListItem>Syria</asp:ListItem>
                    <asp:ListItem>Taiwan</asp:ListItem>
                    <asp:ListItem>Tajikistan</asp:ListItem>
                    <asp:ListItem>Tanzania</asp:ListItem>
                    <asp:ListItem>Thailand</asp:ListItem>
                    <asp:ListItem>Togo</asp:ListItem>
                    <asp:ListItem>Tonga</asp:ListItem>
                    <asp:ListItem>Trinidad and Tobago</asp:ListItem>
                    <asp:ListItem>Tunisia</asp:ListItem>
                    <asp:ListItem>Turkey</asp:ListItem>
                    <asp:ListItem>Turkmenistan</asp:ListItem>
                    <asp:ListItem>Tuvalu</asp:ListItem>
                    <asp:ListItem>Uganda</asp:ListItem>
                    <asp:ListItem>Ukraine</asp:ListItem>
                    <asp:ListItem>United Arab Emirates</asp:ListItem>
                    <asp:ListItem>United Kingdom</asp:ListItem>
                    <asp:ListItem>United States</asp:ListItem>
                    <asp:ListItem>Uruguay</asp:ListItem>
                    <asp:ListItem>Uzbekistan</asp:ListItem>
                    <asp:ListItem>Vanuatu</asp:ListItem>
                    <asp:ListItem>Vatican City</asp:ListItem>
                    <asp:ListItem>Venezuela</asp:ListItem>
                    <asp:ListItem>Vietnam</asp:ListItem>
                    <asp:ListItem>Yemen</asp:ListItem>
                    <asp:ListItem>Zambia</asp:ListItem>
                    <asp:ListItem>Zimbabwe</asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Select a Country" ForeColor="Red" Operator="NotEqual" ValueToCompare="Country">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:Label ID="Label13" runat="server" Text="Address:"></asp:Label>
            </td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <asp:TextBox ID="txtAddress" runat="server" Width="473px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="Enter your Address" ControlToValidate="txtAddress" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:Label ID="Label14" runat="server" Text="City:"></asp:Label>
            </td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <asp:TextBox ID="txtCity" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Enter your City" ControlToValidate="txtCity" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:Label ID="Label15" runat="server" Text="State/Province:"></asp:Label>
            </td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <asp:TextBox ID="txtStateProvince" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="Enter your State/Province" ControlToValidate="txtStateProvince" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:Label ID="Label16" runat="server" Text="Zip/Postal Code:"></asp:Label>
            </td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <asp:TextBox ID="txtZipPostalCode" runat="server" MaxLength="8" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="Enter your Zip/Postal Code" ControlToValidate="txtZipPostalCode" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtZipPostalCode" ErrorMessage="Zip/Postal Code must be numeric" ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:Label ID="Label17" runat="server" Text="Credit/Debit Card Number:"></asp:Label>
            </td>
            <td class="auto-style20">
                </td>
            <td class="auto-style15">
                <asp:TextBox ID="txtCardNum" runat="server" Width="303px" MaxLength="16" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="Enter your Credit/Debit Card Number" ControlToValidate="txtCardNum" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCardNum" ErrorMessage="Card Number must be numeric" ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:Label ID="Label18" runat="server" Text="Name On Card:"></asp:Label>
            </td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <asp:TextBox ID="txtNameOnCard" runat="server" Width="303px" MaxLength="50" onkeydown="return(event.keyCode>=65 && event.keyCode<=90 || event.keyCode==32 || event.keyCode==8)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="Enter your Name On Card" ControlToValidate="txtNameOnCard" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:Label ID="Label19" runat="server" Text="Expiry Of Card(MM/YYYY format):"></asp:Label>
            </td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <asp:TextBox ID="txtExpiry" runat="server" Width="197px" MaxLength="7"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="Enter your Card Expiry" ControlToValidate="txtExpiry" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtExpiry" ErrorMessage="Card Expiry must be in MM/YYYY format" ForeColor="Red" ValidationExpression="(0[1-9]|10|11|12)/20[0-9]{2}$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                <asp:Label ID="Label20" runat="server" Text="CVV:"></asp:Label>
            </td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <asp:TextBox ID="txtCVV" runat="server" Width="118px" MaxLength="3" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ErrorMessage="Enter your CVV" ControlToValidate="txtCVV" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCVV" ErrorMessage="CVV must be numeric" ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                Security Question:</td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <asp:DropDownList ID="ddlSecurityQn" runat="server" DataSourceID="sdsSecQns" DataTextField="Question" DataValueField="Question">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                Answer:</td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <asp:TextBox ID="txtAns" runat="server" Width="314px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtAns" ErrorMessage="Enter your answer for security question" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style14">
                &nbsp;</td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style15">
                <cc1:Recaptcha ID="Recaptcha1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="auto-style7">
            </td>
            <td class="auto-style22">
                &nbsp;</td>
            <td class="auto-style7">
                <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
                <asp:Label ID="lblRegister" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style7">
                &nbsp;</td>
            <td class="auto-style22">
                &nbsp;</td>
            <td class="auto-style7">
                <asp:SqlDataSource ID="sdsSecQns" runat="server" ConnectionString="<%$ ConnectionStrings:KawanConnection %>" SelectCommand="SELECT * FROM [Sec_Qns]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

