using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Ignyt.Framework;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.ComponentModel;
using Ignyt.BusinessInterface.Attributes;

namespace MyWorkbench.BusinessObjects.Accounts {
    [DefaultClassOptions, DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class Transaction : BaseObject, ITransaction {
        public Transaction(Session session)
            : base(session) {
        }

        #region Properties
        private Vendor fVendor;
        [Association("Vendor_Transaction")]
        public Vendor Vendor {
            get {
                return fVendor;
            }
            set {
                SetPropertyValue("Vendor", ref fVendor, value);
            }
        }

        private WorkflowBase fWorkflow;
        [VisibleInDetailView(false), VisibleInListView(false)]
        [NonCloneable]
        public WorkflowBase Workflow {
            get {
                return fWorkflow;
            }
            set {
                if (fWorkflow == value)
                    return;

                WorkflowBase prevWorkFlow = fWorkflow;
                fWorkflow = value;

                if (IsLoading) return;

                if (prevWorkFlow != null && prevWorkFlow.Transaction == this)
                    prevWorkFlow.Transaction = null;

                if (fWorkflow != null)
                    fWorkflow.Transaction = this;

                OnChanged("Workflow");
            }
        }

        private double fDebit;
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        public double Debit {
            get => Math.Round(fDebit, 2);
            set => SetPropertyValue(nameof(Debit), ref fDebit, Math.Round(value, 2));
        }

        private double fCredit;
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        public double Credit {
            get => Math.Round(fCredit, 2);
            set => SetPropertyValue(nameof(Credit), ref fCredit, Math.Round(value, 2));
        }

        private DateTime fTransactionDate;
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public DateTime TransactionDate {
            get => fTransactionDate;
            set => SetPropertyValue(nameof(TransactionDate), ref fTransactionDate, value);
        }

        private string fAdditionalDescription;
        public string AdditionalDescription {
            get => fAdditionalDescription;
            set => SetPropertyValue(nameof(AdditionalDescription), ref fAdditionalDescription, value);
        }

        private string fDescription;
        public string Description {
            get {
                if(this.Payment != null && this.Payment.Workflow != null)
                    fDescription = string.Format("{0} {1} {2} {3}", "Payment for ", this.Payment.Workflow.WorkFlowType.ToString().CamelCaseToWords(), this.Payment.Workflow.No,this.AdditionalDescription);
                else if (this.Workflow != null)
                    fDescription = string.Format("{0} {1} {2}", this.Workflow.WorkFlowType.ToString().CamelCaseToWords(), this.Workflow.No, this.AdditionalDescription);
                else
                    fDescription = "Unallocated Payment";

                return fDescription;
            }
        }

        #region Aging
        [VisibleInListView(false), VisibleInDetailView(false)]
        public DateTime AgingActualdate {
            get {
                if (this.Vendor.AccountType == VendorType.Client)
                {
                    if (this.Credit > 0 && this.Payment != null && this.Payment.Workflow != null)
                        return this.Payment.Workflow.Issued;
                    else if (this.Credit > 0 && this.Workflow != null)
                        return this.Workflow.Issued;
                    else
                        return this.TransactionDate;
                }
                else {
                    if (this.Debit > 0 && this.Payment != null && this.Payment.Workflow != null)
                        return this.Payment.Workflow.Issued;
                    else if (this.Debit > 0 && this.Workflow != null)
                        return this.Workflow.Issued;
                    else
                        return this.TransactionDate;
                }
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public bool Current {
            get {
                if (this.AgingActualdate > Constants.DateTimeTimeZone(this.Session).Date.AddDays(-30))
                    return true;
                else
                    return false;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public bool ThirtyDays {
            get {
                if (this.AgingActualdate > Constants.DateTimeTimeZone(this.Session).Date.AddDays(-61) && this.AgingActualdate <= Constants.DateTimeTimeZone(this.Session).Date.AddDays(-30))
                    return true;
                else
                    return false;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public bool SixtyDays {
            get {
                if (this.AgingActualdate > Constants.DateTimeTimeZone(this.Session).Date.AddDays(-91) && this.AgingActualdate <= Constants.DateTimeTimeZone(this.Session).Date.AddDays(-60))
                    return true;
                else
                    return false;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public bool NinetyDays {
            get {
                if (this.AgingActualdate <= Constants.DateTimeTimeZone(this.Session).Date.AddDays(-90))
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #endregion

        #region Links
        private WorkFlowPayment fPayment;
        public WorkFlowPayment Payment {
            get {
                return fPayment;
            }
            set {
                if (fPayment == value)
                    return;

                WorkFlowPayment prevPayment = fPayment;
                fPayment = value;

                if (IsLoading) return;

                if (prevPayment != null && prevPayment.Transaction == this)
                    prevPayment.Transaction = null;

                if (fPayment != null)
                    fPayment.Transaction = this;
                OnChanged("Payment");
            }
        }
        #endregion

        #region ITransaction
        [NonPersistent]
        [VisibleInDetailView(false),VisibleInListView(false)]
        public IVendor IVendor {
            get {
                return this.Vendor;
            }
            set {
                this.Vendor = value as Vendor;
            }
        }
        #endregion
    }
}
