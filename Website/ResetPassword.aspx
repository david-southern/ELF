<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Website.ResetPassword" %>

<%@ MasterType VirtualPath="~/ELFThings.Master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <div style="width: 750px; text-align: left;">
      <asp:Panel ID="ResetPanel" runat="server">
        <div style="padding: 12px 27px 0px 0px; width: 350px;">
          <h1 class="pageheader">
            Reset Password
          </h1>
          <br />
          Enter a new password:<br />
          <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="NewPassword" ErrorMessage="* Password is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Reset" />
          <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" ErrorMessage="* Confirm Password is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Reset" />
          <asp:CompareValidator ID="PasswordMatches" runat="server" ControlToCompare="NewPassword" Operator="Equal" ControlToValidate="ConfirmNewPassword" ErrorMessage="* Password does not match Confirm Password<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Reset" />
          <asp:TextBox ID="NewPassword" runat="server" Width="225" TabIndex="3" TextMode="Password" /><br />
          <div style="padding: 6px 0 0 0;">
            Confirm Password:
          </div>
          <asp:TextBox ID="ConfirmNewPassword" runat="server" Width="225" TabIndex="4" TextMode="Password" /><br />
          <br />
          <br />
          <center>
            <asp:Button ID="ResetButton" runat="server" Text="Reset Password" OnClick="ResetButton_Click" ValidationGroup="Reset" TabIndex="6" />
          </center>
        </div>
    </div>
    </asp:Panel>
  </center>
</asp:Content>
