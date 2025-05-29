using Caelum.Stella.CSharp.Validation.Error;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.DTO.InputDTO;
using Products.DTO.ViewDTO;
using Products.Models.UpdateModels;
using Products.Services.Product;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Controllers {
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase {
        public readonly IProductService _productService;
        private readonly IValidator<ProductInput> _validatorProductInput;
        private readonly IValidator<ProductUpdate> _validatorProductUpdate;
        public ProductController(IProductService productService, IValidator<ProductInput> validatorProductInput, IValidator<ProductUpdate> validatorProductUpdate) {
            _productService = productService;
            _validatorProductInput = validatorProductInput;
            _validatorProductUpdate = validatorProductUpdate;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all products.", Description = "Returns a list of all registered products in the system.", Tags = ["Products"])]
        [ProducesResponseType(typeof(List<ProductView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult GetAllProducts() {
            if (_productService.GetAllProducts().Count == 0) {
                return NotFound();
            }
            return Ok(_productService.GetAllProducts());
        }

        [HttpGet("{productId}")]
        [SwaggerOperation(Summary = "Retrieves a product by ID.", Description = "Returns the details of a registered product based on the provided product ID.", Tags = ["Products"])]
        [ProducesResponseType(typeof(ProductView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult GetProductById(int productId) {
            try {
                var product = _productService.GetProductById(productId);
                return Ok(product);
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new product.", Description = "Creates a new product with the provided data and returns the list products details.", Tags = ["Products"])]
        [ProducesResponseType(typeof(List<ProductView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult CreateProduct([FromBody] ProductInput productInput) {
            if (!ModelState.IsValid) {
                return BadRequest(new {
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var validationResult = _validatorProductInput.Validate(productInput);

            if (!validationResult.IsValid) {
                return BadRequest(new {
                    Message = "Validation failed",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            try {
                var product = _productService.CreateProduct(productInput);
                return Ok(product);
            }
            catch (InvalidStateException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{productId}")]
        [SwaggerOperation(Summary = "Updates an existing product.", Description = "Updates the product identified by the provided ID with the given data and returns the updated product details.", Tags = ["Products"])]
        [ProducesResponseType(typeof(ProductView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult AlterProduct([FromBody] ProductUpdate productUpdate, int productId) {
            if (!ModelState.IsValid) {
                return BadRequest(new {
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var validationResult = _validatorProductUpdate.Validate(productUpdate);

            if (!validationResult.IsValid) {
                return BadRequest(new {
                    Message = "Validation failed",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            try {
                var client = _productService.AlterProduct(productId, productUpdate);
                return Ok(client);
            }
            catch (InvalidStateException ex) {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{productId}")]
        [SwaggerOperation(Summary = "Delete a product", Description = "Delete the product identified by the provided ID and returns the updated product details.", Tags = ["Products"])]
        [ProducesResponseType(typeof(ProductView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult DeleteProduct(int productId) {
            try {
                var product = _productService.DeleteProduct(productId);
                return Ok(product);
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (InvalidStateException ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
