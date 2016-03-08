<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Website.ForgotPassword" %>

<%@ MasterType VirtualPath="~/ELFThings.Master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <div style="width: 750px; text-align: left;">
      <asp:Panel ID="ForgottenPanel" runat="server">
        <div style="padding: 12px 27px 0px 0px; width: 450px;">
          <h1 class="pageheader">
            Forgotten Password
          </h1>
          If you have forogtten your password, we can send an email to the address you used to register. This email will have instructions you can use to reset your password.<br />
          <br />
          Enter the email address you used when you signed up:<br />
          <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="ResendEmail" ErrorMessage="* Email is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Resend" />
          <asp:RegularExpressionValidator ID="EmailInvalid" runat="server" ControlToValidate="ResendEmail" ErrorMessage="* Email format is incorrect<br>" ValidationExpression="^\w+([_\-\+\.']\w+)*@\w+([\-\.]\w+)*\.\w+([\-\.]\w+)*$" Display="Dynamic" ValidationGroup="Resend" />
          <asp:TextBox ID="ResendEmail" runat="server" Width="350" TabIndex="1" /><br />
          <br />
          <center>
            <asp:Button ID="ResendButton" runat="server" Text="Send me a Reset Password Email" OnClick="ResendButton_Click" ValidationGroup="Resend" TabIndex="6" />
          </center>
        </div>
      </asp:Panel>
    </div>
  </center>
</asp:Content>
