using Ignyt.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Ignyt.Webfunctions
{
    public class TwilioResponse
    {
        public string Sid { get; set; }
        public int? ErrorCode { get; set; }
        public string Message { get; set; }
    }

    public class TwilioFunctions
    {
        private readonly string _accountSID;
        private readonly string _accountAuthToken;
        private readonly string _messageService;

        public TwilioFunctions(Application Application, string Database)
        {
            this.Application = Application;
            this.Database = Database;
            this._accountSID = Application.GetAttribute<TwilioAccountSID>().ToString();
            this._accountAuthToken = Application.GetAttribute<TwilioAuthToken>().ToString();
            this._messageService = Application.GetAttribute<TwilioMessageService>().ToString();
        }

        public Application Application { get; set; }
        public string Database { get; set; }

        public IEnumerable<TwilioResponse> InitiateASMS(IEnumerable<KeyValuePair<string,string>> Messages)
        {
            try
            {
                List<TwilioResponse> responses = new List<TwilioResponse>();

                foreach (KeyValuePair<string, string> message in Messages)
                {
                    responses.Add(InitiateASMS(message));
                }

                return responses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TwilioResponse InitiateASMS(KeyValuePair<string, string> Message)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                TwilioClient.Init(_accountSID, _accountAuthToken);

                var message = MessageResource.Create(
                    new PhoneNumber(Message.Key),
                    from: new PhoneNumber(_messageService),
                    body: Message.Value
                );

                return new TwilioResponse() { Sid = message.Sid, ErrorCode = message.ErrorCode, Message = message.ErrorMessage };
            }
            catch (Exception ex)
            {
                return new TwilioResponse() { Sid = null, ErrorCode = null, Message = ex.Message };
            }
        }
    }
}
