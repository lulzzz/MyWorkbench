using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
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
using Ignyt.BusinessInterface.Communication.Enum;
using MyWorkbench.BusinessObjects.Communication.Common;
using MyWorkbench.BusinessObjects.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MyWorkbench.BusinessObjects.Lookups;

namespace MyWorkbench.BusinessObjects.Communication
{
    [DefaultClassOptions]
    [DefaultProperty("Body")]
    [ImageName("BO_Resume")]
    [NavigationItem("Communication")]
    [Appearance("HideActions", AppearanceItemType = "Action", TargetItems = "Edit;New;Link;Unlink", Visibility = ViewItemVisibility.Hide, Enabled = false, Context = "Any")]
    public class Message : BaseObject, IDetailRowMode, IModal
    {
        public Message(Session session)
            : base(session)
        {
        }

        #region Properties
        private IMessagePopup fCurrentObject;
        [VisibleInListView(false), VisibleInDetailView(false)]
        public IMessagePopup CurrentObject {
            get {
                return fCurrentObject;
            }
            set {
                SetPropertyValue("CurrentObject", ref fCurrentObject, value);
                this.ObjectOid = (fCurrentObject as BaseObject).Oid;
                this.ObjectType = this.fCurrentObject.GetType();
            }
        }

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

        private DateTime fSent;
        [VisibleInListView(true), VisibleInDetailView(false)]
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public DateTime Sent {
            get => fSent;
            set => SetPropertyValue(nameof(Sent), ref fSent, value);
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        [Association("Message_VendorContact")]
        [DevExpress.Xpo.DisplayName("To")]
        [EditorAlias("CustomAddTokenCollectionEditor")]
        public XPCollection<VendorContact> ToCellNumbers {
            get {
                return GetCollection<VendorContact>("ToCellNumbers");
            }
        }

        private string fBody;
        [RuleRequiredField(CustomMessageTemplate = "Body cannot be empty", TargetContextIDs = "Immediate")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        [ModelDefault("RowCount", "5")]
        [Size(918)]
        public string Body {
            get => fBody;
            set => SetPropertyValue(nameof(Body), ref fBody, value);
        }

        private Template fTemplate;
        [DataSourceProperty("Templates")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [ImmediatePostData]
        public Template Template {
            get => fTemplate;
            set => SetPropertyValue(nameof(Template), ref fTemplate, value);
        }

        private CommunicationNotificationStatus fStatus;
        [VisibleInListView(true), VisibleInDetailView(false)]
        public CommunicationNotificationStatus Status {
            get => fStatus;
            set => SetPropertyValue(nameof(Status), ref fStatus, value);
        }

        private string fUniqueProviderIdentifier;
        [VisibleInListView(false), VisibleInDetailView(false)]
        public string UniqueProviderIdentifier {
            get => fUniqueProviderIdentifier;
            set => SetPropertyValue(nameof(UniqueProviderIdentifier), ref fUniqueProviderIdentifier, value);
        }

        [DevExpress.Xpo.DisplayName("Workflow")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public BaseObject BaseObject {
            get {
                return this.Session.FindObject(this.ObjectType, CriteriaOperator.Parse("Oid == ?", this.ObjectOid)) as BaseObject;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        private IList<Template> Templates {
            get {
                return new XpoHelper(this.Session).GetObjects<Template>(CriteriaOperator.Parse("TemplateType == ? && CommunicationType = 0", this.CurrentObject.GetType())).ToList();
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public BindingList<CellNumber> CellNumbers {
            get {
                BindingList<CellNumber> cellNumbers = new BindingList<CellNumber>();

                if (CurrentObject.CellNumbers != null)
                {
                    foreach (CellNumber cellNumber in CurrentObject.CellNumbers)
                    {
                        cellNumbers.Add(cellNumber);
                    }
                }
                return cellNumbers;
            }
        }

        [VisibleInListView(false), VisibleInDetailView(false)]
        public IEnumerable<KeyValuePair<string, string>> Messages {
            get {
                List<KeyValuePair<string, string>> cellNumbers = new List<KeyValuePair<string, string>>();

                foreach (VendorContact vendorContact in this.ToCellNumbers)
                {
                    cellNumbers.Add(new KeyValuePair<string, string>(vendorContact.CellNo, this.Body));
                }

                return cellNumbers;
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (propertyName == "Template")
                if (oldValue != newValue & newValue != null)
                {
                    this.Body = DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor.ReplacePlaceholders(this.Template.Body, CurrentObject).Replace("&nbsp;", " ");
                }
        }
        #endregion

        #region Collections
        [DevExpress.Xpo.DisplayName("Logs")]
        [Association("Message_Log")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public XPCollection<CommunicationLog> CommunicationLog {
            get {
                return GetCollection<CommunicationLog>("CommunicationLog");
            }
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
                this.Status = CommunicationNotificationStatus.Queued;
            }
        }
        #endregion
    }
}
