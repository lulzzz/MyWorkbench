using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using Ignyt.BusinessInterface;
using System.Collections.Generic;
using Ignyt.Framework.ExpressApp;
using Ignyt.BusinessInterface.WorkFlow.Enum;
using System.Reflection;
using MyWorkbench.BusinessObjects.WorkFlow;
using System;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public class WorkFlowExecute<T> where T : BaseObject, IWorkflowDesign
    {
        private T CurrentObject { get; set; }

        private XpoHelper fXpoHelper;
        private XpoHelper XpoHelper {
            get {
                if (fXpoHelper == null)
                    fXpoHelper = new XpoHelper(CurrentObject.Session);

                return fXpoHelper;
            }
        }

        private ICollection<WorkFlow.WorkFlow> fWorkFlows;
        private ICollection<WorkFlow.WorkFlow> WorkFlows {
            get {
                if (fWorkFlows == null)
                {
                    fWorkFlows = XpoHelper.GetObjects<WorkFlow.WorkFlow>(new BinaryOperator("SourceObject", CurrentObject.GetType()) & new BinaryOperator("Status", WorkFlowStatus.Enabled));

                }
                return fWorkFlows;
            }
        }

        public void Execute(T Object)
        {
            try
            {
                WorkFlowHelper.CreateWorkFlowProcess(Object);

                this.ExecuteActions(Object);
            }
            catch (Exception)
            {
                MessageProvider.RegisterMessage(new MessageInformation(MessageTypes.Error, string.Concat(this.CurrentObject.GetType().Name, " workflow actions executed unsuccessfully. See commication tracking for details.")));
            }
        }


        private void ExecuteActions(T Object)
        {
            try
            {
                this.CurrentObject = Object;

                this.WorkFlows.ForEach(ExecuteWorkFlow);

                MessageProvider.RegisterMessage(new MessageInformation(MessageTypes.Success, string.Concat(this.CurrentObject.GetType().Name, " workflow actions executed successfully. See commication tracking for details.")));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ExecuteWorkFlow(WorkFlow.WorkFlow SourceWorkflow)
        {
            try
            {
                ICollection<WorkFlow.WorkFlow> childExecutionActions = XpoHelper.GetObjects<WorkFlow.WorkFlow>(new BinaryOperator("Action", WorkFlowAction.ExecuteAction) & new BinaryOperator("ParentItem", SourceWorkflow) & new BinaryOperator("Status", WorkFlowStatus.Enabled));

                foreach (WorkFlow.WorkFlow workFlow in childExecutionActions)
                {
                    ExecuteAction(SourceWorkflow, workFlow);
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        private void ExecuteAction(WorkFlow.WorkFlow SourceWorkflow, WorkFlow.WorkFlow TargetWorkflow)
        {
            try
            {
                if ((this.CurrentObject as XPBaseObject).Fit(this.CurrentObject.Session.ParseCriteria(SourceWorkflow.SourceCriterion)))
                {
                    if (SourceWorkflow.SourceObject == TargetWorkflow.TargetObject)
                    {
                        ActionUpdate(this.CurrentObject, TargetWorkflow);
                        ActionTracking(SourceWorkflow, string.Concat(TargetWorkflow.TargetObject.Name, string.Empty, TargetWorkflow.FullDescription, " executed"));
                    }
                    else
                    {
                        foreach (PropertyInfo propertyInfo in this.CurrentObject.GetType().GetProperties())
                        {
                            IterateProperties(propertyInfo, TargetWorkflow);
                        }

                        ActionTracking(SourceWorkflow, string.Concat(TargetWorkflow.TargetObject.Name, string.Empty, TargetWorkflow.FullDescription, " executed"));
                    }
                }
            }
            catch (Exception ex)
            {
                ActionTracking(SourceWorkflow, string.Concat(TargetWorkflow.TargetObject.Name, ex.ToString(), TargetWorkflow.FullDescription, " executed"));
                throw ex;
            }
        }

        private void IterateProperties(PropertyInfo PropertyInfo, WorkFlow.WorkFlow WorkFlow)
        {
            if (PropertyInfo.PropertyType == WorkFlow.SourceObject)
            {
                ActionUpdate(PropertyInfo.GetValue(WorkFlow), WorkFlow);
            }
        }

        private void ActionUpdate(object value, WorkFlow.WorkFlow WorkFlow)
        {
            Dictionary<string, object> criteriaValues;

            using (UnitOfWork unitOfWork = new UnitOfWork(this.CurrentObject.Session.DataLayer))
            {
                object secondObject = unitOfWork.FindObject(value.GetType(), CriteriaOperator.Parse("Oid ==?", (value as BaseObject).Oid));
                WorkFlow.WorkFlow workFlow = unitOfWork.FindObject<WorkFlow.WorkFlow>(CriteriaOperator.Parse("Oid ==?", WorkFlow.Oid));

                criteriaValues = Extract(unitOfWork.ParseCriteria(workFlow.TargetCriterion));

                foreach (KeyValuePair<string, object> criteriaValue in criteriaValues)
                {
                    ActionUpdateProperty(criteriaValue, secondObject);
                }

                unitOfWork.CommitTransaction();
            }
        }

        private void ActionTracking(WorkFlow.WorkFlow WorkFlow, string Description)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(WorkFlow.Session.DataLayer))
            {
                WorkFlow.WorkFlow workFlow = unitOfWork.FindObject<WorkFlow.WorkFlow>(CriteriaOperator.Parse("Oid ==?", WorkFlow.Oid));

                WorkFlowDesignTracking workFlowTracking = new WorkFlowDesignTracking(unitOfWork)
                {
                    WorkFlow = workFlow,
                    Description = Description
                };

                unitOfWork.CommitTransaction();
            }
        }

        private void ActionUpdateProperty(KeyValuePair<string, object> CriteriaValue, object SecondObject)
        {
            SecondObject.SetPropertyValue(CriteriaValue.Key, CriteriaValue.Value);
        }

        private static Dictionary<string, object> Extract(CriteriaOperator op)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            GroupOperator opGroup = op as GroupOperator;

            if (ReferenceEquals(opGroup, null))
            {
                ExtractOne(dict, op);
            }
            else
            {
                if (opGroup.OperatorType == GroupOperatorType.And)
                {
                    foreach (var opn in opGroup.Operands)
                    {
                        ExtractOne(dict, opn);
                    }
                }
            }

            return dict;
        }

        private static void ExtractOne(Dictionary<string, object> dict, CriteriaOperator op)
        {
            BinaryOperator opBinary = op as BinaryOperator;

            if (ReferenceEquals(opBinary, null)) return;

            OperandProperty opProperty = opBinary.LeftOperand as OperandProperty;
            OperandValue opValue = opBinary.RightOperand as OperandValue;

            if (ReferenceEquals(opProperty, null) || ReferenceEquals(opValue, null)) return;

            dict.Add(opProperty.PropertyName, opValue.Value);
        }
    }
}