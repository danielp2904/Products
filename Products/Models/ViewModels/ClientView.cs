using Products.Entities;

namespace Products.DTO.ViewDTO {
    public class ClientView {
        public ClientView(int clientId, string cnpj, string name, DateTime registrationDate, bool isDelete) {
            ClientId = clientId;
            CNPJ = cnpj;
            Name = name;
            RegistrationDate = registrationDate;
            IsDelete = isDelete;
        }
        public int ClientId { get; set; }
        public string CNPJ { get; set; }
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsDelete { get; set; }

        public static ClientView FromEntity(Client clientEntity) {
            return new ClientView(
                clientEntity.ClientId,
                clientEntity.CNPJ,
                clientEntity.Name,
                clientEntity.RegistrationDate,
                clientEntity.IsDelete                
            );
        }

        public static List<ClientView> FromEntity(List<Client> clientEntities) {
            return clientEntities.Select(c => FromEntity(c)).ToList();
        }
    }
}
