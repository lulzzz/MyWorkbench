using System;

namespace Ignyt.Framework.Authentication {
    public class AuthenticationStateSuccessful : AuthenticationState {
        public override void Handle(AuthenticationContext context) {
            context.ContextStatus = AuthenticationStateConstants.UserAuthenticationSuccessful;

            if (context.AutenticationToken == Guid.Empty) {
                context.AutenticationToken = Guid.NewGuid();
            }
        }
    }
}
