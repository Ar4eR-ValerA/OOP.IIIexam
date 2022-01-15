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

        public async Task RegisterClient(ClientEntity clientEntity)
        {
            await _clientRepository.InsertAsync(clientEntity);
        }

        public async Task UnregisterClient(Guid clientId)
        {
            await _clientRepository.RemoveAsync(clientId);
        }

        public async Task RegisterCreditCard(CreditCardEntity creditCardEntity)
        {
            await _creditCardRepository.InsertAsync(creditCardEntity);
        }

        public async Task UnregisterCreditCard(Guid creditCardId)
        {
            await _creditCardRepository.RemoveAsync(creditCardId);
        }

        public async Task<bool> HasCreditCard(Guid clientId)
        {
            var cards = await _creditCardRepository.GetWhereAsync(card => card.ClientId == clientId);
            return cards.Any();
        }

        public async Task<decimal> GetCreditCardBalance(Guid clientId)
        {
            var cards = await _creditCardRepository.GetWhereAsync(card => card.ClientId == clientId);
            return cards.Last().CardBalance;
        }

        public async Task SetCreditCardBalance(Guid clientId, decimal newBalance)
        {
            var cards = await _creditCardRepository.GetWhereAsync(card => card.ClientId == clientId);
            cards.Last().CardBalance = newBalance;
            await _creditCardRepository.UpdateAsync(cards.Last());
        }
    }
}