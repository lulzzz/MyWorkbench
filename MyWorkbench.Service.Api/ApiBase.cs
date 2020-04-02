using Ignyt.Framework.Api;
using Ignyt.Framework.ExpressApp;
using MyWorkbench.BaseObjects.Constants;
using System;
using System.Collections.Generic;

namespace MyWorkBench.Service.Api
{
    public abstract class ApiBase : IDisposable
    {
        private static string _connectionString;
        private List<ClientApplicationsObject> _clients;
        private const string _objectLibrary = "MyWorkbench.BusinessObjects";

        private TaskExceptionList _taskExceptionList;
        public TaskExceptionList TaskExceptionList {
            get {
                if (_taskExceptionList == null)
                    _taskExceptionList = new TaskExceptionList();
                return _taskExceptionList;
            }
        }

        protected ClientApplicationsObject ClientApplicationsObject { get; private set; }

        protected DataSourceHelper DataSourceHelper { get; private set; }

        protected ApiBase(string ConnectionString)
        {
            _connectionString = ConnectionString;

            this.DataSourceHelper = new DataSourceHelper(_connectionString, DataLayerType.SimpleDataLayer, ObjectLayerType.SimpleObjectLayer, new string[1] { _objectLibrary });
        }

        private List<ClientApplicationsObject> Clients {
            get {
                if (_clients == null)
                    _clients = new DotNetNukeApi(Ignyt.Framework.Application.MyWorkbench).ClientApplications(Constants.ApplicationID(this.DataSourceHelper.Session));
                return _clients;
            }
        }

        public TaskExceptionList Process()
        {
            try
            {
                if (this.Clients.Count >= 1)
                {
                    foreach (ClientApplicationsObject clientApplicationsObject in this.Clients)
                    {
                        this.ClientApplicationsObject = clientApplicationsObject;

                        try
                        {
                            this.ProcessClient();
                        }
                        catch (Exception ex)
                        {
                            TaskExceptionList.TaskExceptions.Add(ex);
                        }
                    }
                }

                return TaskExceptionList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TaskExceptionList Process(string[] Args)
        {
            try
            {
                if (this.Clients.Count >= 1)
                    foreach (ClientApplicationsObject clientApplicationsObject in this.Clients)
                    {
                        this.ClientApplicationsObject = clientApplicationsObject;

                        try
                        {
                            this.ProcessClient(Args);
                        }
                        catch (Exception ex)
                        {
                            TaskExceptionList.TaskExceptions.Add(ex);
                        }
                    }

                return TaskExceptionList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual void ProcessClient()
        {
            try
            {
                if (this.ClientApplicationsObject.Trial & this.ClientApplicationsObject.DaysRemaining == 0)
                    throw new Exception(this.ClientApplicationsObject.Database + " trial version has expired");

                try
                {
                    this.InitializeDataSourceHelper(this.ClientApplicationsObject.Database);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual void ProcessClient(string[] Args)
        {
            this.ProcessClient();
        }

        private void InitializeDataSourceHelper(string Database)
        {
            this.DataSourceHelper = new DataSourceHelper(_connectionString, Database, DataLayerType.SimpleDataLayer, ObjectLayerType.SimpleObjectLayer, new string[1] { _objectLibrary });
        }

        public void Dispose()
        {
            if (this.DataSourceHelper != null)
                this.DataSourceHelper = null;

            GC.Collect();
        }
    }
}
