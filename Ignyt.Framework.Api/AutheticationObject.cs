using System;

namespace Ignyt.Framework.Api {
    public class AutheticationObject {
        public Guid UserUniqueID { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ApplicationName { get; set; }
        public string Database { get; set; }
        public bool Authenticated { get; set; }
        public bool Trial { get; set; }
        public Nullable<int> DaysRemaining { get; set; }
        public bool CreateUser { get; set; }
        public bool ApplicationDisabled { get; set; }
        public bool UserDisabled { get; set; }
        public Nullable<int> ClientApplicationID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Type { get; set; }
        public string BingMapsApi { get; set; }
        public string GoogleMapsApi { get; set; }
    }
}
