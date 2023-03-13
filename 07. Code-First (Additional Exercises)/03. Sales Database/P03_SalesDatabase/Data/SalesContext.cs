using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;
using System.Data.Common;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        private const string ConnectionString = @"Server=.; Database=MinionsDB; Integrated Security=true; TrustServerCertificate=True";

        public SalesContext()
        { }

        public SalesContext(DbContextOptions options)
            :base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.Email).IsUnicode(false);
            });
        }
    }
}
