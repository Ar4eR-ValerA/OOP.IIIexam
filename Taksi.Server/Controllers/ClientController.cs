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
        public async Task<IActionResult> HasCreditCard([FromQuery] Guid clientId)
        {
            if (clientId == Guid.Empty) return BadRequest();
            bool result = await _service.HasCreditCard(clientId);
            return Ok(result);
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetCreditCardBalance([FromQuery] Guid clientId)
        {
            if (clientId == Guid.Empty) return BadRequest();
            decimal balance = await _service.GetCreditCardBalance(clientId);
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