using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class AuthFailure : System.Web.UI.Page
    {
        public static string Url()
        {
            return "~/AuthFailure.aspx";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

        }
    }
}
