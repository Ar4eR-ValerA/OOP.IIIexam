using System.Net.Http;
using Spectre.Console;
using Taksi.Driver.Tools;

namespace Taksi.Driver.Ui
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
                new Command("Register driver", async () => await _executor.RegisterDriver(_client)),
                new Command("Unregister driver", async () => await _executor.UnregisterDriver(_client)),
                new Command("Order", async () => await _executor.Order(_client)),
                new Command("Update driver status", async () => await _executor.UpdateDriverStatus(_client)),
                new Command("Wait for client", async () => await _executor.WaitForClient(_client)),
                new Command("Start ride", async () => await _executor.StartRide(_client)),
                new Command("End ride", async () => await _executor.EndRide(_client)),
                new Command("Cancel ride", async () => await _executor.CancelRide(_client)),
                new Command("Exit"),
            };

            Command command = _asker.AskChoices("Enter command", commands);

            while (command.Title != "Exit")
            {
                command.Action();

                AnsiConsole.Clear();

                command = _asker.AskChoices("Enter command", commands);
            }
        }
    }
}