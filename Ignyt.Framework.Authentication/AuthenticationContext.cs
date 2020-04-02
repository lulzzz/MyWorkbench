using Ignyt.Framework.Api;
using Ignyt.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Ignyt.Framework.Authentication {
    public class AuthenticationContext {
        private AuthenticationState authenticationState;
        private string contextStatus;
        private Guid autenticationToken;
        private AutheticationObject autheticationObject = new AutheticationObject() { Database = "UNKNOWN" };
        private Ignyt.Framework.Application authenticationApplication;
        private Guid applicationID;
        private string userName;
        private string password;
        private string storedPassword;
        private IEnumerable<AutheticationObject> autheticationObjects = new List<AutheticationObject>();
        private bool multiTenant;
        private DotNetNukeApi dotNetNukeApi;

        public AuthenticationContext(Ignyt.Framework.Application Application, AuthenticationState AuthenticationState, string UserName, string Password) {
            this.AuthenticationState = AuthenticationState;
            this.AuthenticationApplication = Application;
            this.DotNetNukeApi = new DotNetNukeApi(AuthenticationApplication);
            this.ApplicationID = Guid.Parse(Application.GetAttribute<ApplicationID>().ToString());
            this.UserName = UserName;
            this.Password = Password;
        }

        public AuthenticationState AuthenticationState {
            get { return authenticationState; }
            set { authenticationState = value; }
        }

        public Guid AutenticationToken {
            get { return autenticationToken; }
            set { autenticationToken = value; }
        }

        public string ContextStatus {
            get { return contextStatus; }
            set { contextStatus = value; }
        }

        public string ContextStatusAndDatabase {
            get { return String.Format("{0} - {1}", this.contextStatus, this.AutheticationObject.Database); }
        }

        public AutheticationObject AutheticationObject {
            get { return autheticationObject; }
            set { autheticationObject = value; }
        }

        public IEnumerable<AutheticationObject> AutheticationObjects {
            get { return autheticationObjects; }
            set { autheticationObjects = value; }
        }

        public Application AuthenticationApplication {
            get { return authenticationApplication; }
            set { authenticationApplication = value; }
        }

        public DotNetNukeApi DotNetNukeApi {
            get { return dotNetNukeApi; }
            set { dotNetNukeApi = value; }
        }

        public Guid ApplicationID {
            get { return applicationID; }
            set { applicationID = value; }
        }

        public string UserName {
            get { return userName; }
            set { userName = value; }
        }

        public string Password {
            get { return password; }
            set { password = value; }
        }

        public string StoredPassword {
            get { return storedPassword; }
            set { storedPassword = value; }
        }

        public bool MultiTenant {
            get { return multiTenant; }
            set { multiTenant = value; }
        }

        public IEnumerable<string> Databases {
            get {
                List<string> databases = new List<string>();

                foreach (AutheticationObject obj in this.AutheticationObjects) {
                    databases.Add(obj.Database);
                }

                return databases;
            }
        }

        public string DatabasesJSON {
            get {
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                return serializer.Serialize(this.AutheticationObjects);
            }
        }

        public string ConnectionString(string ConnectionString) {
            SqlConnectionStringBuilder connectionStringParser;

            connectionStringParser = new SqlConnectionStringBuilder(ConnectionString) {
                InitialCatalog = this.AutheticationObject.Database
            };
            return connectionStringParser.ConnectionString;
        }

        public void Request() {
            authenticationState.Handle(this);
        }
    }
}
