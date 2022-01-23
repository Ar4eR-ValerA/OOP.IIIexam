using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Taksi.Driver.Ui;

namespace Taksi.Driver.Tools
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


        public async Task RegisterDriver(HttpClient client)
        {
            await _actions.RegisterDriver(client);
        }

        public async Task UnregisterDriver(HttpClient client)
        {
            await _actions.UnregisterDriver(client);
        }

        public async Task UpdateDriverStatus(HttpClient client)
        {
            await _actions.UpdateDriverStatus(client);
        }

        public async Task WaitForClient(HttpClient client)
        {
            await _actions.WaitForClient(client);
        }

        public async Task StartRide(HttpClient client)
        {
            await _actions.StartRide(client);
        }

        public async Task EndRide(HttpClient client)
        {
            await _actions.EndRide(client);
        }

        public async Task CancelRide(HttpClient client)
        {
            await _actions.CancelRide(client);
        }

        public async Task Order(HttpClient client)
        {
            var ans = _asker.AskChoices("Accept order?", new List<string>() {"Yes", "No"});
            if (ans.Equals("Yes"))
                await _actions.Order(client);
        }
    }
}