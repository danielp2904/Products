namespace Products.Entities {
    public class Purchases {
        public int PurchasesId { get; set; }
        public decimal Amount { get; set; }
     
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public PaymentTerms PaymentTerms { get; set; }
    }
}
