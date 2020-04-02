using MyWorkbench.BusinessObjects.Communication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWorkBench.Service.Api
{
    public class WebHooks : ApiBase
    {
        public WebHooks(string ConnectionString) : base(ConnectionString) { }

        protected override void ProcessClient()
        {
            try
            {
                base.ProcessClient();

                this.ProcessWebHooks();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessWebHooks()
        {
            this.ProcessWebHook();
            this.DataSourceHelper.Dispose();
        }

        private void ProcessWebHook()
        {
            try
            {
                List<WebhooksEvents> webhooksEvents = this.DataSourceHelper.Select<WebhooksEvents>(typeof(WebhooksEvents), "Executed Is Null").OfType<WebhooksEvents>().ToList();

                if (webhooksEvents.Count >= 1)
                    webhooksEvents.ForEach(ProcessWebHook);
            }
            catch (Exception ex)
            {
                this.TaskExceptionList.TaskExceptions.Add(ex);
            }
        }

        private void ProcessWebHook(WebhooksEvents WebhooksEvent)
        {
            try
            {
                MyWorkbench.BusinessObjects.Helpers.EmailHelper.UpdateReceivedEvent(this.DataSourceHelper.Session, WebhooksEvent.InternalMessageId, WebhooksEvent.Email, WebhooksEvent.EventType.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
