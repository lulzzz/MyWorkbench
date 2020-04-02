using Ignyt.BusinessInterface.Communication.Enum;
using Ignyt.Webfunctions;
using MyWorkbench.BusinessObjects.Communication;
using MyWorkbench.BusinessObjects.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWorkBench.Service.Api
{
    public class Emails : ApiBase {
        public Emails(string ConnectionString) : base(ConnectionString)  {}

        protected override void ProcessClient() {
            try {
               base.ProcessClient();

               this.ProcessEmails();
            } catch (Exception ex) {
                throw ex;
            }
        }

        private void ProcessEmails() {
            this.ProcessEmail();
            this.DataSourceHelper.Dispose();
        }

        private void ProcessEmail()
        {
            try
            {
                List<Email> emailObjects = this.DataSourceHelper.Select<Email>(typeof(Email), "Sent Is Null").OfType<Email>().ToList();

                if (emailObjects.Count >= 1)
                    emailObjects.ForEach(ProcessEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessEmail(Email Email)
        {
            try
            {
                if (Email.DateTimeToSend != null && Email.DateTimeToSend >= MyWorkbench.BaseObjects.Constants.Constants.DateTimeTimeZone(this.DataSourceHelper.Session))
                    return;

                SendGridFunctionResult sendGridFunctionResult = new SendGridFunctions(Ignyt.Framework.Application.MyWorkbench, this.DataSourceHelper.Database).ExecuteEmail(Email).Result;

                if (!string.IsNullOrEmpty(sendGridFunctionResult.MessageIdenfifier))
                    EmailHelper.UpdateEmailProcessed(Email, sendGridFunctionResult.MessageIdenfifier, sendGridFunctionResult.StatusCode, sendGridFunctionResult.Description, CommunicationNotificationStatus.Accepted);
                else
                    EmailHelper.UpdateEmailNotProcessed(Email, sendGridFunctionResult.Description, sendGridFunctionResult.StatusCode, CommunicationNotificationStatus.Unsuccessful);
            }
            catch (Exception ex)
            {
                EmailHelper.UpdateEmailNotProcessed(Email, ex.Message,"Internal System Error", CommunicationNotificationStatus.SystemError);
                this.TaskExceptionList.TaskExceptions.Add(ex);
            }
        }
    }
}
