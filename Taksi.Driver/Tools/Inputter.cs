using System.Collections.Generic;
using Taksi.DTO.Enums;

namespace Taksi.Driver.Tools
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

        public TaxiType InputTaxiType()
        {
            List<TaxiType> types = new List<TaxiType>()
                {TaxiType.Standard, TaxiType.Comfort, TaxiType.Business, TaxiType.Luxury};
            return _asker.AskChoices<TaxiType>("Enter your taxi type:\n", types);
        }
    }
}