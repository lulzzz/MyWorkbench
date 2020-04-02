using DevExpress.Xpo;
using MyWorkbench.BusinessObjects.Accounts;
using System.Collections.Generic;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public class SageOneAccountingExport : AccountingPackageExport
    {
        public SageOneAccountingExport(Session Session) : base(Session) { }

        public override void ExportInvoices(IEnumerable<Invoice> Invoices)
        {
        }

        public override void ExportInvoice(Invoice Invoice)
        {
        }
    }
}
