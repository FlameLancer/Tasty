using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Tasty.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<OpeningTime> OpeningTimes { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopAddress> ShopAddresses { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var converter = new ValueConverter<TimeSpan, long>(
                v => v.Ticks,
                v => new TimeSpan(v));

            modelBuilder.Entity<Shop>()
                .HasOne(s => s.ShopAddress)
                .WithOne(a => a.Shop)
                .HasForeignKey<ShopAddress>(a => a.ShopId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Shop>()
                .HasMany(s => s.OpeningTimes)
                .WithOne(o => o.Shop)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Shop>()
                .HasMany(s => s.Orders)
                .WithOne(o => o.Shop)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OpeningTime>()
                .HasKey(o => new { o.ShopId, o.DayOfWeek });

            modelBuilder.Entity<OpeningTime>()
                .Property(s => s.Closing)
                    .HasConversion(converter);

            modelBuilder.Entity<Shop>()
                .HasMany(s => s.MenuCategories)
                .WithOne(m => m.Shop)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuCategory>()
                .HasMany(m => m.Items)
                .WithOne(i => i.MenuCategory)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
