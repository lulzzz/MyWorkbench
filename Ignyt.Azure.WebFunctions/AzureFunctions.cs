using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Communication;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Ignyt.Azure.WebFunctions
{
    public static class AzureFunctions
    {
        private static readonly string _sendGridApiKey = "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";

        private static SendGridClient _sendGridClient;
        public static SendGridClient SendGridClient {
            get {
                if(_sendGridClient == null)
                    _sendGridClient = new SendGridClient(_sendGridApiKey);
                return _sendGridClient;
            }
        }


        public static async Task<string> ExecuteEmail(IEmail Email)
        {
            try
            {
                var msg = new SendGridMessage();

                msg.SetFrom(new EmailAddress(Email.FromEmail.Email, Email.FromEmail.FullName));

                foreach (IEmailAddress emailAddress in Email.ToEmail)
                {
                    msg.AddTo(emailAddress.Email, emailAddress.FullName);
                }

                foreach (IEmailAddress emailAddress in Email.CCEmail)
                {
                    msg.AddCc(emailAddress.Email, emailAddress.FullName);
                }

                foreach (IEmailAddress emailAddress in Email.BCCEmail)
                {
                    msg.AddBcc(emailAddress.Email, emailAddress.FullName);
                }

                foreach (IEmailAttachment emailAttachment in Email.Attachments)
                {
                    msg.AddAttachment(emailAttachment.FileName, emailAttachment.Content);
                }

                msg.SetSubject(Email.Subject);
                msg.AddContent(MimeType.Html, Email.Body);

                var response = await SendGridClient. SendEmailAsync(msg);

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
