using Caelum.Stella.CSharp.Validation;
using Caelum.Stella.CSharp.Validation.Error;
using Microsoft.AspNetCore.Mvc;
using Products.DTO.InputDTO;
using Products.DTO.ViewDTO;
using Products.Models.UpdateModels;
using Products.Services.Client;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Controllers {
    [Route("v1/client")]
    [ApiController]
    public class ClientController : ControllerBase {
        public readonly IClientService _clientService;
        public ClientController(IClientService clientService) {
            _clientService = clientService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all clients.", Description = "Returns a list of all registered clients in the system.", Tags = ["Clients"])]
        [ProducesResponseType(typeof(List<ClientView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult GetAllClient() {
            if (_clientService.GetAllClient().Count == 0) {
                return NotFound();
            }
            return Ok(_clientService.GetAllClient());
        }

        [HttpGet("{clientId}")]
        [SwaggerOperation(Summary = "Retrieves a client by ID.", Description = "Returns the details of a registered client based on the provided client ID.", Tags = ["Clients"])]
        [ProducesResponseType(typeof(ClientView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult GetClientByClientId(int clientId) {
            try {
                var client = _clientService.GetClientById(clientId);
                return Ok(client);
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new client.", Description = "Creates a new client with the provided data and returns the list clients details.", Tags = ["Clients"])]
        [ProducesResponseType(typeof(List<ClientView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult CreateClient([FromBody] ClientInput clientInput) {
            if (!ModelState.IsValid) {
                return BadRequest(new {
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            bool cnpjIsValid = new CNPJValidator().IsValid(clientInput.CPNJ);
            if (!cnpjIsValid) {
                return BadRequest(new {
                    Message = "Validation failed",
                    Errors = new[] { "Invalid CNPJ" }
                });
            }

            try {
                var client = _clientService.CreateClient(clientInput);
                return Ok(client);
            }
            catch (InvalidStateException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{clientId}")]
        [SwaggerOperation(Summary = "Updates an existing client.", Description = "Updates the client identified by the provided ID with the given data and returns the updated client details.", Tags = ["Clients"])]
        [ProducesResponseType(typeof(ClientView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult AlterClient([FromBody] ClientUpdate clientUpdate, int clientId) {
            if (!ModelState.IsValid) {
                return BadRequest(new {
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try {
                var client = _clientService.AlterClient(clientUpdate, clientId);
                return Ok(client);
            }
            catch (InvalidStateException ex) {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{clientId}")]
        [SwaggerOperation(Summary = "Delete a client", Description = "Delete the client identified by the provided ID and returns the updated client details.", Tags = ["Clients"])]
        [ProducesResponseType(typeof(ClientView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult DeleteProduct(int clientId) {
            try {
                var client = _clientService.DeleteClient(clientId);
                return Ok(client);
            }
            catch (InvalidStateException ex) {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
        }
    }
}
