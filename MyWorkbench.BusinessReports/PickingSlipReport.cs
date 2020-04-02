using System;
using DevExpress.XtraReports.UI;

namespace MyWorkbench.BusinessReports
{
    public partial class PickingSlipReport : BaseReport
    {
        public PickingSlipReport()
        {
            InitializeComponent();
        }

        private void AdditionalTable_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            foreach (XRTableRow row in this.AdditionalTable.Rows)
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

        private void PickingSlipReport_AfterPrint(object sender, EventArgs e)
        {
            if (this.GetCurrentColumnValue("No") != null)
            {
                PrintingSystem.Document.Name = string.Concat("PICKING - ", this.GetCurrentColumnValue("No"));
            }
        }
    }
}
