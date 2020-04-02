using DevExpress.ExpressApp.Editors;
using DevExpress.Web;
using DevExpress.ExpressApp.Web.Editors;

namespace MyWorkbench.Module.Web.Controllers {
    public class WebValueRangeController : ValueRangeController
    {
        protected override void SetRange(PropertyEditor editor)
        {
            if (editor is WebPropertyEditor)
            {
                if (((WebPropertyEditor)editor).Editor is ASPxSpinEdit spinEdit)
                {
                    spinEdit.MinValue = GetMinValue(editor.Model.ModelMember);
                    spinEdit.MaxValue = GetMaxValue(editor.Model.ModelMember);
                }
            }
        }
    }
}
