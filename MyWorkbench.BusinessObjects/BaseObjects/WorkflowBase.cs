using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Attributes;
using MyWorkbench.BusinessObjects.Accounts;
using MyWorkbench.BusinessObjects.Common;
using MyWorkbench.BusinessObjects.Communication;
using MyWorkbench.BusinessObjects.Helpers;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Ignyt.Framework.ExpressApp;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions]
    [NavigationItem(false)]
    [DefaultProperty("FullDescription")]
    [ImageName("BO_Resume")]
    public abstract class WorkflowBase : BaseObject, IWorkflow, IStatusPriority<Status, Priority>, IEndlessPaging, IEmailPopup, IMessagePopup, IWorkflowDesign {
        public static string _viewUrl = "https://web.myworkbench.com/#ViewID={0}_DetailView&ObjectKey={1}&ObjectClassName=IntelliServe.Module.BusinessObjects.{2}&mode=View";

        public WorkflowBase(Session session)
            : base(session)
        {
        }

        #region Properties
        private string fPrefix;
        [VisibleInDetailView(false), VisibleInListView(false)]
        [NonCloneable]
        public string Prefix {
            get => fPrefix;
            set => SetPropertyValue(nameof(Prefix), ref fPrefix, value);
        }

        private string fNo;
        [VisibleInDetailView(false), VisibleInListView(true)]
        [ModelDefault("Index","1")]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        [NonCloneable]
        public string No {
            get => fNo;
            set => SetPropertyValue(nameof(No), ref fNo, value);
        }

        private Party fParty;
        [DevExpress.Xpo.DisplayName("Created By")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [NonCloneable]
        public Party Party {
            get => fParty;
            set => SetPropertyValue(nameof(Party), ref fParty, value);
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string PartyEmail {
            get {
                if (this.Party != null)
                    return this.Session.FindObject<Employee>(CriteriaOperator.Parse("Oid ==?", this.Party.Oid)).Email;
                return null;
            }
        }

        private string fDescription;
        [VisibleInDetailView(true), VisibleInListView(false)]
        [ModelDefault("RowCount", "5")]
        [Size(SizeAttribute.Unlimited)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        private string fSubject;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public string Subject {
            get => fSubject;
            set => SetPropertyValue(nameof(Party), ref fSubject, value);
        }

        private DateTime fIssued;
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        public DateTime Issued {
            get => fIssued;
            set => SetPropertyValue(nameof(Issued), ref fIssued, value);
        }

        private string fFullUrl;
        [VisibleInDetailView(false), VisibleInListView(false)]
        [Size(-1)]
        public string FullUrl {
            get => fFullUrl;
            set => SetPropertyValue(nameof(FullUrl), ref fFullUrl, value);
        }

        private Vendor fVendor;
        [Association("Vendor_WorkflowBase")]
        [ImmediatePostData(true)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [DataSourceCriteria("[AccountType] = 0")]
        public Vendor Vendor {
            get {
                return fVendor;
            }
            set {
                SetPropertyValue("Vendor", ref fVendor, value);

                if (!IsLoading && !IsSaving)
                {
                    if (this.Vendor != null)
                    {
                        this.Location = this.Vendor.Locations.Count > 0 ? this.Vendor.Locations.First() : null;
                        this.VendorContact = this.Vendor.VendorContacts.Count > 0 ? this.Vendor.VendorContacts.First() : null;
                    }
                }
            }
        }

        private Location fLocation;
        [DataSourceProperty("Vendor.Locations")]
        [Association("Location_WorkflowBase")]
        [DevExpress.Xpo.DisplayName("Location")]
        //[EditorAlias("CustomLookupPropertyEditor")]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Web.Editors.ASPx.ASPxLookupPropertyEditor")]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [ImmediatePostData(true)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public Location Location {
            get {
                return fLocation;
            }
            set {
                SetPropertyValue("Location", ref fLocation, value);
            }
        }

        private VendorContact fVendorContact;
        [DataSourceProperty("Vendor.VendorContacts")]
        [Association("VendorContact_WorkflowBase")]
        [DevExpress.Xpo.DisplayName("Contact")]
        //[EditorAlias("CustomLookupPropertyEditor")]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Web.Editors.ASPx.ASPxLookupPropertyEditor")]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public VendorContact VendorContact {
            get => fVendorContact;
            set => SetPropertyValue(nameof(VendorContact), ref fVendorContact, value);
        }

        [DevExpress.Xpo.DisplayName("Assigned To")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [EditorAlias("CustomTokenCollectionEditor")]
        [Association("WorkflowResource_WorkflowBase")]
        public XPCollection<WorkflowResource> WorkflowResources {
            get {
                return GetCollection<WorkflowResource>("WorkflowResources");
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<IWorkFlowResource> Assigned {
            get {
                return this.WorkflowResources;
            }
            set {
                this.WorkflowResources.AddRangeUnique(value as IEnumerable<WorkflowResource>);
            }
        }

        private string fReferenceNumber;
        [DevExpress.Xpo.DisplayName("Reference")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string ReferenceNumber {
            get => fReferenceNumber;
            set => SetPropertyValue(nameof(ReferenceNumber), ref fReferenceNumber, value);
        }

        private string fPONumber;
        [DevExpress.Xpo.DisplayName("PO Number")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public string PONumber {
            get => fPONumber;
            set => SetPropertyValue(nameof(PONumber), ref fPONumber, value);
        }

        private WorkFlowTerm fTerm;
        [VisibleInListView(false), VisibleInDetailView(false)]
        [ImmediatePostData(true)]
        [Association("WorkFlowTerm_WorkFlowBase")]
        [NonCloneable]
        public WorkFlowTerm Term {
            get {
                return fTerm;
            }
            set {
                SetPropertyValue("Term", ref fTerm, value);

                if (!IsLoading)
                    if (this.Term != null)
                        this.Terms = fTerm.Description;
            }
        }

        private string fTerms;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [ModelDefault("RowCount", "5")]
        [Appearance("TermsEnabled", Enabled = false, Context = "DetailView", Criteria = "WorkFlowType=3 or WorkFlowType=13")]
        [NonCloneable]
        public string Terms {
            get => fTerms;
            set => SetPropertyValue(nameof(Terms), ref fTerms, value);
        }

        private string fRequest;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Size(SizeAttribute.Unlimited)]
        [ModelDefault("RowCount", "5")]
        [DevExpress.Xpo.DisplayName("Request")]
        public string Request {
            get {
                return fRequest;
            }
            set {
                SetPropertyValue("Request", ref fRequest, value);
            }
        }

        private Priority fPriority;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        public Priority Priority {
            get {
                return fPriority;
            }
            set {
                SetPropertyValue("Priority", ref fPriority, value);
            }
        }

        private Status fStatus;
        [DevExpress.Xpo.DisplayName("Status")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [Association("Status_WorkflowBase")]
        [NonCloneable]
        [DataSourceProperty("Statuses")]
        public Status Status {
            get {
                return fStatus;
            }
            set {
                SetPropertyValue("Status", ref fStatus, value);
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public IList<Status> Statuses {
            get {
                return new XpoHelper(this.Session).GetObjects<Status>(CriteriaOperator.Parse("WorkFlowType == ? or WorkFlowType == 10 or WorkFlowType is null", this.WorkFlowType)).ToList();
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [NonCloneable]
        public IStatus CurrentStatus {
            get {
                return this.Status;
            }
            set {
                this.Status = value as Status;
            }
        }

        private WorkflowType fWorkflowType;
        [DevExpress.Xpo.DisplayName("Type")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [Association("WorkflowType_WorkflowBase")]
        public WorkflowType Type {
            get {
                return fWorkflowType;
            }
            set {
                SetPropertyValue("Type", ref fWorkflowType, value);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public virtual string FullDescription {
            get {
                return this.Location != null ?  string.Format("{0} - {1} - {2}", this.No, Vendor == null ? null : this.Vendor.FullName, this.Location) : string.Format("{0} - {1}", this.No, Vendor == null ? null : this.Vendor.FullName);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        [NonCloneable]
        public abstract WorkFlowType WorkFlowType { get; }

        [DevExpress.Xpo.DisplayName("Label")]
        [Association("Label_WorkflowBase")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [EditorAlias("TokenCollectionEditor")]
        public XPCollection<Label> Labels {
            get {
                return GetCollection<Label>("Labels");
            }
        }

        private double fDiscountPercent;
        [DevExpress.Xpo.DisplayName("Discount %")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [VisibleInListView(false),VisibleInDetailView(true)]
        [RuleRange(0, 100)]
        public double DiscountPercent {
            get {
                return Math.Round(fDiscountPercent, 2);
            }
            set {
                SetPropertyValue("DiscountPercent", ref fDiscountPercent, Math.Round(value, 2));
            }
        }

        private double fAdditionalPercent;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [DevExpress.Xpo.DisplayName("Additional %")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [RuleRange(0, 100)]
        public double AdditionalPercent {
            get {
                return Math.Round(fAdditionalPercent, 2);
            }
            set {
                SetPropertyValue("AdditionalPercent", ref fAdditionalPercent, Math.Round(value, 2));
            }
        }

        private double fDepositPercent;
        [VisibleInListView(false),VisibleInDetailView(false)]
        [DevExpress.Xpo.DisplayName("Deposit %")]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [RuleRange(0, 100)]
        public double DepositPercent {
            get {
                return Math.Round(fDepositPercent, 2);
            }
            set {
                SetPropertyValue("DepositPercent", ref fDepositPercent, Math.Round(value, 2));
            }
        }

        private double fExcess;
        [VisibleInListView(false), VisibleInDetailView(true)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        public double Excess {
            get {
                return Math.Round(fExcess, 2);
            }
            set {
                SetPropertyValue("Excess", ref fExcess, Math.Round(value, 2));
            }
        }

        private Currency fCurrency;
        [VisibleInListView(false), VisibleInDetailView(true)]
        public Currency Currency {
            get {
                return fCurrency;
            }
            set {
                SetPropertyValue("Currency", ref fCurrency, value);
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string PreparedBy {
            get {
                return this.Party == null ? string.Empty : this.Party.DisplayName;
            }
        }


        #region Totals
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [DevExpress.Xpo.DisplayName("Total(Cost)")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        [Appearance("TotalCostExclDisable", Enabled = false, Context = "Any")]
        [Persistent]
        public double TotalCostExcl {
            get {
                return IWorkflowHelper.CalculateTotalCostExcl(this);
            }
        }


        [ModelDefault("DisplayFormat", "{0:N2}")]
        [DevExpress.Xpo.DisplayName("Subtotal(Excl)")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Appearance("SubTotalExclDisable", Enabled = false, Context = "Any")]
        [Persistent]
        public double SubTotalExcl {
            get {
                return IWorkflowHelper.CalculateSubTotalExcl(this);
            }
        }

        [ModelDefault("DisplayFormat", "{0:N2}")]
        [DevExpress.Xpo.DisplayName("Total(Excl)")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Appearance("TotalExclDisable", Enabled = false, Context = "Any")]
        [Persistent]
        public double TotalExcl {
            get {
                return IWorkflowHelper.CalculateTotalExcl(this);
            }
        }

        [ModelDefault("DisplayFormat", "{0:N2}")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        [Appearance("CostVATTotalDisable", Enabled = false, Context = "Any")]
        [DevExpress.Xpo.DisplayName("Cost VAT")]
        [Persistent]
        public double CostVATTotal {
            get {
                return IWorkflowHelper.CalculateCostVATTotal(this);
            }
        }

        [ModelDefault("DisplayFormat", "{0:N2}")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Appearance("VATTotalDisable", Enabled = false, Context = "Any")]
        [DevExpress.Xpo.DisplayName("VAT")]
        [Persistent]
        public double VATTotal {
            get {
                return IWorkflowHelper.CalculateVATTotal(this);
            }
        }

        [ModelDefault("DisplayFormat", "{0:N2}")]
        [DevExpress.Xpo.DisplayName("Total Cost(Incl)")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        [Appearance("CostTotalInclDisable", Enabled = false, Context = "Any")]
        [Persistent]
        public double CostTotalIncl {
            get {
                return IWorkflowHelper.CalculateCostTotalIncl(this);
            }
        }

        [ModelDefault("DisplayFormat", "{0:N2}")]
        [DevExpress.Xpo.DisplayName("Total(Incl)")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("TotalInclDisable", Enabled = false, Context = "Any")]
        [Persistent]
        public double TotalIncl {
            get {
                return IWorkflowHelper.CalculateTotalIncl(this);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [Persistent]
        public double AdditionalAmount {
            get {
                return IWorkflowHelper.CalculateAdditionalAmount(this);
            }
        }

        [DevExpress.Xpo.DisplayName("Payment")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [Persistent]
        public double PaymentTotal {
            get {
                return IWorkflowHelper.CalculatePayment(this);
            }
        }

        [DevExpress.Xpo.DisplayName("Outstanding")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [Persistent]
        public double AmountOutstanding {
            get {
                return IWorkflowHelper.CalculateAmountOutstanding(this);
            }
        }

        [DevExpress.Xpo.DisplayName("Deposit")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [Persistent]
        public double DepositAmount {
            get {
                return IWorkflowHelper.CalculateDepositAmount(this);
            }
        }

        [DevExpress.Xpo.DisplayName("Credit")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        [ModelDefault("DisplayFormat", "{0:N2}")]
        [ModelDefault("EditMask", "{0:N2}")]
        [Persistent]
        public double CreditTotal {
            get {
                return IWorkflowHelper.CalculateCreditTotal(this);
            }
        }

        private Transaction fTransaction;
        [VisibleInDetailView(false), VisibleInListView(false)]
        [NonCloneable]
        public Transaction Transaction {
            get {
                return fTransaction;
            }
            set {
                if (fTransaction == value)
                    return;

                Transaction prevTransaction = fTransaction;
                fTransaction = value;

                if (IsLoading) return;

                if (prevTransaction != null && prevTransaction.Workflow == this)
                    prevTransaction.Workflow = null;

                if (fTransaction != null)
                    fTransaction.Workflow = this;

                OnChanged("Transaction");
            }
        }
        #endregion

        #region WorkCalendar Event
        private DateTime? fBookedTime;
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        [ImmediatePostData]
        [DevExpress.Xpo.DisplayName("Start")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("Index", "8")]
        [EditorAlias("CustomDateTimeEditor")]
        public DateTime? BookedTime {
            get {
                return fBookedTime;
            }
            set {
                SetPropertyValue("BookedTime", ref fBookedTime, value);
            }
        }

        private DateTime? fBookedTimeEnd;
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [DevExpress.Xpo.DisplayName("End")]
        [EditorAlias("CustomDateTimeEditor")]
        public DateTime? BookedTimeEnd {
            get {
                return fBookedTimeEnd;
            }
            set {
                SetPropertyValue("BookedTimeEnd", ref fBookedTimeEnd, value);
            }
        }

        private DateTime? fExportedDateTime;
        [VisibleInListView(false), VisibleInDetailView(false)]
        public DateTime? ExportedDateTime {
            get {
                return fExportedDateTime;
            }
            set {
                SetPropertyValue("ExportedDateTime", ref fExportedDateTime, value);
            }
        }

        private DateTime? fLastUpdated;
        [VisibleInListView(false), VisibleInDetailView(false)]
        public DateTime? LastUpdated {
            get {
                return fLastUpdated;
            }
            set {
                SetPropertyValue("LastUpdated", ref fLastUpdated, value);
            }
        }

        private WorkFlowType fWorkFlowTypeConvertedTo;
        [VisibleInDetailView(false), VisibleInListView(false)]
        [NonCloneable]
        public WorkFlowType WorkFlowTypeConvertedTo {
            get {
                return fWorkFlowTypeConvertedTo;
            }
            set {
                SetPropertyValue("WorkFlowTypeConvertedTo", ref fWorkFlowTypeConvertedTo, value);
            }
        }

        private WorkFlowType fWorkFlowTypeConvertedFrom;
        [VisibleInDetailView(false), VisibleInListView(false)]
        [NonCloneable]
        public WorkFlowType WorkFlowTypeConvertedFrom {
            get {
                return fWorkFlowTypeConvertedFrom;
            }
            set {
                SetPropertyValue("WorkFlowTypeConvertedFrom", ref fWorkFlowTypeConvertedFrom, value);
            }
        }
        #endregion

        #endregion

        #region ParentChild
        [Association("ParentItem_WorkflowBase")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        [NonCloneable]
        public XPCollection<WorkflowBase> ParentItems {
            get { return GetCollection<WorkflowBase>("ParentItems"); }
        }

        [Association("ParentItem_WorkflowBase")]
        [DevExpress.Xpo.DisplayName("Workflow Links")]
        [NonCloneable]
        public XPCollection<WorkflowBase> ChildItems {
            get { return GetCollection<WorkflowBase>("ChildItems"); }
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
        #endregion

        #region Collections
        [Association("WorkflowBase_WorkflowItem"), Aggregated]
        [DevExpress.Xpo.DisplayName("Items")]
        public XPCollection<WorkflowItem> Items {
            get {
                return GetCollection<WorkflowItem>("Items");
            }
        }

        [Association("WorkflowBase_WorkFlowEquipment"), Aggregated]
        [NonCloneable]
        public XPCollection<WorkFlowEquipment> Equipment {
            get {
                return GetCollection<WorkFlowEquipment>("Equipment");
            }
        }

        [Association("WorkflowBase_WorkFlowNote"), Aggregated]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [NonCloneable]
        public XPCollection<WorkFlowNote> Notes {
            get {
                return GetCollection<WorkFlowNote>("Notes");
            }
        }

        [Association("WorkflowBase_WorkFlowPayment"), Aggregated]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [NonCloneable]
        public XPCollection<WorkFlowPayment> Payments {
            get {
                return GetCollection<WorkFlowPayment>("Payments");
            }
        }

        [Association("WorkflowBase_WorkFlowImage"), Aggregated]
        [NonCloneable]
        public XPCollection<WorkFlowImage> Images {
            get {
                return GetCollection<WorkFlowImage>("Images");
            }
        }

        [Association("WorkflowBase_WorkFlowAttachment"), Aggregated]
        [NonCloneable]
        public XPCollection<WorkFlowAttachment> Attachments {
            get {
                return GetCollection<WorkFlowAttachment>("Attachments");
            }
        }

        [Association("WorkflowBase_WorkFlowSignature"), Aggregated]
        [NonCloneable]
        public XPCollection<WorkFlowSignature> Signatures {
            get {
                return GetCollection<WorkFlowSignature>("Signatures");
            }
        }

        [Association("WorkflowBase_WorkflowTracking"), Aggregated]
        [NonCloneable]
        public XPCollection<WorkflowTracking> Tracking {
            get {
                return GetCollection<WorkflowTracking>("Tracking");
            }
        }

        [Association("WorkflowBase_WorkflowBaseMassInventoryMovement"), Aggregated]
        [NonCloneable]
        public XPCollection<WorkflowBaseMassInventoryMovement> Movements {
            get {
                return GetCollection<WorkflowBaseMassInventoryMovement>("Movements");
            }
        }

        [Association("WorkflowBase_WorkFlowTask"), Aggregated]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [NonCloneable]
        public XPCollection<WorkFlowTask> Tasks {
            get {
                return GetCollection<WorkFlowTask>("Tasks");
            }
        }

        [Association("WorkflowBase_WorkFlowChecklist"), Aggregated]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [NonCloneable]
        [DevExpress.Xpo.DisplayName("Checklist")]
        public XPCollection<WorkFlowChecklist> WorkFlowChecklists {
            get {
                return GetCollection<WorkFlowChecklist>("WorkFlowChecklists");
            }
        }

        [Association("WorkflowBase_WorkFlowTimeTracking"), Aggregated]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [NonCloneable]
        [DevExpress.Xpo.DisplayName("Time Tracking")]
        public XPCollection<WorkFlowTimeTracking> WorkFlowTimeTracking {
            get {
                return GetCollection<WorkFlowTimeTracking>("WorkFlowTimeTracking");
            }
        }

        [Association("WorkflowBase_WorkFlowTime"), Aggregated]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [NonCloneable]
        [DevExpress.Xpo.DisplayName("Time")]
        public XPCollection<WorkFlowTime> WorkFlowTime {
            get {
                return GetCollection<WorkFlowTime>("WorkFlowTime");
            }
        }

        [Association("WorkflowBase_WorkFlowTimeTrackingMultiple"), Aggregated]
        [VisibleInDetailView(false), VisibleInListView(false)]
        [NonCloneable]
        public XPCollection<WorkFlowTimeTrackingMultiple> WorkFlowTimeTrackingMultiple {
            get {
                return GetCollection<WorkFlowTimeTrackingMultiple>("WorkFlowTimeTrackingMultiple");
            }
        }

        [Association("WorkflowBase_WorkflowSpreadsheet"), Aggregated]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Spreadsheets")]
        public XPCollection<WorkflowSpreadsheet> WorkflowSpreadsheet {
            get {
                return GetCollection<WorkflowSpreadsheet>("WorkflowSpreadsheet");
            }
        }

        [DevExpress.Xpo.DisplayName("Team")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Association("Employee_WorkflowBase"), Aggregated]
        [NonCloneable]
        public XPCollection<Employee> Employee {
            get {
                return GetCollection<Employee>("Employee");
            }
        }

        [NonCloneable]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public XPCollection<Email> Emails {
            get {
                SortingCollection sortCollection = new SortingCollection
                {
                    new SortProperty("Created", SortingDirection.Descending)
                };

                XPCollection<Email> emails = new XPCollection<Email>(this.Session, CriteriaOperator.Parse("ObjectOid = ?", this.Oid))
                {
                    Sorting = sortCollection
                };

                return emails;
            }
        }

        [NonCloneable]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public XPCollection<Message> Messages {
            get {
                SortingCollection sortCollection = new SortingCollection
                {
                    new SortProperty("Created", SortingDirection.Descending)
                };

                XPCollection<Message> messages = new XPCollection<Message>(this.Session, CriteriaOperator.Parse("ObjectOid = ?", this.Oid))
                {
                    Sorting = sortCollection
                };

                return messages;

            }
        }
        #endregion

        #region Interfaces
        private Guid fApplicationID;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public Guid ApplicationID {
            get {
                if (fApplicationID == Guid.Empty)
                    fApplicationID = Constants.ApplicationID(this.Session);
                return fApplicationID;
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public Party SendingUser {
            get {
                if (SecuritySystem.CurrentUserId.ToString() == string.Empty)
                    return this.Party;
                else
                    return Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string Title {
            get {
                return string.Format("{0} - {1}, {2}", this.GetType().Name, this.No, Vendor == null ? null : this.Vendor.FullName);
            }
        }

        private IEnumerable<IEmailAddress> fEmailAddresses;
        [NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<IEmailAddress> EmailAddresses {
            get {
                if (fEmailAddresses == null) {
                    List<IEmailAddress> items = new List<IEmailAddress>();

                    if(this.Vendor != null)
                        items.AddRange(this.Vendor.EmailAddresses);

                    foreach (WorkflowResource resource in this.WorkflowResources)
                        items.AddRange(resource.EmailAddresses);

                    items.AddRange(((Employee)this.Party).EmailAddresses);

                    fEmailAddresses = items;
                }

                return fEmailAddresses;
            }
        }

        private IEnumerable<ICellNumber> fCellNumbers;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<ICellNumber> CellNumbers {
            get {
                if (fCellNumbers == null) {
                    List<ICellNumber> items = new List<ICellNumber>();

                    if (this.Vendor != null && this.Vendor.CellNumbers != null)
                        items.AddRange(this.Vendor.CellNumbers);

                    foreach (WorkflowResource resource in this.WorkflowResources)
                        items.AddRange(resource.CellNumbers);

                    if (((Employee)this.Party).CellNumbers != null)
                        items.AddRange(((Employee)this.Party).CellNumbers);

                    fCellNumbers = items;
                }

                return fCellNumbers;
            }
        }

        private IEnumerable<IFileAttachment> fEmailAttachments;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<IFileAttachment> EmailAttachments {
            get {
                if (fEmailAttachments == null)
                {
                    List<IFileAttachment> items = new List<IFileAttachment>();
                    items.AddRange(this.Attachments);

                    fEmailAttachments = items;
                }
                return fEmailAttachments;
            }
        }

        [VisibleInListView(false),VisibleInDetailView(false)]
        public abstract string ReportDisplayName { get; }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public abstract string ReportFileName { get; }

        private byte[] ftext = null;
        [VisibleInDetailView(false), VisibleInListView(false)]
        [NonPersistent]
        public byte[] Text { get { return ftext; } set { ftext = value; } }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public string AssignedTo {
            get {
                string assignedTo = string.Empty;

                foreach (WorkflowResource resource in this.WorkflowResources)
                {
                    if (assignedTo == string.Empty)
                        assignedTo = resource.Caption;
                    else
                        assignedTo = string.Concat(assignedTo, " and ", resource.Caption);
                }

                return assignedTo;
            }
        }
        #endregion

        #region Events
        protected override void OnSaving() {
            if (string.IsNullOrEmpty(this.fNo))
            {
                this.fPrefix = this.Session.FindObject<SettingsPrefix>(CriteriaOperator.Parse("DataType == ?", this.GetType())).Prefix;
                this.fFullUrl = string.Format(_viewUrl, this.GetType().Name, this.Oid, this.GetType().Name);
                this.fNo = string.Format("{0}{1:D6}", this.fPrefix, DistributedIdGeneratorHelper.Generate(this.Session.DataLayer, this.GetType().Name, string.Empty));
            }

            this.LastUpdated = Constants.DateTimeTimeZone(this.Session);
            WorkFlowHelper.CreateWorkFlowProcess(this);

            base.OnSaving();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (propertyName == "BookedTime")
            {
                if (oldValue != newValue & newValue != null)
                {
                    if (this.BookedTime != null)
                        this.BookedTimeEnd = ((DateTime)this.BookedTime).AddHours(Constants.AppointmentLength(this.Session));
                }
            }
        }

        public override void AfterConstruction() {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this)) {
                if (this.Party == null)
                {
                    if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                        this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);
                }
                
                this.Issued = Constants.DateTimeTimeZone(this.Session);
                this.Priority = Priority.Normal;
                SettingsTerms terms = this.Session.FindObject<SettingsTerms>(CriteriaOperator.Parse("DataType == ?", this.GetType()));
                this.Terms = terms?.Terms;
                if(this.Status == null)
                    this.Status = this.Session.FindObject<Status>(new BinaryOperator("IsDefault", 1, BinaryOperatorType.Equal));
                this.DepositPercent = Constants.DefaultDepositPercentage(this.Session);
                this.Currency = Constants.Currency(this.Session);
            }
        }

        protected override void OnSaved()
        {
            new WorkFlowExecute<WorkflowBase>().Execute(this);

            base.OnSaved();
        }
        #endregion
    }
}
