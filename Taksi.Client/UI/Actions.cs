using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using ITMO.Client.Tools;
using Spectre.Console;

namespace ITMO.Client.UI
{
    public class Actions
    {
        private static Inputter _inputter;

        public Actions()
        {
            _inputter = new Inputter();
        }


        public async Task RegisterClient(HttpClient client)
        {
            var name = _inputter.InputName();
            var response =
                await client.PostAsync(
                    $"https://localhost:5001/clients/register-client?fullName={name}",
                    null!);

            AnsiConsole.Write("Done.");
        }

        public async Task UnregisterClient(HttpClient client)
        {
            var clientId = _inputter.InputGuid("Client Id:");
            var response =
                await client.DeleteAsync(
                    $"https://localhost:5001/clients/unregister-client?id={clientId}");

            AnsiConsole.Write("Done.");
        }

        public async Task RegisterCreditCard(HttpClient client)
        {
            var clientId = _inputter.InputGuid("Client Id:");
            var balance = _inputter.InputDecimal("Card balance:");
            var response =
                await client.PostAsync(
                    $"https://localhost:5001/clients/register-credit-card?clientId={clientId}&balance={balance}",
                    null!);

            AnsiConsole.Write("Done.");
        }
        
        public async Task CreateRide(HttpClient client)
        {
            var clientId = _inputter.InputGuid("Client Id:");
            var taxiType = _inputter.InputTaxiType();
            var response =
                await client.PostAsync(
                    $"https://localhost:5001/rides/create-ride?clientId={clientId}&taxiType={taxiType.ToString()}",
                    null!);

            AnsiConsole.Write("Done.");
        }


        public async Task UnregisterCreditCard(HttpClient client)
        {
            var creditCardId = _inputter.InputGuid("Credit Card Id:");
            var response =
                await client.DeleteAsync(
                    $"https://localhost:5001/clients/unregister-credit-card?id={creditCardId}");

            AnsiConsole.Write("Done.");
        }

        public async Task CheckCreditCard(HttpClient client)
        {
            var clientId = _inputter.InputGuid("Client Id:");
            var response =
                await client.GetAsync(
                    $"https://localhost:5001/clients/check-credit-card?clientId={clientId}");

            AnsiConsole.Write("Done.");
        }

        public async Task GetCreditCardBalance(HttpClient client)
        {
            var clientId = _inputter.InputGuid("Client Id:");
            var response =
                await client.GetAsync(
                    $"https://localhost:5001/clients/get-credit-card-balance?clientId={clientId}");

            AnsiConsole.Write("Done.");
        }

        public async Task SetCreditCardBalance(HttpClient client)
        {
            var clientId = _inputter.InputGuid("Client Id:");
            var newBalance = _inputter.InputDecimal("New card balance:");
            var response =
                await client.PutAsync(
                    $"https://localhost:5001/clients/register-credit-card?clientId={clientId}&newBalance={newBalance}",
                    null!);

            AnsiConsole.Write("Done.");
        }
        
        public async Task GetDriverRating(HttpClient client)
        {
            var driverId = _inputter.InputGuid("Driver Id:");
            var response =
                await client.GetAsync(
                    $"https://localhost:5001/drivers/get-rating?id={driverId}");

            AnsiConsole.Write("Done.");
        }
        
        public async Task RateDriver(HttpClient client)
        {
            var driverId = _inputter.InputGuid("Driver Id:");
            var rate = _inputter.InputDecimal("Rating: ");
                
            var response =
                await client.PostAsync(
                    $"https://localhost:5001/drivers/rate-driver?id={driverId}&rate{rate.ToString(CultureInfo.InvariantCulture)}",
                    null!);

            AnsiConsole.Write("Done.");
        }
        
        public async Task GetAllRides(HttpClient client)
        {
            var clientId = _inputter.InputGuid("Client Id:");
            var response =
                await client.GetAsync(
                    $"https://localhost:5001/rides/get-rides-for-client?clientId={clientId}");

            AnsiConsole.Write("Done.");
        }
    }
}