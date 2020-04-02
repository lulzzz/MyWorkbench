namespace MyWorkbench.BusinessReports
{
    public partial class TransactionReportCreditors : DevExpress.XtraReports.UI.XtraReport
    {
        public TransactionReportCreditors()
        {
            InitializeComponent();
        }

        private void TransactionReportCreditors_AfterPrint(object sender, System.EventArgs e)
        {
            PrintingSystem.Document.Name = "Accounts Aging - Creditors";
        }
    }
}
