using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.Framework;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Inventory;
using MyWorkbench.Framework;
using System;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem("Inventory")]
    [ImageName("Action_Debug_Stop")]
    public class InventoryLocation : BaseObject, IInventoryLocation {
        public InventoryLocation(Session session) : base(session) {
        }

        private string fDescription;
        [Indexed("GCRecord", Unique = true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Description", ref fDescription, value);
            }
        }

        private Party fParty;
        [DevExpress.Xpo.DisplayName("Created By")]
        [VisibleInDetailView(false), VisibleInListView(true)]
        public Party Party {
            get => fParty;
            set => SetPropertyValue(nameof(Party), ref fParty, value);
        }

        private bool fDefaultLocation;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public bool DefaultLocation {
            get {
                return fDefaultLocation;
            }
            set {
                SetPropertyValue("DefaultLocation", ref fDefaultLocation, value);
            }
        }

        [Association("InventoryLocation_WorkflowItem")]
        [VisibleInDetailView(false),VisibleInListView(false)]
        public XPCollection<WorkflowItem> WorkflowItems {
            get {
                return GetCollection<WorkflowItem>("WorkflowItems");
            }
        }

        [Association("InventoryLocation_InventoryTransaction")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<InventoryTransaction> InventoryTransactions {
            get {
                return GetCollection<InventoryTransaction>("InventoryTransactions");
            }
        }

        protected override void OnDeleting() {
            if (this.DefaultLocation == false) base.OnDeleting();
            else {
                MessageProvider.RegisterMessage(new MessageInformation(MessageTypes.Warning, "You cannot delete the default location"));
            }
        }

        public override void AfterConstruction() {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                if (SecuritySystem.CurrentUserId != null && SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);
            }
        }
    }
}
