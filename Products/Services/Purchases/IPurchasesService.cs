using Products.DTO.InputDTO;
using Products.DTO.ViewDTO;
using Products.Models.UpdateModels;

namespace Products.Services.Buy {
    public interface IPurchasesService {
        List<PurchasesView> GetAllPurchases();
        PurchasesView GetPurchasesById(int purchasesId);
        List<PurchasesView> CreatePurchases(PurchasesInput purchasesId);
        PurchasesView AlterPurchases(int purchasesId, PurchasesUpdate purchasesInput);
        List<PurchasesView> GetAllPurchasesByCnpjClient(string cnpjClient);
        List<PurchasesView> GetAllPurchasesByNameClient(string nameClient);
    }
}
