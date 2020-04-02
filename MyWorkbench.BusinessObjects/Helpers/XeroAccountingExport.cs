using DevExpress.Xpo;
using Ignyt.Webfunctions;
using MyWorkbench.BusinessObjects.Accounts;
using MyWorkbench.BusinessObjects.BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public class XeroAccountingExport : AccountingPackageExport
    {
        private Xero.Api.Core.Model.Invoice _invoiceResult = null;

        public XeroAccountingExport(Session Session) : base(Session) { }

        public override void ExportInvoices(IEnumerable<Invoice> Invoices)
        {
            foreach (Invoice invoice in Invoices)
            {
                this.ExportInvoice(invoice);
            }
        }

        public override void ExportInvoice(Invoice Invoice)
        {
            try
            {
                _invoiceResult = XeroFunctions.CoreApi().Invoices.Create(CreateInvoice(Invoice));

                Invoice.ExportedDateTime = MyWorkbench.BaseObjects.Constants.Constants.DateTimeTimeZone(Invoice.Session);
                Invoice.Save();


                Results.Add(new AccountingExport(this.Session) { ObjectOid = Invoice.Oid, Description = string.Concat(Invoice.No, " - ", Invoice.ExportedDateTime == null ? "Successfully exported" : "Update Successfully exported") });
            }
            catch (Exception ex)
            {
                Results.Add(new AccountingExport(Invoice.Session) { ObjectOid = Invoice.Oid, Description = string.Concat(Invoice.No, " - ", ex.ToString().Substring(0, 250)) });
            }
        }

        private Xero.Api.Core.Model.Invoice CreateInvoice(Invoice Invoice)
        {
            Xero.Api.Core.Model.Invoice xeroInvoice = new Xero.Api.Core.Model.Invoice
            {
                Number = Invoice.No,
                Date = Invoice.TransactionDate,
                Type = Xero.Api.Core.Model.Types.InvoiceType.AccountsReceivable,
                Status = Xero.Api.Core.Model.Status.InvoiceStatus.Authorised,
                Reference = Invoice.ReferenceNumber,
                CurrencyCode = Invoice.Currency.Value,
                SubTotal = (decimal)Invoice.SubTotalExcl,
                TotalTax = (decimal)Invoice.VATTotal,
                Total = (decimal)Invoice.TotalIncl,
                AmountPaid = (decimal)Invoice.PaymentTotal,
                AmountDue = (decimal)Invoice.AmountOutstanding,
                AmountCredited = (decimal)Invoice.CreditTotal,
                Contact = XeroFunctions.CoreApi().Contacts.Find(Invoice.Vendor.AccountingPartnerIdentifier)
            };

            Invoice.AccountingPartnerIdentifier = xeroInvoice.Id.ToString();

            if (xeroInvoice.Contact == null)
            {
                xeroInvoice.Contact = new Xero.Api.Core.Model.Contact()
                {
                    Name = Invoice.Vendor.FullName,
                    IsCustomer = true,
                    FirstName = Invoice.Vendor.FirstName,
                    LastName = Invoice.Vendor.LastName,
                    TaxNumber = Invoice.Vendor.VATNo,
                    EmailAddress = Invoice.Vendor.Email,
                    ContactNumber = Invoice.Vendor.CellNo,
                    AccountNumber = Invoice.Vendor.ExternalAccountsCode
                };

                Invoice.Vendor.AccountingPartnerIdentifier = xeroInvoice.Contact.Id.ToString();
            }

            foreach (WorkflowItem workflowItem in Invoice.Items)
            {
                if (xeroInvoice.LineItems == null)
                    xeroInvoice.LineItems = new List<Xero.Api.Core.Model.LineItem>();

                Xero.Api.Core.Model.LineItem lineItem = new Xero.Api.Core.Model.LineItem()
                {
                    Description = workflowItem.Description,
                    Quantity = (decimal)workflowItem.Quantity,
                    UnitAmount = (decimal)workflowItem.SellingPrice,
                    TaxAmount = (decimal)workflowItem.VATTotal,
                    LineAmount = (decimal)workflowItem.TotalIncl
                };

                xeroInvoice.LineItems.Add(lineItem);

                workflowItem.AccountingPartnerIdentifier = lineItem.LineItemId.ToString();
            }

            foreach (WorkFlowPayment workFlowPayment in Invoice.Payments)
            {
                if (xeroInvoice.Payments == null)
                    xeroInvoice.Payments = new List<Xero.Api.Core.Model.Payment>();

                Xero.Api.Core.Model.Payment payment = new Xero.Api.Core.Model.Payment()
                {
                    Amount = (decimal)workFlowPayment.Amount,
                    Date = workFlowPayment.PaymentDate,
                    Type = Xero.Api.Core.Model.Types.PaymentType.AccountsReceivablePrepayment
                };

                xeroInvoice.Payments.Add(payment);

                workFlowPayment.AccountingPartnerIdentifier = xeroInvoice.Contact.Id.ToString();
            }

            foreach (CreditNote creditNote in Invoice.ChildItems.Where(g => g.WorkFlowType == Ignyt.BusinessInterface.WorkFlowType.CreditNote))
            {
                if (xeroInvoice.CreditNotes == null)
                    xeroInvoice.CreditNotes = new List<Xero.Api.Core.Model.CreditNote>();

                Xero.Api.Core.Model.CreditNote xeroCreditNote = XeroFunctions.CoreApi().CreditNotes.Find(creditNote.AccountingPartnerIdentifier);

                if (xeroCreditNote == null)
                    xeroInvoice.CreditNotes.Add(CreateCreditNote(creditNote));
                else
                    xeroInvoice.CreditNotes.Add(xeroCreditNote);
            }

            return xeroInvoice;
        }

        private Xero.Api.Core.Model.CreditNote CreateCreditNote(CreditNote CreditNote)
        {
            Xero.Api.Core.Model.CreditNote xeroCreditNote = new Xero.Api.Core.Model.CreditNote
            {
                Number = CreditNote.No,
                Date = CreditNote.TransactionDate,
                Type = Xero.Api.Core.Model.Types.CreditNoteType.AccountsPayable,
                Status = Xero.Api.Core.Model.Status.InvoiceStatus.Authorised,
                Reference = CreditNote.ReferenceNumber,
                CurrencyCode = CreditNote.Currency.Value,
                SubTotal = (decimal)CreditNote.SubTotalExcl,
                TotalTax = (decimal)CreditNote.VATTotal,
                Total = (decimal)CreditNote.TotalIncl,
                Contact = XeroFunctions.CoreApi().Contacts.Find(CreditNote.Vendor.AccountingPartnerIdentifier)
            };

            CreditNote.AccountingPartnerIdentifier = xeroCreditNote.Id.ToString();

            if (xeroCreditNote.Contact == null)
            {
                xeroCreditNote.Contact = new Xero.Api.Core.Model.Contact()
                {
                    Name = CreditNote.Vendor.FullName,
                    IsCustomer = true,
                    FirstName = CreditNote.Vendor.FirstName,
                    LastName = CreditNote.Vendor.LastName,
                    TaxNumber = CreditNote.Vendor.VATNo,
                    EmailAddress = CreditNote.Vendor.Email,
                    ContactNumber = CreditNote.Vendor.CellNo,
                    AccountNumber = CreditNote.Vendor.ExternalAccountsCode
                };

                CreditNote.Vendor.AccountingPartnerIdentifier = xeroCreditNote.Contact.Id.ToString();
            }

            foreach (WorkflowItem workflowItem in CreditNote.Items)
            {
                if (xeroCreditNote.LineItems == null)
                    xeroCreditNote.LineItems = new List<Xero.Api.Core.Model.LineItem>();

                Xero.Api.Core.Model.LineItem lineItem = new Xero.Api.Core.Model.LineItem()
                {
                    Description = workflowItem.Description,
                    Quantity = (decimal)workflowItem.Quantity,
                    UnitAmount = (decimal)workflowItem.SellingPrice,
                    TaxAmount = (decimal)workflowItem.VATTotal,
                    LineAmount = (decimal)workflowItem.TotalIncl
                };

                xeroCreditNote.LineItems.Add(lineItem);

                workflowItem.AccountingPartnerIdentifier = lineItem.LineItemId.ToString();
            }

            return xeroCreditNote;
        }

        private Xero.Api.Core.Model.Invoice UpdateInvoice(Invoice Invoice)
        {
            Xero.Api.Core.Model.Invoice xeroInvoice = XeroFunctions.CoreApi().Invoices.Find((Guid.Parse(Invoice.AccountingPartnerIdentifier)));

            xeroInvoice.SubTotal = (decimal)Invoice.SubTotalExcl;
            xeroInvoice.TotalTax = (decimal)Invoice.VATTotal;
            xeroInvoice.Total = (decimal)Invoice.TotalIncl;
            xeroInvoice.AmountPaid = (decimal)Invoice.PaymentTotal;
            xeroInvoice.AmountDue = (decimal)Invoice.AmountOutstanding;

            xeroInvoice.Contact.Name = Invoice.Vendor.FullName;
            xeroInvoice.Contact.IsCustomer = true;
            xeroInvoice.Contact.FirstName = Invoice.Vendor.FirstName;
            xeroInvoice.Contact.LastName = Invoice.Vendor.LastName;
            xeroInvoice.Contact.TaxNumber = Invoice.Vendor.VATNo;
            xeroInvoice.Contact.EmailAddress = Invoice.Vendor.Email;
            xeroInvoice.Contact.ContactNumber = Invoice.Vendor.CellNo;

            return xeroInvoice;
        }
    }
}
