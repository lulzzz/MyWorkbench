using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Lookups;
using System;

namespace MyWorkbench.ExpressApp.Authentication {
    public static class UserSecurityHelper {
        private static IObjectSpace _objectSpace;
        private static Employee _employee;
        private static EmployeeRole _employeeRole;

        public static void CreateAdminUser(IObjectSpace ObjectSpace, 
            string FirstName, string LastName, string EmailAddress, string Password) {
            try {
                _objectSpace = ObjectSpace;

                _employee = ObjectSpace.FindObject<Employee>(new BinaryOperator("UserName", EmailAddress));
                _employeeRole = ObjectSpace.FindObject<EmployeeRole>(new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));

                if (_employeeRole == null) {
                    _employeeRole = ObjectSpace.CreateObject<EmployeeRole>();
                    _employeeRole.Name = SecurityStrategy.AdministratorRoleName;
                    _employeeRole.IsAdministrative = true;
                }

                if (_employee == null) {
                    _employee = ObjectSpace.CreateObject<Employee>();

                    _employee.FirstName = FirstName;
                    _employee.LastName = LastName;
                    _employee.Email = EmailAddress;
                    _employee.UserName = EmailAddress;
                    _employee.SetPassword(Password);
                    _employee.EmployeeType = EmployeeType.SystemUser;
                    _employee.EmployeeRoles.Add(_employeeRole);

                    _objectSpace.CommitChanges();
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public static void UpdateUserPassword(IObjectSpace ObjectSpace, string EmailAddress, string Password) {
            try {
                _objectSpace = ObjectSpace;

                _employee = ObjectSpace.FindObject<Employee>(new BinaryOperator("UserName", EmailAddress));

                if (_employee != null) {
                    if (_employee.ComparePassword(Password) == false)
                    {
                        _employee.SetPassword(Password);
                        _objectSpace.CommitChanges();
                    }
                } else {
                    throw new Exception("Login failed for " + EmailAddress + ". User account not found or failed to be created.") ;
                }
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
