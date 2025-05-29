namespace Products.Entities {
    public class Product {
        public int ProductId { get; set; }
        public string Description { get; set; }       
        public decimal Value { get; set; }
        public Boolean IsDelete { get; set; }     
        
        public ICollection<Purchases> Purchases { get; set; }
    }
}
