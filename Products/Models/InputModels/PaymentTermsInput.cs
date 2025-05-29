using System.ComponentModel.DataAnnotations;

namespace Products.DTO.InputDTO {
    public class PaymentTermsInput {
        [Required(ErrorMessage = "The BuyId field is required.")]
        public int PurchasesId { get; set; }
        [Required(ErrorMessage = "The Days field is required.")]
        public int Days { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        [StringLength(255, ErrorMessage = "The Description field must be at most 255 characters long.")]
        public string Description { get; set; }
    }
}
