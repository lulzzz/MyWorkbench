using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using MyWorkbench.ExpressApp.Authentication;
using System.Configuration;
using Ignyt.Framework;
using Ignyt.Framework.Utilities;
using MyWorkbench.BusinessObjects;
using System.Globalization;
using MyWorkbench.Web.Helpers;
using MyWorkbench.Module.Web.Helpers;
using Exceptionless;
using DevExpress.ExpressApp.MiddleTier;
using DevExpress.Xpo;

namespace MyWorkbench.Web {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/DevExpressExpressAppWebWebApplicationMembersTopicAll.aspx
    public partial class MyWorkbenchAspNetApplication : WebApplication {
        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule module2;
        private MyWorkbench.Module.MyWorkbenchModule module3;
        private MyWorkbench.Module.Web.MyWorkbenchAspNetModule module4;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex1;
        private DevExpress.ExpressApp.Security.AuthenticationStandard authenticationStandard1;
        private DevExpress.ExpressApp.AuditTrail.AuditTrailModule auditTrailModule;
        private DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule objectsModule;
        private DevExpress.ExpressApp.Chart.ChartModule chartModule;
        private DevExpress.ExpressApp.Chart.Web.ChartAspNetModule chartAspNetModule;
        private DevExpress.ExpressApp.CloneObject.CloneObjectModule cloneObjectModule;
        private DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule conditionalAppearanceModule;
        private DevExpress.ExpressApp.Dashboards.DashboardsModule dashboardsModule;
        private DevExpress.ExpressApp.Dashboards.Web.DashboardsAspNetModule dashboardsAspNetModule;
        private DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule fileAttachmentsAspNetModule;
        private DevExpress.ExpressApp.HtmlPropertyEditor.Web.HtmlPropertyEditorAspNetModule htmlPropertyEditorAspNetModule;
        private DevExpress.ExpressApp.Kpi.KpiModule kpiModule;
        private DevExpress.ExpressApp.Maps.Web.MapsAspNetModule mapsAspNetModule;
        private DevExpress.ExpressApp.Notifications.NotificationsModule notificationsModule;
        private DevExpress.ExpressApp.Notifications.Web.NotificationsAspNetModule notificationsAspNetModule;
        private DevExpress.ExpressApp.PivotChart.PivotChartModuleBase pivotChartModuleBase;
        private DevExpress.ExpressApp.PivotChart.Web.PivotChartAspNetModule pivotChartAspNetModule;
        private DevExpress.ExpressApp.PivotGrid.PivotGridModule pivotGridModule;
        private DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule pivotGridAspNetModule;
        private DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModuleV2;
        private DevExpress.ExpressApp.ReportsV2.Web.ReportsAspNetModuleV2 reportsAspNetModuleV2;
        private DevExpress.ExpressApp.Scheduler.SchedulerModuleBase schedulerModuleBase;
        private DevExpress.ExpressApp.Scheduler.Web.SchedulerAspNetModule schedulerAspNetModule;
        private DevExpress.ExpressApp.StateMachine.StateMachineModule stateMachineModule;
        private DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase treeListEditorsModuleBase;
        private DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule treeListEditorsAspNetModule;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule;
        private DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule validationAspNetModule;
        private DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule viewVariantsModule;
        private DevExpress.ExpressApp.Workflow.WorkflowModule workflowModule;
        private MultiTenantHelper _multiTenantHelper;
        private DevExpress.ExpressApp.Office.Web.OfficeAspNetModule officeAspNetModule1;
        private DevExpress.ExpressApp.Office.OfficeModule officeModule1;

        private MultiTenantHelper MultiTenantHelper {
            get {
                if (_multiTenantHelper == null)
                    _multiTenantHelper = MultiTenantHelper.InstanceApplication(Application.MyWorkbench);
                return _multiTenantHelper;
            }
        }

        private string _cultureName;
        private string CultureName {
            get {
                if (_cultureName == null)
                {
                    Culture culture = MyWorkbench.BaseObjects.Constants.Constants.CultureInfo(((XPObjectSpace)WebApplication.Instance.CreateObjectSpace()).Session);

                    _cultureName = culture != null ? culture.Name : "en-GB";
                }

                return _cultureName;
            }
        }

        #region Default XAF configuration options (https://www.devexpress.com/kb=T501418)
        static MyWorkbenchAspNetApplication() {
            EnableMultipleBrowserTabsSupport = true;
            //DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.AllowFilterControlHierarchy = true;
            //DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.MaxFilterControlHierarchyDepth = 3;
            //DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.AllowFilterControlHierarchyDefault = true;
            //DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.MaxHierarchyDepthDefault = 3;
            OptimizationSettings.AllowFastProcessListViewRecordActions = true;
            OptimizationSettings.AllowFastProcessLookupPopupWindow = true;
            OptimizationSettings.AllowFastProcessObjectsCreationActions = true;
            OptimizationSettings.EnableMenuDelayedCreation = true;
            OptimizationSettings.EnableNavigationControlDelayedCreation = true;
            OptimizationSettings.LockRecoverViewStateOnNavigationCallback = true;
            OptimizationSettings.WebPropertyEditorOptimizeReadValue = true;
            OptimizationSettings.UseModelValuePersistentPathCalculatorCache = true;
            OptimizationSettings.AllowFastProcessObjectsCreationActions = true;
            DevExpress.ExpressApp.Model.ModelXmlReader.UseXmlReader = true;
            DevExpress.ExpressApp.Model.Core.ModelNode.UseDefaultValuesCache = true;
            DevExpress.ExpressApp.Core.ControllersManager.UseParallelBatchControllerCreation = true;
            DevExpress.ExpressApp.ApplicationModulesManager.UseParallelTypesCollector = true;
            DevExpress.ExpressApp.ApplicationModulesManager.UseStaticCache = true;
            DevExpress.Persistent.Base.ReflectionHelper.UseAssemblyResolutionCache = true;
        }

        private void InitializeDefaults() {
            this.LinkNewObjectToParentImmediately = false;
            this.OptimizedControllersCreation = true;
            this.DelayedViewItemsInitialization = true;
        }
        #endregion

        public MyWorkbenchAspNetApplication() {
            DevExpress.ExpressApp.Security.SecurityAdapterHelper.Enable(DevExpress.ExpressApp.Security.Adapters.ReloadPermissionStrategy.CacheOnFirstAccess);

            InitializeComponent();
			InitializeDefaults();

            this.ShowViewStrategy = new MyShowViewStrategy(this);
        }

        protected override IViewUrlManager CreateViewUrlManager()
        {
            return new ViewUrlManager();
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProvider = new SecuredObjectSpaceProvider((SecurityStrategyComplex)Security, GetDataStoreProvider(args.ConnectionString, args.Connection), true);
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
            ((SecuredObjectSpaceProvider)args.ObjectSpaceProvider).AllowICommandChannelDoWithSecurityContext = true;
        }

        private IXpoDataStoreProvider GetDataStoreProvider(string connectionString, System.Data.IDbConnection connection) {
            System.Web.HttpApplicationState application = System.Web.HttpContext.Current?.Application;
            IXpoDataStoreProvider dataStoreProvider;
            if (application != null && application["DataStoreProvider"] != null) {
                dataStoreProvider = application["DataStoreProvider"] as IXpoDataStoreProvider;
            }
            else {
                dataStoreProvider = new CachedDataStoreProvider(connectionString, connection, true);
                if (application != null) {
                    application["DataStoreProvider"] = dataStoreProvider;
                }
            }
			return dataStoreProvider;
        }

        protected override void OnLoggingOn(LogonEventArgs args) {
            MultiTenantHelper.Authenticate((args.LogonParameters as AuthenticationStandardLogonParameters).UserName, 
                (args.LogonParameters as AuthenticationStandardLogonParameters).Password);
            this.ConnectionString = MultiTenantHelper.ConnectionString(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            MultiTenantHelper.CreateUser(this.ObjectSpaceProvider.CreateUpdatingObjectSpace(true));

            if (!MyWorkbench.BaseObjects.Constants.Constants.HasAccessToSettings(((XPObjectSpace)WebApplication.Instance.CreateObjectSpace()).Session))
                throw new Exception("User does not have access to read the settings. Please rectify in user roles by allowing access to Settings.");

            base.OnLoggingOn(args);
        }

        protected override void OnLoggedOn(LogonEventArgs args) {
            ConfigHelper.GoogleMapsApi = MultiTenantHelper.AutheticationObject.GoogleMapsApi;
            ConfigHelper.BingMapsApi = MultiTenantHelper.AutheticationObject.BingMapsApi;

            this.mapsAspNetModule.GoogleApiKey = ConfigHelper.GoogleMapsApi;
            this.mapsAspNetModule.BingApiKey = ConfigHelper.BingMapsApi;

            this.Model.Options.UseServerMode = true;

            this.SetApplicationCulture();

            base.OnLoggedOn(args);
        }

        private void SetApplicationCulture()
        {
            WebApplication.Instance.SetFormattingCulture(this.CultureName);
            WebApplication.Instance.SetLanguage(this.CultureName);
            WebApplication.Instance.CustomizeFormattingCulture += Instance_CustomizeFormattingCulture1;
            CultureInfo.DefaultThreadCurrentCulture = WebUtils.SetUserLocaleNew(this.CultureName);
            CultureInfo.DefaultThreadCurrentUICulture = WebUtils.SetUserLocaleNew(this.CultureName);
        }

        private void Instance_CustomizeFormattingCulture1(object sender, CustomizeFormattingCultureEventArgs e)
        {
            e.FormattingCulture = WebUtils.SetUserLocaleNew(this.CultureName);
        }

        private void MyWorkbenchAspNetApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
            e.Updater.Update();

            MultiTenantHelper.CreateUser(this.ObjectSpaceProvider.CreateUpdatingObjectSpace(true));

            e.Handled = true;
        }

        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule();
            this.module3 = new MyWorkbench.Module.MyWorkbenchModule();
            this.module4 = new MyWorkbench.Module.Web.MyWorkbenchAspNetModule();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
            this.auditTrailModule = new DevExpress.ExpressApp.AuditTrail.AuditTrailModule();
            this.objectsModule = new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule();
            this.chartModule = new DevExpress.ExpressApp.Chart.ChartModule();
            this.chartAspNetModule = new DevExpress.ExpressApp.Chart.Web.ChartAspNetModule();
            this.cloneObjectModule = new DevExpress.ExpressApp.CloneObject.CloneObjectModule();
            this.conditionalAppearanceModule = new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule();
            this.dashboardsModule = new DevExpress.ExpressApp.Dashboards.DashboardsModule();
            this.dashboardsAspNetModule = new DevExpress.ExpressApp.Dashboards.Web.DashboardsAspNetModule();
            this.fileAttachmentsAspNetModule = new DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule();
            this.htmlPropertyEditorAspNetModule = new DevExpress.ExpressApp.HtmlPropertyEditor.Web.HtmlPropertyEditorAspNetModule();
            this.kpiModule = new DevExpress.ExpressApp.Kpi.KpiModule();
            this.mapsAspNetModule = new DevExpress.ExpressApp.Maps.Web.MapsAspNetModule();
            this.notificationsModule = new DevExpress.ExpressApp.Notifications.NotificationsModule();
            this.notificationsAspNetModule = new DevExpress.ExpressApp.Notifications.Web.NotificationsAspNetModule();
            this.pivotChartModuleBase = new DevExpress.ExpressApp.PivotChart.PivotChartModuleBase();
            this.pivotChartAspNetModule = new DevExpress.ExpressApp.PivotChart.Web.PivotChartAspNetModule();
            this.pivotGridModule = new DevExpress.ExpressApp.PivotGrid.PivotGridModule();
            this.pivotGridAspNetModule = new DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule();
            this.reportsModuleV2 = new DevExpress.ExpressApp.ReportsV2.ReportsModuleV2();
            this.reportsAspNetModuleV2 = new DevExpress.ExpressApp.ReportsV2.Web.ReportsAspNetModuleV2();
            this.schedulerModuleBase = new DevExpress.ExpressApp.Scheduler.SchedulerModuleBase();
            this.schedulerAspNetModule = new DevExpress.ExpressApp.Scheduler.Web.SchedulerAspNetModule();
            this.stateMachineModule = new DevExpress.ExpressApp.StateMachine.StateMachineModule();
            this.treeListEditorsModuleBase = new DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase();
            this.treeListEditorsAspNetModule = new DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule();
            this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.validationAspNetModule = new DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule();
            this.viewVariantsModule = new DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule();
            this.workflowModule = new DevExpress.ExpressApp.Workflow.WorkflowModule();
            this.officeAspNetModule1 = new DevExpress.ExpressApp.Office.Web.OfficeAspNetModule();
            this.officeModule1 = new DevExpress.ExpressApp.Office.OfficeModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // securityStrategyComplex1
            // 
            this.securityStrategyComplex1.AllowAnonymousAccess = false;
            this.securityStrategyComplex1.Authentication = this.authenticationStandard1;
            this.securityStrategyComplex1.RoleType = typeof(MyWorkbench.BusinessObjects.Lookups.EmployeeRole);
            this.securityStrategyComplex1.SupportNavigationPermissionsForTypes = false;
            this.securityStrategyComplex1.UserType = typeof(MyWorkbench.BusinessObjects.Lookups.Employee);
            // 
            // authenticationStandard1
            // 
            this.authenticationStandard1.LogonParametersType = typeof(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters);
            // 
            // auditTrailModule
            // 
            this.auditTrailModule.AuditDataItemPersistentType = typeof(DevExpress.Persistent.BaseImpl.AuditDataItemPersistent);
            // 
            // cloneObjectModule
            // 
            this.cloneObjectModule.ClonerType = null;
            // 
            // dashboardsModule
            // 
            this.dashboardsModule.DashboardDataType = typeof(DevExpress.Persistent.BaseImpl.DashboardData);
            // 
            // notificationsModule
            // 
            this.notificationsModule.CanAccessPostponedItems = false;
            this.notificationsModule.NotificationsRefreshInterval = System.TimeSpan.Parse("00:05:00");
            this.notificationsModule.NotificationsStartDelay = System.TimeSpan.Parse("00:00:05");
            this.notificationsModule.ShowDismissAllAction = true;
            this.notificationsModule.ShowNotificationsWindow = false;
            this.notificationsModule.ShowRefreshAction = true;
            // 
            // pivotChartModuleBase
            // 
            this.pivotChartModuleBase.DataAccessMode = DevExpress.ExpressApp.CollectionSourceDataAccessMode.Client;
            this.pivotChartModuleBase.ShowAdditionalNavigation = false;
            // 
            // reportsModuleV2
            // 
            this.reportsModuleV2.EnableInplaceReports = true;
            this.reportsModuleV2.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportDataV2);
            this.reportsModuleV2.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
            // 
            // reportsAspNetModuleV2
            // 
            this.reportsAspNetModuleV2.ReportViewerType = DevExpress.ExpressApp.ReportsV2.Web.ReportViewerTypes.HTML5;
            // 
            // stateMachineModule
            // 
            this.stateMachineModule.StateMachineStorageType = typeof(DevExpress.ExpressApp.StateMachine.Xpo.XpoStateMachine);
            // 
            // validationModule
            // 
            this.validationModule.AllowValidationDetailsAccess = true;
            this.validationModule.IgnoreWarningAndInformationRules = false;
            // 
            // workflowModule
            // 
            this.workflowModule.RunningWorkflowInstanceInfoType = typeof(DevExpress.ExpressApp.Workflow.Xpo.XpoRunningWorkflowInstanceInfo);
            this.workflowModule.StartWorkflowRequestType = typeof(DevExpress.ExpressApp.Workflow.Xpo.XpoStartWorkflowRequest);
            this.workflowModule.UserActivityVersionType = typeof(DevExpress.ExpressApp.Workflow.Versioning.XpoUserActivityVersion);
            this.workflowModule.WorkflowControlCommandRequestType = typeof(DevExpress.ExpressApp.Workflow.Xpo.XpoWorkflowInstanceControlCommandRequest);
            this.workflowModule.WorkflowDefinitionType = typeof(DevExpress.ExpressApp.Workflow.Xpo.XpoWorkflowDefinition);
            this.workflowModule.WorkflowInstanceKeyType = typeof(DevExpress.Workflow.Xpo.XpoInstanceKey);
            this.workflowModule.WorkflowInstanceType = typeof(DevExpress.Workflow.Xpo.XpoWorkflowInstance);
            // 
            // officeModule1
            // 
            this.officeModule1.RichTextMailMergeDataType = typeof(DevExpress.Persistent.BaseImpl.RichTextMailMergeData);
            // 
            // MyWorkbenchAspNetApplication
            // 
            this.ApplicationName = "MyWorkbench";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.MaxLogonAttemptCount = 20;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.auditTrailModule);
            this.Modules.Add(this.objectsModule);
            this.Modules.Add(this.chartModule);
            this.Modules.Add(this.cloneObjectModule);
            this.Modules.Add(this.conditionalAppearanceModule);
            this.Modules.Add(this.dashboardsModule);
            this.Modules.Add(this.validationModule);
            this.Modules.Add(this.kpiModule);
            this.Modules.Add(this.notificationsModule);
            this.Modules.Add(this.pivotChartModuleBase);
            this.Modules.Add(this.pivotGridModule);
            this.Modules.Add(this.reportsModuleV2);
            this.Modules.Add(this.schedulerModuleBase);
            this.Modules.Add(this.stateMachineModule);
            this.Modules.Add(this.treeListEditorsModuleBase);
            this.Modules.Add(this.viewVariantsModule);
            this.Modules.Add(this.workflowModule);
            this.Modules.Add(this.securityModule1);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.chartAspNetModule);
            this.Modules.Add(this.dashboardsAspNetModule);
            this.Modules.Add(this.fileAttachmentsAspNetModule);
            this.Modules.Add(this.htmlPropertyEditorAspNetModule);
            this.Modules.Add(this.mapsAspNetModule);
            this.Modules.Add(this.notificationsAspNetModule);
            this.Modules.Add(this.pivotChartAspNetModule);
            this.Modules.Add(this.pivotGridAspNetModule);
            this.Modules.Add(this.reportsAspNetModuleV2);
            this.Modules.Add(this.schedulerAspNetModule);
            this.Modules.Add(this.treeListEditorsAspNetModule);
            this.Modules.Add(this.validationAspNetModule);
            this.Modules.Add(this.module4);
            this.Modules.Add(this.officeModule1);
            this.Modules.Add(this.officeAspNetModule1);
            this.Security = this.securityStrategyComplex1;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.MyWorkbenchAspNetApplication_DatabaseVersionMismatch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
