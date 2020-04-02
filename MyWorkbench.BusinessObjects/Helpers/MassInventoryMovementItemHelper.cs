using DevExpress.Xpo;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Inventory;
using MyWorkbench.BusinessObjects.Lookups;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public static class MassInventoryMovementItemHelper
    {
        #region Methods
        public static void UpdateInventoryTransaction(this MassInventoryMovementItem MassInventoryMovementItem)
        {
            if (MassInventoryMovementItem != null)
            {
                XPCollection<InventoryTransaction> colDelete = MassInventoryMovementItem.InventoryTransactions;
                MassInventoryMovementItem.Session.Delete(colDelete);
                MassInventoryMovementItem.Session.Save(colDelete);

                //If item is not being disposed of
                if (!MassInventoryMovementItem.MassInventoryMovement.DisposeOf)
                {
                    MassInventoryMovementItem.InventoryTransactions.Add(new InventoryTransaction(MassInventoryMovementItem.Session)
                    {
                        Item = MassInventoryMovementItem.Item,
                        TransactionDate = MassInventoryMovementItem.DateTime,
                        InventoryIn = MassInventoryMovementItem.Quantity,
                        Delivered = true,
                        Description = MassInventoryMovementItem.MassInventoryMovement.Reason.ToString(),
                        InventoryLocation = MassInventoryMovementItem.MassInventoryMovement.ToLocation
                    });
                }

                MassInventoryMovementItem.InventoryTransactions.Add(new InventoryTransaction(MassInventoryMovementItem.Session)
                {
                    Item = MassInventoryMovementItem.Item,
                    TransactionDate = MassInventoryMovementItem.DateTime,
                    InventoryOut = MassInventoryMovementItem.Quantity,
                    Delivered = true,
                    Description = MassInventoryMovementItem.MassInventoryMovement.Reason.ToString(),
                    InventoryLocation = MassInventoryMovementItem.MassInventoryMovement.FromLocation
                });
            }
        }
        #endregion
    }
}
