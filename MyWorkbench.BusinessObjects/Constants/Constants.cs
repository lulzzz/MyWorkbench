using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects;
using MyWorkbench.BusinessObjects.Common;
using MyWorkbench.BusinessObjects.Lookups;
using System;

namespace MyWorkbench.BaseObjects.Constants {
    public static class Constants {
        public static Guid ApplicationID(Session Session) {
            if (Session.FindObject<Settings>(null) == null)
                return Guid.Parse("4d71a4e1-3675-46d2-856a-74d006a6b093");

            return Session.FindObject<Settings>(null).ApplicationID == null ? Guid.Parse("4d71a4e1-3675-46d2-856a-74d006a6b093") : Guid.Parse(Session.FindObject<Settings>(null).ApplicationID);
        }

        public static double VatPercentage(Session Session) {
            return (double)Session.FindObject<Settings>(null).VatPercentage / 100;
        }

        public static double DefaultDepositPercentage(Session Session) {
            return Session.FindObject<Settings>(null).DefaultDepositPercentage;
        }

        public static Vendor DefaultSaleVendor(Session Session)
        {
            return Session.FindObject<Settings>(null).DefaultSaleVendor;
        }

        public static double AppointmentLength(Session Session) {
            return Session.FindObject<Settings>(null).AppointmentLength;
        }

        public static string CurrentTimeZone(Session Session) {
            return Session.FindObject<Settings>(null).CurrentTimeZone.Value;
        }

        public static string CurrentTimeZone(UnitOfWork UnitOfWork)
        {
            return UnitOfWork.FindObject<Settings>(null).CurrentTimeZone.Value;
        }

        public static DateTime DateTimeTimeZone(Session Session) {
            if (Session.FindObject<Settings>(null) != null)
                return DateTime.UtcNow.ConvertToCurrentTimeZone(CurrentTimeZone(Session));
            else
                return DateTime.Now;
        }

        public static DateTime DateTimeTimeZone(UnitOfWork UnitOfWork)
        {
            if (UnitOfWork.FindObject<Settings>(null) != null)
                return DateTime.UtcNow.ConvertToCurrentTimeZone(CurrentTimeZone(UnitOfWork));
            else
                return DateTime.Now;
        }

        public static Currency Currency(Session Session)
        {
            return Session.FindObject<Settings>(null).Currency;
        }

        public static Culture CultureInfo(Session Session)
        {
            return Session.FindObject<Settings>(null).Culture;
        }

        public static bool HasAccessToSettings(Session Session)
        {
            Settings settings = Session.FindObject<Settings>(null);

            if (settings != null)
                return true;
            else
                return false;
        }

        public static double DefaultMarkupPercentage(Session Session) {
            if (Session.FindObject<Settings>(null) == null)
                return 0;
            else
                return
                    Session.FindObject<Settings>(null).DefaultMarkupPercentage;
        }

        public static bool VatRegistered(Session Session) {
            if (Session.FindObject<Settings>(null) != null)
                return Session.FindObject<Settings>(null).VatRegistered;
            else {
                return false;
            }
        }

        public static bool AllowEndlessPaging(Session Session) {
            return Session.FindObject<Settings>(null).AllowEndlessPaging;
        }

        public static SettingMapProvider SettingMapProvider(Session Session) {
            return Session.FindObject<Settings>(null).MapProvider;
        }

        public static AccountingPartner AccountingPartner(Session Session)
        {
            return Session.FindObject<Settings>(null).AccountingPartner;
        }

        public static DayOfWeek PaymentDayOfWeek(Session Session)
        {
            return Session.FindObject<Settings>(null).PaymentDayOfWeek;
        }
    }
}
