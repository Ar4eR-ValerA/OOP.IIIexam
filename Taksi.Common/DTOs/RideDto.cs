using System;
using System.Collections.Generic;
using Taksi.DTO.Enums;
using Taksi.DTO.Models;

namespace Taksi.DTO.DTOs
{
    public class RideDto
    {
        public Guid Id { get; set; }
        
        public List<Point2d> Path { get; set; }
        public RideStatus Status { get; set; }
        public Guid AssignedDriver { get; set; }
        public Guid AssignedClient { get; set; }
    }
}