using Xero.Api.Core;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Infrastructure.RateLimiter;
using Xero.Api.Serialization;

namespace Ignyt.Webfunctions.Xero.Authentication.Partner
{
    public class Core : XeroCoreApi
    {
        private static readonly DefaultMapper Mapper = new DefaultMapper();

        public Core(ITokenStore store, IUser user, bool includeRateLimiter = false) :
            base(Constants.BaseUri,
                new PartnerAuthenticator(
                    Constants.BaseUri,
                    Constants.BaseUri,
                    Constants.CallBackUri,
                    store,
                    Constants.SigningCertificatePath,
                    Constants.SigningCertificatePassword),
                new Consumer(
                    Constants.Key,
                    Constants.Secret),
                user,
                Mapper,
                Mapper,
                includeRateLimiter ? new RateLimiter() : null)
        {
        }
    }
}