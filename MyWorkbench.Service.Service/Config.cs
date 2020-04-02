using System;
using System.Configuration;

namespace MyWorkbench.Service {
    public static class Config {
        public static string ConnectionString {
            get {
                return ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            }
        }

        public static string DefaultConnection {
            get {
                return ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            }
        }

        public static string HangFireUri {
            get {
                return ConfigurationManager.AppSettings["HangFireUri"].ToString();
            }
        }

        public static string MailServer {
            get {
                return ConfigurationManager.AppSettings["MailServer"].ToString();
            }
        }

        public static int MailPort {
            get {
                return int.Parse(ConfigurationManager.AppSettings["MailPort"].ToString());
            }
        }

        public static bool MailSSL {
            get {
                return bool.Parse(ConfigurationManager.AppSettings["MailSSL"].ToString());
            }
        }

        public static string MailUsername {
            get {
                return ConfigurationManager.AppSettings["MailUsername"].ToString();
            }
        }

        public static string MailPassword {
            get {
                return ConfigurationManager.AppSettings["MailPassword"].ToString();
            }
        }

        public static string GoogleApiKey {
            get {
                return ConfigurationManager.AppSettings["GoogleApiKey"].ToString();
            }
        }
    }
}
