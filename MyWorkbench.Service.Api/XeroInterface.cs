using MyWorkBench.Service.Api;
using System;
using Xero.Api.Core;
using Xero.Api.Example.Applications.Public;
using Xero.Api.Example.TokenStores;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Serialization;

namespace MyWorkbench.Service.Api
{
    public class XeroInterface : ApiBase
    {
        private const string _baseUri = "https://api.xero.com";
        private const string _callbackUrl = "https://web.myworkbench.co.za/Xero/Authorize";
        private const string _consumerKey= "VLOB3VHOQGUMTLRF6ZJ6GIT3YUBV3S";
        private const string _consumerSecret = "JJVAVTCOBYDUDZG3WDVQHN9Y6C47DA";

        private XeroCoreApi _xeroCoreApi = null;
        private XeroCoreApi XeroCoreApi {
            get {
                if (_xeroCoreApi == null)
                    _xeroCoreApi = new XeroCoreApi(_baseUri, new PublicAuthenticator(_baseUri, _baseUri, _callbackUrl, new MemoryTokenStore()),
                        new Consumer(_consumerKey, _consumerSecret), new ApiUser { Name = Environment.MachineName },
                        new DefaultMapper(), new DefaultMapper());
                return _xeroCoreApi;
            }
        }

        public XeroInterface(string ConnectionString) : base(ConnectionString) { }

        protected override void ProcessClient()
        {
            try
            {
                base.ProcessClient();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UploadInvoices()
        {

        }
    }
}
