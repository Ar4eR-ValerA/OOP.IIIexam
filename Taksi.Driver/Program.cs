using Taksi.Driver.Ui;

namespace Taksi.Driver
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