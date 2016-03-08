using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectUtils;
using System.Data.Common;
using System.Threading;

namespace Website
{
    public partial class Order : System.Web.UI.Page
    {
        public static string Url()
        {
            return "~/Order.aspx";
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            LoginUtils.RequireLogin();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string log_cat = "Page_Load";

            Master.HideContent(ELFThings.ContentSections.BLUE_SECTION);

            if (!IsPostBack)
            {
                DBAccess my_access = null;

                try
                {
                    my_access = new DBAccess(this);

                    string sql, err_msg;

                    sql = DBAccess.FormatSQL("SELECT "
                        + "item_code, "
                        + "item_desc + ' - $' + CAST(item_price AS VARCHAR) as item_text "
                        + "FROM PurchaseItems "
                        + "WHERE item_code <> {0} AND cart_order BETWEEN 0 and 9999 ORDER BY cart_order ",
                        Constants.PURCHASE_GAMES);

                    DbDataReader reader = my_access.GetReader(sql, out err_msg);

                    if (err_msg != null)
                    {
                        int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                            "An error occured while databinding lesson items: {0}", err_msg);
                        Master.SetErrorMessage("An error occured while trying to build the order page. "
                            + "  Please contact {1} with this error id: {0}", log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                        return;
                    }

                    LessonsItems.DataSource = reader;
                    LessonsItems.DataTextField = "item_text";
                    LessonsItems.DataValueField = "item_code";
                    LessonsItems.DataBind();

                    sql = DBAccess.FormatSQL("SELECT "
                        + "item_code, "
                        + "item_desc + ' - $' + CAST(item_price AS VARCHAR) as item_text "
                        + "FROM PurchaseItems "
                        + "WHERE item_code = {0} AND cart_order BETWEEN 0 and 9999 ORDER BY cart_order ",
                        Constants.PURCHASE_GAMES);

                    reader = my_access.GetReader(sql, out err_msg);

                    if (err_msg != null)
                    {
                        int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                            "An error occured while databinding game items: {0}", err_msg);
                        Master.SetErrorMessage("An error occured while trying to build the order page. "
                            + "  Please contact {1} with this error id: {0}", log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                        return;
                    }

                    GamesItems.DataSource = reader;
                    GamesItems.DataTextField = "item_text";
                    GamesItems.DataValueField = "item_code";
                    GamesItems.DataBind();

                    string couponCode = (string)Session[ProjectUtils.Constants.SESSION_COUPON];
                    if (!String.IsNullOrEmpty(couponCode))
                    {
                        CouponCode.Text = couponCode;
                    }

                    string actCode = (string)Session[ProjectUtils.Constants.SESSION_ACT_CODE];

                    if (!String.IsNullOrEmpty(actCode))
                    {
                        sql = DBAccess.FormatSQL("SELECT pi.item_code "
                            + "FROM Purchases p JOIN PurchaseItems pi on (p.item_id = pi.id) "
                            + "WHERE p.activation_code = {0} AND pi.cart_order BETWEEN 0 and 9999", actCode);

                        reader = my_access.GetReader(sql, out err_msg);

                        if (err_msg != null)
                        {
                            int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                                "An error occured while loogin up purchases: {0}", err_msg);
                            Master.SetErrorMessage("An error occured while trying to build the ordering page. "
                                + "  Please contact {1} with this error id: {0}", log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                            return;
                        }


                        while (reader.Read())
                        {
                            string item_code = DBAccess.SafeString(reader, "item_code");

                            foreach (ListItem checkItem in LessonsItems.Items)
                            {
                                if (checkItem.Value == item_code)
                                {
                                    checkItem.Selected = true;
                                }
                            }

                            foreach (ListItem checkItem in GamesItems.Items)
                            {
                                if (checkItem.Value == item_code)
                                {
                                    checkItem.Selected = true;
                                }
                            }
                        }
                    }
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

        protected void CheckoutButton_Click(object sender, EventArgs e)
        {
            string log_cat = "CheckoutButton_Click";
            DBAccess my_access = null;
            string email = null;
            string couponCode = null;

            try
            {
                my_access = new DBAccess(this);

                string sql, err_msg;
                DbDataReader reader;

                couponCode = CouponCode.Text;

                bool isCheep = false, isFree = false;
                decimal buxOff = 0M;
                string schoolName = null;

                if (!String.IsNullOrEmpty(couponCode))
                {
                    couponCode = couponCode.Trim();

                    if (couponCode.Contains(Constants.COUPON_CHEEPSTUFF))
                    {
                        isCheep = true;
                        couponCode = couponCode.Replace(Constants.COUPON_CHEEPSTUFF, "");
                    }

                    if (couponCode.Contains(Constants.COUPON_FREESTUFF))
                    {
                        isFree = true;
                        couponCode = couponCode.Replace(Constants.COUPON_FREESTUFF, "");
                    }

                    if (couponCode.Contains(Constants.COUPON_DONTBILLME))
                    {
                        Session[Constants.COUPON_DONTBILLME] = "true";
                        couponCode = couponCode.Replace(Constants.COUPON_DONTBILLME, "");
                    }

                    //if (couponCode.Contains(Constants.COUPON_50BUXOFF))
                    //{
                    //    buxOff = 50;
                    //    couponCode = couponCode.Replace(Constants.COUPON_50BUXOFF, "");
                    //}

                    couponCode = couponCode.Trim(" \r\n\t,-.".ToCharArray());

                    if (!String.IsNullOrEmpty(couponCode))
                    {
                        sql = String.Format("SELECT school_name FROM PTARegistration WHERE org_code = {0}",
                            DBAccess.FormatSQLValue(couponCode));

                        reader = my_access.GetReader(sql, out err_msg);

                        if (err_msg != null)
                        {
                            int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                                "An error occured while validating coupon code: '{0}'.  Error: {1}",
                                couponCode, err_msg);
                            Master.SetErrorMessage("An error occured while trying to process your order. "
                                + "  Please contact {0} with this error id: {1}",
                                ProjectUtils.Constants.SUPPORT_EMAIL, log_id);
                            return;
                        }

                        if (reader.Read())
                        {
                            schoolName = DBAccess.SafeString(reader, "school_name");
                        }

                        if (schoolName == null)
                        {
                            CodeValid.Text = "The promotional code you entered is not valid.  Please check it and re-enter.";
                            return;
                        }
                    }
                }

                Dictionary<string, string> purchaseInfo = new Dictionary<string, string>();

                sql = "SELECT item_code, id, item_price from PurchaseItems";

                reader = my_access.GetReader(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "An error occured while loading purchase item codes: {0}", err_msg);
                    Master.SetErrorMessage("An error occured while trying to process your order. "
                        + "  Please contact {1} with this error id: {0}", log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                    return;
                }

                Dictionary<string, ProjectUtils.Pair> validItems = new Dictionary<string, ProjectUtils.Pair>();

                while (reader.Read())
                {
                    string itemId = reader["id"].ToString();
                    string itemCode = (string)reader["item_code"];
                    decimal itemPrice = (decimal)reader["item_price"];

                    validItems[itemCode] = new ProjectUtils.Pair(itemId, itemPrice);
                }

                List<ProjectUtils.Pair> selectedItems = new List<ProjectUtils.Pair>();

                bool boughtLessons = false;
                List<string> gamesBought = new List<string>();

                foreach (ListItem thisItem in LessonsItems.Items)
                {
                    if (!validItems.ContainsKey(thisItem.Value))
                    {
                        Master.SetErrorMessage("An unknown purchase item was receved.");
                        return;
                    }

                    if (thisItem.Selected)
                    {
                        selectedItems.Add(validItems[thisItem.Value]);
                        boughtLessons = true;
                    }
                }

                foreach (ListItem thisItem in GamesItems.Items)
                {
                    if (!validItems.ContainsKey(thisItem.Value))
                    {
                        Master.SetErrorMessage("An unknown purchase item was receved.");
                        return;
                    }

                    if (thisItem.Selected)
                    {
                        selectedItems.Add(validItems[thisItem.Value]);
                        gamesBought.Add((string)validItems[thisItem.Value].first);
                    }
                }

                if (selectedItems.Count < 1)
                {
                    Master.SetErrorMessage("You did not select any items to purchase.");
                    return;
                }

                string actCode = ProjectUtils.Utils.GenerateUnambiguousSerialNumber();

                decimal totalPrice = 0;

                sql = "";

                string user_id = (string)Session[Constants.SESSION_USER_ID];

                foreach (ProjectUtils.Pair itemData in selectedItems)
                {
                    decimal itemPrice = (decimal)itemData.second;

                    if (isCheep)
                    {
                        itemPrice /= 100;
                    }

                    if (isFree)
                    {
                        itemPrice = 0;
                    }

                    //if (boughtLessons && gamesBought.Contains((string)itemData.first))
                    //{
                    //    buxOff += itemPrice;
                    //}

                    sql += String.Format("INSERT INTO Purchases ( user_id, activation_code, item_id, amount_paid, coupon ) "
                        + " VALUES ( {0}, {1}, {2}, {3}, {4} ); ",
                        DBAccess.FormatSQLValue(user_id),
                        DBAccess.FormatSQLValue(actCode),
                        DBAccess.FormatSQLValue((string)itemData.first),
                        DBAccess.FormatSQLValue(itemPrice),
                        DBAccess.FormatSQLValue(CouponCode.Text)
                        );
                    totalPrice += itemPrice;
                }

                if (buxOff > 0)
                {
                    decimal discount = Math.Min(totalPrice, buxOff);

                    sql += DBAccess.FormatSQL("INSERT INTO Purchases ( user_id, activation_code, item_id, amount_paid, coupon ) "
                        + " VALUES ( {0}, {1}, {2}, {3}, {4} ); ",
                        user_id, actCode,
                        (string)validItems[Constants.PURCHASE_BUXOFF].first, -discount, CouponCode.Text);

                    totalPrice -= discount;
                }

                if (totalPrice > 50)
                {
                    decimal discount = totalPrice * 0.1M;

                    sql += DBAccess.FormatSQL("INSERT INTO Purchases ( user_id, activation_code, item_id, amount_paid, coupon ) "
                        + " VALUES ( {0}, {1}, {2}, {3}, {4} ); ",
                        user_id, actCode,
                        (string)validItems[Constants.PURCHASE_PCTOFF].first, -discount, CouponCode.Text);

                    totalPrice -= discount;
                }

                my_access.ExecuteNonQuery(sql, out err_msg);

                if (err_msg != null)
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "An error occured while purchase information for email {0}: {1}", email, err_msg);
                    Master.SetErrorMessage("An error occured while trying to process your order. "
                        + "  Please contact {1} with this error id: {0}", log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                    return;
                }

                Session[ProjectUtils.Constants.SESSION_ACT_CODE] = actCode;
                Session[ProjectUtils.Constants.SESSION_TOTAL_PRICE] = totalPrice;
                Session[ProjectUtils.Constants.SESSION_COUPON] = couponCode;
                Session[ProjectUtils.Constants.SESSION_SCHOOL_NAME] = schoolName;

                Response.Redirect(OrderConfirmation.Url());
            }
            catch (Exception ex)
            {
                // The Response.Redirect above throws a ThreadAbortException, don't complain about that
                if (!(ex is ThreadAbortException))
                {
                    int log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                        "An exception occured while processing purchase for email {0}: {1} {2}", email, ex.Message, ex.StackTrace);
                    Master.SetErrorMessage("An error occured while trying to process your order. "
                        + "  Please contact {1} with this error id: {0}", log_id, ProjectUtils.Constants.SUPPORT_EMAIL);
                }
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
