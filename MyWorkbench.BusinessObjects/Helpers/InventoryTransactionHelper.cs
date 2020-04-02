using DevExpress.Persistent.BaseImpl;
using Ignyt.BusinessInterface;
using Ignyt.BusinessInterface.Attributes;
using MyWorkbench.BusinessObjects.Inventory;
using MyWorkbench.BusinessObjects.Lookups;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public static class IInventoryTransactionHelper
    {
        public static void UpdateInventoryTransaction(IInventoryTransaction IInventoryTransaction)
        {
            if (IInventoryTransaction.MovementType.GetAttribute<InventoryIn>() != null)
            {
                if (IInventoryTransaction.MovementType.GetAttribute<InventoryIn>().ToString().ToLower() == "true" && (IInventoryTransaction.Delivered == false || IInventoryTransaction.Delivered == null))
                    return;

                if (IInventoryTransaction.IInventoryTransactionRecord == null)
                    IInventoryTransaction.IInventoryTransactionRecord = new InventoryTransaction((IInventoryTransaction as BaseObject).Session);

                if (IInventoryTransaction.MovementType.GetAttribute<InventoryIn>().ToString().ToLower() == "true")
                    (IInventoryTransaction.IInventoryTransactionRecord as InventoryTransaction).InventoryIn = IInventoryTransaction.Quantity;
                else
                    (IInventoryTransaction.IInventoryTransactionRecord as InventoryTransaction).InventoryOut = IInventoryTransaction.Quantity;

                (IInventoryTransaction.IInventoryTransactionRecord as InventoryTransaction).Item = IInventoryTransaction.IItem as Item;
                (IInventoryTransaction.IInventoryTransactionRecord as InventoryTransaction).Delivered = IInventoryTransaction.Delivered;
                (IInventoryTransaction.IInventoryTransactionRecord as InventoryTransaction).InventoryLocation = IInventoryTransaction.IInventoryLocation as InventoryLocation;
                (IInventoryTransaction.IInventoryTransactionRecord as InventoryTransaction).Description = IInventoryTransaction.MovementType.ToString();

                (IInventoryTransaction.IInventoryTransactionRecord as InventoryTransaction).Save();
            }
        }

        public static void DeleteInventoryTransaction(IInventoryTransaction IInventoryTransaction)
        {
            if (IInventoryTransaction.MovementType.GetAttribute<InventoryIn>() != null)
            {
                if (IInventoryTransaction.IInventoryTransactionRecord != null)
                {
                    (IInventoryTransaction.IInventoryTransactionRecord as InventoryTransaction).Delete();
                    (IInventoryTransaction.IInventoryTransactionRecord as InventoryTransaction).Save();
                }
            }
        }
    }
}
