namespace Products.Entities {
    public class Client {
        public int ClientId { get; set; }
        public string CNPJ { get; set; }
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsDelete { get; set; }
        public ICollection<Purchases> Purchases { get; set; }
    }
}
