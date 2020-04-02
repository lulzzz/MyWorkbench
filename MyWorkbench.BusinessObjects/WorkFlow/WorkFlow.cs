using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ignyt.Framework.ExpressApp;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Attributes;
using Ignyt.BusinessInterface.WorkFlow.Enum;
using MyWorkbench.BusinessObjects.Communication;
using MyWorkbench.BusinessObjects.Communication.Common;
using MyWorkbench.BusinessObjects.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MyWorkbench.BusinessObjects.WorkFlow
{
    [DefaultClassOptions]
    [NavigationItem("Design WorkFlow")]
    [DefaultProperty("FullDescription")]
    [ImageName("Action_Debug_Breakpoint_Toggle")]
    public class WorkFlow : BaseObject
    {
        public WorkFlow(Session session)
            : base(session)
        {
        }

        #region Properties
        [RuleRequiredField(DefaultContexts.Save)]
        [Indexed("GCRecord", Unique = true)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Ascending)]
        public string Description {
            get { return GetPropertyValue<string>("Description"); }
            set { SetPropertyValue<string>("Description", value); }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public WorkFlowStatus Status {
            get { return GetPropertyValue<WorkFlowStatus>("Status"); }
            set { SetPropertyValue<WorkFlowStatus>("Status", value); }
        }

        [ValueConverter(typeof(TypeToStringConverter)), ImmediatePostData]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter<IWorkflowDesign>))]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public Type SourceObject {
            get { return GetPropertyValue<Type>("SourceObject"); }
            set { SetPropertyValue<Type>("SourceObject", value); }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        public string Source {
            get {
                return this.SourceObject == null ? null : this.SourceObject.Name;
            }
        }

        [CriteriaOptions("SourceObject"), Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public string SourceCriterion {
            get { return GetPropertyValue<string>("SourceCriterion"); }
            set { SetPropertyValue<string>("SourceCriterion", value); }
        }

        [ValueConverter(typeof(TypeToStringConverter)), ImmediatePostData]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter<IWorkflowDesign>))]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Appearance("TargetObjectVisible", Visibility = ViewItemVisibility.Hide, Criteria = "Action == 0 or Action == 1", Context = "DetailView")]
        public Type TargetObject {
            get { return GetPropertyValue<Type>("TargetObject"); }
            set { SetPropertyValue<Type>("TargetObject", value); }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        public string Target {
            get {
                return this.TargetObject == null ? null : this.TargetObject.Name;
            }
        }

        [CriteriaOptions("TargetObject"), Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("TargetCriterionVisible", Visibility = ViewItemVisibility.Hide, Criteria = "Action == 0 or Action == 1", Context = "DetailView")]
        public string TargetCriterion {
            get { return GetPropertyValue<string>("TargetCriterion"); }
            set { SetPropertyValue<string>("TargetCriterion", value); }
        }

        [VisibleInDetailView(true), VisibleInListView(true)]
        [Appearance("ObjectStateVisible", Visibility = ViewItemVisibility.Hide, Criteria = "Action == 2", Context = "DetailView")]
        public WorkFlowObjectState ObjectState {
            get { return GetPropertyValue<WorkFlowObjectState>("ObjectState"); }
            set { SetPropertyValue<WorkFlowObjectState>("ObjectState", value); }
        }

        [VisibleInDetailView(true), VisibleInListView(true)]
        [ImmediatePostData(true)]
        public WorkFlowAction Action {
            get { return GetPropertyValue<WorkFlowAction>("Action"); }
            set { SetPropertyValue<WorkFlowAction>("Action", value); }
        }

        [VisibleInDetailView(true), VisibleInListView(true)]
        [Appearance("ActionTypeVisible", Visibility = ViewItemVisibility.Hide, Criteria = "Action == 0 or Action == 1", Context = "DetailView")]
        public WorkFlowActionType ActionType {
            get { return GetPropertyValue<WorkFlowActionType>("ActionType"); }
            set { SetPropertyValue<WorkFlowActionType>("ActionType", value); }
        }

        [VisibleInDetailView(true), VisibleInListView(true)]
        [Appearance("IntervalVisible", Visibility = ViewItemVisibility.Hide, Criteria = "Action == 2", Context = "DetailView")]
        public WorkFlowInterval Interval {
            get { return GetPropertyValue<WorkFlowInterval>("Interval"); }
            set { SetPropertyValue<WorkFlowInterval>("Interval", value); }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public DateTime Created {
            get { return GetPropertyValue<DateTime>("Created"); }
            set { SetPropertyValue<DateTime>("Created", value); }
        }

        [VisibleInDetailView(true), VisibleInListView(true)]
        [Appearance("ExpiresVisible", Visibility = ViewItemVisibility.Hide, Criteria = "Action == 2", Context = "DetailView")]
        public WorkFlowInterval Expires {
            get { return GetPropertyValue<WorkFlowInterval>("Expires"); }
            set { SetPropertyValue<WorkFlowInterval>("Expires", value); }
        }

        [VisibleInDetailView(true), VisibleInListView(true)]
        public WorkFlowOutput Output {
            get { return GetPropertyValue<WorkFlowOutput>("Output"); }
            set { SetPropertyValue<WorkFlowOutput>("Output", value); }
        } 

        [VisibleInDetailView(true), VisibleInListView(true)]
        [DataSourceProperty("Templates")]
        [Appearance("TemplateVisible", Visibility = ViewItemVisibility.Hide, Criteria = "Action == 2", Context = "DetailView")]
        public Template Template {
            get { return GetPropertyValue<Template>("Template"); }
            set { SetPropertyValue<Template>("Template", value); }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string FullDescription {
            get {
                if (this.ParentItem == null)
                    return string.Format("{0} - {1} {2}", this.Description, this.ObjectState.ToString(), this.Source);
                else
                    return string.Format("{0} - {1} {2} {3}", this.Description, this.ActionType.ToString(), this.Action.ToString(), this.Target);
            }
        }

        [RuleRequiredField(CustomMessageTemplate = "To cannot be empty", TargetContextIDs = "Immediate")]
        [EditorAlias("TokenCollectionEditor")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Association("ToRecipientRole_WorkFlow")]
        [Appearance("ToRecipientRoleVisible", Visibility = ViewItemVisibility.Hide, Criteria = "Action == 2", Context = "DetailView")]
        public XPCollection<RecipientRole> ToRecipientRole {
            get {
                return GetCollection<RecipientRole>("ToRecipientRole");
            }
        }

        [EditorAlias("TokenCollectionEditor")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Association("CCRecipientRole_WorkFlow")]
        [Appearance("CCRecipientRoleVisible", Visibility = ViewItemVisibility.Hide, Criteria = "Action == 2", Context = "DetailView")]
        public XPCollection<RecipientRole> CCRecipientRole {
            get {
                return GetCollection<RecipientRole>("CCRecipientRole");
            }
        }

        [EditorAlias("TokenCollectionEditor")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Association("BCCRecipientRole_WorkFlow")]
        [Appearance("BCCRecipientRoleVisible", Visibility = ViewItemVisibility.Hide, Criteria = "Action == 2", Context = "DetailView")]
        public XPCollection<RecipientRole> BCCRecipientRole {
            get {
                return GetCollection<RecipientRole>("BCCRecipientRole");
            }
        }

        [VisibleInDetailView(false), VisibleInListView(true)]
        [DataSourceProperty("Templates")]
        public string ActionRequired {
            get {
                if (this.Action == WorkFlowAction.Email)
                    return "Email " + this.Output.ToString();
                else if (this.Action == WorkFlowAction.Message)
                    return "Send Message";
                else
                    return this.ActionType.ToString();
            }
        }
        #endregion

        private WorkFlow fParentItem;
        [Association("ParentItem_WorkFlow")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public WorkFlow ParentItem {
            get {
                return fParentItem;
            }
            set {
                SetPropertyValue("ParentItem", ref fParentItem, value);
            }
        }

        [Association("ParentItem_WorkFlow")]
        public XPCollection<WorkFlow> ChildItems {
            get { return GetCollection<WorkFlow>("ChildItems"); }
        }

        #region Methods
        private IList<Template> Templates {
            get {
                if (this.Action == WorkFlowAction.Message || this.Action == WorkFlowAction.Email)
                    return new XpoHelper(this.Session).GetObjects<Template>(CriteriaOperator.Parse("TemplateType == ?", this.ParentItem.SourceObject)).ToList();
                else
                    return null;
            }
        }

        private void LoadRecipientRole()
        {
            foreach (string name in Enum.GetNames(typeof(RecipientRoles)))
            {
                using (var uow = new UnitOfWork(this.Session.DataLayer))
                {
                    if (uow.FindObject<RecipientRole>(CriteriaOperator.Parse("Recipient == ?", name)) == null)
                    {
                        RecipientRole recipient = new RecipientRole(uow)
                        {
                            Recipient = name
                        };
                        uow.CommitChanges();
                    }
                }
            }
        }

        [VisibleInDetailView(true), VisibleInListView(false)]
        [Association("WorkFlow_WorkFlowProcess")]
        public XPCollection<WorkFlowProcess> WorkFlowProcess {
            get {
                return GetCollection<WorkFlowProcess>("WorkFlowProcess");
            }
        }

        [VisibleInDetailView(true), VisibleInListView(false)]
        [Association("WorkFlow_WorkFlowDesignTracking")]
        public XPCollection<WorkFlowDesignTracking> WorkFlowTracking {
            get {
                return GetCollection<WorkFlowDesignTracking>("WorkFlowTracking");
            }
        }
        #endregion

        #region Events
        protected override void OnSaving()
        {
            if (this.Action == WorkFlowAction.ExecuteAction)
            {
                this.ObjectState = WorkFlowObjectState.NotApplicable;
                this.Interval = WorkFlowInterval.NotApplicable;
                this.Expires = WorkFlowInterval.NotApplicable;
            }
            else{
                this.TargetObject = this.SourceObject;
            }

            base.OnSaving();
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            this.Output = WorkFlowOutput.PDF;
            this.Status = WorkFlowStatus.Enabled;
            this.Created = Constants.DateTimeTimeZone(this.Session);
            this.Expires = WorkFlowInterval.Never;
            this.Interval = WorkFlowInterval.Immediately;

            this.LoadRecipientRole();
        }
        #endregion
    }
}
