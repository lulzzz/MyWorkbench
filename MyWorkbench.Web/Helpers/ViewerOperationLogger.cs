using DevExpress.XtraReports.Web.WebDocumentViewer;
using DevExpress.XtraReports.UI;
using System;

namespace MyWorkbench.Web.Helpers
{
    public class ViewerOperationLogger : WebDocumentViewerOperationLogger
    {
        public override Action BuildStarting(string reportId, XtraReport report, ReportBuildProperties buildProperties)
        {
            report.BeforePrint += Report_BeforePrint;
            report.CreateDocument();
            return null;
        }

        private void Report_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }
    }
}