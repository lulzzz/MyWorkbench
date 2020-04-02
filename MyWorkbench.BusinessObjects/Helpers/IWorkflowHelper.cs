using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using MyWorkbench.BusinessObjects.BaseObjects;
using System;
using System.Linq;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public static class IWorkflowHelper
    {
        public static WorkflowBase Convert(Session Session, Type ConvertToType, WorkflowBase CurrentObject)
        {
            object newObject = Activator.CreateInstance(ConvertToType, Session);
            CurrentObject.CopyProperties(newObject);

            for (int i = CurrentObject.Items.Count - 1; i >= 0; i--)
            {
                var workflowItem = new WorkflowItem(Session);

                CurrentObject.Items[i].CopyProperties(workflowItem);

                (newObject as WorkflowBase).Items.Add(workflowItem);
            }

            for (int i = CurrentObject.Attachments.Count - 1; i >= 0; i--)
            {
                var workFlowAttachment = new WorkFlowAttachment(Session);

                CurrentObject.Attachments[i].CopyProperties(workFlowAttachment);

                (newObject as WorkflowBase).Attachments.Add(workFlowAttachment);
            }

            (newObject as WorkflowBase).ChildItems.Add(CurrentObject);
            (newObject as WorkflowBase).Tracking.Add(CreateTracking(newObject as WorkflowBase, string.Format("Converted from {0}", CurrentObject.No)));

            return newObject as WorkflowBase;
        }

        public static void ConvertSaved(WorkflowBase TargetObject, WorkflowBase SourceObject)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(TargetObject.Session.DataLayer))
            {
                WorkflowBase sourceObject = unitOfWork.FindObject(SourceObject.GetType(), CriteriaOperator.Parse("Oid == ?", SourceObject.Oid)) as WorkflowBase;
                WorkflowBase targetObject = unitOfWork.FindObject(TargetObject.GetType(), CriteriaOperator.Parse("Oid == ?", TargetObject.Oid)) as WorkflowBase;

                sourceObject.WorkFlowTypeConvertedTo = targetObject.WorkFlowType;
                targetObject.WorkFlowTypeConvertedFrom = sourceObject.WorkFlowType;
                sourceObject.ChildItems.Add(targetObject as WorkflowBase);
                sourceObject.Tracking.Add(CreateTracking(sourceObject, string.Format("Converted to {0}", (targetObject as WorkflowBase).GetType().Name)));

                unitOfWork.CommitTransaction();
            }
        }

        public static WorkflowTracking CreateTracking(WorkflowBase WorkflowBase,string Description)
        {
            WorkflowTracking tracking = new WorkflowTracking(WorkflowBase.Session)
            {
                Workflow = WorkflowBase,
                Description = Description
            };

            return tracking;
        }

        public static double CalculateAmountOutstanding(this WorkflowBase Workflow)
        {
            if (!Workflow.Session.IsObjectsSaving)
            {
                if (Workflow.WorkFlowType == Ignyt.BusinessInterface.WorkFlowType.Purchase)
                    return Math.Round(Workflow.CostTotalIncl - Workflow.PaymentTotal - Workflow.CreditTotal, 2);
                else if (Workflow.WorkFlowType == Ignyt.BusinessInterface.WorkFlowType.SupplierInvoice)
                    return Math.Round(Workflow.CostTotalIncl - Workflow.PaymentTotal - Workflow.CreditTotal, 2);
                else
                    return Math.Round(Workflow.TotalIncl - Workflow.PaymentTotal - Workflow.CreditTotal - Workflow.Excess, 2);
            }
            else {
                return 0;
            }
        }

        public static double CalculateCreditTotal(this WorkflowBase Workflow)
        {
            if (!Workflow.Session.IsObjectsSaving)
            {
                if (Workflow.WorkFlowType == Ignyt.BusinessInterface.WorkFlowType.Purchase)
                    return Math.Round(Workflow.ChildItems.Where(g => g.WorkFlowType == Ignyt.BusinessInterface.WorkFlowType.SupplierCreditNote).ToList().Sum(g => g.CostTotalIncl), 2);
                else if (Workflow.WorkFlowType == Ignyt.BusinessInterface.WorkFlowType.SupplierInvoice)
                    return Math.Round(Workflow.ChildItems.Where(g => g.WorkFlowType == Ignyt.BusinessInterface.WorkFlowType.SupplierCreditNote).ToList().Sum(g => g.CostTotalIncl), 2);
                else
                    return Math.Round(Workflow.ChildItems.Where(g => g.WorkFlowType == Ignyt.BusinessInterface.WorkFlowType.CreditNote).ToList().Sum(g => g.TotalIncl), 2);


            }
            else
            {
                return 0;
            }
        }

        public static double CalculatePayment(this WorkflowBase Workflow)
        {
            if (!Workflow.Session.IsObjectsSaving)
            {
                return Math.Round(Workflow.Payments.ToList().Sum(g => g.Amount), 2);
            }
            else
            {
                return 0;
            }
        }

        public static double CalculateTotalCostExcl(this WorkflowBase Workflow)
        {
            if (!Workflow.Session.IsObjectsSaving)
            {
                return Math.Round(Workflow.Items.Sum(g => g.TotalCostExcl - (g.TotalCostExcl * Workflow.DiscountPercent / 100)), 2);
            }
            else
            {
                return 0;
            }
        }

        public static double CalculateSubTotalExcl(this WorkflowBase Workflow)
        {
            if (!Workflow.Session.IsObjectsSaving)
            {
                return Math.Round(Workflow.Items.Sum(g => g.TotalExcl), 2);
            }
            else
            {
                return 0;
            }
        }

        public static double CalculateTotalExcl(this WorkflowBase Workflow)
        {
            if (!Workflow.Session.IsObjectsSaving)
            {
                return Math.Round(Workflow.Items.Sum(g => g.TotalExcl - (g.TotalExcl * Workflow.DiscountPercent / 100)), 2);
            }
            else
            {
                return 0;
            }
        }

        public static double CalculateVATTotal(this WorkflowBase Workflow)
        {
            if (!Workflow.Session.IsObjectsSaving)
            {
                return Math.Round(Workflow.Items.Sum(g => g.VATTotal - (g.VATTotal * Workflow.DiscountPercent / 100)), 2);
            }
            else
            {
                return 0;
            }
        }

        public static double CalculateCostVATTotal(this WorkflowBase Workflow)
        {
            if (!Workflow.Session.IsObjectsSaving)
            {
                return Math.Round(Workflow.Items.Sum(g => g.CostVATTotal - (g.CostVATTotal * Workflow.DiscountPercent / 100)), 2);
            }
            else
            {
                return 0;
            }
        }

        public static double CalculateAdditionalAmount(this WorkflowBase Workflow)
        {
            if (!Workflow.Session.IsObjectsSaving)
            {
                return Math.Round((Workflow.TotalExcl + Workflow.VATTotal) * (Workflow.AdditionalPercent / 100), 2);
            }
            else
            {
                return 0;
            }
        }

        public static double CalculateTotalIncl(this WorkflowBase Workflow)
        {
            if (!Workflow.Session.IsObjectsSaving)
            {
                return Math.Round((Workflow.TotalExcl + Workflow.VATTotal) + Workflow.AdditionalAmount, 2);
            }
            else
            {
                return 0;
            }
        }

        public static double CalculateCostTotalIncl(this WorkflowBase Workflow)
        {
            if (!Workflow.Session.IsObjectsSaving)
            {
                return Math.Round((Workflow.TotalCostExcl + Workflow.CostVATTotal) + Workflow.AdditionalAmount, 2);
            }
            else
            {
                return 0;
            }
        }

        public static double CalculateDepositAmount(this WorkflowBase Workflow)
        {
            if (!Workflow.Session.IsObjectsSaving)
            {
                return Math.Round(Workflow.TotalIncl * (Workflow.DepositPercent / 100), 2);
            }
            else
            {
                return 0;
            }
        }
    }
}
