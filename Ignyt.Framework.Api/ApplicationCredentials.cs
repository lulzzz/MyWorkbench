using System;

namespace Ignyt.Framework.Api {
    public sealed class ApplicationCredentials {
        private Application _application { get; set; }

        private static ApplicationCredentials _instance = null;
        private static readonly object _padlock = new object();

        private ApplicationCredentialObject _ApplicationCredentialObject = null;

        public ApplicationCredentialObject ApplicationCredentialObject {
            get {
                return _ApplicationCredentialObject;
            }
        }

        #region Constructor
        private ApplicationCredentials(Application Application) {
            this._application = Application;

            _ApplicationCredentialObject = new DotNetNukeApi(Application).DNNApplicationCredentials(Guid.Parse(Application.GetAttribute<ApplicationID>().ToString()));
        }

        public static ApplicationCredentials Instance(Application Application) {
            if (_instance == null) {
                lock (_padlock) {
                    if (_instance == null) {
                        _instance = new ApplicationCredentials(Application);
                    }
                }
            }
            return _instance;
        }
        #endregion
    }
}
