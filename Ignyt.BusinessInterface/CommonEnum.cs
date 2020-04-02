using DevExpress.Persistent.Base;
using Ignyt.BusinessInterface.Attributes;

namespace Ignyt.BusinessInterface {
    public enum ExceptionSources
    {
        MyWorkbenchCloud = 0, MyWorkbenchHangfire = 1
    }

    public enum AccountingPartner
    {
        XeroAccounting = 0, SageOneAccounting = 1
    }

    public enum Interval
    {
        Weekly = 0,
        Fortnightly = 1,
        Monthly = 2,
        Annually = 3,
        BiAnnually = 4,
        Daily = 5,
        Quarterly = 6
    }

    public enum DataType {
        Numeric = 0,
        Text = 1,
        DateTime = 2
    }

    public enum TransactionType {
        Invoice = 0,
        CreditNote = 1,
        Payment = 2,
        Purchase = 3,
        PurchasePayment = 4,
        Sale = 5,
        SupplierInvoice = 6,
        SupplierPayment = 7
    }

    public enum CommonEnum {
        Mr = 0,
        Mrs = 1,
        Miss = 2,
        Dr = 3,
        Prof = 4,
        Rev = 5,
        Ms = 6
    }

    public enum ItemType {
        Material = 0,
        Labour = 1,
        CallOut = 2
    }

    public enum EmployeeType {
        Employee = 0,
        Driver = 1,
        SystemUser = 2,
        TeamEmployee = 3
     }

    public enum Priority {
        [ImageName("State_Priority_Low")]
        [Color("#80CC99")]
        Low = 0,
        [ImageName("State_Priority_Normal")]
        [Color("#FFCC80")]
        Normal = 1,
        [ImageName("State_Priority_High")]
        [Color("#FF9999")]
        High = 2
    }

    public enum PaymentType {
        Cash = 0,
        Cheque = 1,
        Credit = 2,
        CreditCard = 3,
        DebitCard = 4,
        EFT = 5,
        Account = 6,
        WrittenOff = 7,
        Purchase = 8
    }

    public enum VendorType {
        Client = 0,
        Supplier = 1
    }

    public enum CommunicationType {
        Message = 0,
        Email = 1
    }

    public enum SettingMapProvider {
        Bing = 0, Google = 1
    }

    public enum WorkFlowType {
        [VendorAccountType(VendorType.Client)]
        [InventoryIn(false)]
        Project = 0,
        [VendorAccountType(VendorType.Client)]
        [InventoryIn(false)]
        JobCard = 1,
        [VendorAccountType(VendorType.Client)]
        Quote = 2,
        [VendorAccountType(VendorType.Client)]
        [InventoryIn(false)]
        Invoice = 3,
        [VendorAccountType(VendorType.Client)]
        [InventoryIn(true)]
        CreditNote = 4,
        [VendorAccountType(VendorType.Client)]
        Ticket = 6,
        [VendorAccountType(VendorType.Supplier)]
        RequestForQuote = 7,
        [VendorAccountType(VendorType.Supplier)]
        [InventoryIn(true)]
        Purchase = 8,
        [VendorAccountType(VendorType.Client)]
        [InventoryIn(true)]
        MassInventoryMovement = 9,
        [VendorAccountType(VendorType.Client)]
        All = 10,
        [VendorAccountType(VendorType.Client)]
        [InventoryIn(false)]
        Sale = 11,
        [VendorAccountType(VendorType.Client)]
        [InventoryIn(false)]
        RecurringJobCard = 12,
        [VendorAccountType(VendorType.Client)]
        [InventoryIn(false)]
        RecurringInvoice = 13,
        [VendorAccountType(VendorType.Supplier)]
        [InventoryIn(true)]
        SupplierInvoice = 14,
        [VendorAccountType(VendorType.Supplier)]
        [InventoryIn(false)]
        SupplierCreditNote = 15,
        [VendorAccountType(VendorType.Client)]
        [InventoryIn(false)]
        Booking = 16,
        [VendorAccountType(VendorType.Client)]
        [InventoryIn(false)]
        Task = 17,
        [VendorAccountType(VendorType.Client)]
        [InventoryIn(false)]
        RecurringTask = 18,
        [VendorAccountType(VendorType.Client)]
        [InventoryIn(false)]
        RecurringBooking = 19
    }
}
