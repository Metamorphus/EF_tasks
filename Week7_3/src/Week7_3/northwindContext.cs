using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Week7_3
{
    public partial class northwindContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlite(@"Data Source=northwind.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("sqlite_autoindex_Categories_1");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasColumnType("int");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnType("nvarchar");

                entity.Property(e => e.Description).HasColumnType("nvarchar");

                entity.Property(e => e.Picture).HasColumnType("blob");
            });

            modelBuilder.Entity<CustomerCustomerDemo>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.CustomerTypeId })
                    .HasName("sqlite_autoindex_CustomerCustomerDemo_1");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasColumnType("char");

                entity.Property(e => e.CustomerTypeId)
                    .HasColumnName("CustomerTypeID")
                    .HasColumnType("char");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerCustomerDemo)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.CustomerCustomerDemo)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CustomerDemographics>(entity =>
            {
                entity.HasKey(e => e.CustomerTypeId)
                    .HasName("sqlite_autoindex_CustomerDemographics_1");

                entity.Property(e => e.CustomerTypeId)
                    .HasColumnName("CustomerTypeID")
                    .HasColumnType("char");

                entity.Property(e => e.CustomerDesc).HasColumnType("nvarchar");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("sqlite_autoindex_Customers_1");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasColumnType("char");

                entity.Property(e => e.Address).HasColumnType("nvarchar");

                entity.Property(e => e.City).HasColumnType("nvarchar");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnType("nvarchar");

                entity.Property(e => e.ContactName).HasColumnType("nvarchar");

                entity.Property(e => e.ContactTitle).HasColumnType("nvarchar");

                entity.Property(e => e.Country).HasColumnType("nvarchar");

                entity.Property(e => e.Fax).HasColumnType("nvarchar");

                entity.Property(e => e.Phone).HasColumnType("nvarchar");

                entity.Property(e => e.PostalCode).HasColumnType("nvarchar");

                entity.Property(e => e.Region).HasColumnType("nvarchar");
            });

            modelBuilder.Entity<EmployeeTerritories>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.TerritoryId })
                    .HasName("sqlite_autoindex_EmployeeTerritories_1");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("EmployeeID")
                    .HasColumnType("int");

                entity.Property(e => e.TerritoryId)
                    .HasColumnName("TerritoryID")
                    .HasColumnType("nvarchar");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeTerritories)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Territory)
                    .WithMany(p => p.EmployeeTerritories)
                    .HasForeignKey(d => d.TerritoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("sqlite_autoindex_Employees_1");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("EmployeeID")
                    .HasColumnType("int");

                entity.Property(e => e.Address).HasColumnType("nvarchar");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City).HasColumnType("nvarchar");

                entity.Property(e => e.Country).HasColumnType("nvarchar");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Extension).HasColumnType("nvarchar");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("nvarchar");

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.HomePhone).HasColumnType("nvarchar");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("nvarchar");

                entity.Property(e => e.Notes).HasColumnType("nvarchar");

                entity.Property(e => e.Photo).HasColumnType("blob");

                entity.Property(e => e.PhotoPath).HasColumnType("nvarchar");

                entity.Property(e => e.PostalCode).HasColumnType("nvarchar");

                entity.Property(e => e.Region).HasColumnType("nvarchar");

                entity.Property(e => e.ReportsTo).HasColumnType("int");

                entity.Property(e => e.Title).HasColumnType("nvarchar");

                entity.Property(e => e.TitleOfCourtesy).HasColumnType("nvarchar");

                entity.HasOne(d => d.ReportsToNavigation)
                    .WithMany(p => p.InverseReportsToNavigation)
                    .HasForeignKey(d => d.ReportsTo);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("sqlite_autoindex_Order Details_1");

                entity.ToTable("Order Details");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasColumnType("int");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasColumnType("int");

                entity.Property(e => e.Discount)
                    .IsRequired()
                    .HasColumnType("SINGLE")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Quantity)
                    .HasColumnType("smallint")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.UnitPrice)
                    .IsRequired()
                    .HasColumnType("decimal")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("sqlite_autoindex_Orders_1");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasColumnType("int");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasColumnType("char");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("EmployeeID")
                    .HasColumnType("int");

                entity.Property(e => e.Freight)
                    .HasColumnType("decimal")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.ShipAddress).HasColumnType("nvarchar");

                entity.Property(e => e.ShipCity).HasColumnType("nvarchar");

                entity.Property(e => e.ShipCountry).HasColumnType("nvarchar");

                entity.Property(e => e.ShipName).HasColumnType("nvarchar");

                entity.Property(e => e.ShipPostalCode).HasColumnType("nvarchar");

                entity.Property(e => e.ShipRegion).HasColumnType("nvarchar");

                entity.Property(e => e.ShipVia).HasColumnType("int");

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId);

                entity.HasOne(d => d.ShipViaNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipVia);
            });

            modelBuilder.Entity<ProductCategoryMap>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.ProductId })
                    .HasName("sqlite_autoindex_Product_Category_Map_1");

                entity.ToTable("Product_Category_Map");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasColumnType("int");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasColumnType("int");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCategoryMap)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCategoryMap)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("sqlite_autoindex_Products_1");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasColumnType("int");

                entity.Property(e => e.AttributeXml)
                    .HasColumnName("AttributeXML")
                    .HasColumnType("varchar");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasColumnType("int");

                entity.Property(e => e.CreatedBy).HasColumnType("nvarchar");

                entity.Property(e => e.CreatedOn)
                    .IsRequired()
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Discontinued)
                    .IsRequired()
                    .HasColumnType("bit")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ModifiedBy).HasColumnType("nvarchar");

                entity.Property(e => e.ModifiedOn)
                    .IsRequired()
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ProductGuid)
                    .HasColumnName("ProductGUID")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnType("nvarchar");

                entity.Property(e => e.QuantityPerUnit).HasColumnType("nvarchar");

                entity.Property(e => e.ReorderLevel)
                    .HasColumnType("smallint")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasColumnType("int");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UnitsInStock)
                    .HasColumnType("smallint")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UnitsOnOrder)
                    .HasColumnType("smallint")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.RegionId)
                    .HasColumnName("RegionID")
                    .HasColumnType("int");

                entity.Property(e => e.RegionDescription)
                    .IsRequired()
                    .HasColumnType("char");
            });

            modelBuilder.Entity<Shippers>(entity =>
            {
                entity.HasKey(e => e.ShipperId)
                    .HasName("sqlite_autoindex_Shippers_1");

                entity.Property(e => e.ShipperId)
                    .HasColumnName("ShipperID")
                    .HasColumnType("int");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnType("nvarchar");

                entity.Property(e => e.Phone).HasColumnType("nvarchar");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.HasKey(e => e.SupplierId)
                    .HasName("sqlite_autoindex_Suppliers_1");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasColumnType("int");

                entity.Property(e => e.Address).HasColumnType("nvarchar");

                entity.Property(e => e.City).HasColumnType("nvarchar");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnType("nvarchar");

                entity.Property(e => e.ContactName).HasColumnType("nvarchar");

                entity.Property(e => e.ContactTitle).HasColumnType("nvarchar");

                entity.Property(e => e.Country).HasColumnType("nvarchar");

                entity.Property(e => e.Fax).HasColumnType("nvarchar");

                entity.Property(e => e.HomePage).HasColumnType("nvarchar");

                entity.Property(e => e.Phone).HasColumnType("nvarchar");

                entity.Property(e => e.PostalCode).HasColumnType("nvarchar");

                entity.Property(e => e.Region).HasColumnType("nvarchar");
            });

            modelBuilder.Entity<Territories>(entity =>
            {
                entity.HasKey(e => e.TerritoryId)
                    .HasName("sqlite_autoindex_Territories_1");

                entity.Property(e => e.TerritoryId)
                    .HasColumnName("TerritoryID")
                    .HasColumnType("nvarchar");

                entity.Property(e => e.RegionId)
                    .HasColumnName("RegionID")
                    .HasColumnType("int");

                entity.Property(e => e.TerritoryDescription)
                    .IsRequired()
                    .HasColumnType("char");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Territories)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TextEntry>(entity =>
            {
                entity.HasKey(e => e.ContentId)
                    .HasName("sqlite_autoindex_TextEntry_1");

                entity.Property(e => e.ContentId)
                    .HasColumnName("contentID")
                    .HasColumnType("int");

                entity.Property(e => e.CallOut)
                    .HasColumnName("callOut")
                    .HasColumnType("nvarchar");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("nvarchar");

                entity.Property(e => e.ContentGuid)
                    .IsRequired()
                    .HasColumnName("contentGUID")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.ContentName)
                    .IsRequired()
                    .HasColumnName("contentName")
                    .HasColumnType("nvarchar");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasColumnType("nvarchar");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DateExpires)
                    .HasColumnName("dateExpires")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExternalLink)
                    .HasColumnName("externalLink")
                    .HasColumnType("nvarchar");

                entity.Property(e => e.IconPath)
                    .HasColumnName("iconPath")
                    .HasColumnType("nvarchar");

                entity.Property(e => e.LastEditedBy)
                    .HasColumnName("lastEditedBy")
                    .HasColumnType("nvarchar");

                entity.Property(e => e.ListOrder)
                    .HasColumnName("listOrder")
                    .HasColumnType("int")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modifiedBy")
                    .HasColumnType("nvarchar");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("nvarchar");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("nvarchar");
            });
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
        public virtual DbSet<CustomerDemographics> CustomerDemographics { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<EmployeeTerritories> EmployeeTerritories { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<ProductCategoryMap> ProductCategoryMap { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Shippers> Shippers { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Territories> Territories { get; set; }
        public virtual DbSet<TextEntry> TextEntry { get; set; }
    }
}