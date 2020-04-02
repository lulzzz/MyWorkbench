using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using MyWorkbench.BusinessObjects.Helpers.Notifications;
using MyWorkbench.BusinessObjects.Lookups;
using MyWorkbench.Module.Web.Helpers;
using System.Collections.Generic;

namespace MyWorkbench.Module.Web.Controllers
{
    public class EmployeeController : ObjectViewController<DetailView, Employee>
    {
        #region Properties
        private MultiTenantHelper _multiTenantHelper;
        private MultiTenantHelper MultiTenantHelper {
            get {
                if (_multiTenantHelper == null)
                    _multiTenantHelper = new MultiTenantHelper(Ignyt.Framework.Application.MyWorkbench, this.Application.ConnectionString.Database());
                return _multiTenantHelper;
            }
        }

        private Employee _employee;
        private Employee Employee {
            get {
                this._employee = this.View.CurrentObject as Employee;
                return this._employee;
            }
        }
        #endregion

        public EmployeeController() {}

        protected override void OnActivated()
        {
            base.OnActivated();

            this.ObjectSpace.ObjectSaving += ObjectSpace_ObjectSaving;
        }

        private void ObjectSpace_ObjectSaving(object sender, ObjectManipulatingEventArgs e)
        {
            if (this.Employee.EmployeeType == Ignyt.BusinessInterface.EmployeeType.SystemUser)
                MultiTenantHelper.CreateUser(this.Employee);
        }
    }
}
