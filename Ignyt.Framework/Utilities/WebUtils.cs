using System;
using System.Web;

namespace Ignyt.Framework.Utilities
{
    public class WebUtils
    {
        public static void SetUserLocale(string CurrencySymbol, System.Globalization.DateTimeFormatInfo DateTimeFormatInfo)
        {
            HttpRequest Request = HttpContext.Current.Request;

            if (Request.UserLanguages == null)
                return;

            string Lang = Request.UserLanguages[0];

            if (Lang != null)
            {
                if (Lang.Length < 3)
                    Lang = String.Format("{0}-{1}", Lang, Lang.ToUpper());

                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Lang);

                if (CurrencySymbol != null && CurrencySymbol != "")
                    System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = CurrencySymbol;

                if (DateTimeFormatInfo != null)
                    System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat = DateTimeFormatInfo;
            }
        }

        public static System.Globalization.CultureInfo SetUserLocaleNew(string CurrencySymbol, System.Globalization.DateTimeFormatInfo DateTimeFormatInfo)
        {
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-US");

            if (HttpContext.Current.Request.UserLanguages != null)
            {
                string Lang = HttpContext.Current.Request.UserLanguages[0];

                if (Lang != null)
                {
                    if (Lang.Length < 3)
                        Lang = String.Format("{0}-{1}", Lang, Lang.ToUpper());

                    cultureInfo = new System.Globalization.CultureInfo(Lang);

                    if (CurrencySymbol != null && CurrencySymbol != "")
                        cultureInfo.NumberFormat.CurrencySymbol = CurrencySymbol;

                    if (DateTimeFormatInfo != null)
                        cultureInfo.DateTimeFormat = DateTimeFormatInfo;
                }
            }

            return cultureInfo;
        }

        public static System.Globalization.CultureInfo SetUserLocaleNew(string CultureName)
        {
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(CultureName);
            return cultureInfo;
        }
    }
}
