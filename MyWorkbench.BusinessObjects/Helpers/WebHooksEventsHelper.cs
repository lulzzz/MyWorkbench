using DevExpress.Xpo;
using MyWorkbench.BusinessObjects.Communication;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public static class WebHooksEventsHelper
    {
        public static void AddReceivedEvent(Session Session, string InternalMessageId, string EmailAddress, string EventType)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(Session.DataLayer))
            {
                WebhooksEvents webhooksEvents = new WebhooksEvents(unitOfWork)
                {
                    Email = EmailAddress,
                    InternalMessageId = InternalMessageId,
                    EventType = EventType
                };

                unitOfWork.CommitChanges();
            }
        }
    }
}