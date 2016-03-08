<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="OrderConfirmation.aspx.cs" Inherits="Website.OrderConfirmation" %>

<%@ MasterType VirtualPath="~/ELFThings.Master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <div style="width: 600px; text-align: left;">
      <asp:Panel ID="PaymentPanel" runat="server">
        <center>
          <h1 class="pageheader">
            Review Your Order
          </h1>
        </center>
        <br />
        <div style="clear: both;">
          Please review your order. If you would like to add or remove any items, please click the "Return to Shopping Cart" button.
        </div>
        <div style="float: right; padding: 9px 0px 6px 0px;">
          <asp:Button ID="ReturnToCartButton" runat="server" Text="Return to Shopping Cart" OnClick="ReturnToCartButton_Click" Width="160px" />
        </div>
        <div style="clear: both; padding: 0 0 0 0; width: 100%;">
          <asp:Repeater ID="OrderSummary" runat="server">
            <HeaderTemplate>
              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                  <th style="background-color: #ccc; color: black; border: solid 1px black; border-right: none;">
                    Item
                  </th>
                  <th style="background-color: #ccc; width: 50px; color: black; border: solid 1px black;">
                    Price
                  </th>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
              <tr>
                <td style="padding: 0px 10px; height: 20px; border: solid 1px black; border-top: none 0px; border-right: none 0px;">
                  <%# DataBinder.Eval(Container.DataItem, "item_desc", "{0}") %>
                </td>
                <td style="padding: 0px 10px; height: 20px; width: 50px; border: solid 1px black; border-top: none 0px;">
                  <%# DataBinder.Eval(Container.DataItem, "amount_paid", "{0:C2}") %>
                </td>
              </tr>
            </ItemTemplate>
            <FooterTemplate>
              <tr>
                <td style="padding: 0px 10px; height: 20px; border: none; text-align: right;">
                  Total
                </td>
                <td style="padding: 0px 10px; height: 20px; width: 50px; border: solid 1px black; border-top: none;">
                  <%# DataBinder.Eval(Page, "totalPrice", "{0:C2}") %>
                </td>
              </tr>
              </table>
            </FooterTemplate>
          </asp:Repeater>
        </div>
        <asp:Label ID="OrgCodeFeedback" runat="server" CssClass="pageheader" Style="font-size: 100%" />
        <div style="clear: both">
        </div>
        <asp:Panel ID="PaymentInfo" runat="server">
          <center>
            <div class="pageheader">
              Payment Method
            </div>
          </center>
          <div style="clear: both; padding: 6px 0px 0px 0px; width: 100%;">
            Name on Card:<br />
            <asp:RequiredFieldValidator ID="CCFirstNameRequired" runat="server" ControlToValidate="CCFirstName" ErrorMessage="* First name is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Purchase" />
            <asp:RequiredFieldValidator ID="CCLastNameRequired" runat="server" ControlToValidate="CCLastName" ErrorMessage="* Last name is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Purchase" />
          </div>
          <div style="float: left; padding: 6px 0px 0px 0px; width: 45%;">
            First:<br />
            <asp:TextBox ID="CCFirstName" runat="server" Width="100%" TabIndex="1" />
          </div>
          <div style="float: right; padding: 6px 0px 0px 0px; width: 45%;">
            Last:<br />
            <asp:TextBox ID="CCLastName" runat="server" Width="100%" TabIndex="2" />
          </div>
          <div style="clear: both; padding: 12px 0px 0px 0px; width: 100%;">
            Billing Address:<br />
            <asp:RequiredFieldValidator ID="CCAddressRequired" runat="server" ControlToValidate="CCAddr1" ErrorMessage="* Billing address is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Purchase" />
            <asp:RequiredFieldValidator ID="CCCityRequired" runat="server" ControlToValidate="CCCity" ErrorMessage="* Billing city is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Purchase" />
            <asp:RequiredFieldValidator ID="CCStateRequired" runat="server" ControlToValidate="CCState" ErrorMessage="* Billing state is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Purchase" />
            <asp:RequiredFieldValidator ID="CCZipRequired" runat="server" ControlToValidate="CCZip" ErrorMessage="* Billing ZIP is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Purchase" />
            <asp:RegularExpressionValidator ID="CCZipInvalid" runat="server" ControlToValidate="CCZip" ErrorMessage="* ZIP format is incorrect (numbers, spaces, or dashes only please)<br>" ValidationExpression="^\d+([\d\s\-])*\d$" Display="Dynamic" ValidationGroup="Purchase" />
            <asp:TextBox ID="CCAddr1" runat="server" Width="100%" TabIndex="3" /><br />
            <div style="padding: 6px 0 0 0">
              <asp:TextBox ID="CCAddr2" runat="server" Width="100%" TabIndex="4" />
            </div>
          </div>
          <div style="float: left; padding: 6px 10px 0px 0px; width: 45%;">
            City:<br />
            <asp:TextBox ID="CCCity" runat="server" Width="100%" TabIndex="5" />
          </div>
          <div style="float: right; padding: 6px 0px 0px 15px; width: 22.5%;">
            Zip:<br />
            <asp:TextBox ID="CCZip" runat="server" Width="100%" TabIndex="7" />
          </div>
          <div style="float: right; padding: 6px 0px 0px 15px; width: 22.5%;">
            State:<br />
            <div style="margin: 1px 0 0 0;">
              <asp:DropDownList ID="CCState" runat="server" TabIndex="6">
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
          <div style="clear: both; padding: 12px 0px 0px 0px; width: 100%;">
            <asp:RequiredFieldValidator ID="CCNumRequired" runat="server" ControlToValidate="CCNum" ErrorMessage="* Credit card number is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Purchase" />
            <asp:RegularExpressionValidator ID="CCNumInvalid" runat="server" ControlToValidate="CCNum" ErrorMessage="* Credit card number format is incorrect (numbers, spaces, or dashes only please)<br>" ValidationExpression="^\d+([\d\s\-])*\d$" Display="Dynamic" ValidationGroup="Purchase" />
            <asp:RequiredFieldValidator ID="CCSecCodeRequired" runat="server" ControlToValidate="CCSecCode" ErrorMessage="* Security Code is required<br>" ForeColor="Red" Display="Dynamic" ValidationGroup="Purchase" />
            <asp:RegularExpressionValidator ID="CCSecCodeInvalid" runat="server" ControlToValidate="CCSecCode" ErrorMessage="* Security Code is incorrect (3 or 4 numbers only please)<br>" ValidationExpression="^\d{1,4}$" Display="Dynamic" ValidationGroup="Purchase" />
            Credit Card Number:<br />
            <asp:TextBox ID="CCNum" runat="server" Width="100%" TabIndex="8" />
          </div>
          <div style="float: left; padding: 6px 25px 0px 0px; width: 25%;">
            Expiration Date:<br />
            <asp:DropDownList ID="CCExpMonth" runat="server" TabIndex="9">
              <asp:ListItem Text="01" />
              <asp:ListItem Text="02" />
              <asp:ListItem Text="03" />
              <asp:ListItem Text="04" />
              <asp:ListItem Text="05" />
              <asp:ListItem Text="06" />
              <asp:ListItem Text="07" />
              <asp:ListItem Text="08" />
              <asp:ListItem Text="09" />
              <asp:ListItem Text="10" />
              <asp:ListItem Text="11" />
              <asp:ListItem Text="12" />
            </asp:DropDownList>
            &nbsp;
            <asp:DropDownList ID="CCExpYear" runat="server" TabIndex="10">
              <asp:ListItem Text="2010" />
              <asp:ListItem Text="2011" />
              <asp:ListItem Text="2012" />
              <asp:ListItem Text="2013" />
              <asp:ListItem Text="2014" />
              <asp:ListItem Text="2015" />
              <asp:ListItem Text="2016" />
              <asp:ListItem Text="2017" />
              <asp:ListItem Text="2018" />
              <asp:ListItem Text="2019" />
              <asp:ListItem Text="2020" />
            </asp:DropDownList>
          </div>
          <div style="float: left; padding: 6px 0px 0px 0px; width: 25%;">
            Security Code:<br />
            <asp:TextBox ID="CCSecCode" runat="server" Width="100%" TabIndex="11" />
          </div>
          <br />
          <br />
        </asp:Panel>
        <div style="clear: both; padding: 12px 0px 0px 0px; width: 100%;">
          <asp:Label ID="PurchaseMessage" runat="server" />
          <br />
          <asp:Button ID="Purchase" runat="server" Text="Purchase" OnClick="PurchaseButton_Click" TabIndex="12" ValidationGroup="Purchase" />
        </div>
      </asp:Panel>
      <asp:Panel ID="ResultPanel" runat="server" Visible="false">
        <center>
          <div style="width: 530px">
            <center>
              <div class="pageheader">
                Order Confirmation: Order #<asp:Label ID="ActCode" runat="server" /></div>
            </center>
            <asp:Label ID="ResultLabel" runat="server" />
            Thank you for your order. Your confirmation email was sent to
            <asp:Label ID="PurchaseEmail" runat="server" CssClass="pageheader" Style="font-size: 100%" />. The email will include the download links and/or game activation codes for the items you purchased.
            <br />
            <br />
            <center>
              <div style="font-size: 80%; width: 400px;">
                If you have not received your email within 10 minutes, please check your spam folder to see if the email is there. If it is not, please contact us as
                <asp:Label ID="SupportEmail" runat="server" />
              </div>
            </center>
          </div>
        </center>
      </asp:Panel>
    </div>
  </center>
</asp:Content>
