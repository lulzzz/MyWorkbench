using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Ignyt.BusinessInterface.Communication.Enum;
using MyWorkbench.BusinessObjects.Communication;
using MyWorkbench.BaseObjects.Constants;
using System;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public static class EmailHelper
    {
        public static void UpdateReceivedEvent(Session Session, string UniqueProviderIdentifier, string EmailAddress, string EventType)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(Session.DataLayer))
            {
                Email email = unitOfWork.FindObject<Email>(CriteriaOperator.Parse("UniqueProviderIdentifier == ?", UniqueProviderIdentifier));

                email.CommunicationLog.Add(new CommunicationLog(unitOfWork) { Description = string.Format("{0} - {1}", EmailAddress, "Updated event notification"), CurrentStatus = EventType });
                email.Status = Ignyt.BusinessInterface.Communication.Enum.CommunicationNotificationStatus.UpdateReceived;

                unitOfWork.CommitChanges();
            }
        }

        public static void UpdateEmailProcessed(Email Email, string MessageIdenfifier, string Status, string Description, CommunicationNotificationStatus CommunicationNotificationStatus)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(Email.Session.DataLayer))
            {
                Email email = unitOfWork.FindObject<Email>(CriteriaOperator.Parse("Oid == ?", Email.Oid));

                email.CommunicationLog.Add(new CommunicationLog(unitOfWork) { Description = string.Format("{0} - {1}", Status, Description), CurrentStatus = Status, MessageIdenfifier = MessageIdenfifier });
                email.Status = CommunicationNotificationStatus;
                email.Sent = Constants.DateTimeTimeZone(unitOfWork);
                email.UniqueProviderIdentifier = MessageIdenfifier;

                unitOfWork.CommitChanges();
            }
        }

        public static void UpdateEmailNotProcessed(Email Email, string Description, string Status, CommunicationNotificationStatus CommunicationNotificationStatus)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(Email.Session.DataLayer))
            {
                Email email = unitOfWork.FindObject<Email>(CriteriaOperator.Parse("Oid == ?", Email.Oid));

                email.CommunicationLog.Add(new CommunicationLog(unitOfWork) { Description = string.Format("{0} - {1}", Status, Description), CurrentStatus = Status });
                email.Status = CommunicationNotificationStatus;

                unitOfWork.CommitChanges();
            }
        }
    }
}