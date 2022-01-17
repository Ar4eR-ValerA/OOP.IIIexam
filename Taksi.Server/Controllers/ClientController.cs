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
        protected readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }

        [HttpPost("client")]
        public async Task RegisterClient(ClientEntity client)
        {
            await _service.RegisterClient(client);
        }

        [HttpPost("card")]
        public async Task RegisterCreditCard(CreditCardEntity creditCard)
        {
            await _service.RegisterCreditCard(creditCard);
        }

        [HttpGet("card")]
        public IActionResult HasCreditCard([FromQuery] Guid clientId)
        {
            if (clientId == Guid.Empty) return BadRequest();
            bool result = _service.HasCreditCard(clientId).Result;
            return Ok(result);
        }

        [HttpGet("balance")]
        public IActionResult GetCreditCardBalance([FromQuery] Guid clientId)
        {
            if (clientId == Guid.Empty) return BadRequest();
            decimal balance = _service.GetCreditCardBalance(clientId).Result;
            return Ok(balance);
        }

        [HttpPut]
        public Task Update([FromQuery] Guid clientId, decimal newBalance)
        {
            return _service.SetCreditCardBalance(clientId, newBalance);
        }

        [HttpDelete("client")]
        public void UnregisterClient([FromQuery] Guid clientId)
        {
            _service.UnregisterClient(clientId);
        }

        [HttpDelete("card")]
        public void UnregisterCreditCard([FromQuery] Guid creditCardId)
        {
            _service.UnregisterCreditCard(creditCardId);
        }
    }
}