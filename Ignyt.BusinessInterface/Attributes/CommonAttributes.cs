using System;

namespace Ignyt.BusinessInterface.Attributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class Color : Attribute {
        public string color;

        public Color(string Color) {
            this.color = Color;
        }

        public override string ToString() {
            return color;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class Description : Attribute
    {
        public string description;

        public Description(string Description)
        {
            this.description = Description;
        }

        public override string ToString()
        {
            return description;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class InventoryIn : Attribute
    {
        public bool inventoryIn;

        public InventoryIn(bool InventoryIn)
        {
            this.inventoryIn = InventoryIn;
        }

        public override string ToString()
        {
            return inventoryIn.ToString();
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class VendorAccountType : Attribute
    {
        public VendorType VendorType;

        public VendorAccountType(VendorType VendorType)
        {
            this.VendorType = VendorType;
        }

        public override string ToString()
        {
            return VendorType.ToString();
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ListViewSort : Attribute {
        public DevExpress.Data.ColumnSortOrder sortingDirection;

        public ListViewSort(DevExpress.Data.ColumnSortOrder SortingDirection) {
            this.sortingDirection = SortingDirection;
        }
    }
}
