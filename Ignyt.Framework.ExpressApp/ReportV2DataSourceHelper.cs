using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base.ReportsV2;
using DevExpress.Xpo;
using DevExpress.XtraReports.UI;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.ReportsV2;
using System.Data;
using DevExpress.ExpressApp;

namespace Ignyt.Framework.ExpressApp {
    public class ReportV2DataSourceHelper : IDisposable {
        public const string XafReportParametersObjectName = "XafReportParametersObject";
        private Session session;
        private IXPSimpleObject currentObject;
        private Dictionary<int, IReportObjectSpaceProvider> objectSpaceProvidersCache = new Dictionary<int, IReportObjectSpaceProvider>();
        IDbConnection connection;
        XPObjectSpaceProvider provider;
        IObjectSpace objectspace;

        private IReportObjectSpaceProvider ReportObjectSpaceProvider(Session Session) {
            connection = ((DevExpress.Xpo.DB.MSSqlConnectionProvider)(((DevExpress.Xpo.Helpers.BaseDataLayer)(Session.DataLayer)).ConnectionProvider)).Connection;
            provider = new XPObjectSpaceProvider(new ConnectionDataStoreProvider(connection));

            objectspace = provider.CreateObjectSpace();

            if (currentObject != null)
                this.CloneObject(objectspace);

            return new ReportObjectSpaceProvider(objectspace);
        }

        public ReportV2DataSourceHelper(Session Session)
            : this() {
                this.session = Session;
        }

        public ReportV2DataSourceHelper(IXPSimpleObject CurrentObject)
            : this() {
            this.session = CurrentObject.Session;
            this.currentObject = CurrentObject;
        }

        private ReportV2DataSourceHelper() { }

        public void SetupBeforePrint(XtraReport report) {
            SetupBeforePrint(report, null, null, false, null, false);
        }

        public void SetupBeforePrint(XtraReport report, ReportParametersObjectBase parametersObject, CriteriaOperator criteria, bool canApplyCriteria, SortProperty[] sortProperty, bool canApplySortProperty) {
            SetupReport(report, parametersObject, criteria, canApplyCriteria, sortProperty, canApplySortProperty);
            OnBeforeShowPreview(report);
        }

        private void CloneObject(IObjectSpace ObjectSpace) {
            CloneIXPSimpleObjectHelper helper = new CloneIXPSimpleObjectHelper(((XPObjectSpace)ObjectSpace).Session);
            helper.Clone(currentObject,true);
        }

        public void SetupReport(XtraReport report) {
            SetupReport(report, null, null, false, null, false);
        }

        public void SetupReport(XtraReport report, ReportParametersObjectBase parametersObject, CriteriaOperator criteria, bool canApplyCriteria, SortProperty[] sortProperty, bool canApplySortProperty) {
            SetupReportDataSource(report, criteria, canApplyCriteria, sortProperty, canApplySortProperty);
            SetXafReportParametersObject(report, parametersObject);
            RegisterObjectSpaceProviderService(report);
            RegisterReportEnumLocalizer(report);
            AttachCriteriaWithReportParametersManager(report);
        }

        public void AttachCriteriaWithReportParametersManager(XtraReport report) {
            report.BeforePrint += Report_BeforePrint;
        }

        public virtual object GetMasterReportDataSource(XtraReport report) {
            object result = null;
            if (report.DataSource != null) {
                result = report.DataSource;
            }
            return result;
        }

        public IReportObjectSpaceProvider CreateReportObjectSpaceProviderCore(Component component) {
            DisposeReportObjectSpaceProvider(component);
            IReportObjectSpaceProvider objectSpaceProvider = CreateReportObjectSpaceProvider();
            objectSpaceProvidersCache.Add(component.GetHashCode(), objectSpaceProvider);
            component.Disposed += new EventHandler(Report_Disposed);
            return objectSpaceProvider;
        }

        protected virtual void OnCustomSetupReportDataSource(CustomSetupReportDataSourceEventArgs args) {
            if (CustomSetupReportDataSource != null) {
                CustomSetupReportDataSource(this, args);
            }
        }
        public virtual void SetXafReportParametersObject(XtraReport report, ReportParametersObjectBase parametersObject) {
            if (parametersObject != null) {
                DevExpress.XtraReports.Parameters.Parameter xafReportParameter = new DevExpress.XtraReports.Parameters.Parameter() { Name = XafReportParametersObjectName, Value = parametersObject, Type = typeof(ReportParametersObjectBase), Visible = false };
                report.Parameters.Add(xafReportParameter);
            }
        }

        protected void SetCriteria(CriteriaOperator criteria, object dataSource) {
            CustomSetCriteriaEventArgs args = new CustomSetCriteriaEventArgs(criteria, dataSource);
            OnCustomSetCriteria(args);
            if (!args.Handled) {
                ((ISupportCriteria)dataSource).Criteria = criteria;
            }
        }

        protected void SetSorting(SortProperty[] sortProperty, object dataSource) {
            CustomSetSortingEventArgs args = new CustomSetSortingEventArgs(sortProperty, dataSource);
            OnCustomSetSorting(args);
            if (!args.Handled) {
                ((ISupportSorting)dataSource).Sorting.Clear();
                if (sortProperty != null) {
                    ((ISupportSorting)dataSource).Sorting.AddRange(sortProperty);
                }
            }
        }

        protected virtual void OnCustomSetCriteria(CustomSetCriteriaEventArgs args) {
            if (CustomSetCriteria != null) {
                CustomSetCriteria(this, args);
            }
        }
        protected virtual void OnCustomSetSorting(CustomSetSortingEventArgs args) {
            if (CustomSetSorting != null) {
                CustomSetSorting(this, args);
            }
        }

        protected virtual ReportEnumLocalizer CreateReportEnumLocalizer() {
            return new ReportEnumLocalizer();
        }
        protected virtual IReportObjectSpaceProvider CreateReportObjectSpaceProvider() {
            return ReportObjectSpaceProvider(this.session);
        }

        protected virtual void OnBeforeShowPreview(XtraReport report) {
            if (BeforeShowPreview != null) {
                BeforeShowPreviewEventArgs args = new BeforeShowPreviewEventArgs(report);
                BeforeShowPreview(this, args);
            }
        }

        public void SetupReportDataSource(XtraReport report, CriteriaOperator criteria, bool canApplyCriteria, SortProperty[] sortProperty, bool canApplySortProperty) {
            CustomSetupReportDataSourceEventArgs args = new CustomSetupReportDataSourceEventArgs(report, criteria, canApplyCriteria, sortProperty, canApplySortProperty);
            OnCustomSetupReportDataSource(args);
            if (!args.Handled) {
                if (canApplyCriteria || canApplySortProperty) {
                    object dataSource = GetMasterReportDataSource(report);
                    if (dataSource != null) {
                        if (canApplyCriteria) {
                            SetCriteria(criteria, dataSource);
                        }
                        if (canApplySortProperty) {
                            SetSorting(sortProperty, dataSource);
                        }
                    }
                }
            }
        }

        public void RegisterObjectSpaceProviderService(XtraReport report) {
            IServiceContainer serviceContainer = report.PrintingSystem;
            serviceContainer.RemoveService(typeof(IReportObjectSpaceProvider));
            IReportObjectSpaceProvider objectSpaceProvider = CreateReportObjectSpaceProviderCore(report);
            serviceContainer.AddService(typeof(IReportObjectSpaceProvider), objectSpaceProvider);
        }

        public void RegisterReportEnumLocalizer(XtraReport report) {
            ReportEnumLocalizer enumLocalizer = CreateReportEnumLocalizer();
            enumLocalizer.Attach(report);
        }

        private void ClearObjectSpaceProvidersCache() {
            foreach (KeyValuePair<int, IReportObjectSpaceProvider> item in objectSpaceProvidersCache) {
                item.Value.DisposeObjectSpaces();
            }
            objectSpaceProvidersCache.Clear();
        }

        private void DisposeReportObjectSpaceProvider(Component report) {
            IReportObjectSpaceProvider objectSpaceProvider;
            objectSpaceProvidersCache.TryGetValue(report.GetHashCode(), out objectSpaceProvider);
            if (objectSpaceProvider != null) {
                objectSpaceProvidersCache.Remove(report.GetHashCode());
                objectSpaceProvider.DisposeObjectSpaces();
            }
        }

        private void Report_Disposed(object sender, EventArgs e) {
            Component report = (Component)sender;
            report.Disposed -= new EventHandler(Report_Disposed);
            DisposeReportObjectSpaceProvider(report);
        }

        private void Report_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            UpdateReportDataSourceCriteria((XtraReport)sender);
        }

        private void UpdateReportDataSourceCriteria(XtraReport report) {
            if (report.Parameters.Count > 0) {
                if (report.DataSource is DataSourceBase) {
                    ((DataSourceBase)report.DataSource).UpdateCriteriaWithReportParameters(report);
                }
            }
        }

        public event EventHandler<CustomSetCriteriaEventArgs> CustomSetCriteria;
        public event EventHandler<CustomSetSortingEventArgs> CustomSetSorting;
        public event EventHandler<CustomSetupReportDataSourceEventArgs> CustomSetupReportDataSource;
        public event EventHandler<BeforeShowPreviewEventArgs> BeforeShowPreview;

        public void Dispose() {
            if(this.objectspace != null)
            {
                this.objectspace.Dispose();
                this.objectspace = null;
            }

            if (this.provider != null) {
                this.provider.Dispose();
                this.provider = null;
            }
        }
    }
}
