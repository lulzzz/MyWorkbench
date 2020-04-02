using DevExpress.Persistent.Base;
using Ignyt.BusinessInterface.Attributes;

namespace Ignyt.BusinessInterface.WorkFlow.Enum {
    public enum WorkFlowAction {
        Message = 0,
        Email = 1,
        ExecuteAction = 2
    }

    public enum WorkFlowActionType
    {
        Create = 0,
        Update = 1
    }

    public enum WorkFlowStatus {
        [ImageName("State_Validation_Valid")]
        [Color("#144955")]
        Enabled = 0,
        [ImageName("State_Validation_Invalid")]
        [Color("#a5c25c")]
        Disabled = 1
    }

    public enum WorkFlowRecipientType {
        To = 0,
        CC = 1,
        BCC = 2
    }

    public enum WorkFlowObjectState {
        [ImageName("Action_New")]
        New = 0,
        [ImageName("Action_ModelDifferences_Export")]
        NewAndUpdated = 1,
        [ImageName("Action_Edit")]
        Updated = 2,
        [ImageName("State_Validation_Warning")]
        NotApplicable = 3
    }

    public enum WorkFlowOutput
    {
        [ImageName("Action_Export_ToPDF")]
        [Color("#144955")]
        PDF = 0,
        [ImageName("Action_Export_Pivot")]
        [Color("#a5c25c")]
        ZIP = 1
    }

    public enum WorkFlowInterval {
        Weekly = 0,
        Fortnightly = 1,
        Monthly = 2,
        Annually = 3,
        BiAnnually = 4,
        Daily = 5,
        Quarterly = 6,
        Never = 7,
        [ImageName("State_Validation_Warning")]
        NotApplicable = 8,
        Immediately = 9
    }

    public enum WorkFlowExecutionStatus
    {
        Queued = 0, Successful = 1, Unsuccessful = 2, Actionexecuted = 3
    }
}
