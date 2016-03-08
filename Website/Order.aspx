<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="Website.Order" %>

<%@ MasterType VirtualPath="~/ELFThings.Master" %>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <div style="width: 750px; text-align: left;">
      <div style="width: 600px">
        <center>
          <h1 class="pageheader">
            Your Order
          </h1>
        </center>
        Check the box next to each item you would like to purchase
        <table>
          <tr>
            <td colspan="2">
              <div class="contentheading" style="font-size: 100%">
                ELF Spelling and Writing Program
              </div>
            </td>
          </tr>
          <tr valign="top">
            <td>
              <div style="white-space: nowrap;">
                <asp:CheckBoxList ID="LessonsItems" runat="server" RepeatDirection="Vertical">
                </asp:CheckBoxList>
              </div>
            </td>
            <td>
              <div style="color: green; padding: 0 0 0 30px;">
                Lessons for each grade level are delivered as a downloadable PDF file. Once you have purchased a lesson, you will be emailed a link to download the lesson.
              </div>
            </td>
          </tr>
          <tr>
            <td colspan="2">
              <br />
              <div class="contentheading" style="font-size: 100%">
                ELF Spelling/Writing Program and Phonics Games:
              </div>
            </td>
          </tr>
          <tr valign="top">
            <td>
              <div style="white-space: nowrap;">
                <asp:CheckBoxList ID="GamesItems" runat="server" RepeatDirection="Vertical">
                </asp:CheckBoxList>
              </div>
            </td>
            <td>
              <div style="color: green; padding: 0 0 0 30px;">
                Note: ELF Phonics Games require Microsoft Windows XP, Windows Vista, or Windows 7. Once you have purchased the games, you will be emailed an activation code for the game.
              </div>
            </td>
          </tr>
        </table>
        <br />
        <br />
        <div class="pageheader" style="font-size: 100%">
          Promotional Code
        </div>
        <div style="clear: both; padding: 6px 0px 0px 0px; width: 500px;">
          If you have a Promotional or School (PTA) Organization code, please enter it here:
          <asp:TextBox ID="CouponCode" runat="server" Width="500" /><br />
          <asp:Label ID="CodeValid" runat="server" ForeColor="Red" />
        </div>
        <br />
        <br />
        <div style="clear: both; padding: 12px 0px 0px 0px; width: 500px;">
          <asp:Button ID="Checkout" runat="server" Text="Continue" OnClick="CheckoutButton_Click" />
        </div>
      </div>
    </div>
  </center>
</asp:Content>
