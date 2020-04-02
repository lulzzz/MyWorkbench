using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Communication;
using MyWorkbench.BusinessObjects.BaseObjects;

namespace MyWorkbench.BusinessObjects.Communication
{
    [DefaultClassOptions]
    [NavigationItem(false)]
    [ImageName("BO_Resume")]
    public class EmailAttachment : AttachmentBase, IModal
    {
        public EmailAttachment(Session session)
            : base(session)
        {
        }

        private Email fEmail;
        [Association("Email_EmailAttachment")]
        public Email Email {
            get { return fEmail; }
            set {
                SetPropertyValue("Email", ref fEmail, value);
            }
        }
    }
}
