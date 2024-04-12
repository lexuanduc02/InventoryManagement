using InventoryManagement.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Models.MerchandiseModels;
using InventoryManagement.Models.CustomerModels;
using InventoryManagement.Models.SaleInvoiceModels;

namespace InventoryManagement.Domains.EF
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasKey(c => c.Id);
            builder.Entity<Customer>().HasKey(x => x.Id);
            builder.Entity<Merchandise>().HasKey(x => x.Id);
            builder.Entity<MerchandisePurchaseInvoice>().HasKey(x => new { x.PurchaseInvoiceId, x.MerchandiseId });
            builder.Entity<MerchandiseSaleInvoice>().HasKey(x => new { x.SaleInvoiceId, x.MerchandiseId });
            builder.Entity<Partner>().HasKey(x => x.Id);
            builder.Entity<PurchaseInvoice>().HasKey(x => x.Id);
            builder.Entity<Role>().HasKey(x => x.Id);
            builder.Entity<SaleInvoice>().HasKey(x => x.Id);
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<Warehouse>().HasKey(x => x.Id);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Merchandise> Merchandises { get; set; }
        public DbSet<MerchandisePurchaseInvoice> MerchandisesPurchase { get; set; }
        public DbSet<MerchandiseSaleInvoice> MerchandisesSale { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SaleInvoice> SaleInvoices { get; set;}
        public DbSet<User> Users { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<MerchandiseSaleInvoice> MerchandiseSaleInvoices { get; set; }
        public DbSet<MerchandisePurchaseInvoice> MerchandisePurchaseInvoices { get; set; }
    }
}
