using DevExpress.Persistent.BaseImpl;
using Ignyt.Framework.Interfaces;
using System;
using System.Collections.Generic;

namespace Ignyt.BusinessInterface {
    public enum RecipientRoles
    {
        Vendor = 0, Employee = 1, Team = 2, VendorContact = 3, SystemUser = 4, Driver = 5
    }

    public interface ICustomizable {}

    public interface IStatusPriority<T,U> {
        T Status { get; set; }
        U Priority { get; set; }
    }

    public interface IWorkFlowResource {
        string Description { get; }
        System.Drawing.Color Color { get; set; }
        System.Drawing.Image Image { get; set; }

    }

    public interface IInlineEdit { }

    public interface IAccountingPartner {
        Guid Oid { get; }
        string AccountingPartnerIdentifier { get; set; }
    }

    public interface IEndlessPaging { }

    public interface IFileAttachment {
        FileData Attachment { get; set; }
        string Description { get; set; }
        DateTime? ExpiryDate { get; set; }
        bool IncludeInEmail { get; set; }
        DateTime? DateUploaded { get; set; }
        string Content { get; }
        string Type { get; }
        string FileName { get; }
    }

    public interface ITransaction {
        Guid Oid { get; }
        IVendor IVendor { get; set; }
        double Debit { get; set; }
        double Credit { get; set; }
        DateTime TransactionDate { get; set; }
        string AdditionalDescription { get; set; }
    }

    public interface IVendor {
        VendorType AccountType { get; set; }
    }

    public interface IMultiplePayments
    {
    }

    public interface IModal
    {
    }

    public interface ITransactionType{
        Guid Oid { get; }
        TransactionType TransactionType { get; }
        ITransaction ITransaction { get; set; }
        IVendor IVendor { get; set; }
        IWorkflow IWorkflow { get; }
        DateTime TransactionDate { get; }
        double Amount { get; }
        string AdditionalDescription { get; }
    }

    public interface IEmailAddress : ICommunicationType
    {
        RecipientRoles RecipientRole { get; }
        string Email { get; }
        string FullName { get; }
    }

    public interface IRecipientType {
        string Description { get; }
    }

    public interface ICommunicationType : IKeyValuePair<string, string> { }

    public interface ICellNumber : ICommunicationType
    {
        RecipientRoles RecipientRole { get; }
        string CellNo { get; }
        string FullName { get; }
    }

    public interface IEmailPopup
    {
        Guid ApplicationID { get; }
        Party SendingUser { get; }
        string Title { get; }
        string ReportDisplayName { get; }
        string ReportFileName { get; }
        byte[] Text { get; set; }
        IEnumerable<IEmailAddress> EmailAddresses { get; }
        IEnumerable<IFileAttachment> EmailAttachments { get; }
    }

    public interface ITimeTracking
    {
    }

    public interface IMessagePopup
    {
        Guid ApplicationID { get; }
        Party SendingUser { get; }
        string Title { get; }
        IEnumerable<ICellNumber> CellNumbers { get; }
    }

    public interface IDetailRowMode { }

    public interface ISingletonBO { }

    public interface IInventoryLocation { }

    public interface IInventoryItem { }

    public interface IItem {
        string Description { get; set; }
        double DefaultQuantity { get; set; }
        double CostPrice { get; set; }
        double SellingPrice { get; set; }
        bool VAT { get; set; }
        string FullDescription { get; }
        double QuantityOnHand { get; }
        DateTime Created { get; set; }
    }

    public interface IStatus
    {
        Guid Oid { get; }
        string Description { get; set; }
        System.Drawing.Color Color { get; set; }
        WorkFlowType WorkFlowType { get; set; }
        int UniqueID { get; }
    }

    public interface IEventEntryObject<T> where T : Event {
        Guid Oid { get; }
        T WorkCalendarEvent { get; set; }
        DateTime? BookedTime { get; set; }
        DateTime? BookedTimeEnd { get; set; }
        string EventSubject { get; }
        string EventDescription { get; }
        string EventLocation { get; }
        IEnumerable<DevExpress.Persistent.Base.General.IResource> Resources { get; }
        Priority Priority { get; set; }
        IStatus EventStatus { get; }
        string EventFullDescription { get; }
    }

    public interface IInventoryTransactionRecord { }

    public interface IInventoryTransaction
    {
        IWorkflowType IInventoryTransactionType { get; }
        IInventoryTransactionRecord IInventoryTransactionRecord { get; set; }
        IItem IItem { get; }
        double Quantity { get; set; }
        Nullable<bool> Delivered { get; }
        DateTime TransactionDate { get; }
        IInventoryLocation IInventoryLocation { get; }
        WorkFlowType MovementType { get; }
        bool ReduceInventory { get; }

        IEnumerable<IInventoryTransaction> RelatedTransactions { get; }
    }
}
