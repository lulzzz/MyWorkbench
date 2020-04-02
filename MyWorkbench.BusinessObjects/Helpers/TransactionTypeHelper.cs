using DevExpress.Persistent.BaseImpl;
using Ignyt.Framework;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.Accounts;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Inventory;
using MyWorkbench.BusinessObjects.Lookups;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace MyWorkbench.BusinessObjects.Helpers {
    public class TransactionTypeHelper : SingletonBase<TransactionTypeHelper> {
        #region Private Fields
        private static readonly Dictionary<TransactionType, ITransactionStrategy> _strategies = new Dictionary<TransactionType, ITransactionStrategy>();
        #endregion

        #region Constructor
        private TransactionTypeHelper() {
            _strategies.Add(TransactionType.Payment, new PaymentStrategy());
            _strategies.Add(TransactionType.Invoice, new InvoiceStrategy());
            _strategies.Add(TransactionType.CreditNote, new CreditNoteStrategy());
            _strategies.Add(TransactionType.Purchase, new PurchaseStrategy());
            _strategies.Add(TransactionType.PurchasePayment, new PurchasePaymentStrategy());
            _strategies.Add(TransactionType.Sale, new SaleStrategy());
            _strategies.Add(TransactionType.SupplierInvoice, new SupplierInvoiceStrategy());
            _strategies.Add(TransactionType.SupplierPayment, new SupplierPaymentStrategy());
        }
        #endregion

        #region Methods
        public void SaveTransaction(ITransactionType TransactionType) {
            _strategies[TransactionType.TransactionType].SaveTransaction(TransactionType);
        }

        public void DeleteTransaction(ITransactionType TransactionType) {
            _strategies[TransactionType.TransactionType].DeleteTransaction(TransactionType);
        }
        #endregion

        #region Strategies
        interface ITransactionStrategy {
            void SaveTransaction(ITransactionType TransactionType);
            void DeleteTransaction(ITransactionType TransactionType);
        }

        class PaymentStrategy : ITransactionStrategy {
            public void SaveTransaction(ITransactionType TransactionType)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<WorkFlowPayment>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction == null)
                    {
                        transactionType.ITransaction = new Transaction(unitOfWork)
                        {
                            Vendor = transactionType.IVendor as Vendor,
                            Payment = transactionType as WorkFlowPayment,
                            Credit = transactionType.Amount,
                            TransactionDate = transactionType.TransactionDate,
                            AdditionalDescription = transactionType.AdditionalDescription
                        };
                    }
                    else
                    {
                        transactionType.ITransaction.IVendor = transactionType.IVendor;
                        transactionType.ITransaction.Credit = transactionType.Amount;
                        transactionType.ITransaction.TransactionDate = transactionType.TransactionDate;
                        transactionType.ITransaction.AdditionalDescription = transactionType.AdditionalDescription;
                    }

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }

            public void DeleteTransaction(ITransactionType TransactionType) {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<WorkFlowPayment>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction != null)
                        (transactionType.ITransaction as Transaction).Delete();

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }
        }

        class InvoiceStrategy : ITransactionStrategy {
            public void SaveTransaction(ITransactionType TransactionType) {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<Invoice>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction == null)
                    {
                        transactionType.ITransaction = new Transaction(unitOfWork)
                        {
                            Workflow = transactionType.IWorkflow as WorkflowBase,
                            Vendor = transactionType.IVendor as Vendor,
                            Debit = transactionType.Amount,
                            TransactionDate = transactionType.TransactionDate,
                            AdditionalDescription = transactionType.AdditionalDescription
                        };
                    }
                    else
                    {
                        transactionType.ITransaction.IVendor = transactionType.IVendor;
                        transactionType.ITransaction.Debit = transactionType.Amount;
                        transactionType.ITransaction.TransactionDate = transactionType.TransactionDate;
                        transactionType.ITransaction.AdditionalDescription = transactionType.AdditionalDescription;
                    }

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }

            public void DeleteTransaction(ITransactionType TransactionType) {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<Invoice>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction != null)
                        (transactionType.ITransaction as Transaction).Delete();

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }
        }

        class SaleStrategy : ITransactionStrategy
        {
            public void SaveTransaction(ITransactionType TransactionType)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<Sale>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction == null)
                    {
                        transactionType.ITransaction = new Transaction(unitOfWork)
                        {
                            Workflow = transactionType.IWorkflow as WorkflowBase,
                            Vendor = transactionType.IVendor as Vendor,
                            Debit = transactionType.Amount,
                            TransactionDate = transactionType.TransactionDate,
                            AdditionalDescription = transactionType.AdditionalDescription
                        };
                    }
                    else
                    {
                        transactionType.ITransaction.IVendor = transactionType.IVendor;
                        transactionType.ITransaction.Debit = transactionType.Amount;
                        transactionType.ITransaction.TransactionDate = transactionType.TransactionDate;
                        transactionType.ITransaction.AdditionalDescription = transactionType.AdditionalDescription;
                    }

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }

            public void DeleteTransaction(ITransactionType TransactionType)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<Sale>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction != null)
                        (transactionType.ITransaction as Transaction).Delete();

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }
        }

        class CreditNoteStrategy : ITransactionStrategy {
            public void SaveTransaction(ITransactionType TransactionType) {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<CreditNote>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction == null)
                    {
                        if (transactionType.IVendor.AccountType == VendorType.Client)
                        {
                            transactionType.ITransaction = new Transaction(unitOfWork)
                            {
                                Workflow = transactionType.IWorkflow as WorkflowBase,
                                Vendor = transactionType.IVendor as Vendor,
                                Credit = transactionType.Amount,
                                TransactionDate = transactionType.TransactionDate,
                                AdditionalDescription = transactionType.AdditionalDescription
                            };
                        }
                        else
                        {
                            transactionType.ITransaction = new Transaction(unitOfWork)
                            {
                                Workflow = transactionType.IWorkflow as WorkflowBase,
                                Vendor = transactionType.IVendor as Vendor,
                                Debit = transactionType.Amount,
                                TransactionDate = transactionType.TransactionDate,
                                AdditionalDescription = transactionType.AdditionalDescription
                            };
                        }
                    }
                    else
                    {
                        if (transactionType.IVendor.AccountType == VendorType.Client)
                        {
                            transactionType.ITransaction.IVendor = transactionType.IVendor;
                            transactionType.ITransaction.Credit = transactionType.Amount;
                            transactionType.ITransaction.TransactionDate = transactionType.TransactionDate;
                            transactionType.ITransaction.AdditionalDescription = transactionType.AdditionalDescription;
                        }
                        else
                        {
                            transactionType.ITransaction.IVendor = transactionType.IVendor;
                            transactionType.ITransaction.Debit = transactionType.Amount;
                            transactionType.ITransaction.TransactionDate = transactionType.TransactionDate;
                            transactionType.ITransaction.AdditionalDescription = transactionType.AdditionalDescription;
                        }
                    }

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }

            public void DeleteTransaction(ITransactionType TransactionType) {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<CreditNote>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction != null)
                        (transactionType.ITransaction as Transaction).Delete();

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }
        }

        class PurchaseStrategy : ITransactionStrategy
        {
            public void SaveTransaction(ITransactionType TransactionType)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<Purchase>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction == null)
                    {
                        transactionType.ITransaction = new Transaction(unitOfWork)
                        {
                            Workflow = transactionType.IWorkflow as WorkflowBase,
                            Vendor = transactionType.IVendor as Vendor,
                            Credit = transactionType.Amount,
                            TransactionDate = transactionType.TransactionDate,
                            AdditionalDescription = transactionType.AdditionalDescription
                        };
                    }
                    else
                    {

                        transactionType.ITransaction.IVendor = transactionType.IVendor;
                        transactionType.ITransaction.Credit = transactionType.Amount;
                        transactionType.ITransaction.Debit = 0;
                        transactionType.ITransaction.TransactionDate = transactionType.TransactionDate;
                        transactionType.ITransaction.AdditionalDescription = transactionType.AdditionalDescription;
                    }

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }

            public void DeleteTransaction(ITransactionType TransactionType)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<Purchase>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction != null)
                        (transactionType.ITransaction as Transaction).Delete();

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }
        }

        class SupplierInvoiceStrategy : ITransactionStrategy
        {
            public void SaveTransaction(ITransactionType TransactionType)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<SupplierInvoice>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction == null)
                    {
                        transactionType.ITransaction = new Transaction(unitOfWork)
                        {
                            Workflow = transactionType.IWorkflow as WorkflowBase,
                            Vendor = transactionType.IVendor as Vendor,
                            Credit = transactionType.Amount,
                            TransactionDate = transactionType.TransactionDate,
                            AdditionalDescription = transactionType.AdditionalDescription
                        };
                    }
                    else
                    {
                        transactionType.ITransaction.IVendor = transactionType.IVendor;
                        transactionType.ITransaction.Credit = transactionType.Amount;
                        transactionType.ITransaction.Debit = 0;
                        transactionType.ITransaction.TransactionDate = transactionType.TransactionDate;
                        transactionType.ITransaction.AdditionalDescription = transactionType.AdditionalDescription;
                    }

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }

            public void DeleteTransaction(ITransactionType TransactionType)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<SupplierInvoice>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction != null)
                        (transactionType.ITransaction as Transaction).Delete();

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }
        }

        class PurchasePaymentStrategy : ITransactionStrategy {
            public void SaveTransaction(ITransactionType TransactionType) {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<WorkFlowPayment>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction == null)
                    {
                        transactionType.ITransaction = new Transaction(unitOfWork)
                        {
                            Vendor = transactionType.IVendor as Vendor,
                            Payment = transactionType as WorkFlowPayment,
                            Debit = transactionType.Amount,
                            TransactionDate = transactionType.TransactionDate,
                            AdditionalDescription = transactionType.AdditionalDescription
                        };
                    }
                    else
                    {
                        transactionType.ITransaction.IVendor = transactionType.IVendor;
                        transactionType.ITransaction.Debit = transactionType.Amount;
                        transactionType.ITransaction.Credit = 0;
                        transactionType.ITransaction.TransactionDate = transactionType.TransactionDate;
                        transactionType.ITransaction.AdditionalDescription = transactionType.AdditionalDescription;
                    }

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }

            public void DeleteTransaction(ITransactionType TransactionType) {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<WorkFlowPayment>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction != null)
                        (transactionType.ITransaction as Transaction).Delete();

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }
        }

        class SupplierPaymentStrategy : ITransactionStrategy
        {
            public void SaveTransaction(ITransactionType TransactionType)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<WorkFlowPayment>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction == null)
                    {
                        transactionType.ITransaction = new Transaction(unitOfWork)
                        {
                            Vendor = transactionType.IVendor as Vendor,
                            Payment = transactionType as WorkFlowPayment,
                            Debit = transactionType.Amount,
                            TransactionDate = transactionType.TransactionDate,
                            AdditionalDescription = transactionType.AdditionalDescription
                        };
                    }
                    else
                    {
                        transactionType.ITransaction.IVendor = transactionType.IVendor;
                        transactionType.ITransaction.Debit = transactionType.Amount;
                        transactionType.ITransaction.Credit = 0;
                        transactionType.ITransaction.TransactionDate = transactionType.TransactionDate;
                        transactionType.ITransaction.AdditionalDescription = transactionType.AdditionalDescription;
                    }

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }

            public void DeleteTransaction(ITransactionType TransactionType)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork((TransactionType as BaseObject).Session.DataLayer))
                {
                    ITransactionType transactionType = unitOfWork.FindObject<WorkFlowPayment>(CriteriaOperator.Parse("Oid == ?", TransactionType.Oid));

                    if (transactionType == null)
                        return;

                    if (transactionType.ITransaction != null)
                        (transactionType.ITransaction as Transaction).Delete();

                    unitOfWork.CommitChanges();
                    (TransactionType as BaseObject).Session.Reload(TransactionType);
                }
            }
        }
        #endregion
    }
}
