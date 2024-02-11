using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using ShoppingStore.Models;


namespace ShoppingStore.Data
{
    public class ShoppingStoreDBContext: IdentityDbContext
    {
        public ShoppingStoreDBContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<Items> Items { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrdersItems> OrdersItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
            });

            modelBuilder.Entity<Orders>().HasMany(order => order.Items)
                .WithMany(item => item.Orders)
                .UsingEntity<OrdersItems>();

        }
    }
}
