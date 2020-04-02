using DevExpress.Persistent.BaseImpl;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using MyWorkbench.BusinessObjects;
using MyWorkbench.BusinessObjects.Accounts;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Lookups;
using System.Linq;

namespace MyWorkbench.OData
{
	public class Startup {
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services) {
			services.AddOData();
			services.AddMvc(options => {
				options.EnableEndpointRouting = false;
			}).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie();
			services.AddSingleton<XpoDataStoreProviderService>();
			services.AddSingleton(Configuration);
			services.AddHttpContextAccessor();
			services.AddScoped<SecurityProvider>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if(env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}
			else {
				app.UseHsts();
			}
			app.UseAuthentication();
			app.UseMiddleware<UnauthorizedRedirectMiddleware>();
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseCookiePolicy();
			app.UseMvc(routeBuilder => {
				routeBuilder.EnableDependencyInjection();
				routeBuilder.Count().Expand().Select().OrderBy().Filter().MaxTop(null);
				routeBuilder.MapODataServiceRoute("ODataRoutes", null, GetEdmModel());
			});
		}
		private IEdmModel GetEdmModel() {
			ODataModelBuilder builder = new ODataConventionModelBuilder();
            EntitySetConfiguration<WorkflowBase> workflowBases = builder.EntitySet<WorkflowBase>("WorkflowBases");
            EntitySetConfiguration<Employee> employees = builder.EntitySet<Employee>("Employees");
            EntitySetConfiguration<Project> projects = builder.EntitySet<Project>("Projects");
            EntitySetConfiguration<Quote> quotes = builder.EntitySet<Quote>("Quotes");
            EntitySetConfiguration<JobCard> jobcards = builder.EntitySet<JobCard>("JobCards");
            EntitySetConfiguration<Party> parties = builder.EntitySet<Party>("Parties");
			EntitySetConfiguration<PermissionContainer> permissions = builder.EntitySet<PermissionContainer>("Permissions");

			permissions.EntityType.HasKey(t => t.Key);
            workflowBases.EntityType.HasKey(t => t.Oid);
            employees.EntityType.HasKey(t => t.Oid);
            projects.EntityType.HasKey(t => t.Oid);
            quotes.EntityType.HasKey(t => t.Oid);
            jobcards.EntityType.HasKey(t => t.Oid);
            parties.EntityType.HasKey(t => t.Oid);

			FunctionConfiguration logon = builder.Function("Login");
			logon.Returns<int>();
			logon.Parameter<string>("userName");
			logon.Parameter<string>("password");
			builder.Action("Logoff");

			ActionConfiguration getPermissions = builder.Action("GetPermissions");
			getPermissions.Parameter<string>("typeName");
			getPermissions.CollectionParameter<string>("keys");
			return builder.GetEdmModel();
		}
	}
}
