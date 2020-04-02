using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Attributes;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Inventory {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem("Inventory")]
    [ImageName("Action_Debug_Stop")]
    public class InventoryTransaction : BaseObject, IEndlessPaging, IInventoryTransactionRecord
    {
        public InventoryTransaction(Session session)
            : base(session) {
        }

        #region Properties 
        private Item fItem;
        [Association("Item_InventoryTransaction")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "1")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Item Item {
            get {
                return fItem;
            }
            set {
                SetPropertyValue("Item", ref fItem, value);
            }
        }

        [Association("ParentItem_InventoryTransaction")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public XPCollection<InventoryTransaction> ParentItems {
            get { return GetCollection<InventoryTransaction>("ParentItems"); }
        }

        private double fInventoryIn;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "2")]
        public double InventoryIn {
            get {
                return Math.Round(fInventoryIn, 3);
            }
            set {
                SetPropertyValue("InventoryIn", ref fInventoryIn, Math.Round(value, 3));
            }
        }

        private double fInventoryOut;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "3")]
        public double InventoryOut {
            get {
                return Math.Round(fInventoryOut, 3);
            }
            set {
                SetPropertyValue("InventoryOut", ref fInventoryOut, Math.Round(value, 3));
            }
        }

        [PersistentAlias("InventoryIn - InventoryOut")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public double AbsoluteQuantity {
            get {
                object tempObject = EvaluateAlias("AbsoluteQuantity");
                if (tempObject != null) {
                    return Math.Round((double)tempObject, 3);
                } else {
                    return 0;
                }
            }
        }

        private DateTime fTransactionDate;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "4")]
        [RuleRequiredField(DefaultContexts.Save)]
        [EditorAlias("CustomDateTimeEditor")]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public DateTime TransactionDate {
            get {
                return fTransactionDate;
            }
            set {
                SetPropertyValue("TransactionDate", ref fTransactionDate, value);
            }
        }

        private InventoryLocation fInventoryLocation;
        [Association("InventoryLocation_InventoryTransaction")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "5")]
        [RuleRequiredField(DefaultContexts.Save)]
        public InventoryLocation InventoryLocation {
            get {
                return fInventoryLocation;
            }
            set {
                SetPropertyValue("InventoryLocation", ref fInventoryLocation, value);
            }
        }

        private Party fParty;
        [VisibleInListView(false), VisibleInDetailView(true)]
        public Party Party {
            get {
                return fParty;
            }
            set {
                SetPropertyValue("Party", ref fParty, value);
            }
        }

        private Nullable<bool> fDelivered;
        [VisibleInListView(false), VisibleInDetailView(true)]
        public Nullable<bool> Delivered {
            get {
                return fDelivered;
            }
            set {
                SetPropertyValue("Delivered", ref fDelivered, value);
            }
        }

        private string fDescription;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "6")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Description", ref fDescription, value);
            }
        }
        #endregion

        #region Collections
        MassInventoryMovementItem massInventoryMovementItem;
        [Association("MassInventoryMovementItem_InventoryTransaction")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public MassInventoryMovementItem MassInventoryMovementItem {
            get {
                return massInventoryMovementItem;
            }
            set {
                SetPropertyValue("MassInventoryMovementItem", ref massInventoryMovementItem, value);
            }
        }

        [Association("ParentItem_InventoryTransaction")]
        public XPCollection<InventoryTransaction> ChildItems {
            get { return GetCollection<InventoryTransaction>("ChildItems"); }
        }
        #endregion

        public override void AfterConstruction() {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);

                this.TransactionDate = Constants.DateTimeTimeZone(this.Session);
            }
        }
    }
}
