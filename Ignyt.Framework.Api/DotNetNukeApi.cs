using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Ignyt.Framework.Api {
    public class DotNetNukeApi {
        private readonly string _authenticationUrl = "/DesktopModules/IgnytServices/API/IgnytAutentication/";
        private RestHelper<AutheticationObject> _authentication = null;
        private RestHelper<ClientApplicationsObject> _clientApplicationsObject = null;
        private RestHelper<ApplicationCredentialObject> _applicationCredentialObject = null;
        private IEnumerable<AutheticationObject> _autheticationObjects;
        private RestHelper _helpers = null;

        public DotNetNukeApi(Application Application) {
            this._authenticationUrl = string.Format("{0}{1}", Application.GetAttribute<ApplicationWebsite>().ToString(), this._authenticationUrl);
        }

        public IEnumerable<AutheticationObject> AutheticationObjects {
            get {
                return _autheticationObjects;
            }
        }

        public IEnumerable<AutheticationObject> WebAuthentication(Guid ApplicationID, string UserName, string Password) {
            _autheticationObjects = DNNUserAuthenticationV2(ApplicationID, UserName, Password);

            if (_autheticationObjects == null || _autheticationObjects.Count() == 0) {
                throw new Exception("You have entered an incorrect username and password combination. If you are having difficulty, please visit www.myworkbench.co.za for more assistance or contact our support center at info@ignytsolutions.com");
            }

            return _autheticationObjects;
        }

        private AutheticationObject DNNUserAuthentication(Guid ApplicationID, string UserName, string Password) {
            try {
                using (_authentication = new RestHelper<AutheticationObject>()) {
                    return _authentication.Get(string.Format("{0}{1}{2}{3}", _authenticationUrl, string.Concat("ApplicationAuthetication?ApplicationUniqueID=", ApplicationID), string.Concat("&UserName=", UserName), string.Concat("&Password=", Password)));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        private IEnumerable<AutheticationObject> DNNUserAuthenticationV2(Guid ApplicationID, string UserName, string Password) {
            try {
                using (_authentication = new RestHelper<AutheticationObject>()) {
                    string querystring = string.Format("{0}{1}{2}{3}", _authenticationUrl, string.Concat("ApplicationAutheticationV2?ApplicationUniqueID=", ApplicationID), string.Concat("&UserName=", UserName), string.Concat("&Password=", Password));
                    return _authentication.GetList(querystring);
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public ApplicationCredentialObject DNNApplicationCredentials(Guid ApplicationID, string UserName, string Password, string Addtional) {
            try {
                using (_applicationCredentialObject = new RestHelper<ApplicationCredentialObject>()) {
                    return _applicationCredentialObject.Get(string.Format("{0}{1}{2}{3}{4}", _authenticationUrl, string.Concat("GetApplicationCredentials?ApplicationUniqueID=", ApplicationID), string.Concat("&UserName=", UserName), string.Concat("&Password=", Password), string.Concat("&Addtional=", Addtional)));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public ApplicationCredentialObject DNNApplicationCredentials(Guid ApplicationID) {
            try {
                using (_applicationCredentialObject = new RestHelper<ApplicationCredentialObject>()) {
                    return _applicationCredentialObject.Get(string.Format("{0}{1}", _authenticationUrl, string.Concat("GetApplicationCredentialsNotAuthentication?ApplicationUniqueID=", ApplicationID)));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public string ClientSMSInsert(Guid ApplicationUniqueID, string UserName, string Password, string Number, string MessageText, string ResultString) {
            try {
                using (_helpers = new RestHelper()) {
                    return _helpers.Get(string.Format("{0}{1}{2}{3}{4}{5}{6}", _authenticationUrl, string.Concat("InsertSMSRecord?ApplicationUniqueID=", ApplicationUniqueID), string.Concat("&UserName=", UserName), string.Concat("&Password=", Password), string.Concat("&Number=", Number), string.Concat("&MessageText=", MessageText), string.Concat("&ResultString=", ResultString)));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public string ClientSMSInsert(Guid ApplicationUniqueID, string Number, string MessageText, string ResultString) {
            try {
                using (_helpers = new RestHelper()) {
                    return _helpers.Get(string.Format("{0}{1}{2}{3}{4}{5}", _authenticationUrl, string.Concat("InsertSMSRecordApplicationUniqueID?ApplicationUniqueID=", ApplicationUniqueID), string.Concat("&Number=", Number), string.Concat("&MessageText=", MessageText), string.Concat("&ResultString=", ResultString), string.Concat("&Addtional=", string.Empty)));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public string CreateUserUpdate(string UserName, string Password, string UserID, string ClientApplicationID) {
            try {
                using (_helpers = new RestHelper()) {
                    return _helpers.Get(string.Format("{0}{1}{2}{3}{4}", _authenticationUrl, string.Concat("CreateUserUpdate?UserName=", UserName), string.Concat("&Password=", Password), string.Concat("&ClientApplicationID=", ClientApplicationID), string.Concat("&UserID=", UserID)));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public KeyValuePair<HttpStatusCode, string> CreateUser(string UserName, string Password, string EmailAdress, string FirstName, string LastName, string Database) {
            try {
                using (_helpers = new RestHelper()) {
                    return _helpers.GetStatus(string.Format("{0}{1}{2}{3}{4}{5}{6}", _authenticationUrl, string.Concat("CreateUser?UserName=", UserName), string.Concat("&Password=", Password), string.Concat("&EmailAdress=", EmailAdress), string.Concat("&FirstName=", FirstName), string.Concat("&LastName=", LastName), string.Concat("&Database=", Database)));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public KeyValuePair<HttpStatusCode, string> DeleteUser(string EmailAddress, string Database) {
            try {
                using (_helpers = new RestHelper()) {
                    return _helpers.GetStatus(string.Format("{0}{1}{2}", _authenticationUrl, string.Concat("DeleteUser?EmailAddress=", EmailAddress), string.Concat("&Database=", Database)));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public KeyValuePair<HttpStatusCode, string> UpdateUser(string UserName, string Password, string EmailAdress, string FirstName, string LastName, string Database) {
            try {
                using (_helpers = new RestHelper()) {
                    return _helpers.GetStatus(string.Format("{0}{1}{2}{3}{4}{5}{6}", _authenticationUrl, string.Concat("UpdateUser?UserName=", UserName), string.Concat("&Password=", Password), string.Concat("&EmailAdress=", EmailAdress), string.Concat("&FirstName=", FirstName), string.Concat("&LastName=", LastName), string.Concat("&Database=", Database)));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public KeyValuePair<HttpStatusCode, string> ChangeUserPassword(string AuthenticatingUserName, string AuthenticatingPassword, string UserName, string OldPassword, string NewPassword) {
            try {
                using (_helpers = new RestHelper()) {
                    return _helpers.GetStatus(string.Format("{0}{1}{2}{3}{4}{5}", _authenticationUrl, string.Concat("ChangeUserPassword?AuthenticatingUserName=", AuthenticatingUserName), string.Concat("&AuthenticatingPassword=", AuthenticatingPassword), string.Concat("&UserName=", UserName), string.Concat("&OldPassword=", OldPassword), string.Concat("&NewPassword=", NewPassword)));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public KeyValuePair<HttpStatusCode, string> ResetUserPassword(string UserName, string Password) {
            try {
                using (_helpers = new RestHelper()) {
                    return _helpers.GetStatus(string.Format("{0}{1}{2}", _authenticationUrl, string.Concat("ResetUserPassword?UserName=", UserName), string.Concat("&Password=", Password)));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public List<ClientApplicationsObject> ClientApplications(Guid ApplicationUniqueID) {
            try {
                using (_clientApplicationsObject = new RestHelper<ClientApplicationsObject>()) {
                    return _clientApplicationsObject.GetList(string.Format("{0}{1}", _authenticationUrl, string.Concat("ClientApplications?ApplicationUniqueID=", ApplicationUniqueID)));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public string InsertEventLogError(string UserName, string Password, string Description) {
            try {
                using (_helpers = new RestHelper()) {
                    return _helpers.Get(string.Format("{0}{1}{2}{3}", _authenticationUrl, string.Concat("InsertEventLogError?UserName=", UserName), string.Concat("&Password=", Password), string.Concat("&Description=", Description)));
                }
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
