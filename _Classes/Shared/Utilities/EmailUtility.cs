using System;
using System.Net.Mail;
using MailMessage = System.Net.Mail.MailMessage;

namespace symmons.com._Classes.Shared.Utilities
{
    /// <summary>
    /// The EmailUtility class contains methods for sending emails through the ASP.NET framework 
    /// and logging those emails in the custom site database.
    /// </summary>
    public class EmailUtility
    {
        public static bool SendEmail(string emailText, string emailTo, string emailFrom, string emailSubject, string emailCC, string emailBcc, string emailAttachment)
        {
            emailText = emailText.Replace("\r\n", "<br />");
            emailText = emailText.Replace("\n", "<br />");
            emailText = emailText.Replace("\r", "<br />");

            var objMail = new MailMessage(emailFrom, emailTo, emailSubject, emailText) {Body = emailText};
            if (!String.IsNullOrEmpty(emailCC))
                objMail.CC.Add(emailCC);
            if (!String.IsNullOrEmpty(emailBcc))
                objMail.Bcc.Add(emailBcc);
            objMail.IsBodyHtml = true;

            if (emailAttachment != String.Empty)
                if (System.IO.File.Exists(emailAttachment))
                {
                    var mailAttachment = new Attachment(emailAttachment);
                    objMail.Attachments.Add(mailAttachment);
                }

            //setting for email smtp, etc are in <system.net> node in web.config
            var emailClient = new SmtpClient();

            //Attempt to send
            emailClient.Send(objMail);

            if (System.IO.File.Exists(emailAttachment))
                System.IO.File.Delete(emailAttachment);

            return true;
        }
    }
}