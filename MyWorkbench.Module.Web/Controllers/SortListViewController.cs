using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using DevExpress.ExpressApp.Model;
using Ignyt.BusinessInterface.Attributes;

namespace MyWorkbench.Module.Web.Controllers {
    public class SortListViewController : ViewController<ListView> {
        public SortListViewController() {
        }

        protected override void OnActivated() {
            Type type = this.View.ObjectTypeInfo.Type;

            List<PropertyInfo> properties = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ListViewSort))).ToList();

            //Dennis: This code applies a client side sorting.
            foreach (PropertyInfo propertyInfo in properties) {
                IModelColumn columnInfo = ((IModelList<IModelColumn>)View.Model.Columns)[propertyInfo.Name];

                if (columnInfo != null) {
                    columnInfo.SortIndex = 0;
                    columnInfo.SortOrder = ((ListViewSort)propertyInfo.GetCustomAttribute(typeof(ListViewSort))).sortingDirection;
                }
            }
        }
    }
}
