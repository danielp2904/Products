using Products.Entities;

namespace Products.DTO.ViewDTO {
    public class PurchasesView {
        public PurchasesView(int purchasesId, decimal amount, ClientView client, ProductView product, PaymentTermsView paymentTerms) {
            PurchasesId = purchasesId;
            Amount = amount;
            Client = client;
            Product = product;
            PaymentTerms = paymentTerms;
        }
        public int PurchasesId { get; set; }
        public decimal Amount { get; set; }
        public ClientView Client { get; set; }
        public ProductView Product { get; set; }
        public PaymentTermsView PaymentTerms { get; set; }

        public static PurchasesView FromEntity(Purchases purchasesEntity)  {
            return new PurchasesView(
                purchasesEntity.PurchasesId,
                purchasesEntity.Amount,
                ClientView.FromEntity(purchasesEntity.Client),
                ProductView.FromEntity(purchasesEntity.Product),
                PaymentTermsView.FromEntity( purchasesEntity.PaymentTerms)
            );
        }

        public static List<PurchasesView> FromEntity(List<Purchases> purchasesEntities) {
            return purchasesEntities.Select(c => FromEntity(c)).ToList();
        }
    }
}
