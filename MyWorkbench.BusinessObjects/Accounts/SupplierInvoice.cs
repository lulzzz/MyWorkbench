using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Ignyt.Framework;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Helpers;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Accounts {
    [DefaultClassOptions]
    [NavigationItem("Accounts")]
    [ImageName("BO_Resume")]
    [Appearance("HideActionDelete", AppearanceItemType = "Action", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class SupplierInvoice : WorkflowBase, IEndlessPaging, ICustomizable, ITransactionType, IDetailRowMode, IAccountingPartner, IMultiplePayments
    {
        public SupplierInvoice(Session session)
            : base(session) {
        }

        #region ITransactionType
        [VisibleInDetailView(false), VisibleInListView(false)]
        public TransactionType TransactionType {
            get {
                return TransactionType.SupplierInvoice;
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
                return this.CostTotalIncl;
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
                return string.Format("{0}{1} - {2}",Environment.NewLine, this.ReferenceNumber, this.PONumber);
            }
        }
        #endregion

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override WorkFlowType WorkFlowType {
            get {
                return WorkFlowType.SupplierInvoice;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportDisplayName {
            get {
                return "Print Supplier Invoice";
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportFileName {
            get {
                return "Supplier Invoice - " + this.No;
            }
        }

        private string fAccountingPartnerIdentifier;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string AccountingPartnerIdentifier {
            get => fAccountingPartnerIdentifier;
            set => SetPropertyValue(nameof(AccountingPartnerIdentifier), ref fAccountingPartnerIdentifier, value);
        }

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

            new WorkFlowExecute<SupplierInvoice>().Execute(this);

            base.OnSaving();
        }
        #endregion
    }
}
