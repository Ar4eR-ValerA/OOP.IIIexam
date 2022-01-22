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
        bool HasCreditCard(Guid clientId);
        decimal GetCreditCardBalance(Guid clientId);
        void SetCreditCardBalance(Guid clientId, decimal newBalance);
    }
}