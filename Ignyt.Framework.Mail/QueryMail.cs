using System.Collections.Generic;
using ActiveUp.Net.Mail;

namespace Ignyt.Framework.Mail
{
    public class QueryMail
    {
        private string _mailServer;
        private int _port;
        private bool _ssl;
        private string _login;
        private string _password;

        public QueryMail(string MailServer, int Port, bool Ssl, string Login, string Password)
        {
            this._mailServer = MailServer;
            this._port = Port;
            this._ssl = Ssl;
            this._login = Login;
            this._password = Password;
        }

        public List<MailMessage> GetUnreadMail()
        {
            List<MailMessage> messages = new List<MailMessage>();

            using (MailRepository rep = new MailRepository(_mailServer, _port, _ssl, _login, _password))
            {
                IEnumerable<Message> unreadMessages = rep.GetUnreadMails("Inbox");

                foreach (Message email in unreadMessages)
                {
                    MailMessage message = new MailMessage() { Subject = email.Subject, Body = email.BodyText.TextStripped, OriginalBody = email.BodyText.Text, From = email.From.Name, FromEmail = email.From.Email, ToEmailAddress = new string[email.To.Count], EnvelopeTo = email.HeaderFields.GetValues("envelope-to"), MessageId = email.MessageId };

                    for (int i = 0; i < email.To.Count; i++)
                    {
                        message.ToEmailAddress[i] = email.To[i].Email;
                    }

                    foreach (MimePart attachment in email.Attachments)
                    {
                        message.Attachments.Add(new MailMessageAttachment() { Name = attachment.Filename, Content = attachment.BinaryContent });
                    }

                    messages.Add(message);
                }

                return messages;
            }
        }

        public void MoveEmailRead(string SourceMailBox, string DestinationMailBox, string MessageId)
        {
            using (MailRepository rep = new MailRepository(_mailServer, _port, _ssl, _login, _password))
            {
                rep.MoveEmailRead(SourceMailBox, DestinationMailBox, MessageId);
            }
        }
    }
}
