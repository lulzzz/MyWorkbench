namespace Ignyt.Framework.Authentication {
    public class AuthenticationStateAuthenticated : AuthenticationState {
        public override void Handle(AuthenticationContext context) {
            context.AuthenticationState = new AuthenticationStateExternal();
            context.Request();
        }
    }
}
