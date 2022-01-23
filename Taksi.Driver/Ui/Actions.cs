using System.Net.Http;
using System.Threading.Tasks;
using Spectre.Console;
using Taksi.Driver.Tools;

namespace Taksi.Driver.Ui
{
    public class Actions
    {
        private static Inputter _inputter;

        public Actions()
        {
            _inputter = new Inputter();
        }


        public async Task RegisterDriver(HttpClient client)
        {
            var name = _inputter.InputName();
            var taxiType = _inputter.InputTaxiType();
            var response =
                await client.PostAsync(
                    $"https://localhost:5001/drivers/register-driver?name={name}&taxiType={taxiType.ToString()}",
                    null!);

            AnsiConsole.Write("Done.");
        }

        public async Task UnregisterDriver(HttpClient client)
        {
            var driverId = _inputter.InputGuid("Driver Id:");
            var response =
                await client.DeleteAsync(
                    $"https://localhost:5001/drivers/unregister-driver?id={driverId}");

            AnsiConsole.Write("Done.");
        }

        public async Task Order(HttpClient client)
        {
            var rideId = _inputter.InputGuid("Ride Id:");
            var driverId = _inputter.InputGuid("Driver Id:");
            var response =
                await client.PatchAsync(
                    $"https://localhost:5001/rides/assign-driver?rideId={rideId}&driverId={driverId}",
                    null!);

            AnsiConsole.Write("Done.");
        }

        public async Task UpdateDriverStatus(HttpClient client)
        {
            var driverId = _inputter.InputGuid("Driver Id:");
            var status = _inputter.InputDriverStatus();
            var response =
                await client.PostAsync(
                    $"https://localhost:5001/drivers/set-status?id={driverId}&status={status.ToString()}",
                    null!);

            AnsiConsole.Write("Done.");
        }

        public async Task WaitForClient(HttpClient client)
        {
            var rideId = _inputter.InputGuid("Ride Id:");
            var response =
                await client.PatchAsync(
                    $"https://localhost:5001/rides/wait-for-client?rideId={rideId}",
                    null!);

            AnsiConsole.Write("Done.");
        }

        public async Task StartRide(HttpClient client)
        {
            var rideId = _inputter.InputGuid("Ride Id:");
            var response =
                await client.PatchAsync(
                    $"https://localhost:5001/rides/start-ride?rideId={rideId}",
                    null!);

            AnsiConsole.Write("Done.");
        }

        public async Task EndRide(HttpClient client)
        {
            var rideId = _inputter.InputGuid("Ride Id:");
            var response =
                await client.PatchAsync(
                    $"https://localhost:5001/rides/end-ride?rideId={rideId}",
                    null!);

            AnsiConsole.Write("Done.");
        }

        public async Task CancelRide(HttpClient client)
        {
            var rideId = _inputter.InputGuid("Ride Id:");
            var response =
                await client.PatchAsync(
                    $"https://localhost:5001/rides/cancel-ride?rideId={rideId}",
                    null!);

            AnsiConsole.Write("Done.");
        }
    }
}