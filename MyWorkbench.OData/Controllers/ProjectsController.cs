using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyWorkbench.BusinessObjects;

namespace MyWorkbench.OData.Controllers {
	public class ProjectsController : SecuredController {
		public ProjectsController(XpoDataStoreProviderService xpoDataStoreProviderService, IConfiguration config, SecurityProvider securityHelper, IHttpContextAccessor contextAccessor)
			: base(xpoDataStoreProviderService, config, securityHelper, contextAccessor) { }

		[HttpGet]
		[EnableQuery]
		public ActionResult Get() {
			IQueryable<Project> projects = ObjectSpace.GetObjects<Project>().AsQueryable();
			return Ok(projects);
		}
	}
}
