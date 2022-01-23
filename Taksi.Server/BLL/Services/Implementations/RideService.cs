using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taksi.DTO.Enums;
using Taksi.Server.BLL.Services.Interfaces;
using Taksi.Server.DAL.Entities;
using Taksi.Server.DAL.Repositories.Interfaces;
using Taksi.Server.Logger;

namespace Taksi.Server.BLL.Services.Implementations
{
    public class RideService : IRideService
    {
        private readonly IRepository<RideEntity> _rideRepo;
        private readonly IRepository<DriverEntity> _driverRepo;
        private readonly ILogger _logger;

        public RideService(IRepository<RideEntity> rideRepo, IRepository<DriverEntity> driverRepo, ILogger logger)
        {
            _rideRepo = rideRepo ?? throw new ArgumentNullException(nameof(rideRepo));
            _driverRepo = driverRepo ?? throw new ArgumentNullException(nameof(driverRepo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RegisterRide(RideEntity rideEntity)
        {
            _logger.LogInfo($"Ride {rideEntity.Id} registered.");
            
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

            driverEntity.Status = DriverStatus.Busy;
            rideEntity.AssignedDriver = driverId;
            rideEntity.Status = RideStatus.DriverComing;
            
            _logger.LogInfo($"Assign driver {driverId} for ride {rideId}.\nUpdate ride {rideId} status on DriverComing.");

            await _driverRepo.UpdateAsync(driverEntity);
            await _rideRepo.UpdateAsync(rideEntity);
        }

        public async Task WaitForClient(Guid rideId)
        {
            var ride = await _rideRepo.GetByIdAsync(rideId);

            // TODO: Custom exception class?
            if (ride.Status != RideStatus.DriverComing)
                throw new ArgumentException("Only DriverComing -> WaitingClient sequence is correct");

            ride.Status = RideStatus.WaitingClient;
            
            _logger.LogInfo($"Update ride {rideId} status on WaitingClient.");
            
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
            
            _logger.LogInfo($"Update ride {rideId} status on InProcess.");
            
            await _rideRepo.UpdateAsync(ride);

            // TODO: Send messages to clients, if SignalR will work with us of course
        }

        public async Task EndRide(Guid rideId)
        {
            var ride = await _rideRepo.GetByIdAsync(rideId);
            var driver = await _driverRepo.GetByIdAsync(ride.AssignedDriver);

            // TODO: Custom exception class?
            if (ride.Status != RideStatus.InProcess)
                throw new ArgumentException("Only InProcess -> Finished sequence is correct");

            ride.Status = RideStatus.Finished;
            driver.Status = DriverStatus.WaitingForClient;
            
            _logger.LogInfo($"Update ride {rideId} status on Finished.");
            
            await _rideRepo.UpdateAsync(ride);
            await _driverRepo.UpdateAsync(driver);

            // TODO: Send messages to clients, if SignalR will work with us of course
        }

        public async Task CancelRide(Guid rideId)
        {
            var ride = await _rideRepo.GetByIdAsync(rideId);
            var driver = await _driverRepo.GetByIdAsync(ride.AssignedDriver);

            // TODO: Custom exception class?
            if (ride.Status != RideStatus.DriverComing)
                throw new ArgumentException("Only DriverComing -> Cancelled sequence is correct");

            ride.Status = RideStatus.Cancelled;
            driver.Status = DriverStatus.WaitingForClient;


            _logger.LogInfo($"Update ride {rideId} status on Cancelled.");
            
            await _rideRepo.UpdateAsync(ride);
            await _driverRepo.UpdateAsync(driver);

            // TODO: Send messages to clients, if SignalR will work with us of course
        }

        // TODO: Добавить какой-нить общий метод find, который по куче параметров найдёт все подходящие поездки

        public async Task<RideEntity> FindOneRide(Guid rideId)
        {
            _logger.LogInfo($"Find ride {rideId}.");
            
            return await _rideRepo.GetByIdAsync(rideId);
        }

        public IEnumerable<RideEntity> GetAllForClient(Guid clientId)
        {
            _logger.LogInfo($"Find all rides for client {clientId}.");
            
            return _rideRepo.GetWhereAsync(ride => ride.AssignedClient == clientId);
        }
    }
}