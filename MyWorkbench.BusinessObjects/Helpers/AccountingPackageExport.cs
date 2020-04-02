using DevExpress.Xpo;
using MyWorkbench.BusinessObjects.Accounts;
using MyWorkbench.BusinessObjects.Communication;
using System.Collections.Generic;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public abstract class AccountingPackageExport
    {
        public Session Session { get; set; }

        public AccountingPackageExport(Session Session)
        {
            this.Session = Session;
        }

        private List<AccountingExport> results;
        public List<AccountingExport> Results {
            get {
                if (results == null)
                    results = new List<AccountingExport>();
                return results;
            }
        }

        public abstract void ExportInvoices(IEnumerable<Invoice> Invoices);

        public abstract void ExportInvoice(Invoice Invoice);

        public void Commit() {
            foreach (AccountingExport accountingExport in Results)
            {
                accountingExport.Save();
            }

            this.Session.CommitTransaction();
        }
    }
}
