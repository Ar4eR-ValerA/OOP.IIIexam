using System;
using System.Collections.Generic;
using Taksi.DTO.Enums;
using Taksi.DTO.Models;

namespace Taksi.DTO.DTOs
{
    public class RideDto
    {
        private List<Point2d> _path;

        public RideDto(
            Guid id,
            List<Point2d> path,
            RideStatus rideStatus,
            Guid assignedClient,
            Guid assignedDriver)
        {
            Id = id;
            _path = path;
            Status = rideStatus;
            AssignedClient = assignedClient;
            AssignedDriver = assignedDriver;
        }

        public Guid Id { get; set; }
        public IReadOnlyList<Point2d> Path => _path;
        public RideStatus Status { get; set; }
        public Guid AssignedDriver { get; set; }
        public Guid AssignedClient { get; set; }
    }
}