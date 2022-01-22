using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taksi.DTO.DTOs;
using Taksi.DTO.Enums;
using Taksi.DTO.Models;
using Taksi.Server.Attributes;
using Taksi.Server.BLL.Services.Interfaces;
using Taksi.Server.DAL.Entities;

namespace Taksi.Server.Controllers
{
    [ApiController]
    [Route("/drivers")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _service;
        private const int _fullNameMaxLen = 50;

        public DriverController(IDriverService service)
        {
            _service = service;
        }

        //TODO: проверить задаются ли в Swaggere enum как число
        [HttpPost("register-driver")]
        public async Task<DriverDto> RegisterDriver(
            [FromQuery]
            [Required(ErrorMessage = "Name not specified")]
            [StringLength(_fullNameMaxLen, ErrorMessage = "Comment too long")]
            string name,
            [Range(0, 3)] [Required(ErrorMessage = "Taxi type not specified")]
            TaxiType taxiType)
        {
            var driver = new DriverEntity(name, taxiType);
            await _service.RegisterDriver(driver);

            return driver.GetDto();
        }

        [HttpDelete("unregister-driver")]
        public async Task<IActionResult> UnregisterDriver(
            [FromQuery] [Required(ErrorMessage = "Id not specified")] [RequireNonDefault]
            Guid id)
        {
            await _service.UnregisterDriver(id);

            return Ok();
        }

        [HttpPost("rate-driver")]
        public async Task<IActionResult> RateDriver(
            [FromQuery] [Required(ErrorMessage = "Id not specified")] [RequireNonDefault]
            Guid id,
            [Range(0, 5)] [Required(ErrorMessage = "Rate not specified")]
            double rate)
        {
            await _service.RateDriver(id, rate);

            return Ok();
        }

        [HttpGet("get-rating")]
        public async Task<double> GetRating(
            [FromQuery] [Required(ErrorMessage = "Id not specified")] [RequireNonDefault]
            Guid id)
        {
            var rating = await _service.GetRating(id);

            return rating;
        }

        [HttpPost("set-location")]
        public async Task<IActionResult> SetLocation(
            [FromQuery] [Required(ErrorMessage = "Id not specified")] [RequireNonDefault]
            Guid id, Point2d point)
        {
            await _service.SetLocation(id, new Point2dEntity(point.X, point.Y));

            return Ok();
        }

        [HttpGet("get-location")]
        public async Task<Point2d> GetLocation(
            [FromQuery] [Required(ErrorMessage = "Id not specified")] [RequireNonDefault]
            Guid id)
        {
            var location = await _service.GetLocation(id);

            return new Point2d(location.X, location.Y);
        }

        [HttpPost("set-status")]
        public async Task<IActionResult> SetStatus(
            [FromQuery] [Required(ErrorMessage = "Id not specified")] [RequireNonDefault]
            Guid id,
            [Range(0, 2)] DriverStatus status)
        {
            await _service.SetStatus(id, status);

            return Ok();
        }

        [HttpGet("get-status")]
        public async Task<DriverStatus> GetStatus(
            [FromQuery] [Required(ErrorMessage = "Id not specified")] [RequireNonDefault]
            Guid id)
        {
            var status = await _service.GetStatus(id);

            return status;
        }

        [HttpGet("get-taxi-type")]
        public async Task<TaxiType> GetTaxiType(
            [FromQuery] [Required(ErrorMessage = "Id not specified")] [RequireNonDefault]
            Guid id)
        {
            var taxiType = await _service.GetTaxiType(id);

            return taxiType;
        }

        [HttpGet("get-nearest-to-location")]
        public Guid GetNearestToLocation(Point2d point)
        {
            var id = _service.GetNearestToLocation(new Point2dEntity(point.X, point.Y));

            return id;
        }
    }
}