using Ignyt.Webfunctions;
using Ignyt.BusinessInterface.Communication.Enum;
using MyWorkbench.BusinessObjects.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using MyWorkbench.BusinessObjects.Helpers;
using MyWorkbench.BusinessObjects.Lookups;

namespace MyWorkBench.Service.Api
{
    public class Messages : ApiBase {
        public Messages(string ConnectionString) : base(ConnectionString)  {}

        protected override void ProcessClient() {
            try {
               base.ProcessClient();

               this.ProcessMessages();
            } catch (Exception ex) {
                throw ex;
            }
        }

        private void ProcessMessages() {
            this.ProcessMessage();
            this.DataSourceHelper.Dispose();
        }

        private void ProcessMessage()
        {
            try
            {
                List<Message> messageObjects = this.DataSourceHelper.Select<Message>(typeof(Message), "Sent Is Null").OfType<Message>().ToList();

                if (messageObjects.Count >= 1)
                    messageObjects.ForEach(ProcessMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessMessage(Message Message)
        {
            try
            {
                IEnumerable<TwilioResponse> responses = new TwilioFunctions(Ignyt.Framework.Application.MyWorkbench, this.DataSourceHelper.Database).InitiateASMS(Message.Messages);

                if (responses.Where(g => g.Sid == null).Count() >= 1)
                    MessageHelper.UpdateMessageProcessed(Message, CommunicationNotificationStatus.Unsuccessful, GetResponses(responses));
                else
                    MessageHelper.UpdateMessageProcessed(Message, CommunicationNotificationStatus.Accepted, GetResponses(responses));
            }
            catch (Exception ex)
            {
                MessageHelper.UpdateMessageProcessed(Message, CommunicationNotificationStatus.SystemError, GetResponses(new List<TwilioResponse>() { new TwilioResponse() { Message = ex.Message } }));
            }
        }

        private string GetResponses(IEnumerable<TwilioResponse> Responses)
        {
            string result = null;

            foreach (TwilioResponse response in Responses)
            {
                if(result == null)
                    result = string.Concat(result, response.Message);
                else
                    result = string.Concat(result, ", ", response.Message);
            }

            return result;
        }
    }
}
