using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyWorkbench.BusinessObjects.Lookups;

namespace MyWorkbench.OData.Controllers {
	public class EmployeesController : SecuredController {
		public EmployeesController(XpoDataStoreProviderService xpoDataStoreProviderService, IConfiguration config, SecurityProvider securityHelper, IHttpContextAccessor contextAccessor)
			: base(xpoDataStoreProviderService, config, securityHelper, contextAccessor) { }

		[HttpGet]
		[EnableQuery]
		public ActionResult Get() {
			IQueryable<Employee> employees = ObjectSpace.GetObjects<Employee>().AsQueryable();
			return Ok(employees);
		}
	}
}
