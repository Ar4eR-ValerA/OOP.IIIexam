using System.Net.Http;
using System.Threading.Tasks;
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
                new Command("Register driver", () => _executor.RegisterDriver(_client)),
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