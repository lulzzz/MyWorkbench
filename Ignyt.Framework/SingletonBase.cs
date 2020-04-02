using System;

namespace Ignyt.Framework {
    public abstract class SingletonBase<T> where T : class {
        private static readonly object padlock = new object();
        private static Lazy<T> sInstance = null;
        public static Application CurrentApplication;
        public static string CurrentDatabase;

        public static T Instance {
            get {
                lock (padlock) {
                    if (sInstance == null)
                        sInstance = new Lazy<T>(() => CreateInstanceOfT());

                    return sInstance.Value;
                }
            }
        }

        public static T InstanceApplication(Application Application) {
            lock (padlock) {
                if (sInstance == null) {
                    CurrentApplication = Application;
                    sInstance = new Lazy<T>(() => CreateInstanceOfT());
                }

                return sInstance.Value;
            }
        }

        public static T InstanceApplication(Application Application,string Database)
        {
            lock (padlock)
            {
                if (sInstance == null)
                {
                    CurrentApplication = Application;
                    CurrentDatabase = Database;
                    sInstance = new Lazy<T>(() => CreateInstanceOfT());
                }

                return sInstance.Value;
            }
        }

        private static T CreateInstanceOfT() {
            return Activator.CreateInstance(typeof(T), true) as T;
        }
    }
}
