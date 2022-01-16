using System;
using Taksi.DTO.Enums;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class DriverEntity : IIdentifiable
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DriverStatus Status { get; set; }
        public TaxiType TaxiType { get; set; }
        public double RatingSum { get; set; }
        public int CountOfRatings { get; set; }
        public Point2dEntity Location { get; set; }
    }
}