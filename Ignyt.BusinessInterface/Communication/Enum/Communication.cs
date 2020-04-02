using DevExpress.Persistent.Base;

namespace Ignyt.BusinessInterface.Communication.Enum {
    public enum CommunicationNotificationStatus
    {
        [ImageName("State_Validation_Skipped")]
        Queued = 0,
        [ImageName("State_Validation_Valid")]
        Successful = 1,
        [ImageName("State_Validation_InValid")]
        Unsuccessful = 2,
        [ImageName("State_Validation_Information")]
        Accepted = 3,
        [ImageName("State_Validation_Warning")]
        SentUnknown = 4,
        [ImageName("State_Validation_Information")]
        SystemError = 5,
        [ImageName("State_Validation_Information")]
        UpdateReceived = 6
    }
}
