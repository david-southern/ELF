<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Website.Contact" %>

<%@ MasterType VirtualPath="~/ELFThings.Master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <asp:Panel ID="FeedbackPanel" runat="server">
      <div style="width: 750px; text-align: left;">
        <h1 class="pageheader">
          Contact Us
        </h1>
        We value your ideas and suggestions. Please submit any questions or suggestions for improvement. We are a growing company so check our website often.
        <br />
        <br />
        <table>
          <tr>
            <td align="right">
              Feedback Subject:
            </td>
            <td>
              <asp:DropDownList ID="FeedbackType" runat="server">
                <asp:ListItem Value="Support" Text="Support Request" Selected="True" />
                <asp:ListItem Value="Comment" Text="Comment" />
                <asp:ListItem Value="Question" Text="Question" />
              </asp:DropDownList>
            </td>
          </tr>
          <tr>
            <td align="right">
              Name:
            </td>
            <td>
              <asp:TextBox ID="Name" runat="server" Width="400px" />
            </td>
          </tr>
          <tr>
            <td align="right">
              Email:
            </td>
            <td>
              <asp:TextBox ID="Email" runat="server" Width="400px" />
              <span style="font-size: x-small; color: Red">* Required if you want a reply</span>
            </td>
          </tr>
          <tr valign="top">
            <td align="right">
              Feedback:
            </td>
            <td>
              <asp:TextBox ID="Feedback" runat="server" TextMode="MultiLine" Rows="10" Width="400px" MaxLength="5000" />
            </td>
          </tr>
          <tr>
            <td>
            </td>
            <td>
              <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
            </td>
          </tr>
        </table>
      </div>
    </asp:Panel>
  </center>
</asp:Content>
