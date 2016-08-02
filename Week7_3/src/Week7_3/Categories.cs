using System;
using System.Collections.Generic;

namespace Week7_3
{
    public partial class Categories
    {
        public Categories()
        {
            ProductCategoryMap = new HashSet<ProductCategoryMap>();
            Products = new HashSet<Products>();
        }

        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public virtual ICollection<ProductCategoryMap> ProductCategoryMap { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}
