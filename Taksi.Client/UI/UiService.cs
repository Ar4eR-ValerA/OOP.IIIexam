using System.Net.Http;
using ITMO.Client.Tools;
using Spectre.Console;

namespace ITMO.Client.UI
{
    public class UiService
    {
        private readonly Asker _asker;
        private readonly Executor _executor;
        private readonly HttpClient _client;

        public UiService()
        {
            _asker = new Asker();
            _executor = new Executor();
            _client = new HttpClient();
        }

        public void Run()
        {
            Command[] commands =
            {
                new Command("Register client", async () => await _executor.RegisterClient(_client)),
                new Command("Register credit card", async () => await _executor.RegisterCreditCard(_client)),
                new Command("Unregister client", async () => await _executor.UnregisterClient(_client)),
                new Command("Unregister credit card", async () => await _executor.UnregisterCreditCard(_client)),
                new Command("Check credit card", async () => await _executor.CheckCreditCard(_client)),
                new Command("Get credit card balance", async () => await _executor.GetCreditCardBalance(_client)),
                new Command("Set credit card balance", async () => await _executor.SetCreditCardBalance(_client)),
                new Command("exit"),
            };

            Command command = _asker.AskChoices("Enter command", commands);

            while (command.Title != "exit")
            {
                command.Action();

                AnsiConsole.Clear();

                command = _asker.AskChoices("Enter command", commands);
            }
        }
    }
}