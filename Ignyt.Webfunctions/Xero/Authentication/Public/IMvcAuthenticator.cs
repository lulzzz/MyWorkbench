using Xero.Api.Infrastructure.Interfaces;

namespace Ignyt.Webfunctions.Xero.Authentication.Public
{
    public interface IMvcAuthenticator
    {
        string GetRequestTokenAuthorizeUrl(string userId);
        bool GetRequestToken(string userId);
        void DeleteRequestToken(string userId);
        IToken RetrieveAndStoreAccessToken(string userId, string tokenKey, string verfier, string organisationShortCode);
    }
}