using System;
using Taksi.DTO.DTOs;
using Taksi.DTO.Enums;
using Taksi.DTO.Models;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class DriverEntity : IIdentifiable
    {
        internal DriverEntity()
        {
        }

        public DriverEntity(string fullName, TaxiType taxiType)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            TaxiType = taxiType;
            Status = DriverStatus.WaitingForClient;
            RatingSum = 0;
            CountOfRatings = 0;
            //TODO: Подумать, что делать с координатами при регистрации водителя.
            Location = new Point2dEntity(0, 0);
        }

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DriverStatus Status { get; set; }
        public TaxiType TaxiType { get; set; }
        public double RatingSum { get; set; }
        public int CountOfRatings { get; set; }
        public Point2dEntity Location { get; set; }

        public DriverDto GetDto()
        {
            return new DriverDto(Id, FullName, Status, TaxiType, RatingSum, CountOfRatings,
                new Point2d(Location.X, Location.Y));
        }
    }
}