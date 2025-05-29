using Caelum.Stella.CSharp.Validation.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.DTO.InputDTO;
using Products.DTO.ViewDTO;
using Products.Entities;
using Products.Models.UpdateModels;
using Products.Services.PaymentTerms;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Controllers {
    [Route("api/paymentTerms")]
    [ApiController]
    public class PaymentTermsController : ControllerBase {
        private readonly IPaymentTermsService _paymentTermsService;
        public PaymentTermsController(IPaymentTermsService paymentTermsService) {
            _paymentTermsService = paymentTermsService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all payment terms.", Description = "Returns a list of all registered payment terms in the system.", Tags = ["PaymentTerms"])]
        [ProducesResponseType(typeof(List<PaymentTermsView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult GetAllPaymentTerms() {
            if (_paymentTermsService.GetAllPaymentTerms().Count == 0) {
                return NotFound();
            }
            return Ok(_paymentTermsService.GetAllPaymentTerms());
        }

        [HttpGet("{paymentTermsId}")]
        [SwaggerOperation(Summary = "Retrieves a payment term by ID.", Description = "Returns the details of a registered payment term based on the provided payment term ID.", Tags = ["PaymentTerms"])]
        [ProducesResponseType(typeof(PaymentTermsView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult GetPaymentTermsbById(int paymentTermsId) {
            try {
                var paymentTerms = _paymentTermsService.GetPaymentTermsById(paymentTermsId);
                return Ok(paymentTerms);
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new payment term.", Description = "Creates a new payment term with the provided data and returns the list of payment terms details.", Tags = ["PaymentTerms"])]
        [ProducesResponseType(typeof(List<PaymentTermsView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult CreatePaymentTerms([FromBody] PaymentTermsInput paymentTermsInput) {
            if (!ModelState.IsValid) {
                return BadRequest(new {
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            try {
                var paymentTerms = _paymentTermsService.CreatePaymentTerms(paymentTermsInput);
                return Ok(paymentTerms);
            }
            catch (InvalidStateException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{paymentTermsId}")]
        [SwaggerOperation(Summary = "Updates an existing payment term.", Description = "Updates the details of a registered payment term based on the provided payment term ID and returns the updated payment term details.", Tags = ["PaymentTerms"])]
        [ProducesResponseType(typeof(PaymentTermsView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult AlterPaymentTerms(int paymentTermsId, [FromBody] PaymentTermsUpdate paymentTermsUpdate) {
            if (!ModelState.IsValid) {
                return BadRequest(new {
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            try {
                var updatedPaymentTerms = _paymentTermsService.AlterPaymentTerms(paymentTermsId, paymentTermsUpdate);
                return Ok(updatedPaymentTerms);
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
