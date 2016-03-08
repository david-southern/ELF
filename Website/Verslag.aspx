<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verslag.aspx.cs" Inherits="Website.Verslag"
    Trace="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="ErrorMessage" runat="server" EnableViewState="false"></asp:Label>
    </div>
    <asp:Panel ID="NoLoginPanel" runat="server">
        <div style="clear: both">
            <h1>
                Sales Report</h1>
                Start Date: <asp:TextBox ID="StartDate" runat="server" Text="1900-01-01" /><br />
                End Date: <asp:TextBox ID="EndDate" runat="server" Text="2100-01-01" /><br />
                <asp:Button ID="RegenButton" runat="server" Text="Regenerate Report" OnClick="RegenerateClick" />
            <h3>
                Sales by Product type</h3>
            <asp:GridView ID="SalesByTypeGrid" runat="server" BorderColor="Black" AutoGenerateColumns="false">
                <HeaderStyle BackColor="#AAAAAA" />
                <AlternatingRowStyle BackColor="#D8D8D8" />
                <Columns>
                    <asp:BoundField HeaderText="Item" DataField="item_code" />
                    <asp:BoundField HeaderText="Units Sold" DataField="units_sold" />
                    <asp:BoundField HeaderText="Total Sales" DataField="total_sales" DataFormatString="{0:c}" />
                </Columns>
            </asp:GridView>
            <hr />
            <h3>
                Sales by PTA Organization</h3>
            <asp:GridView ID="SalesByPTAOrgGrid" runat="server" BorderColor="Black" AutoGenerateColumns="false">
                <HeaderStyle BackColor="#AAAAAA" />
                <AlternatingRowStyle BackColor="#D8D8D8" />
                <Columns>
                    <asp:BoundField HeaderText="Org Name" DataField="item_code" HtmlEncode="false" />
                    <asp:BoundField HeaderText="Units Sold" DataField="units_sold" />
                    <asp:BoundField HeaderText="Total Sales" DataField="total_sales" DataFormatString="{0:c}" />
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
