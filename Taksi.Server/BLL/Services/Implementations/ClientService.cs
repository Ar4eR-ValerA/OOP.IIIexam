using System;
using System.Linq;
using System.Threading.Tasks;
using Taksi.Server.BLL.Services.Interfaces;
using Taksi.Server.DAL.Entities;
using Taksi.Server.DAL.Exceptions;
using Taksi.Server.DAL.Repositories.Interfaces;
using Taksi.Server.Logger;

namespace Taksi.Server.BLL.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IRepository<ClientEntity> _clientRepository;
        private readonly IRepository<CreditCardEntity> _creditCardRepository;
        private readonly ILogger _logger;

        public ClientService(IRepository<ClientEntity> clientRepo, IRepository<CreditCardEntity> creditCardRepo, ILogger logger)
        {
            _clientRepository = clientRepo ?? throw new ArgumentNullException(nameof(clientRepo));
            _creditCardRepository = creditCardRepo ?? throw new ArgumentNullException(nameof(creditCardRepo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RegisterClient(ClientEntity clientEntity)
        {
            _logger.LogInfo($"Client {clientEntity.Id} registered.");
            
            await _clientRepository.InsertAsync(clientEntity);
        }

        public async Task UnregisterClient(Guid clientId)
        {
            _logger.LogInfo($"Client {clientId} unregistered.");
            
            await _clientRepository.RemoveAsync(clientId);
        }

        public async Task RegisterCreditCard(CreditCardEntity creditCardEntity)
        {
            _logger.LogInfo($"Registered credit card {creditCardEntity.Id} for client {creditCardEntity.ClientId}.");
            
            await _creditCardRepository.InsertAsync(creditCardEntity);
        }

        public async Task UnregisterCreditCard(Guid creditCardId)
        {
            _logger.LogInfo($"Credit card {creditCardId} unregistered.");
            
            await _creditCardRepository.RemoveAsync(creditCardId);
        }

        public bool HasCreditCard(Guid clientId)
        {
            var cards = _creditCardRepository.GetWhereAsync(card => card.ClientId == clientId);
            
            _logger.LogInfo($"Check credit card existing for client {clientId}");
            
            return cards.Any();
        }

        public decimal GetCreditCardBalance(Guid clientId)
        {
            if (!HasCreditCard(clientId))
                throw new EntityDoesNotExistException("Current client doesn't have credit card");
            var cards = _creditCardRepository.GetWhereAsync(card => card.ClientId == clientId);
            
            _logger.LogInfo($"Get balance for client {clientId}");
            
            return cards.Last().CardBalance;
        }

        public void SetCreditCardBalance(Guid clientId, decimal newBalance)
        {
            if (!HasCreditCard(clientId))
                throw new EntityDoesNotExistException("Current client doesn't have credit card");
            var cards = _creditCardRepository.GetWhereAsync(card => card.ClientId == clientId);
            cards.Last().CardBalance = newBalance;
            
            _logger.LogInfo($"Set balance for client {clientId}");

            _creditCardRepository.UpdateAsync(cards.Last());
        }
    }
}