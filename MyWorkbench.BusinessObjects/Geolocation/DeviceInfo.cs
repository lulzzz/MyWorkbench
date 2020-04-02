using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace MyWorkbench.BusinessObjects.Geolocation
{
    public class DeviceInfo : BaseObject
    {
        public DeviceInfo(Session session)
            : base(session)
        {
        }

        private string fDeviceID;
        public string DeviceID {
            get {
                return fDeviceID;
            }
            set {
                SetPropertyValue("DeviceID", ref fDeviceID, value);
            }
        }
    }
}
