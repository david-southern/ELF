<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="PTOProgramFlyers.aspx.cs" Inherits="Website.PTOProgramFlyers" %>

<%@ MasterType VirtualPath="~/ELFThings.Master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <div style="width: 750px; text-align: left;">
      <h1 class="pageheader">
      Promotional Flyers for Parents
      </h1>
      The success of your fundraising program depends on getting the word out to your parents about the ELF program,
      and also informting the parents of your Organization Code so that they can properly give your organization
      credit for thier purchases.<br />
      <br />
      We have developed a free high-quality promotional flyer that gives parents the information that they need
      to know about the ELF program.  We will custom-print your Organization Code on these flyers, and send them
      to your program coordinator.  You can then distribute these flyers to your parents in order to make the
      fundraising process as easy and convenient as possible.<br />
      <br />
      * Note: The flyers will be sent to the Program Coordinator address that you supplied when you registered for the
      fundraiser.  Please make sure that your address is correct.<br />
      &nbsp;&nbsp;<a href="PTORegister.aspx">Update my Account Information</a><br />
      <br />
      <asp:Panel ID="OrderFlyersPanel" runat="server">
      Please indicate how many flyers you will need: <asp:TextBox ID="FlyerCount" runat="server" /><br />
      <br />
      <asp:Button ID="SendFlyersButton" runat="server" Text="Send me some flyers!" OnClick="SendFlyersButtonClick" />
      </asp:Panel>
      <asp:Panel ID="FlyersSentPanel" runat="server" Visible="false">
      <br />
      <asp:Button ID="OrderMoreButton" runat="server" Text="I need more flyers!" OnClick="OrderMoreButtonClick" />
      </asp:Panel>
    </div>
  </center>
</asp:Content>
