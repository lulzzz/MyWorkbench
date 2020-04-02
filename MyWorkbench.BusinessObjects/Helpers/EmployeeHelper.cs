using Ignyt.Framework.Api;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.Collections.Generic;
using System.Net;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public static class EmployeeHelper
    {
        public static string CreatePortalUser(this Employee Employee, string UserName, string Password, string EmailAdress, string FirstName, string LastName, string Database)
        {
            try
            {
                KeyValuePair<HttpStatusCode, string> result = new DotNetNukeApi(Ignyt.Framework.Application.MyWorkbench).CreateUser(UserName, Password, EmailAdress, FirstName, LastName, Database);

                return result.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}