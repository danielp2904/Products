using System.ComponentModel.DataAnnotations;

namespace Products.Models.UpdateModels {
    public class ClientUpdate {
        [StringLength(150, ErrorMessage = "The Name field must be at most 150 characters long.")]
        public string Name { get; set; }
    }
}
