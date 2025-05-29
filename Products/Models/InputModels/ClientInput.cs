using System.ComponentModel.DataAnnotations;

namespace Products.DTO.InputDTO {
    public class ClientInput {
        [Required(ErrorMessage = "The CPNJ field is required.")]
        [StringLength(14, ErrorMessage = "The CPNJ field must be at most 14 characters long.")]
        public string CPNJ { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(150, ErrorMessage = "The Name field must be at most 150 characters long.")]
        public string Name { get; set; }
    }
}
