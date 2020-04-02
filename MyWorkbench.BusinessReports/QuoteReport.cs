using DevExpress.XtraReports.UI;
using System;

namespace MyWorkbench.BusinessReports
{
    public partial class QuoteReport : MyWorkbench.BusinessReports.BaseReport
    {
        public QuoteReport()
        {
            InitializeComponent();
        }

        private void QuoteReport_AfterPrint(object sender, EventArgs e)
        {
            if (this.GetCurrentColumnValue("No") != null)
            {
                PrintingSystem.Document.Name = string.Concat("QUOTE - ", this.GetCurrentColumnValue("No"));
            }
        }

        private void ClientTable_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            foreach (XRTableRow row in this.ClientTable.Rows)
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

        private void ItemSummaryTableRow2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (double.Parse(AdditionalPercentage.Text) <= 0)
            {
                e.Cancel = true;
            }
        }

        private void ItemSummaryTableRow4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (double.Parse(GetCurrentColumnValue("VATTotal").ToString()) <= 0)
            {
                e.Cancel = true;
            }
        }

        private void ItemSummaryTableRow5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (double.Parse(GetCurrentColumnValue("VATTotal").ToString()) <= 0)
            {
                e.Cancel = true;
            }
        }

        private void NotesTable_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            foreach (XRTableRow row in this.NotesTable.Rows)
            {
                foreach (XRTableCell cell in row.Cells)
                {
                    if (cell.Text == "")
                    {
                        row.Visible = false;
                    }
                }
            }
        }

        private void XrTableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (string.IsNullOrEmpty(ContactName.Text) && string.IsNullOrEmpty(LocationName.Text))
            {
                e.Cancel = true;
            }
        }
    }
}
