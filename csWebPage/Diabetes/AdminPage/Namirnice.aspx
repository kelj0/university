<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Namirnice.aspx.cs" Inherits="AdminPage.Namirnice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button runat="server" Text="Home" ID="namirnice_home" OnClick="namirnice_home_Click" type="submit" value="namirnice_home" />
        <br />
              <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="dvGrid" style="padding: 10px; " class="auto-style2">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                        DataKeyNames="IDNamirnica" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" PageSize="15" AllowPaging="true" OnPageIndexChanging="OnPaging"
                        OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="No records has been added."
                        Width="450">
                        <Columns>
                            <asp:TemplateField  HeaderText="IDNamirnica" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ReadOnly="true" ID="lblIDNamirnica" runat="server" Text='<%# Eval("IDNamirnica") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ReadOnly="true" ID="txtIDNamirnica" runat="server" Text='<%# Eval("IDNamirnica") %>' Width="140"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Naziv" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Naziv") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Naziv") %>' Width="140"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Energija_kcal" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="lblEnergija_kcal" runat="server" Text='<%# Eval("Energija_kcal") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEnergija_kcal" runat="server" Text='<%# Eval("Energija_kcal") %>' Width="140"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Energija_kJ" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="lblEnergija_kJ" runat="server" Text='<%# Eval("Energija_kJ") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEnergija_kJ" runat="server" Text='<%# Eval("Energija_kJ") %>' Width="140"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tip" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="txtTip" runat="server" Text='<%# Eval("Tip") %>' Width="140"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTip" runat="server" Text='<%# Eval("Tip") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Jedinica" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="txtJedinica" runat="server" Text='<%# Eval("Jedinica") %>' Width="140"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtJedinica" runat="server" Text='<%# Eval("Jedinica") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Kolicina" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="lblKolicina" runat="server" Text='<%# Eval("Kolicina") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtKolicina" runat="server" Text='<%# Eval("Kolicina") %>' Width="140"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"
                            ItemStyle-Width="150" />
                        </Columns>
                    </asp:GridView>
                    <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                        <tr>
                            <td class="auto-style1">
                                <asp:TextBox placeholder="Naziv" ID="txtNaziv" runat="server" Width="176px" />
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox placeholder="Energija kcal" ID="txtEnergija_kcal" runat="server" Width="176px" />
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox placeholder="Energija kJ" ID="txtEnergija_kJ" runat="server" Width="176px" />
                            </td>
                            <td>
                                <asp:TextBox placeholder="Tip(Ugljikohidrati,Masti,Bjelancevine)" ID="txtTip" Width="250px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox placeholder="Jedinica" ID="txtJedinica" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox placeholder="Kolicina" ID="txtKolicina" runat="server" Width="176px" />
                            </td>
                            <td class="auto-style3">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="Insert" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
