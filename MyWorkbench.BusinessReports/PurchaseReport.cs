﻿using System;
using DevExpress.XtraReports.UI;

namespace MyWorkbench.BusinessReports {
    public partial class PurchaseReport : BaseReport {
        public PurchaseReport() {
            InitializeComponent();
        }

        private void ClientTable_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            foreach (XRTableRow row in this.ClientTable.Rows) {
                foreach (XRTableCell cell in row.Cells) {
                    if (cell.Text == string.Empty) {
                        row.Visible = false;
                    }
                }
            }
        }

        private void AdditionalTable_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            foreach (XRTableRow row in this.AdditionalTable.Rows) {
                foreach (XRTableCell cell in row.Cells) {
                    if (cell.Text == string.Empty) {
                        row.Visible = false;
                    }
                }
            }
        }

        private void PurchaseReport_AfterPrint(object sender, EventArgs e) {
            if (this.GetCurrentColumnValue("No") != null) {
                PrintingSystem.Document.Name = string.Concat("PURCHASE - ", this.GetCurrentColumnValue("No"));
            }
        }

        private void ItemSummaryTableRowVAT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            if (double.Parse(GetCurrentColumnValue("CostVATTotal").ToString()) <= 0) {
                e.Cancel = true;
            }
        }

        private void ItemSummaryTableRowTotalIncl_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            if (double.Parse(GetCurrentColumnValue("CostVATTotal").ToString()) <= 0) {
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