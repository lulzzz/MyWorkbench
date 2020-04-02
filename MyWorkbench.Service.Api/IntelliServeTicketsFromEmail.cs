using System;
using System.Collections.Generic;
using Diggit.Framework.Api.DNNApi;
using Diggit.Framework.Api.Objects;
using Diggit.Framework.DevExpress;
using Diggit.Framework.Helpers;
using Diggit.Framework.MailSystem.IMAP;
using DevExpress.Data.Filtering;
using System.IO;
using IntelliServe.Module.BusinessObjects;

namespace IntelliServe.Service.Api {
    public class IntelliServeTicketsFromEmail : IDisposable {
        private const string _objectLibrary = "IntelliServe.Module.BusinessObjects";
        private const string _objectLibraryBase = "Diggit.Framework.ExpressApp";
        private const string _objectLibraryReports = "IntelliServe.Module.BusinessReports";
        private Guid _ApplicationUniqueID;
        private static string _connectionString;
        private ClientApplicationsObject _clientApplicationsObject;
        private DataSourceHelper _dataSourceHelper;
        private List<MailMessage> _mailMessages;
        private string _mailServer;
        private int _port;
        private bool _ssl;
        private string _login;
        private string _password;

        public IntelliServeTicketsFromEmail(Guid ApplicationUniqueID, string ConnectionString, string MailServer, int Port, bool Ssl, string Login, string Password) {
            _ApplicationUniqueID = ApplicationUniqueID;
            _connectionString = ConnectionString;
            _mailServer = MailServer;
            _port = Port;
            _ssl = Ssl;
            _login = Login;
            _password = Password;
        }

        private List<MailMessage> MailMessages {
            get {
                if (_mailMessages == null)
                    _mailMessages = new QueryMail(_mailServer, _port, _ssl, _login, _password).GetMail();
                return _mailMessages;
            }
        }

        private bool IsDatabaseSetup {
            get {
                return this._dataSourceHelper.Session.FindObject<IntelliServe.Module.BusinessObjects.Settings>(null) == null ? false : true;
            }
        }

        private void ClientApplicationsObject(string EmailAddress) {
            try {
                this._clientApplicationsObject = null;

                using (DiggitDotNetNukeApi dnnApi = new DiggitDotNetNukeApi()) {
                    this._clientApplicationsObject = dnnApi.ClientDatabase(_ApplicationUniqueID, EmailAddress);
                }
            } catch {
                this._clientApplicationsObject = null;
            }
        }

        public void Process() {
            try {
                if (this.MailMessages.Count >= 1)
                    this.MailMessages.ForEach(ProcessEmail);
            } catch (Exception ex) {
                EventLogHelper.Instance.WriteEventLogException("IntelliServeServiceApi-Tickets", ex, null);
            }
        }

        private IntelliServe.Module.BusinessObjects.User AssignedTo(string EmailAddress) {
            return this._dataSourceHelper.Session.FindObject<IntelliServe.Module.BusinessObjects.User>(CriteriaOperator.Parse(string.Format("UserName = '{0}'", EmailAddress)));
        }

        private IntelliServe.Module.BusinessObjects.ClientContact ClientContact(string EmailAddress) {
            return this._dataSourceHelper.Session.FindObject<IntelliServe.Module.BusinessObjects.ClientContact>(CriteriaOperator.Parse(string.Format("Email = '{0}'", EmailAddress)));
        }

        private void CreateUpdateTicket(MailMessage MailMessage, string ToEmailAddress) {
            Ticket ticket = GetTicketFromSubject(MailMessage.Subject);

            if (ticket != null) {
                ticket.TicketComments.Add(new IntelliServe.Module.BusinessObjects.TicketComment(this._dataSourceHelper.Session) { Comment = GetNewCommentFromBody(MailMessage.Body) });

                foreach (MailMessageAttachment attachment in MailMessage.Attachments) {
                    DevExpress.Persistent.BaseImpl.FileData fileData = new DevExpress.Persistent.BaseImpl.FileData(this._dataSourceHelper.Session);
                    fileData.LoadFromStream(attachment.Name, new MemoryStream(attachment.Content));

                    ticket.TicketAttachments.Add(new IntelliServe.Module.BusinessObjects.TicketAttachment(this._dataSourceHelper.Session) { Description = attachment.Name, Document = fileData });
                }
            } else {
                ticket = new IntelliServe.Module.BusinessObjects.Ticket(this._dataSourceHelper.Session) { AssignedTo = this.AssignedTo(ToEmailAddress), ClientContact = this.ClientContact(MailMessage.FromEmail), Subject = MailMessage.Subject, Description = MailMessage.Body, Email = MailMessage.FromEmail };
                ticket.TicketComments.Add(new IntelliServe.Module.BusinessObjects.TicketComment(this._dataSourceHelper.Session) { Comment = string.Format("Email Ticket created by {0} from {1}", MailMessage.From, _clientApplicationsObject.Database) });

                foreach (MailMessageAttachment attachment in MailMessage.Attachments) {
                    DevExpress.Persistent.BaseImpl.FileData fileData = new DevExpress.Persistent.BaseImpl.FileData(this._dataSourceHelper.Session);
                    fileData.LoadFromStream(attachment.Name, new MemoryStream(attachment.Content));

                    ticket.TicketAttachments.Add(new IntelliServe.Module.BusinessObjects.TicketAttachment(this._dataSourceHelper.Session) { Description = attachment.Name, Document = fileData });
                }
            }
            ticket.Save();
        }

        //Get the comment based on the text above the #% %# line in the body of the email
        private string GetNewCommentFromBody(string body) {
            int pos = -1;
            pos = body.IndexOf("#%");

            if (pos != -1) {
                return body.Substring(0, pos - 1);
            } else return body;
        }

        //find the ticket based on the ticket number in the subject in the form ##TicketNumber##
        private Module.BusinessObjects.Ticket GetTicketFromSubject(string subject) {
            int pos1 = -1;
            int pos2 = -1;
            pos1 = subject.IndexOf("#%");
            pos2 = subject.IndexOf("%#");

            if (pos1 != -1 && pos2 != -1) {
                string ticketNumber = subject.Substring(pos1 + 2, pos2 - pos1 - 1);
                return this._dataSourceHelper.Session.FindObject<IntelliServe.Module.BusinessObjects.Ticket>(new BinaryOperator("TicketNumber", ticketNumber));
            } else
                return null;
        }

        private void ProcessEmail(MailMessage MailMessage) {
            try {
                foreach (string str in MailMessage.ToEmailAddress) {
                    //find the database the email address belongs to, e.g. intelliservediggit@intelliserve.co.za belongs to digg-it
                    this.ClientApplicationsObject(str);

                    if (this._clientApplicationsObject != null) {
                        if (_clientApplicationsObject.Trial & _clientApplicationsObject.DaysRemaining == 0)
                            return;

                        try {
                            this.InitializeDataSourceHelper(_clientApplicationsObject.Database);
                        } catch {
                            return;
                        }

                        EventLogHelper.Instance.WriteEventLogEntry("IntelliServeServiceApi-Tickets", String.Format("Email to Tickets started: {0}", _dataSourceHelper.Database), null);

                        if (this.IsDatabaseSetup) {
                            this.CreateUpdateTicket(MailMessage, str);

                            EventLogHelper.Instance.WriteEventLogEntrySuccessAudit("IntelliServeServiceApi-Tickets", string.Format("Email Ticket created by {0} from {1}", MailMessage.From, _clientApplicationsObject.Database), _clientApplicationsObject.Database);

                            break;
                        } else {
                            EventLogHelper.Instance.WriteEventLogEntryWarning("IntelliServeServiceApi-Tickets", string.Format("Database setup incomplete - Email Ticket not created by {0} from {1}", MailMessage.From, _clientApplicationsObject.Database), _clientApplicationsObject.Database);
                        }
                    } else {
                        EventLogHelper.Instance.WriteEventLogEntryWarning("IntelliServeServiceApi-Tickets", "Email Address is not associated to a database - " + str, str);
                    }
                }
            } catch (Exception ex) {
                EventLogHelper.Instance.WriteEventLogException("IntelliServeServiceApi-Tickets", ex, _clientApplicationsObject.Database);
            }
        }

        private void InitializeDataSourceHelper(string Database) {
            this._dataSourceHelper = new DataSourceHelper(_connectionString, Database, DataLayerType.SimpleDataLayer, ObjectLayerType.SimpleObjectLayer, new string[3] { _objectLibrary, _objectLibraryBase, _objectLibraryReports });
        }

        public void Dispose() {
            if (this._dataSourceHelper != null) {
                this._dataSourceHelper = null;
                GC.Collect();
            }
        }
    }
}
