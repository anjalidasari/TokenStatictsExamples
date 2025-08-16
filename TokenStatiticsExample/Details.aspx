<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="TokenStatiticsExample.Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Token List</title>
</head>
<body>
    
    <form id="form1" runat="server">
            <h2 style="text-align:center;">Token Details</h2>
        <table border="1" cellpadding="5" cellspacing="0" style="margin:auto;">
            <tr>
                <th>Symbol</th>
                <td><asp:Label ID="lblSymbol" runat="server" /></td>
            </tr>
            <tr>
                <th>Name</th>
                <td><asp:Label ID="lblName" runat="server" /></td>
            </tr>
            <tr>
                <th>Total Supply</th>
                <td><asp:Label ID="lblTotalSupply" runat="server" /></td>
            </tr>
            <tr>
                <th>Contract Address</th>
                <td><asp:Label ID="lblContractAddress" runat="server" /></td>
            </tr>
            <tr>
                <th>Total Holders</th>
                <td><asp:Label ID="lblTotalHolders" runat="server" /></td>
            </tr>
            <tr>
                <th>Price</th>
                <td><asp:Label ID="lblPrice" runat="server" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
