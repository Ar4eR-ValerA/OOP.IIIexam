using System;
using System.Linq;
using System.Threading.Tasks;
using Taksi.Server.BLL.Services.Interfaces;
using Taksi.Server.DAL.Entities;
using Taksi.Server.DAL.Exceptions;
using Taksi.Server.DAL.Repositories.Interfaces;

namespace Taksi.Server.BLL.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IRepository<ClientEntity> _clientRepository;
        private readonly IRepository<CreditCardEntity> _creditCardRepository;

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

        public bool HasCreditCard(Guid clientId)
        {
            var cards = _creditCardRepository.GetWhereAsync(card => card.ClientId == clientId);
            return cards.Any();
        }

        public decimal GetCreditCardBalance(Guid clientId)
        {
            if (!HasCreditCard(clientId))
                throw new EntityDoesNotExistException("Current client doesn't have credit card");
            var cards = _creditCardRepository.GetWhereAsync(card => card.ClientId == clientId);
            return cards.Last().CardBalance;
        }

        public void SetCreditCardBalance(Guid clientId, decimal newBalance)
        {
            if (!HasCreditCard(clientId))
                throw new EntityDoesNotExistException("Current client doesn't have credit card");
            var cards = _creditCardRepository.GetWhereAsync(card => card.ClientId == clientId);
            cards.Last().CardBalance = newBalance;
            _creditCardRepository.UpdateAsync(cards.Last());
        }
    }
}