using Ignyt.Framework;
using Ignyt.Framework.Api;
using MyWorkbench.BusinessObjects.Lookups;
using System.Collections.Generic;
using System.Net;

namespace MyWorkbench.BusinessObjects.Helpers.Notifications
{
    public class MultiTenantHelper : SingletonBase<MultiTenantHelper>
    {
        private const string _userCreated = "User {0} Successfuly created. \n Username is {1} \n Generated password is {2}";
        private const string _userNotCreated = "User unSuccessfuly created - {0}";
        private const string _userConflict = "User {0} unSuccessfuly created. \n Username {1} already exists globally. Please select a different email address";
        private const string _userDeleted = "User Successfuly deleted";
        private const string _userNotDeleted = "User Successfuly deleted - {0}";
        private const string _userUpdated = "User Successfuly updated";
        private const string _userNotUpdated = "User unSuccessfuly updated - {0}";

        private string Database { get; set; }
        private DotNetNukeApi DotNetNukeApi { get; set; }

        public MultiTenantHelper(Application Application, string CurrentDatabase)
        {
            this.Database = CurrentDatabase;
            this.DotNetNukeApi = new DotNetNukeApi(Application);
        }

        private KeyValuePair<HttpStatusCode, string> ChangePassword(string AuthenticatingUserName, string AuthenticatingPassword, string UserName, string OldPassword, string NewPassword)
        {
            return DotNetNukeApi.ChangeUserPassword(AuthenticatingUserName, AuthenticatingPassword, UserName, OldPassword, NewPassword);
        }

        public KeyValuePair<bool, string> DeleteUser(Employee Employee)
        {
            KeyValuePair<HttpStatusCode, string> result = this.DotNetNukeApi.DeleteUser(Employee.Email, this.Database);

            if (result.Key == HttpStatusCode.OK)
            {
                return new KeyValuePair<bool, string>(true, _userDeleted);
            }
            else return new KeyValuePair<bool, string>(false, string.Format(_userNotDeleted, result.Value));
        }

        public KeyValuePair<bool, string> UpdateUser(Employee Employee)
        {
            KeyValuePair<HttpStatusCode, string> result = this.DotNetNukeApi.UpdateUser(Employee.UserName, TextEncryption.Decypt(Employee.StoredPassword), Employee.Email, Employee.FirstName, Employee.LastName, this.Database);

            if (result.Key != HttpStatusCode.OK)
            {
                return new KeyValuePair<bool, string>(false, string.Format(_userNotUpdated, result.Value));
            }
            else
            {
                return new KeyValuePair<bool, string>(false, string.Format(_userUpdated, result.Value));
            }
        }

        public KeyValuePair<HttpStatusCode, string> SetPassword(Employee AuthenticatingEmployee, Employee UpdatingEmployee, string NewPassword)
        {
            return this.ChangePassword(AuthenticatingEmployee.UserName, AuthenticatingEmployee.DecryptedStoredPassword, UpdatingEmployee.UserName, UpdatingEmployee.DecryptedStoredPassword, NewPassword);
        }

        public KeyValuePair<bool, string> CreateUser(Employee Employee)
        {
            KeyValuePair<HttpStatusCode, string> result = this.DotNetNukeApi.CreateUser(Employee.UserName, Employee.RandomPassword, Employee.Email, Employee.FirstName, Employee.LastName, this.Database);

            if (result.Key == HttpStatusCode.OK)
            {
                Employee.SetPassword(Employee.RandomPassword);

                KeyValuePair<bool, string> createdResult = new KeyValuePair<bool, string>(true, string.Format(_userCreated, Employee.FullName, Employee.UserName, Employee.RandomPassword));

                return createdResult;
            }
            else if (result.Key == HttpStatusCode.Conflict)
            {
                return new KeyValuePair<bool, string>(false, string.Format(_userConflict, Employee.FullName, Employee.UserName));
            }
            else return new KeyValuePair<bool, string>(false, string.Format(_userNotCreated, result.Value));
        }
    }
}
