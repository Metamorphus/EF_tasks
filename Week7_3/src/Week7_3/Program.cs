using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week7_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (northwindContext db = new northwindContext())
            {
                DStarters(db);
                CapitalizeNames(db);
                DistinctCountries(db);
                LondonDrugDealers(db);
                TofuPurchases(db);
                GermanProducts(db);
                IkuraBuyers(db);
                EmployeesAndOrders(db);
                OrdersAndEmployees(db);
                AllPhones(db);
                CustomersByCity(db);
                SymmetricalPhones(db);
                BestCustomer(db);
            }
        }

        public static void DStarters(northwindContext db)
        {
            var ds = db.Customers.Where(cus => cus.ContactName[0] == 'D').ToList();
            foreach (var cus in ds)
            {
                Console.WriteLine(cus.ContactName);
            }
        }

        public static void CapitalizeNames(northwindContext db)
        {
            var ds = db.Customers.ToList();
            foreach (var cus in ds)
            {
                cus.ContactName = cus.ContactName.ToUpper();
            }
            db.SaveChanges();
        }

        public static void DistinctCountries(northwindContext db)
        {
            var ds = db.Customers.Select(cus => cus.Country).Distinct().ToList();
            foreach (var cus in ds)
            {
                Console.WriteLine(cus);
            }
            Console.WriteLine();
        }

        public static void LondonDrugDealers(northwindContext db)
        {
            var ds = db.Customers.Where(cus => cus.ContactTitle.StartsWith("Sales") && cus.City.Equals("London")).ToList();
            foreach (var cus in ds)
            {
                Console.WriteLine(cus.ContactName);
            }
            Console.WriteLine();
        }

        public static void TofuPurchases(northwindContext db)
        {
            var orderDetails = db.OrderDetails.ToList();
            var products = db.Products.ToList();
            var res = from p in products
                      join od in orderDetails on p.ProductId equals od.ProductId
                      where p.ProductName.Equals("Tofu")
                      select od.OrderId;
            foreach (var id in res)
            {
                Console.WriteLine(id);
            }
            Console.WriteLine();
        }

        public static void GermanProducts(northwindContext db)
        {
            var orderDetails = db.OrderDetails.ToList();
            var products = db.Products.ToList();
            var orders = db.Orders.ToList();
            var res = from p in products
                      join od in orderDetails on p.ProductId equals od.ProductId
                      join o in orders on od.OrderId equals o.OrderId
                      where o.ShipCountry.Equals("Germany")
                      select p.ProductName;
            Console.WriteLine(res.Count());
            Console.WriteLine();
        }

        public static void IkuraBuyers(northwindContext db)
        {
            var orderDetails = db.OrderDetails.ToList();
            var products = db.Products.ToList();
            var orders = db.Orders.ToList();
            var res = from p in products
                      join od in orderDetails on p.ProductId equals od.ProductId
                      join o in orders on od.OrderId equals o.OrderId
                      where p.ProductName.Equals("Ikura")
                      select o.CustomerId;
            foreach (var id in res)
            {
                Console.WriteLine(id);
            }
            Console.WriteLine();
        }

        public static void EmployeesAndOrders(northwindContext db)
        {
            var employees = db.Employees.ToList();
            var orders = db.Orders.ToList();
            var res = from e in employees
                      join o in orders on e.EmployeeId equals o.EmployeeId into oe
                      from u in oe.DefaultIfEmpty()
                      select new { u.EmployeeId, u.OrderId };
            Console.WriteLine(res.Count());
            Console.WriteLine();
        }

        public static void OrdersAndEmployees(northwindContext db)
        {
            var employees = db.Employees.ToList();
            var orders = db.Orders.ToList();
            var res = from o in orders
                      join e in employees on o.EmployeeId equals e.EmployeeId into oe
                      from u in oe.DefaultIfEmpty()
                      select new { o.OrderId, u.EmployeeId };
            Console.WriteLine(res.Count());
            Console.WriteLine();
        }

        public static void AllPhones(northwindContext db)
        {
            var shipperPhones = db.Shippers.Select(sh => sh.Phone).ToList();
            var supplierPhones = db.Suppliers.Select(sup => sup.Phone).ToList();
            var res = shipperPhones.Union(supplierPhones);
            foreach (var phone in res)
            {
                Console.WriteLine(phone);
            }
            Console.WriteLine();
        }

        public static void CustomersByCity(northwindContext db)
        {
            var res = db.Customers.GroupBy(cus => cus.City).
                Select(cu => new { City = cu.First().City, Count = cu.Count() }).ToList();
            foreach (var inst in res)
            {
                Console.WriteLine(inst.City + " " + inst.Count);
            }
            Console.WriteLine();
        }

        public static void SymmetricalPhones(northwindContext db)
        {
            var res = db.Customers.Where(c => c.Phone.Count() == 9 && c.Phone[4] == '-').
                Select(c => c.Phone).ToList();
            foreach (var phone in res)
            {
                Console.WriteLine(phone);
            }
            Console.WriteLine();
        }

        public static void BestCustomer(northwindContext db)
        {
            var res = db.Orders.GroupBy(o => o.CustomerId)
                .Select(o => new { Id = o.First().CustomerId, Count = o.Count() })
                .OrderByDescending(x => x.Count).First();
            Console.WriteLine(res.Id);
            Console.WriteLine();
        }
    }
}
