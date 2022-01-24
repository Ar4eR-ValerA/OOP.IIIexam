using System;
using System.Net.Http;
using ITMO.Client.Tools;
using ITMO.Client.UI;

namespace ITMO.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var uiService = new UiService();
            uiService.Run();
        }
    }
}