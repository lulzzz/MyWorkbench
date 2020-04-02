using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Lookups;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public static class WorkFlowTimeTrackingMultipleHelper
    {
        public static void Execute(this WorkFlowTimeTrackingMultiple TimeTrackingMultiple)
        {
            WorkFlowTimeTracking timeTracking;

            foreach (Employee employee in TimeTrackingMultiple.Employee)
            {
                foreach (WorkFlowTimeTrackingMultipleItem item in TimeTrackingMultiple.WorkFlowTimeTrackingMultipleItems)
                {
                    timeTracking = new WorkFlowTimeTracking(TimeTrackingMultiple.Session)
                    {
                        Employee = employee,
                        Description = TimeTrackingMultiple.Description,
                        StartDateTime = item.StartDateTime,
                        EndDateTime = item.EndDateTime,
                        Workflow = TimeTrackingMultiple.Workflow
                    };

                    timeTracking.Save();
                }
            }
        }
    }
}
