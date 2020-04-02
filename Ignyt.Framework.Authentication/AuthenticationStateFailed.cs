using System;

namespace Ignyt.Framework.Authentication {
    public class AuthenticationStateFailed : AuthenticationState {
        public override void Handle(AuthenticationContext context) {
            throw new Exception(context.ContextStatus);
        }
    }
}
