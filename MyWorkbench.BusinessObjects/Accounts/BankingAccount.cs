using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using System;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Accounts
{
    [DefaultClassOptions, DefaultProperty("Description")]
    [NavigationItem("Banking")]
    [ImageName("BO_Resume")]
    public class BankingAccount : BaseObject
    {
        public BankingAccount(Session session)
            : base(session)
        {
        }

        private string fDescription;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }

        private DateTime fDateTime;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [NonCloneable]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public DateTime Created {
            get => fDateTime;
            set => SetPropertyValue(nameof(DateTime), ref fDateTime, value);
        }

        private Party fParty;
        [DevExpress.Xpo.DisplayName("Created By")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [NonCloneable]
        public Party Party {
            get => fParty;
            set => SetPropertyValue(nameof(Party), ref fParty, value);
        }

        [Association("BankingAccount_Banking"), Aggregated]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [NonCloneable]
        [DevExpress.Xpo.DisplayName("Bank Transtions")]
        public XPCollection<Banking> Banking {
            get {
                return GetCollection<Banking>("Banking");
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                if (this.Party == null)
                {
                    if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                        this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);
                }

                this.Created = Constants.DateTimeTimeZone(this.Session);
            }
        }
    }
}
