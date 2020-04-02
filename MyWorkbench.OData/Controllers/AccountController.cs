using Ignyt.Framework;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyWorkbench.ExpressApp.Authentication;
using System;

namespace MyWorkbench.OData.Controllers {
	public class AccountController : BaseController {
        private MultiTenantHelper _multiTenantHelper;
        private MultiTenantHelper MultiTenantHelper {
            get {
                if (_multiTenantHelper == null)
                    _multiTenantHelper = MultiTenantHelper.InstanceApplication(Application.MyWorkbench);
                return _multiTenantHelper;
            }
        }

        public AccountController(XpoDataStoreProviderService xpoDataStoreProviderService, IConfiguration config, SecurityProvider securityHelper)
			: base(xpoDataStoreProviderService, config, securityHelper) { }
		[HttpGet]
		[ODataRoute("Login(userName={userName}, password={password})")]
		[AllowAnonymous]
		public ActionResult Login(string userName, string password) {
			ActionResult result;
			string connectionString = Config.GetConnectionString("MyWorkbench");

            try
            {
                MultiTenantHelper.Authenticate(userName, password);
                connectionString = MultiTenantHelper.ConnectionString(connectionString);

                if (SecurityProvider.InitConnection(userName, password, HttpContext, XpoDataStoreProviderService, connectionString))
                {
                    result = Ok();
                }
                else
                {
                    result = Unauthorized();
                }
            }
            catch
            {
                result = Unauthorized();
            }

            return result;
        }

		[HttpGet]
		[ODataRoute("Logoff()")]
		public ActionResult Logoff() {
			HttpContext.SignOutAsync();
			return Ok();
		}
	}
}
