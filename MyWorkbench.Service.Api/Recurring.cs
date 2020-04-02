using MyWorkbench.BusinessObjects;
using MyWorkbench.BusinessObjects.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWorkBench.Service.Api
{
    public class Recurring : ApiBase {
        public Recurring(string ConnectionString) : base(ConnectionString)  {}

        protected override void ProcessClient() {
            try {
               base.ProcessClient();

               this.ProcessRecurrings();
            } catch (Exception ex) {
                throw ex;
            }
        }

        private void ProcessRecurrings() {
            this.ProcessRecurringJobCard();
            this.ProcessRecurringInvoice();
            this.ProcessRecurringBooking();
            this.ProcessRecurringTask();
            this.DataSourceHelper.Dispose();
        }

        private void ProcessRecurringJobCard()
        {
            try
            {
                List<RecurringJobCard> recurringObjects = this.DataSourceHelper.Select<RecurringJobCard>(typeof(RecurringJobCard), "(NextDate Is Null or CurrentDateTime >= NextDate) and Starting <= CurrentDateTime").OfType<RecurringJobCard>().ToList();

                if (recurringObjects.Count >= 1)
                    recurringObjects.ForEach(ProcessRecurringJobCard);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessRecurringTask()
        {
            try
            {
                List<RecurringTask> recurringObjects = this.DataSourceHelper.Select<RecurringTask>(typeof(RecurringTask), "(NextDate Is Null or CurrentDateTime >= NextDate) and Starting <= CurrentDateTime").OfType<RecurringTask>().ToList();

                if (recurringObjects.Count >= 1)
                    recurringObjects.ForEach(ProcessRecurringTask);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessRecurringBooking()
        {
            try
            {
                List<RecurringBooking> recurringObjects = this.DataSourceHelper.Select<RecurringBooking>(typeof(RecurringBooking), "(NextDate Is Null or CurrentDateTime >= NextDate) and Starting <= CurrentDateTime").OfType<RecurringBooking>().ToList();

                if (recurringObjects.Count >= 1)
                    recurringObjects.ForEach(ProcessRecurringBooking);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessRecurringInvoice()
        {
            try
            {
                List<RecurringInvoice> recurringObjects = this.DataSourceHelper.Select<RecurringInvoice>(typeof(RecurringInvoice), "(NextDate Is Null or CurrentDateTime >= NextDate) and Starting <= CurrentDateTime").OfType<RecurringInvoice>().ToList();

                if (recurringObjects.Count >= 1)
                    recurringObjects.ForEach(ProcessRecurringInvoice);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessRecurringJobCard(RecurringJobCard RecurringJobCard)
        {
            try
            {
                if (RecurringJobCard.WorkFlowStatus == Ignyt.BusinessInterface.WorkFlow.Enum.WorkFlowStatus.Enabled)
                {
                    MyWorkbench.BusinessObjects.Helpers.RecurringHelper.ExecuteRecurring(RecurringJobCard);
                }
            }
            catch (Exception ex)
            {
                this.TaskExceptionList.TaskExceptions.Add(ex);
            }
        }

        private void ProcessRecurringInvoice(RecurringInvoice RecurringInvoice)
        {
            try
            {
                if (RecurringInvoice.WorkFlowStatus == Ignyt.BusinessInterface.WorkFlow.Enum.WorkFlowStatus.Enabled)
                {
                    MyWorkbench.BusinessObjects.Helpers.RecurringHelper.ExecuteRecurring(RecurringInvoice);
                }
            }
            catch (Exception ex)
            {
                this.TaskExceptionList.TaskExceptions.Add(ex);
            }
        }

        private void ProcessRecurringTask(RecurringTask RecurringTask)
        {
            try
            {
                if (RecurringTask.WorkFlowStatus == Ignyt.BusinessInterface.WorkFlow.Enum.WorkFlowStatus.Enabled)
                {
                    MyWorkbench.BusinessObjects.Helpers.RecurringHelper.ExecuteRecurring(RecurringTask);
                }
            }
            catch (Exception ex)
            {
                this.TaskExceptionList.TaskExceptions.Add(ex);
            }
        }

        private void ProcessRecurringBooking(RecurringBooking RecurringBooking)
        {
            try
            {
                if (RecurringBooking.WorkFlowStatus == Ignyt.BusinessInterface.WorkFlow.Enum.WorkFlowStatus.Enabled)
                {
                    MyWorkbench.BusinessObjects.Helpers.RecurringHelper.ExecuteRecurring(RecurringBooking);
                }
            }
            catch (Exception ex)
            {
                this.TaskExceptionList.TaskExceptions.Add(ex);
            }
        }
    }
}
