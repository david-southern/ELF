using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectUtils;
using System.Data.Common;
using System.Drawing;

namespace Website
{
    public partial class Verslag : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // LoginUtils.RequireLogin();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (LoginUtils.GetProfileString(Constants.PROFILE_ADMIN_TAG) != Constants.PROFILE_ADMIN_TAG)
            //{
            //    ErrorMessage.Text = "You must be logged in as an admin to view this page.";
            //    ErrorMessage.ForeColor = System.Drawing.Color.Red;
            //    ErrorMessage.Font.Size = FontUnit.XXLarge;
            //    NoLoginPanel.Visible = false;
            //    return;
            //}

            if (!IsPostBack)
            {
                RegenerateClick(null, null);
            }
        }

        protected void RegenerateClick(object sender, EventArgs e)
        {
            DBAccess my_access = null;

            try
            {
                my_access = new DBAccess(this);

                BindPurchases(my_access);
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = HttpUtility.HtmlEncode(ex.Message);
                ErrorMessage.ForeColor = Color.Red;
                ErrorMessage.Font.Size = FontUnit.XXLarge;
            }
            finally
            {
                if (my_access != null)
                {
                    my_access.CloseConnection();
                }
            }
        }

        private class SalesItem : IComparable
        {
            public int sort_order { get; set; }
            public string item_code { get; set; }
            public int units_sold { get; set; }
            public decimal total_sales { get; set; }

            public SalesItem()
            {
                sort_order = int.MinValue;
            }

            public override string ToString()
            {
                return String.Format("{0}({1}): {2:c}", item_code, units_sold, total_sales);
            }
            #region IComparable Members

            public int CompareTo(object obj)
            {
                SalesItem other = obj as SalesItem;

                if (obj == null)
                {
                    return 0;
                }

                if (this.sort_order == int.MinValue || other.sort_order == int.MinValue)
                {
                    return this.item_code.CompareTo(other.item_code);
                }

                return this.sort_order.CompareTo(other.sort_order);
            }

            #endregion
        }

        private void BindPurchases(DBAccess my_access)
        {
            string sql, err_msg;

            Dictionary<string, string> PTAInfo = new Dictionary<string, string>();

            sql = "select org_code, school_name, contact_first_name, contact_last_name, contact_addr1, contact_addr2, contact_city, contact_state, contact_zip, contact_phone from PTARegistration";

            DbDataReader reader = my_access.GetReader(sql, out err_msg);

            if (err_msg != null)
            {
                throw new ExceptionEx("DB Error on PTA select: {0}", err_msg);
            }

            List<SalesItem> salesByItemList = new List<SalesItem>();
            List<SalesItem> salesByOrgList = new List<SalesItem>();

            SalesByTypeGrid.DataSource = salesByItemList;
            SalesByTypeGrid.DataBind();

            SalesByPTAOrgGrid.DataSource = salesByOrgList;
            SalesByPTAOrgGrid.DataBind();

            string org_code = null;

            while (reader.Read())
            {
                org_code = DBAccess.SafeString(reader, "org_code");

                if (String.IsNullOrEmpty(org_code))
                {
                    continue;
                }

                string school_name = DBAccess.SafeString(reader, "school_name");
                string contact_first_name = DBAccess.SafeString(reader, "contact_first_name");
                string contact_last_name = DBAccess.SafeString(reader, "contact_last_name");
                string contact_addr1 = DBAccess.SafeString(reader, "contact_addr1");
                string contact_addr2 = DBAccess.SafeString(reader, "contact_addr2");
                string contact_city = DBAccess.SafeString(reader, "contact_city");
                string contact_state = DBAccess.SafeString(reader, "contact_state");
                string contact_zip = DBAccess.SafeString(reader, "contact_zip");
                string contact_phone = DBAccess.SafeString(reader, "contact_phone");

                string contact_info = String.Format("{7}{8}{0} {1}{8}{2}{8}{3}, {4}  {5}{8}{6}",
                    contact_first_name, contact_last_name, contact_addr1 + (String.IsNullOrEmpty(contact_addr2) ? "" : "<br>" + contact_addr2),
                    contact_city, contact_state, contact_zip, contact_phone, school_name, "<br>");

                PTAInfo.Add(org_code, contact_info);
            }

            DateTime start_date, end_date;

            if (!DateTime.TryParse(StartDate.Text, out start_date))
            {
                throw new ExceptionEx("Unknown StartDate: '{0}'", StartDate.Text);
            }

            if (!DateTime.TryParse(EndDate.Text, out end_date))
            {
                throw new ExceptionEx("Unknown EndDate: '{0}'", EndDate.Text);
            }

            sql = DBAccess.FormatSQL("SELECT pi.item_desc, p.activation_code, p.coupon, p.amount_paid, pi.cart_order "
                + " FROM Purchases p JOIN PurchaseItems pi ON (p.item_id = pi.id) "
                + " WHERE p.paid = 1 AND p.created_utc BETWEEN {0} AND {1} "
                + " AND coupon not like '%DONTBILLME%' AND coupon not like '%FREESTUFF%' AND coupon not like '%CHEEPSTUFF%' "
                + " ORDER BY p.activation_code, p.amount_paid DESC ", start_date, end_date);

            reader = my_access.GetReader(sql, out err_msg);

            if (err_msg != null)
            {
                throw new ExceptionEx("DB Error on Purchases select: {0}", err_msg);
            }

            if (!reader.HasRows)
            {
                throw new ExceptionEx("No sales data found between {0} and {1}", StartDate.Text, EndDate.Text);
            }

            Dictionary<string, SalesItem> salesByItem = new Dictionary<string, SalesItem>();
            Dictionary<string, SalesItem> salesByOrg = new Dictionary<string, SalesItem>();

            List<SalesItem> transactionList = new List<SalesItem>();
            string current_activation_code = null, current_coupon = null;
            decimal running_total = 0.0M, running_discount = 0.0M;

            Action AggregateTransaction = delegate()
            {
                if (transactionList.Count > 0)
                {
                    decimal discount_pct = (running_total + running_discount) / running_total;

                    if (!String.IsNullOrEmpty(org_code) && !PTAInfo.ContainsKey(org_code))
                    {
                        throw new ExceptionEx("Unknown org code '{0}' found in coupon '{1}'", org_code, current_coupon);
                    }

                    foreach (SalesItem item in transactionList)
                    {
                        item.total_sales *= discount_pct;

                        if (!salesByItem.ContainsKey(item.item_code))
                        {
                            salesByItem[item.item_code] = item;
                        }
                        else
                        {
                            salesByItem[item.item_code].units_sold++;
                            salesByItem[item.item_code].total_sales += item.total_sales;
                        }

                        if (!String.IsNullOrEmpty(org_code))
                        {
                            if (!salesByOrg.ContainsKey(org_code))
                            {
                                SalesItem org_item = new SalesItem();
                                org_item.item_code = PTAInfo[org_code];
                                org_item.units_sold = 0;
                                org_item.total_sales = 0;
                                salesByOrg[org_code] = org_item;
                            }

                            salesByOrg[org_code].units_sold++;
                            salesByOrg[org_code].total_sales += item.total_sales;
                        }
                    }
                    transactionList.Clear();
                }
            };

            while (reader.Read())
            {
                string activation_code = DBAccess.SafeString(reader, "activation_code");
                string coupon = DBAccess.SafeString(reader, "coupon");
                string item_desc = DBAccess.SafeString(reader, "item_desc");
                decimal amount_paid = (decimal)reader["amount_paid"];
                int sort_order = (int)reader["cart_order"];

                if (activation_code != current_activation_code)
                {
                    org_code = GetOrgCodeFromCoupon(current_coupon);
                    AggregateTransaction();
                    current_activation_code = activation_code;
                    current_coupon = coupon;
                    running_total = 0.0M;
                    running_discount = 0.0M;
                }

                if (amount_paid > 0)
                {
                    SalesItem item = new SalesItem();
                    item.sort_order = sort_order;
                    item.item_code = item_desc;
                    item.units_sold = 1;
                    item.total_sales = amount_paid;
                    transactionList.Add(item);
                    running_total += amount_paid;
                }

                if (amount_paid < 0)
                {
                    running_discount += amount_paid;
                }
            }

            org_code = GetOrgCodeFromCoupon(current_coupon);
            AggregateTransaction();

            salesByItemList = salesByItem.Values.ToList<SalesItem>();
            salesByOrgList = salesByOrg.Values.ToList<SalesItem>();

            salesByItemList.Sort();
            salesByOrgList.Sort();

            SalesByTypeGrid.DataSource = salesByItemList;
            SalesByTypeGrid.DataBind();

            SalesByPTAOrgGrid.DataSource = salesByOrgList;
            SalesByPTAOrgGrid.DataBind();
        }

        public string GetOrgCodeFromCoupon(string current_coupon)
        {
            if (current_coupon == null)
            {
                return null;
            }

            current_coupon = current_coupon.Replace(Constants.COUPON_50BUXOFF, "")
                .Replace(Constants.COUPON_CHEEPSTUFF, "")
                .Replace(Constants.COUPON_DONTBILLME, "")
                .Replace(Constants.COUPON_FREESTUFF, "");

            current_coupon = current_coupon.Trim(" ,.-".ToCharArray()).Trim();

            return current_coupon;
        }
    }
}
