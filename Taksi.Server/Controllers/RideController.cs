using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taksi.DTO.DTOs;
using Taksi.Server.BLL.Services.Interfaces;
using Taksi.Server.DAL.Entities;

namespace Taksi.Server.Controllers
{
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IRideService _service;

        public RideController(IRideService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/create-ride")]
        public async Task<RideDto> CreateRide(
            [FromQuery] Guid clientId,
            [FromQuery] double startX,
            [FromQuery] double startY,
            [FromQuery] double endX,
            [FromQuery] double endY)
        {
            var rideEntity = new RideEntity(
                new List<Point2dEntity>
                {
                    new Point2dEntity(startX, startY),
                    new Point2dEntity(endX, endY)
                },
                clientId);
            await _service.RegisterRide(rideEntity);

            return new RideDto(
                rideEntity.Id,
                rideEntity.Path.Select(p => p.GetDto()).ToList(),
                rideEntity.Status,
                rideEntity.AssignedClient,
                rideEntity.AssignedDriver);
        }

        [HttpGet]
        [Route("/get-ride-for-client")]
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
    }
}