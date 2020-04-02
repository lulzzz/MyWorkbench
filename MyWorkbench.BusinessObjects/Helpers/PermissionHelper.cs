using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using System;

namespace MyWorkbench.BusinessObjects.Helpers {
    public static class PermissionHelper {
        public static void UpdatePermissionVisibility(ChoiceActionItem choiceActionItem, IObjectSpace objectSpace) {
            if (choiceActionItem.Data != null) {
                string typeName = choiceActionItem.Data.AsString();
                System.Type type = Type.GetType(typeName + ",MyWorkbench.BusinessObjects");
                if (SecuritySystem.IsGranted(new PermissionRequest(objectSpace, type, SecurityOperations.Create))) {
                    choiceActionItem.Active["Convert"] = true;
                } else {
                    choiceActionItem.Active["Convert"] = false;
                }
            }
        }
    }
}
