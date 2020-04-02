using System;

namespace Ignyt.Framework.Authentication {
    public class AuthenticationStateExternal : AuthenticationState {
        public override void Handle(AuthenticationContext context) {
            try {
                context.AutheticationObjects = context.DotNetNukeApi.WebAuthentication(context.ApplicationID, context.UserName, context.Password);
            } catch (Exception ex) {
                context.ContextStatus = String.Format("{0} - {1} ({2})", context.UserName, AuthenticationStateConstants.UserAuthenticationFailed, ex.Message);
                context.AuthenticationState = new AuthenticationStateFailed();
                context.Request();
            }

            context.AuthenticationState = new AuthenticationStateAuthenticationObject();
            context.Request();
        }
    }
}
