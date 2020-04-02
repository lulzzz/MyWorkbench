using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using DevExpress.ExpressApp.Model;
using Ignyt.BusinessInterface.Attributes;
using DevExpress.ExpressApp.Web.Editors.ASPx;

namespace MyWorkbench.Module.Web.Controllers {
    public class ASPxGridLookupDetailController : ViewController<DetailView>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            foreach (ASPxGridLookupPropertyEditor propertyEditor in View.GetItems<ASPxGridLookupPropertyEditor>())
            {
                propertyEditor.ControlCreated += PropertyEditor_ControlCreated;
            }
        }
        protected override void OnDeactivated()
        {
            foreach (ASPxGridLookupPropertyEditor propertyEditor in View.GetItems<ASPxGridLookupPropertyEditor>())
            {
                propertyEditor.ControlCreated -= PropertyEditor_ControlCreated;
            }
            base.OnDeactivated();
        }
        
        private void PropertyEditor_ControlCreated(object sender, EventArgs e)
        {
            ASPxGridLookupPropertyEditor propertyEditor = (ASPxGridLookupPropertyEditor)sender;

            if (propertyEditor.Editor != null)
            {
                propertyEditor.Editor.GridView.SettingsPager.Visible = false;
                propertyEditor.Editor.GridView.SettingsPager.PageSize = 20;
            }
        }
    }
}
