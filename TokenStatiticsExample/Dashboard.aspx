<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TokenStatiticsExample.WebForm1" %>
<%@ Register Assembly="System.Web.DataVisualization" 
Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    </head>
<body >
    <form id="form1" runat="server">
        <div>
            <H4>Dashboard</H4>
            <asp:Panel ID="PanelDashboard" runat="server">
            <table>
                
                <tr>
                    <td style="vertical-align:top; padding-right:20px;">
            <h3><b>Save/Update Token</b></h3>
            <br />
            Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <br />
            <br />
            Symbol&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtSymbol" runat="server"></asp:TextBox>
            <br />
            <br />
            Contract Address&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox ID="txtContractAddress" runat="server"></asp:TextBox>
            <br />
            <br />
            Total Supply&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp <asp:TextBox ID="txtTotalSupply" runat="server"></asp:TextBox>
            <br />
            <br />
            Total Holders&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
            <asp:TextBox ID="txtTotalHolders" runat="server"></asp:TextBox>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
            <asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#3333ff" OnClick="btnSave_Click" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" BackColor="#006699" OnClick="btnReset_Click" />
                </td>
                    <td style="vertical-align: top;">
                <asp:Chart ID="Chart1" runat="server" Width="303px" Height="348px">
                    <Series>
                        <asp:Series Name="Series1" ChartType="Pie" XValueMember="Name" YValueMembers="Total_Supply"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
                </tr>

                </table>
            <br />
            <br />
            <br />
            <br />
            <br />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         &nbsp;&nbsp;&nbsp;

            <asp:Button ID="btnExport" runat="server" Text="DownloadTokens(Excel)" OnClick="btnExport_Click" Height="32px" style="margin-left: 75px" Width="185px" />

            <asp:GridView ID="TokenGridDetails" runat="server" AutoGenerateColumns="false" Width="927px" AutoGenerateEditButton="true" OnRowEditing="TokenGridDetails_RowEditing" OnRowCancelingEdit="TokenGridDetails_RowCancelingEdit" OnRowUpdating="TokenGridDetails_RowUpdating" OnRowCommand="TokenGridDetails_RowCommand" AllowPaging="true" PageSize="10" OnPageIndexChanging="TokenGridDetails_PageIndexChanging" Height="219px" >
                <Columns>
                    <asp:TemplateField HeaderText="Id">
                        <ItemTemplate>
                            <asp:Label ID="lblRank" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Symbol">
                        <ItemTemplate>
                          <asp:LinkButton ID="lnkSymbol" runat="server" Text='<%# Eval("Symbol") %>' CommandName="ViewDetails" CommandArgument='<%# Eval("Symbol") %>'></asp:LinkButton>                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Textbox ID="txtName" runat="server" Text='<%#Eval("Name") %>'></asp:Textbox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contract Address">
                        <ItemTemplate>
                            <asp:Textbox ID="txtContractAddress" runat="server" Text='<%#Eval("Contract_Address") %>'></asp:Textbox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Holders">
                        <ItemTemplate>
                            <asp:Textbox ID="txtTotalHolders" runat="server" Text='<%#Eval("Total_holders") %>'></asp:Textbox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Supply">
                        <ItemTemplate>
                            <asp:Textbox ID="txtTotalSupply" runat="server" Text='<%#Eval("Total_Supply") %>'></asp:Textbox>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:BoundField DataField="Total_Supply_Percent" HeaderText="Total Supply %" />

                    
                    
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
        </div>
        </asp:Panel>
    </form>
</body>
</html>
