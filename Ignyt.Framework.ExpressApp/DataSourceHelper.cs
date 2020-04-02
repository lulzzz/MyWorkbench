using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Ignyt.Framework.ExpressApp
{
    public enum DataLayerType
    {
        SimpleDataLayer = 0, ThreadSafeDataLayer = 1
    }

    public enum ObjectLayerType
    {
        SessionObjectLayer = 0, SimpleObjectLayer = 1
    }

    public class DataSourceHelper : IDisposable
    {
        #region Fields / Properties
        private const int _defaultMaxRowsCount = 2000;
        private string[] _objectLibrary;
        private string _connectionString;
        private string _database;
        private DataLayerType _dataLayerType;
        private ObjectLayerType _objectLayerType;

        private IDbConnection _IDbConnection;
        private IDataStore _dataStore;
        private IDataLayer _dataLayer;
        private UnitOfWork _unitOfWork;
        private IObjectLayer _objectLayer;
        private Session _session;

        public Session Session {
            get {
                if (_session == null)
                {
                    _session = new Session(this._objectLayer);
                    _session.LockingOption = LockingOption.None;
                }
                return _session;
            }
        }

        private Assembly[] Assesmblies {
            get {
                List<Assembly> assesmblies = new List<Assembly>();

                foreach (string str in _objectLibrary)
                {
                    assesmblies.Add(Assembly.Load(str));
                }

                return assesmblies.ToArray();
            }
        }

        public string Database {
            get {
                return this._database;
            }
        }

        public IDataLayer DataLayer {
            get {
                return this._dataLayer;
            }
        }
        #endregion

        public DataSourceHelper(string ConnectionString, string Database, DataLayerType DataLayerType, ObjectLayerType ObjectLayerType, string[] ObjectLibrary)
        {
            this._connectionString = ConnectionString;
            this._database = Database;
            this._dataLayerType = DataLayerType;
            this._objectLibrary = ObjectLibrary;
            this._objectLayerType = ObjectLayerType;

            Initialize();
        }

        public DataSourceHelper(string ConnectionString, string Database, DataLayerType DataLayerType, ObjectLayerType ObjectLayerType)
        {
            this._connectionString = ConnectionString;
            this._database = Database;
            this._dataLayerType = DataLayerType;
            this._objectLayerType = ObjectLayerType;

            Initialize();
        }

        public DataSourceHelper(string ConnectionString, DataLayerType DataLayerType, ObjectLayerType ObjectLayerType)
        {
            this._connectionString = ConnectionString;
            this._dataLayerType = DataLayerType;
            this._objectLayerType = ObjectLayerType;

            Initialize();
        }

        public DataSourceHelper(string ConnectionString, DataLayerType DataLayerType, ObjectLayerType ObjectLayerType, string[] ObjectLibrary)
        {
            this._connectionString = ConnectionString;
            this._dataLayerType = DataLayerType;
            this._objectLibrary = ObjectLibrary;
            this._objectLayerType = ObjectLayerType;

            Initialize();
        }

        #region Properties
        private void InitializeDataLayer()
        {
            XPDictionary dict = new ReflectionDictionary();

            dict.GetDataStoreSchema(this.Assesmblies);

            this._IDbConnection = new SqlConnection(this._connectionString);
            this._dataStore = MSSqlConnectionProvider.CreateProviderFromConnection(this._IDbConnection, AutoCreateOption.DatabaseAndSchema);

            if (this._dataLayerType == DataLayerType.SimpleDataLayer)
                this._dataLayer = new SimpleDataLayer(dict, this._dataStore);
            else
                this._dataLayer = new ThreadSafeDataLayer(dict, this._dataStore);
        }

        private void InitializeObjectLayer()
        {
            if (this._objectLayerType == ObjectLayerType.SessionObjectLayer)
            {
                this._unitOfWork = new UnitOfWork(this._dataLayer);
                this._objectLayer = new SessionObjectLayer(this._unitOfWork, true, null, null, null);
            }
            else
                this._objectLayer = new SimpleObjectLayer(this._dataLayer);
        }

        public ICollection<T> Select<T>(Type Type, string Criteria) where T : BaseObject
        {
            XPClassInfo objectInfo = Session.GetClassInfo(Type);
            return Session.GetObjects(objectInfo, CriteriaOperator.Parse(Criteria), null, _defaultMaxRowsCount, false, true).OfType<T>().ToList();
        }
        #endregion

        private void Initialize()
        {
            // Change database only
            if (this._database != null)
            {
                SqlConnectionStringBuilder connectionStringParser = new SqlConnectionStringBuilder(_connectionString) { InitialCatalog = _database };
                _connectionString = connectionStringParser.ConnectionString;
            }

            this.TryConnect();

            this.InitializeDataLayer();
            this.InitializeObjectLayer();
        }

        private void TryConnect()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this._connectionString))
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this._database + ", Connection exception, " + ex.ToString());
            }
        }

        #region Dispose
        public void Dispose()
        {
            if (_IDbConnection != null)
            {
                _IDbConnection.Close();
                _IDbConnection.Dispose();
                _IDbConnection = null;
            }

            if (_dataStore != null)
            {
                _dataStore = null;
            }

            if (_dataLayer != null)
            {
                _dataLayer.Dispose();
                _dataLayer = null;
            }

            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
                _unitOfWork = null;
            }


            if (_objectLayer != null)
            {
                _objectLayer = null;
            }

            if (_session != null)
            {
                _session.Dispose();
                _session = null;
            }

            GC.Collect();
        }
        #endregion
    }
}