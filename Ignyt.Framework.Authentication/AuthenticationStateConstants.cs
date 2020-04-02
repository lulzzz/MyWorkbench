using System;

namespace Ignyt.Framework.Authentication {
    internal static class AuthenticationStateConstants {
        public static string UserUpdated = "User Successfuly updated";
        public static string UserUpdateFailed = "401 Unauthorized - User update failed";
        public static string UserPasswordChanged = "Change password successful";
        public static string UserPasswordChangedFailed = "401 Unauthorized - Change password failed";
        public static string UserNameEmpty = "401 Unauthorized - UserName is empty";
        public static string PasswordEmpty = "401 Unauthorized - Password is empty";
        public static string UserAuthenticatedFromCache = "Ignyt Authentication from cache";
        public static string UserAuthenticationFailed = "401 Unauthorized - Ignyt Authentication failed";
        public static string ConnectionStringParserFailed = "401 Unauthorized - Connection string parser failed";
        public static string UserAuthenticationSuccessful = "Authentication Successful";
        public static string UserDatabaseSuccessful = "Successfuly set user database";
        public static string TrialExpired = "Your trial license has expired";
    }
}
