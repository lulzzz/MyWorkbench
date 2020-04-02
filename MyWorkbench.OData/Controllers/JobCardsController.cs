using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyWorkbench.BusinessObjects;

namespace MyWorkbench.OData.Controllers {
	public class JobCardsController : SecuredController {
		public JobCardsController(XpoDataStoreProviderService xpoDataStoreProviderService, IConfiguration config, SecurityProvider securityHelper, IHttpContextAccessor contextAccessor)
			: base(xpoDataStoreProviderService, config, securityHelper, contextAccessor) { }

		[HttpGet]
		[EnableQuery]
		public ActionResult Get() {
			IQueryable<JobCard> jobcards = ObjectSpace.GetObjects<JobCard>().AsQueryable();
			return Ok(jobcards);
		}
	}
}
