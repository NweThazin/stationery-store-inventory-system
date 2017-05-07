<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test1.aspx.cs" Inherits="Test1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblShow" runat="server"></asp:Label>
        <br />
        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show Time" />
    
    </div>
    </form>
</body>
</html>
