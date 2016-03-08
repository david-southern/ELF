<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Beheer.aspx.cs" Inherits="Website.Beheer" Trace="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <asp:Label ID="ErrorMessage" runat="server"></asp:Label>
  </div>
  <asp:Panel ID="NoLoginPanel" runat="server">
    <div style="width: 45%; margin: 5px; padding: 10px; border: solid 1px black; float: left;">
      There are
      <asp:Label ID="SupportRequestCount" runat="server" />
      unhandled support requests.<br />
      <asp:Button ID="SeeAllSupport" runat="server" Text="See all Support Requests" OnClick="SeeAllSupportClick" />
      <asp:GridView ID="SupportGrid" runat="server" BorderColor="Black" AutoGenerateColumns="false">
        <Columns>
          <asp:HyperLinkField HeaderText="Handle" Text="Handle" DataNavigateUrlFormatString="/HandleSupport.aspx?request_id={0}" DataNavigateUrlFields="id" />
          <asp:BoundField HeaderText="Name" DataField="name" />
          <asp:BoundField HeaderText="Type" DataField="type" />
          <asp:BoundField HeaderText="Request" DataField="request" />
          <asp:BoundField HeaderText="Created" DataField="created_utc" />
        </Columns>
        <HeaderStyle BackColor="#AAAAAA" />
        <AlternatingRowStyle BackColor="#D8D8D8" />
      </asp:GridView>
    </div>
    <div style="width: 45%; margin: 5px; padding: 10px; border: solid 1px black; float: right;">
      There are
      <asp:Label ID="FlyerRequestCount" runat="server" />
      unhandled requests for PTA flyers.<br>
      <asp:Button ID="SeeAllFlyers" runat="server" Text="See all PTA Flyer Requests" OnClick="SeeAllFlyersClick" />
      <asp:GridView ID="FlyerGrid" runat="server" BorderColor="Black" AutoGenerateColumns="false">
        <Columns>
          <asp:HyperLinkField HeaderText="Handle" Text="Handle" DataNavigateUrlFormatString="/HandleFlyers.aspx?request_id={0}" DataNavigateUrlFields="id" />
          <asp:BoundField HeaderText="School" DataField="school_name" />
          <asp:BoundField HeaderText="# Req" DataField="flyers_requested" />
          <asp:BoundField HeaderText="Already Sent" DataField="flyers_sent_total" />
          <asp:BoundField HeaderText="Created" DataField="created_utc" />
        </Columns>
        <HeaderStyle BackColor="#AAAAAA" />
        <AlternatingRowStyle BackColor="#D8D8D8" />
      </asp:GridView>
    </div>
    <div style="clear: both;">
      <br />
    </div>
    <div style="width: 45%; margin: 5px; padding: 10px; border: solid 1px black; float: left;">
      There are
      <asp:Label ID="CustomerCount" runat="server" />
      customer accounts.<br>
      <asp:Button ID="SeeAllCustomers" runat="server" Text="See all customers" OnClick="SeeAllCustomersClick" />
      <asp:GridView ID="CustomerGrid" runat="server" BorderColor="Black" AutoGenerateColumns="false">
        <HeaderStyle BackColor="#AAAAAA" />
        <AlternatingRowStyle BackColor="#D8D8D8" />
        <Columns>
          <asp:BoundField HeaderText="User" DataField="username" />
          <asp:BoundField HeaderText="Created" DataField="created_utc" />
        </Columns>
      </asp:GridView>
    </div>
    <div style="width: 45%; margin: 5px; padding: 10px; border: solid 1px black; float: right;">
      There are
      <asp:Label ID="PTACount" runat="server" />
      PTA registrations.<br>
      <asp:Button ID="SeeAllPTA" runat="server" Text="See all PTA Registations" OnClick="SeeAllPTAClick" />
      <asp:GridView ID="PTAGrid" runat="server" BorderColor="Black" AutoGenerateColumns="false">
        <HeaderStyle BackColor="#AAAAAA" />
        <AlternatingRowStyle BackColor="#D8D8D8" />
        <Columns>
          <asp:BoundField HeaderText="OrgName" DataField="school_name" />
          <asp:BoundField HeaderText="Contact" DataField="contact_name" />
          <asp:BoundField HeaderText="OrgCode" DataField="org_code" />
          <asp:BoundField HeaderText="Created" DataField="created_utc" />
        </Columns>
      </asp:GridView>
    </div>
    <div style="clear: both">
      There are
      <asp:Label ID="PurchaseCount" runat="server" />
      Purchases.<br>
      <asp:Button ID="SeeAllPurchases" runat="server" Text="See all Purchases" OnClick="SeeAllPurchasesClick" />
      <asp:GridView ID="PurchaseGrid" runat="server" BorderColor="Black" AutoGenerateColumns="false">
        <HeaderStyle BackColor="#AAAAAA" />
        <AlternatingRowStyle BackColor="#D8D8D8" />
        <Columns>
          <asp:BoundField HeaderText="User" DataField="username" />
          <asp:BoundField HeaderText="Item" DataField="item_code" />
          <asp:BoundField HeaderText="Paid" DataField="amount_paid" />
          <asp:BoundField HeaderText="Coupon" DataField="coupon" />
          <asp:BoundField HeaderText="ActCode" DataField="activation_code" />
          <asp:BoundField HeaderText="HardwareKey" DataField="hardware_key" />
          <asp:BoundField HeaderText="Created" DataField="created_utc" />
        </Columns>
      </asp:GridView>
    </div>
    <hr />
    Error Code:
    <asp:TextBox ID="ErrorCode" runat="server" />
    <asp:Button ID="DiagnoseError" runat="server" Text="Diagnose this error code" OnClick="Diagnose_Click" />
    <br />
    <asp:GridView ID="ResultsGrid" runat="server" BorderColor="Black">
      <HeaderStyle BackColor="#AAAAAA" />
      <AlternatingRowStyle BackColor="#D8D8D8" />
    </asp:GridView>
  </asp:Panel>
  </form>
</body>
</html>
