using Caelum.Stella.CSharp.Validation.Error;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.DTO.InputDTO;
using Products.DTO.ViewDTO;
using Products.Models.UpdateModels;
using Products.Services.Buy;
using Products.Services.Client;
using Products.Services.Product;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Controllers {
    [Route("api/purchases")]
    [ApiController]
    public class PurchasesController : ControllerBase {
        private readonly IPurchasesService _purchasesService;
        private readonly IClientService _clientService;
        private readonly IProductService _productService;
        private readonly IValidator<PurchasesInput> _purchasesValidationInput;
        private readonly IValidator<PurchasesUpdate> _purchasesValidationUpdate;
        public PurchasesController(IPurchasesService purchasesService,IClientService clientService,IProductService productService,IValidator<PurchasesInput> purchasesValidationInput,IValidator<PurchasesUpdate> purchasesValidationUpdate) {
            _purchasesService = purchasesService;
            _clientService = clientService;
            _productService = productService;
            _purchasesValidationInput = purchasesValidationInput;
            _purchasesValidationUpdate = purchasesValidationUpdate;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all Purchases.", Description = "Returns a list of all registered Purchases in the system.", Tags = ["Purchases"])]
        [ProducesResponseType(typeof(List<PurchasesView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult GetAllPurchases() {
            if (_purchasesService.GetAllPurchases().Count == 0) {
                return NotFound();
            }
            return Ok(_purchasesService.GetAllPurchases());
        }

        [HttpGet("id/{purchasesId}")]
        [SwaggerOperation(Summary = "Retrieves a purchase by ID.", Description = "Returns the details of a registered purchase based on the provided purchase ID.", Tags = ["Purchases"])]
        [ProducesResponseType(typeof(PurchasesView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult GetPurchasesById(int purchasesId) {
            try {
                var purchases = _purchasesService.GetPurchasesById(purchasesId);
                return Ok(purchases);
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new purchase.", Description = "Creates a new purchase with the provided data and returns the list of purchases details.", Tags = ["Purchases"])]
        [ProducesResponseType(typeof(List<PurchasesView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult CreatePurchases([FromBody] PurchasesInput purchasesInput) {
            if (!ModelState.IsValid) {
                return BadRequest(new {
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var validationResult = _purchasesValidationInput.Validate(purchasesInput);
            if (!validationResult.IsValid) {
                return BadRequest(new {
                    Message = "Validation failed",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            try {
                // Tratar para dar badresquest apos trativa inicial
                var clientExists = _clientService.GetClientById(purchasesInput.ClientId);
                var productExists = _productService.GetProductById(purchasesInput.ProductId);

                var purchases = _purchasesService.CreatePurchases(purchasesInput);
                return Ok(purchases);
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (InvalidStateException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{purchasesId}")]
        [SwaggerOperation(Summary = "Updates a purchase by ID.", Description = "Updates the details of a registered purchase based on the provided purchase ID and returns the updated purchase details.", Tags = ["Purchases"])]
        [ProducesResponseType(typeof(PurchasesView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult AlterPurchases(int purchasesId, [FromBody] PurchasesUpdate purchasesUpdate) {
            if (!ModelState.IsValid) {
                return BadRequest(new {
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            var validationResult = _purchasesValidationUpdate.Validate(purchasesUpdate);
            if (!validationResult.IsValid) {
                return BadRequest(new {
                    Message = "Validation failed",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }
            try {
                var purchase = _purchasesService.AlterPurchases(purchasesId, purchasesUpdate);
                return Ok(purchase);
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (InvalidStateException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("cnpj/{cnpj}")]
        [SwaggerOperation(Summary = "Retrieves a purchase by cnpj client.", Description = "Returns the details of a registered purchase based on the provided cnpj client.", Tags = ["Purchases"])]
        [ProducesResponseType(typeof(List<PurchasesView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult GetAllPurchasesByCnpjClient(string cnpj) {
            try {
                var purchases = _purchasesService.GetAllPurchasesByCnpjClient(cnpj);
                return Ok(purchases);
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("name/{name}")]
        [SwaggerOperation(Summary = "Retrieves a purchase by name client.", Description = "Returns the details of a registered purchase based on the provided name client.", Tags = ["Purchases"])]
        [ProducesResponseType(typeof(List<PurchasesView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult GetAllPurchasesByNameClient(string name) {
            try {
                var purchases = _purchasesService.GetAllPurchasesByNameClient(name);
                return Ok(purchases);
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
        }
    }
}
