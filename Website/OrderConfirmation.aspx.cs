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
    public partial class OrderConfirmation : System.Web.UI.Page
    {
        internal static string Url()
        {
            return "~/OrderConfirmation.aspx";
        }

        public decimal totalPrice
        {
            get;
            set;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            string log_cat = "Page_Load";
            DBAccess my_access = null;

            try
            {
                Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

                string actCode = (string)Session[ProjectUtils.Constants.SESSION_ACT_CODE];
                string email = (string)Session[ProjectUtils.Constants.SESSION_USERNAME];
                totalPrice = (decimal)Session[ProjectUtils.Constants.SESSION_TOTAL_PRICE];
                bool dontBillMe = ((string)Session[Constants.COUPON_DONTBILLME]) == "true";

                my_access = new DBAccess(this);

                string sql, err_msg;

                sql = DBAccess.FormatSQL("SELECT p.id, pi.item_desc, p.amount_paid "
                    + "FROM Purchases p JOIN PurchaseItems pi on (p.item_id = pi.id) "
                    + "WHERE p.activation_code = {0} ORDER BY pi.cart_order", actCode);

                DbDataReader reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "An error occured while databinding purchase summary: {0}", err_msg);
                    Master.SetErrorMessage("An error occured while trying to build the confirm page. "
                        + "  Please contact {1} with this error id: {0}", log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                    return;
                }

                OrderSummary.DataSource = reader;
                OrderSummary.DataBind();

                string schoolName = (string)Session[ProjectUtils.Constants.SESSION_SCHOOL_NAME];

                if (schoolName != null)
                {
                    OrgCodeFeedback.Text = "* Your purchase will support the fundraising efforts of " + WebUtils.PreventXSS(schoolName);
                }

                PurchaseMessage.Text = "Upon clicking 'Purchase'";

                if (totalPrice > 0 && !dontBillMe)
                {
                    PurchaseMessage.Text += ", your credit card will be billed, and";
                }
                else
                {
                    PaymentInfo.Visible = false;
                }

                PurchaseMessage.Text += " you will receive an email confirming your purchase.  You will receive "
                    + "lesson download links and/or game activation codes in your confirmation email.";
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        protected void PurchaseButton_Click(object sender, EventArgs e)
        {
            DBAccess my_access = null;
            string log_cat = "PurchaseButton_Click";

            string actCode = null;
            string email = null;

            try
            {
                my_access = new DBAccess(this);

                actCode = (string)Session[ProjectUtils.Constants.SESSION_ACT_CODE];
                email = (string)Session[ProjectUtils.Constants.SESSION_USERNAME];
                totalPrice = (decimal)Session[ProjectUtils.Constants.SESSION_TOTAL_PRICE];
                string coupon = (string)Session[ProjectUtils.Constants.SESSION_COUPON];
                bool dontBillMe = ((string)Session[Constants.COUPON_DONTBILLME]) == "true";

                if (totalPrice > 0 && !dontBillMe)
                {
                    CreditCardUtils.BillingTxnResult result = null;

                    try
                    {
                        CreditCardUtils.CardInfo info = new CreditCardUtils.CardInfo();

                        info.cardNum = CCNum.Text;
                        info.address1 = CCAddr1.Text;
                        info.address2 = CCAddr2.Text;
                        info.city = CCCity.Text;
                        info.cvv = CCSecCode.Text;
                        Int32.TryParse(CCExpMonth.SelectedValue, out info.expMonth);
                        Int32.TryParse(CCExpYear.SelectedValue, out info.expYear);
                        info.firstName = CCFirstName.Text;
                        info.lastName = CCLastName.Text;
                        info.state = CCState.Text;
                        info.zip = CCZip.Text;

                        result = ProjectUtils.CreditCardUtils.BillCreditCard(info, actCode, totalPrice);
                    }
                    catch (CCBillingException bex)
                    {
                        Master.SetErrorMessage(bex.Message);
                        return;
                    }

                    if (result == null)
                    {
                        throw new Exception("Null result returned from BillCreditCard");
                    }

                    if (!result.success)
                    {
                        Master.SetErrorMessage(result.declineMessage);
                        return;
                    }
                }

                string sql, err_msg;

                sql = String.Format("UPDATE Purchases SET paid = 1 WHERE activation_code = {0}",
                    DBAccess.FormatSQLValue(actCode));

                my_access.ExecuteNonQuery(sql, out err_msg);

                if (err_msg != null)
                {
                    throw new Exception("DB Error while updating Purchases to PAID status: " + err_msg);
                }

                string errorMessage;

                if (LocalUtils.SendPurchaseEmail(actCode, email, out errorMessage))
                {
                    PaymentPanel.Visible = false;
                    ResultPanel.Visible = true;
                    PurchaseEmail.Text = email;
                    SupportEmail.Text = ProjectUtils.Constants.SUPPORT_EMAIL;
                    ActCode.Text = actCode;
                }
                else
                {
                    Master.SetErrorMessage(errorMessage);
                }

                Session.Remove(ProjectUtils.Constants.SESSION_ACT_CODE);
            }
            catch (Exception ex)
            {
                int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while finializing purchase: ActCode: {0} email: {1}.  "
                    + "Exception: {2} {3}",
                    actCode, email, ex.Message, ex.StackTrace);
                Master.SetErrorMessage("A problem occured while finalizing your purchase.  "
                    + "Please contact {1} with this error id: {0}",
                    log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
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

        protected void ReturnToCartButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Order.Url());
        }
    }
}
