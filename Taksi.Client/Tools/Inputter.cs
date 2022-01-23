using System;
using System.Collections.Generic;

namespace ITMO.Client.Tools
{
    public class Inputter
    {
        private readonly Asker _asker;

        public Inputter()
        {
            _asker = new Asker();
        }

        public string InputName()
        {
            return _asker.AskString("Enter your name:\n");
        }

        public Guid InputGuid(string message)
        {
            return _asker.AskGuid(message + "\n");
        }
        
        public decimal InputDecimal(string message)
        {
            return _asker.AskDecimal(message + "\n");
        }
    }
}