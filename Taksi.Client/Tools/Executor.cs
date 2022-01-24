using System.Net.Http;
using System.Threading.Tasks;
using ITMO.Client.UI;

namespace ITMO.Client.Tools
{
    public class Executor
    {
        private readonly Actions _actions;

        public Executor()
        {
            _actions = new Actions();
        }


        public async Task RegisterClient(HttpClient client)
        {
            await _actions.RegisterClient(client);
        }

        public async Task UnregisterClient(HttpClient client)
        {
            await _actions.UnregisterClient(client);
        }
        
        public async Task RegisterCreditCard(HttpClient client)
        {
            await _actions.RegisterCreditCard(client);
        }
        
        public async Task UnregisterCreditCard(HttpClient client)
        {
            await _actions.UnregisterCreditCard(client);
        }
        
        public async Task CheckCreditCard(HttpClient client)
        {
            await _actions.CheckCreditCard(client);
        }
        
        public async Task SetCreditCardBalance(HttpClient client)
        {
            await _actions.SetCreditCardBalance(client);
        }
        
        public async Task RateDriver(HttpClient client)
        {
            await _actions.RateDriver(client);
        }

        public async Task CreateRide(HttpClient client)
        {
            await _actions.CreateRide(client);
        }
    }
}