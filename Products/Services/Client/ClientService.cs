using Caelum.Stella.CSharp.Validation;
using Products.Data;
using Products.DTO.InputDTO;
using Products.DTO.ViewDTO;
using Products.Models.UpdateModels;

namespace Products.Services.Client {
    public class ClientService : IClientService {
        public readonly DataContext _dataContext;
        public ClientService(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public List<ClientView> GetAllClient() {
            var client = _dataContext.Client.Where(e => e.IsDelete == false).ToList();
            return ClientView.FromEntity(client);
        }

        public ClientView GetClientById(int clientId) {
            var client = _dataContext.Client.FirstOrDefault(e => e.ClientId == clientId && e.IsDelete == false);
            if (client == null) {
                throw new KeyNotFoundException($"Client with ID {clientId} not found.");
            }
            return ClientView.FromEntity(client);
        }

        public List<ClientView> CreateClient(ClientInput DTOClient) {            
            var newClient = new Entities.Client() {
                CNPJ = DTOClient.CPNJ,
                Name = DTOClient.Name
            };
            _dataContext.Add(newClient);
            _dataContext.SaveChanges();
            var client = _dataContext.Client.ToList();
            return ClientView.FromEntity(client);
        }

        public ClientView AlterClient(ClientUpdate clientUpdate, int clientId) {
            var client = _dataContext.Client.FirstOrDefault(e => e.ClientId == clientId && e.IsDelete == false);
            if (client == null) {
                throw new KeyNotFoundException($"Client with ID {clientId} not found.");
            }
            client.Name = clientUpdate.Name;
            _dataContext.Update(client);
            _dataContext.SaveChanges();
            return ClientView.FromEntity(client);
        }

        public List<ClientView> DeleteClient(int clientId) {
            var client = _dataContext.Client.FirstOrDefault(e => e.ClientId == clientId && e.IsDelete == false);
            
            if (client == null) {
                throw new KeyNotFoundException($"Client with ID {clientId} not found.");
            }

            client.IsDelete = true;
            _dataContext.Update(client);
            _dataContext.SaveChanges();
            var clients = _dataContext.Client.Where(e => e.IsDelete == false).ToList();
            return ClientView.FromEntity(clients);
        }
    }
}
