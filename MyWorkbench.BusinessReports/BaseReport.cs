using DevExpress.XtraReports.UI;

namespace MyWorkbench.BusinessReports
{
    public partial class BaseReport : DevExpress.XtraReports.UI.XtraReport
    {
        public BaseReport()
        {
            InitializeComponent();
        }

        private void CompanyTableRow3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (CompanyFaxNo.Text == string.Empty)
            {
                e.Cancel = true;
            }
        }

        private void CompanyTableRow6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (CompanyVAT.Text == string.Empty)
            {
                e.Cancel = true;
            }
        }

        private void CompanyTableRow2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (CompanyPhoneNo.Text == string.Empty)
            {
                e.Cancel = true;
            }
        }

        private void CompanyTableRow4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (CompanyEmail.Text == string.Empty)
            {
                e.Cancel = true;
            }
        }

        private void CompanyTableRow5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (CompanyReg.Text == string.Empty)
            {
                e.Cancel = true;
            }
        }

        private void DocumentTable_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            foreach (XRTableRow row in this.DocumentTable.Rows)
            {
                foreach (XRTableCell cell in row.Cells)
                {
                    if (cell.Text == string.Empty)
                    {
                        row.Visible = false;
                    }
                }
            }
        }

        private void CompanyTable_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            foreach (XRTableRow row in this.CompanyTable.Rows)
            {
                foreach (XRTableCell cell in row.Cells)
                {
                    if (cell.Text == string.Empty)
                    {
                        row.Visible = false;
                    }
                }
            }
        }

        private void DocumentTable2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            foreach (XRTableRow row in this.DocumentTable.Rows)
            {
                foreach (XRTableCell cell in row.Cells)
                {
                    if (cell.Text == string.Empty)
                    {
                        row.Visible = false;
                    }
                }
            }
        }
    }
}
