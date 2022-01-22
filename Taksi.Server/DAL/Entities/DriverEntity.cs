using System;
using Taksi.DTO.DTOs;
using Taksi.DTO.Enums;
using Taksi.DTO.Models;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class DriverEntity : IIdentifiable
    {
        public DriverEntity()
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

        public virtual Guid Id { get; set; }
        public virtual string FullName { get; set; }
        public virtual DriverStatus Status { get; set; }
        public virtual TaxiType TaxiType { get; set; }
        public virtual double RatingSum { get; set; }
        public virtual int CountOfRatings { get; set; }
        public virtual Point2dEntity Location { get; set; }

        public DriverDto GetDto()
        {
            return new DriverDto(Id, FullName, Status, TaxiType, RatingSum, CountOfRatings,
                new Point2d(Location.X, Location.Y));
        }
    }
}