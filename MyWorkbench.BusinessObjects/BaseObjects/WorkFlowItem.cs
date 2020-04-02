using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.Framework;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Attributes;
using MyWorkbench.BusinessObjects.Inventory;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.ComponentModel;
using MyWorkbench.BusinessObjects.Helpers;
using System.Collections.Generic;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions]
    [DefaultProperty("FullDescription")]
    [ImageName("BO_Resume")]
    [NavigationItem(false)]
    [Appearance("Color", AppearanceItemType = "ViewItem", TargetItems = "*", Context = "ListView", Criteria = "Delivered", FontColor = "Green", FontStyle = System.Drawing.FontStyle.Strikeout)]
    public class WorkflowItem : BaseObject, IInlineEdit, INestedWorkflowItem<WorkflowItem, WorkflowBase, InventoryLocation, Item>, 
        IInventoryTransaction, IAccountingPartner, IModal
    {
        public WorkflowItem(Session session)
            : base(session) {
        }

        #region Properties
        private WorkflowBase fWorkflow;
        [Association("WorkflowBase_WorkflowItem")]
        public WorkflowBase Workflow {
            get { return fWorkflow; }
            set {
                SetPropertyValue("Workflow", ref fWorkflow, value);
            }
        }

        private InventoryLocation fInventoryLocation;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [XafDisplayName("Location")]
        [Association("InventoryLocation_WorkflowItem")]
        [RuleRequiredField(DefaultContexts.Save)]
        public InventoryLocation InventoryLocation {
            get {
                return fInventoryLocation;
            }
            set {
                SetPropertyValue("InventoryLocation", ref fInventoryLocation, value);
            }
        }

        private Item fItem;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [ImmediatePostData]
        [Association("Item_WorkflowItem")]
        [RuleRequiredField(DefaultContexts.Save)]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        public Item Item {
            get {
                return fItem;
            }
            set {
                SetPropertyValue("Item", ref fItem, value);

                if(fItem != null && !IsLoading) {
                    this.Description = fItem.Description;
                    this.CostPrice = fItem.CostPrice;
                    this.SellingPrice = fItem.SellingPrice;
                    this.VAT = fItem.VAT;
                    this.Quantity = fItem.DefaultQuantity == 0 ? 1 : fItem.DefaultQuantity;
                    this.VatPercentage = fItem.VAT ? Constants.VatPercentage(this.Session) : 0;
                }
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string FullDescription {
            get {
                return this.Description;
            }
        }

        private string fDescription;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(true),VisibleInDetailView(true)]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Description", ref fDescription, value);
            }
        }

        private double fQuantity;
        [ModelDefault("DisplayFormat", "{0:N3}")]
        [ModelDefault("EditMask", "{0:N3}")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [RuleValueComparison("", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0, CustomMessageTemplate = RegularExpressions.ValueError)]
        public double Quantity {
            get {
                return Math.Round(fQuantity, 3);
            }
            set {
                SetPropertyValue("Quantity", ref fQuantity, Math.Round(value, 3));
            }
        }

        private double fCostPrice;
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("CostPriceDisable", Enabled = false, Criteria = "IsParent", Context = "Any")]
        [ImmediatePostData]
        [RuleValueComparison("", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0, CustomMessageTemplate = RegularExpressions.ValueError)]
        public double CostPrice {
            get {
                return Math.Round(fCostPrice, 2);
            }
            set {
                SetPropertyValue("CostPrice", ref fCostPrice, Math.Round(value, 2));
            }
        }

        private double fSellingPrice;
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [RuleValueComparison("", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0, CustomMessageTemplate = RegularExpressions.ValueError)]
        public double SellingPrice {
            get {
                return Math.Round(fSellingPrice, 2);
            }
            set {
                SetPropertyValue("SellingPrice", ref fSellingPrice, Math.Round(value, 2));
            }
        }

        private bool fVAT;
        [VisibleInListView(true), VisibleInDetailView(true)]
        public bool VAT {
            get {
                return fVAT;
            }
            set {
                if(!IsLoading)
                    this.VatPercentage = value ? Constants.VatPercentage(this.Session) : 0;

                SetPropertyValue("VAT", ref fVAT, value);
            }
        }

        private double fVatPercentage;
        [VisibleInDetailView(false), VisibleInListView(false)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        public double VatPercentage {
            get {
                return Math.Round(fVatPercentage, 2);
            }
            set {
                SetPropertyValue("VatPercentage", ref fVatPercentage, Math.Round(value, 2));
            }
        }

        [PersistentAlias("CostPrice * Quantity")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double TotalCostExcl {
            get {
                if (!IsInvalidated) {
                    object tempObject = EvaluateAlias("TotalCostExcl");

                    if (tempObject != null) {
                        return Math.Round((double)tempObject, 2);
                    } else {
                        return 0;
                    }

                } else {
                    return 0;
                }
            }
        }

        [PersistentAlias("SellingPrice * Quantity")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [VisibleInDetailView(false), VisibleInListView(true)]
        public double TotalExcl {
            get {
                if (!IsInvalidated) {
                    object tempObject = EvaluateAlias("TotalExcl");
                    if (tempObject != null) {
                        return Math.Round((double)tempObject, 2);
                    } else {
                        return 0;
                    }
                } else return 0;
            }
        }

        [PersistentAlias("CostPrice * Quantity + CostVATTotal")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [Appearance("TotalCostInclDisabled", Enabled = false, Context = "DetailView")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double TotalCostIncl {
            get {
                if (!IsInvalidated)
                {
                    object tempObject = EvaluateAlias("TotalCostIncl");
                    if (tempObject != null)
                    {
                        return Math.Round((double)tempObject, 2);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else return 0;
            }
        }

        [PersistentAlias("SellingPrice * Quantity + VATTotal")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [Appearance("TotalInclDisabled", Enabled = false, Context = "DetailView")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double TotalIncl {
            get {
                if (!IsInvalidated) {
                    object tempObject = EvaluateAlias("TotalIncl");
                    if (tempObject != null) {
                        return Math.Round((double)tempObject, 2);
                    } else {
                        return 0;
                    }
                } else return 0;
            }
        }

        [PersistentAlias("Iif(VAT, TotalCostExcl * VatPercentage, 0)")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double CostVATTotal {
            get {
                object tempObject = EvaluateAlias("CostVATTotal");
                if (tempObject != null)
                {
                    return Math.Round(Convert.ToDouble(EvaluateAlias("CostVATTotal")), 2);
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("Iif(VAT, TotalExcl * VatPercentage, 0)")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double VATTotal {
            get {
                object tempObject = EvaluateAlias("VATTotal");
                if (tempObject != null) {
                    return Math.Round(Convert.ToDouble(EvaluateAlias("VATTotal")), 2);
                } else {
                    return 0;
                }
            }
        }

        [VisibleInListView(true), VisibleInDetailView(false)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [XafDisplayName("On Hand")]
        public double QuanityOnHand {
            get {
                return Math.Round(this.Item == null ? 0 : this.Item.QuantityOnHand, 2);
            }
        }

        private DateTime fCreated;
        [VisibleInDetailView(false), VisibleInListView(false)]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Ascending)]
        public DateTime Created {
            get {
                return fCreated;
            }
            set {
                SetPropertyValue("Created", ref fCreated, value);
            }
        }

        private InventoryTransaction fInventoryTransaction;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public InventoryTransaction InventoryTransaction {
            get {
                return fInventoryTransaction;
            }
            set {
                SetPropertyValue("InventoryTransaction", ref fInventoryTransaction, value);
            }
        }

        private Nullable<bool> fDelivered;
        [VisibleInListView(false),VisibleInDetailView(false)]
        public Nullable<bool> Delivered {
            get {
                return fDelivered;
            }
            set {
                SetPropertyValue("Delivered", ref fDelivered, value);
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public bool IsParent {
            get {
                if (this.ChildItems.Count >= 1)
                    return true;
                else
                    return false;
            }
        }

        private WorkflowItem fParentItem;
        [Association("ParentItem_WorkflowItem")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public WorkflowItem ParentItem {
            get {
                return fParentItem;
            }
            set {
                SetPropertyValue("ParentItem", ref fParentItem, value);
            }
        }

        [Association("ParentItem_WorkflowItem")]
        public XPCollection<WorkflowItem> ChildItems {
            get { return GetCollection<WorkflowItem>("ChildItems"); }
        }
        #endregion

        private string fAccountingPartnerIdentifier;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string AccountingPartnerIdentifier {
            get => fAccountingPartnerIdentifier;
            set => SetPropertyValue(nameof(AccountingPartnerIdentifier), ref fAccountingPartnerIdentifier, value);
        }

        #region IInventoryTransaction
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IWorkflowType IInventoryTransactionType {
            get {
                return Workflow.Type;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public IInventoryTransactionRecord IInventoryTransactionRecord {
            get {
                return this.InventoryTransaction;
            } set {
                this.InventoryTransaction = value as InventoryTransaction;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public IItem IItem {
            get {
                return this.Item;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public DateTime TransactionDate {
            get {
                return this.Created;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public IInventoryLocation IInventoryLocation {
            get {
                return this.InventoryLocation;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public WorkFlowType MovementType {
            get {
                return Workflow != null ? Workflow.WorkFlowType : WorkFlowType.All;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public bool ReduceInventory {
            get {
                return Workflow == null ? false : Workflow.Type == null ? false : Workflow.Type.ReduceInventory;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<IInventoryTransaction> RelatedTransactions {
            get { return this.ChildItems; }
        }
        #endregion

        #region Events
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (propertyName == "CostPrice")
            {
                this.SellingPrice = this.CostPrice * (1 + Constants.DefaultMarkupPercentage(this.Session) / 100);
            } else if (propertyName == "SellingPrice")
            {
                this.CostPrice = this.SellingPrice / (1 + Constants.DefaultMarkupPercentage(this.Session) / 100);
            }
        }

        public override void AfterConstruction() {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
                this.Created = Constants.DateTimeTimeZone(this.Session);

                this.InventoryLocation = InventoryLocation == null ? this.Session.FindObject<InventoryLocation>(new BinaryOperator("DefaultLocation", true)) : this.InventoryLocation;
        }

        protected override void OnSaving()
        {
            IInventoryTransactionHelper.UpdateInventoryTransaction(this);

            base.OnSaving();
        }

        protected override void OnDeleting()
        {
            IInventoryTransactionHelper.DeleteInventoryTransaction(this);

            base.OnDeleting();
        }
        #endregion
    }
}
