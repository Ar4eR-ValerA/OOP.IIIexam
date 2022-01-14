using System;
using System.Linq;
using System.Threading.Tasks;
using Taksi.Server.BLL.Services.Interfaces;
using Taksi.Server.DAL.Entities;
using Taksi.Server.DAL.Repositories.Interfaces;

namespace Taksi.Server.BLL.Services.Implementations
{
    public class ClientService : IClientService
    {
        private IRepository<ClientEntity> _clientRepository;
        private IRepository<CreditCardEntity> _creditCardRepository;

        public ClientService(IRepository<ClientEntity> clientRepo, IRepository<CreditCardEntity> creditCardRepo)
        {
            _clientRepository = clientRepo ?? throw new ArgumentNullException(nameof(clientRepo));
            _creditCardRepository = creditCardRepo ?? throw new ArgumentNullException(nameof(creditCardRepo));
        }
        
        public Task RegisterClient(ClientEntity clientEntity)
        {
            throw new NotImplementedException();
        }

        public async Task UnregisterClient(Guid clientId)
        {
            await _clientRepository.RemoveAsync(clientId);
        }

        public Task RegisterCreditCard(CreditCardEntity creditCardEntity)
        {
            throw new NotImplementedException();
        }

        public Task UnregisterCreditCard(Guid creditCardId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasCreditCard(Guid clientId)
        {
            var cards = await _creditCardRepository.GetWhereAsync(card => card.ClientId == clientId);
            return cards.Any();
        }

        public Task<decimal> GetCreditCardBalance(Guid clientId)
        {
            throw new NotImplementedException();
        }

        public Task SetCreditCardBalance(Guid clientId, decimal newBalance)
        {
            throw new NotImplementedException();
        }
    }
}