#define DO_LOGS

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Website
{
    public partial class ELFThings : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorPanel.Visible = false;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (ProjectUtils.LoginUtils.IsLoggedIn())
            {
                LoginLink.Text = "My Account";
            }

#if DEBUG && DO_LOGS
            Page.ClientScript.RegisterClientScriptInclude("log4javascript", "js/log4javascript.js");
#else
            Page.ClientScript.RegisterClientScriptInclude("log4javascript", "js/nologger.js");
#endif
        }

        public void SetNotificationMessage(string format_string, params object[] format_parms)
        {
            ErrorMessage.Text = String.Format(format_string, format_parms);

            ErrorPanel.BackColor = Color.FromArgb(0xDD, 0xDD, 0xFF);
            ErrorPanel.Visible = true;
        }

        public void SetErrorMessage(string format_string, params object[] format_parms)
        {
            ErrorMessage.Text = String.Format(format_string, format_parms);

            ErrorPanel.BackColor = Color.FromArgb(0xFF, 0xDD, 0xDD);
            ErrorPanel.Visible = true;
        }

        public enum ContentSections
        {
            BLUE_SECTION,
            WHITE_SECTION
        }

        public void HideContent(ContentSections whichSection)
        {
            switch (whichSection)
            {
                case ContentSections.BLUE_SECTION:
                    BlueSection.Visible = false;
                    break;

                case ContentSections.WHITE_SECTION:
                    WhiteSection.Visible = false;
                    break;
            }
        }
    }
}
