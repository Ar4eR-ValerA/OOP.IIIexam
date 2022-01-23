using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taksi.DTO.DTOs;
using Taksi.DTO.Models;
using Taksi.Server.BLL.Services.Interfaces;
using Taksi.Server.DAL.Entities;

namespace Taksi.Server.Controllers
{
    [ApiController]
    [Route("/rides")]
    public class RideController : ControllerBase
    {
        private readonly IRideService _service;

        public RideController(IRideService service)
        {
            _service = service;
        }

        [HttpPost("create-ride")]
        public async Task<RideDto> CreateRide(
            [FromQuery] Guid clientId,
            [FromBody] IEnumerable<Point2d> points)
        {
            var rideEntity = new RideEntity(points.Select(p => new Point2dEntity(p.X, p.Y)).ToList(), clientId);
            await _service.RegisterRide(rideEntity);

            return new RideDto(
                rideEntity.Id,
                rideEntity.Path.Select(p => p.GetDto()).ToList(),
                rideEntity.Price,
                rideEntity.Status,
                rideEntity.AssignedClient,
                rideEntity.AssignedDriver);
        }

        [HttpGet("get-ride-for-client")]
        public IActionResult FindRidesForClient([FromQuery] Guid clientId)
        {
            // TODO: Позже думаю можно сделать этот метод просто Find и искать поездки по любым заданным параметрам
            if (clientId != Guid.Empty)
            {
                var result = _service.GetAllForClient(clientId);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPatch("assign-driver")]
        public async Task<IActionResult> AssignDriver([FromQuery] Guid rideId, Guid driverId)
        {
            if (rideId != Guid.Empty && driverId != Guid.Empty)
            {
                await _service.AssignDriver(rideId, driverId);
                return Ok(await _service.FindOneRide(rideId));
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPatch("wait-for-client")]
        public async Task<IActionResult> WaitForClient([FromQuery] Guid rideId)
        {
            if (rideId != Guid.Empty)
            {
                await _service.WaitForClient(rideId);
                return Ok(await _service.FindOneRide(rideId));
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPatch("start-ride")]
        public async Task<IActionResult> StartRide([FromQuery] Guid rideId)
        {
            if (rideId != Guid.Empty)
            {
                await _service.StartRide(rideId);
                return Ok(await _service.FindOneRide(rideId));
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPatch("end-ride")]
        public async Task<IActionResult> EndRide([FromQuery] Guid rideId)
        {
            if (rideId != Guid.Empty)
            {
                await _service.EndRide(rideId);
                return Ok(await _service.FindOneRide(rideId));
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPatch("cancel-ride")]
        public async Task<IActionResult> CancelRide([FromQuery] Guid rideId)
        {
            if (rideId != Guid.Empty)
            {
                await _service.CancelRide(rideId);
                return Ok(await _service.FindOneRide(rideId));
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }
    }
}