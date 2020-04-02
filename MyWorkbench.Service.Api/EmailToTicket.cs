using DevExpress.Data.Filtering;
using Ignyt.Framework.Mail;
using MyWorkbench.BusinessObjects;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyWorkBench.Service.Api
{
    public class EmailToTicket : ApiBase, IDisposable
    {
        #region Fields
        private readonly string _mailServer;
        private readonly int _port;
        private readonly bool _ssl;
        private readonly string _login;
        private readonly string _password;
        #endregion

        #region Properties
        private List<MailMessage> _mailMessages;
        private List<MailMessage> MailMessages {
            get {
                if (_mailMessages == null)
                    _mailMessages = this.QueryMail.GetUnreadMail();
                return _mailMessages;
            }
        }
        
        private QueryMail _queryMail;
        private QueryMail QueryMail {
            get {
                if (_queryMail == null)
                    _queryMail = new QueryMail(_mailServer, _port, _ssl, _login, _password);
                return _queryMail;
            }
        }
        
        private Settings Settings {
            get {
                Settings settings = this.DataSourceHelper.Session.FindObject<Settings>(null);

                return settings;
            }
        }
        #endregion

        #region Constructor
        public EmailToTicket(string ConnectionString, string MailServer, int Port, bool Ssl, string Login, string Password) : base(ConnectionString)
        {
            this._mailServer = MailServer;
            this._port = Port;
            this._ssl = Ssl;
            this._login = Login;
            this._password = Password;
        }
        #endregion

        #region Overrides
        protected override void ProcessClient()
        {
            try
            {
                base.ProcessClient();

                if(Settings.EmailAccountName != null)
                    this.ProcessEmails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Methods
        private VendorContact VendorContact(string EmailAddress, string FirstName, string LastName)
        {
            VendorContact vendorContact = this.DataSourceHelper.Session.FindObject<VendorContact>(CriteriaOperator.Parse(string.Format("Email = '{0}'", EmailAddress)));

            if (vendorContact == null)
            {
                vendorContact = new VendorContact(this.DataSourceHelper.Session) { FirstName = FirstName, LastName = LastName, Email = EmailAddress };
                vendorContact.Save();
            }

            return vendorContact;
        }

        private void ProcessEmails()
        {
            try
            {
                if (this.MailMessages.Count >= 1)
                    this.MailMessages.ForEach(ProcessEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessEmail(MailMessage MailMessage)
        {
            try
            {
                if(MailMessage.ToEmailAddress.Contains(Settings.EmailAccountName))
                    this.CreateUpdateTicket(MailMessage, MailMessage.OriginalFromEmail, MailMessage.OriginalFromFirstName, MailMessage.OriginalFromLastName);
            }
            catch (Exception ex)
            {
                this.TaskExceptionList.TaskExceptions.Add(ex);
            }
        }
        
        private void CreateUpdateTicket(MailMessage MailMessage, string FromEmail, string FromFirstName, string FromLastName)
        {
            Ticket ticket = GetTicketFromSubject(MailMessage.Subject ?? "No Subject");
            
            if (ticket != null)
            {
                WorkFlowNote workFlowNote = new WorkFlowNote(this.DataSourceHelper.Session) { Description = GetNewCommentFromBody(MailMessage.Body), CreatedBy = string.Concat(FromFirstName, " ", FromLastName) };
                ticket.Notes.Add(workFlowNote);

                foreach (MailMessageAttachment attachment in MailMessage.Attachments)
                {
                    DevExpress.Persistent.BaseImpl.FileData fileData = new DevExpress.Persistent.BaseImpl.FileData(this.DataSourceHelper.Session);
                    fileData.LoadFromStream(attachment.Name, new System.IO.MemoryStream(attachment.Content));

                    ticket.Attachments.Add(new WorkFlowAttachment(this.DataSourceHelper.Session) { Description = attachment.Name, Attachment = fileData });
                }

                ticket.Save();
            }
            else
            {
                ticket = new Ticket(this.DataSourceHelper.Session) { VendorContact = this.VendorContact(FromEmail, FromFirstName, FromLastName), Subject = MailMessage.Subject, Description = MailMessage.Body };
                ticket.Notes.Add(new WorkFlowNote(this.DataSourceHelper.Session) { Description = string.Format("Email to Ticket created by {0}", MailMessage.From), CreatedBy = string.Concat(FromFirstName, " ", FromLastName) });

                foreach (MailMessageAttachment attachment in MailMessage.Attachments)
                {
                    DevExpress.Persistent.BaseImpl.FileData fileData = new DevExpress.Persistent.BaseImpl.FileData(this.DataSourceHelper.Session);
                    fileData.LoadFromStream(attachment.Name, new MemoryStream(attachment.Content));

                    ticket.Attachments.Add(new WorkFlowAttachment(this.DataSourceHelper.Session) { Description = attachment.Name, Attachment = fileData });
                }

                ticket.Save();
            }
        }

        private Ticket GetTicketFromSubject(string subject)
        {
            int pos1 = subject.IndexOf("##");
            int pos2 = subject.LastIndexOf("##");

            if (pos1 != 0 && pos2 != 0)
            {
                try
                {
                    string ticketNumber = subject.Substring(pos1 + 2, pos2 - pos1 - 2);
                    return this.DataSourceHelper.Session.FindObject<Ticket>(new BinaryOperator("No", ticketNumber));
                }
                catch (Exception)
                {
                    return null;
                }

            }
            else
                return null;
        }

        private string GetNewCommentFromBody(string body)
        {
            int pos = body.IndexOf("## Please reply");

            if (pos != 0)
            {
                try
                {
                    return body.Substring(0, pos - 1);
                }
                catch (Exception)
                {
                    return body;
                }

            }
            else return body;
        }
        #endregion
    }
}
