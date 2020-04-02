using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Ignyt.Framework;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Accounts;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Helpers;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.Linq;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Inventory {
    [DefaultClassOptions]
    [NavigationItem("Accounts")]
    [ImageName("BO_Resume")]
    [Appearance("HideActionDelete", AppearanceItemType = "Action", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class Purchase : WorkflowBase, IEndlessPaging, ICustomizable, ITransactionType, IDetailRowMode, IAccountingPartner, IMultiplePayments
    {
        public Purchase(Session session)
            : base(session) {
        }

        #region ITransactionType
        [VisibleInDetailView(false), VisibleInListView(false)]
        public TransactionType TransactionType {
            get {
                return TransactionType.Purchase;
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

        [VisibleInDetailView(false), VisibleInListView(true)]
        [ModelDefault("Index", "10")]
        public bool Delivered {
            get {
                if (this.Items != null)
                    if (this.Items.Where(g => g.Delivered == false).Count() == 0) {
                        return true;
                    } else return false;
                else return false;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override WorkFlowType WorkFlowType {
            get {
                return WorkFlowType.Purchase;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportDisplayName {
            get {
                return "Print Purchase";
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportFileName {
            get {
                return "Purchase - " + this.No;
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

            new WorkFlowExecute<Purchase>().Execute(this);

            base.OnSaving();
        }
        #endregion
    }
}
