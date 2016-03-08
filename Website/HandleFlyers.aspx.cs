using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectUtils;
using System.Data.Common;

namespace Website
{
    public partial class HandleFlyers : System.Web.UI.Page
    {
        public static string Url()
        {
            return "~/HandleSupport.aspx";
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            LoginUtils.RequireLogin();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

            if (LoginUtils.GetProfileString(Constants.PROFILE_ADMIN_TAG) != Constants.PROFILE_ADMIN_TAG)
            {
                Master.SetErrorMessage("You must be logged in as an admin to view this page.");
                return;
            }

            DBAccess my_access = null;
            string log_cat = "Page_PreRender";
            string request_id = null;

            try
            {
                my_access = new DBAccess(this);

                string sql, err_msg;

                request_id = Request.QueryString["request_id"];

                Guid requestId;

                try
                {
                    requestId = new Guid(request_id);
                    request_id = requestId.ToString();
                }
                catch (Exception)
                {
                    Master.SetErrorMessage("Invalid/No account reset token was found");
                    return;
                }

                sql = DBAccess.FormatSQL("select id, school_name, contact_first_name, contact_last_name, "
                    + "contact_addr1, contact_addr2, contact_city, contact_state, contact_zip, contact_phone, "
                    + "org_code, promotion_expire_date, flyers_requested, flyers_sent_total "
                    + "FROM PTARegistration WHERE id = {0}", request_id);

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    throw new ExceptionEx("DB Error while trying to lookup support request info for: {0}.  Error: {1}",
                        request_id, err_msg);
                }

                request_id = null;
                string school_name = null;
                string first_name = null;
                string last_name = null;
                string addr1 = null;
                string addr2 = null;
                string city = null;
                string state = null;
                string zip = null;
                string phone = null;
                string org_code = null;
                DateTime exp_date = DateTime.MinValue;
                int requested = -1;
                int total_sent = -1;

                while (reader.Read())
                {
                    request_id = DBAccess.SafeString(reader, "id");
                    school_name = DBAccess.SafeString(reader, "school_name");
                    first_name = DBAccess.SafeString(reader, "contact_first_name");
                    last_name = DBAccess.SafeString(reader, "contact_last_name");
                    addr1 = DBAccess.SafeString(reader, "contact_addr1");
                    addr2 = DBAccess.SafeString(reader, "contact_addr2");
                    city = DBAccess.SafeString(reader, "contact_city");
                    state = DBAccess.SafeString(reader, "contact_state");
                    zip = DBAccess.SafeString(reader, "contact_zip");
                    phone = DBAccess.SafeString(reader, "contact_phone");
                    org_code = DBAccess.SafeString(reader, "org_code");
                    exp_date = (DateTime)reader["promotion_expire_date"];
                    requested = (int)reader["flyers_requested"];
                    object total_sent_obj = reader["flyers_sent_total"];
                    total_sent = DBAccess.IsNull(total_sent_obj) ? 0 : (int)total_sent_obj;
                }

                if (request_id == null)
                {
                    Master.SetErrorMessage("Flyer request ID {0} did not match any user account",
                        Request.QueryString["request_id"]);
                    return;
                }
                /*
                 *
                 <asp:Label ID="SchoolName" runat="server" />
                        <asp:Label ID="ContactName" runat="server" />
                        <asp:Label ID="ContactAddress" runat="server" />
                        <asp:Label ID="NumRequested" runat="server" />
                        <asp:Label ID="OrgCode" runat="server" />
                        <asp:Label ID="ExpDate" runat="server" />
                        <asp:TextBox ID="NumSent" runat="server" /><br />
        */
                SchoolName.Text = school_name;
                ContactName.Text = String.IsNullOrEmpty(first_name) ? last_name : first_name + " " + last_name;
                ContactPhone.Text = phone;
                ContactAddress.Text = addr1 + "<br>" + (String.IsNullOrEmpty(addr2) ? "" : addr2 + "<br>")
                    + city + ", " + state + "  " + zip;
                NumRequested.Text = requested.ToString();
                OrgCode.Text = org_code;
                ExpDate.Text = exp_date.ToString("MMM dd, yyyy");
                TotalSent.Text = total_sent.ToString();

                ViewState[Constants.VIEWSTATE_FEEDBACK_ID] = request_id;
                ViewState[Constants.VIEWSTATE_NUM_REQUESTED] = requested;
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while rendering feedback request for request_id: {0}, Exception: {1} {2}",
                    request_id, ex.Message, ex.StackTrace);
                Master.SetErrorMessage("An error occured while rending feedback request.  Please "
                    + "contact David with error code: {0} for further help.", log_id);
                return;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        protected void MarkHandledButton_Click(object sender, EventArgs e)
        {
            DBAccess my_access = null;
            string log_cat = "MarkHandledButton_Click";
            string request_id = null;

            try
            {
                my_access = new DBAccess(this);

                string sql, err_msg;

                request_id = ViewState[Constants.VIEWSTATE_FEEDBACK_ID].ToString();
                int requested = (int)ViewState[Constants.VIEWSTATE_NUM_REQUESTED];

                int numSent = Utils.SafeInt(NumSentThisTime.Text, -1);

                if (numSent == -1)
                {
                    numSent = requested;
                }

                sql = DBAccess.FormatSQL("UPDATE PTARegistration "
                    + "SET flyers_sent = {0}, flyers_sent_timestamp_utc = GETUTCDATE(), "
                    + "flyers_sent_total = COALESCE(flyers_sent_total, 0) + {0} WHERE id = {1}",
                    numSent, request_id);

                my_access.ExecuteNonQuery(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "DB error while updating flyers sent request {0}.  Error: {1}", request_id, err_msg);

                    Master.SetErrorMessage("DB Error while updating flyers sent {0}.  Contact David with error code: {1}",
                        request_id, log_id);

                    return;
                }

                my_access.LogMessage(DBAccess.LOG_LEVEL_INFO, log_cat,
                    "Marked flyer request {0} as handled", request_id);

                Master.SetNotificationMessage("Marked flyer request as having been sent.");
                ResponsePanel.Visible = false;
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while marking flyer request as handled for request_id: {0}, Exception: {1} {2}",
                    request_id, ex.Message, ex.StackTrace);
                Master.SetErrorMessage("An error occured while marking flyer request.  Please "
                    + "contact David with error code: {0} for further help.", log_id);
                return;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }


    }
}
