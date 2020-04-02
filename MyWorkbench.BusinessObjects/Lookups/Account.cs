using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups
{
    [DefaultClassOptions, DefaultProperty("AccountName")]
    [NavigationItem("Lookups")]
    [ImageName("BO_Resume")]
    public class Account : BaseObject {
        public Account(Session session)
            : base(session) {
        }

        #region Properties
        private string fAccountName;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        [RuleRequiredField(DefaultContexts.Save)]
        public string AccountName {
            get => fAccountName;
            set => SetPropertyValue(nameof(AccountName), ref fAccountName, value);
        }

        private AccountCategory fAccountCategory;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        [NonCloneable]
        public AccountCategory AccountCategory {
            get => fAccountCategory;
            set => SetPropertyValue(nameof(AccountCategory), ref fAccountCategory, value);
        }

        private VATType fVATType;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        [NonCloneable]
        public VATType VATType {
            get => fVATType;
            set => SetPropertyValue(nameof(VATType), ref fVATType, value);
        }

        private string fDescription;
        [VisibleInDetailView(true), VisibleInListView(false)]
        [NonCloneable]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        private bool fActive;
        [VisibleInDetailView(true), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [NonCloneable]
        public bool Active {
            get => fActive;
            set => SetPropertyValue(nameof(Active), ref fActive, value);
        }
        #endregion

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                this.Active = true;
            }
        }
    }
}
