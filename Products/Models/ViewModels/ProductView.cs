using Products.Entities;

namespace Products.DTO.ViewDTO {
    public class ProductView {
        public ProductView(int productId, string description, decimal value, bool isDelete) {
            ProductId = productId;
            Description = description;
            Value = value;
            IsDelete = isDelete;
        }

        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public bool IsDelete { get; set; }

        public static ProductView FromEntity(Product productEntity) {
            return new ProductView(
                productEntity.ProductId,
                productEntity.Description,
                productEntity.Value,
                productEntity.IsDelete
            );
        }

        public static List<ProductView> FromEntity(List<Product> productEntities) {
            return productEntities.Select(p => FromEntity(p)).ToList();
        }
    }
}
