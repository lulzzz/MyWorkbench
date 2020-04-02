namespace Ignyt.Framework.Authentication {
    public class AuthenticationStateUserName : AuthenticationState {
        public override void Handle(AuthenticationContext context) {
            if (context.UserName == string.Empty) {
                context.ContextStatus = AuthenticationStateConstants.UserNameEmpty;
                context.AuthenticationState = new AuthenticationStateFailed();
                context.Request();
            } else if (context.Password == string.Empty) {
                context.ContextStatus = AuthenticationStateConstants.PasswordEmpty;
                context.AuthenticationState = new AuthenticationStateFailed();
                context.Request();
            } else {
                context.AuthenticationState = new AuthenticationStateAuthenticated();
                context.Request();
            }
        }
    }
}
