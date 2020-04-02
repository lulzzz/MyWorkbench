using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using MyWorkbench.BusinessObjects.WorkFlow;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public static class WorkFlowHelper
    {
        public static void CreateWorkFlowProcess(BaseObject BaseObject)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(BaseObject.Session.DataLayer))
            {
                WorkFlowProcess workFlowProcess = new WorkFlowProcess(unitOfWork)
                {
                    Type = BaseObject.GetType().ToString(),
                    Criterion = string.Format("[Oid] = '{0}'", BaseObject.Oid),
                    DateReceived = MyWorkbench.BaseObjects.Constants.Constants.DateTimeTimeZone(unitOfWork),
                    IsNewObject = BaseObject.Session.IsNewObject(BaseObject),
                    DateToExecute = MyWorkbench.BaseObjects.Constants.Constants.DateTimeTimeZone(unitOfWork)
                };

                unitOfWork.CommitChanges();

                CreateWorkFlowProcessTracking(workFlowProcess, "WorkFlow created");
            }
        }

        public static void CreateWorkFlowProcessTracking(MyWorkbench.BusinessObjects.WorkFlow.WorkFlowProcess WorkFlowProcess, string Description)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(WorkFlowProcess.Session.DataLayer))
            {
                MyWorkbench.BusinessObjects.WorkFlow.WorkFlowProcess workFlowProcess = unitOfWork.FindObject<MyWorkbench.BusinessObjects.WorkFlow.WorkFlowProcess>(CriteriaOperator.Parse("Oid == ?", WorkFlowProcess.Oid));

                workFlowProcess.WorkFlowProcessTracking.Add(new WorkFlowProcessTracking(unitOfWork) { Description = Description });

                unitOfWork.CommitChanges();
            }
        }
    }
}   