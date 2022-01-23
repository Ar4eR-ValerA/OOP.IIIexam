using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spectre.Console;
using Taksi.Driver.Tools;
using Taksi.DTO.DTOs;

namespace Taksi.Driver.Ui
{
    public class Actions
    {
        private static Asker _asker;
        private static Inputter _inputter;

//         private readonly TableCreator _tableCreator;
//         private readonly TableFiller _tableFiller;
//
        public Actions()
        {
            _asker = new Asker();
            _inputter = new Inputter();
        }


        public async void RegisterDriver(HttpClient client)
        {
            var name = _inputter.InputName();
            var taxiType = _inputter.InputTaxiType();
            var response = 
                await client.PostAsync(
                    $"https://localhost:5001/drivers/register-driver?name={name}&taxiType={taxiType.ToString()}", null!);

            var responseStream = await response.Content.ReadAsStreamAsync();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = await readStream.ReadToEndAsync();
            
            var driver = JsonConvert.DeserializeObject<DriverDto>(responseString);
            AnsiConsole.Write("Done.");
        }
    }
}