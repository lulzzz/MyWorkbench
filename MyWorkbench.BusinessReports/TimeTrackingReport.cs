using DevExpress.XtraReports.UI;
using System;

namespace MyWorkbench.BusinessReports
{
    public partial class TimeTrackingReport : DevExpress.XtraReports.UI.XtraReport
    {
        public TimeTrackingReport()
        {
            InitializeComponent();
        }

        private void TimeTrackingReport_AfterPrint(object sender, EventArgs e)
        {
            this.PrintingSystem.Document.Name = "Time Tracking Report";
        }

        private void TimeTrackingReport_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            XtraReport report = sender as XtraReport;

            report.Parameters["StartDate"].Value = DateTime.Today.AddDays(-365);
            report.Parameters["EndDate"].Value = DateTime.Today.AddDays(1);
        }

        private void TimeTrackingReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XtraReport report = sender as XtraReport;

            if (report.Parameters["StartDate"].RawValue == null)
                report.Parameters["StartDate"].Value = DateTime.Today.AddDays(-90);
            if (report.Parameters["EndDate"].RawValue == null)
                report.Parameters["EndDate"].Value = DateTime.Today.AddDays(1);
        }
    }
}
