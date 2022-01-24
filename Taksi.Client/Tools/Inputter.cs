using System;
using System.Collections.Generic;
using Taksi.DTO.Enums;

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
        
        public TaxiType InputTaxiType()
        {
            List<TaxiType> types = new List<TaxiType>()
                {TaxiType.Standard, TaxiType.Comfort, TaxiType.Business, TaxiType.Luxury};
            return _asker.AskChoices<TaxiType>("Enter your taxi type:\n", types);
        }
    }
}