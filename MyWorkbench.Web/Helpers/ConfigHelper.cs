using System;
using System.Configuration;

namespace MyWorkbench.Web.Helpers {
    public static class ConfigHelper {
        public static string GoogleMapsApi { get; set; }
        public static string BingMapsApi { get; set; }

        public static string ConnectionString {
            get {
                return ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            }
        }
    }
}