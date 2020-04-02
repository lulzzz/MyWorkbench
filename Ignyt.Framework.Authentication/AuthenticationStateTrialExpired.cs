using System;

namespace Ignyt.Framework.Authentication {
    public class AuthenticationStateTrialExpired : AuthenticationState {
        public override void Handle(AuthenticationContext context) {
            context.ContextStatus = string.Concat(context.UserName," - ",AuthenticationStateConstants.TrialExpired);
            throw new Exception(context.ContextStatus);
        }
    }
}
