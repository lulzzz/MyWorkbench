using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using MyWorkbench.BaseObjects.Constants;
using System;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Ignyt.BusinessInterface.Attributes;
using MyWorkbench.BusinessObjects.Utils;

namespace MyWorkbench.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem(false)]
    [DefaultProperty("Description")]
    [ImageName("BO_Resume")]
    [Appearance("HideActions", AppearanceItemType = "Action", TargetItems = "Edit;New;Link;Unlink;Delete", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class AccountingExport : BaseObject
    {
        public AccountingExport(Session session)
            : base(session)
        {
        }

        #region Properties
        private Guid fObjectOid;
        [VisibleInListView(false), VisibleInDetailView(false)]
        public Guid ObjectOid {
            get {
                return fObjectOid;
            }
            set {
                SetPropertyValue("ObjectOid", ref fObjectOid, value);
            }
        }

        private Type fObjectType;
        [VisibleInListView(false), VisibleInDetailView(false)]
        [ValueConverter(typeof(TypeToStringConverter))]
        public Type ObjectType {
            get {
                return fObjectType;
            }
            set {
                SetPropertyValue("ObjectType", ref fObjectType, value);
            }
        }

        private Party fParty;
        [VisibleInListView(true), VisibleInDetailView(false)]
        public Party Party {
            get => fParty;
            set => SetPropertyValue(nameof(Party), ref fParty, value);
        }

        private DateTime fCreated;
        [VisibleInListView(true), VisibleInDetailView(false)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        [ListViewSort(DevExpress.Data.ColumnSortOrder.Descending)]
        public DateTime Created {
            get => fCreated;
            set => SetPropertyValue(nameof(Created), ref fCreated, value);
        }

        private string fDescription;
        [VisibleInListView(true), VisibleInDetailView(true)]
        [Size(SizeAttribute.Unlimited)]
        public string Description {
            get => fDescription;
            set => SetPropertyValue(nameof(Description), ref fDescription, value);
        }
        #endregion

        #region Events
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (this.Session.IsNewObject(this))
            {
                if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                    this.Party = this.Session.GetObjectByKey<Party>((Guid)SecuritySystem.CurrentUserId);
                
                this.Created = Constants.DateTimeTimeZone(this.Session);
            }
        }
        #endregion
    }
}
