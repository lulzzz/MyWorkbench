using Ignyt.Webfunctions.Xero.Authentication;
using Ignyt.Webfunctions.Xero.Authentication.Public;
using Ignyt.Webfunctions.Xero.Authentication.TokenStore;
using System;
using Xero.Api.Core;
using Xero.Api.Core.Model;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Serialization;

namespace Ignyt.Webfunctions
{
    public class ApplicationSettings
    {
        public string BaseApiUrl { get; set; }
        public Consumer Consumer { get; set; }
        public object Authenticator { get; set; }
    }

    public static class XeroFunctions
    {
        private static ApplicationSettings _applicationSettings;

        static XeroFunctions()
        {
            var memoryStore = new MemoryAccessTokenStore();
            var requestTokenStore = new MemoryRequestTokenStore();

            var publicConsumer = new Consumer(Constants.Key, Constants.Secret);

            var publicAuthenticator = new PublicMvcAuthenticator(Constants.BaseUri, Constants.BaseUri, Constants.CallBackUri, memoryStore,
                publicConsumer, requestTokenStore);

            var publicApplicationSettings = new ApplicationSettings
            {
                BaseApiUrl = Constants.BaseUri,
                Consumer = publicConsumer,
                Authenticator = publicAuthenticator
            };

            _applicationSettings = publicApplicationSettings;
        }

        public static ApiUser User()
        {
            return new ApiUser { Name = Environment.MachineName };
        }

        public static IConsumer Consumer()
        {
            return _applicationSettings.Consumer;
        }

        public static IMvcAuthenticator MvcAuthenticator()
        {
            return (IMvcAuthenticator)_applicationSettings.Authenticator;
        }

        public static IXeroCoreApi CoreApi()
        {
            if (_applicationSettings.Authenticator is IAuthenticator)
            {
                return new XeroCoreApi(_applicationSettings.BaseApiUrl, _applicationSettings.Authenticator as IAuthenticator,
                    _applicationSettings.Consumer, User(), new DefaultMapper(), new DefaultMapper());
            }

            return null;
        }
    }
}