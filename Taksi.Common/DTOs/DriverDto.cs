using System;
using Taksi.DTO.Enums;
using Taksi.DTO.Models;

namespace Taksi.DTO.DTOs
{
    public class DriverDto
    {
        public Guid Id { get; set; }
        
        public string FullName { get; set; }
        public DriverStatus Status { get; set; }
        public TaxiType TaxiType { get; set; }
        public Point2d Location { get; set; }
    }
}