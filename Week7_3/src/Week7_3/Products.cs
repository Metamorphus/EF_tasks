using System;
using System.Collections.Generic;

namespace Week7_3
{
    public partial class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
            ProductCategoryMap = new HashSet<ProductCategoryMap>();
        }

        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long? SupplierId { get; set; }
        public long? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public string UnitPrice { get; set; }
        public long? UnitsInStock { get; set; }
        public long? UnitsOnOrder { get; set; }
        public long? ReorderLevel { get; set; }
        public string Discontinued { get; set; }
        public string AttributeXml { get; set; }
        public string DateCreated { get; set; }
        public string ProductGuid { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Deleted { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<ProductCategoryMap> ProductCategoryMap { get; set; }
        public virtual Categories Category { get; set; }
        public virtual Suppliers Supplier { get; set; }
    }
}
