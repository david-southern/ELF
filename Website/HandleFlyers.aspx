<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="HandleFlyers.aspx.cs" Inherits="Website.HandleFlyers" %>

<%@ MasterType VirtualPath="~/ELFThings.Master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <div style="width: 750px; text-align: left;">
      <asp:Panel ID="ResponsePanel" runat="server">
        <div style="padding: 12px 27px 0px 0px; width: 350px;">
          <h1 class="pageheader">
            Flyers Request
          </h1>
          <br />
          <table cellpadding="3px">
            <tr>
              <td align="right" style="white-space: nowrap;" valign="top">
                School Name:
              </td>
              <td>
                <asp:Label ID="SchoolName" runat="server" Width="500px" />
              </td>
            </tr>
            <tr>
              <td align="right" style="white-space: nowrap;" valign="top">
                Contact Name:
              </td>
              <td>
                <asp:Label ID="ContactName" runat="server" />
              </td>
            </tr>
            <tr>
              <td align="right" style="white-space: nowrap;" valign="top">
                Contact Phone:
              </td>
              <td>
                <asp:Label ID="ContactPhone" runat="server" />
              </td>
            </tr>
            <tr>
              <td align="right" style="white-space: nowrap;" valign="top">
                Contact Address:
              </td>
              <td>
                <asp:Label ID="ContactAddress" runat="server" />
              </td>
            </tr>
            <tr>
              <td align="right" style="white-space: nowrap;" valign="top">
                Org Code:
              </td>
              <td>
                <asp:Label ID="OrgCode" runat="server" />
              </td>
            </tr>
            <tr>
              <td align="right" style="white-space: nowrap;" valign="top">
                Expiration Date:
              </td>
              <td>
                <asp:Label ID="ExpDate" runat="server" />
              </td>
            </tr>
            <tr>
              <td align="right" style="white-space: nowrap;" valign="top">
                Number of Flyers Requested:
              </td>
              <td>
                <asp:Label ID="NumRequested" runat="server" />
              </td>
            </tr>
            <tr>
              <td align="right" style="white-space: nowrap;" valign="top">
                Number of Flyers Sent Previously:
              </td>
              <td>
                <asp:Label ID="TotalSent" runat="server" />
              </td>
            </tr>
            <tr>
              <td>
              </td>
              <td>
                <br />
                If you are sending a different amount than requested, enter the amount here:
                <asp:TextBox ID="NumSentThisTime" runat="server" /><br />
                <asp:Button ID="MarkHandledButton" runat="server" Text="Flyers Sent" OnClick="MarkHandledButton_Click" /><br />
              </td>
            </tr>
          </table>
        </div>
    </div>
    </asp:Panel>
  </center>
</asp:Content>
