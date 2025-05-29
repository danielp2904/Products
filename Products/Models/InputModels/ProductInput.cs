using Microsoft.EntityFrameworkCore;
using Products.Models.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Products.DTO.InputDTO {
    public class ProductInput : IProductValue {
        [Required(ErrorMessage = "The Description field is required.")]
        [StringLength(255, ErrorMessage = "The Description field must be at most 150 characters long.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Value field is required.")]
        public decimal Value { get; set; }
    }
}
