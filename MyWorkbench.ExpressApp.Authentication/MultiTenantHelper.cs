using DevExpress.ExpressApp;
using Ignyt.Framework;
using Ignyt.Framework.Api;
using Ignyt.Framework.Authentication;
using System;
using System.Collections.Generic;

namespace MyWorkbench.ExpressApp.Authentication {
    public class MultiTenantHelper : SingletonBase<MultiTenantHelper> {
        private AuthenticationContext _authenticationContext;
        public AutheticationObject AutheticationObject {
            get {
                return this._authenticationContext.AutheticationObject;
            }
        }

        public void Authenticate(string EmailAddress, string Password) {
            _authenticationContext = new AuthenticationContext(CurrentApplication, new AuthenticationStateUserName(), EmailAddress, Password);
            _authenticationContext.Request();

            if (_authenticationContext.AutenticationToken == Guid.Empty) {
                throw new Exception(_authenticationContext.ContextStatus);
            }
        }

        public void CreateUser(IObjectSpace ObjectSpace) {
            if (AutheticationObject.CreateUser) {
                UserSecurityHelper.CreateAdminUser(ObjectSpace, AutheticationObject.FirstName, AutheticationObject.LastName, _authenticationContext.UserName, _authenticationContext.Password);
                _authenticationContext.AuthenticationState = new AuthenticationStateUserUpdate();
                _authenticationContext.Request();
            } else {
                UserSecurityHelper.UpdateUserPassword(ObjectSpace, _authenticationContext.UserName, _authenticationContext.Password);
            }
        }

        public void ChangeDatabase(string Database) {
            _authenticationContext.AuthenticationState = new AuthenticationStateChangeAuthenticationObject(Database);
            _authenticationContext.Request();

            if (_authenticationContext.AutenticationToken == Guid.Empty) {
                throw new Exception(_authenticationContext.ContextStatus);
            }
        }

        public string ConnectionString(string ConnectionString) {
            return this._authenticationContext.ConnectionString(ConnectionString);
        }

        public string AuthenticateConnection(string EmailAddress, string Password, string CurrentConnectionString) {
            this.Authenticate(EmailAddress, Password);
            return this.ConnectionString(CurrentConnectionString);
        }

        public IEnumerable<string> AuthenticateDatabases(string EmailAddress, string Password) {
            this.Authenticate(EmailAddress, Password);

            return this._authenticationContext.Databases;
        }
    }
}
