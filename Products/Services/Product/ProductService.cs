using Products.Data;
using Products.DTO.InputDTO;
using Products.DTO.ViewDTO;
using Products.Entities;
using Products.Models.UpdateModels;

namespace Products.Services.Product {
    public class ProductService : IProductService {
        public readonly DataContext _dataContext;
        public ProductService(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public List<ProductView> CreateProduct(ProductInput productInput) {
            var productEntity = new Entities.Product {
                Description = productInput.Description,
                Value = productInput.Value,
                IsDelete = false,
            };

            _dataContext.Products.Add(productEntity);
            _dataContext.SaveChanges();
            var products = _dataContext.Products.Where(e => e.IsDelete == false).ToList();
            return ProductView.FromEntity(products);
        }

        public List<ProductView> GetAllProducts() {
            var products = _dataContext.Products.Where(e => e.IsDelete == false).ToList();
            return ProductView.FromEntity(products);
        }

        public ProductView GetProductById(int productId) {
            var product = _dataContext.Products
                .FirstOrDefault(e => e.ProductId == productId && e.IsDelete == false);
            if (product == null) {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }
            return ProductView.FromEntity(product);
        }

        public ProductView AlterProduct(int productId, ProductUpdate productUpdate) {
            var product = _dataContext.Products.FirstOrDefault(e => e.ProductId == productId && e.IsDelete == false);
            if (product == null) {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }
            product.Description = productUpdate.Description;
            product.Value = productUpdate.Value;
            _dataContext.Update(product);
            _dataContext.SaveChanges();
            return ProductView.FromEntity(product);
        }

        public List<ProductView> DeleteProduct(int productId) {
            var product = _dataContext.Products.FirstOrDefault(e => e.ProductId == productId && e.IsDelete == false);
            if (product == null) {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

            product.IsDelete = true;
            _dataContext.Update(product);
            _dataContext.SaveChanges();
            var products = _dataContext.Products.Where(e => e.IsDelete == false).ToList();
            return ProductView.FromEntity(products);
        }
    }
}
