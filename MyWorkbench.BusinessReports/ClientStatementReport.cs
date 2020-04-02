using System;
using DevExpress.XtraReports.UI;

namespace MyWorkbench.BusinessReports
{
    public partial class ClientStatementReport : DevExpress.XtraReports.UI.XtraReport
    {
        public ClientStatementReport()
        {
            InitializeComponent();
        }

        private void ClientStatementReport_AfterPrint(object sender, EventArgs e)
        {
            if (this.GetCurrentColumnValue("FullName") != null)
            {
                PrintingSystem.Document.Name = string.Concat("STATEMENT - ", this.GetCurrentColumnValue("FullName"));
            }
        }

        private void ClientStatementReport_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            XtraReport report = sender as XtraReport;
            report.Parameters["StartDate"].Value = DateTime.Today.AddDays(-365);
            report.Parameters["EndDate"].Value = DateTime.Today.AddDays(1);
        }

        private void ClientStatementReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XtraReport report = sender as XtraReport;
            if (report.Parameters["StartDate"].RawValue == null)
                report.Parameters["StartDate"].Value = DateTime.Today.AddDays(-90);
            if (report.Parameters["EndDate"].RawValue == null)
                report.Parameters["EndDate"].Value = DateTime.Today.AddDays(1);
        }

        private void FullName_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (FullName.Text == string.Empty)
            {
                e.Cancel = true;
            }
        }

        private void Phone_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Phone.Text == string.Empty)
            {
                e.Cancel = true;
            }
        }

        private void Email_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Email.Text == string.Empty)
            {
                e.Cancel = true;
            }
        }

        private void FullAddress_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (FullAddress.Text == string.Empty)
            {
                e.Cancel = true;
            }
        }
    }
}
