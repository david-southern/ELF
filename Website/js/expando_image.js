var ANIMATION_TIME = 200;

var FULL_TARGET_WIDTH, FULL_TARGET_HEIGHT;

var TARGET_WIDTH, TARGET_HEIGHT, TARGET_TOP, TARGET_LEFT;
var RETURN_WIDTH, RETURN_HEIGHT, RETURN_TOP, RETURN_LEFT;

var expandImage;
var backdrop;

function openAnimate()
{
  jQuery(".expando_image").unbind("click");
  expandImage.unbind("click");
  backdrop.unbind("click");

  expandImage.show();

  log(LOG_INFO, "Starting position: (" + expandImage.offset().left + ", " + expandImage.offset().top + ")");
  log(LOG_INFO, "Starting Size: (" + expandImage.width() + ", " + expandImage.height() + ")");
  log(LOG_INFO, "Target position: (" + TARGET_LEFT + ", " + TARGET_TOP + ")");
  log(LOG_INFO, "Target Size: (" + TARGET_WIDTH + ", " + TARGET_HEIGHT + ")");

  backdrop.css("top", jQuery(window).scrollTop());
  backdrop.css("left", jQuery(window).scrollLeft());
  backdrop.width(jQuery(window).width());
  backdrop.height(jQuery(window).height());
  backdrop.fadeTo(ANIMATION_TIME, 0.8);

  expandImage.animate(
    {
      width: TARGET_WIDTH,
      height: TARGET_HEIGHT,
      top: TARGET_TOP,
      left: TARGET_LEFT
    },
    ANIMATION_TIME,
    function()
    {
      expandImage.click(closeAnimate);
      backdrop.click(closeAnimate);
    }
  );
  
}

function setupAnimation(source_image)
{
  var expand_img_src = source_image.attr("expando_src");

  expandImage.attr("src", expand_img_src);

  RETURN_HEIGHT = source_image.height();
  RETURN_WIDTH = source_image.width();
  RETURN_TOP = source_image.offset().top;
  RETURN_LEFT = source_image.offset().left;

  expandImage.css("top", RETURN_TOP).css("left", RETURN_LEFT);
  expandImage.width(RETURN_WIDTH);
  expandImage.height(RETURN_HEIGHT);

  var winWidth = jQuery(window).width();
  var winHeight = jQuery(window).height();

  var full_width_attr = source_image.attr("expando_width");
  var full_height_attr = source_image.attr("expando_height");

  if (full_width_attr)
  {
    FULL_TARGET_WIDTH = full_width_attr;
  }
  
  if (full_height_attr)
  {
    FULL_TARGET_HEIGHT = full_height_attr;
  }
  
  TARGET_HEIGHT = Math.min(winHeight - 60, FULL_TARGET_HEIGHT);
  TARGET_WIDTH = Math.min(winWidth - 60, FULL_TARGET_WIDTH);

  if (TARGET_HEIGHT / TARGET_WIDTH > FULL_TARGET_HEIGHT / FULL_TARGET_WIDTH)
  {
    TARGET_HEIGHT = TARGET_WIDTH * (FULL_TARGET_HEIGHT / FULL_TARGET_WIDTH);
  }
  else
  {
    TARGET_WIDTH = TARGET_HEIGHT * (FULL_TARGET_WIDTH / FULL_TARGET_HEIGHT);
  }

  TARGET_TOP = winHeight / 2 - TARGET_HEIGHT / 2 + jQuery(window).scrollTop();
  TARGET_LEFT = winWidth / 2 - TARGET_WIDTH / 2 + jQuery(window).scrollLeft();

  TARGET_TOP = Math.max(TARGET_TOP, 30);
  TARGET_LEFT = Math.max(TARGET_LEFT, 30);

  log(LOG_INFO, "Window size: (" + winWidth + ", " + winHeight + ")");

  openAnimate();
}

function closeAnimate()
{
  expandImage.unbind("click");
  backdrop.unbind("click");

  backdrop.fadeTo(ANIMATION_TIME, 0);

  expandImage.animate(
    {
      width: RETURN_WIDTH,
      height: RETURN_HEIGHT,
      top: RETURN_TOP,
      left: RETURN_LEFT
    },
    ANIMATION_TIME,
    function()
    {
      expandImage.hide();
      backdrop.hide();
      jQuery(".expando_image").click(function() { setupAnimation(jQuery(this)); });
    }
  );
}

function preloadImages(jqImg)
{
  var expand_img_src = jqImg.attr("expando_src");

  var preloader = jQuery("<img/>").attr("src", expand_img_src);
  preloader.hide();
  jQuery("body").append(preloader);
}

function setupExpandos()
{
  return;
  
  backdrop = jQuery("<div>&nbsp;</div>").addClass("ExpandoBackdrop");
  jQuery("body").append(backdrop);
  backdrop.click(closeAnimate);
  backdrop.hide();

  expandImage = jQuery("#ExpandoImage");
  jQuery(".expando_image").css("cursor", "pointer");

  jQuery(".expando_image").each(function() { preloadImages(jQuery(this)); });

  jQuery(".expando_image").click(function() { setupAnimation(jQuery(this)); });
}