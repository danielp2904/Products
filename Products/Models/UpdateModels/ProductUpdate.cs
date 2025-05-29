using Microsoft.EntityFrameworkCore;
using Products.Models.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Products.Models.UpdateModels {
    public class ProductUpdate : IProductValue {        
        [StringLength(255, ErrorMessage = "The Description field must be at most 150 characters long.")]
        public string Description { get; set; }       
        public decimal Value { get; set; }
    }
}
