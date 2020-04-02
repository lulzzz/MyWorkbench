using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using System.Linq.Expressions;
using System.Collections.Specialized;

namespace Ignyt.Framework.ExpressApp {
    /// <summary>
    /// Class for XPO implementation
    /// </summary>
    public class XpoHelper {
        #region Privates
        private Session _Session;

        private Session _SessionPerThread;

        private string _UniqueKey;

        private string[] _UniqueKeys;

        private const int DefaultMaxRowsCount = 2000;
        #endregion 

        #region Properties
        /// <summary>
        /// XPO session.
        /// </summary>
        public Session Session {
            get { return getSession(); }
        }

        /// <summary>
        /// XPO session.
        /// </summary>
        public Session NewSession {
            get { return getNewSession(); }
        }
        #endregion

        #region Constructor
        public XpoHelper() { }

        public XpoHelper(Session Session) {
            this._Session = Session;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Return all objects of certain type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ICollection<T> GetObjects<T>() {
            return GetObjects(typeof(T), null).OfType<T>().ToList();
        }

        /// <summary>
        /// Return all objects of certain type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ICollection<T> GetObjects<T>(Session session) {
            return GetObjects(typeof(T), null).OfType<T>().ToList();
        }

        /// <summary>
        /// Return collection of certain type.
        /// </summary>
        /// <param name="criteria"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ICollection<T> GetObjects<T>(string criteria) {
            return GetObjects(typeof(T), CriteriaOperator.Parse(criteria)).OfType<T>().ToList();
        }

        /// <summary>
        /// Return collection of certain type.
        /// </summary>
        /// <param name="criteria"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ICollection<T> GetObjects<T>(CriteriaOperator criteria) {
            return GetObjects(typeof(T), criteria).OfType<T>().ToList();
        }

        /// <summary>
        /// Return collection of certain type.
        /// </summary>
        /// <param name="session"> </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="criteria">
        /// The criteria.
        /// </param>
        /// <returns>
        /// Collection of certain type.
        /// </returns>
        public ICollection GetObjects(Session session, Type type, CriteriaOperator criteria) {
            XPClassInfo objectInfo = getSession().GetClassInfo(type);
            return session.GetObjects(objectInfo, criteria, null, DefaultMaxRowsCount, false, true);
        }

        /// <summary>
        /// Return collection of certain type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="criteria">
        /// The criteria.
        /// </param>
        /// <returns>
        /// Collection of certain type.
        /// </returns>
        public ICollection GetObjects(Type type, CriteriaOperator criteria) {
            return GetObjects(getSession(), type, criteria);
        }

        /// <summary>
        /// Find object of certain type by criteria.
        /// </summary>
        /// <typeparam name="T">Required type</typeparam>
        /// <param name="criteria">Criteria for researching</param>
        /// <returns>Return an object of certain type by criteria</returns>
        public T FindObjectById<T>(CriteriaOperator criteria) {
            return getSession().FindObject<T>(criteria);
        }

        /// <summary>
        /// Find object of certain type by criteria.
        /// </summary>
        /// <param name="type">Required type</param>
        /// <param name="criteria">Criteria for researching</param>
        /// <returns>Return an object of certain type by criteria</returns>
        public object FindObjectById(Type type, CriteriaOperator criteria) {
            return getSession().FindObject(type, criteria);
        }

        /// <summary>
        /// Save XPO object.
        /// </summary>
        /// <param name="xpoObject">XPO object.</param>

        //public void Save(object xpoObject) {
        //    getSession().Save(xpoObject);
        //}

        public void Save(object xpoObject) {
            var session = xpoObject is IXPObject ? (xpoObject as IXPObject).Session : getSession();
            session.Save(xpoObject);
            //if (session.InTransaction)
            //{
            //    session.CommitTransaction();
            //}
        }

        /// <summary>
        /// Save XPO object and if open transaction close it.
        /// </summary>
        /// <param name="xpoObject">XPO object.</param>
        public void SaveAndCommit(object xpoObject) {
            var session = xpoObject is IXPObject ? (xpoObject as IXPObject).Session : getSession();
            session.Save(xpoObject);
            if (session.InTransaction) {
                session.CommitTransaction();
            }
        }

        /// <summary>
        /// Save and Merge XPO object and if open transaction close it.
        /// </summary>
        /// <param name="xpoObject">XPO object.</param>
        public void MergeAndSaveAndCommit<T, U>(ICollection<T> xpoNewObject, string UniqueKey)
            where T : OrderedDictionary
            where U : IXPObject {
            this.MergeAndSave<T, U>(xpoNewObject, UniqueKey);
            if (getSession().InTransaction) {
                getSession().CommitTransaction();
            }
        }

        /// <summary>
        /// Save and Merge XPO object and if open transaction close it.
        /// </summary>
        /// <param name="xpoObject">XPO object.</param>
        public void MergeAndSaveAndCommit<T, U>(ICollection<T> xpoNewObject, params string[] UniqueKeys)
            where T : OrderedDictionary
            where U : IXPObject {
            this.MergeAndSave<T, U>(xpoNewObject, UniqueKeys);
            if (getSession().InTransaction) {
                getSession().CommitTransaction();
            }
        }

        /// <summary>
        /// Save and Merge XPO object.
        /// </summary>
        /// <param name="xpoObject">XPO object.</param>
        public void MergeAndSave<T, U>(ICollection<T> xpoNewObject, string UniqueKey)
            where T : OrderedDictionary
            where U : IXPObject {

            this._UniqueKey = UniqueKey;

            xpoNewObject.ForEach<OrderedDictionary>(ProcessDictionaryEntry<U>);
        }

        /// <summary>
        /// Save and Merge XPO object.
        /// </summary>
        /// <param name="xpoObject">XPO object.</param>
        public void MergeAndSave<T, U>(ICollection<T> xpoNewObject, params string[] UniqueKeys)
            where T : OrderedDictionary
            where U : IXPObject {

            this._UniqueKeys = UniqueKeys;

            xpoNewObject.ForEach<OrderedDictionary>(ProcessDictionaryEntry<U>);
        }

        private void ProcessDictionaryEntry<T>(OrderedDictionary dictionary) {
            var session = getSession();
            var criteriaOperator = string.Empty;

            if (this._UniqueKeys != null) {
                foreach (string str in this._UniqueKeys) {
                    if (criteriaOperator != string.Empty)
                        criteriaOperator = criteriaOperator + " and ";

                    criteriaOperator = criteriaOperator + string.Format("{0} = '{1}'", str, dictionary[str]);
                }
            } else
                criteriaOperator = string.Format("{0} = '{1}'", this._UniqueKey, dictionary[this._UniqueKey]);

            T obj = this.FindObjectById<T>(CriteriaOperator.Parse(criteriaOperator));

            if (obj.IsNotNull()) {
                obj.CopyPropertiesFromDictionary(dictionary);
            } else {
                obj = (T)this.Create(typeof(T));
                obj.CopyPropertiesFromDictionary(dictionary);
            }

            session.Save(obj);
        }

        /// <summary>
        /// Save XPO object and if open transaction close it.
        /// </summary>
        /// <param name="xpoObject">XPO object.</param>
        public void UpdateAndCommit<T>(Expression<Func<T>> evaluator, CriteriaOperator criteria) where T : IXPObject {
            var session = getSession();
            session.Update<T>(evaluator, criteria);
            if (session.InTransaction) {
                session.CommitTransaction();
            }
        }

        /// <summary>
        /// Save XPO object
        /// </summary>
        /// <param name="xpoObject">XPO object.</param>
        public void Update<T>(Expression<Func<T>> evaluator, CriteriaOperator criteria) where T : IXPObject {
            getSession().Update<T>(evaluator, criteria);
        }

        public void DeleteAndCommit(object xpoObject) {
            if (xpoObject == null) return;

            var session = getSession();
            session.Delete(xpoObject);
            if (session.InTransaction) {
                session.CommitTransaction();
            }
        }

        public void DeleteAndCommit(IEnumerable xpoObjects) {
            var session = getSession();

            foreach (var obj in xpoObjects) {
                session.Delete(obj);
            }

            if (session.InTransaction) {
                session.CommitTransaction();
            }
        }

        public void CommitTransaction() {
            if (Session.InTransaction) {
                Session.CommitTransaction();
            }
        }


        public void RefreshSession() {
            _Session = null;
        }

        public void RefreshSession(Session Session) {
            _Session = Session;
        }

        /// <summary>
        /// Return new XPO session.
        /// </summary>
        /// <returns></returns>
        private Session getNewSession() {
            return new Session(XpoDefault.DataLayer);
        }

        /// <summary>
        /// Return current XPO session.
        /// </summary>
        /// <returns>XPO session.</returns>
        private Session getSession() {
            return this._Session;
        }

        /// <summary>
        /// Deletes XPO Object.
        /// </summary>
        public void Delete(object xpoObject) {
            getSession().Delete(xpoObject);
        }

        /// <summary>
        /// Returns XPO object of given type and id.
        /// </summary>
        /// <param name="id">
        /// The id of object to return.
        /// </param>
        /// <returns>
        /// Object of given type and with provided id.
        /// </returns>
        public T GetById<T>(object id) {
            return getSession().GetObjectByKey<T>(id);
        }

        /// <summary>
        /// Creates object of given type.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <returns>Instance of given type.</returns>
        public object Create<T>() {
            return Create(typeof(T));
        }

        /// <summary>
        /// Creates object of given type.
        /// </summary>
        /// <param name="type">Type of object to create.</param>
        /// <returns>Instance of given type.</returns>
        public object Create(Type type) {
            return Activator.CreateInstance(type, getSession());
        }

        /// <summary>
        /// Queries objects of given type and returns them as array.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <typeparam name="T">
        /// Type of objects.
        /// </typeparam>
        /// <returns>
        /// Array of objects.
        /// </returns>
        public IEnumerable<T> Query<T>(Func<XPQuery<T>, IEnumerable<T>> filter = null) {
            var xpQuery = new XPQuery<T>(Session);
            return (filter == null ? xpQuery : filter(xpQuery)).ToArray();
        }

        /// <summary>
        /// Queries objects of given type and returns them as array.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <typeparam name="T">
        /// Type of objects.
        /// </typeparam>
        /// <returns>
        /// Array of objects.
        /// </returns>
        public T QueryFirstOrDefault<T>(Func<XPQuery<T>, IEnumerable<T>> filter = null) where T : class {
            var xpQuery = new XPQuery<T>(Session);
            return (filter == null ? xpQuery : filter(xpQuery)).FirstOrDefault();
        }

        /// <summary>
        /// Queries objects of given type and returns them as array.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <typeparam name="T">
        /// Type of objects.
        /// </typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <returns>
        /// Array of objects.
        /// </returns>
        public IEnumerable<TOut> Query<T, TOut>(Func<XPQuery<T>, IEnumerable<TOut>> filter) {
            var xpQuery = new XPQuery<T>(Session);
            return filter(xpQuery).ToArray();
        }

        public void SetSession(Session session) {
            _Session = session;
        }

        /// <summary>
        /// Sets session for current thread and returns previous one if it was not disposed or null otherwise.
        /// </summary>
        /// <param name="session">
        /// Session to set.
        /// </param>
        /// <param name="disposePrevious">
        /// Should previous session (for current thread) be disposed?
        /// </param>
        /// <returns>
        /// Previous session if it was not disposed or null otherwise.
        /// </returns>
        public Session SetSessionForThread(Session session, bool disposePrevious = true) {
            if (disposePrevious && _SessionPerThread != null) {
                _SessionPerThread.Dispose();
            }

            var previous = _SessionPerThread;
            _SessionPerThread = session;

            return disposePrevious ? null : previous;
        }
        #endregion
    }
}
