using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Accounts {
    public enum BankingType {
        Account = 0, Customer = 1, Supplier = 2, Transfer = 3, VAT = 4
    }

    [DefaultClassOptions, DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class Banking : BaseObject, IModal
    {
        public Banking(Session session)
            : base(session) {
        }

        private BankingAccount fBankingAccount;
        [Association("BankingAccount_Banking")]
        public BankingAccount BankingAccount {
            get { return fBankingAccount; }
            set {
                SetPropertyValue("BankingAccount", ref fBankingAccount, value);
            }
        }

        private DateTime fDateTime;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        public DateTime DateTime {
            get => fDateTime;
            set => SetPropertyValue(nameof(DateTime), ref fDateTime, value);
        }

        private string fPayee;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        public string Payee {
            get => fPayee;
            set => SetPropertyValue(nameof(Payee), ref fPayee, value);
        }

        private string fDescription;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        private BankingType fType;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        public BankingType Type {
            get => fType;
            set => SetPropertyValue(nameof(Type), ref fType, value);
        }

        private Account fSelection;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        [ImmediatePostData]
        public Account Selection {
            get {
                return fSelection;
            }
            set {
                SetPropertyValue(nameof(Selection), ref fSelection, value);

                if (!IsLoading && !IsSaving)
                {
                    if (this.Selection != null)
                        this.VATType = this.Selection.VATType;
                }
            }
        }

        private string fReference;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        public string Reference {
            get => fReference;
            set => SetPropertyValue(nameof(Reference), ref fReference, value);
        }

        private VATType fVATType;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        public VATType VATType {
            get => fVATType;
            set => SetPropertyValue(nameof(VATType), ref fVATType, value);
        }

        private double fSpent;
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [NonCloneable]
        public double Spent {
            get => fSpent;
            set => SetPropertyValue(nameof(Spent), ref fSpent, value);
        }

        private double fReceived;
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [NonCloneable]
        public double Received {
            get => fReceived;
            set => SetPropertyValue(nameof(Received), ref fReceived, value);
        }

        private bool fReconciled;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [NonCloneable]
        public bool Reconciled {
            get => fReconciled;
            set => SetPropertyValue(nameof(Reconciled), ref fReconciled, value);
        }

        private bool fNew;
        [VisibleInListView(false), VisibleInDetailView(false)]
        [NonCloneable]
        public bool New {
            get => fNew;
            set => SetPropertyValue(nameof(New), ref fNew, value);
        }

        private bool fReviewed;
        [VisibleInListView(false), VisibleInDetailView(false)]
        [NonCloneable]
        public bool Reviewed {
            get => fReviewed;
            set => SetPropertyValue(nameof(Reviewed), ref fReviewed, value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                this.Reconciled = true;
                this.New = true;
                this.Reviewed = false;
            }
        }
    }
}
