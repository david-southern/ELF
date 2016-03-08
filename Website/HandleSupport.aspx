<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="HandleSupport.aspx.cs" Inherits="Website.HandleSupport" %>

<%@ MasterType VirtualPath="~/ELFThings.Master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <div style="width: 750px; text-align: left;">
      <asp:Panel ID="ResponsePanel" runat="server">
        <div style="padding: 12px 27px 0px 0px; width: 350px;">
          <h1 class="pageheader">
          Support/Feedback Request
          </h1>
          <br />
      <table>
        <tr>
          <td align="right" style="white-space: nowrap;">
            Feedback Subject:
          </td>
          <td>
            <asp:Label ID="FeedbackType" runat="server" />
          </td>
        </tr>
        <tr>
          <td align="right" style="white-space: nowrap;">
            Name:
          </td>
          <td>
            <asp:Label ID="FeedbackName" runat="server" />
          </td>
        </tr>
        <tr>
          <td align="right" style="white-space: nowrap;">
            Email:
          </td>
          <td>
            <asp:Label ID="FeedbackEmail" runat="server" />
          </td>
        </tr>
        <tr valign="top" style="white-space: nowrap;">
          <td align="right">
            Message:
          </td>
          <td>
            <asp:TextBox ID="FeedbackMessage" runat="server" TextMode="MultiLine" Rows="10" Width="500" ReadOnly="true" />
          </td>
        </tr>
        <tr>
        <td><br /><br /><br /></td><td></td>
        </tr>
        <tr valign="top">
          <td align="right" style="white-space: nowrap;">
            Response: (no cussin!!)
          </td>
          <td>
            <asp:TextBox ID="FeedbackResponse" runat="server" TextMode="MultiLine" Rows="10" Width="500" />
          </td>
        </tr>
        <tr>
          <td>
          </td>
          <td>
            <asp:Button ID="SendButton" runat="server" Text="Send Response" OnClick="SendButton_Click" /><br />
            <asp:Label ID="NoEmailLabel" runat="server" ForeColor="Red" />
            <br />
            <asp:Button ID="MarkHandledButton" runat="server" Text="Mark Request as handled" OnClick="MarkHandledButton_Click" /><br />
            * No email will be sent to the customer.  Whatever text you type in the response field will be saved as an explanation note.
          </td>
        </tr>
      </table>
          
        </div>
    </div>
    </asp:Panel>
  </center>
</asp:Content>
