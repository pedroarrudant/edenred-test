using EdenredTest.Application.Features.DeleteVehicle.Models;
using EdenredTest.Application.Features.GetAllVehicles.Models;
using EdenredTest.Application.Features.GetVehicleById.Models;
using EdenredTest.Application.Features.InsertVehicle.Models;
using EdenredTest.Application.Features.UpdateVehicle.Models;
using EdenredTest.Application.Shared.Domain;
using EdenredTest.Controllers.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EdenredTest.Controllers
{
    [ApiController]
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMediator _mediator;

        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public Task<Vehicle?> GetVehicleById([FromRoute] long id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new GetVehicleByIdInput { Id = id }, cancellationToken);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public Task<IEnumerable<Vehicle>> GetAllVehicles(CancellationToken cancellationToken)
        {
            return _mediator.Send(new GetAllVehiclesInput(), cancellationToken);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> InsertVehicle([FromBody] InsertVehicleRequest request, CancellationToken cancellationToken)
        {
            var vehicle = new Vehicle
            {
                LicensePlate = request.LicensePlate,
                ManufactureYear = request.ManufactureYear,
                Brand = request.Brand,
                Model = request.Model
            };

            var inserted = await _mediator.Send(new InsertVehicleInput { Vehicle = vehicle }, cancellationToken);

            if (!inserted.Success)
            {
                return BadRequest(inserted.Message);
            }

            return NoContent();
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateVehicle([FromRoute] long id, [FromBody] InsertVehicleRequest request, CancellationToken cancellationToken)
        {
            var vehicle = new Vehicle
            {
                Id = id,
                LicensePlate = request.LicensePlate,
                ManufactureYear = request.ManufactureYear,
                Brand = request.Brand,
                Model = request.Model
            };

            var updated = await _mediator.Send(new UpdateVehicleInput { Vehicle = vehicle }, cancellationToken);

            if (!updated.Success)
            {
                if (!string.IsNullOrEmpty(updated.Message))
                {
                    return BadRequest(updated.Message);
                }

                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteVehicle([FromRoute] long id, CancellationToken cancellationToken)
        {
            var deleted = await _mediator.Send(new DeleteVehicleInput { Id = id }, cancellationToken);

            return deleted ? NoContent() : NotFound();
        }
    }
}
