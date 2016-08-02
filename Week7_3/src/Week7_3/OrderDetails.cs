using System;
using System.Collections.Generic;

namespace Week7_3
{
    public partial class OrderDetails
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public string UnitPrice { get; set; }
        public long Quantity { get; set; }
        public string Discount { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }
}
