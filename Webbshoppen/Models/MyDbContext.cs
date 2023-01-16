using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Webbshoppen.Models
{
    internal class MyDbContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:mohammad2.database.windows.net,1433;Initial Catalog=Webbshopp;Persist Security Info=False;User ID=mohammad;Password=alzuabi1#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<Payment> Payments { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries  { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(p =>  p.Email)
                .IsUnique(true);
        }
    }
}
