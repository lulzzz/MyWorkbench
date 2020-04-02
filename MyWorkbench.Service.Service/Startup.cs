using System;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using Hangfire;
using MyWorkBench.Service.Api;

[assembly: OwinStartup(typeof(MyWorkbench.Service.Startup))]
namespace MyWorkbench.Service {
    public class Startup {
        #region Jobs
        [AutomaticRetry(Attempts = 1,LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void ProcessEmails() {
            TaskExceptionList taskExceptionList = new TaskExceptionList();

            Console.WriteLine("{0} MyWorkbench Emails - Emails started Successfuly!", DateTime.Now.ToString());

            using (Emails emails = new Emails(Config.ConnectionString)) {
                taskExceptionList = emails.Process();
            }

            if(taskExceptionList.TaskExceptions.Count >= 1)
                throw new Exception(taskExceptionList.ToString());
        }

        [AutomaticRetry(Attempts = 1, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void ProcessMessages()
        {
            TaskExceptionList taskExceptionList = new TaskExceptionList();

            Console.WriteLine("{0} MyWorkbench Messages - Messages started Successfuly!", DateTime.Now.ToString());

            using (Messages messages = new Messages(Config.ConnectionString))
            {
                taskExceptionList = messages.Process();
            }

            if (taskExceptionList.TaskExceptions.Count >= 1)
                throw new Exception(taskExceptionList.ToString());
        }

        [AutomaticRetry(Attempts = 1, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void ProcessEmailToTicket()
        {
            TaskExceptionList taskExceptionList = new TaskExceptionList();

            Console.WriteLine("{0} MyWorkbench EmailToTicket - EmailToTicket started Successfuly!", DateTime.Now.ToString());

            using (EmailToTicket emailToTicket = new EmailToTicket(Config.ConnectionString, Config.MailServer, Config.MailPort, Config.MailSSL, Config.MailUsername, Config.MailPassword))
            {
                taskExceptionList = emailToTicket.Process();
            }

            if (taskExceptionList.TaskExceptions.Count >= 1)
                throw new Exception(taskExceptionList.ToString());
        }

        [AutomaticRetry(Attempts = 1, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void ProcessRecurring()
        {
            TaskExceptionList taskExceptionList = new TaskExceptionList();

            Console.WriteLine("{0} MyWorkbench Recurring - Recurring started Successfuly!", DateTime.Now.ToString());

            using (Recurring recurring = new Recurring(Config.ConnectionString))
            {
                taskExceptionList = recurring.Process();
            }

            if (taskExceptionList.TaskExceptions.Count >= 1)
                throw new Exception(taskExceptionList.ToString());
        }

        [AutomaticRetry(Attempts = 1, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public void ProcessGeoCode()
        {
            TaskExceptionList taskExceptionList = new TaskExceptionList();

            Console.WriteLine("{0} MyWorkbench Geocode - Geocode started Successfuly!", DateTime.Now.ToString());

            using (GeoCode geocode = new GeoCode(Config.ConnectionString, Config.GoogleApiKey))
            {
                taskExceptionList = geocode.Process();
            }

            if (taskExceptionList.TaskExceptions.Count >= 1)
                throw new Exception(taskExceptionList.ToString());
        }
        #endregion

        public void Configuration(IAppBuilder app) {
            GlobalConfiguration.Configuration.UseSqlServerStorage(Config.DefaultConnection, new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) });

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate(() => ProcessEmails(), Cron.Minutely);
            RecurringJob.AddOrUpdate(() => ProcessMessages(), Cron.Minutely);
            RecurringJob.AddOrUpdate(() => ProcessEmailToTicket(), Cron.MinuteInterval(5));
            RecurringJob.AddOrUpdate(() => ProcessRecurring(), Cron.MinuteInterval(5));
            RecurringJob.AddOrUpdate(() => ProcessGeoCode(), Cron.MinuteInterval(5));
        }
    }
}
