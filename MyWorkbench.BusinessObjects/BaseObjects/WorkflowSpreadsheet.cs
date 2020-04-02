using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Attributes;
using System;
using System.ComponentModel;
using DevExpress.ExpressApp.Editors;

namespace MyWorkbench.BusinessObjects.BaseObjects {
    [DefaultClassOptions, DefaultProperty("Description")]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class WorkflowSpreadsheet : BaseObject, IWorkflowSpreadsheet<WorkflowBase>
    {
        public WorkflowSpreadsheet(Session session)
            : base(session) {
        }

        [Association("WorkflowBase_WorkflowSpreadsheet")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public XPCollection<WorkflowBase> Workflow {
            get {
                return GetCollection<WorkflowBase>("Workflow");
            }
        }

        private string fDescription;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true),VisibleInListView(true)]
        [NonCloneable]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        private byte[] fText;
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.SpreadsheetPropertyEditor)]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [DevExpress.Xpo.DisplayName("")]
        public byte[] Text {
            get { return fText; }
            set { SetPropertyValue(nameof(Text), ref fText, value); }
        }

        private DateTime fDateTime;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:d}")]
        [ModelDefault("EditMask", "d")]
        [VisibleInDetailView(false), VisibleInListView(true)]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        [NonCloneable]
        public DateTime DateTime {
            get => fDateTime;
            set => SetPropertyValue(nameof(DateTime), ref fDateTime, value);
        }

        private Party fParty;
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(false), VisibleInListView(true)]
        [DevExpress.Xpo.DisplayName("Created By")]
        [NonCloneable]
        public Party Party {
            get => fParty;
            set => SetPropertyValue(nameof(Party), ref fParty, value);
        }

        public override void AfterConstruction() {
            base.AfterConstruction();
            
            if (this.Session.IsNewObject(this))
            {
                if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);


                this.DateTime = Constants.DateTimeTimeZone(this.Session);
            }
        }
    }
}
