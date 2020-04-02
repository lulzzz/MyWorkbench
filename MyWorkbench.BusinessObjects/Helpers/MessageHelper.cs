using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Ignyt.BusinessInterface.Communication.Enum;
using MyWorkbench.BusinessObjects.Communication;
using MyWorkbench.BaseObjects.Constants;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public static class MessageHelper
    {
        public static void UpdateMessageProcessed(Message Message, CommunicationNotificationStatus CommunicationNotificationStatus,string Responses)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(Message.Session.DataLayer))
            {
                Message message = unitOfWork.FindObject<Message>(CriteriaOperator.Parse("Oid == ?", Message.Oid));

                message.CommunicationLog.Add(new CommunicationLog(unitOfWork) { Description = Responses });
                message.Status = CommunicationNotificationStatus;
                message.Sent = Constants.DateTimeTimeZone(unitOfWork);

                unitOfWork.CommitChanges();
            }
        }
    }
}