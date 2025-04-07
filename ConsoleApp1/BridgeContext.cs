using ConsoleApp1.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class BridgeContext(DbContextOptions<BridgeContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CashPayment>()
                .ToTable("CashPayment")
                .HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);

            modelBuilder.Entity<InterCompanyPayment>()
                .ToTable("InterCompanyPayment")
                .HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        }

        public DbSet<CashPayment> CashPayments => Set<CashPayment>();
        public DbSet<InterCompanyPayment> InterCompanyPayments => Set<InterCompanyPayment>();
        public DbSet<BusinessEntityConfig> BusinessEntityConfigs => Set<BusinessEntityConfig>();
    }

    public class AmpContext(string connectionString) : DbContext
    {
        private readonly string _connectionString = connectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        public DbSet<CashPayment> CashPayments => Set<CashPayment>();
        public DbSet<InterCompanyPayment> InterCompanyPayments => Set<InterCompanyPayment>();
    }
}
