using DevExpress.ExpressApp;
using DevExpress.ExpressApp.MiddleTier;
using Exceptionless;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.Collections.Generic;

namespace MyWorkbench.Module.Web.Helpers {
    public static class ToastMessageHelper {
        public static Employee GetEmployee(XafApplication Application)
        {
            Employee employee = null;

            if (SecuritySystem.CurrentUserId.ToString() != string.Empty)
                employee = Application.CreateObjectSpace().GetObjectByKey<Employee>((Guid)SecuritySystem.CurrentUserId);

            return employee;
        }

        public static void ShowSuccessMessage(XafApplication Application, string Message, InformationPosition Position, bool Permanent) {
            Employee employee = GetEmployee(Application);

            MessageOptions options = new MessageOptions {
                Duration = Permanent ? 500000000 : 5000,
                Message = string.Format(Message),
                Type = InformationType.Success
            };

            options.Web.Position = Position;
            Application.ShowViewStrategy.ShowMessage(options);

            if (employee != null)
                ExceptionlessClient.Default.CreateLog(ExceptionSources.MyWorkbenchCloud.ToString(), Message, LogLevel.Warning.ToString()).SetUserDescription(employee.Email, employee.FullName);
            else
                ExceptionlessClient.Default.CreateLog(ExceptionSources.MyWorkbenchCloud.ToString(), Message, LogLevel.Warning.ToString());
        }

        public static void ShowWarningMessage(XafApplication Application, string Message, InformationPosition Position) {
            Employee employee = GetEmployee(Application);

            MessageOptions options = new MessageOptions {
                Duration = 5000,
                Message = string.Format(Message),
                Type = InformationType.Warning
            };

            options.Web.Position = Position;
            Application.ShowViewStrategy.ShowMessage(options);

            if (employee != null)
                ExceptionlessClient.Default.CreateLog(ExceptionSources.MyWorkbenchCloud.ToString(), Message, LogLevel.Warning.ToString()).SetUserDescription(employee.Email, employee.FullName);
            else
                ExceptionlessClient.Default.CreateLog(ExceptionSources.MyWorkbenchCloud.ToString(), Message, LogLevel.Warning.ToString());
        }

        public static void ShowSuccessMessage(XafApplication Application, string Message, InformationPosition Position) {
            Employee employee = GetEmployee(Application);

            MessageOptions options = new MessageOptions {
                Duration = 5000,
                Message = string.Format(Message),
                Type = InformationType.Success
            };

            options.Web.Position = Position;
            Application.ShowViewStrategy.ShowMessage(options);

            if (employee != null)
                ExceptionlessClient.Default.CreateLog(ExceptionSources.MyWorkbenchCloud.ToString(), Message, LogLevel.Info.ToString()).SetUserDescription(employee.Email, employee.FullName);
            else
                ExceptionlessClient.Default.CreateLog(ExceptionSources.MyWorkbenchCloud.ToString(), Message, LogLevel.Info.ToString());
        }

        public static void ShowErrorMessage(XafApplication Application, Exception Exception, InformationPosition Position) {
            Employee employee = GetEmployee(Application);

            MessageOptions options = new MessageOptions {
                Duration = 5000,
                Message = string.Format(Exception == null ? "Unknown Error" : Exception.ToString()),
                Type = InformationType.Error
            };

            options.Web.Position = Position;
            Application.ShowViewStrategy.ShowMessage(options);

            if (employee != null)
                ExceptionlessClient.Default.CreateLog("My Workbench", Exception.ToString(), LogLevel.Error.ToString()).MarkAsCritical().SetUserDescription(employee.Email, employee.FullName);
            else
                ExceptionlessClient.Default.CreateLog("My Workbench", Exception.ToString(), LogLevel.Error.ToString()).MarkAsCritical();
        }

        public static void ShowErrorMessages(XafApplication Application, List<Exception> Exceptions, InformationPosition Position) {
            string exceptions = string.Empty;
            Employee employee = GetEmployee(Application);

            foreach (Exception exception in Exceptions) {
                if (exceptions == string.Empty)
                    exceptions = string.Format(exception == null ? "Unknown Error" : exception.ToString());
                else
                    exceptions = ", " + string.Format(exception == null ? "Unknown Error" : exception.ToString());
            }

            MessageOptions options = new MessageOptions {
                Duration = 5000,
                Message = string.Format(new Exception(exceptions).ToString()),
                Type = InformationType.Error
            };

            options.Web.Position = Position;
            Application.ShowViewStrategy.ShowMessage(options);

            if (employee != null)
                ExceptionlessClient.Default.CreateLog(ExceptionSources.MyWorkbenchCloud.ToString(), exceptions, LogLevel.Error.ToString()).MarkAsCritical().SetUserDescription(employee.Email, employee.FullName);
            else
                ExceptionlessClient.Default.CreateLog(ExceptionSources.MyWorkbenchCloud.ToString(), exceptions, LogLevel.Error.ToString()).MarkAsCritical();
        }
    }
}
