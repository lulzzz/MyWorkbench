using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Ignyt.BusinessInterface.WorkFlow.Enum;
using MyWorkbench.BusinessObjects.Accounts;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using MyWorkbench.BaseObjects.Constants;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public static class RecurringHelper
    {
        public static void ExecuteRecurring(RecurringJobCard RecurringJobCard)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(RecurringJobCard.Session.DataLayer))
            {
                var myCloner = new DevExpress.Persistent.Base.Cloner();

                RecurringJobCard recurringJobCard = unitOfWork.FindObject<RecurringJobCard>(CriteriaOperator.Parse("Oid == ?", RecurringJobCard.Oid));

                if (recurringJobCard.WorkFlowStatus == WorkFlowStatus.Enabled && (recurringJobCard.NextDate == null || recurringJobCard.NextDate <= MyWorkbench.BaseObjects.Constants.Constants.DateTimeTimeZone(unitOfWork)))
                {
                    JobCard jobCard = new JobCard(unitOfWork);

                    recurringJobCard.CopyProperties(jobCard);
                    jobCard.BookedTime = recurringJobCard.NextDate != null ? recurringJobCard.NextDate : recurringJobCard.Starting;
                    jobCard.BookedTimeEnd = ((DateTime)jobCard.BookedTime).AddHours(Constants.AppointmentLength(jobCard.Session));
                    jobCard.Party = recurringJobCard.Party;

                    if (recurringJobCard.Items.Count >= 1)
                    {
                        var j = recurringJobCard.Items.Count;

                        for (int i = 0; i < j; i++)
                        {
                            jobCard.Items.Add(myCloner.CloneTo(recurringJobCard.Items[i], typeof(WorkflowItem)) as WorkflowItem);
                        }
                    }

                    if (recurringJobCard.WorkflowResources.Count >= 1)
                    {
                        var j = recurringJobCard.WorkflowResources.Count;

                        for (int i = 0; i < j; i++)
                        {
                            jobCard.WorkflowResources.Add(myCloner.CloneTo(recurringJobCard.WorkflowResources[i], typeof(WorkflowResource)) as WorkflowResource);
                        }
                    }

                    if (recurringJobCard.Equipment.Count >= 1)
                    {
                        var j = recurringJobCard.Equipment.Count;

                        for (int i = 0; i < j; i++)
                        {
                            jobCard.Equipment.Add(myCloner.CloneTo(recurringJobCard.Equipment[i], typeof(WorkFlowEquipment)) as WorkFlowEquipment);
                        }
                    }

                    recurringJobCard.AdvanceNextDate();

                    // New Object Tracking
                    (jobCard as WorkflowBase).ChildItems.Add(recurringJobCard);
                    (jobCard as WorkflowBase).Tracking.Add(CreateTracking(unitOfWork, jobCard, string.Format("Created from {0}", recurringJobCard.No)));

                    //Current Object Tracking
                    recurringJobCard.ChildItems.Add(jobCard as WorkflowBase);
                    recurringJobCard.Tracking.Add(CreateTracking(unitOfWork, jobCard, string.Format("Converted to {0}", (jobCard as WorkflowBase).GetType().Name)));
                }

                unitOfWork.CommitChanges();
            }
        }

        public static void ExecuteRecurring(RecurringInvoice RecurringInvoice)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(RecurringInvoice.Session.DataLayer))
            {
                var myCloner = new DevExpress.Persistent.Base.Cloner();

                RecurringInvoice recurringInvoice = unitOfWork.FindObject<RecurringInvoice>(CriteriaOperator.Parse("Oid == ?", RecurringInvoice.Oid));
                Invoice invoice = null;

                if (recurringInvoice.WorkFlowStatus == WorkFlowStatus.Enabled && (recurringInvoice.NextDate == null || recurringInvoice.NextDate <= MyWorkbench.BaseObjects.Constants.Constants.DateTimeTimeZone(unitOfWork)))
                {
                    invoice = new Invoice(unitOfWork);

                    recurringInvoice.CopyProperties(invoice);
                    invoice.Party = recurringInvoice.Party;

                    if (recurringInvoice.Items.Count >= 1)
                    {
                        var j = recurringInvoice.Items.Count;

                        for (int i = 0; i < j; i++)
                        {
                            invoice.Items.Add(myCloner.CloneTo(recurringInvoice.Items[i],typeof(WorkflowItem)) as WorkflowItem);
                        }
                    }

                    if (recurringInvoice.WorkflowResources.Count >= 1)
                    {
                        var j = recurringInvoice.WorkflowResources.Count;

                        for (int i = 0; i < j; i++)
                        {
                            invoice.WorkflowResources.Add(myCloner.CloneTo(recurringInvoice.WorkflowResources[i], typeof(WorkflowResource)) as WorkflowResource);
                        }
                    }

                    recurringInvoice.AdvanceNextDate();

                    // New Object Tracking
                    (invoice as WorkflowBase).ChildItems.Add(recurringInvoice);
                    (invoice as WorkflowBase).Tracking.Add(CreateTracking(unitOfWork, invoice, string.Format("Created from {0}", recurringInvoice.No)));

                    //Current Object Tracking
                    recurringInvoice.ChildItems.Add(invoice as WorkflowBase);
                    recurringInvoice.Tracking.Add(CreateTracking(unitOfWork, invoice, string.Format("Converted to {0}", (invoice as WorkflowBase).GetType().Name)));
                }

                unitOfWork.CommitChanges();
            }
        }

        public static void ExecuteRecurring(RecurringTask RecurringTask)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(RecurringTask.Session.DataLayer))
            {
                var myCloner = new DevExpress.Persistent.Base.Cloner();

                RecurringTask recurringTask = unitOfWork.FindObject<RecurringTask>(CriteriaOperator.Parse("Oid == ?", RecurringTask.Oid));
                Task task = null;

                if (recurringTask.WorkFlowStatus == WorkFlowStatus.Enabled && (recurringTask.NextDate == null || recurringTask.NextDate <= MyWorkbench.BaseObjects.Constants.Constants.DateTimeTimeZone(unitOfWork)))
                {
                    task = new Task(unitOfWork);

                    recurringTask.CopyProperties(task);
                    task.BookedTime = recurringTask.NextDate != null ? recurringTask.NextDate : recurringTask.Starting;
                    task.BookedTimeEnd = ((DateTime)task.BookedTime).AddHours(Constants.AppointmentLength(task.Session));
                    task.Party = recurringTask.Party;

                    if (recurringTask.WorkflowResources.Count >= 1)
                    {
                        var j = recurringTask.WorkflowResources.Count;

                        for (int i = 0; i < j; i++)
                        {
                            task.WorkflowResources.Add(myCloner.CloneTo(recurringTask.WorkflowResources[i], typeof(WorkflowResource)) as WorkflowResource);
                        }
                    }

                    recurringTask.AdvanceNextDate();

                    // New Object Tracking
                    (task as WorkflowBase).ChildItems.Add(recurringTask);
                    (task as WorkflowBase).Tracking.Add(CreateTracking(unitOfWork, task, string.Format("Created from {0}", recurringTask.No)));

                    //Current Object Tracking
                    recurringTask.ChildItems.Add(task as WorkflowBase);
                    recurringTask.Tracking.Add(CreateTracking(unitOfWork, task, string.Format("Converted to {0}", (task as WorkflowBase).GetType().Name)));
                }

                unitOfWork.CommitChanges();
            }
        }

        public static void ExecuteRecurring(RecurringBooking RecurringBooking)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(RecurringBooking.Session.DataLayer))
            {
                var myCloner = new DevExpress.Persistent.Base.Cloner();

                RecurringBooking recurringBooking = unitOfWork.FindObject<RecurringBooking>(CriteriaOperator.Parse("Oid == ?", RecurringBooking.Oid));
                Booking booking = null;

                if (recurringBooking.WorkFlowStatus == WorkFlowStatus.Enabled && (recurringBooking.NextDate == null || recurringBooking.NextDate <= MyWorkbench.BaseObjects.Constants.Constants.DateTimeTimeZone(unitOfWork)))
                {
                    booking = new Booking(unitOfWork);

                    recurringBooking.CopyProperties(booking);
                    booking.BookedTime = recurringBooking.NextDate != null ? recurringBooking.NextDate : recurringBooking.Starting;
                    booking.Party = recurringBooking.Party;

                    if (recurringBooking.Items.Count >= 1)
                    {
                        var j = recurringBooking.Items.Count;

                        for (int i = 0; i < j; i++)
                        {
                            booking.Items.Add(myCloner.CloneTo(recurringBooking.Items[i], typeof(WorkflowItem)) as WorkflowItem);
                        }
                    }

                    if (recurringBooking.WorkflowResources.Count >= 1)
                    {
                        var j = recurringBooking.WorkflowResources.Count;

                        for (int i = 0; i < j; i++)
                        {
                            booking.WorkflowResources.Add(myCloner.CloneTo(recurringBooking.WorkflowResources[i], typeof(WorkflowResource)) as WorkflowResource);
                        }
                    }

                    recurringBooking.AdvanceNextDate();

                    // New Object Tracking
                    (booking as WorkflowBase).ChildItems.Add(recurringBooking);
                    (booking as WorkflowBase).Tracking.Add(CreateTracking(unitOfWork, booking, string.Format("Created from {0}", recurringBooking.No)));

                    //Current Object Tracking
                    recurringBooking.ChildItems.Add(booking as WorkflowBase);
                    recurringBooking.Tracking.Add(CreateTracking(unitOfWork, booking, string.Format("Converted to {0}", (booking as WorkflowBase).GetType().Name)));
                }

                unitOfWork.CommitChanges();
            }
        }

        private static WorkflowTracking CreateTracking(Session session, WorkflowBase WorkflowBase, string Description)
        {
            WorkflowTracking tracking = new WorkflowTracking(session)
            {
                Workflow = WorkflowBase,
                Description = Description
            };

            return tracking;
        }
    }
}   