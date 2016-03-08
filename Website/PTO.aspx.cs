using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectUtils;

namespace Website
{
    public partial class PTO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUtils.IsLoggedIn() && LocalUtils.HasPTARegistration())
            {
                Response.Redirect(PTOProgram.Url());
            }

            Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

        }
    }
}
