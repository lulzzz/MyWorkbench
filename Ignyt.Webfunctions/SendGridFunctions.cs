using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Communication;
using Ignyt.Framework;
using Sendgrid.Webhooks.Service;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ignyt.Webfunctions
{
    public class SendGridFunctionResult
    {
        public string StatusCode { get; set; }
        public string Description { get; set; }
        public string MessageIdenfifier { get; set; }

        public SendGridFunctionResult(string StatusCode, string Description, string MessageIdenfifier)
        {
            this.StatusCode = StatusCode;
            this.Description = Description;
            this.MessageIdenfifier = MessageIdenfifier;
        }
    }

    public class SendGridFunctions
    {
        public SendGridFunctions(Application Application, string Database)
        {
            this.Application = Application;
            this.Database = Database;
        }

        #region Properties
        public Application Application { get; set; }
        public string Database { get; set; }

        private SendGridClient _sendGridClient;
        public SendGridClient SendGridClient {
            get {
                if (_sendGridClient == null)
                    _sendGridClient = new SendGridClient(Application.GetAttribute<SendGridApiKey>().ToString());
                return _sendGridClient;
            }
        }

        private WebhookParser _webhookParser;
        public WebhookParser WebhookParser {
            get {
                if (_webhookParser == null)
                    _webhookParser = new WebhookParser();
                return _webhookParser;
            }
        }
        #endregion

        public async Task<SendGridFunctionResult> ExecuteEmail(IEmail Email)
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
                msg.AddContent(MimeType.Html, string.Format("{0}{1}{2}", Email.Body,Environment.NewLine, Email.Footer));
                msg.AddCategory(this.Database);
                msg.AddCategory(this.Application.ToString());
                msg.SetClickTracking(true, true);
                msg.SetOpenTracking(true);

                var response = await SendGridClient.SendEmailAsync(msg);
                Dictionary<string, string> result = response.DeserializeResponseHeaders(response.Headers);

                return new SendGridFunctionResult(response.StatusCode.ToString(), response.Body.ToString(), result["X-Message-Id"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
