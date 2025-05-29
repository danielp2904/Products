using Products.DTO.InputDTO;
using Products.DTO.ViewDTO;
using Products.Models.UpdateModels;

namespace Products.Services.Client {
    public interface IClientService {
        List<ClientView> GetAllClient();
        ClientView GetClientById(int ClientId);
        List<ClientView> CreateClient(ClientInput clientInput);
        ClientView AlterClient(ClientUpdate clientUpdate, int ClientId);
        List<ClientView> DeleteClient(int ClientId);
    }
}
