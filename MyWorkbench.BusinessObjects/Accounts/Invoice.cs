using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Ignyt.Framework;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Helpers;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Accounts {
    [DefaultClassOptions]
    [NavigationItem("Accounts")]
    [ImageName("BO_Resume")]
    [Appearance("HideActionDelete", AppearanceItemType = "Action", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class Invoice : WorkflowBase, ICustomizable, ITransactionType, IDetailRowMode, IAccountingPartner, IMultiplePayments
    {
        public Invoice(Session session)
            : base(session) {
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override WorkFlowType WorkFlowType {
            get {
                return WorkFlowType.Invoice;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportDisplayName {
            get {
                return "Print Invoice";
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportFileName {
            get {
                return "Invoice - " + this.No;
            }
        }

        private string fAccountingPartnerIdentifier;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string AccountingPartnerIdentifier {
            get => fAccountingPartnerIdentifier;
            set => SetPropertyValue(nameof(AccountingPartnerIdentifier), ref fAccountingPartnerIdentifier, value);
        }

        #region ITransactionType
        [VisibleInDetailView(false), VisibleInListView(false)]
        public TransactionType TransactionType {
            get {
                return TransactionType.Invoice;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public DateTime TransactionDate {
            get {
                return this.Issued;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public double Amount {
            get {
                return this.TotalIncl;
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IVendor IVendor {
            get {
                return this.Vendor;
            }
            set {
                this.Vendor = value as Vendor;
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IWorkflow IWorkflow {
            get {
                return this;
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false)]
        [NonCloneable]
        public ITransaction ITransaction {
            get {
                return this.Transaction;
            }
            set {
                this.Transaction = value as Transaction;
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string AdditionalDescription {
            get {
                return this.ReferenceNumber;
            }
        }
        #endregion

        #region Collections
        [NonCloneable]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<AccountingExport> AccountingExport {
            get {
                SortingCollection sortCollection = new SortingCollection
                {
                    new SortProperty("Created", SortingDirection.Descending)
                };

                XPCollection<AccountingExport> accountingExport = new XPCollection<AccountingExport>(this.Session, CriteriaOperator.Parse("ObjectOid = ?", this.Oid))
                {
                    Sorting = sortCollection
                };

                return accountingExport;
            }
        }
        #endregion

        #region Events
        protected override void OnLoaded()
        {
            base.OnLoaded();
            Items.ListChanged -= ItemsOnListChanged;
            Items.ListChanged += ItemsOnListChanged;
        }

        void ItemsOnListChanged(object sender, ListChangedEventArgs listChangedEventArgs)
        {
            if (!this.Session.IsNewObject(this))
                TransactionTypeHelper.Instance.SaveTransaction(this);
        }

        protected override void OnSaved()
        {
            if (!this.IsDeleted)
            {
                TransactionTypeHelper.Instance.SaveTransaction(this);
            }
            else
            {
                TransactionTypeHelper.Instance.DeleteTransaction(this);
            }

            new WorkFlowExecute<Invoice>().Execute(this);

            base.OnSaving();
        }
        #endregion
    }
}
