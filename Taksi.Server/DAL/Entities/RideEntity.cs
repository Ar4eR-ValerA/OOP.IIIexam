using System;
using System.Collections.Generic;
using Taksi.DTO.Enums;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class RideEntity : IIdentifiable
    {
        public Guid Id { get; set; }
        
        public List<Point2dEntity> Path { get; set; }
        public RideStatus Status { get; set; }
        public Guid AssignedDriver { get; set; }
        public Guid AssignedClient { get; set; }
    }
}