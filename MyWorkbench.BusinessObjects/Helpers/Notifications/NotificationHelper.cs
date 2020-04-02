using DevExpress.ExpressApp;
using DevExpress.Xpo;
using Ignyt.Framework;
using Ignyt.Framework.Api;
using Ignyt.BusinessInterface;
using System;
using System.Collections.Generic;

namespace MyWorkbench.BusinessObjects.Helpers.Notifications {
    public class NotificationHelper : ExportHelperBase {
        private ApplicationCredentials _applicationCredentials;
        private Application _application;
        private Session _session;

        public NotificationHelper(Session Session, Application Application, XafApplication XafApplication)
            : base(XafApplication) {
            _session = Session;
            _application = Application;
            _applicationCredentials = ApplicationCredentials.Instance(Application);
        }

        public NotificationHelper(Session Session, Application Application)
            : base(null) {
            _session = Session;
            _application = Application;
            _applicationCredentials = ApplicationCredentials.Instance(Application);
        }

        public NotificationHelper(Application Application)
            : base(null)
        {
            _application = Application;
        }

        public KeyValuePair<object, object> SendSMSNotification(string Number, string Text) {
            KeyValuePair<object, object> result = new KeyValuePair<object, object>();

            try {
                //result = MessageService.InstanceApplication(_application).SendSMS(FormatNumber(Number), Text);
            } catch (Exception ex) {
                result = new KeyValuePair<object, object>("ERR", ex.Message);
            } finally {
                //InsertNotificationsBase(CommunicationMethod.Message, Number, Text, null, null, result.ToString());
            }

            return result;
        }

        public KeyValuePair<object, object> SendSMSNotificationMultitenant(string Number, string Text, string UserName, string Password) {
            KeyValuePair<object, object> result = new KeyValuePair<object, object>();

            try {
                //result = MessageService.InstanceApplication(_application).SendSMS(UserName, Password, FormatNumber(Number), Text);
            } catch (Exception ex) {
                result = new KeyValuePair<object, object>("ERR", ex.Message);
            } finally {
                //InsertNotificationsBase(CommunicationMethod.Message, Number, Text, null, UserName, result.ToString());
            }

            return result;
        }

        public string SendEmailNotification(IEnumerable<IEmailAddress> EmailAddresses, IEnumerable<IEmailAddress> CCEmailAddresses, IEnumerable<IEmailAddress> BCCEmailAddresses, string Subject, string HTMLContent,
            IEmailAddress FromAddress, IEmailAddress ReplyToAddress, IEnumerable<IFileAttachment> Attachments) {
            //EmailService.Instance.Initialize(this._application);
            //return EmailService.Instance.SendWarmupPool(EmailAddresses, CCEmailAddresses, BCCEmailAddresses, Subject, HTMLContent,
            //FromAddress, ReplyToAddress, Attachments);
            return string.Empty;
        }

        private string FormatNumber(string Number) {
            return Number.Left(1) == "0" ? "+27" + Number.Substring(1, Number.Length - 1) : Number;
        }
    }
}
