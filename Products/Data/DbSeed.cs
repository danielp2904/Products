using Microsoft.EntityFrameworkCore;
using Products.Entities;

namespace Products.Data {
    public class DbSeed {

        public void SeedClient(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Client>().HasData(
                new Client { CNPJ = "12345678901234", Name = "Cliente A" },
                new Client { CNPJ = "12345678901234", Name = "Cliente B" }
                );
        }

        public  void SeedProduct(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product>().HasData(
                    new Product { Description = "Produto A", Value = 100.00m },
                    new Product { Description = "Produto B", Value = 200.00m }
                );
        }

        public  void SeedPurchases(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product>().HasData(
                new Purchases { Amount = 150.00m, ClientId = 1, ProductId = 1 },
                new Purchases { Amount = 250.00m, ClientId = 2, ProductId = 2 }
            );
        }

        public  void SeedPaymentTerms(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product>().HasData(
                    new PaymentTerms { Description = "À vista", Days = 0 },
                    new PaymentTerms { Description = "30 dias", Days = 30 },
                    new PaymentTerms { Description = "60 dias", Days = 60 },
                    new PaymentTerms { Description = "90 dias", Days = 90 }
                );
        }
    }
}
