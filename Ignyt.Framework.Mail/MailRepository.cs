using System;
using System.Collections.Generic;
using System.Linq;
using ActiveUp.Net.Mail;

namespace Ignyt.Framework.Mail {
    public class MailRepository : IDisposable {
        private Imap4Client fclient = null;
        private Imap4Client Client {
            get {
                if (fclient == null)
                    fclient = new Imap4Client();
                return fclient;
            }
        }

        public MailRepository(string mailServer, int port, bool ssl, string login, string password) {
            if (ssl)
                Client.ConnectSsl(mailServer, port);
            else
                Client.Connect(mailServer, port);
            Client.Login(login, password);
        }

        public IEnumerable<Message> GetAllMails(string MailBox)
        {
            return GetMails(MailBox, "ALL").Cast<Message>();
        }

        public IEnumerable<Message> GetAllMails(string MailBox, string ToAddress) {
            return GetMails(MailBox, "ALL TO \"" + ToAddress + "\"").Cast<Message>();
        }

        public IEnumerable<Message> GetUnreadMails(string MailBox, string ToAddress) {
            return GetMails(MailBox, "UNSEEN TO \"" + ToAddress + "\"").Cast<Message>();
        }

        public IEnumerable<Message> GetUnreadMails(string MailBox)
        {
            return GetMails(MailBox, "UNSEEN").Cast<Message>();
        }

        public void MoveEmailRead(string SourceMailBox, string DestinationMailBox, string MessageId)
        {
            Mailbox mails = Client.SelectMailbox(SourceMailBox);
            int[] ids = mails.Search("ALL");

            if (ids.Length > 0)
            {
                for (var i = 0; i < ids.Length; i++)
                {
                    if (MessageId.Contains(Convert.ToString(ids[i])))
                    {
                        Client.Command("copy " + ids[i].ToString() + DestinationMailBox);  //copy emails

                        FlagCollection flags = new FlagCollection(); //delete emails
                        flags.Add("Deleted");
                        mails.AddFlags(ids[i], flags);
                    }
                }
            }
        }

        private MessageCollection GetMails(string mailBox, string searchPhrase) {
            Mailbox mails = Client.SelectMailbox(mailBox);
            MessageCollection messages = mails.SearchParse(searchPhrase);
            return messages;
        }

        public void Dispose() {
            if (this.fclient != null) {
                this.fclient.Disconnect();
                this.fclient = null;
            }
        }
    }
}
