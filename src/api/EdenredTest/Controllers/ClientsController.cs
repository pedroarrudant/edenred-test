using EdenredTest.Application.Features.GetClientById.Models;
using EdenredTest.Application.Features.InsertClient.Models;
using EdenredTest.Application.Features.GetAllClients.Models;
using EdenredTest.Application.Features.DeleteClient.Models;
using EdenredTest.Application.Features.UpdateClient.Models;
using EdenredTest.Application.Shared.Models;
using EdenredTest.Controllers.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Net;

namespace EdenredTest.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientsController : Controller
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IMediator _mediator;

        public ClientsController(ILogger<ClientsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<Client> GetClientByName([FromRoute] long id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetClientByIdInput() { Id = id }, cancellationToken);

            return result?.client;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<Client>> GetAllClients(CancellationToken cancellationToken) //TODO: Needs pagination
        {
            return await _mediator.Send(new GetAllClientsInput(), cancellationToken);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> InsertClient([FromBody] InsertClientRequest request, CancellationToken cancellationToken)
        {
            var client = new Client() 
            {
                FullName = request.FullName,
                IdentificationDocument = request.IdentificationDocument,
                BirthDate = request.BirthDate,
                Email = request.Email,
                TelephoneNumber = request.TelephoneNumber
            };

            var result = await _mediator.Send(new InsertClientInput { Client = client }, cancellationToken);

            return result.Success ? NoContent() : BadRequest();
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateClient([FromRoute] long id, [FromBody] InsertClientRequest request, CancellationToken cancellationToken)
        {
            var client = new Client()
            {
                Id = id,
                FullName = request.FullName,
                IdentificationDocument = request.IdentificationDocument,
                BirthDate = request.BirthDate,
                Email = request.Email,
                TelephoneNumber = request.TelephoneNumber
            };

            var updated = await _mediator.Send(new UpdateClientInput { Client = client }, cancellationToken);

            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> DeleteClient([FromRoute] long id, CancellationToken cancellationToken)
        {
            var deleted = await _mediator.Send(new DeleteClientInput { Id = id }, cancellationToken);

            return deleted ? NoContent() : NotFound();
        }
    }
}
