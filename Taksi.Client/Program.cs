using System;
using System.Net.Http;
using ITMO.Client.Tools;
using ITMO.Client.UI;

namespace ITMO.Client
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