using System;
using System.Collections.Generic;
using Taksi.DTO.Enums;
using Taksi.DTO.Models;

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

        public double InputDouble(string message)
        {
            return _asker.AskDouble(message + "\n");
        }

        public TaxiType InputTaxiType()
        {
            List<TaxiType> types = new List<TaxiType>()
                {TaxiType.Standard, TaxiType.Comfort, TaxiType.Business, TaxiType.Luxury};
            return _asker.AskChoices<TaxiType>("Enter your taxi type:\n", types);
        }

        public List<Point2d> InputPoints()
        {
            List<Point2d> points = new List<Point2d>() {InputPoint(), InputPoint()};
            var ans = _asker.AskChoices("Add more point?", new List<string>() {"Yes", "No"});
            while (!ans.Equals("No"))
            {
                points.Add(InputPoint());
                ans = _asker.AskChoices("Add more point?", new List<string>() {"Yes", "No"});
            }

            return points;
        }

        private Point2d InputPoint()
        {
            var x = _asker.AskDouble("X: ");
            var y = _asker.AskDouble("Y: ");
            return new Point2d(x, y);
        }
    }
}