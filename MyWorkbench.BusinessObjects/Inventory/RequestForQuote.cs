using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Helpers;
using MyWorkbench.BusinessObjects.Lookups;
using System;

namespace MyWorkbench.BusinessObjects {
    [DefaultClassOptions]
    [NavigationItem("Accounts")]
    [ImageName("BO_Resume")]
    [Appearance("HideActionDelete", AppearanceItemType = "Action", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class RequestForQuote : WorkflowBase, IEndlessPaging, ICustomizable, IDetailRowMode
    {
        public RequestForQuote(Session session)
            : base(session) {
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override WorkFlowType WorkFlowType {
            get {
                return WorkFlowType.RequestForQuote;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string FullDescription {
            get {
                string description = string.Empty;

                foreach (Vendor vendor in this.Suppliers)
                {
                    if (description != string.Empty)
                        description += ", ";

                    description += vendor.FullName;
                }

                return description;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportDisplayName {
            get {
                return "Print Request For Quote";
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public override string ReportFileName {
            get {
                return "Request - " + this.No;
            }
        }

        [Association("Vendor_RequestForQuote")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [EditorAlias("TokenCollectionEditor")]
        [ModelDefault("Index", "2")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XPCollection<Vendor> Suppliers {
            get {
                return GetCollection<Vendor>("Suppliers");
            }
        }

        protected override void OnSaved()
        {
            new WorkFlowExecute<RequestForQuote>().Execute(this);

            base.OnSaved();
        }
    }
}
