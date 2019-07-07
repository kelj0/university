<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jedinice.aspx.cs" Inherits="AdminPage.Jedinice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button runat="server" Text="Home" ID="jedinice_home" OnClick="jedinice_home_Click" type="submit" value="jedinice_home" />
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="dvGrid" style="padding: 10px; " class="auto-style2">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                        DataKeyNames="IDJedinica,Jedinica" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" PageSize="5" AllowPaging="true" OnPageIndexChanging="OnPaging"
                        OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="No records has been added."
                        Width="450">
                        <Columns>
                            <asp:TemplateField  HeaderText="IDJedinica" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ReadOnly="true" ID="lblIDJedinica" runat="server" Text='<%# Eval("IDJedinica") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ReadOnly="true" ID="txtIDJedinica" runat="server" Text='<%# Eval("IDJedinica") %>' Width="140"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Jedinica" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Jedinica") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Jedinica") %>' Width="140"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"
                            ItemStyle-Width="150" />
                        </Columns>
                    </asp:GridView>
                    <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                        <tr>
                            <td class="auto-style1">Name of new unit: <br />
                                <asp:TextBox ID="txtName" runat="server" Width="176px" />
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
