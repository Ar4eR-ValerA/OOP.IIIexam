using System;
using System.Collections.Generic;
using System.Linq;
using Taksi.DTO.DTOs;
using Taksi.DTO.Enums;
using Taksi.DTO.Models;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class RideEntity : IIdentifiable
    {
        private readonly List<Point2dEntity> _path;

        public RideEntity()
        {
            _path = new List<Point2dEntity>();
        }

        public RideEntity(List<Point2dEntity> path, Guid assignedClient)
        {
            Id = Guid.NewGuid();
            _path = path;
            AssignedClient = assignedClient;
        }

        public virtual Guid Id { get; set; }
        public virtual IReadOnlyList<Point2dEntity> Path => _path;
        public virtual RideStatus Status { get; set; }
        public virtual Guid AssignedDriver { get; set; }
        public virtual Guid AssignedClient { get; set; }

        public RideDto GetDto()
        {
            return new RideDto(
                Id,
                Path.Select(p => new Point2d(p.X, p.Y)).ToList(),
                Status,
                AssignedClient,
                AssignedDriver);
        }
    }
}