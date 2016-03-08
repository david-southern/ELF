<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Website.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdditionalHeadContent" runat="server">

  <script type="text/javascript" src="js/default.js"></script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SubContent" runat="server">
  <div id="ScrollerContainer" style="float: left; overflow: hidden;">
    <img id="ScrollerImage" src="" style="margin: 0 15px 0 0;" alt="" />
  </div>
  <div id="ScrollerHeader" class="sectionheader">
  </div>
  <div id="ScrollerText">
  </div>
  <img id="ScrollerMore" src="images/learn_more_white.png" style="padding: 8px 0 0 0;" alt="Learn More" />
  <div style="clear: both;">
  </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
  <div style="float: right; width: 280px;">
    <div class="contentheading">
      ELF Phonics Games
    </div>
    <center>
      <a href="installer/ELF.exe" style="font-size: 115%">Download free trial Games</a>
      <a href="GamesInfo.aspx">
        <img src="images/game_mainscreen.png" alt="ELF Spelling Games" border="1" />
      </a>
    </center>
    <br />
    The Elf Phonics Games are individual and competitive games that build success in phonics for children from preschool to sixth grade.
    <br />
    <a href="GamesInfo.aspx">
      <img src="images/learn_more.png" border="0" alt="Learn More" class="learnmore" /></a>
  </div>
  <img src="images/spanner.png" style="float: right; padding: 17px 14px 0px 14px;" alt="" />
  <div>
    <div class="contentheading">
      An Integrated Writing and Spelling Program
    </div>
    <a href="ProgramInfo.aspx">
      <img src="images/lesson_sample.png" style="float: right" alt="Lesson Sample" border="0" />
    </a>
    The Integrated Spelling and Writing Program is a proven program that produces successful writers while mastering the spelling of high frequency words that account for over 80% of writing.
    <br />
    <a href="ProgramInfo.aspx">
      <img src="images/learn_more.png" border="0" alt="Learn More" class="learnmore" /></a>
    <div style="font-weight: bold; font-size: 125%; padding: 0 0 6px 0;">
      Download Sample Lessons
    </div>
    <ul class="latestnews" style="">
      <li>
        <a href="lessons/Lesson_1_7_sample.pdf" target="new">
          <img src="images/pdf_button.png" border="0" alt="" />First Grade Sample Lesson</a>
      </li>
      <li>
        <a href="lessons/Lesson_2_8_sample.pdf" target="new">
          <img src="images/pdf_button.png" border="0" alt="" />Second Grade Sample Lesson</a>
      </li>
      <li>
        <a href="lessons/Lesson_3_7_sample.pdf" target="new">
          <img src="images/pdf_button.png" border="0" alt="" />Third Grade Sample Lesson</a>
      </li>
      <li>
        <a href="lessons/Lesson_4_3_sample.pdf" target="new">
          <img src="images/pdf_button.png" border="0" alt="" />Fourth Grade Sample Lesson</a>
      </li>
      <li>
        <a href="lessons/Lesson_5_1_sample.pdf" target="new">
          <img src="images/pdf_button.png" border="0" alt="" />Fifth Grade Sample Lesson</a>
      </li>
    </ul>
    You will need Adobe Acrobat to view these files. &nbsp; If you do not have Adobe Acrobat you can download it for free at
    <a href="http://get.adobe.com/reader" target="_blank">get.adobe.com/reader</a>.
  </div>
  <div style="clear: both;">
  </div>
</asp:Content>
