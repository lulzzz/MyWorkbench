using System;

namespace Ignyt.Framework {
    [AttributeUsage(AttributeTargets.Field)]
    public class ApplicationWebsite : Attribute {
        public string applicationWebsite;

        public ApplicationWebsite(string ApplicationWebsite) {
            this.applicationWebsite = ApplicationWebsite;
        }

        public override string ToString() {
            return applicationWebsite;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ApplicationUrl : Attribute {
        public string applicationUrl;

        public ApplicationUrl(string ApplicationUrl) {
            this.applicationUrl = ApplicationUrl;
        }

        public override string ToString() {
            return applicationUrl;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ApplicationID : Attribute {
        public string applicationID;

        public ApplicationID(string ApplicationID) {
            this.applicationID = ApplicationID;
        }

        public override string ToString() {
            return applicationID;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class SendGridApiKey : Attribute
    {
        public string sendGridApiKey;

        public SendGridApiKey(string SendGridApiKey)
        {
            this.sendGridApiKey = SendGridApiKey;
        }

        public override string ToString()
        {
            return sendGridApiKey;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class TwilioAccountSID : Attribute
    {
        public string twilioAccountSID;

        public TwilioAccountSID(string TwilioAccountSID)
        {
            this.twilioAccountSID = TwilioAccountSID;
        }

        public override string ToString()
        {
            return twilioAccountSID;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class TwilioAuthToken : Attribute
    {
        public string twilioAuthToken;

        public TwilioAuthToken(string TwilioAuthToken)
        {
            this.twilioAuthToken = TwilioAuthToken;
        }

        public override string ToString()
        {
            return twilioAuthToken;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class TwilioMessageService : Attribute
    {
        public string twilioMessageService;

        public TwilioMessageService(string TwilioMessageService)
        {
            this.twilioMessageService = TwilioMessageService;
        }

        public override string ToString()
        {
            return twilioMessageService;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class SendGridIPAddresses : Attribute
    {
        public string[] sendGridIPAddresses;

        public SendGridIPAddresses(string[] SendGridIPAddresses)
        {
            this.sendGridIPAddresses = SendGridIPAddresses;
        }

        public override string ToString()
        {
            return sendGridIPAddresses.ToString();
        }
    }

    public enum Application {
        [ApplicationUrl("https://web.myworkbench.co.za")]
        [ApplicationWebsite("https://www.myworkbench.co.za")]
        [ApplicationID("Your Application ID")]
        [SendGridApiKey("Your Api Key")]
        [SendGridIPAddresses(new string[] { })]
        [TwilioAccountSID("Your SID")]
        [TwilioAuthToken("Your Token")]
        [TwilioMessageService("Your Service")]
        MyWorkbench = 0,
        [ApplicationUrl("https://app.myworkbench.co.za")]
        [ApplicationWebsite("https://www.myworkbench.co.za")]
        [ApplicationID("Your Application ID")]
        [SendGridApiKey("Your Api Key")]
        [SendGridIPAddresses(new string[] { })]
        [TwilioAccountSID("Your SID")]
        [TwilioAuthToken("Your Token")]
        [TwilioMessageService("Your Service")]
        MyWorkBenchTest = 1,
        Hangfire = 2
    }
}
