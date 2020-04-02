using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.DC.Xpo;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Data;
using System.IO;

namespace Ignyt.Framework.ExpressApp {
    public class ReportsV2Helper : IDisposable {
        private ReportDataSourceHelper _reportDataSourceHelper;
        private ReportV2DataSourceHelper _reportV2DataSourceHelper;
        private IXPSimpleObject _currentObject;
        IObjectSpace _objectSpace;

        private IObjectSpace ReportObjectSpaceProvider {
            get {
                if (_objectSpace == null) {
                    XpoTypesInfoHelper.ForceInitialize();
                    ITypesInfo typesInfo = XpoTypesInfoHelper.GetTypesInfo();
                    XpoTypeInfoSource xpoTypeInfoSource = XpoTypesInfoHelper.GetXpoTypeInfoSource();
                    RegisterBOTypes(typesInfo);

                    IDbConnection connection = ((DevExpress.Xpo.DB.MSSqlConnectionProvider)(((DevExpress.Xpo.Helpers.BaseDataLayer)(this._currentObject.Session.DataLayer)).ConnectionProvider)).Connection;
                    XPObjectSpaceProvider provider = new XPObjectSpaceProvider(new ConnectionDataStoreProvider(connection), typesInfo, xpoTypeInfoSource);

                    _objectSpace = provider.CreateObjectSpace();
                }

                return _objectSpace;
            }
        }

        public ReportsV2Helper(XafApplication Application, IXPSimpleObject CurrentObject) {
            this._currentObject = CurrentObject;

            if (Application != null)
                this._reportDataSourceHelper = new ReportDataSourceHelper(Application);
            else
                this._reportV2DataSourceHelper = new ReportV2DataSourceHelper(this._currentObject.Session);
        }

        public ReportsV2Helper(IXPSimpleObject CurrentObject)
        {
            this._currentObject = CurrentObject;

            this._reportV2DataSourceHelper = new ReportV2DataSourceHelper(this._currentObject.Session);
        }

        private void RegisterBOTypes(ITypesInfo typesInfo) {
            foreach (Type type in _currentObject.GetType().Assembly.GetTypes()) {
                typesInfo.RegisterEntity(type);
            }
            typesInfo.RegisterEntity(typeof(ReportDataV2));
        }

        #region Properties
        private IReportDataV2 ReportData(string ReportDisplayName) {
            return this._currentObject.Session.FindObject<ReportDataV2>(new BinaryOperator("DisplayName", ReportDisplayName));
        }

        private XtraReport Report(string ReportDisplayName) {
            if (ReportDataProvider.ReportsStorage.ReportDataType != null) {
                return ReportDataProvider.ReportsStorage.LoadReport(this.ReportData(ReportDisplayName));
            } else {
                IReportDataV2 reportData = ReportObjectSpaceProvider.FindObject<ReportDataV2>(new BinaryOperator("DisplayName", ReportDisplayName));
                ReportContainer container = new ReportContainer(reportData);

                return container.Report;
            }
        }
        #endregion

        public byte[] ExportObject(string KeyMemberName, string ReportDisplayName) {
            ArrayList keys = new ArrayList
            {
                _currentObject.Session.GetKeyValue(_currentObject)
            };

            using (MemoryStream stream = new MemoryStream()) {
                CriteriaOperator criteria = new InOperator(KeyMemberName, keys);
                XtraReport report = Report(ReportDisplayName);

                if (_reportDataSourceHelper != null) {
                    _reportDataSourceHelper.SetupReport(report, null, criteria, true, null, true);
                } else {
                    _reportV2DataSourceHelper.SetupReport(report, null, criteria, true, null, true);
                }

                report.ExportToPdf(stream, new PdfExportOptions { ShowPrintDialogOnOpen = false });
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ToArray();
            }
        }

        public void Dispose() {
            if (this._reportDataSourceHelper != null) {
                this._reportDataSourceHelper = null;
            }

            if (this._reportV2DataSourceHelper != null) {
                this._reportV2DataSourceHelper.Dispose();
                this._reportV2DataSourceHelper = null;
            }
        }
    }
}