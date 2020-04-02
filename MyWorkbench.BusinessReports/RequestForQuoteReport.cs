using DevExpress.XtraReports.UI;
using System;

namespace MyWorkbench.BusinessReports
{
    public partial class RequestForQuoteReport : MyWorkbench.BusinessReports.BaseReport
    {
        public RequestForQuoteReport()
        {
            InitializeComponent();
        }

        private void RequestForQuote_AfterPrint(object sender, EventArgs e)
        {
            if (this.GetCurrentColumnValue("No") != null)
            {
                PrintingSystem.Document.Name = string.Concat("QUOTE REQUEST - ", this.GetCurrentColumnValue("No"));
            }
        }

        private void NotesTable_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            foreach (XRTableRow row in this.NotesTable.Rows)
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
