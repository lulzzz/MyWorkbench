using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Ignyt.BusinessInterface.Communication.Enum;
using System;
using System.ComponentModel;

namespace Ignyt.BusinessInterface.Communication
{
    public interface IEmail
    {
        IEmailPopup CurrentObject { get; set; }
        Guid ObjectOid { get; set; }
        Type ObjectType { get; set; }
        Party Party { get; set; }
        DateTime Created { get; set; }
        DateTime? Sent { get; set; }
        BindingList<IEmailAddress> ToEmail { get; }
        BindingList<IEmailAddress> CCEmail { get; }
        BindingList<IEmailAddress> BCCEmail { get; }
        string Subject { get; set; }
        string Body { get; set; }
        CommunicationNotificationStatus Status { get; set; }
        BindingList<IEmailAttachment> Attachments { get; }
        string Signature { get; }
        string Footer { get; }
        IEmailAddress FromEmail { get; }
        IEmailAddress ReplyTo { get; }
        string UniqueProviderIdentifier { get; set; }
    }
}
