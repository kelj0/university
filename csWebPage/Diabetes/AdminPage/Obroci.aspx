<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Obroci.aspx.cs" Inherits="AdminPage.Obrok" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button runat="server" Text="Home" ID="obrok_home" OnClick="obrok_home_Click" type="submit" value="obrok_home" />
        <br />
        <div>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
