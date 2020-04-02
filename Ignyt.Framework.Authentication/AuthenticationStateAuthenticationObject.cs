using System.Linq;

namespace Ignyt.Framework.Authentication {
    public class AuthenticationStateAuthenticationObject : AuthenticationState {
        public override void Handle(AuthenticationContext context) {
            context.MultiTenant = context.AutheticationObjects.Count() > 1 ? true : false;
            context.AutheticationObject = context.AutheticationObjects.FirstOrDefault();

            if (context.AutheticationObject.Trial == true && context.AutheticationObject.DaysRemaining <= 0)
                context.AuthenticationState = new AuthenticationStateTrialExpired();
            else
                context.AuthenticationState = new AuthenticationStateSuccessful();

            context.Request();
        }
    }
}
