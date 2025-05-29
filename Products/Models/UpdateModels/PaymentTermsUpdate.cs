using System.ComponentModel.DataAnnotations;

namespace Products.Models.UpdateModels {
    public class PaymentTermsUpdate {
        public int Days { get; set; }
        [StringLength(255, ErrorMessage = "The Description field must be at most 255 characters long.")]
        public string Description { get; set; }
    }
}
