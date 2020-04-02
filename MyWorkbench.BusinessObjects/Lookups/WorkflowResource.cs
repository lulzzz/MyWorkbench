using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MyWorkbench.BusinessObjects.Lookups {
    [DefaultClassOptions]
    [DefaultProperty("Description")]
    [NavigationItem(false)]
    public class WorkflowResource : Resource, IWorkFlowResource
    {
        public WorkflowResource(Session session)
            : base(session) {
        }

        private Type fResourceType;
        public Type ResourceType {
            get => fResourceType;
            set => SetPropertyValue(nameof(ResourceType), ref fResourceType, value);
        }

        private Guid fObjectOid;
        public Guid ObjectOid {
            get => fObjectOid;
            set => SetPropertyValue(nameof(ObjectOid), ref fObjectOid, value);
        }

        private IEnumerable<IEmailAddress> fEmailAddresses;
        [NonPersistent]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<IEmailAddress> EmailAddresses {
            get {
                if (fEmailAddresses == null)
                {
                    List<IEmailAddress> items = new List<IEmailAddress>();

                    if (this.ResourceType != null)
                    {
                        if (this.Session.FindObject(this.ResourceType, CriteriaOperator.Parse("Oid = ?", this.Oid)) is IEmailAddress iEmailAddress)
                            items.AddUnique(iEmailAddress);
                    }

                    fEmailAddresses = items;
                }

                return fEmailAddresses;
            }
        }

        private IEnumerable<ICellNumber> fCellNumbers;
        [VisibleInDetailView(false), VisibleInListView(false)]
        public IEnumerable<ICellNumber> CellNumbers {
            get {
                if (fCellNumbers == null)
                {
                    List<ICellNumber> items = new List<ICellNumber>();

                    if (this.ResourceType != null)
                    {
                        if (this.Session.FindObject(this.ResourceType, CriteriaOperator.Parse("Oid = ?", this.Oid)) is ICellNumber iCellNumber)
                            items.AddUnique(iCellNumber);
                    }

                    fCellNumbers = items;
                }

                return fCellNumbers;
            }
        }

        public string Description {
            get {
                string result = this.Caption;

                if (this.ResourceType != null)
                    result = result + " - " + this.ResourceType.Name;

                return result;
            }
        }

        private System.Drawing.Image fImage;
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        [ImageEditorAttribute(DetailViewImageEditorFixedWidth = 160, DetailViewImageEditorFixedHeight = 160, ListViewImageEditorCustomHeight = 48)]
        public System.Drawing.Image Image {
            get {
                return fImage;
            }
            set {
                SetPropertyValue("Image", ref fImage, value);
            }
        }

        #region Collections
        [Association("WorkflowResource_WorkflowBase")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public XPCollection<WorkflowBase> WorkflowBases {
            get {
                return GetCollection<WorkflowBase>("WorkflowBases");
            }
        }

        [Association("WorkflowResource_WorkFlowTask")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public XPCollection<WorkFlowTask> WorkFlowTasks {
            get {
                return GetCollection<WorkFlowTask>("WorkFlowTasks");
            }
        }
        #endregion
    }
}
