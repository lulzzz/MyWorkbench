using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.ComponentModel;
using System.Linq;

namespace MyWorkbench.BusinessObjects.Accounts
{
    [DomainComponent, DefaultClassOptions]
    [NonPersistent]
    [DefaultListViewOptions(true, NewItemRowPosition.Top)]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    [Appearance("HideActions", AppearanceItemType = "Action", TargetItems = "Edit;SwitchToEditMode;Export", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class MultiplePayment : BaseObject
    {
        public MultiplePayment(Session session) : base(session) {}

        public PaymentType PaymentType { get; set; }

        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        public double PaymentAmount { get; set; }

        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        public DateTime PaymentDate { get; set; }

        public double UnallocatedPayments {
            get {
                return this.Vendor.Transactions.Where(g => g.Payment != null && g.Payment.Workflow == null).Sum(g => g.Credit);
            }
            set {

            }
        }

        [VisibleInDetailView(false)]
        public WorkflowBase Workflow { get; set; }

        [VisibleInDetailView(false)]
        public Vendor Vendor { get; set; }
    }

    [DomainComponent]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    [Appearance("HideActions", AppearanceItemType = "Action", TargetItems = "Edit;SwitchToEditMode;Export", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class MultiplePaymentList : BaseObject
    {
        private readonly BindingList<MultiplePayment> multiplePayments;
        public BindingList<MultiplePayment> MultiplePayments { get { return multiplePayments; } }

        public MultiplePaymentList(Session session) : base(session) {
            multiplePayments = new BindingList<MultiplePayment>();
        }
    }
}
