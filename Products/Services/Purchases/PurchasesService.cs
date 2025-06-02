using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.DTO.InputDTO;
using Products.DTO.ViewDTO;
using Products.Entities;
using Products.Models.UpdateModels;

namespace Products.Services.Buy {
    public class PurchasesService : IPurchasesService {
        private readonly DataContext _dataContext;
        public PurchasesService(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public PurchasesView AlterPurchases(int purchasesId, PurchasesUpdate purchasesUpdate) {
            var purchases = _dataContext.Purchases.FirstOrDefault(e => e.PurchasesId == purchasesId);
            if (purchases == null) {
                throw new KeyNotFoundException($"Purchases with ID {purchasesId} not found.");
            }
            purchases.Amount = purchasesUpdate.Amount;
            _dataContext.Update(purchases);
            _dataContext.SaveChanges();
            return PurchasesView.FromEntity(purchases);
        }

        public List<PurchasesView> CreatePurchases(PurchasesInput purchasesInput) {
            var purchase = new Entities.Purchases {
                ProductId = purchasesInput.ProductId,
                ClientId = purchasesInput.ClientId,
                Amount = purchasesInput.Amount,
            };
            _dataContext.Purchases.Add(purchase);
            _dataContext.SaveChanges();
            var purchases = _dataContext.Purchases.ToList();
            return PurchasesView.FromEntity(purchases);
        }

        public List<PurchasesView> GetAllPurchases() {
            var purchases = _dataContext.Purchases.Include(e => e.Client)
                .Include(e => e.Product)
                .Include(e => e.PaymentTerms)
                .ToList();
            return PurchasesView.FromEntity(purchases);
        }

        public PurchasesView GetPurchasesById(int purchasesId) {
            var purchases = _dataContext.Purchases.Include(e => e.Client)
                .Include(e => e.Product)
                .Include(e => e.PaymentTerms)
                .FirstOrDefault(e => e.PurchasesId == purchasesId);
            if (purchases == null) {
                throw new KeyNotFoundException($"Purchases with ID {purchasesId} not found.");
            }
            return PurchasesView.FromEntity(purchases);
        }

        public List<PurchasesView> GetAllPurchasesByCnpjClient(string cnpj) { 
            var purchases = _dataContext.Purchases.Include(e => e.Client)
                .Include(e => e.Product)
                .Include(e => e.PaymentTerms)
                .Where(e => e.Client.CNPJ == cnpj)
                .ToList();
            if(purchases == null) {
                throw new KeyNotFoundException($"No purchases found for client with CNPJ {cnpj}.");
            }
            return PurchasesView.FromEntity(purchases);
        }

        public List<PurchasesView> GetAllPurchasesByNameClient(string nameClient) {
            var purchases = _dataContext.Purchases.Include(e => e.Client)
                .Include(e => e.Product)
                .Include(e => e.PaymentTerms)
                .Where(e => e.Client.Name == nameClient)
                .ToList();
            if (purchases == null) {
                throw new KeyNotFoundException($"No purchases found for client with name {nameClient}.");
            }
            return PurchasesView.FromEntity(purchases);
        }
    }
}
