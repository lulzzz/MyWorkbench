namespace MyWorkbench.BusinessReports
{
    public partial class TransactionReport : DevExpress.XtraReports.UI.XtraReport
    {
        public TransactionReport()
        {
            InitializeComponent();
        }

        private void TransactionReport_AfterPrint(object sender, System.EventArgs e)
        {
            PrintingSystem.Document.Name = "Accounts Aging - Debtors";
        }
    }
}
