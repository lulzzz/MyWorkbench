using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.Linq;

namespace MyWorkbench.BusinessObjects.Lookups {
    [ImageName("BO_Role")]
    [NavigationItem("People")]
    public class EmployeeRole : PermissionPolicyRoleBase, IPermissionPolicyRoleWithUsers {
        public EmployeeRole(Session session)
            : base(session) {
        }

        [Association("Employee_EmployeeRole")]
        public XPCollection<Employee> Employees {
            get {
                return GetCollection<Employee>("Employees");
            }
        }

        IEnumerable<IPermissionPolicyUser> IPermissionPolicyRoleWithUsers.Users {
            get { return Employees.OfType<IPermissionPolicyUser>(); }
        }
    }
}
