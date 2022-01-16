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
        private IRepository<DriverEntity> _driverRepo;
        
        public RideService(IRepository<RideEntity> rideRepo, IRepository<DriverEntity> driverRepo)
        {
            _rideRepo = rideRepo ?? throw new ArgumentNullException(nameof(rideRepo));
            _driverRepo = driverRepo ?? throw new ArgumentNullException(nameof(driverRepo));
        }
        
        public async Task RegisterRide(RideEntity rideEntity)
        {
            await _rideRepo.InsertAsync(rideEntity);
        }

        public async Task AssignDriver(Guid rideId, Guid driverId)
        {
            var driverTask = _driverRepo.GetByIdAsync(driverId);
            var rideTask = _rideRepo.GetByIdAsync(rideId);

            var driverEntity = await driverTask;
            var rideEntity = await rideTask;
            
            if (driverEntity is null)
            {
                throw new ArgumentException("There is no such driver");
            }
            
            if (rideEntity is null)
            {
                throw new ArgumentException("There is no such ride");
            }
            
            rideEntity.AssignedDriver = driverId;
            await _rideRepo.UpdateAsync(rideEntity);
        }

        public async Task WaitForClient(Guid rideId)
        {
            var ride = await _rideRepo.GetByIdAsync(rideId);
            
            // TODO: Custom exception class?
            if (ride.Status != RideStatus.DriverComing)
                throw new ArgumentException("Only DriverComing -> WaitingClient sequence is correct");

            ride.Status = RideStatus.WaitingClient;
            await _rideRepo.UpdateAsync(ride);

            // TODO: Send messages to clients, if SignalR will work with us of course
        }

        public async Task StartRide(Guid rideId)
        {
            var ride = await _rideRepo.GetByIdAsync(rideId);
            
            // TODO: Custom exception class?
            if (ride.Status != RideStatus.WaitingClient)
                throw new ArgumentException("Only WaitingClient -> InProcess sequence is correct");

            ride.Status = RideStatus.InProcess;
            await _rideRepo.UpdateAsync(ride);

            // TODO: Send messages to clients, if SignalR will work with us of course
        }

        public async Task EndRide(Guid rideId)
        {
            var ride = await _rideRepo.GetByIdAsync(rideId);
            
            // TODO: Custom exception class?
            if (ride.Status != RideStatus.InProcess)
                throw new ArgumentException("Only InProcess -> Finished sequence is correct");

            ride.Status = RideStatus.Finished;
            await _rideRepo.UpdateAsync(ride);

            // TODO: Send messages to clients, if SignalR will work with us of course
        }

        public async Task CancelRide(Guid rideId)
        {
            var ride = await _rideRepo.GetByIdAsync(rideId);
            
            // TODO: Custom exception class?
            if (ride.Status != RideStatus.DriverComing)
                throw new ArgumentException("Only DriverComing -> Cancelled sequence is correct");

            ride.Status = RideStatus.Cancelled;
            await _rideRepo.UpdateAsync(ride);

            // TODO: Send messages to clients, if SignalR will work with us of course
        }

        public async Task<IEnumerable<RideEntity>> GetAllForClient(Guid clientId)
        {
            return await _rideRepo.GetWhereAsync(ride => ride.AssignedClient == clientId);
        }
    }
}