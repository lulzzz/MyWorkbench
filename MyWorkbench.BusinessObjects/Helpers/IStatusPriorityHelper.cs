using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using MyWorkbench.BusinessObjects.Lookups;
using Ignyt.BusinessInterface;
using DevExpress.Persistent.BaseImpl;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public static class IStatusPriorityHelper
    {
        public static void UpdateStatus(IStatusPriority<Status, Priority> IStatusPriority, Status Status)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork((IStatusPriority as BaseObject).Session.DataLayer))
            {
                IStatusPriority<Status, Priority> istatusPriority = unitOfWork.FindObject(IStatusPriority.GetType(), CriteriaOperator.Parse("Oid == ?", (IStatusPriority as BaseObject).Oid)) as IStatusPriority<Status, Priority>;

                istatusPriority.Status = unitOfWork.FindObject<Status>(CriteriaOperator.Parse("Oid == ?", Status.Oid));

                unitOfWork.CommitChanges();
            }
        }

        public static void UpdatePriority(IStatusPriority<Status, Priority> IStatusPriority, Priority Priority)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork((IStatusPriority as BaseObject).Session.DataLayer))
            {
                IStatusPriority<Status, Priority> istatusPriority = unitOfWork.FindObject(IStatusPriority.GetType(), CriteriaOperator.Parse("Oid == ?", (IStatusPriority as BaseObject).Oid)) as IStatusPriority<Status, Priority>;

                istatusPriority.Priority = Priority;

                unitOfWork.CommitChanges();
            }
        }
    }
}