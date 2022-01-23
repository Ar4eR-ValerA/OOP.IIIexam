using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ITMO.Client.UI;

namespace ITMO.Client.Tools
{
    public class Executor
    {
        private readonly Inputter _inputter;
        private readonly Asker _asker;
        private readonly Actions _actions;

        public Executor()
        {
            _inputter = new Inputter();
            _actions = new Actions();
            _asker = new Asker();
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
        
        public async Task GetCreditCardBalance(HttpClient client)
        {
            await _actions.GetCreditCardBalance(client);
        }

        public async Task SetCreditCardBalance(HttpClient client)
        {
            await _actions.SetCreditCardBalance(client);
        }
    }
}