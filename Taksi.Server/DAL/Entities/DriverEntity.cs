using System;
using Taksi.DTO.Enums;
using Taksi.DTO.Models;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class DriverEntity : IIdentifiable
    {
        public Guid Id { get; set; }
        
        public string FullName { get; set; }
        public DriverStatus Status { get; set; }
        public TaxiType TaxiType { get; set; }
        public Point2dEntity Location { get; set; }
    }
}