using System;
using DevExpress.XtraReports.UI;

namespace MyWorkbench.BusinessReports
{
    public partial class InvoiceReport : BaseReport
    {
        public InvoiceReport()
        {
            InitializeComponent();
        }

        private void ItemSummaryTableRow8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (double.Parse(GetCurrentColumnValue("DepositAmount").ToString()) <= 0 || Double.Parse(GetCurrentColumnValue("AmountOutstanding").ToString()) <= Double.Parse(GetCurrentColumnValue("TotalIncl").ToString()) - Double.Parse(GetCurrentColumnValue("DepositAmount").ToString()))
            {
                e.Cancel = true;
            }
        }

        private void LblPaid_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (double.Parse(GetCurrentColumnValue("AmountOutstanding").ToString()) > 0)
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

        private void InvoiceReport_AfterPrint(object sender, EventArgs e)
        {
            if (this.GetCurrentColumnValue("No") != null)
            {
                PrintingSystem.Document.Name = string.Concat("INVOICE - ", this.GetCurrentColumnValue("No"));
            }
        }

        private void ItemSummaryTableRowVAT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (double.Parse(GetCurrentColumnValue("VATTotal").ToString()) <= 0)
            {
                e.Cancel = true;
            }
        }

        private void ItemSummaryTableRowDiscount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (double.Parse(DiscountPercent.Text) <= 0)
            {
                e.Cancel = true;
            }
        }

        private void ItemSummaryTableRowAdditional_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (double.Parse(AdditionalPercentage.Text) <= 0)
            {
                e.Cancel = true;
            }
        }

        private void ItemSummaryTableRowTotalIncl_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (double.Parse(GetCurrentColumnValue("VATTotal").ToString()) <= 0)
            {
                e.Cancel = true;
            }
        }

        private void ItemSummaryTableRowExcess_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Double.Parse(GetCurrentColumnValue("Excess").ToString()) <= 0)
            {
                e.Cancel = true;
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
