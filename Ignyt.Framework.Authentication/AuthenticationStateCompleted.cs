namespace Ignyt.Framework.Authentication {
    public class AuthenticationStateCompleted : AuthenticationState {
        public override void Handle(AuthenticationContext context) {
            context.ContextStatus = AuthenticationStateConstants.UserAuthenticationSuccessful;
        }
    }
}
