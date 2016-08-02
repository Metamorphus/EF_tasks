using System;
using System.Collections.Generic;

namespace Week7_3
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public long OrderId { get; set; }
        public string CustomerId { get; set; }
        public long? EmployeeId { get; set; }
        public string OrderDate { get; set; }
        public string RequiredDate { get; set; }
        public string ShippedDate { get; set; }
        public long? ShipVia { get; set; }
        public string Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual Customers Customer { get; set; }
        public virtual Employees Employee { get; set; }
        public virtual Shippers ShipViaNavigation { get; set; }
    }
}
