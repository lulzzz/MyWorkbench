using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Utils;
using System;
using MyWorkbench.BusinessObjects.Helpers.Notifications;
using MyWorkbench.BusinessObjects.Lookups;
using MyWorkbench.Module.Web.Helpers;
using System.Collections.Generic;
using System.Net;

namespace MyWorkbench.Module.Web.Controllers
{
    public class ChangeUserPassword : ChangePasswordController
    {
        public new event EventHandler<CustomChangePasswordEventArgs> CustomChangePassword;

        private MultiTenantHelper _multiTenantHelper;
        private MultiTenantHelper MultiTenantHelper {
            get {
                if (_multiTenantHelper == null)
                    _multiTenantHelper = new MultiTenantHelper(Ignyt.Framework.Application.MyWorkbench, this.Application.ConnectionString.Database());
                return _multiTenantHelper;
            }
        }

        private Employee _authenticatingEmployee;
        private Employee AuthenticatingEmployee {
            get {
                if (_authenticatingEmployee == null)
                    _authenticatingEmployee = this.ObjectSpace.GetObjectByKey<Employee>((Guid)SecuritySystem.CurrentUserId);
                return _authenticatingEmployee;
            }
        }

        private bool IsCurrentUser(IObjectSpace objectSpace, object obj)
        {
            if ((obj == null) || (SecuritySystem.CurrentUser == null))
            {
                return false;
            }

            string currentUserHandle = objectSpace.GetObjectHandle(SecuritySystem.CurrentUser);
            string currentObjectHandle = objectSpace.GetObjectHandle(obj);

            return (currentUserHandle == currentObjectHandle);
        }

        protected override void ChangePassword(ChangePasswordParameters parameters)
        {
            Guard.ArgumentNotNull(parameters, "parameters");

            try
            {
                CustomChangePasswordEventArgs customChangePasswordEventArgs = new CustomChangePasswordEventArgs(parameters);

                CustomChangePassword?.Invoke(this, customChangePasswordEventArgs);

                if (!customChangePasswordEventArgs.Handled)
                {

                    if (!AuthenticatingEmployee.ComparePassword(parameters.OldPassword))
                    {
                        throw new Exception(String.Format("{0} {1}", SecurityExceptionLocalizer.GetExceptionMessage(SecurityExceptionId.OldPasswordIsWrong), SecurityExceptionLocalizer.GetExceptionMessage(SecurityExceptionId.RetypeTheInformation)));
                    }

                    if (parameters.NewPassword != parameters.ConfirmPassword)
                    {
                        throw new Exception(String.Format("{0} {1}", SecurityExceptionLocalizer.GetExceptionMessage(SecurityExceptionId.PasswordsAreDifferent), SecurityExceptionLocalizer.GetExceptionMessage(SecurityExceptionId.RetypeTheInformation)));
                    }

                    if (AuthenticatingEmployee.ComparePassword(parameters.NewPassword))
                    {
                        throw new Exception(String.Format("{0} {1}", SecurityExceptionLocalizer.GetExceptionMessage(SecurityExceptionId.NewPasswordIsEqualToOldPassword), SecurityExceptionLocalizer.GetExceptionMessage(SecurityExceptionId.RetypeTheInformation)));
                    }

                    KeyValuePair<HttpStatusCode, string> result = MultiTenantHelper.SetPassword(AuthenticatingEmployee, AuthenticatingEmployee, parameters.NewPassword);

                    if (result.Key == HttpStatusCode.OK)
                    {
                        AuthenticatingEmployee.SetPassword(parameters.NewPassword);
                        AuthenticatingEmployee.ChangePasswordOnFirstLogon = false;
                        this.ObjectSpace.SetModified(AuthenticatingEmployee);
                        this.ObjectSpace.CommitChanges();
                    }

                    SecurityModule.TryUpdateLogonParameters(parameters.NewPassword);

                    if (!View.ObjectSpace.IsModified)
                    {
                        bool isCurrentUser = IsCurrentUser(View.ObjectSpace, View.CurrentObject);
                        if (isCurrentUser)
                        {
                            View.ObjectSpace.ReloadObject(View.CurrentObject);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ToastMessageHelper.ShowErrorMessage(this.Application, ex, InformationPosition.Bottom);
            }
            finally
            {
                parameters.ClearValues();
            }
        }
    }
}
