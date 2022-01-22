using System;

namespace Taksi.Server.Logger
{
    public class ConsoleLogger : ILogger
    {
        private readonly bool _timePrefix;

        public ConsoleLogger(bool timePrefix = false)
        {
            _timePrefix = timePrefix;
        }

        public void LogInfo(string info)
        {
            if (_timePrefix)
            {
                Console.Write(DateTime.Now + " ");
            }

            Console.WriteLine(info);
        }
    }
}