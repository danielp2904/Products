using Microsoft.EntityFrameworkCore;
using Products.Models.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Products.DTO.InputDTO {
    public class PurchasesInput : IPurchasesAmount {
        [Required(ErrorMessage = "The ClientId field is required.")]
        public int ClientId { get; set; }        
        [Required(ErrorMessage = "The ProductId field is required.")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "The Amount field is required.")]
        public decimal Amount { get; set; }
    }
}
