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
    public partial class Beheer : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            LoginUtils.RequireLogin();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (LoginUtils.GetProfileString(Constants.PROFILE_ADMIN_TAG) != Constants.PROFILE_ADMIN_TAG)
            {
                ErrorMessage.Text = "You must be logged in as an admin to view this page.";
                ErrorMessage.ForeColor = System.Drawing.Color.Red;
                ErrorMessage.Font.Size = FontUnit.XXLarge;
                NoLoginPanel.Visible = false;
                return;
            }

            DBAccess my_access = null;

            if (!IsPostBack)
            {
                try
                {
                    my_access = new DBAccess(this);

                    BindLogMessages(my_access);
                    BindSupportRequests(my_access, 5);
                    BindFlyerRequests(my_access, 5);
                    BindCustomerAccounts(my_access, 5);
                    BindPTARegistrations(my_access, 5);
                    BindPurchases(my_access, 15);

                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = HttpUtility.HtmlEncode(ex.Message);
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

        private void BindPTARegistrations(DBAccess my_access, int limit)
        {
            string sql, err_msg;

            sql = "SELECT COUNT(*) FROM PTARegistration;";

            object ptaCountObj = my_access.GetScalar(sql, out err_msg);

            if (err_msg != null)
            {
                throw new ExceptionEx("DB Error on PTA count: {0}", err_msg);
            }

            PTACount.Text = ptaCountObj.ToString();

            sql = "SELECT " + (limit > 0 ? " TOP " + limit.ToString() : "")
                + " id, school_name, created_utc, org_code, contact_last_name + ', ' + contact_first_name as contact_name "
                + "FROM PTARegistration ORDER BY created_utc DESC;";

            DbDataReader reader = my_access.GetReader(sql, out err_msg);

            if (err_msg != null)
            {
                throw new ExceptionEx("DB Error on PTA select: {0}", err_msg);
            }

            PTAGrid.DataSource = reader;
            PTAGrid.DataBind();
        }

        private void BindPurchases(DBAccess my_access, int limit)
        {
            string sql, err_msg;

            sql = "select count(*) FROM ( select DISTINCT activation_code from Purchases WHERE paid = 1 ) tmp;";

            object ptaCountObj = my_access.GetScalar(sql, out err_msg);

            if (err_msg != null)
            {
                throw new ExceptionEx("DB Error on PTA count: {0}", err_msg);
            }

            PurchaseCount.Text = ptaCountObj.ToString();

            sql = "SELECT " + (limit > 0 ? " TOP " + limit.ToString() : "")
                + " ua.username, p.created_utc, pi.item_desc, p.activation_code, pi.item_code, "
                + " p.hardware_key, p.coupon, p.amount_paid "
                + " FROM Purchases p JOIN PurchaseItems pi ON (p.item_id = pi.id) "
                + " JOIN UserAccounts ua on (p.user_id = ua.id) "
                + " WHERE p.paid = 1 "
                + " ORDER BY created_utc DESC ";

            DbDataReader reader = my_access.GetReader(sql, out err_msg);

            if (err_msg != null)
            {
                throw new ExceptionEx("DB Error on PTA select: {0}", err_msg);
            }

            PurchaseGrid.DataSource = reader;
            PurchaseGrid.DataBind();
        }

        private void BindCustomerAccounts(DBAccess my_access, int limit)
        {
            string sql, err_msg;

            sql = "SELECT COUNT(*) FROM UserAccounts;";

            object flyerRequestCountObj = my_access.GetScalar(sql, out err_msg);

            if (err_msg != null)
            {
                throw new ExceptionEx("DB Error on flyer request count: {0}", err_msg);
            }

            CustomerCount.Text = flyerRequestCountObj.ToString();

            sql = "SELECT " + (limit > 0 ? " TOP " + limit.ToString() : "")
                + " username, created_utc from UserAccounts ORDER BY created_utc DESC;";

            DbDataReader reader = my_access.GetReader(sql, out err_msg);

            if (err_msg != null)
            {
                throw new ExceptionEx("DB Error on flyer request select: {0}", err_msg);
            }

            CustomerGrid.DataSource = reader;
            CustomerGrid.DataBind();
        }

        private void BindFlyerRequests(DBAccess my_access, int limit)
        {
            string sql, err_msg;

            sql = "SELECT COUNT(*) FROM PTARegistration WHERE flyers_sent_timestamp_utc IS NULL AND flyers_requested IS NOT NULL;";

            object flyerRequestCountObj = my_access.GetScalar(sql, out err_msg);

            if (err_msg != null)
            {
                throw new ExceptionEx("DB Error on flyer request count: {0}", err_msg);
            }

            FlyerRequestCount.Text = flyerRequestCountObj.ToString();

            sql = "SELECT " + (limit > 0 ? " TOP " + limit.ToString() : "")
                + " id, school_name, created_utc, flyers_requested, flyers_sent_total "
                + "FROM PTARegistration WHERE flyers_sent_timestamp_utc IS NULL AND flyers_requested IS NOT NULL "
                + "ORDER BY created_utc DESC;";

            DbDataReader reader = my_access.GetReader(sql, out err_msg);

            if (err_msg != null)
            {
                throw new ExceptionEx("DB Error on flyer request select: {0}", err_msg);
            }

            FlyerGrid.DataSource = reader;
            FlyerGrid.DataBind();
        }

        private void BindSupportRequests(DBAccess my_access, int limit)
        {
            string sql, err_msg;

            sql = "SELECT COUNT(*) FROM SupportRequests WHERE response IS NULL;";

            object supportCountObj = my_access.GetScalar(sql, out err_msg);

            if (err_msg != null)
            {
                throw new ExceptionEx("DB Error on support request count: {0}", err_msg);
            }

            SupportRequestCount.Text = supportCountObj.ToString();

            sql = "SELECT " + (limit > 0 ? " TOP " + limit.ToString() : "")
                + " id, name, type, request, created_utc FROM SupportRequests "
                + "WHERE response IS NULL ORDER BY created_utc DESC;";

            DbDataReader reader = my_access.GetReader(sql, out err_msg);

            if (err_msg != null)
            {
                throw new ExceptionEx("DB Error on support request select: {0}", err_msg);
            }

            SupportGrid.DataSource = reader;
            SupportGrid.DataBind();
        }

        private void BindLogMessages(DBAccess my_access)
        {
            string sql, err_msg;

            sql = "SELECT TOP 100 id, created_utc, message_text, log_owner, "
                + "message_category, log_level, thread_id, type_id, hostname "
                + "FROM LogMessages ORDER BY id DESC";

            DbDataReader reader = my_access.GetReader(sql, out err_msg);

            if (err_msg != null)
            {
                throw new ExceptionEx("DB Error on log message select: {0}", err_msg);
            }

            ResultsGrid.DataSource = reader;
            ResultsGrid.DataBind();
        }

        protected void SeeAllSupportClick(object sender, EventArgs e)
        {
            DBAccess my_access = null;

            try
            {
                my_access = new DBAccess(this);

                BindSupportRequests(my_access, 0);
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        protected void SeeAllFlyersClick(object sender, EventArgs e)
        {
            DBAccess my_access = null;

            try
            {
                my_access = new DBAccess(this);

                BindFlyerRequests(my_access, 0);
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        protected void SeeAllCustomersClick(object sender, EventArgs e)
        {
            DBAccess my_access = null;

            try
            {
                my_access = new DBAccess(this);

                BindCustomerAccounts(my_access, 0);
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        protected void SeeAllPTAClick(object sender, EventArgs e)
        {
            DBAccess my_access = null;

            try
            {
                my_access = new DBAccess(this);

                BindPTARegistrations(my_access, 0);
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        protected void SeeAllPurchasesClick(object sender, EventArgs e)
        {
            DBAccess my_access = null;

            try
            {
                my_access = new DBAccess(this);

                BindPurchases(my_access, 200);
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        protected void Diagnose_Click(object sender, EventArgs e)
        {
            DBAccess my_access = null;

            try
            {
                my_access = new DBAccess(this);

                string sql, err_msg;

                int diag_id;

                if (!Int32.TryParse(ErrorCode.Text, out diag_id))
                {
                    throw new ExceptionEx("Could not convert '{0}' to an error code", ErrorCode.Text);
                }

                sql = String.Format("SELECT id, created_utc, message_text, log_owner, "
                    + "message_category, log_level, thread_id, type_id, hostname "
                    + "FROM LogMessages "
                    + "WHERE id > {0} AND id < {1}"
                    + "ORDER BY id ASC",
                    DBAccess.FormatSQLValue(diag_id - 10),
                    DBAccess.FormatSQLValue(diag_id + 10));

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    throw new ExceptionEx("DB Error on log message select: {0}", err_msg);
                }

                ResultsGrid.DataSource = reader;
                ResultsGrid.DataBind();
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
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
