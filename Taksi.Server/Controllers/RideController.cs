using System;
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
        public async Task<RideDto> Create(
            [FromQuery] Guid client,
            [FromQuery] double startX,
            [FromQuery] double startY,
            [FromQuery] double endX,
            [FromQuery] double endY)
        {
            
            return await _service.Create(name);
        }

        [HttpGet]
        public IActionResult Find([FromQuery] string name, [FromQuery] Guid id)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Employee result = _service.FindByName(name);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            if (id != Guid.Empty)
            {
                Employee result = _service.FindById(id);
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