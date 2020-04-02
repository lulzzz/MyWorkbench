using System;
using System.Collections.Generic;

namespace Ignyt.Framework.Mail {
    public class MailMessage {
        public string[] ToEmailAddress { get; set; }
        public string From { get; set; }
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string OriginalBody { get; set; }
        public string[] EnvelopeTo { get; set; }
        public string FromFirstName
        {
            get
            {
                return From.Split(' ')[0];
            }            
        }
        public string FromLastName
        {
            get
            {
                string fromLastName = "";
                try 
	            {   
                    fromLastName = From.Substring(From.IndexOf(" "));
                }
                catch (Exception)
                {
                    return "";                   
                }
                return fromLastName;
            }
        }
        public string OriginalFromFirstName
        {
            get
            {
                return GetOriginalFrom(0);
            }           
        }
        public string OriginalFromLastName
        {
            get
            {
                return GetOriginalFrom(1);
            }           
        }
        public string OriginalFromEmail
        {
            get
            {
                return GetOriginalFrom(2);
            }            
        }
        public string MessageId { get; set; }
        public List<MailMessageAttachment> Attachments { get; set; }
        public MailMessage() {
            this.Attachments = new List<MailMessageAttachment>();
        }       
        private string GetOriginalFrom(int ReturnQuery)
        {
            if (Subject.Contains("Fwd:") && OriginalBody.Contains("From:"))
            {
                int fromPos = OriginalBody.IndexOf("From:") + 6;
                int lessThanPos = OriginalBody.IndexOf("<", fromPos);                
                int greaterThanPos = OriginalBody.IndexOf(">", lessThanPos);                
                string from = OriginalBody.Substring(fromPos, lessThanPos - fromPos -1);
                string fromEmail = OriginalBody.Substring(lessThanPos + 1, greaterThanPos - lessThanPos -1);
                string fromFirstName = "";
                string fromLastName = "";

                from = from.Trim();

                int count = 0;
                foreach (string item in from.Split(' '))
                {
                    if (count == 0)
                        fromFirstName = item;
                    else
                        fromLastName = fromLastName + item;
                    count++;
                }

                if (ReturnQuery == 0)
                    return fromFirstName.Trim();
                else if (ReturnQuery == 1)
                    return fromLastName.Trim();
                else
                    return fromEmail.Trim();
            }
            else
            {
                if (ReturnQuery == 0)
                    return FromFirstName.Trim();
                else if (ReturnQuery == 1)
                    return FromLastName.Trim();
                else
                    return FromEmail.Trim();
            }
        }

    }
}
