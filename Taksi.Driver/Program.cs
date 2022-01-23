using System.Net.Http;
using Taksi.Driver.Tools;
using Taksi.Driver.Ui;
using Taksi.Server.DAL.Entities;

namespace Taksi.Driver
{
    class Program
    {
        private static readonly HttpClient Client = new();
        private readonly Asker _asker;

        static void Main(string[] args)
        {
            var uiService = new UiService();
            uiService.Run();
        }
    }
}