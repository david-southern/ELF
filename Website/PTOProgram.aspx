<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="PTOProgram.aspx.cs" Inherits="Website.PTOProgram" %>

<%@ MasterType VirtualPath="~/ELFThings.Master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <div style="width: 750px; text-align: left;">
      <h1 class="pageheader">
        <asp:Label ID="OrgName" runat="server" />
        Fundraiser Status
      </h1>
      <br />
      <div style="font-size: 140%">
        Current Program Status:
        <asp:Label ID="ProgramStatus" runat="server" /><br />
      </div>
      <br />
      <asp:Panel ID="SalesStatusPanel" runat="server">
        Your Organization Code:
        <asp:Label ID="OrgCode" runat="server" /><br />
        Fundraiser Ending Date:
        <asp:Label ID="ExpDate" runat="server" /><br />
        Current Program Sales Total:
        <asp:Label ID="ProgramSalesTotal" runat="server" /><br />
        Current Program Fundraiser Total:
        <asp:Label ID="FundraiserSalesTotal" runat="server" /><br />
        <br />
        <a href="PTOProgramFlyers.aspx">Order free promotional flyers</a>
      </asp:Panel>
      <asp:Panel ID="StartProgramPanel" runat="server">
        When you are ready to begin your fundraiser, click this button to start:<br />
        <br />
        <asp:Button ID="StartProgramButton" runat="server" OnClick="StartProgramButton_Click" Text="Request an Organization Code" />
        <br /><br />
        Remember that once the fundraiser starts, your Organization Code will only remain active for 90 days, so don't request an Organization Code until you are ready to begin
        <br />
      </asp:Panel>
      <a href="PTORegister.aspx">Update my Account Information</a>
      <br />
      <br />
      <span style="font-weight: bold">How the Program works:</span>
      <ul style="margin: 0">
        <li>You can check this page at any time to check the status of your organization's fundraiser.</li>
        <li>Once you are ready to begin your fundraiser, click the "Request an Organization Code" button above.</li>
        <li>Once you have an 'Organization Code', you can order free promotional flyers to help with your fundraiser.</li>
        <li>Distribute the flyers to the parents and teachers in your organization. The flyers will include your Organization Code. Make sure to tell the parents to use your Organization Code when they purchase the Program materials.</li>
        <li>For the duration of the fundraiser (90 days from the day that you first requested your Organization Code) any purchases of the ELF Program or Phonics Games that indicate your Organization Code will be credited to your fundraiser.</li>
        <li>You can check the progress of your fundraiser at any time on this page to see how many sales have been credited.</li>
        <li>When the fundraiser is completed, we will send a check for 25% of the total sales to the address you specified when you signed up.</li>
        <li>That's all there is to it!.</li>
      </ul>
    </div>
  </center>
</asp:Content>
