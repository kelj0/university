<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Combinations.aspx.cs" Inherits="AdminPage.Combinations" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button runat="server" Text="Home" ID="kombinacije_home" OnClick="kombinacije_home_Click" type="submit" value="kombinacije_home" />
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="dvGrid" style="padding: 10px; " class="auto-style2" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                        DataKeyNames="IDKombinacija" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" PageSize="5" AllowPaging="true" OnPageIndexChanging="OnPaging"
                        OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="No records has been added."
                        Width="450" >
                        <Columns>
                            <asp:TemplateField  HeaderText="IDKombinacija" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="lblIDKombinacija" runat="server" Text='<%# Eval("IDKombinacija") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ReadOnly="true" ID="txtIDKombinacija" runat="server" Text='<%# Eval("IDKombinacija") %>' Width="140"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Broj obroka" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrojObroka" runat="server" Text='<%# Eval("BrojObroka") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBrojObroka" runat="server" Text='<%# Eval("BrojObroka") %>' Width="140"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DatumKreiranja" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="lblDatumKreiranja" runat="server" Text='<%# Eval("DatumKreiranja","{0:MM.dd.yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ReadOnly="true" ID="txtDatumKreiranja" runat="server" Text='<%# Eval("DatumKreiranja","{0:MM.dd.yyyy}") %>' Width="140"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VrijediDo" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="lblVrijediDo" runat="server" Text='<%# Eval("VrijediDo","{0:MM.dd.yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtVrijediDo" runat="server" Text='<%# Eval("VrijediDo","{0:MM.dd.yyyy}") %>' Width="140"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"
                            ItemStyle-Width="150" />
                        </Columns>
                    </asp:GridView>
                    <asp:Table ID="tblKombinacija" border="1" cellpadding="0" cellspacing="0" runat="server" style="border-collapse: collapse">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox placeholder="Broj Obroka" ID="txtBrojObroka" runat="server" Width="176px" />
                            </asp:TableCell><asp:TableCell>
                                <asp:TextBox placeholder="Vrijedi do(npr. 21.05.2019)" ID="txtVrijediDo" runat="server" Width="176px" />
                            </asp:TableCell><asp:TableCell>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="Insert" />
                            </asp:TableCell></asp:TableRow></asp:Table><asp:Panel Visible="false" ID="pnlDetalji" runat="server">
                        <asp:Table ID="tblKombinacijaDetalji" runat="server" BorderStyle="Solid">
                        </asp:Table>
                        <asp:Button ID="btnAddDetalji" runat="server" Text="Add" OnClick="btnAddDetalji_Click" />
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Label Visible="false" BorderStyle="Dotted" BorderColor="Red" ID="lblError" runat="server">Greska: Krivi zbroj kombinacija</asp:Label>
        </div>
    </form>
</body>
</html>