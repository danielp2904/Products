namespace Products.Entities {
    public class PaymentTerms {
        public int PaymentTermsId { get; set; }
        public int Days { get; set; }
        public string Description { get; set; }        
        
        public int PurchasesId { get; set; }
        public Purchases Purchases { get; set; }
    }
}
