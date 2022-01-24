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
        
        Task RegisterCreditCard(CreditCardEntity creditCardEntity);
        Task UnregisterCreditCard(Guid creditCardId);
        
        bool HasCreditCard(Guid clientId);
        decimal GetCreditCardBalance(Guid clientId);
        void SetCreditCardBalance(Guid clientId, decimal newBalance);
    }
}