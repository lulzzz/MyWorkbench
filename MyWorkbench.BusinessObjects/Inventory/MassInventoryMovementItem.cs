using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface.Attributes;
using MyWorkbench.BusinessObjects.Helpers;
using System;

namespace MyWorkbench.BusinessObjects.Inventory {
    [DefaultClassOptions]
    [NavigationItem(false)]
    [ImageName("Action_Debug_Stop")]
    [RuleCriteria("", DefaultContexts.Save, "Quantity > 0", "Quantity must be greater than 0", SkipNullOrEmptyValues = false)]
    public class MassInventoryMovementItem : BaseObject {
        public MassInventoryMovementItem(Session session)
            : base(session) {
        }

        // Fields...
        private double fQuantity;
        private Item fItem;
        private MassInventoryMovement fMassInventoryMovement;
        private DateTime fDateTime;

        [Association("MassInventoryMovement_MassInventoryMovementItem")]
        public MassInventoryMovement MassInventoryMovement {
            get {
                return fMassInventoryMovement;
            }
            set {
                SetPropertyValue("MassInventoryMovement", ref fMassInventoryMovement, value);
            }
        }

        [Association("Item_MassInventoryMovementItem")]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public Item Item {
            get {
                return fItem;
            }
            set {
                SetPropertyValue("Item", ref fItem, value);
            }
        }

        public double Quantity {
            get {
                return Math.Round(fQuantity, 3);
            }
            set {
                SetPropertyValue("Quantity", ref fQuantity, Math.Round(value, 3));
            }
        }

        [VisibleInListView(false),VisibleInDetailView(false)]
        [Association("MassInventoryMovementItem_InventoryTransaction"), DevExpress.Xpo.Aggregated]
        public XPCollection<InventoryTransaction> InventoryTransactions {
            get {
                return GetCollection<InventoryTransaction>("InventoryTransactions");
            }
        }

        [Index(-1)]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public DateTime DateTime {
            get {
                return fDateTime;
            }
            set {
                SetPropertyValue("DateTime", ref fDateTime, value);
            }
        }

        public override void AfterConstruction() {
            base.AfterConstruction();

            this.Quantity = 1;
            this.DateTime = Constants.DateTimeTimeZone(this.Session);
        }

        protected override void OnSaving() {
            base.OnSaving();

            if (!this.IsDeleted)
                MassInventoryMovementItemHelper.UpdateInventoryTransaction(this);
        }

        protected override void OnDeleting() {
            base.OnDeleting();

            foreach (object obj in Session.CollectReferencingObjects(this)) {
                Session.Delete(obj);
            }
        }
    }
}
