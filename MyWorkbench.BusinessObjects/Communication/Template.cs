using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MyWorkbench.BusinessObjects.Communication {
    [DefaultClassOptions, DefaultProperty("FullDescription")]
    [NavigationItem("Communication")]
    [ImageName("Action_Debug_Breakpoint_Toggle")]
    public class Template : BaseObject, IPlaceHoldersProviderLight
    {
        public Template(Session session)
            : base(session) {
        }

        private Type fTemplateType;
        [ImmediatePostData]
        [ValueConverter(typeof(TypeToStringConverter))]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter<BaseObject>))]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [DevExpress.Xpo.DisplayName("Type")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Type TemplateType {
            get {
                return fTemplateType;
            }
            set {
                SetPropertyValue("TemplateType", ref fTemplateType, value);
            }
        }


        [VisibleInDetailView(false), VisibleInListView(true)]
        public string ObjectType {
            get {
                return this.TemplateType == null ? null : this.TemplateType.Name;
            }
        }

        private string fName;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public string Name {
            get {
                return fName;
            }
            set {
                SetPropertyValue("Name", ref fName, value);
            }
        }

        private CommunicationType fCommunicationType;
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        public CommunicationType CommunicationType {
            get {
                return fCommunicationType;
            }
            set {
                SetPropertyValue("CommunicationType", ref fCommunicationType, value);
            }
        }

        private string fSubject;
        [Appearance("SubjectVisibility", TargetItems = "Subject", Visibility = ViewItemVisibility.Hide, Criteria = "CommunicationType = 0", Context = "DetailView")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ToolTip("Email Subject")]
        public string Subject {
            get {
                return fSubject;
            }
            set {
                SetPropertyValue("Subject", ref fSubject, value);
            }
        }

        private string fBody;
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [EditorAlias("HtmlWithPlaceholdersLight"), Size(SizeAttribute.Unlimited)]
        [ToolTip("Enter your template text here. You can copy and paste placeholders from below to insert actual data into the template. Please note message do not support formatting. Any formatting you do for messages will be removed on saving")]
        public string Body {
            get {
                return fBody;
            }
            set {
                SetPropertyValue("Body", ref fBody, value);
            }
        }

        private bool fIncludeAttachment;
        [Appearance("IncludeAttachmentVisibility", TargetItems = "IncludeAttachment", Visibility = ViewItemVisibility.Hide, Criteria = "CommunicationType = 0", Context = "DetailView")]
        [VisibleInDetailView(true), VisibleInListView(true)]
        [ToolTip("Include a PDF copy of the document you are setting up the template for. E.g. if template type is job card a copy of the job card will be attached to the email")]
        public bool IncludeAttachments {
            get {
                return fIncludeAttachment;
            }
            set {
                SetPropertyValue("IncludeAttachments", ref fIncludeAttachment, value);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string FullDescription {
            get {
                return string.Format("{0}-{1}", this.Name, this.CommunicationType);
            }
        }

        #region Placeholders
        [Browsable(false)]
        public IList<String> Placeholders {
            get {
                List<String> placeholders = new List<String>();

                if (this.TemplateType != null)
                {
                    foreach (IMemberInfo member in XafTypesInfo.Instance.FindTypeInfo(this.TemplateType).Members.OrderBy(m => m.Name))
                        if (member.IsPersistent && member.IsProperty)
                            placeholders.Add(member.Name);
                }
                return placeholders;
            }
        }

        //private string fTextWithPlaceholders;
        //[EditorAlias(EditorAliases.DefaultPropertyEditor), Size(SizeAttribute.Unlimited)]
        //[VisibleInListView(false),VisibleInDetailView(false)]
        //public string TextWithPlaceholders {
        //    get {
        //        if(fTextWithPlaceholders == null)
        //            fTextWithPlaceholders = DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor.ReplacePlaceholders(this.Body, this).Replace("&nbsp;", " ");
        //        return fTextWithPlaceholders;
        //    }
        //    set {
        //        SetPropertyValue("TextWithPlaceholders", ref fTextWithPlaceholders, value);
        //    }
        //}
        #endregion

        #region Events
        public override void AfterConstruction() {
            base.AfterConstruction();
            this.CommunicationType = CommunicationType.Message;
        }

        protected override void OnSaving() {
            if (CommunicationType == CommunicationType.Message) Body = HtmlRemoval.StripTagsRegex(Body);
            base.OnSaving();
        }
        #endregion
    }
}
