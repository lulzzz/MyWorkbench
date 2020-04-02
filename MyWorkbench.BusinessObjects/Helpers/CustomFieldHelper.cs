using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWorkbench.BusinessObjects.Helpers {
    public static class CustomFieldHelper { 
        public static void CreateCustomFields(ITypesInfo typesInfo, IObjectSpace ObjectSpace) {
            IList<CustomField> customFields = ObjectSpace.GetObjects<CustomField>(null).OrderBy(g => g.CustomType).ToList();

            //foreach (CustomField customField in customFields) {
            //    if (typesInfo.FindTypeInfo(customField.CustomType) is TypeInfo typeInfo) {
            //        typeInfo.CreateMember(customField.Name, customField.DataType == DataType.Numeric ? typeof(int) : customField.DataType == DataType.Text ? typeof(string) : typeof(DateTime));

            //        typeInfo.AddAttribute(new VisibleInDetailViewAttribute(true));
            //        XafTypesInfo.Instance.RefreshInfo(customField.CustomType);
            //    }
            //}
        }
    }
}
