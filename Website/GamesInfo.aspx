<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="GamesInfo.aspx.cs" Inherits="Website.GamesInfo" %>
<%@ MasterType VirtualPath="~/ELFThings.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdditionalHeadContent" runat="server">

  <script type="text/javascript" src="js/expando_image.js"></script>

  <script type="text/javascript" src="js/game_info.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
  <img src="" id="ExpandoImage" style="position: absolute; display: none;" alt="" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <div style="width: 750px; text-align: left;">
      <h1 class="pageheader">
        E-L-Fun Phonics Games
      </h1>
      <a href="Order.aspx"><img src="images/order_now_button.png" alt="Order Now" border="0" style="float: right; padding: 0 0 20px 20px; margin: 0px 0 0 0"/></a>
      These computer games appeal to children and adults and have been found to be successful in homes and schools with English speaking and English second language children of all ages.
      <div style="padding: 10px 0 10px 25px; font-size: 150%">
        <a href="installer/ELF.exe">Download a free trial version of the ELF Phonics Games</a>
      </div>
      <span style="font-weight: bold;">Applying phonics is a three step process:</span>
      <ol>
        <li>Knowing and recognizing the forms of the letters in lower and capital forms. </li>
        <li>Being able to hear and distinguish between the sounds each letter represents. </li>
        <li>Associating the correct sound to each letter. </li>
      </ol>
      These games lead the student systematically through these steps to blending sounds into words. They can be used with children from three to twelve years of age. Included are:
    </div>
    <br />
    <table width="100%">
      <tr>
        <td align="center">
          <div class="contentheading" style="font-size: 100%">
            Vowel Howl&trade;
          </div>
          <img src="images/sample_vowel_howl_thumb.jpg" class="expando_image" expando_src="images/sample_vowel_howl_hires.jpg" alt="Vowel Howl Screenshot" />
        </td>
        <td align="center">
          <div class="contentheading" style="font-size: 100%">
            Super Sonic Phonics&trade;
          </div>
          <img src="images/sample_sonic_phonics_thumb.jpg" class="expando_image" expando_src="images/sample_sonic_phonics_hires.jpg" alt="Sonic Phonics Screenshot" />
        </td>
      </tr>
      <tr>
        <td align="center">
          <div class="contentheading" style="font-size: 100%">
            Tic-Tac-Gold&trade;
          </div>
          <img src="images/sample_tic_tac_thumb.jpg" class="expando_image" expando_src="images/sample_tic_tac_hires.jpg" alt="Tic-Tac-Gold Screenshot" />
        </td>
        <td align="center">
          <div class="contentheading" style="font-size: 100%">
            Genius Word Builder&trade;
          </div>
          <img src="images/sample_gwb_thumb.jpg" class="expando_image" expando_src="images/sample_gwb_hires.jpg" alt="Genius Word Builder Screenshot" />
        </td>
      </tr>
    </table>
  </center>
</asp:Content>
