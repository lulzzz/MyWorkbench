using System;
using System.Linq;

namespace Ignyt.Framework.Authentication {
    public class AuthenticationStateChangeAuthenticationObject : AuthenticationState {
        private string _database;

        public AuthenticationStateChangeAuthenticationObject(string Database) {
            _database = Database;
        }

        public override void Handle(AuthenticationContext context) {
            context.AutheticationObject = context.AutheticationObjects.Where(g => g.Database == _database).FirstOrDefault();
            context.AutenticationToken = Guid.Empty;

            if (context.AutheticationObject.Trial == true && context.AutheticationObject.DaysRemaining <= 0)
                context.AuthenticationState = new AuthenticationStateTrialExpired();
            else
                context.AuthenticationState = new AuthenticationStateSuccessful();

            context.Request();
        }
    }
}
