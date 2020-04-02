using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Inventory {
    public enum MovementType {
        Movement = 0,
        StockTake = 1
    }

    [DefaultProperty("FullDescription")]
    [DefaultClassOptions]
    [NavigationItem("Inventory")]
    [ImageName("Action_Debug_Stop")]
    public class MassInventoryMovement : BaseObject, IWorkflow, IEndlessPaging {
        public MassInventoryMovement(Session session)
            : base(session) {
        }

        #region Properties
        private string fPrefix;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string Prefix {
            get => fPrefix;
            set => SetPropertyValue(nameof(Prefix), ref fPrefix, value);
        }

        private string fNo;
        [VisibleInDetailView(false),VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("No")]
        [Indexed(Unique = true)]
        public string No {
            get {
                return fNo;
            }
            set {
                SetPropertyValue("No", ref fNo, value);
            }
        }

        private InventoryLocation fFromLocation;
        [DevExpress.Xpo.DisplayName("From")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public InventoryLocation FromLocation {
            get {
                return fFromLocation;
            }
            set {
                SetPropertyValue("FromLocation", ref fFromLocation, value);
            }
        }

        private InventoryLocation fToLocation;
        [Appearance("ToVisibility", TargetItems = "ToLocation", Visibility = ViewItemVisibility.Hide, Criteria = "DisposeOf", Context = "DetailView")]
        [DevExpress.Xpo.DisplayName("To")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public InventoryLocation ToLocation {
            get {
                return fToLocation;
            }
            set {
                SetPropertyValue("ToLocation", ref fToLocation, value);
            }
        }

        private MovementType fReason;
        [VisibleInDetailView(true), VisibleInListView(true)]
        public MovementType Reason {
            get {
                return fReason;
            }
            set {
                SetPropertyValue("Reason", ref fReason, value);
            }
        }

        private bool fDisposeOf;
        [ImmediatePostData(true)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public bool DisposeOf {
            get {
                return fDisposeOf;
            }
            set {
                SetPropertyValue("DisposeOf", ref fDisposeOf, value);
            }
        }

        private Party fParty;
        [DevExpress.Xpo.DisplayName("Created By")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public Party Party {
            get => fParty;
            set => SetPropertyValue(nameof(Party), ref fParty, value);
        }

        private DateTime fIssued;
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ModelDefault("Index", "3")]
        public DateTime Issued {
            get => fIssued;
            set => SetPropertyValue(nameof(Issued), ref fIssued, value);
        }

        private string fTerms;
        [Size(-1)]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public string Terms {
            get {
                return fTerms;
            }
            set {
                SetPropertyValue("Terms", ref fTerms, value);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public virtual string FullDescription {
            get {
                return string.Format("{0}{1}{2}",this.FromLocation, string.Empty, this.ToLocation);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<IWorkFlowResource> Assigned {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public IStatus CurrentStatus {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Collections
        [Association("MassInventoryMovement_MassInventoryMovementItem"), Aggregated]
        public XPCollection<MassInventoryMovementItem> MassInventoryMovementItems {
            get {
                return GetCollection<MassInventoryMovementItem>("MassInventoryMovementItems");
            }
        }

        [VisibleInDetailView(false),VisibleInListView(false)]
        [Association("MassInventoryMovement_WorkflowBaseMassInventoryMovement"), Aggregated]
        public XPCollection<WorkflowBaseMassInventoryMovement> WorkflowBaseMassInventoryMovements {
            get {
                return GetCollection<WorkflowBaseMassInventoryMovement>("WorkflowBaseMassInventoryMovements");
            }
        }
        #endregion

        #region IWorkflow NotImplemented
        public WorkFlowType WorkFlowType {
            get{
                return WorkFlowType.MassInventoryMovement;
            }
        }

        private double fDiscountPercent;
        [VisibleInDetailView(false),VisibleInListView(false)]
        public double DiscountPercent {
            get => fDiscountPercent;
            set => SetPropertyValue(nameof(DiscountPercent), ref fDiscountPercent, value);
        }

        private double fAdditionalPercent;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double AdditionalPercent {
            get => fAdditionalPercent;
            set => SetPropertyValue(nameof(AdditionalPercent), ref fAdditionalPercent, value);
        }

        private double fTotalCostExcl;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double TotalCostExcl {
            get => fTotalCostExcl;
            set => SetPropertyValue(nameof(TotalCostExcl), ref fTotalCostExcl, value);
        }

        private double fSubTotalExcl;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double SubTotalExcl {
            get => fSubTotalExcl;
            set => SetPropertyValue(nameof(SubTotalExcl), ref fSubTotalExcl, value);
        }

        private double fTotalExcl;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double TotalExcl {
            get => fTotalExcl;
            set => SetPropertyValue(nameof(TotalExcl), ref fTotalExcl, value);
        }

        private double fVATTotal;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double VATTotal {
            get => fVATTotal;
            set => SetPropertyValue(nameof(VATTotal), ref fVATTotal, value);
        }

        private double fTotalIncl;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double TotalIncl {
            get => fTotalIncl;
            set => SetPropertyValue(nameof(TotalIncl), ref fTotalIncl, value);
        }

        private double fAdditionalAmount;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public double AdditionalAmount {
            get => fAdditionalAmount;
            set => SetPropertyValue(nameof(AdditionalAmount), ref fAdditionalAmount, value);
        }

        public IEnumerable<IWorkflowItem<IWorkflow, IInventoryLocation, IItem>> Items => throw new NotImplementedException();
        #endregion

        #region Events
        public override void AfterConstruction() {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this)) {
                if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);

                this.Issued = Constants.DateTimeTimeZone(this.Session);
                SettingsTerms terms = this.Session.FindObject<SettingsTerms>(CriteriaOperator.Parse("DataType == ?", this.GetType()));
                this.Terms = terms?.Terms;
                InventoryLocation invLocation = Session.FindObject<InventoryLocation>(new BinaryOperator("DefaultLocation", true));

                if (invLocation != null) {
                    this.FromLocation = invLocation;
                }
            }
        }

        protected override void OnSaving() {
            if (this.fNo == null)
                this.fNo = string.Format("{0}{1:D6}", this.fPrefix, DistributedIdGeneratorHelper.Generate(this.Session.DataLayer, this.GetType().Name, string.Empty));

            base.OnSaving();
        }

        protected override void OnSaved() {
            base.OnSaved();
        }
        #endregion
    }
}
