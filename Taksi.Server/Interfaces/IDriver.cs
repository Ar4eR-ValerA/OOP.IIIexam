using System;
using System.Collections.Generic;
using Taksi.DTO.Models;

namespace Taksi.Server.Interfaces
{
    public interface IDriver
    {
        void RegisterDriver(DriverDto driverDto);
        IEnumerable<RideDto> GetNearRides(Guid driverId);
        void UpdateLocation(Guid driverId, Point point);
        void AssignDriver(Guid driverId, Guid rideId);
        void UpdateRideStatus(Guid rideId, RideStatus rideStatus);
    }
}