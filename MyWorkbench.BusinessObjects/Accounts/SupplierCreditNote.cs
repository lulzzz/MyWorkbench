using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
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
    public class SupplierCreditNote : WorkflowBase, ICustomizable, ITransactionType, IDetailRowMode, IAccountingPartner
    {
        public SupplierCreditNote(Session session)
            : base(session) {
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override WorkFlowType WorkFlowType {
            get {
                return WorkFlowType.SupplierCreditNote;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportDisplayName {
            get {
                return "Print Credit Note";
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportFileName {
            get {
                return "Credit - " + this.No;
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
                return TransactionType.CreditNote;
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

        #region Property Overrides
        //[VisibleInDetailView(false), VisibleInListView(true)]
        //[ModelDefault("Index", "1")]
        //[Copy(false)]
        //public new string No {
        //    get { return base.No; }
        //    set { base.No = value; }
        //}

        //[VisibleInDetailView(true), VisibleInListView(true)]
        //[XafDisplayName("Client")]
        //[ModelDefault("Index", "2")]
        //[ImmediatePostData(true)]
        //public new Vendor Vendor {
        //    get { return base.Vendor; }
        //    set { base.Vendor = value; }
        //}

        //[VisibleInDetailView(true), VisibleInListView(false)]
        //[XafDisplayName("Contact")]
        //public new VendorContact VendorContact {
        //    get { return base.VendorContact; }
        //    set { base.VendorContact = value; }
        //}

        //[VisibleInDetailView(true), VisibleInListView(true)]
        //[ModelDefault("Index", "7")]
        //public new double TotalIncl {
        //    get { return base.TotalIncl; }
        //    set { base.TotalIncl = value; }
        //}

        //[VisibleInDetailView(true), VisibleInListView(true)]
        //[ModelDefault("Index", "3")]
        //[Copy(false)]
        //public new DateTime Issued {
        //    get { return base.Issued; }
        //    set { base.Issued = value; }
        //}

        //[VisibleInDetailView(true), VisibleInListView(true)]
        //[ModelDefault("Index", "4")]
        //[XafDisplayName("Reference")]
        //public new string ReferenceNumber {
        //    get { return base.ReferenceNumber; }
        //    set { base.ReferenceNumber = value; }
        //}

        //[VisibleInDetailView(true), VisibleInListView(true)]
        //[ModelDefault("Index", "5")]
        //[Copy(false)]
        //public new Priority Priority {
        //    get { return base.Priority; }
        //    set { base.Priority = value; }
        //}

        //[VisibleInDetailView(true), VisibleInListView(true)]
        //[ModelDefault("Index", "6")]
        //[Copy(false)]
        //public new Status Status {
        //    get { return base.Status; }
        //    set { base.Status = value; }
        //}

        //[VisibleInDetailView(true), VisibleInListView(false)]
        //public new Location Location {
        //    get { return base.Location; }
        //    set { base.Location = value; }
        //}

        //[VisibleInDetailView(true), VisibleInListView(false)]
        //public new WorkflowType Type {
        //    get { return base.Type; }
        //    set { base.Type = value; }
        //}
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

            new WorkFlowExecute<SupplierCreditNote>().Execute(this);

            base.OnSaving();
        }
        #endregion
    }
}
