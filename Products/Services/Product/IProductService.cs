using Products.DTO.InputDTO;
using Products.DTO.ViewDTO;
using Products.Models.UpdateModels;

namespace Products.Services.Product {
    public interface IProductService {
        List<ProductView> GetAllProducts();
        ProductView GetProductById(int productId);
        List<ProductView> CreateProduct(ProductInput productInput);
        ProductView AlterProduct(int productId, ProductUpdate productUpdate);
        List<ProductView> DeleteProduct(int productId);
    }
}
