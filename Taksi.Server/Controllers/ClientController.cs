using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taksi.Server.BLL.Services.Interfaces;
using Taksi.Server.DAL.Entities;

namespace Taksi.Server.Controllers
{
    [ApiController]
    [Route("/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }

        [HttpPost("register-client")]
        public async Task RegisterClient([FromQuery] string fullName)
        {
            await _service.RegisterClient(new ClientEntity(fullName));
        }

        [HttpPost("register-credit-card")]
        public async Task RegisterCreditCard([FromQuery] Guid clientId, decimal balance)
        {
            await _service.RegisterCreditCard(new CreditCardEntity(clientId, balance));
        }

        [HttpGet("check-credit-card")]
        public IActionResult HasCreditCard([FromQuery] Guid clientId)
        {
            if (clientId == Guid.Empty) return BadRequest();
            bool result = _service.HasCreditCard(clientId);
            return Ok(result);
        }

        [HttpGet("get-credit-card-balance")]
        public IActionResult GetCreditCardBalance([FromQuery] Guid clientId)
        {
            if (clientId == Guid.Empty) return BadRequest();
            decimal balance = _service.GetCreditCardBalance(clientId);
            return Ok(balance);
        }

        [HttpPut("set-credit-card-balance")]
        public void Update([FromQuery] Guid clientId, decimal newBalance)
        {
            _service.SetCreditCardBalance(clientId, newBalance);
        }

        [HttpDelete("unregister-client")]
        public async Task UnregisterClient([FromQuery] Guid clientId)
        {
            await _service.UnregisterClient(clientId);
        }

        [HttpDelete("unregister-credit-card")]
        public async Task UnregisterCreditCard([FromQuery] Guid creditCardId)
        {
            await _service.UnregisterCreditCard(creditCardId);
        }
    }
}