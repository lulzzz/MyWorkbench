using DevExpress.Persistent.BaseImpl;
using Ignyt.BusinessInterface.WorkFlow.Enum;
using System;
using System.Collections.Generic;

namespace Ignyt.BusinessInterface {
    public interface IWorkflowType
    {
        string Description { get; set; }
        bool ReduceInventory { get; set; }
    }

    public interface IWorkCalendarEvent { }

    public interface IWorkflow {
        string Prefix { get; set; }
        string No { get; set; }
        Party Party  {get; set; }
        DateTime Issued { get; set; }
        WorkFlowType WorkFlowType { get; }
        IStatus CurrentStatus { get; set; }
        IEnumerable<IWorkFlowResource> Assigned { get; set; }
        string FullDescription { get; }
    }

    public interface IWorkflowDesign { }

    public interface IRecurringWorkFlow
    {
        Guid Oid { get; }
        Interval IntervalPeriod { get; set; }
        DateTime Starting { get; set; }
        DateTime? NextDate { get; set; }
        WorkFlowStatus WorkFlowStatus { get; set; }
    }
}