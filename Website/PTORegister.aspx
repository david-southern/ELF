<%@ Page Title="Fundraising Registration" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="PTORegister.aspx.cs" Inherits="Website.PTORegister" %>

<%@ MasterType VirtualPath="~/ELFThings.Master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <div style="width: 750px; text-align: left;">
      <h1 class="pageheader">
        Fundraising Registration
      </h1>
      <asp:Label ID="InstructionsLabel" runat="server" Font-Bold="true">
        How the Program works:
      </asp:Label>
      <asp:Panel ID="InstructionsPanel" runat="server">
        <ul style="margin: 0">
          <li>Enter your information on this page. We will create an account for your school's fundraiser in our system.</li>
          <li>You can go to our
            <a href="PTOProgram.aspx">Program page</a>
            at any time to check the status of your school's fundraiser.</li>
          <li>Once you are ready to begin your fundraiser, login to the
            <a href="PTOProgram.aspx">Program page</a>.</li>
          <li>Once you are logged in, you can request an 'Organization Code', and download promotional flyers to help with your fundraiser.</li>
          <li>Distribute the flyers to your parents and teachers. The flyers will include your Organization Code. Make sure to tell the parents to use your Organization Code when they purchase the Program materials.</li>
          <li>For the duration of the fundraiser (usually one year from the day that you first requested your Organization Code) any purchases of the ELF Program or Phonics Games that indicate your Organization Code will be credited to your fundraiser.</li>
          <li>You can check the progress of your fundraiser at any time on the
            <a href="PTOProgram.aspx">Program Page</a>
            to see how many sales have been credited.</li>
          <li>Every 90 days we will send a check for 25% of the total sales to the address you specify below.</li>
          <li>That's all there is to it!.</li>
        </ul>
        <br />
      </asp:Panel>
      <div style="width: 500px; padding: 8px 0;">
        School Name: <span style="font-size: 75%">(Note: This name will appear on the flyers you send to your parents)</span><br />
        <asp:TextBox ID="SchoolName" runat="server" Width="500" TabIndex="1" />
        <asp:RequiredFieldValidator ID="SchoolNameRequired" runat="server" ControlToValidate="SchoolName" ErrorMessage="* School Name is required" ForeColor="Red" Display="Dynamic" />
      </div>
      <div style="width: 500px; margin: 10px 0 0 0;">
        Primary Contact:<br />
        <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="ContactFirstName" ErrorMessage="* First Name is required<br>" ForeColor="Red" Display="Dynamic" />
        <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="ContactLastName" ErrorMessage="* Last Name is required<br>" ForeColor="Red" Display="Dynamic" />
        <div style="float: left; padding: 6px 25px 0px 0px; width: 225px;">
          First Name:<br />
          <asp:TextBox ID="ContactFirstName" runat="server" Width="225" TabIndex="2" />
        </div>
        <div style="float: right; padding: 6px 0px 0px 25px; width: 225px;">
          Last Name:<br />
          <asp:TextBox ID="ContactLastName" runat="server" Width="225" TabIndex="3" />
        </div>
        <div style="clear: both; padding: 6px 0px 0px 0px; width: 500px;">
          Title:<br />
          <asp:TextBox ID="ContactTitle" runat="server" Width="500" TabIndex="4" />
        </div>
        <div style="clear: both; padding: 12px 0px 0px 0px; width: 500px;">
          Mailing Address:<br />
          <asp:RequiredFieldValidator ID="AddrRequired" runat="server" ControlToValidate="ContactAddrStreet" ErrorMessage="* Address is required<br>" ForeColor="Red" Display="Dynamic" />
          <asp:RequiredFieldValidator ID="CityRequired" runat="server" ControlToValidate="ContactAddrCity" ErrorMessage="* City is required<br>" ForeColor="Red" Display="Dynamic" />
          <asp:RequiredFieldValidator ID="StateRequired" runat="server" ControlToValidate="ContactAddrState" ErrorMessage="* State is required<br>" ForeColor="Red" Display="Dynamic" />
          <asp:RequiredFieldValidator ID="ZipRequired" runat="server" ControlToValidate="ContactAddrZip" ErrorMessage="* ZIP is required" ForeColor="Red" Display="Dynamic" />
          <asp:RegularExpressionValidator ID="ZipInvalid" runat="server" ControlToValidate="ContactAddrZip" ErrorMessage="* ZIP format is incorrect (numbers, spaces, or dashes only please)<br>" ValidationExpression="^\d+([\d\s\-])*\d$" Display="Dynamic" />
          <asp:TextBox ID="ContactAddrStreet" runat="server" Width="500" TabIndex="5" /><br />
          <div style="padding: 6px 0 0 0;">
            <asp:TextBox ID="ContactAddrStreet2" runat="server" Width="500" TabIndex="6" />
          </div>
        </div>
        <div style="float: left; padding: 6px 10px 0px 0px; width: 250px;">
          City:<br />
          <asp:TextBox ID="ContactAddrCity" runat="server" Width="250" TabIndex="7" />
        </div>
        <div style="float: right; padding: 6px 0px 0px 15px; width: 85px;">
          Zip:<br />
          <asp:TextBox ID="ContactAddrZip" runat="server" Width="85" TabIndex="9" />
        </div>
        <div style="float: right; padding: 6px 0px 0px 10px; width: 130px;">
          State:<br />
          <div style="margin: 1px 0 0 0;">
            <asp:DropDownList ID="ContactAddrState" runat="server" TabIndex="8">
              <asp:ListItem Value="AL">Alabama</asp:ListItem>
              <asp:ListItem Value="AK">Alaska</asp:ListItem>
              <asp:ListItem Value="AZ">Arizona</asp:ListItem>
              <asp:ListItem Value="AR">Arkansas</asp:ListItem>
              <asp:ListItem Value="CA">California</asp:ListItem>
              <asp:ListItem Value="CO">Colorado</asp:ListItem>
              <asp:ListItem Value="CT">Connecticut</asp:ListItem>
              <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
              <asp:ListItem Value="DE">Delaware</asp:ListItem>
              <asp:ListItem Value="FL">Florida</asp:ListItem>
              <asp:ListItem Value="GA">Georgia</asp:ListItem>
              <asp:ListItem Value="HI">Hawaii</asp:ListItem>
              <asp:ListItem Value="ID">Idaho</asp:ListItem>
              <asp:ListItem Value="IL">Illinois</asp:ListItem>
              <asp:ListItem Value="IN">Indiana</asp:ListItem>
              <asp:ListItem Value="IA">Iowa</asp:ListItem>
              <asp:ListItem Value="KS">Kansas</asp:ListItem>
              <asp:ListItem Value="KY">Kentucky</asp:ListItem>
              <asp:ListItem Value="LA">Louisiana</asp:ListItem>
              <asp:ListItem Value="ME">Maine</asp:ListItem>
              <asp:ListItem Value="MD">Maryland</asp:ListItem>
              <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
              <asp:ListItem Value="MI">Michigan</asp:ListItem>
              <asp:ListItem Value="MN">Minnesota</asp:ListItem>
              <asp:ListItem Value="MS">Mississippi</asp:ListItem>
              <asp:ListItem Value="MO">Missouri</asp:ListItem>
              <asp:ListItem Value="MT">Montana</asp:ListItem>
              <asp:ListItem Value="NE">Nebraska</asp:ListItem>
              <asp:ListItem Value="NV">Nevada</asp:ListItem>
              <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
              <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
              <asp:ListItem Value="NM">New Mexico</asp:ListItem>
              <asp:ListItem Value="NY">New York</asp:ListItem>
              <asp:ListItem Value="NC">North Carolina</asp:ListItem>
              <asp:ListItem Value="ND">North Dakota</asp:ListItem>
              <asp:ListItem Value="OH">Ohio</asp:ListItem>
              <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
              <asp:ListItem Value="OR">Oregon</asp:ListItem>
              <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
              <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
              <asp:ListItem Value="SC">South Carolina</asp:ListItem>
              <asp:ListItem Value="SD">South Dakota</asp:ListItem>
              <asp:ListItem Value="TN">Tennessee</asp:ListItem>
              <asp:ListItem Value="TX">Texas</asp:ListItem>
              <asp:ListItem Value="UT">Utah</asp:ListItem>
              <asp:ListItem Value="VT">Vermont</asp:ListItem>
              <asp:ListItem Value="VA">Virginia</asp:ListItem>
              <asp:ListItem Value="WA">Washington</asp:ListItem>
              <asp:ListItem Value="WV">West Virginia</asp:ListItem>
              <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
              <asp:ListItem Value="WY">Wyoming</asp:ListItem>
            </asp:DropDownList>
          </div>
        </div>
        <div style="clear: both; padding: 6px 0px 0px 0px; width: 500px;">
          Phone:<br />
          <asp:TextBox ID="ContactPhone" runat="server" Width="500" TabIndex="10" />
        </div>
        <div style="clear: both; padding: 12px 0px 0px 0px; width: 500px;">
          <asp:Button ID="Register" runat="server" Text="Register" OnClick="RegisterButton_Click" TabIndex="15" />
          <asp:Button ID="Update" runat="server" Text="Update" OnClick="UpdateButton_Click" TabIndex="15" Visible="false" />
        </div>
      </div>
    </div>
  </center>
</asp:Content>
