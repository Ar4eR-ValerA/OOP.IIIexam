using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taksi.DTO.Enums;
using Taksi.Server.BLL.Services.Interfaces;
using Taksi.Server.DAL.Entities;
using Taksi.Server.DAL.Repositories.Interfaces;

namespace Taksi.Server.BLL.Services.Implementations
{
    public class RideService : IRideService
    {
        private IRepository<RideEntity> _rideRepo;
        
        public RideService(IRepository<RideEntity> rideRepo)
        {
            _rideRepo = rideRepo ?? throw new ArgumentNullException(nameof(rideRepo));
        }
        
        public Task RegisterRide(RideEntity rideEntity)
        {
            throw new NotImplementedException();
        }

        public Task AssignDriver(Guid rideId, Guid driverId)
        {
            throw new NotImplementedException();
        }

        public async Task WaitForClient(Guid rideId)
        {
            RideEntity ride = await _rideRepo.GetByIdAsync(rideId);
            
            // TODO: Custom exception class?
            if (ride.Status != RideStatus.DriverComing)
                throw new ArgumentException("Only DriverComing -> WaitingClient sequence is correct");

            ride.Status = RideStatus.WaitingClient;
            await _rideRepo.UpdateAsync(ride);

            // Send messages to clients, if SignalR will work with us of course
        }

        public Task StartRide(Guid rideId)
        {
            throw new NotImplementedException();
        }

        public Task EndRide(Guid rideId)
        {
            throw new NotImplementedException();
        }

        public Task CancelRide(Guid rideId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RideEntity>> GetAllForClient(Guid clientId)
        {
            return await _rideRepo.GetWhereAsync(ride => ride.AssignedClient == clientId);
        }
    }
}