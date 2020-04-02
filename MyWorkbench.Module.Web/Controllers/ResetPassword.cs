using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Utils;
using MyWorkbench.BusinessObjects.Helpers.Notifications;
using MyWorkbench.BusinessObjects.Lookups;
using MyWorkbench.Module.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Net;

namespace MyWorkbench.Module.Web.Controllers
{
    public class ResetPassword : ResetPasswordController
    {
        public new event EventHandler<CustomResetPasswordEventArgs> CustomResetPassword;

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

        private Employee _targetEmployee;
        private Employee TargetEmployee {
            get {
                return _targetEmployee;
            }
            set {
                _targetEmployee = value;
            }
        }

        protected override void ExecuteResetPassword(object targetUserObject, ResetPasswordParameters resetPasswordParameters)
        {

            Guard.ArgumentNotNull(targetUserObject, "targetUserObject");
            Guard.ArgumentNotNull(resetPasswordParameters, "resetPasswordParameters");

            CustomResetPasswordEventArgs customResetPasswordEventArgs = new CustomResetPasswordEventArgs(targetUserObject, resetPasswordParameters);
            try
            {
                CustomResetPassword?.Invoke(this, customResetPasswordEventArgs);

                if (!customResetPasswordEventArgs.Handled)
                {
                    this.TargetEmployee = (Employee)targetUserObject;

                    KeyValuePair<HttpStatusCode, string> result = MultiTenantHelper.SetPassword(this.AuthenticatingEmployee, this.TargetEmployee, resetPasswordParameters.Password);

                    if (result.Key == HttpStatusCode.OK)
                    {
                        this.TargetEmployee.SetPassword(resetPasswordParameters.Password);
                        this.TargetEmployee.ChangePasswordOnFirstLogon = false;
                        this.ObjectSpace.CommitChanges();
                    }

                    if (SecuritySystem.CurrentUserId.Equals(ObjectSpace.GetKeyValue(targetUserObject)))
                    {
                        SecurityModule.TryUpdateLogonParameters(resetPasswordParameters.Password);
                    }
                }
            }
            catch (Exception ex)
            {
                ToastMessageHelper.ShowErrorMessage(this.Application, ex, InformationPosition.Bottom);
            }
        }
    }
}
