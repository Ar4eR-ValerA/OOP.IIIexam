using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taksi.Server.DAL.Entities;

namespace Taksi.Server.BLL.Services.Interfaces
{
    public interface IRideService
    {
        public double StandardCoefficient { get; set; }
        public double ComfortCoefficient { get; set; }
        public double BusinessCoefficient { get; set; }
        public double LuxuryCoefficient { get; set; }
        public double DensityCoefficient { get; set; }
        
        Task RegisterRide(RideEntity rideEntity);
        
        Task AssignDriver(Guid rideId, Guid driverId);
        
        Task WaitForClient(Guid rideId);
        
        Task StartRide(Guid rideId);
        Task EndRide(Guid rideId);
        
        Task CancelRide(Guid rideId);

        Task<RideEntity> FindOneRide(Guid rideId);

        IEnumerable<RideEntity> GetAllForClient(Guid clientId);
    }
}