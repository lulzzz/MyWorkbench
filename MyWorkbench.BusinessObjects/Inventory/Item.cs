using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Utils;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using System;
using System.ComponentModel;
using MyWorkbench.BaseObjects.Constants;
using MyWorkbench.BusinessObjects.BaseObjects;
using DevExpress.ExpressApp.Model;
using Ignyt.BusinessInterface.Attributes;
using DevExpress.ExpressApp.DC;
using Ignyt.Framework;

namespace MyWorkbench.BusinessObjects.Inventory {
    [DefaultClassOptions]
    [DefaultProperty("FullDescription")]
    [NavigationItem("Inventory")]
    [ImageName("Action_Debug_Stop")]
    [Appearance("FontColorBold", AppearanceItemType = "ViewItem", TargetItems = "*", Context = "ListView", Criteria = "[IsParent]", FontStyle = System.Drawing.FontStyle.Bold)]
    public class Item : BaseObject, IItem, IInlineEdit, IEndlessPaging, ICustomizable
    {
        public Item(Session session)
            : base(session) {
        }

        #region Properties
        private string fStockCode;
        [Size(50)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Indexed("GCRecord", Unique = true)]
        [VisibleInListView(true),VisibleInDetailView(true)]
        [ModelDefault("Index", "1")]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Ascending)]
        public string StockCode {
            get {
                return fStockCode;
            }
            set {
                SetPropertyValue("StockCode", ref fStockCode, value);
            }
        }

        [Association("ParentItem_Item")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public XPCollection<Item> ParentItems {
            get { return GetCollection<Item>("ParentItems"); }
        }

        private ItemCategory fCategory;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Association("ItemCategory_Item")]
        public ItemCategory Category {
            get {
                return fCategory;
            }
            set {
                SetPropertyValue("Category", ref fCategory, value);
            }
        }
        
        private string fDescription;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "2")]
        public string Description {
            get {
                return fDescription;
            }
            set {
                SetPropertyValue("Description", ref fDescription, value);
            }
        }

        private double fCostPrice;
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "3")]
        [Appearance("CostPriceDisable", Enabled = false, Criteria = "IsParent", Context = "DetailView")]
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
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "4")]
        [RuleValueComparison("", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0, CustomMessageTemplate = RegularExpressions.ValueError)]
        public double SellingPrice {
            get {
                return Math.Round(fSellingPrice, 2);
            }
            set {
                SetPropertyValue("SellingPrice", ref fSellingPrice, Math.Round(value, 2));
            }
        }

        [EnableInlineEdit(DefaultBoolean.False)]
        [VisibleInDetailView(false), VisibleInListView(false)]
        [PersistentAlias("StockCode + ' - ' +  Iif(Len(Description) > 40, Substring(Description,0,40) + '...', Description)")]
        [Size(10)]
        public string FullDescription {
            get { return EvaluateAlias("FullDescription").ToString(); }
        }

        private ItemType fItemType;
        [DevExpress.Xpo.DisplayName("")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public ItemType ItemType {
            get {
                return fItemType;
            }
            set {
                SetPropertyValue("ItemType", ref fItemType, value);
            }
        }

        private bool fVAT;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "5")]
        public bool VAT {
            get {
                return fVAT;
            }
            set {
                SetPropertyValue("VAT", ref fVAT, value);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        [EnableInlineEdit(DefaultBoolean.False)]
        public double VatPercentage {
            get {
                if (this.VAT)
                    return Math.Round((double)Constants.VatPercentage(this.Session) / 100, 2);
                else
                    return 0;
            }
        }

        private double fQuantityOnHand;
        [PersistentAlias("fQuantityOnHand")]
        [ModelDefault("DisplayFormat", "{0:N3}")]
        [ModelDefault("EditMask", "{0:N3}")]
        [EnableInlineEdit(DefaultBoolean.False)]
        [VisibleInListView(true), VisibleInDetailView(false)]
        [ModelDefault("Index", "6")]
        public double QuantityOnHand {
            get {
                this.UpdateQuantityOnHand(false);

                return Math.Round(this.fQuantityOnHand, 3);
            }
        }

        private DateTime fPriceLastAdjusted;
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [EnableInlineEdit(DefaultBoolean.False)]
        public DateTime PriceLastAdjusted {
            get {
                return fPriceLastAdjusted;
            }
            set {
                SetPropertyValue("PriceLastAdjusted", ref fPriceLastAdjusted, value);
            }
        }

        private string fExternalStockCode;
        [Size(50)]
        [VisibleInListView(false), VisibleInDetailView(false)]
        [EnableInlineEdit(DefaultBoolean.False)]
        public string ExternalStockCode {
            get {
                return fExternalStockCode;
            }
            set {
                SetPropertyValue("ExternalStockCode", ref fExternalStockCode, value);
            }
        }

        private double fDefaultQuantity;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:N3}")]
        [ModelDefault("EditMask", "{0:N3}")]
        [RuleValueComparison("", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0, CustomMessageTemplate = RegularExpressions.ValueError)]
        public double DefaultQuantity {
            get {
                return Math.Round(fDefaultQuantity, 3);
            }
            set {
                SetPropertyValue("DefaultQuantity", ref fDefaultQuantity, Math.Round(value, 3));
            }
        }

        private System.Drawing.Image fImage;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        [ImageEditorAttribute(DetailViewImageEditorFixedWidth = 160, DetailViewImageEditorFixedHeight = 160, ListViewImageEditorCustomHeight = 30)]
        [EnableInlineEdit(DefaultBoolean.False)]
        [ModelDefault("Index", "7")]
        public System.Drawing.Image Image {
            get {
                return fImage;
            }
            set {
                SetPropertyValue("Image", ref fImage, value);
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [EnableInlineEdit(DefaultBoolean.False)]
        public bool IsParent {
            get {
                if (this.ChildItems.Count >= 1)
                    return true;
                else
                    return false;
            }
        }

        private DateTime fCreated;
        [VisibleInListView(false), VisibleInDetailView(false)]
        public DateTime Created {
            get => fCreated;
            set => SetPropertyValue(nameof(Created), ref fCreated, value);
        }
        #endregion

        #region Collections
        [Association("ParentItem_Item")]
        public XPCollection<Item> ChildItems {
            get { return GetCollection<Item>("ChildItems"); }
        }

        [Association("Item_WorkflowItem")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<WorkflowItem> WorkflowItems {
            get { return GetCollection<WorkflowItem>("WorkflowItems"); }
        }

        [Association("Item_InventoryTransaction")]
        public XPCollection<InventoryTransaction> InventoryTransactions {
            get { return GetCollection<InventoryTransaction>("InventoryTransactions"); }
        }

        [Association("Item_MassInventoryMovementItem")]
        [XafDisplayName("Inventory Movement")]
        public XPCollection<MassInventoryMovementItem> MassInventoryMovementItems {
            get { return GetCollection<MassInventoryMovementItem>("MassInventoryMovementItems"); }
        }
        #endregion

        #region Methods
        public void UpdateQuantityOnHand(bool forceChangeEvents) {
            double oldQuantityOnHand = fQuantityOnHand;
            double tempTotal = 0;

            if (IsLoading) return;

            foreach (InventoryTransaction detail in this.InventoryTransactions)
                if (detail.Delivered == true) {
                    tempTotal += detail.InventoryIn - detail.InventoryOut;
                }

            fQuantityOnHand = tempTotal;

            if (forceChangeEvents)
                OnChanged("QuantityOnHand", oldQuantityOnHand, fQuantityOnHand);
        }

        #endregion

        #region Events
        public override void AfterConstruction() {
            base.AfterConstruction();

            this.VAT = Constants.VatRegistered(this.Session);
            this.DefaultQuantity = 1;
            this.Created = Constants.DateTimeTimeZone(this.Session);
            this.PriceLastAdjusted = Constants.DateTimeTimeZone(this.Session);
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue) {
            base.OnChanged(propertyName, oldValue, newValue);

            if (propertyName == "CostPrice") {
                this.SellingPrice = this.CostPrice * (1 + Constants.DefaultMarkupPercentage(this.Session) / 100);
            }
            else if (propertyName == "SellingPrice")
            {
                this.CostPrice = this.SellingPrice / (1 + Constants.DefaultMarkupPercentage(this.Session) / 100);
            }

            if (propertyName == "CostPrice" || propertyName == "SellingPrice") {
                if (oldValue != newValue)
                    this.PriceLastAdjusted = Constants.DateTimeTimeZone(this.Session);
            }
        }

        protected override void OnSaving() {
            base.OnSaving();
        }
        #endregion
    }
}
