using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyWorkbench.BusinessObjects;
using MyWorkbench.BusinessObjects.Accounts;

namespace MyWorkbench.OData.Controllers {
	public class QuotesController : SecuredController {
		public QuotesController(XpoDataStoreProviderService xpoDataStoreProviderService, IConfiguration config, SecurityProvider securityHelper, IHttpContextAccessor contextAccessor)
			: base(xpoDataStoreProviderService, config, securityHelper, contextAccessor) { }

		[HttpGet]
		[EnableQuery]
		public ActionResult Get() {
			IQueryable<Quote> quotes = ObjectSpace.GetObjects<Quote>().AsQueryable();
			return Ok(quotes);
		}
	}
}
