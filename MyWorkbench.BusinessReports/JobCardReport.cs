using System;
using DevExpress.XtraReports.UI;

namespace MyWorkbench.BusinessReports
{
    public partial class JobCardReport : BaseReport
    {
        public JobCardReport()
        {
            InitializeComponent();
        }

        private void ClientTableRow3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (string.IsNullOrEmpty(ClientPhone.Text))
            {
                e.Cancel = true;
            }
        }

        private void ClientTableRow4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (string.IsNullOrEmpty(ClientCell.Text))
            {
                e.Cancel = true;
            }
        }

        private void ClientTableRow7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (string.IsNullOrEmpty(ClientVATNo.Text))
            {
                e.Cancel = true;
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

        private void JobCardReport_AfterPrint(object sender, EventArgs e)
        {
            if (this.GetCurrentColumnValue("No") != null)
            {
                PrintingSystem.Document.Name = string.Concat("JOBCARD - ", this.GetCurrentColumnValue("No"));
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

