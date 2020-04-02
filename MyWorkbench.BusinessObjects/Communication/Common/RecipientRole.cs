using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Communication.Common
{
    [DefaultProperty("Recipient")]
    [NavigationItem(false)]
    public class RecipientRole : BaseObject {
        public RecipientRole(Session session)
            : base(session) {
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Recipient {
            get { return GetPropertyValue<string>("Recipient"); }
            set { SetPropertyValue<string>("Recipient", value); }
        }

        [Association("ToRecipientRole_WorkFlow")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<MyWorkbench.BusinessObjects.WorkFlow.WorkFlow> ToWorkFlow => GetCollection<MyWorkbench.BusinessObjects.WorkFlow.WorkFlow>("ToWorkFlow");

        [Association("CCRecipientRole_WorkFlow")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<MyWorkbench.BusinessObjects.WorkFlow.WorkFlow> CCWorkFlow => GetCollection<MyWorkbench.BusinessObjects.WorkFlow.WorkFlow>("CCWorkFlow");

        [Association("BCCRecipientRole_WorkFlow")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public XPCollection<MyWorkbench.BusinessObjects.WorkFlow.WorkFlow> BCCWorkFlow => GetCollection<MyWorkbench.BusinessObjects.WorkFlow.WorkFlow>("BCCWorkFlow");
    }
}
