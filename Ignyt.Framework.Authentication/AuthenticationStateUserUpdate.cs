namespace Ignyt.Framework.Authentication {
    public class AuthenticationStateUserUpdate : AuthenticationState {
        public override void Handle(AuthenticationContext context) {
            try {
                if (context.AutheticationObject.CreateUser) {
                    context.DotNetNukeApi.CreateUserUpdate(context.UserName, context.Password, context.AutheticationObject.UserID.ToString(),
                        context.AutheticationObject.ClientApplicationID.ToString());

                    context.ContextStatus = AuthenticationStateConstants.UserUpdated;
                    context.AutheticationObject.CreateUser = false;

                    context.AuthenticationState = new AuthenticationStateSuccessful();
                    context.Request();
                }
            } catch {
                context.ContextStatus = AuthenticationStateConstants.UserUpdateFailed;
                context.AuthenticationState = new AuthenticationStateFailed();
                context.Request();
            }
        }
    }
}
