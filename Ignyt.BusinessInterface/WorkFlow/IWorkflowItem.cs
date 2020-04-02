using DevExpress.Xpo;

namespace Ignyt.BusinessInterface {
    public interface INestedWorkflowItem<S, T, U, V> : IWorkflowItem<T, U, V> where T : IWorkflow where U : IInventoryLocation where V : IItem {
        S ParentItem { get; set; }
        XPCollection<S> ChildItems { get; }
    }

    public interface IWorkflowItem<T, U, V> where T : IWorkflow where U : IInventoryLocation where V : IItem  {
        T Workflow { get; set; }
        U InventoryLocation { get; set; }
        V Item { get; set; }
        string Description { get; set; }
        double Quantity { get; set; }
        double CostPrice { get; set; }
        double SellingPrice { get; set; }
        bool VAT { get; set; }
        double VatPercentage { get; set; }
        double TotalCostExcl { get; }
        double TotalExcl { get; }
        double VATTotal { get; }
        double TotalIncl { get; }
        double QuanityOnHand { get; }
    }
}
