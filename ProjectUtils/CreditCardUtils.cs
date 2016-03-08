using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ProjectUtils
{
    public class CCBillingException : Exception
    {
        public CCBillingException(string message)
            : base(message)
        {
        }
    }

    public static class CreditCardUtils
    {
        private static readonly string MERCHANT_USERNAME = "<redacted>";
        private static readonly string MERCHANT_PASSWORD = "<redacted>";
        private static readonly string GATEWAY_URL = "<redacted>";

        public class CardInfo
        {
            public string cardNum;
            public int expMonth, expYear;
            public string cvv;
            public string firstName;
            public string lastName;
            public string address1;
            public string address2;
            public string city;
            public string state;
            public string zip;

            internal string getExpDate()
            {
                return String.Format("{0:00}{1:00}", expMonth, expYear);
            }
        }

        public class BillingTxnResult
        {
            public bool success;
            public string declineMessage;

            public int response;
            public string authCode;
            public string txnId;
            public string avsResponse;
            public string cvvResponse;
            public string responseCode;
            public string responseText;

            public int billingAuditID;
        }

        public static Dictionary<string, string> AVSResponseCodeText;
        public static Dictionary<string, string> CVVResponseCodeText;
        public static Dictionary<string, string> ResponseCodeText;

        static CreditCardUtils()
        {
            AVSResponseCodeText = new Dictionary<string, string>();
            CVVResponseCodeText = new Dictionary<string, string>();
            ResponseCodeText = new Dictionary<string, string>();

            AVSResponseCodeText.Add("X", "Exact match, 9-character numeric ZIP ");
            AVSResponseCodeText.Add("Y", "Exact match, 5-character numeric ZIP ");
            AVSResponseCodeText.Add("D", "Exact match, 5-character numeric ZIP ");
            AVSResponseCodeText.Add("M", "Exact match, 5-character numeric ZIP ");
            AVSResponseCodeText.Add("A", "Address match only ");
            AVSResponseCodeText.Add("B", "Address match only ");
            AVSResponseCodeText.Add("W", "9-character numeric ZIP match only ");
            AVSResponseCodeText.Add("Z", "5-character Zip match only ");
            AVSResponseCodeText.Add("P", "5-character Zip match only ");
            AVSResponseCodeText.Add("L", "5-character Zip match only ");
            AVSResponseCodeText.Add("N", "No address or ZIP match ");
            AVSResponseCodeText.Add("C", "No address or ZIP match ");
            AVSResponseCodeText.Add("U", "Address information for cardholder is unavailable ");
            AVSResponseCodeText.Add("G", "Non-U.S. Issuer does not participate ");
            AVSResponseCodeText.Add("I", "Non-U.S. Issuer does not participate ");
            AVSResponseCodeText.Add("R", "Retry, AVS system is unavailable ");
            AVSResponseCodeText.Add("E", "Not a mail/phone order ");
            AVSResponseCodeText.Add("S", "AVS is not supported by card issuing bank ");
            AVSResponseCodeText.Add("0", "AVS Not Available ");
            AVSResponseCodeText.Add("O", "AVS Not Available ");
            // AVSResponseCodeText.Add("B", "AVS Not Available ");

            CVVResponseCodeText.Add("M", "CVV2/CVC2 Match ");
            CVVResponseCodeText.Add("N", "CVV2/CVC2 No Match ");
            CVVResponseCodeText.Add("P", "Not Processed ");
            CVVResponseCodeText.Add("S", "Merchant has indicated that CVV2/CVC2 is not present on card ");
            CVVResponseCodeText.Add("U", "Issuer is not certified and/or has not provided Visa encryption keys ");
        }

        private static string addPostVar(string varString, string varName, string varValue)
        {
            string retval = varString;

            if (retval.Length > 0)
            {
                retval += "&";
            }

            retval += WebUtils.PreventXSS(varName) + "=" + WebUtils.PreventXSS(varValue);

            return retval;
        }

        private static void LogAndThrow(DBAccess my_access, string log_cat, Exception ex,
            string formatString, params object[] formatParams)
        {
            if (ex != null && ex is CCBillingException)
            {
                throw ex;
            }

            int log_id;

            string message = String.Format(formatString, formatParams);

            if (ex != null)
            {
                message += "Exception: " + ex.Message + " " + ex.StackTrace;
            }

            log_id = my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat, message);

            throw new CCBillingException(String.Format("An error occured while attempting to bill your credit card.  "
                + "Please contact {0} with error code: {1}",
                ProjectUtils.Constants.SUPPORT_EMAIL, log_id));
        }

        public static BillingTxnResult BillCreditCard(CardInfo info, string actCode, decimal amount)
        {
            string log_cat = "BillCreditCard";
            DBAccess my_access = null;
            string sql, err_msg;
            string pageResult = null;
            string diagPostVars = "";
            BillingTxnResult result = new BillingTxnResult();

            try
            {
                my_access = new DBAccess("CreditCardUtils");

                my_access.LogMessage(DBAccess.LOG_LEVEL_INFO, log_cat,
                    "Sending billing request to payment gateway: ActCode: {0}", actCode);

                sql = String.Format("INSERT INTO BillingAudit ( activation_code, amount, payment_status ) "
                    + " VALUES ( {0}, {1}, 'Starting' )",
                    DBAccess.FormatSQLValue(actCode),
                    DBAccess.FormatSQLValue(amount)
                    );

                my_access.ExecuteNonQuery(sql, out err_msg);

                if (err_msg != null)
                {
                    LogAndThrow(my_access, log_cat, null, "DB Error while inserting Starting BillingAudit: {0}", err_msg);
                }

                string postVars = "";

                postVars = addPostVar(postVars, "type", "sale");
                postVars = addPostVar(postVars, "payment", "creditcard");

                postVars = addPostVar(postVars, "firstname", info.firstName);
                postVars = addPostVar(postVars, "lastname", info.lastName);
                postVars = addPostVar(postVars, "address1", info.address1);

                if (!String.IsNullOrEmpty(info.address2))
                {
                    postVars = addPostVar(postVars, "address2", info.address2);
                }
                postVars = addPostVar(postVars, "city", info.city);
                postVars = addPostVar(postVars, "state", info.state);
                postVars = addPostVar(postVars, "zip", info.zip);
                postVars = addPostVar(postVars, "country", "US");

                postVars = addPostVar(postVars, "ccexp", info.getExpDate());
                postVars = addPostVar(postVars, "amount", String.Format("{0:F2}", amount));
                postVars = addPostVar(postVars, "orderid", actCode);

                postVars = addPostVar(postVars, "username", MERCHANT_USERNAME);
                postVars = addPostVar(postVars, "password", MERCHANT_PASSWORD);

                diagPostVars = postVars;

                postVars = addPostVar(postVars, "ccnumber", info.cardNum);
                postVars = addPostVar(postVars, "cvv", info.cvv);

                try
                {
                    sql = String.Format("INSERT INTO BillingAudit ( activation_code, amount, payment_status ) "
                        + " VALUES ( {0}, {1}, 'Posting' )",
                        DBAccess.FormatSQLValue(actCode),
                        DBAccess.FormatSQLValue(amount)
                        );

                    my_access.ExecuteNonQuery(sql, out err_msg);

                    if (err_msg != null)
                    {
                        LogAndThrow(my_access, log_cat, null, "DB Error while inserting Posting BillingAudit: {0}", err_msg);
                    }

                    pageResult = WebUtils.DownloadURL(GATEWAY_URL, postVars);

                    sql = String.Format("INSERT INTO BillingAudit ( activation_code, amount, payment_status, payment_full_response ) "
                        + " VALUES ( {0}, {1}, 'Parsing', {2} )",
                        DBAccess.FormatSQLValue(actCode),
                        DBAccess.FormatSQLValue(amount),
                        DBAccess.FormatSQLValue(pageResult)
                        );

                    my_access.ExecuteNonQuery(sql, out err_msg);

                    if (err_msg != null)
                    {
                        LogAndThrow(my_access, log_cat, null, "DB Error while inserting Parsing BillingAudit: {0}", err_msg);
                    }

                }
                catch (Exception ex)
                {
                    LogAndThrow(my_access, log_cat, ex, "Posting billing request to gateway: ActCode: {0}, postVars: {1}", actCode, diagPostVars);
                }

                my_access.LogMessage(DBAccess.LOG_LEVEL_INFO, log_cat,
                    "Received billing response from payment gateway: ActCode: {0}, pageResult: {1}",
                    actCode, pageResult);

                if (String.IsNullOrEmpty(pageResult))
                {
                    LogAndThrow(my_access, log_cat, null, "Empty pageResponse received from payment gateway for ActCode: {0}, postVars: {1}", actCode, diagPostVars);
                }

                string[] matchStrings;

                if (!WebUtils.FindRegexMatches(pageResult, "response=([123])", out matchStrings) || matchStrings.Length != 1)
                {
                    LogAndThrow(my_access, log_cat, null, "No response field found in payment gateway reply for ActCode: {0}, postVars: {1}", actCode, postVars);
                }

                if (!Int32.TryParse(matchStrings[0], out result.response) || result.response < 1 || result.response > 3)
                {
                    LogAndThrow(my_access, log_cat, null, "Invalid response field '{0}' found in payment gateway reply for ActCode: {1}, postVars: {2}", matchStrings[0], actCode, diagPostVars);
                }

                if (WebUtils.FindRegexMatches(pageResult, "responsetext=([^&]*)", out matchStrings) && matchStrings.Length > 0)
                {
                    result.responseText = matchStrings[0];
                }

                if (result.response == 3)
                {   // CVV must be 3 or 4 digits 

                    string declineMessage = null;

                    if (result.responseText.StartsWith("CVV must be 3 or 4 digits"))
                    {
                        declineMessage = "<li>The security code was an incorrect format, it should be either 3 or 4 numbers.</li>";
                    }

                    if (result.responseText.StartsWith("Invalid Credit Card Number"))
                    {
                        declineMessage = "<li>The credit card number was invalid.</li>";
                    }

                    if (!String.IsNullOrEmpty(declineMessage))
                    {
                        result.success = false;

                        result.declineMessage = "<div style='text-align: left;'><span style='font-size: 130%'>Your credit card payment was declined by the payment processor.</span><br>";
                        result.declineMessage += "  The payment processor supplied these reasons for the decline: <ul style='margin: 0'>" + declineMessage + "</ul>";
                        result.declineMessage += "</div>";
                        return result;
                    }

                    LogAndThrow(my_access, log_cat, null, "Payment gateway returned an error code 3(Error in transaction data or system error) with responseText: {0} for ActCode: {1}, postVars: {2}", result.responseText, actCode, diagPostVars);
                }

                if (WebUtils.FindRegexMatches(pageResult, "authcode=([^&]*)", out matchStrings) && matchStrings.Length > 0)
                {
                    result.authCode = matchStrings[0];
                }

                if (WebUtils.FindRegexMatches(pageResult, "transactionid=([^&]*)", out matchStrings) && matchStrings.Length > 0)
                {
                    result.txnId = matchStrings[0];
                }

                if (WebUtils.FindRegexMatches(pageResult, "avsresponse=([^&]*)", out matchStrings) && matchStrings.Length > 0)
                {
                    result.avsResponse = matchStrings[0];
                }

                if (WebUtils.FindRegexMatches(pageResult, "cvvresponse=([^&]*)", out matchStrings) && matchStrings.Length > 0)
                {
                    result.cvvResponse = matchStrings[0];
                }

                if (WebUtils.FindRegexMatches(pageResult, "response_code=([^&]*)", out matchStrings) && matchStrings.Length > 0)
                {
                    result.responseCode = matchStrings[0];
                }

                if (result.response == 1)
                {
                    result.success = true;
                }
                else
                {
                    result.success = false;
                    result.declineMessage = "<div style='text-align: left;'><span style='font-size: 130%'>Your credit card payment was declined by the payment processor.</span><br>";

                    string addlMessage = "";

                    if (result.cvvResponse == "N")
                    {
                        addlMessage += "<li>The payment processor indicates that the Security Code or Expiration Date does not match that of the card.</li>";
                    }
                    else if (!String.IsNullOrEmpty(result.cvvResponse) && result.cvvResponse != "M")
                    {
                        addlMessage += "<li>The payment processor indicates that there was an error with the Security Code verification.</li>";
                    }

                    if (result.avsResponse == "G" || result.avsResponse == "I")
                    {
                        addlMessage += "<li>The payment processor indicates that address is not a US address, and is not accepted.</li>";
                    }
                    else if (result.avsResponse == "N" || result.avsResponse == "C" || result.avsResponse == "W"
                        || result.avsResponse == "A" || result.avsResponse == "Z")
                    {
                        addlMessage += "<li>The payment processor indicates that the address or ZIP supplied does not match the card's billing address or ZIP.</li>";
                    }
                    else if (result.avsResponse == "U")
                    {
                        // If CVV fails then AVS returns U even if the address is correct, so only report the 
                        // U code if CVV was successful
                        if (result.cvvResponse == "M")
                        {
                            addlMessage += "<li>The payment processor indicates that the address or ZIP supplied was incorrect.</li>";
                        }
                    }
                    else if (result.avsResponse == "S")
                    {
                        addlMessage += "<li>The payment processor indicates that your bank does not do address verification, which we require.</li>";
                    }
                    else if (result.avsResponse == "R" || result.avsResponse == "E"
                        || result.avsResponse == "0" || result.avsResponse == "O" || result.avsResponse == "B")
                    {
                        addlMessage += "<li>There was a problem with the payment processor's address verification system.</li>";
                    }
                    else if (!String.IsNullOrEmpty(result.avsResponse) && result.avsResponse != "Y"
                        && result.avsResponse != "X" && result.avsResponse != "D" && result.avsResponse != "M")
                    {
                        addlMessage += "<li>There was a unknown error with the payment processor's address verification system.</li>";
                    }

                    if (!String.IsNullOrEmpty(addlMessage))
                    {
                        result.declineMessage += "  The payment processor supplied these reasons for the decline: <ul style='margin: 0'>" + addlMessage + "</ul>";
                    }

                    result.declineMessage += "</div>";
                }

                string status = result.response == 1 ? "Success" : result.response == 2 ? "Declined" : "Error";

                my_access.LogMessage(DBAccess.LOG_LEVEL_INFO, log_cat,
                    "Completed billing credit card: ActCode: {0}, Status: {1}",
                    actCode, status);

                return result;
            }
            catch (Exception ex)
            {
                LogAndThrow(my_access, log_cat, ex, "Unknown billing exception");
            }
            finally
            {
                if (my_access != null)
                {
                    string finalStatus = result.response == 1 ? "Success" : result.response == 2 ? "Declined" : "Error";

                    sql = String.Format("INSERT INTO BillingAudit ( activation_code, amount, payment_status, "
                        + "payment_full_response, payment_txn_id, payment_auth_code, payment_avs_response, "
                        + "payment_cvv_response, payment_response_code, payment_response_text ) "
                        + " VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9} )",
                    DBAccess.FormatSQLValue(actCode),
                    DBAccess.FormatSQLValue(amount),
                    DBAccess.FormatSQLValue(finalStatus),
                    DBAccess.FormatSQLValue(pageResult),
                    DBAccess.FormatSQLValue(result.txnId),
                    DBAccess.FormatSQLValue(result.authCode),
                    DBAccess.FormatSQLValue(result.avsResponse),
                    DBAccess.FormatSQLValue(result.cvvResponse),
                    DBAccess.FormatSQLValue(result.responseCode),
                    DBAccess.FormatSQLValue(result.responseText)
                    );

                    my_access.ExecuteNonQuery(sql, out err_msg);

                    if (err_msg != null)
                    {
                        LogAndThrow(my_access, log_cat, null, "DB Error while inserting Final({0}) BillingAudit for ActCode: {1}, postVars: {2}.  Err Msg: {3}", finalStatus, actCode, diagPostVars, err_msg);
                    }

                    my_access.CloseConnection();
                }
            }

            // We shouldn't be able to get here, but add this to keep the compiler happy
            return null;
        }
    }
}
