<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="AdminPage.AdminHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Hello <%=Session["name"]%></h1>
            <br />
            <br />

            <asp:Button runat="server" Text="Namirnice" ID="btn_namirnice" OnClick="btn_namirnice_Click" type="submit" value="Namirnice" />
            <br />
            <asp:Button runat="server" Text="Obroci" ID="btn_obroci" OnClick="btn_obroci_Click" type="submit" value="Obroci" />
            <br />
            <asp:Button runat="server" Text="Kombinacije" ID="btn_kombinacije" OnClick="btn_kombinacije_Click" type="submit" value="Kombinacije" />
            <br />
            <asp:Button runat="server" Text="Jedinice" ID="btn_jedinice" OnClick="btn_jedinice_Click" type="submit" value="Jedinice" />
            <br />
            <asp:Button runat="server" Text="KorisniciCSV" ID="btn_CSV" OnClick="btn_CSV_Click" type="submit" value="KorisniciCSV" />
            <br />
            <asp:Button runat="server" Text="Logout" ID="Logout" OnClick="Logout_Click" type="submit" value="Logout" />
            <br />
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
