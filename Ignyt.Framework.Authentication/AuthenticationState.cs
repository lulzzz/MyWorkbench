namespace Ignyt.Framework.Authentication {
    public abstract class AuthenticationState {
        public abstract void Handle(AuthenticationContext context);
    }
}