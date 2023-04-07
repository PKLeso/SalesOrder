using Microsoft.EntityFrameworkCore;
using SalesOrder.Models;

namespace SalesOrder.Data
{
    public class SalesOrderDbContext: DbContext
    {
        public SalesOrderDbContext(DbContextOptions<SalesOrderDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesOrderLines>().HasKey(o => new
            {
                o.SalesOrderId
            });

            modelBuilder.Entity<SalesOrderLines>().HasOne(m => m.SalesOrder).WithMany()
                .HasForeignKey(m => m.SalesOrderId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Entry> Entries { get; set; }

        public DbSet<SalesOrders> SalesOrders { get; set; }
        public DbSet<SalesOrderLines> SalesOrderLines { get; set; }


    }
}
