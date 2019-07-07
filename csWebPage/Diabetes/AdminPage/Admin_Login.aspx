<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Login.aspx.cs" Inherits="AdminPage.Admin_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Admin login</h1>
    <form id="loginFrom" method="post" runat="server">
        <div>
            <asp:TextBox placeholder="Username" ID="username" runat="server"></asp:TextBox>
            <asp:TextBox placeholder="Password" ID="password" TextMode="Password" runat="server" />
            <asp:Button runat="server" Text="Log in" ID="submit_login" OnClick="submit_login_Click" type="submit" value="Log in" />
            <br />
            <asp:Label ID="Error" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>


