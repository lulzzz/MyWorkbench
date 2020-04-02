using DevExpress.ExpressApp.SystemModule.Notifications;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using MyWorkbench.BusinessObjects.Lookups;
using System.Collections.Generic;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public static class EventEntryHelper
    {
        public static void Execute<T>(T EventEntryObject) where T : BaseObject, IEventEntryObject<WorkCalendarEvent>
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(EventEntryObject.Session.DataLayer))
            {
                T eventEntryHelper = unitOfWork.FindObject(EventEntryObject.GetType(), CriteriaOperator.Parse("Oid == ?", EventEntryObject.Oid)) as T;

                if (eventEntryHelper.BookedTime != null & eventEntryHelper.BookedTimeEnd != null)
                {
                    InitializeEvent(eventEntryHelper, unitOfWork);

                    unitOfWork.CommitTransaction();
                }
            }
        }

        public static void Execute(Session Session, Guid Oid, DateTime BookedTime, DateTime BookedTimeEnd, object Status, object Priority, List<object> resources)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(Session.DataLayer))
            {
                WorkCalendarEvent workCalendarEvent = unitOfWork.FindObject<WorkCalendarEvent>(CriteriaOperator.Parse("Oid == ?", Oid));

                if (workCalendarEvent.Project != null)
                {
                    workCalendarEvent.Project.BookedTime = BookedTime;
                    workCalendarEvent.Project.BookedTimeEnd = BookedTimeEnd;
                    workCalendarEvent.Project.Status = unitOfWork.FindObject<Status>(CriteriaOperator.Parse("UniqueID == ?", Status.ToString()));
                    workCalendarEvent.Project.Priority = (Ignyt.BusinessInterface.Priority)Priority;

                    foreach(object obj in resources)
                        workCalendarEvent.Project.WorkflowResources.Add(unitOfWork.FindObject<WorkflowResource>(CriteriaOperator.Parse("Oid == ?", Guid.Parse(obj.ToString()))));
                }
                else if (workCalendarEvent.WorkFlowTask != null)
                {
                    workCalendarEvent.WorkFlowTask.BookedTime = BookedTime;
                    workCalendarEvent.WorkFlowTask.BookedTimeEnd = BookedTimeEnd;
                    workCalendarEvent.WorkFlowTask.Status = unitOfWork.FindObject<Status>(CriteriaOperator.Parse("UniqueID == ?", Status.ToString()));
                    workCalendarEvent.WorkFlowTask.Priority = (Ignyt.BusinessInterface.Priority)Priority;

                    foreach (object obj in resources)
                        workCalendarEvent.WorkFlowTask.WorkflowResources.Add(unitOfWork.FindObject<WorkflowResource>(CriteriaOperator.Parse("Oid == ?", Guid.Parse(obj.ToString()))));
                }
                else if (workCalendarEvent.JobCard != null)
                {
                    workCalendarEvent.JobCard.BookedTime = BookedTime;
                    workCalendarEvent.JobCard.BookedTimeEnd = BookedTimeEnd;
                    workCalendarEvent.JobCard.Status = unitOfWork.FindObject<Status>(CriteriaOperator.Parse("UniqueID == ?", Status.ToString()));
                    workCalendarEvent.JobCard.Priority = (Ignyt.BusinessInterface.Priority)Priority;

                    foreach (object obj in resources)
                        workCalendarEvent.JobCard.WorkflowResources.Add(unitOfWork.FindObject<WorkflowResource>(CriteriaOperator.Parse("Oid == ?", Guid.Parse(obj.ToString()))));
                }

                unitOfWork.CommitTransaction();
            }
        }

        private static void InitializeEvent<T>(T EventEntryObject, UnitOfWork unitOfWork) where T : BaseObject, IEventEntryObject<WorkCalendarEvent>
        {
            if (EventEntryObject.WorkCalendarEvent == null)
                EventEntryObject.WorkCalendarEvent = new WorkCalendarEvent(unitOfWork);

            EventEntryObject.WorkCalendarEvent.AllDay = false;
            EventEntryObject.WorkCalendarEvent.Location = EventEntryObject.EventLocation;
            EventEntryObject.WorkCalendarEvent.StartOn = (DateTime)EventEntryObject.BookedTime;
            EventEntryObject.WorkCalendarEvent.EndOn = (DateTime)EventEntryObject.BookedTimeEnd;
            EventEntryObject.WorkCalendarEvent.Subject = EventEntryObject.EventSubject;
            EventEntryObject.WorkCalendarEvent.Description = EventEntryObject.EventDescription;
            EventEntryObject.WorkCalendarEvent.SetPropertyValue(EventEntryObject.GetType().Name, EventEntryObject);
            EventEntryObject.WorkCalendarEvent.Status = (int)EventEntryObject.Priority;

            if (EventEntryObject.EventStatus != null)
                EventEntryObject.WorkCalendarEvent.Label = (int)EventEntryObject.EventStatus.UniqueID;

            EventEntryObject.WorkCalendarEvent.ReminderTime = new PostponeTime(EventEntryObject.WorkCalendarEvent.Oid.ToString(), TimeSpan.FromHours(1), string.Concat("Reminder(", EventEntryObject.EventFullDescription, ")"));
            EventEntryObject.WorkCalendarEvent.AlarmTime = EventEntryObject.BookedTime;

            if (EventEntryObject.Resources != null && EventEntryObject.Resources.Count() >= 1)
            {
                if(!EventEntryObject.Session.IsNewObject(EventEntryObject))
                    EventEntryObject.WorkCalendarEvent.Resources.RemoveWhere(g => g.Oid != null);

                foreach (IResource resource in EventEntryObject.Resources)
                {
                    if (resource != null)
                        EventEntryObject.WorkCalendarEvent.Resources.AddUnique(resource as Resource);
                }
            }
        }
    }
}
