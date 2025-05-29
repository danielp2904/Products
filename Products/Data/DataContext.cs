using Microsoft.EntityFrameworkCore;
using Products.Entities;

namespace Products.Data {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Purchases> Purchases { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<PaymentTerms> PaymentTerms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            OnModelCreatingClient(modelBuilder);
            OnModelCreatingPurchases(modelBuilder);
            OnModelCreatingProduct(modelBuilder);
            OnModelCreatingPaymentTerms(modelBuilder);
        }

        private static void OnModelCreatingClient(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Client>()
                .Property(e => e.CNPJ)
                .HasMaxLength(14)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property(e => e.Name)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property(e => e.RegistrationDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property(e => e.IsDelete)
                .HasDefaultValue(false)
                .IsRequired();

            modelBuilder.Entity<Client>().HasData(
                new Client { ClientId = 1, CNPJ = "12345678901234", Name = "Cliente A", IsDelete = false, RegistrationDate = new DateTime(2024, 01, 01) },
                new Client { ClientId = 2, CNPJ = "12345678901234", Name = "Cliente B", IsDelete = false, RegistrationDate = new DateTime(2024, 01, 01) }
                );
        }

        private static void OnModelCreatingPurchases(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Purchases>()
                .Property(e => e.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            modelBuilder.Entity<Purchases>()
                .HasOne(e => e.Client)
                .WithMany(e => e.Purchases)
                .HasForeignKey(e => e.ClientId)
                .IsRequired();

            modelBuilder.Entity<Purchases>()
                .HasOne(e => e.Product)
                .WithMany(e => e.Purchases)
                .HasForeignKey(e => e.ClientId)
                .IsRequired();

            modelBuilder.Entity<Purchases>().HasData(
                new Purchases { PurchasesId = 1, Amount = 150.00m, ClientId = 1, ProductId = 1 },
                new Purchases { PurchasesId = 2, Amount = 250.00m, ClientId = 2, ProductId = 2 }
            );
        }

        private static void OnModelCreatingProduct(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(e => e.Value)
                .HasPrecision(18, 2)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(e => e.IsDelete)
                .HasDefaultValue(false)
                .IsRequired();

            modelBuilder.Entity<Product>().HasData(
                    new Product { ProductId = 1, Description = "Produto A", Value = 100.00m, IsDelete = false },
                    new Product { ProductId = 2, Description = "Produto B", Value = 200.00m, IsDelete = false }
                );
        }

        private static void OnModelCreatingPaymentTerms(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PaymentTerms>()
                .Property(e => e.Description)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<PaymentTerms>()
                .Property(e => e.Days)
                .IsRequired();

            modelBuilder.Entity<PaymentTerms>()
                .HasOne(e => e.Purchases)
                .WithOne(e => e.PaymentTerms)
                .HasForeignKey<Purchases>(e => e.PurchasesId)
                .IsRequired();

            modelBuilder.Entity<PaymentTerms>().HasData(
                    new PaymentTerms { PaymentTermsId = 1, Description = "À vista", Days = 0, PurchasesId = 1 },
                    new PaymentTerms { PaymentTermsId = 2, Description = "30 dias", Days = 30, PurchasesId = 2 }
                );
        }
    }

}
