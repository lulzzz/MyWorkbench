using DevExpress.Persistent.BaseImpl;
using Ignyt.BusinessInterface;
using MyWorkbench.BusinessObjects.BaseObjects;
using MyWorkbench.BusinessObjects.Inventory;
using MyWorkbench.BusinessObjects.Lookups;
using MyWorkbench.BaseObjects.Constants;

namespace MyWorkbench.BusinessObjects.Helpers {
    public static class IWorkflowItemHelper {
        #region Methods
        public static void PopulateItemDefaults(this IWorkflowItem<WorkflowBase, InventoryLocation, Item> Item) {
            Item.Description = Item.Item.Description;
            Item.CostPrice = Item.Item.CostPrice;
            Item.SellingPrice = Item.Item.SellingPrice;
            Item.VAT = Item.Item.VAT;
            Item.Quantity = Item.Item.DefaultQuantity == 0 ? 1 : Item.Item.DefaultQuantity;
            Item.VatPercentage = Item.Item.VAT ? Constants.VatPercentage((Item as BaseObject).Session) : 0;
        }
        #endregion
    }
}
