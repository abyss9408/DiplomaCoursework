<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" ValidateRequest="false" %> 
<!-- when ValidateRequest is set to false input validation is disabled and true=enable input validation -->

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        You enter ID:
        <asp:Label ID="lblId" runat="server"></asp:Label>
        <br />
    
        <br />
    
        <asp:TextBox ID="txtSubmit" runat="server" Height="79px" Width="389px" TextMode="MultiLine"></asp:TextBox>
    
    </div>
        <asp:Button ID="btnSubmit" runat="server"  Text="Submit Comment" OnClick="btnSubmit_Click"  />
        <br />
        <asp:Label ID="lblDisplay" runat="server"></asp:Label>
    </form>
</body>
</html>
