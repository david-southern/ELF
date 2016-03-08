<%@ Page Title="" Language="C#" MasterPageFile="~/ELFThings.Master" AutoEventWireup="true" CodeBehind="ProgramInfo.aspx.cs" Inherits="Website.ProgramInfo" %>

<%@ MasterType VirtualPath="~/ELFThings.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdditionalHeadContent" runat="server">

  <script type="text/javascript" src="js/expando_image.js"></script>

  <script type="text/javascript" src="js/program_info.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
  <img src="" id="ExpandoImage" style="position: absolute; display: none;" alt="" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
  <center>
    <div style="width: 750px; text-align: left;">
      <h1 class="pageheader">
        An Integrated Writing and Spelling Program
      </h1>
      <a href="Order.aspx"><img src="images/order_now_button.png" alt="Order Now" border="0" style="float: right; padding: 0 0 0 20px; margin: -20px 0 0 0"/></a>
      This unique program introduces two thousand of the most frequently used words in passages for grades one to five. These two thousand words account for nearly 80% of printed materials.<br />
      <br />
      Each week's lesson contains a five day plan. Monday is a reading and writing activity. Tuesday and Thursday contain phonics, comprehension, visual memory, and writing skill activities.  Wednesday and Friday are reviews and assessments. Tuesday through Thursday includes a CUPS team learning activity that applies spelling, usage. capitalization and punctuation skills.<br />
      <br />
      Each week's lessons include:
      <br />
      <table width="100%">
        <tr>
          <td align="center">
            <div class="contentheading" style="font-size: 100%">
              Reading and Writing
            </div>
            <img src="images/sample_writing_thumb.jpg" class="expando_image" expando_src="images/sample_writing_hires.jpg" alt="Sample Reading Lesson" />
          </td>
          <td align="center">
            <div class="contentheading" style="font-size: 100%">
              Spelling
            </div>
            <img src="images/sample_spelling_thumb.jpg" class="expando_image" expando_src="images/sample_spelling_hires.jpg" alt="Sample Spelling Lesson" />
          </td>
          <td>
            <div style="width: 200px; padding: 0 0 0 20px;">
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
          </td>
        </tr>
        <tr>
          <td align="center">
            <div class="contentheading" style="font-size: 100%">
              Life Skills activities
            </div>
            <img src="images/sample_life_skills_thumb.jpg" class="expando_image" expando_src="images/sample_life_skills_hires.jpg" alt="Sample Life Skills Lesson" />
          </td>
          <td align="center">
            <div class="contentheading" style="font-size: 100%">
              Answer Keys
            </div>
            <img src="images/sample_answer_key_thumb.jpg" class="expando_image" expando_src="images/sample_answer_key_hires.jpg" alt="Sample Answer Key" />
          </td>
          <td align="center">
            <img src="images/girl_writing.jpg" alt="Girl Writing" />
          </td>
        </tr>
      </table>
    </div>
  </center>
</asp:Content>
