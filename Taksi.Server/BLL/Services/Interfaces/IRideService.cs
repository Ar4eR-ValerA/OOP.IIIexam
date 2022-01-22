using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taksi.Server.DAL.Entities;

namespace Taksi.Server.BLL.Services.Interfaces
{
    public interface IRideService
    {
        // rideEntity without assigned driver.
        // Send messages to drivers, etc
        Task RegisterRide(RideEntity rideEntity);
        
        // Update status, assigned driver, etc
        Task AssignDriver(Guid rideId, Guid driverId);

        // Send message to client, update status, etc
        Task WaitForClient(Guid rideId);

        // Update RideStatus, etc
        Task StartRide(Guid rideId);
        Task EndRide(Guid rideId);

        // Send messages, update status, etc
        Task CancelRide(Guid rideId);

        Task<RideEntity> FindOneRide(Guid rideId);

        IEnumerable<RideEntity> GetAllForClient(Guid clientId);
    }
}