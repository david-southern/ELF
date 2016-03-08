var SCROLLER_DATA = [];
var URL_PREFIX = "";

var scrollImage;
var scrollHeader;
var scrollText;
var scrollLeft;
var scrollRight;
var scrollIndex;

function addScrollerData(header, imageSource, url, text)
{
  SCROLLER_DATA.push({ header: header, imageSource: imageSource, URL: url, text: text });
}

function preloadImages()
{
  jQuery.each(SCROLLER_DATA, function(index, imgData)
  {
    var preloader = jQuery("<img/>").attr("src", imgData.imageSource);
    preloader.hide();
    jQuery("body").append(preloader);
  });
}

function learnMore()
{
  window.location = URL_PREFIX + SCROLLER_DATA[scrollIndex].URL;
}

function initializeScroller()
{
  preloadImages();

  scrollImage = jQuery("#ScrollerImage");
  scrollHeader = jQuery("#ScrollerHeader");
  scrollText = jQuery("#ScrollerText");

  jQuery("#ScrollerMore").click(learnMore);
  jQuery("#ScrollerMore").css("cursor", "pointer");

  scrollImage.click(function(ev)
  {
    var distance = function(x1, y1, x2, y2)
    {
      return Math.sqrt(Math.pow(x2 - x1, 2) + Math.pow(y2 - y1, 2));
    };

    var clickX = ev.pageX - scrollImage.offset().left;
    var clickY = ev.pageY - scrollImage.offset().top;

    var clickArea = "IMAGE";

    if (distance(0, jQuery(this).height() / 2, clickX, clickY) < 38)
    {
      scrollIndex--;
      adjustScroller();
      clickArea = "LEFT";
    }
    else if (distance(jQuery(this).width(), jQuery(this).height() / 2, clickX, clickY) < 38)
    {
      scrollIndex++;
      adjustScroller();
      clickArea = "RIGHT";
    }
    else
    {
      learnMore();
    }

    log(LOG_DEBUG, "Click: (" + clickX + ", " + clickY + ") - " + clickArea);
  });

  scrollIndex = 0;
  adjustScroller();
}

function adjustScroller()
{
  while (scrollIndex < 0)
  {
    scrollIndex += SCROLLER_DATA.length;
  }

  while (scrollIndex >= SCROLLER_DATA.length)
  {
    scrollIndex -= SCROLLER_DATA.length;
  }

  scrollImage.attr("src", SCROLLER_DATA[scrollIndex].imageSource);
  scrollHeader.html(SCROLLER_DATA[scrollIndex].header);
  scrollText.html(SCROLLER_DATA[scrollIndex].text);
  
  if (SCROLLER_DATA[scrollIndex].URL)
  {
    jQuery("#ScrollerMore").show();
  }
  else
  {
    jQuery("#ScrollerMore").hide();
  }
}

function initializePage()
{
  addScrollerData("WELCOME TO E-L-FUN",
    "images/scroller_welcome.gif",
    null,
    "English Language Fundamentals LLC presents exciting reading, writing, and spelling materials for home "
      + "and classroom use.  ELF provides proven solutions for raising student achievement in reading, "
      + "writing and spelling.");


  addScrollerData("Parents Love ELF",
    "images/scroller_parent.gif",
    "Parents.aspx",
    "for the motivating phonics and word building games as well as the Integrated writing and "
      + "spelling program for home use.<br><br>These materials include learning activities for "
      + "preschool to grade six learners in phonics, reading, writing, spelling and thinking.");

  addScrollerData("PTAs Love ELF",
    "images/scroller_pta.gif",
    "PTO.aspx",
    "for the opportunity it gives them to raise funds while improving their children's "
      + "knowledge and performance.<br><div style='font-weight: bold; margin-top: 4px;'>The Ultimate Fund Raiser</div>"
      + "<ul style='margin: 0 0 0 590px;'>"
      + "<li>No product to handle</li>"
      + "<li>No direct sales</li>"
      + "<li>One person can run the program</li>"
      + "<li>Students improve reading, writing and spelling scores</li>"
      + "<li>Schools gain funds to help all students learn</li>"
      + "</ul>");

  addScrollerData("Teachers Love ELF",
    "images/scroller_teacher.gif",
    "Teachers.aspx",
    "because they see strong sustained growth in student achievement from using the Integrated "
      + "Writing and Spelling Program and the ELF computer games.<br><br>These materials help students "
      + "master the spelling of high frequency words, while learning to write clear, complete sentences "
      + "and passages.  It integrates phonics, reading, writing, thinking and team learning skills.");

  addScrollerData("Principals Love ELF",
    "images/scroller_principal.gif",
    "Principal.aspx",
    "for the improved performance on state tests in writing and spelling. The Life Skills Program integrated "
      + "with writing and spelling helps students develop essential skills for success in life.<br><br>"
      + "Improved scores on state and national tests in English, spelling and reading are the pay-off.  "
      + "Parents like the difference they see in basic phonics skills and fewer errors in writing.");

  addScrollerData("ELL / TITLE 1 Teachers",
    "images/scroller_ell.gif",
    "ELL.aspx",
    "love the program because it is a natural fit for ELL and Title I students. The five "
      + "levels allow the teacher to match the material to the student's level. The weekly design "
      + "includes activities for each day of the week. On Monday the students read the introductory passage "
      + "and take it home to copy it as a writing assignment. Learning activities in "
      + "phonics, structure, comprehension, and visual memory are presented during the rest of the week.");

  initializeScroller();
}