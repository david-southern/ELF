using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
// using System.Web.Mail;
using System.Net.Mail;

namespace ProjectUtils
{
    public class SendEmail
    {
        private static readonly string SMTP_SERVER = "smtp.e-l-fun.com";
        private static readonly int SMTP_PORT = 25;
        private static readonly string SMTP_USER = "support@e-l-fun.com";
        private static readonly string SMTP_PASS = "NGkoY%q^";

        private static readonly int SMTP_TIMEOUT_MILLIS = 60000;

        public static bool SendSupportEmail(string sender, string to, string cc, string bcc,
            string subject, string body)
        {
            if (to == null || to.Length < 1)
            {
                throw new ArgumentException("[to] parameter cannot be null in SendSupportEmail");
            }

            if (subject == null || subject.Length < 1)
            {
                throw new ArgumentException("[subject] parameter cannot be null in SendSupportEmail");
            }

            if (body == null || body.Length < 1)
            {
                throw new ArgumentException("[body] parameter cannot be null in SendSupportEmail");
            }

            string[] to_arr = { to };

            string[] cc_arr = null;
            string[] bcc_arr = null;

            if (cc != null && cc.Length > 0)
            {
                cc_arr = new string[1];
                cc_arr[0] = cc;
            }

            if (bcc != null && bcc.Length > 0)
            {
                bcc_arr = new string[1];
                bcc_arr[0] = bcc;
            }

            return SendSupportEmail(sender, to_arr, cc_arr, bcc_arr, subject, body);
        }

        public static bool SendSupportEmail(string sender, string[] to, string[] cc, string[] bcc,
            string subject, string body)
        {
            string log_cat = "SendSupportEmail";
            DBAccess my_access = new DBAccess("SendEmail");

            if (sender == null)
            {
                sender = SMTP_USER;
            }

            if (to == null || to.Length < 1)
            {
                throw new ArgumentException("[to] parameter cannot be null in SendSupportEmail");
            }

            if (subject == null || subject.Length < 1)
            {
                throw new ArgumentException("[subject] parameter cannot be null in SendSupportEmail");
            }

            if (body == null || body.Length < 1)
            {
                throw new ArgumentException("[body] parameter cannot be null in SendSupportEmail");
            }

            try
            {
                MailAddress senderAddress = new MailAddress(sender, "ELF Support");

                string toAddressesString = String.Join(",", to);

                MailMessage mailMsg = new MailMessage();
                mailMsg.From = senderAddress;
                mailMsg.To.Add(toAddressesString);

                if (cc != null)
                {
                    string ccAddressesString = String.Join(",", cc);
                    mailMsg.CC.Add(ccAddressesString);
                }

                if (bcc != null)
                {
                    string bccAddressesString = String.Join(",", bcc);
                    mailMsg.Bcc.Add(bccAddressesString);
                }

                mailMsg.Subject = subject;
                mailMsg.IsBodyHtml = true;
                mailMsg.Body = body;

                SmtpClient smtpClient = new SmtpClient(SMTP_SERVER, SMTP_PORT);
                System.Net.NetworkCredential credentials = new NetworkCredential(SMTP_USER, SMTP_PASS);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = credentials;
                smtpClient.Timeout = SMTP_TIMEOUT_MILLIS;
                smtpClient.Send(mailMsg);
                return true;
            }
            catch (Exception ex)
            {
                my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while trying to send email: {0} at {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }

#if false
        // GMail settings
        private static readonly string SMTP_SERVER = "smtp.gmail.com";
        private static readonly int SMTP_PORT = 587;
        private static readonly string SMTP_USER = "support@snapdirectory.com";
        private static readonly string SMTP_PASS = "h0tbl@ck";

        public static bool SendSupportGmail(string sender, string[] to, string[] cc, string[] bcc,
            string subject, string body)
        {
            string log_cat = "SendSupportEmail";
            DBAccess my_access = new DBAccess("SendEmail");

            if (sender == null)
            {
                sender = SMTP_USER;
            }

            if (to == null || to.Length < 1)
            {
                throw new ArgumentException("[to] parameter cannot be null in SendSupportEmail");
            }

            if (subject == null || subject.Length < 1)
            {
                throw new ArgumentException("[subject] parameter cannot be null in SendSupportEmail");
            }

            if (body == null || body.Length < 1)
            {
                throw new ArgumentException("[body] parameter cannot be null in SendSupportEmail");
            }

            try
            {
                MailAddress senderAddress = new MailAddress(sender, "SnapDirectory Support");

                string toAddressesString = String.Join(",", to);

                MailMessage mailMsg = new MailMessage();
                mailMsg.From = senderAddress;
                mailMsg.To.Add(toAddressesString);

                if (cc != null)
                {
                    string ccAddressesString = String.Join(",", cc);
                    mailMsg.CC.Add(ccAddressesString);
                }

                if (bcc != null)
                {
                    string bccAddressesString = String.Join(",", bcc);
                    mailMsg.Bcc.Add(bccAddressesString);
                }

                mailMsg.Subject = subject;
                mailMsg.IsBodyHtml = true;
                mailMsg.Body = body;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                SmtpClient smtpClient = new SmtpClient(SMTP_SERVER, SMTP_PORT);
                System.Net.NetworkCredential credentials = new NetworkCredential(SMTP_USER, SMTP_PASS);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = SMTP_TIMEOUT_MILLIS;
                smtpClient.Send(mailMsg);
                return true;
            }
            catch (Exception ex)
            {
                my_access.LogMessage(DBAccess.LOG_LEVEL_ERROR, log_cat,
                    "Caught exception while trying to send email: {0} at {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }
#endif

    }
}