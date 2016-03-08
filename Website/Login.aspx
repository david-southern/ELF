<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Website.Login" %>

<%@ MasterType VirtualPath="~/ELFThings.Master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <div style="width: 750px; text-align: left;">
      <asp:Panel ID="LoginPanel" runat="server">
        <div style="padding: 12px 27px 0px 0px; width: 350px; float: left; border-right: solid 1px #CCC;">
          <div class="pageheader">
            Create Account
          </div>
          <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="RegisterEmail" ErrorMessage="* Email is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Register" />
          <asp:RequiredFieldValidator ID="ConfirmEmailRequired" runat="server" ControlToValidate="ConfirmRegisterEmail" ErrorMessage="* Confirm Email is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Register" />
          <asp:CompareValidator ID="EmailMatches" runat="server" ControlToCompare="RegisterEmail" Operator="Equal" ControlToValidate="ConfirmRegisterEmail" ErrorMessage="* Email does not match Confirm Email<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Register" />
          <asp:RegularExpressionValidator ID="EmailInvalid" runat="server" ControlToValidate="RegisterEmail" ErrorMessage="* Email format is incorrect<br>" ValidationExpression="^\w+([_\-\+\.']\w+)*@\w+([\-\.]\w+)*\.\w+([\-\.]\w+)*$" Display="Dynamic" ValidationGroup="Register" />
          <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="RegisterPassword" ErrorMessage="* Password is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Register" />
          <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmRegisterPassword" ErrorMessage="* Confirm Password is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Register" />
          <asp:CompareValidator ID="PasswordMatches" runat="server" ControlToCompare="RegisterPassword" Operator="Equal" ControlToValidate="ConfirmRegisterPassword" ErrorMessage="* Password does not match Confirm Password<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Register" />
          Email:<br />
          <asp:TextBox ID="RegisterEmail" runat="server" Width="350" TabIndex="1" /><br />
          <div style="padding: 6px 0 0 0;">
          Confirm Email:
          </div>
          <asp:TextBox ID="ConfirmRegisterEmail" runat="server" Width="350" TabIndex="2" />
          <br />
          <br />
          Password:<br />
          <asp:TextBox ID="RegisterPassword" runat="server" Width="225" TabIndex="3" TextMode="Password" /><br />
          <div style="padding: 6px 0 0 0;">
          Confirm Password:
          </div>
          <asp:TextBox ID="ConfirmRegisterPassword" runat="server" Width="225" TabIndex="4" TextMode="Password" /><br />
          <br />
          &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="AllowEmail" runat="server" Text="Check this box if you would like to receive notifications of new versions and products from ELFun." Style="font-size: 75%" TabIndex="5" />
          <br />
          <br />
          <br />
          <center>
            <asp:Button ID="RegisterButton" runat="server" Text="Create Account" OnClick="RegisterButton_Click" ValidationGroup="Register" TabIndex="6" />
          </center>
        </div>
        <div style="padding: 12px 0px 0px 10px; width: 350px; float: right;">
          <div class="pageheader">
            Sign in to your Account
          </div>
          <asp:RequiredFieldValidator ID="EmailRequired2" runat="server" ControlToValidate="LoginEmail" ErrorMessage="* Email is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Login" />
          <asp:RegularExpressionValidator ID="EmailInvalid2" runat="server" ControlToValidate="LoginEmail" ErrorMessage="* Email format is incorrect<br>" ValidationExpression="^\w+([_\-\+\.']\w+)*@\w+([\-\.]\w+)*\.\w+([\-\.]\w+)*$" Display="Dynamic" ValidationGroup="Login" />
          <asp:RequiredFieldValidator ID="PasswordRequired2" runat="server" ControlToValidate="LoginPassword" ErrorMessage="* Password is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Login" />
          Email:<br />
          <asp:TextBox ID="LoginEmail" runat="server" Width="350" TabIndex="7" />
          <div style="padding: 6px 0 0 0">
          Password:<br />
          <asp:TextBox ID="LoginPassword" runat="server" Width="225" TabIndex="8" TextMode="Password" /><br />
          &nbsp;&nbsp;&nbsp;<a href="ForgotPassword.aspx">Forgot your password?</a>
          </div>
          <br />
          <br />
          <center>
            <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" ValidationGroup="Login" TabIndex="9" />
          </center>
        </div>
        <div style="clear: both;">
          &nbsp;</div>
      </asp:Panel>
      <asp:Panel ID="MyAccountPanel" runat="server" Visible="false">
        <div style="padding: 12px 0px 0px 0px; width: 550px;">
          <div class="pageheader">
            Update Account Settings
          </div>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UpdateEmail" ErrorMessage="* Email is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Update" />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ConfirmUpdateEmail" ErrorMessage="* Confirm Email is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Update" />
          <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="UpdateEmail" Operator="Equal" ControlToValidate="ConfirmUpdateEmail" ErrorMessage="* Email does not match Confirm Email<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Update" />
          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="UpdateEmail" ErrorMessage="* Email format is incorrect<br>" ValidationExpression="^\w+([_\-\+\.']\w+)*@\w+([\-\.]\w+)*\.\w+([\-\.]\w+)*$" Display="Dynamic" ValidationGroup="Update" />
          <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="UpdatePassword" Operator="Equal" ControlToValidate="ConfirmUpdatePassword" ErrorMessage="* Password does not match Confirm Password<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Update" />
          <asp:Label ID="PasswordInvalid" runat="server" ForeColor="Red" />
          Email:<br />
          <asp:TextBox ID="UpdateEmail" runat="server" Width="350" TabIndex="1" /><br />
          Confirm Email:<br />
          <asp:TextBox ID="ConfirmUpdateEmail" runat="server" Width="350" TabIndex="2" />
          <br />
          <br />
          Password:<br />
          <asp:TextBox ID="UpdatePassword" runat="server" Width="225" TabIndex="3" TextMode="Password" /><br />
          Confirm Password:<br />
          <asp:TextBox ID="ConfirmUpdatePassword" runat="server" Width="225" TabIndex="4" TextMode="Password" /><br />
          <br />
          &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="UpdateAllowEmail" runat="server" Text="Check this box if you would like to receive notifications of new versions and products from ELFun." Style="font-size: 75%" TabIndex="5" />
          <br />
          <br />
          <center>
            <asp:Button ID="UpdateButton" runat="server" Text="Update Account Settings" OnClick="UpdateButton_Click" ValidationGroup="Update" TabIndex="6" />
          </center>
          <br />
          <br />
          <div class="pageheader">
            Resend Purchase Email
          </div>
          Click this button if you would like us to re-send an email with all of your purchase information and download links.
          <center>
            <asp:Button ID="ResendButton" runat="server" Text="Resend Purchase Information" OnClick="ResendButton_Click" TabIndex="7" />
          </center>
        </div>
      </asp:Panel>
    </div>
  </center>
</asp:Content>
