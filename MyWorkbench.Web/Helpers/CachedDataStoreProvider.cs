using System;
using System.Data;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo.DB;

namespace MyWorkbench.Web.Helpers
    
{
    public class CachedDataStoreProvider : IXpoDataStoreProvider
    {
        private static IDisposable[] rootDisposableObjects;
        private static DataCacheRoot root;
        private static IXpoDataStoreProvider xpoDataStoreProvider;
        public static void ResetDataCacheRoot()
        {
            root = null;
            if (rootDisposableObjects != null)
            {
                foreach (IDisposable disposableObject in rootDisposableObjects)
                {
                    disposableObject.Dispose();
                }
                rootDisposableObjects = null;
            }
        }
        public string ConnectionString {
            get {
                return xpoDataStoreProvider.ConnectionString;
            }
        }

        public CachedDataStoreProvider(string connectionString, IDbConnection connection, bool enablePoolingInConnectionString)
        {
            if (xpoDataStoreProvider == null)
            {
                xpoDataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, enablePoolingInConnectionString);
            }
        }
        public IDataStore CreateWorkingStore(out IDisposable[] disposableObjects)
        {
            if (root == null)
            {
                IDataStore baseDataStore = xpoDataStoreProvider.CreateWorkingStore(out rootDisposableObjects);
                root = new DataCacheRoot(baseDataStore);
            }
            disposableObjects = new IDisposable[0];
            return new DataCacheNode(root);
        }

        public IDataStore CreateUpdatingStore(bool allowUpdateSchema, out IDisposable[] disposableObjects)
        {
            return xpoDataStoreProvider.CreateUpdatingStore(allowUpdateSchema, out disposableObjects);
        }

        public IDataStore CreateSchemaCheckingStore(out IDisposable[] disposableObjects)
        {
            return xpoDataStoreProvider.CreateSchemaCheckingStore(out disposableObjects);
        }
    }
}