using DevExpress.ExpressApp.CloneObject;
using DevExpress.Xpo.Metadata;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace Ignyt.Framework {
    public class MyCloner : Cloner
    {
        public override void CopyMemberValue(
            XPMemberInfo memberInfo, IXPSimpleObject sourceObject, IXPSimpleObject targetObject)
        {
            if (!memberInfo.IsAssociation)
            {
                base.CopyMemberValue(memberInfo, sourceObject, targetObject);
            }
        }
    }
}
