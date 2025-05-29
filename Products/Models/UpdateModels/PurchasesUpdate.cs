using Products.Models.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Products.Models.UpdateModels {
    public class PurchasesUpdate : IPurchasesAmount {
        [Required(ErrorMessage = "The Amount field is required.")]
        public decimal Amount { get; set; }
    }
}
