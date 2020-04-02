using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Web.TestScripts;
using DevExpress.Web;
using DevExpress.Xpo;
using System;
using System.Web.UI.WebControls;
using System.ComponentModel;
using DevExpress.ExpressApp.Web;
using MyWorkbench.Module.Editors;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.FileAttachments.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;

namespace MyWorkbench.Module.Web.Editors
{
    #region WebCustomUserControlViewItem
    /// <summary>
    /// A custom Application Model element extension for the View Item node to be able to specify custom ASP.NET controls via the Model Editor.
    /// </summary>
    public interface IModelWebCustomUserControlViewItem : IModelCustomUserControlViewItem
    {
        [Category("Data")]
        string CustomControlPath { get; set; }
    }

    /// <summary>
    /// An custom View Item that hosts a custom ASP.NET user control (http://documentation.devexpress.com/#Xaf/CustomDocument2612) to show it in the XAF View.
    /// </summary>
    [ViewItem(typeof(IModelCustomUserControlViewItem))]
    public class WebCustomUserControlViewItem : CustomUserControlViewItem
    {
        protected IModelWebCustomUserControlViewItem model;
        public WebCustomUserControlViewItem(IModelViewItem model, Type objectType)
            : base(model, objectType)
        {
            this.model = model as IModelWebCustomUserControlViewItem;
            if (this.model == null)
                throw new ArgumentNullException("IModelWebCustomUserControlViewItem must extend IModelCustomUserControlViewItem in the ExtendModelInterfaces method of your Web ModuleBase descendant.");
        }

        protected override object CreateControlCore()
        {
            // You can access the View and other properties here to additionally initialize your control.
            System.Web.UI.Control userControl = WebWindow.CurrentRequestPage.LoadControl(model.CustomControlPath);
            userControl.ID = GetType().Name;
            return userControl;
        }
    }
    #endregion

    #region CustomDateTimeEditor
    [PropertyEditor(typeof(DateTime?), "CustomDateTimeEditor", false)]
    public class CustomDateTimeEditor : ASPxDateTimePropertyEditor
    {
        public CustomDateTimeEditor(Type objectType, IModelMemberViewItem info) :
            base(objectType, info)
        { }

        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            if (this.ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.Edit)
            {
                ASPxDateEdit aspxcontrol = control as ASPxDateEdit;
                aspxcontrol.TimeSectionProperties.Visible = true;
            }
        }
    }
    #endregion

    #region CustomTimeEditor
    [PropertyEditor(typeof(DateTime?), "CustomTimeEditor", false)]
    public class CustomTimeEditor : ASPxPropertyEditor, ITestable
    {
        private const string TimeFormat = "HH:mm";

        public CustomTimeEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        public new ASPxTimeEdit Editor {
            get { return (ASPxTimeEdit)base.Editor; }
        }
        private void SelectedDateChangedHandler(object source, EventArgs e)
        {
            FixYear(source as ASPxTimeEdit);

            base.EditValueChangedHandler(source, e);
        }
        protected override string GetPropertyDisplayValue()
        {
            if (object.Equals(base.PropertyValue, DateTime.MinValue) || !(base.PropertyValue is DateTime))
            {
                return string.Empty;
            }
            return ((DateTime)base.PropertyValue).ToString(TimeFormat);
        }
        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            if (control is ASPxTimeEdit)
            {
                ASPxTimeEdit aSPxDateEdit = (ASPxTimeEdit)control;
                //aSPxDateEdit.CalendarProperties.DisplayFormatString = base.DisplayFormat;
                aSPxDateEdit.EditFormat = EditFormat.Custom;
                aSPxDateEdit.EditFormatString = base.EditMask;
                aSPxDateEdit.DisplayFormatString = TimeFormat;
                //aSPxDateEdit.CalendarProperties.DaySelectedStyle.CssClass = "ASPxSelectedItem";
                aSPxDateEdit.DateChanged += new EventHandler(this.SelectedDateChangedHandler);
            }
        }
        protected override void SetImmediatePostDataScript(string script)
        {
            this.Editor.ClientSideEvents.ValueChanged = script;
        }
        protected override WebControl CreateEditModeControlCore()
        {
            return new ASPxTimeEdit();
        }
        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            if (this.Editor != null)
            {
                this.Editor.DateChanged -= new EventHandler(this.SelectedDateChangedHandler);
            }
            base.BreakLinksToControl(unwireEventsOnly);
        }

        private void FixYear(ASPxTimeEdit editor)
        {
            if (editor == null)
                return;

            if (editor.Value is DateTime)
            {
                DateTime time = (DateTime)editor.Value;
                editor.Value = time.AddYears(Math.Max(2000 - time.Year, 0));
            }
        }
    }
    #endregion

    #region WebProgressPropertyEditor
    [PropertyEditor(typeof(float), "CustomProgressEditor", false)]
    public class WebProgressPropertyEditor : ASPxPropertyEditor
    {
        public WebProgressPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        private void SetProgressValue()
        {
            if (!(InplaceViewModeEditor is TaskProgressBar progressBar))
                progressBar = Editor as TaskProgressBar;

            if (progressBar != null)
            {
                progressBar.ProgressValue = PropertyValue;
                progressBar.DisplayMode = ProgressBarDisplayMode.Percentage;

                if (Convert.ToDecimal(progressBar.ProgressValue) <= 30)
                    progressBar.IndicatorStyle.BackColor = System.Drawing.Color.Red;
                else if (Convert.ToDecimal(progressBar.ProgressValue) <= 70)
                    progressBar.IndicatorStyle.BackColor = System.Drawing.Color.Orange;
                else
                    progressBar.IndicatorStyle.BackColor = System.Drawing.Color.Green;
            }
        }
        protected override WebControl CreateEditModeControlCore()
        {
            TaskProgressBar result = new TaskProgressBar
            {
                Width = Unit.Percentage(100),
                ID = "TaskProgressBar"
            };
            return result;
        }
        protected override WebControl CreateViewModeControlCore()
        {
            return CreateEditModeControlCore();
        }
        protected override void ReadViewModeValueCore()
        {
            base.ReadViewModeValueCore();
            SetProgressValue();
        }
        protected override void ReadEditModeValueCore()
        {
            base.ReadEditModeValueCore();
            SetProgressValue();
        }
    }
    #endregion

    #region TaskProgressBar
    public class TaskProgressBar : ASPxProgressBar
    {
        private float progressValue = 0;
        public object ProgressValue {
            get { return progressValue; }
            set {
                progressValue = (float)value;
                this.Value = Convert.ToDecimal(progressValue);
            }
        }
    }
    #endregion

    #region CustomLookupPropertyEditor
    [PropertyEditor(typeof(object), "CustomLookupPropertyEditor", false)]
    public class CustomLookupPropertyEditor : ASPxLookupPropertyEditor
    {
        protected override void OnControlCreated()
        {
            base.OnControlCreated();
            if (ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.Edit)
            {
                DropDownEdit.DropDown.CallbackPageSize = 50;
                DropDownEdit.DropDown.EnableCallbackMode = true;
                DropDownEdit.DropDown.AnimationType = AnimationType.Fade;
                DropDownEdit.DropDown.EnableViewState = false;
                DropDownEdit.DropDown.ItemsRequestedByFilterCondition += DropDown_ItemsRequestedByFilterCondition;
                DropDownEdit.AddingEnabled = true;
            }
        }

        private void DropDown_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            XPCollection collection = (source as ASPxComboBox).DataSource as XPCollection;
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;
        }

        public CustomLookupPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
    }
    #endregion

    #region IFileManager
    public interface IModelFileManager : IModelViewItem { }

    public interface IFileManager
    {
        string RootFolder { get; set; }
    }

    [ViewItem(typeof(IModelFileManager))]
    public class MyFileManagerViewItem : ViewItem
    {

        protected override object CreateControlCore()
        {
            System.Web.UI.Control userControl = WebWindow.CurrentRequestPage.LoadControl("CustomControls\\FileManager.ascx");
            ((IFileManager)userControl).RootFolder = "Obj";
            return userControl;
        }
        public MyFileManagerViewItem(IModelViewItem modelNode, Type objectType) : base(objectType, modelNode.Id) { }
    }
    #endregion
}
