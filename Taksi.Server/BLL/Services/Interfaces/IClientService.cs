using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taksi.Server.DAL.Entities;

namespace Taksi.Server.BLL.Services.Interfaces
{
    public interface IClientService
    {
        Task RegisterClient(ClientEntity clientEntity);
        Task UnregisterClient(Guid clientId);

        // Only one credit card for each client should be allowed for simplicity
        Task RegisterCreditCard(CreditCardEntity creditCardEntity);
        Task UnregisterCreditCard(Guid creditCardId);

        // Work with credit card repository
        Task<bool> HasCreditCard(Guid clientId);
        Task<decimal> GetCreditCardBalance(Guid clientId);
        Task SetCreditCardBalance(Guid clientId, decimal newBalance);
    }
}